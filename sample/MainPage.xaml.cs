using CommunityToolkit.Mvvm.Input;

namespace Maui.InAppReviews.SampleApp;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
		BindingContext = this;
	}

	[RelayCommand]
	private async Task RequestReview()
	{
		ReviewStatus status = await InAppReview.Current.RequestAsync();
		
		await DisplayAlert("Review Status", status.ToString(), "Ok");
	}
}

