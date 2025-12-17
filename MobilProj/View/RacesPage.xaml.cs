using MobilProj.ViewModel;

namespace MobilProj.View;

public partial class RacesPage : ContentPage
{
    RacesViewModel VM;

    public RacesPage(RacesViewModel vm)
    {
        InitializeComponent();

        VM = vm;
        BindingContext = vm;
    }

    private async void Refresh_Clicked(object sender, EventArgs e) => await VM.RefreshRaces();
    protected override async void OnAppearing()
    {
        if (VM != null)
            await VM.LoadRaces();
    }

    private async void Share_Clicked(object sender, EventArgs e) => await VM.ShareRaces();
}