namespace MangaDexSharp.Helpers.UploadUtility;

/// <summary>
/// Represents a delegate that is called when an upload event happens
/// </summary>
/// <param name="instance">The upload instance that triggered the event</param>
public delegate void UploadDelegate(IUploadInstance instance);

/// <summary>
/// Represents a delegate that is called when an upload event happens
/// </summary>
/// <typeparam name="T">The parameter for the delegate</typeparam>
/// <param name="instance">The upload instance that triggered the event</param>
/// <param name="parameter">The parameter for the event</param>
public delegate void UploadDelegate<T>(IUploadInstance instance, T parameter);

/// <summary>
/// Represents a delegate that is called when an upload event happens
/// </summary>
/// <typeparam name="T1">The parameter for the delegate</typeparam>
/// <typeparam name="T2">The parameter for the delegate</typeparam>
/// <param name="instance">The upload instance that triggered the event</param>
/// <param name="first">The parameter for the delegate</param>
/// <param name="second">The parameter for the delegate</param>
public delegate void UploadDelegate<T1, T2>(IUploadInstance instance, T1 first, T2 second);
