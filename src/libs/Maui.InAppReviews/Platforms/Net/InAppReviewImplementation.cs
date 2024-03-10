// ReSharper disable once CheckNamespace
namespace Maui.InAppReviews;

internal sealed class InAppReviewImplementation : IInAppReview
{
	/// <summary>
	/// Requests an app review.
	/// </summary>
	public Task<ReviewStatus> RequestAsync(CancellationToken cancellationToken = default)
	{
		return Task.FromResult(ReviewStatus.NotSupported);
	}
}