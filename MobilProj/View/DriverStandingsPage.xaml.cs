using MobilProj.ViewModel;
using System.Threading.Tasks;

namespace MobilProj.View;

public partial class DriverStandingsPage : ContentPage
{
	DriverStandingsViewModel VM => BindingContext as DriverStandingsViewModel;

	public DriverStandingsPage()
	{
		InitializeComponent();
	}

	private async void Button_Clicked(object sender, EventArgs e) => await VM.LoadStandings();
}