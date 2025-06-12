namespace MangaDexSharp.Utilities.Download;

/// <summary>
/// Represents a delegate that is called when a download event happens
/// </summary>
/// <param name="instance">The download instance that triggered the event</param>
public delegate void DownloadDelegate(IDownloadInstance instance);

/// <summary>
/// Represents a delegate that is called when a download event happens
/// </summary>
/// <typeparam name="T">The parameter for the delegate</typeparam>
/// <param name="instance">The download instance that triggered the event</param>
/// <param name="parameter">The parameter for the event</param>
public delegate void DownloadDelegate<T>(IDownloadInstance instance, T parameter);

/// <summary>
/// Represents a delegate that is called when a download event happens
/// </summary>
/// <typeparam name="T1">The parameter for the delegate</typeparam>
/// <typeparam name="T2">The parameter for the delegate</typeparam>
/// <param name="instance">The download instance that triggered the event</param>
/// <param name="first">The parameter for the delegate</param>
/// <param name="second">The parameter for the delegate</param>
public delegate void DownloadDelegate<T1, T2>(IDownloadInstance instance, T1 first, T2 second);
