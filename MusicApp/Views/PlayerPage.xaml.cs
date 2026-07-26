
using Music2._0.ViewModels;
using MusicApp.ViewModels;

namespace MusicApp.Views;

public partial class PlayerPage : ContentPage
{
	protected readonly PlayerPageViewModel PageModel;
	public PlayerPage(PlayerPageViewModel pagemodel)
	{
		InitializeComponent();
		PageModel= pagemodel;
		this.BindingContext= PageModel;
	}
}