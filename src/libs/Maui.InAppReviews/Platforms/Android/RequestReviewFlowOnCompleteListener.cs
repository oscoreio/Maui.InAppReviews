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
			
			var reviewInfo = (ReviewInfo)task.GetResult(Java.Lang.Class.FromType(typeof(ReviewInfo)));
			TaskCompletionSource.TrySetResult(reviewInfo);
		}
		catch (Exception ex)
		{
			TaskCompletionSource.TrySetException(ex);
		}
	}
}