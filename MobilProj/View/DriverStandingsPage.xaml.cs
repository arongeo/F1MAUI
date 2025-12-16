using MobilProj.ViewModel;
using System.Threading.Tasks;

namespace MobilProj.View;

public partial class DriverStandingsPage : ContentPage
{
	DriverStandingsViewModel VM;

	public DriverStandingsPage(DriverStandingsViewModel vm)
	{
		InitializeComponent();

		VM = vm;
		BindingContext = vm;
	}

	private async void Refresh_Clicked(object sender, EventArgs e) => await VM.RefreshStandings();
    protected override async void OnAppearing()
    {
		if (VM != null)
			await VM.LoadStandings();
    }

	private async void Share_Clicked(object sender, EventArgs e) => await VM.ShareStandings();
}