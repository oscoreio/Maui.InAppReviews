using Microsoft.Maui.LifecycleEvents;

namespace Maui.InAppReviews;

/// <summary>
/// This class contains the extension method to enable the in-app updates for android.
/// </summary>
public static class MauiAppBuilderExtensions
{
    /// <summary>
    /// This method will enable the in-app reviews for android and ios.
    /// Set debugMode to true to enable the fake app update manager.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="setupAction"></param>
    /// <returns></returns>
    public static MauiAppBuilder UseInAppReviews(
        this MauiAppBuilder builder,
        Action<InAppReviewsOptions>? setupAction = null) 
    {
        builder = builder ?? throw new ArgumentNullException(nameof(builder));
        
        setupAction?.Invoke(InAppReview.Options);
        
#if WINDOWS
        builder
            .ConfigureLifecycleEvents(static events =>
            {
                events.AddWindows(static windows =>
                {
                    windows
                        .OnWindowCreated(window => InAppReview.Options.Window ??= window)
                        ;
                });
            });
#endif
        
        return builder;
    }
}
