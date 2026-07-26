using MusicApp.ViewModels;

namespace MusicApp.Views;

public partial class WelcomeScreen : ContentPage
{
	private readonly WelcomeScreenViewModel Pagemodel;
	public WelcomeScreen(WelcomeScreenViewModel pagemodel)
	{
		InitializeComponent();
		Pagemodel = pagemodel;
		this.BindingContext= Pagemodel;
	}
}