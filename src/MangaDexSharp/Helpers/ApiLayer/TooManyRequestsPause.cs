namespace MangaDexSharp;

/// <summary>
/// A helper for pausing the API when too many requests are made
/// </summary>
public sealed class TooManyRequestsPause
{
	/// <summary>
	/// Stops multiple threads from checking for pauses at the same time
	/// </summary>
#if NET9_0_OR_GREATER
	private readonly Lock _lock = new();
#else
	private readonly object _lock = new();
#endif

	/// <summary>
	/// Pause the requests until this time.
	/// </summary>
	public DateTimeOffset ResumeAt { get; private set; } = DateTimeOffset.MinValue;

	/// <summary>
	/// The task that will complete when the pause is over.
	/// </summary>
	private Task _pause = Task.CompletedTask;

	/// <summary>
	/// Waits until the pause is over.
	/// </summary>
	/// <param name="token">The cancellation token for the request</param>
	public Task WaitAsync(CancellationToken token)
	{
		Task pauseTask;
		lock(_lock)
		{
			if (DateTimeOffset.UtcNow >= ResumeAt)
				return Task.CompletedTask;

			pauseTask = _pause;
		}

		return Wait(pauseTask, token);
	}

	/// <summary>
	/// Pauses the requests until the specified time.
	/// </summary>
	/// <param name="until">The time until which requests should be paused</param>
	public void Pause(DateTimeOffset until)
	{
		lock(_lock)
		{
			if (until <= ResumeAt)
				return;

			ResumeAt = until;
			var delay = until - DateTimeOffset.UtcNow;
			if (delay <= TimeSpan.Zero)
			{
				_pause = Task.CompletedTask;
				return;
			}

			_pause = Task.Delay(delay);
		}
	}

	/// <summary>
	/// Pauses the requests for the specified duration.
	/// </summary>
	/// <param name="duration">The duration for which requests should be paused</param>
	public void Pause(TimeSpan duration)
	{
		if (duration <= TimeSpan.Zero)
			return;

		Pause(DateTimeOffset.UtcNow + duration);
	}

	/// <summary>
	/// This is a shim of Task.WaitAsync for dotnet standard
	/// </summary>
	/// <param name="task">The task to wait until</param>
	/// <param name="token">The cancellation token for the request</param>
	/// <exception cref="OperationCanceledException"></exception>
	internal static async Task Wait(Task task, CancellationToken token)
	{
#if NET6_0_OR_GREATER
		await task.WaitAsync(token).ConfigureAwait(false);
		return;
#else
		if (task.IsCompleted)
		{
			await task.ConfigureAwait(false);
			return;
		}

		if (!token.CanBeCanceled)
		{
			await task.ConfigureAwait(false);
			return;
		}

		using var cts = CancellationTokenSource.CreateLinkedTokenSource(token);
		var delayTask = Task.Delay(Timeout.InfiniteTimeSpan, cts.Token);
		var completed = await Task.WhenAny(task, delayTask).ConfigureAwait(false);
		if (completed == delayTask)
			throw new OperationCanceledException(token);

		cts.Cancel();
		await task.ConfigureAwait(false);
#endif
	}
}
