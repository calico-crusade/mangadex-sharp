using CardboardBox.Setup.CliParser;
using Spectre.Console;
using System.Reflection;

namespace MangaDexSharp.Utilities.Cli.Verbs;

using Services;

[Verb("default", isDefault: true, Hidden = true, HelpText = "Default verb for the CLI. This should not be used directly.")]
public class DefaultOptions
{

}

internal class DefaultVerb(
    ILogger<DefaultVerb> logger,
    ICommandLineBuilder _verbs,
    IServiceProvider _provider,
    AuthOptionsCache _cache,
    ICredentialsService _creds) : BooleanVerb<DefaultOptions>(logger)
{
    public object? GetPropertyValue(VerbOption option)
    {
        //This entire method is cursed as hell... but it works...

        object? GetText(bool allowEmpty, int? index = null)
        {
            var optional = !option.Required || allowEmpty;
            var promptText = "Text Value";
            if (index.HasValue)
                promptText += $" [#{index.Value + 1}]";
            if (optional)
                promptText += " (Optional)";

            var prompt = new TextPrompt<string>($"{promptText}: ".Escape());
            if (optional) prompt.AllowEmpty();
            if (option.Attribute.Default is not null)
                prompt
                    .DefaultValue((string)option.Attribute.Default)
                    .ShowDefaultValue();
            if (option.Secret) prompt.Secret();
            var result = AnsiConsole.Prompt(prompt);
            return string.IsNullOrWhiteSpace(result) 
                ? null
                : result.Trim();
        }

        object? GetNumber(bool allowEmpty, int? index = null, Type? type = null)
        {
            var optional = !option.Required || allowEmpty;
            var promptText = "Numeric Value";
            if (index.HasValue)
                promptText += $" [#{index.Value + 1}]";
            if (optional)
                promptText += " (Optional)";
            var prompt = new TextPrompt<double>($"{promptText}: ".Escape());
            if (optional) prompt.AllowEmpty();
            if (option.Attribute.Default is not null)
                prompt
                    .DefaultValue((double)option.Attribute.Default)
                    .ShowDefaultValue();
            if (option.Secret) prompt.Secret();
            var result = AnsiConsole.Prompt(prompt);
            if (result == default) return null;
            return Convert.ChangeType(result, type ?? option.Type);
        }

        object? GetBool(bool allowEmpty)
        {
            var optional = !option.Required || allowEmpty;
            var prompt = new TextPrompt<string>($"Boolean Value{(optional ? " (Optional)" : "")}: ".Escape());
            if (optional) prompt.AllowEmpty();
            if (option.Attribute.Default is not null)
                prompt.AddChoice("default")
                    .DefaultValue("default");
            prompt.AddChoice("true")
                .AddChoice("false")
                .ShowChoices();
            var result = AnsiConsole.Prompt(prompt);
            if (result == "true") return true;
            if (result == "false") return false;
            return option.Attribute.Default;
        }

        object? GetArray(Func<bool, int?, object?> fetcher, Type type)
        {
            var generic = typeof(List<>);
            var arrayType = generic.MakeGenericType(type); //List<string>
            var outputs = Activator.CreateInstance(arrayType);
            var method = arrayType.GetMethod("Add", [type])
                ?? throw new Exception($"Could not find Add method for type {arrayType.Name}");

            int index = 0;
            while (true)
            {
                var item = fetcher(true, index);
                if (item is null) break;

                method.Invoke(outputs, [item]);
                index++;
            }

            return index == 0
                ? null
                : outputs;
        }

        AnsiConsole.WriteLine($"{option.Name} - {option.Description}".Escape());
        if (option.Type == typeof(string))
            return GetText(false);

        Type[] numbers =
        [
            typeof(int),
            typeof(short),
            typeof(long),
            typeof(uint),
            typeof(ushort),
            typeof(ulong),
            typeof(decimal), 
            typeof(double)
        ];
        if (numbers.Contains(option.Type))
            return GetNumber(false);

        if (option.Type == typeof(bool) ||
            option.Type == typeof(bool?))
            return GetBool(false);

        if (typeof(IEnumerable<string>).IsAssignableFrom(option.Type))
            return GetArray(GetText, typeof(string));

        var generic = typeof(IEnumerable<>);
        var arrayType = numbers
            .Select(t => generic.MakeGenericType(t))
            .FirstOrDefault(t => t == option.Type);
        if (arrayType is not null)
            return GetArray((b, i) => GetNumber(b, i, arrayType), arrayType);

        return null;
    }

    public async Task<bool> RunVerb(VerbDisplay verb, CancellationToken token)
    {
        _logger.LogInformation("Running verb: {verb}", verb.Name);
        var options = Activator.CreateInstance(verb.Verb.Options);

        foreach (var option in verb.Properties)
        {
            var value = GetPropertyValue(option);
            if (value is null) continue;
            option.Property.SetValue(options, value);
        }

        var service = _provider.GetService(verb.Verb.VerbService);
        if (service is null)
        {
            _logger.LogError("No service found for verb: {verb}", verb.Name);
            return false;
        }

        var method = service.GetType()
            .GetMethod("Run", [verb.Verb.Options, typeof(CancellationToken)]);
        if (method is null)
        {
            _logger.LogError("No `Run` method found for verb: {verb}", verb.Name);
            return false;
        }

        if (method.ReturnType != typeof(Task<int>))
        {
            _logger.LogWarning("Run method does not return Task<int> for {name}", verb.Name);
            return false;
        }

        var exe = method.Invoke(service, [options, token]);
        if (exe is not Task<int> execute)
        {
            _logger.LogWarning("Could not cast run method to Task<int> for {name}", verb.Name);
            return false;
        }

        var code = await execute;
        var success = code == _verbs.ExitCodeSuccess;
        _logger.LogInformation("Verb {name} exited with code {code}, {success}", 
            verb.Name, code, success ? "Succeeded!" : "Failed!");
        return success;
    }

    public IEnumerable<VerbDisplay> Verbs()
    {
        IEnumerable<VerbOption> GetOptions(CommandLineBuilder.CommandVerb verb)
        {
            var properties = verb.Options.GetProperties();
            foreach (var prop in properties)
            {
                var attribute = prop.GetCustomAttribute<OptionAttribute>();
                if (attribute is null)
                {
                    _logger.LogWarning("Cannot find option attribute for {property} on {verb}", 
                        prop.Name, verb.Options.Name);
                    continue;
                }
                if (attribute.Hidden) continue;
                yield return new VerbOption(attribute, prop);
            }
        }

        foreach (var verb in _verbs.Verbs)
        {
            var attribute = verb.Options.GetCustomAttribute<VerbAttribute>();
            if (attribute is null)
            {
                _logger.LogWarning("Cannot find verb attribute for {verb}", verb.Options.Name);
                continue;
            }

            if (attribute.Hidden) continue;

            var properties = GetOptions(verb).ToArray();
            yield return new VerbDisplay(verb, attribute, properties);
        }
    }

    public override async Task<bool> Execute(DefaultOptions _, CancellationToken token)
    {
        var defaultActions = new Dictionary<string, Func<CancellationToken, Task<bool>>>
        {
            ["clear - Clear the console"] = (_) =>
            {
                Console.Clear();
                return Task.FromResult(true);
            },
            ["logout - Clear any existing/cached access tokens"] = async (_) =>
            {
                _cache.Auth = null;
                if (_creds is PersonalCredentialsService personal)
                    await personal.ClearCache();
                return true;
            },
            ["exit - Exit the application"] = (_) => Task.FromResult(false),
        };

        while (true)
        {
            var options = Verbs()
                .Select(t =>
                {
                    var display = t.Display();
                    var action = (CancellationToken token) => RunVerb(t, token);
                    return (display, action);
                })
                .Concat(defaultActions.Select(t => (display: t.Key, action: t.Value)));
            var (_, action) = options.ConsoleSelect("What do you want to do?", t => t.display);
            var result = await action(token);
            if (!result) return true;
        }
    }

    public record class VerbDisplay(
        CommandLineBuilder.CommandVerb Verb,
        VerbAttribute Attribute,
        VerbOption[] Properties)
    {
        public string Name => Attribute.Name;
        public string Description => Attribute.HelpText;

        public string Display()
        {
            return $"{Name} - {Description}".Escape();
        }
    }

    public record class VerbOption(
        OptionAttribute Attribute,
        PropertyInfo Property)
    {
        private bool? _secret;

        public string Name => Attribute.LongName;

        public string Description => Attribute.HelpText;

        public bool Required => Attribute.Required;

        public Type Type => Property.PropertyType;

        public bool Secret => _secret ??= Property.GetCustomAttribute<SecretAttribute>() is not null;
    }
}
