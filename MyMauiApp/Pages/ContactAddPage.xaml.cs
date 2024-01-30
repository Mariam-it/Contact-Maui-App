using Shared.Services;

namespace MyMauiApp.Pages
{
    public partial class ContactAddPage : ContentPage
    {
        private readonly IContactService _contactService;

        public ContactAddPage(IContactService contactService)
        {
            InitializeComponent();
            _contactService = contactService;
        }
        /// <summary>
        /// Hanterar h�ndelsen n�r knappen "L�gg till kontakt" klickas.
        /// L�gger till en ny kontakt med informationen fr�n inmatningsf�lten.
        /// </summary>
        private async void AddContact(object sender, EventArgs e)
        {
            var newContact = new Shared.Models.Contact
            {
                FirstName = FirstNameEntry.Text,
                LastName = LastNameEntry.Text,
                PhoneNumber = PhoneNumberEntry.Text,
                Email = EmailEntry.Text,
                Address = AddressEntry.Text
            };
            // L�gg till den nya kontakten i kontaktlistan
            
            _contactService.AddContactToList(newContact);

            // Rensa inmatningsf�lten
            FirstNameEntry.Text = "";
            LastNameEntry.Text = "";
            PhoneNumberEntry.Text = "";
            EmailEntry.Text = "";
            AddressEntry.Text = "";
            // Visa en bekr�ftelse f�r anv�ndaren
            await DisplayAlert("Success", "Contact added successfully!", "OK");
        }
    }
}


