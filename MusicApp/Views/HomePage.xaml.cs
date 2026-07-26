using MusicApp.ViewModels;

namespace MusicApp.Views;

public partial class HomePage : ContentPage
{
	protected readonly HomePageViewModel Pagemodel;
	public HomePage(HomePageViewModel pagemodel)
	{
		InitializeComponent();
		Pagemodel= pagemodel;
		this.BindingContext= Pagemodel;
	}
}