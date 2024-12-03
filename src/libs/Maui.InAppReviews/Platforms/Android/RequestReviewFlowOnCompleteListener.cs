using Android.Gms.Tasks;
using Xamarin.Google.Android.Play.Core.Review;
using Task = Android.Gms.Tasks.Task;

// ReSharper disable once CheckNamespace
namespace Maui.InAppReviews;

/// <summary>
/// Implementation for Feature
/// </summary>
internal sealed class RequestReviewFlowOnCompleteListener : Java.Lang.Object, IOnCompleteListener
{
	public TaskCompletionSource<ReviewInfo> TaskCompletionSource { get; } = new();

	public void OnComplete(Task task)
	{
		if (!task.IsSuccessful)
		{
			TaskCompletionSource.TrySetCanceled();
			return;
		}

		try
		{
			if (task.GetResult(Java.Lang.Class.FromType(typeof(ReviewInfo))) is ReviewInfo reviewInfo)
			{
				TaskCompletionSource.TrySetResult(reviewInfo);
			}
			else
			{
				TaskCompletionSource.TrySetCanceled();
			}
		}
		catch (Exception ex)
		{
			TaskCompletionSource.TrySetException(ex);
		}
	}
}