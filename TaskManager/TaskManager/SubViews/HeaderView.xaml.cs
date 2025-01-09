using System.Runtime.CompilerServices;
using TaskManager.Views;

namespace TaskManager.SubViews;

public partial class HeaderView : ContentView
{
	public HeaderView()
	{
		InitializeComponent();
	}
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(HeaderView), string.Empty);

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public static readonly BindableProperty BackImageVisibleProperty = BindableProperty.Create(nameof(BackImageVisible), typeof(bool), typeof(HeaderView), false);

    public bool BackImageVisible
    {
        get => (bool)GetValue(BackImageVisibleProperty);
        set => SetValue(BackImageVisibleProperty, value);
    }

    public static readonly BindableProperty PlusVisibleProperty = BindableProperty.Create(nameof(PlusImageVisible), typeof(bool), typeof(HeaderView), false);

    public bool PlusImageVisible
    {
        get => (bool)GetValue(PlusVisibleProperty);
        set => SetValue(PlusVisibleProperty, value);
    }

    protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);
        if (propertyName == nameof(Title))
        {
            HeaderTitle.Text = Title;
        }
        if (propertyName == nameof(BackImageVisible))
        {
            LeftImage.IsVisible = BackImageVisible;
        }
        if (propertyName == nameof(PlusImageVisible))
        {
            RightImage.IsVisible = PlusImageVisible;
        }
    }
    async void LeftImage_Clicked(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
        await App.Current.MainPage.Navigation.PopAsync(false);
    }

    async void RightImage_Clicked(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
        await App.Current.MainPage.Navigation.PushAsync(new AddEditTasksPage());
    }
}
