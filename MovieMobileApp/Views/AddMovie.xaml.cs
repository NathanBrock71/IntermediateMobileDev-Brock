using MovieMobileApp.ViewModels;

namespace MovieMobileApp.Views;

public partial class AddMovie : ContentPage
{
	public AddMovie()
	{
		InitializeComponent();
        BindingContext = new AddMovieViewModel();
    }
}