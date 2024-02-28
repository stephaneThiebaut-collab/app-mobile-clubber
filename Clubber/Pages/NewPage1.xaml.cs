namespace Clubber.Pages;

public partial class NewPage1 : ContentPage
{
	public NewPage1()
	{
		InitializeComponent();
		test();

    }
	public async void test()
	{
		await DisplayAlert("OK", "Ok", "Ok");
	}
}