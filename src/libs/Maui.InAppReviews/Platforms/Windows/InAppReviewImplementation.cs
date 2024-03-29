using Windows.Services.Store;

// ReSharper disable once CheckNamespace
namespace Maui.InAppReviews;

/// <summary>
/// Implementation for StoreReview
/// </summary>
internal sealed class InAppReviewImplementation : IInAppReview
{
    /// <summary>
    /// Requests an app review.
    /// </summary>
    public async Task<ReviewStatus> RequestAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var context = StoreContext.GetDefault();

#if NET6_0_OR_GREATER
            if (InAppReview.Options.Window is null)
            {
                throw new InvalidOperationException("WindowObject is null. Please set the WindowObject property before calling RequestReview.");
            }
                    
            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(InAppReview.Options.Window);
                
            WinRT.Interop.InitializeWithWindow.Initialize(context, hWnd);
#endif

            var result = await context.RequestRateAndReviewAppAsync();
            
            return result.Status switch
            {
                StoreRateAndReviewStatus.Succeeded => ReviewStatus.Succeeded,
                StoreRateAndReviewStatus.CanceledByUser => ReviewStatus.CanceledByUser,
                StoreRateAndReviewStatus.Error => ReviewStatus.Error,
                StoreRateAndReviewStatus.NetworkError => ReviewStatus.NetworkError,
                _ => ReviewStatus.Error,
            };
        }
        catch(Exception ex)
        {
            InAppReview.Options.DebugAction.Invoke(ex.ToString());
            
            return ReviewStatus.Error;
        }
    }
}