
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
    private void ProgressSlider_DragCompleted(
    object sender,
    EventArgs e)
    {
        if (sender is Slider slider)
        {
            if (BindingContext is PlayerPageViewModel viewModel)
            {
                viewModel.SeekCommand.Execute(slider.Value);
            }
        }
    }
}