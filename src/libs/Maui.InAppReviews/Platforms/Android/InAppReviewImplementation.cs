using Xamarin.Google.Android.Play.Core.Review;
using Xamarin.Google.Android.Play.Core.Review.Testing;
using CancellationToken = System.Threading.CancellationToken;

// ReSharper disable once CheckNamespace
namespace Maui.InAppReviews;

/// <summary>
/// Implementation for Feature
/// </summary>
internal sealed class InAppReviewImplementation : IInAppReview
{
	private IReviewManager? _manager;
	
	/// <summary>
	/// Requests an app review.
	/// </summary>
	public async Task<ReviewStatus> RequestAsync(CancellationToken cancellationToken = default)
	{
		_manager ??= InAppReview.Options.UseFakeReviewManager
			? new FakeReviewManager(Platform.AppContext)
			: ReviewManagerFactory.Create(Platform.AppContext);

		ReviewInfo reviewInfo;
		using (var requestReviewFlow = new RequestReviewFlowOnCompleteListener())
		{
			_manager.RequestReviewFlow().AddOnCompleteListener(requestReviewFlow);
			
			reviewInfo = await requestReviewFlow.TaskCompletionSource.Task.ConfigureAwait(true);
		}
		
		using (var launchReviewFlow = new LaunchReviewFlowOnCompleteListener())
		{
			_manager.LaunchReviewFlow(
				Platform.CurrentActivity ??
				throw new InvalidOperationException("Current Activity is null, ensure that the MainActivity.cs file is configuring Essentials in your source code so the StoreReview can use it."),
				reviewInfo).AddOnCompleteListener(launchReviewFlow);
			
			var status = await launchReviewFlow.TaskCompletionSource.Task.ConfigureAwait(true);
			
			return status
				? ReviewStatus.Succeeded
				: ReviewStatus.Error;
		}
	}
}