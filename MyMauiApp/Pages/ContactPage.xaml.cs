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
    /// Hanterar h�ndelsen n�r "Ta bort kontakt" klickas.
    /// Tar bort en kontakt med hj�lp av e-postadressen som identifierare.
    /// </summary>
    private async void OnDeleteContactClicked(object sender, EventArgs e)
    {
        var email = EmailEntry.Text;
        if (string.IsNullOrWhiteSpace(email))
        {
            await DisplayAlert("Fel", "Ange en e-postadress", "OK");
            return;
        }

        // Anropa DeleteContact p� ViewModel
        bool deleteSuccess = _viewModel.DeleteContact(email);

        // Rensa textf�ltet efter borttagningen
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
    /// Hanterar knappen "L�gg till" klickad h�ndelse.
    /// �ppnar sidan f�r att l�gga till en ny kontakt.
    /// </summary>
    private async void Btn_Add_Clicked(object sender, EventArgs e)
    {
        var contactAddPage= _serviceProvider.GetRequiredService<ContactAddPage>();
        await Navigation.PushAsync(contactAddPage);
    }
    /// <summary>
    /// Hanterar knappen "H�mta" klickad h�ndelse.
    /// �ppnar sidan med kontaktlistan.
    /// </summary>
    private async void Btn_Get_Clicked(object sender, EventArgs e)
    {
        var contactListPage = _serviceProvider.GetRequiredService<ContactListPage>();
        await Navigation.PushAsync(contactListPage); // Navigera till den nya sidan
    }
}