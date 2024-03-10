namespace Maui.InAppReviews;

/// <summary>
/// Interface for in-app reviews.
/// </summary>
public interface IInAppReview
{
	/// <summary>
	/// Requests an app review.
	/// </summary>
	Task<ReviewStatus> RequestAsync(CancellationToken cancellationToken = default);
}