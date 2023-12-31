using Shared.Services;

namespace MyMauiApp.Pages
{
    public partial class ContactAddPage : ContentPage
    {
        private readonly ContactService _contactService;

        public ContactAddPage(ContactService contactService)
        {
            InitializeComponent();
            _contactService = contactService;
        }
        /// <summary>
        /// Hanterar händelsen när knappen "Lägg till kontakt" klickas.
        /// Lägger till en ny kontakt med informationen från inmatningsfälten.
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
            // Lägg till den nya kontakten i kontaktlistan
            _contactService.Add(newContact);
            // Rensa inmatningsfälten
            FirstNameEntry.Text = "";
            LastNameEntry.Text = "";
            PhoneNumberEntry.Text = "";
            EmailEntry.Text = "";
            AddressEntry.Text = "";
            // Visa en bekräftelse för användaren
            await DisplayAlert("Success", "Contact added successfully!", "OK");
        }
    }
}


