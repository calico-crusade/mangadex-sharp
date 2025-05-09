using System.IO;
using System.Threading;

namespace MangaDexSharp;

/// <summary>
/// Represents a file that is to be uploaded to the chapter
/// </summary>
public interface IFileUpload : IDisposable
{
	/// <summary>
	/// The name of the file
	/// </summary>
	string FileName { get; }

	/// <summary>
	/// How to get the byte content of the file
	/// </summary>
    /// <param name="token">The cancellation token for loading the data</param>
	/// <returns>The byte content of the file</returns>
	Task<byte[]> Content(CancellationToken token);
}

/// <summary>
/// Represents a files raw byte content
/// </summary>
/// <param name="fileName">The name of the file</param>
/// <param name="data">The byte data of the content</param>
public class RawFileUpload(string fileName, byte[] data) : IFileUpload
{
    /// <summary>
    /// The name of the file
    /// </summary>
    public string FileName { get; set; } = fileName;

    /// <summary>
    /// The byte data of the content
    /// </summary>
    public byte[] Data { get; set; } = data;

    /// <summary>
    /// How to get the byte content of the file
    /// </summary>
    /// <param name="_">The cancellation token for loading the data</param>
    /// <returns>The byte content of the file</returns>
    public Task<byte[]> Content(CancellationToken _) => Task.FromResult(Data);

    /// <summary>
    /// Disposes the <see cref="Data"/> property
    /// </summary>
    public void Dispose() 
	{
		Data = [];
		GC.SuppressFinalize(this); 
	}
}

/// <summary>
/// Represents a files stream content
/// </summary>
/// <param name="fileName">The name of the file</param>
/// <param name="data">The stream of the data</param>
/// <param name="leaveOpen">Whether or not to leave the given stream open after processing</param>
/// <remarks>
/// Warning; using this class can cause issues if request retries are enabled
/// (like in the case of using upload sessions). So it is preferably to use
/// <see cref="MemoryStreamFileUpload"/> instead as those can be rewound.
/// </remarks>
public class StreamFileUpload(string fileName, Stream data, bool leaveOpen = false) : IFileUpload
{
    /// <summary>
    /// The name of the file
    /// </summary>
    public string FileName { get; set; } = fileName;

    /// <summary>
    /// The stream of the data
    /// </summary>
    public Stream Data { get; set; } = data;

    /// <summary>
    /// Returns a byte array version of the stream content
    /// </summary>
    /// <param name="token">The cancellation token for loading the data</param>
	/// <returns>The byte content of the stream</returns>
    public async Task<byte[]> Content(CancellationToken token)
	{
		using var ms = new MemoryStream();
		await Data.CopyToAsync(ms, token);
		return ms.ToArray();
    }

    /// <summary>
    /// Disposes the <see cref="Data"/> property
    /// </summary>
    public void Dispose()
    {
        if (!leaveOpen) Data.Dispose();
        GC.SuppressFinalize(this);
    }
}

/// <summary>
/// Represents an in-memory stream content
/// </summary>
/// <param name="fileName">The name of the file</param>
/// <param name="data">The stream of the data</param>
public class MemoryStreamFileUpload(string fileName, MemoryStream data) : IFileUpload
{
    /// <summary>
    /// The name of the file
    /// </summary>
    public string FileName { get; set; } = fileName;

    /// <summary>
    /// The stream of the data
    /// </summary>
    public MemoryStream Data { get; set; } = data;

    /// <summary>
    /// Returns the byte array version of the stream content
    /// </summary>
    /// <param name="_">The cancellation token for loading the data</param>
	/// <returns>The byte content of the file</returns>
    public Task<byte[]> Content(CancellationToken _)
    {
        Data.Position = 0;
        var result = Data.ToArray();
        return Task.FromResult(result);
    }

    /// <summary>
    /// Disposes the <see cref="Data"/> property
    /// </summary>
    public void Dispose()
    {
		Data.Dispose();
        GC.SuppressFinalize(this);
    }
}

/// <summary>
/// Represents a file from the disk
/// </summary>
/// <param name="path">The path to file</param>
public class PathFileUpload(string path) : IFileUpload
{
    /// <summary>
    /// The name of the file
    /// </summary>
    public string FileName => Path.GetFileName(path);

    /// <summary>
    /// Returns the byte array version of the stream content
    /// </summary>
    /// <param name="token">The cancellation token for loading the data</param>
	/// <returns>The byte content of the file</returns>
    public async Task<byte[]> Content(CancellationToken token)
    {
        using var ms = new MemoryStream();
        using var io = File.OpenRead(path);
        await io.CopyToAsync(ms, token);
        return ms.ToArray();
    }

    /// <summary>
    /// Disposes the upload instance
    /// </summary>
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}