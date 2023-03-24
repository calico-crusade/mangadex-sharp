using System.IO;

namespace MangaDexSharp;

/// <summary>
/// Represents a file that is to be uploaded to the chapter
/// </summary>
public interface IFileUpload
{
	/// <summary>
	/// The name of the file
	/// </summary>
	string FileName { get; set; }

	/// <summary>
	/// How to get the byte content of the file
	/// </summary>
	/// <returns></returns>
	Task<byte[]> Content();
}

/// <summary>
/// Represents a files raw byte content
/// </summary>
public class RawFileUpload : IFileUpload
{
	/// <summary>
	/// The name of the file
	/// </summary>
	public string FileName { get; set; }

	/// <summary>
	/// The byte data of the content
	/// </summary>
	public byte[] Data { get; set; }

	/// <summary>
	/// Represents a files raw byte content
	/// </summary>
	/// <param name="fileName">The name of the file</param>
	/// <param name="data">The byte data of the content</param>
	public RawFileUpload(string fileName, byte[] data)
	{
		FileName = fileName;
		Data = data;
	}

	/// <summary>
	/// Returns the <see cref="Data"/> property
	/// </summary>
	/// <returns></returns>
	public Task<byte[]> Content() => Task.FromResult(Data);
}

/// <summary>
/// Represents a files stream content
/// </summary>
public class StreamFileUpload : IFileUpload
{
	/// <summary>
	/// The name of the file
	/// </summary>
	public string FileName { get; set; }

	/// <summary>
	/// The stream of the data
	/// </summary>
	public Stream Data { get; set; }

	/// <summary>
	/// Represents a files stream content
	/// </summary>
	/// <param name="fileName">The name of the file</param>
	/// <param name="data">The stream of the data</param>
	public StreamFileUpload(string fileName, Stream data)
	{
		FileName = fileName;
		Data = data;
	}

	/// <summary>
	/// Returns a byte array version of the stream content
	/// </summary>
	/// <returns></returns>
	public async Task<byte[]> Content()
	{
		using var ms = new MemoryStream();
		await Data.CopyToAsync(ms);
		return ms.ToArray();
	}
}

/// <summary>
/// Represents an in-memory stream content
/// </summary>
public class MemoryStreamFileUpload : IFileUpload
{
	/// <summary>
	/// The name of the file
	/// </summary>
	public string FileName { get; set; }

	/// <summary>
	/// The stream of the data
	/// </summary>
	public MemoryStream Data { get; set; }

	/// <summary>
	/// Represents an in-memory stream content
	/// </summary>
	/// <param name="fileName">The name of the file</param>
	/// <param name="data">The stream of the data</param>
	public MemoryStreamFileUpload(string fileName, MemoryStream data)
	{
		FileName = fileName;
		Data = data;
	}

	/// <summary>
	/// Returns the byte array version of the stream content
	/// </summary>
	/// <returns></returns>
	public Task<byte[]> Content() => Task.FromResult(Data.ToArray());
}
