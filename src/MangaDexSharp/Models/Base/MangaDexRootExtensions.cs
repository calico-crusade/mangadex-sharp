namespace MangaDexSharp;

/// <summary>
/// A few helpful extension methods for <see cref="MangaDexRoot"/> models
/// </summary>
public static class MangaDexRootExtensions
{
    /// <summary>
    /// All of the possible error results
    /// </summary>
    public static readonly string[] ResultErrors = new[] { MangaDexRoot.RESULT_ERROR, MangaDexRoot.RESULT_KO };

    /// <summary>
    /// Converts the <see cref="MangaDexError"/> to a string
    /// </summary>
    /// <param name="error">The error</param>
    /// <returns>The error string</returns>
    public static string CompileError(MangaDexError error)
    {
        return $"{error.Title} [{error.Status} - #{error.Id}]: {error.Detail}";
    }

    /// <summary>
    /// Converts all of the errors to a string
    /// </summary>
    /// <param name="errors">The errors to convert</param>
    /// <returns>The error string</returns>
    public static string? CompileErrors(params MangaDexError[] errors)
    {
        return errors.Length == 0
            ? null 
            : string.Join(Environment.NewLine, errors.Select(CompileError));
    }

    /// <summary>
    /// Determines if the given result has an error
    /// </summary>
    /// <param name="root">The API return result</param>
    /// <returns>Whether or not the result has an error</returns>
    public static bool IsError(this MangaDexRoot root)
    {
        return root.Errors.Length > 0 ||
            ResultErrors.Contains(root.Result.ToLower());
    }

    /// <summary>
    /// Determines if the given result has an error
    /// </summary>
    /// <param name="root">The API return result</param>
    /// <param name="error">The error message</param>
    /// <returns>Whether or not the result has an error</returns>
    public static bool IsError(this MangaDexRoot root, out string error)
    {
        error = string.Empty;
        if (root.IsError())
        {
            error = CompileErrors(root.Errors) ?? "An unknown error occurred!";
            return true;
        }

        return false;
    }

    /// <summary>
    /// Determines if the given result has an error
    /// </summary>
    /// <typeparam name="T">The type of result data</typeparam>
    /// <param name="root">The API return result</param>
    /// <param name="error">The error message</param>
    /// <param name="data">The result data</param>
    /// <returns>Whether or not the result has an error</returns>
    public static bool IsError<T>(this MangaDexRoot<T> root, out string error, out T data)
        where T: new()
    {
        data = root.Data;
        return root.IsError(out error);
    }

    /// <summary>
    /// Determines if the given result has an error
    /// </summary>
    /// <typeparam name="T">The structure type</typeparam>
    /// <param name="root">The API return result</param>
    /// <param name="error">The error message</param>
    /// <param name="data">The result data</param>
    /// <returns>Whether or not the result has an error</returns>
    public static bool IsError<T>(this MangaDexStruct<T> root, out string error, out T data)
    {
        data = root.Data!;
        return root.IsError(out error);
    }
}
