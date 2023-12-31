namespace MyMauiApp.Pages;

public partial class ContactPage : ContentPage
{
    private readonly IServiceProvider _serviceProvider;
	public ContactPage(IServiceProvider serviceProvider)
    {
        InitializeComponent();
        _serviceProvider = serviceProvider;
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

        var contactService = _serviceProvider.GetRequiredService<Shared.Services.ContactService>();
        var contactToDelete = contactService.GetOne(c => c.Email == email);

        if (contactToDelete != null && !string.IsNullOrWhiteSpace(contactToDelete.Email))
        {
            contactService.Delete(contactToDelete);
            await DisplayAlert("Borttagning", "Kontakten har tagits bort", "OK");

            EmailEntry.Text = string.Empty;
        }
        else
        {
            await DisplayAlert("Fel", "Kontakten hittades inte", "OK");
        }
    }

}