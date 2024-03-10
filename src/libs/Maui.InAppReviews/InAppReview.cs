namespace Maui.InAppReviews;

/// <summary>
/// Cross platform in-app reviews implementations
/// </summary>
public static class InAppReview
{
	private static IInAppReview? _currentImplementation;

	/// <summary>
	/// Options for the <see cref="IInAppReview"/>.
	/// </summary>
	public static InAppReviewsOptions Options { get; set; } = new();
    
	/// <summary>
	/// Provides the default implementation for static usage of this API.
	/// </summary>
	public static IInAppReview Current =>
		_currentImplementation ??= new InAppReviewImplementation();
}