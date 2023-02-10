using System.IO;

namespace MangaDexSharp;

public interface IFileUpload
{
	string FileName { get; set; }
	Task<byte[]> Content();
}


public class RawFileUpload : IFileUpload
{
	public string FileName { get; set; }

	public byte[] Data { get; set; }

	public RawFileUpload(string fileName, byte[] data)
	{
		FileName = fileName;
		Data = data;
	}

	public Task<byte[]> Content() => Task.FromResult(Data);
}

public class StreamFileUpload : IFileUpload
{
	public string FileName { get; set; }

	public Stream Data { get; set; }

	public StreamFileUpload(string fileName, Stream data)
	{
		FileName = fileName;
		Data = data;
	}

	public async Task<byte[]> Content()
	{
		using var ms = new MemoryStream();
		await Data.CopyToAsync(ms);
		return ms.ToArray();
	}
}

public class MemoryStreamFileUpload : IFileUpload
{
	public string FileName { get; set; }

	public MemoryStream Data { get; set; }

	public MemoryStreamFileUpload(string fileName, MemoryStream data)
	{
		FileName = fileName;
		Data = data;
	}

	public Task<byte[]> Content() => Task.FromResult(Data.ToArray());
}
