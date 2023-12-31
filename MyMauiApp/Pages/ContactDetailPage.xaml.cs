namespace MyMauiApp.Pages;

public partial class ContactDetailPage : ContentPage
{
    private readonly IServiceProvider _serviceProvider;
    public ContactDetailPage(IServiceProvider serviceProvider)
    {
        InitializeComponent();
        _serviceProvider = serviceProvider;
    }
    /// <summary>
    /// S�tter kontaktobjektet som DataContext f�r sidan.
    /// </summary>
    /// <param name="contact">Kontaktobjektet som ska visas.</param>
    public void SetContact(Shared.Models.Contact contact)
    {
        BindingContext = contact;
    }
    /// <summary>
    /// Hanterar h�ndelsen n�r knappen "Uppdatera kontakt" klickas.
    /// Uppdaterar kontaktinformationen och hanterar eventuella fel.
    /// </summary>
    private async void OnUpdateContactClicked(object sender, EventArgs e)
    {
        if (BindingContext is Shared.Models.Contact contact)
        {
            var contactService = _serviceProvider.GetRequiredService<Shared.Services.ContactService>();
            try
            {
                // F�rs�k att uppdatera kontaktinformationen
                contactService.Update(contact);

                // Visa en bekr�ftelse
                await DisplayAlert("Uppdatering", "Kontaktinformationen har uppdaterats", "OK");
            }
            catch (Exception ex)
            {
                // Felhantering: Visa ett felmeddelande och logga detaljerna om felet
                await DisplayAlert("Fel", "Ett fel uppstod vid uppdatering av kontaktinformationen.", "OK");
                Console.WriteLine($"Fel vid uppdatering av kontakt: {ex.Message}");
            }
        }
        else
        {
            await DisplayAlert("Fel", "Ingen kontakt att uppdatera", "OK");
        }
    }

}