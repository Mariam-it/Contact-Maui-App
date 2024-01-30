using MyMauiApp.ViewModels;
namespace MyMauiApp.Pages;

public partial class ContactPage : ContentPage
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ContactViewModel _viewModel;
    public ContactPage(IServiceProvider serviceProvider, ContactViewModel viewModel)
    {
        InitializeComponent();
        _serviceProvider = serviceProvider;
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    /// <summary>
    /// Hanterar händelsen när "Ta bort kontakt" klickas.
    /// Tar bort en kontakt med hjälp av e-postadressen som identifierare.
    /// </summary>
    private async void OnDeleteContactClicked(object sender, EventArgs e)
    {
        var email = EmailEntry.Text;
        if (string.IsNullOrWhiteSpace(email))
        {
            await DisplayAlert("Fel", "Ange en e-postadress", "OK");
            return;
        }

        // Anropa DeleteContact på ViewModel
        bool deleteSuccess = _viewModel.DeleteContact(email);

        // Rensa textfältet efter borttagningen
        EmailEntry.Text = string.Empty;
        if (deleteSuccess)
        {
            await DisplayAlert("Borttagning", "Kontakten har tagits bort.", "OK");
        }
        else
        {
            await DisplayAlert("Fel", "Kunde inte ta bort kontakten.", "OK");
        }
    }

    /// <summary>
    /// Hanterar knappen "Lägg till" klickad händelse.
    /// Öppnar sidan för att lägga till en ny kontakt.
    /// </summary>
    private async void Btn_Add_Clicked(object sender, EventArgs e)
    {
        var contactAddPage= _serviceProvider.GetRequiredService<ContactAddPage>();
        await Navigation.PushAsync(contactAddPage);
    }
    /// <summary>
    /// Hanterar knappen "Hämta" klickad händelse.
    /// Öppnar sidan med kontaktlistan.
    /// </summary>
    private async void Btn_Get_Clicked(object sender, EventArgs e)
    {
        var contactListPage = _serviceProvider.GetRequiredService<ContactListPage>();
        await Navigation.PushAsync(contactListPage); // Navigera till den nya sidan
    }
}