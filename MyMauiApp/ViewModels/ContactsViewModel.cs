using System.Collections.ObjectModel;
using Shared.Services;
using Contact = Shared.Models.Contact;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MyMauiApp.ViewModels
{
    public class ContactsViewModel : ObservableObject
    {
        private readonly ContactService _contactService;
        /// <summary>
        /// Konstruktor för ContactsViewModel.
        /// </summary>

        public ContactsViewModel(ContactService contactService)
        {
            _contactService = contactService;
            LoadContacts();
        }

        public ObservableCollection<Contact> Contacts { get; } = [];

        /// <summary>
        /// Laddar kontakter från ContactService och uppdaterar Contacts-egenskapen.
        /// </summary>
        public void LoadContacts()
        {
            var contactsFromService = _contactService?.GetAll();
            Contacts.Clear();
            if (contactsFromService != null)
            {
                foreach (var contact in contactsFromService)
                {
                    Contacts.Add(contact);
                }
            }
        }
    }
}
