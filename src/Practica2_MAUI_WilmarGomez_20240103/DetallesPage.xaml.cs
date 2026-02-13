namespace Practica2_MAUI_WilmarGomez_20240103;

public partial class DetallesPage : ContentPage
{
	public DetallesPage()
	{
		InitializeComponent();
	}

	private async void OnRegresarClicked(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
	}
}
