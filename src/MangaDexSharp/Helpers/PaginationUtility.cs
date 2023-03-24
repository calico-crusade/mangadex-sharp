using System.Runtime.CompilerServices;
using System.Threading;

namespace MangaDexSharp;

/// <summary>
/// A utility for fetching all of the results of a paginated list, taking rate-limits into consideration.
/// </summary>
/// <typeparam name="T">The return results of the request</typeparam>
public abstract class PaginationUtility<T>
{
	/// <summary>
	/// How long to wait after hitting the request cap
	/// </summary>
	public virtual int Delay { get; set; } = 3 * 1000;

	/// <summary>
	/// How many requests to do before waiting to avoid rate limits
	/// </summary>
	public virtual int Cap { get; set; } = 3;
	
	/// <summary>
	/// How to request the data for this endpoint
	/// </summary>
	/// <param name="offset">How many items to skip in the current request</param>
	/// <param name="token">The cancellation token to ensure the requests can be cancelled.</param>
	/// <returns>The results of the request and the current batch size and total number of records</returns>
	public abstract Task<(T[] result, int limit, int total)> Request(int offset,  CancellationToken token);

	/// <summary>
	/// Executes the <see cref="Request(int, CancellationToken)"/> method recursively until all of the records have been fetched.
	/// </summary>
	/// <param name="offset">The current number of items to skip</param>
	/// <param name="total">The total number of requests made before the next cap is hit</param>
	/// <param name="token">The cancellation token to ensure the requests can be cancelled</param>
	/// <returns>All of the items from the recursive request</returns>
	public async IAsyncEnumerable<T> Recurse(int offset, int total, [EnumeratorCancellation] CancellationToken token)
	{
		//Ensure the task isn't cancelled
		if (token.IsCancellationRequested) yield break;

		//Convert to the output type
		var (items, trackLimit, trackTotal) = await Request(offset, token);

		//Ensure we have items to return 
		if (items.Length == 0) yield break;

		//Return the current set
		foreach(var item in items) yield return item;

		//Ensure there are more items to request
		if (trackTotal <= offset + trackLimit) yield break;

		//Avoid rate limits with cap check & delay
		if (total >= Cap)
		{
			Console.WriteLine("Waiting...");
			await Task.Delay(Delay, token);
			total = 0;
		}

		//Ensure the task isn't cancelled
		if (token.IsCancellationRequested) yield break;

		//Get the next set of data
		var next = Recurse(offset + trackLimit, total + 1, token);

		//Return all items from said set
		await foreach(var item in next) yield return item;
	}

	/// <summary>
	/// Gets all of the items from the request
	/// </summary>
	/// <param name="token">The cancellation token to ensure the requests can be cancelled</param>
	/// <returns>All of the items from the request</returns>
	public IAsyncEnumerable<T> All(CancellationToken? token = null)
	{
		return Recurse(0, 0, token ?? CancellationToken.None);
	}
}

/// <summary>
/// A paginated utility targetted towards <see cref="MangaDexCollection{T}"/> types
/// </summary>
/// <typeparam name="TResult">The base implementaion of <see cref="MangaDexCollection{T}"/></typeparam>
/// <typeparam name="TSource">The result type from the collection</typeparam>
/// <typeparam name="TFilter">The <see cref="IPaginateFilter"/> object used for the request</typeparam>
public class PaginationUtility<TResult, TSource, TFilter> : PaginationUtility<TSource>
	where TFilter : IPaginateFilter
	where TResult : MangaDexCollection<TSource>
{
	/// <summary>
	/// Represents the filter used for the requests
	/// </summary>
	public TFilter Filter { get; }

	/// <summary>
	/// The underlying request to the API
	/// </summary>
	public Func<TFilter, CancellationToken, Task<TResult>> RequestFn { get; }

	/// <summary>
	/// A paginated utility targetted towards <see cref="MangaDexCollection{T}"/> types
	/// </summary>
	/// <param name="filter">Represents the filter used for the requests</param>
	/// <param name="request">The underlying request to the API</param>
	public PaginationUtility(TFilter filter, Func<TFilter, CancellationToken, Task<TResult>> request)
	{
		Filter = filter;
		RequestFn = request;
	}

	/// <summary>
	/// Implements the <see cref="PaginationUtility{T}.Request(int, CancellationToken)"/> method for <see cref="MangaDexCollection{T}"/> types
	/// </summary>
	/// <param name="offset">How many items to skip in the current request</param>
	/// <param name="token">The cancellation token to ensure the requests can be cancelled.</param>
	/// <returns>The results of the request and the current batch size and total number of records</returns>
	public override async Task<(TSource[] result, int limit, int total)> Request(int offset, CancellationToken token)
	{
		token.ThrowIfCancellationRequested();
		Filter.Offset = offset;
		var result = await RequestFn(Filter, token);
		return (result.Data.ToArray(), result.Limit, result.Total);
	}
}