using TaskManager.ViewModels;

namespace TaskManager.Views;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var vm = BindingContext as HomeViewModel;
        if (vm != null)
        {
            await vm.LoadData();
        }
    }
}
