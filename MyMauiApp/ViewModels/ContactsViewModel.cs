using System.Collections.ObjectModel;
using Shared.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MyMauiApp.ViewModels
{
    public class ContactsViewModel : ObservableObject
    {
        private readonly IContactService _contactService;
        /// <summary>
        /// Konstruktor för ContactsViewModel.
        /// </summary>

        public ContactsViewModel(IContactService contactService)
        {
            _contactService = contactService;
            Contacts = [];
            LoadContacts();
        }

        public ObservableCollection<Shared.Models.Contact> Contacts { get; private set; }
        public void LoadContacts()
        {
            var contactsFromService = _contactService.GetContacsFromList();
            Contacts.Clear();
            foreach (var contact in contactsFromService)
            {
                Contacts.Add(contact);
            }
        }
        public void RemoveContact(string email)
        {
            var contactToRemove = Contacts.FirstOrDefault(c => c.Email == email);
            if (contactToRemove != null)
            {
                Contacts.Remove(contactToRemove);
                _contactService.RemoveContactFromList(email);
            }
        }
    }
}
