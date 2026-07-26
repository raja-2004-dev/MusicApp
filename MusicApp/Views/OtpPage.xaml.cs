using MusicApp.ViewModels;

namespace MusicApp.Views;

public partial class OtpPage : ContentPage
{
	protected readonly OtpPageViewModel Pagemodel;
	public OtpPage(OtpPageViewModel pagemodel)
	{
		InitializeComponent();
		Pagemodel = pagemodel;
		this.BindingContext = Pagemodel;
	}
}