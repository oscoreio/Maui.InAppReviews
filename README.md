# Maui.InAppReviews

[![Nuget package](https://img.shields.io/nuget/vpre/Oscore.Maui.InAppReviews)](https://www.nuget.org/packages/Oscore.Maui.InAppReviews/)
[![CI/CD](https://github.com/oscoreio/Maui.InAppReviews/actions/workflows/dotnet.yml/badge.svg?branch=main)](https://github.com/oscoreio/Maui.InAppReviews/actions/workflows/dotnet.yml)
[![License: MIT](https://img.shields.io/github/license/oscoreio/Maui.InAppReviews)](https://github.com/oscoreio/Maui.InAppReviews/blob/main/LICENSE)

NuGet package that implementing native In-App Reviews for MAUI with debugging capabilities.
![InAppReviews](https://developer.android.com/static/images/google/play/in-app-review/iar-flow.jpg)

You also can use [AppStoreInfo](https://github.com/oscoreio/Maui.AppStoreInfo) to open review page in the store.

### Supported Platforms
| Platform | Minimum Version Supported             |
|----------|---------------------------------------|
| iOS      | 12.2+                                 |
| macOS    | 15+                                   |
| Android  | 5.0 (API 21)                          |
| Windows  | 11 and 10 version 1809+ (build 17763) |
> [!TIP]
> Also works successfully on iOS 18+/macOS 18+ - there was an API change here

# Usage
- Add NuGet package to your project:
```xml
<PackageReference Include="Oscore.Maui.InAppReviews" Version="1.2.0" />
```
- Add the following to your `MauiProgram.cs` `CreateMauiApp` method:
```diff
builder
    .UseMauiApp<App>()
+   .UseInAppReviews()
    .ConfigureFonts(fonts =>
    {
        fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
        fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
    });
```

```cs
ReviewStatus status = await InAppReview.Current.RequestAsync();
```


# Links
- https://developer.android.com/guide/playcore/in-app-review
- https://github.com/jamesmontemagno/StoreReviewPlugin
- [Requesting Reviews with iOS 10.3’s SKStoreReviewController](https://devblogs.microsoft.com/xamarin/requesting-reviews-ios-10-3s-skstorereviewcontroller/?WT.mc_id=friends-0000-jamont)  
- [In-app reviews for your Android apps](https://devblogs.microsoft.com/xamarin/android-in-app-reviews/?WT.mc_id=friends-0000-jamont)  

### Testing & Debugging issues

#### iOS

* You cannot submit a review on iOS while developing, but the review popup dialog displays in your simulator/device.
* However, when you download the app from Testflight, the popup dialog does not display at all, as [mentioned here](https://developer.apple.com/documentation/storekit/skstorereviewcontroller/2851536-requestreview):
> When you call this method while your app is still in development mode, a rating/review request view is always displayed so that you can test the user interface and experience. However, this method has no effect when you call it in an app that you distribute using TestFlight."

#### Android

* Unlike iOS, you cannot see the review popup dialog while developing or if you distribute it manually. As you can [see here](https://developer.android.com/guide/playcore/in-app-review/test), you have to download the app from the Play Store to see the popup. I recommend using Android Play Store's [“Internal App Sharing”](https://play.google.com/console/about/internalappsharing/) feature to test.
* Occasionally, some devices may not show the popup at all as [seen here](https://github.com/jamesmontemagno/StoreReviewPlugin/pull/27#issuecomment-877410136). One way to test whether your device is affected by it, is by downloading [this game that uses v3.1 of this nuget, target SDK version 30, target framework v11.0](https://play.google.com/store/apps/details?id=com.tfp.numberbomb) and win the game once to see the popup. Additionally, you can debug the error using adb locat, as you can [see here](https://github.com/jamesmontemagno/StoreReviewPlugin/issues/26#issue-940942211)
* The [most common issue/crash type](https://github.com/jamesmontemagno/StoreReviewPlugin/issues/20) is that developers release the app in the release configuration but they only test in the debug configuration. They do not realize that they have set Linker behavior to `Link SDK assemblies only`/`Link all`, and did not follow the proguard steps mentioned above
