namespace MangaDexSharp;

/// <summary>
/// Represents an exception that occurred while using the MangaDex API
/// </summary>
public class MangaDexException(MangaDexRoot result) 
    : Exception(
        MangaDexRootExtensions.CompileErrors(result.Errors) 
        ?? "An unknown error occurred!")
{
    /// <summary>
    /// The result that caused the error
    /// </summary>
    public MangaDexRoot Result { get; } = result;
}
