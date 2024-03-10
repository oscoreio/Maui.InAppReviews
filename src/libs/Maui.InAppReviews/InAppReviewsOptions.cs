namespace Maui.InAppReviews;

/// <summary>
/// Represents options for in-app reviews on Android.
/// </summary>
public class InAppReviewsOptions
{
    /// <summary>
    /// Set this to true to use the test fake review manager. <br/>
    /// </summary>
    public bool UseFakeReviewManager { get; set; }
    
    /// <summary>
    /// Set this to the window object to use for the review request for Windows. <br/>
    /// </summary>
    public object? Window { get; set; }
    
    /// <summary>
    /// This action will be triggered when debug event occurs. <br/>
    /// Default action will write the text to the debug output. <br/>
    /// </summary>
    public Action<string> DebugAction { get; set; } = static text =>
        System.Diagnostics.Debug.WriteLine(text);
}