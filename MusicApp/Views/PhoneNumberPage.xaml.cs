using MusicApp.ViewModels;

namespace MusicApp.Views;

public partial class PhoneNumberPage : ContentPage
{
	protected readonly PhoneNumberViewModel Pagemodel;
	public PhoneNumberPage(PhoneNumberViewModel pagemodel)
	{
		InitializeComponent();
		Pagemodel = pagemodel;
		this.BindingContext= Pagemodel;
	}
}