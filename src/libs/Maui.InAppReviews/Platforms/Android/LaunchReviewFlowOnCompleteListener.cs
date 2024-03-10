using Android.Gms.Tasks;
using Task = Android.Gms.Tasks.Task;

// ReSharper disable once CheckNamespace
namespace Maui.InAppReviews;

/// <summary>
/// Implementation for Feature
/// </summary>
internal sealed class LaunchReviewFlowOnCompleteListener : Java.Lang.Object, IOnCompleteListener
{
	public TaskCompletionSource<bool> TaskCompletionSource { get; } = new();
	
	public void OnComplete(Task task)
	{
		TaskCompletionSource.TrySetResult(task.IsSuccessful);
	}
}