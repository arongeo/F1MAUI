using MobilProj.ViewModel;

namespace MobilProj.View;

public partial class DriverView : ContentPage
{
	DriverViewModel VM => BindingContext as DriverViewModel;

	public DriverView()
	{
		InitializeComponent();
	}

}