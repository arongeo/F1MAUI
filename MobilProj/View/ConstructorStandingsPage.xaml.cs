using MobilProj.ViewModel;

namespace MobilProj.View;

public partial class ConstructorStandingsPage : ContentPage
{
	ConstructorStandingsViewModel VM;

	public ConstructorStandingsPage(ConstructorStandingsViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;
		VM = vm;
	}

    protected override async void OnAppearing()
    {
        if (VM != null)
            await VM.LoadStandings();
    }

	private async void Button_Clicked(object sender, EventArgs e) => await VM.RefreshStandings();
}