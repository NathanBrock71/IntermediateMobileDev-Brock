using MappingApp.ViewModels;

namespace MappingApp;

public partial class SavedRoutesPage : ContentPage
{
	public SavedRoutesPage()
	{
		InitializeComponent();
        BindingContext = new SavedRoutesPageViewModel();
    }
}