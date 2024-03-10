using UIKit;
using StoreKit;

// ReSharper disable once CheckNamespace
namespace Maui.InAppReviews;

#pragma warning disable CA1416 // Strange warning because here checks the version of the OS

internal sealed class InAppReviewImplementation : IInAppReview
{
	/// <summary>
	/// Requests an app review.
	/// </summary>
	public Task<ReviewStatus> RequestAsync(CancellationToken cancellationToken = default)
	{
		if (OperatingSystem.IsMacCatalystVersionAtLeast(14) ||
		    OperatingSystem.IsIOSVersionAtLeast(14))
		{
			if (UIApplication.SharedApplication.ConnectedScenes
				    .ToArray<UIScene>()?
				    .FirstOrDefault(x => x.ActivationState == UISceneActivationState.ForegroundActive) is UIWindowScene windowScene)
			{
				SKStoreReviewController.RequestReview(windowScene);
					
				return Task.FromResult(ReviewStatus.Succeeded);
			}
		}
		else
		{
			SKStoreReviewController.RequestReview();
				
			return Task.FromResult(ReviewStatus.Succeeded);
		}

		return Task.FromResult(ReviewStatus.NotSupported);
	}
}