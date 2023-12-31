using Newtonsoft.Json;
using Shared.Models;

namespace Shared.Services
{
    public class ContactService
    {
        private List<Contact> _contacts = [];
        private readonly string _jsonFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "contacts.json");
        private readonly IFile _file;

        public ContactService(IFile file)
        {
            _file = file;
            LoadContacts();

        }
        /// <summary>
        /// Lägger till en ny kontakt i listan och sparar ändringarna.
        /// </summary>
        /// <param name="contact">Kontakt som ska läggas till.</param>
        public void Add(Contact contact)
        {
            _contacts.Add(contact);
            SaveContacts();
        }
        /// <summary>
        /// Hämtar alla kontakter.
        /// </summary>
        /// <returns>En lista med alla kontakter.</returns>
        public IEnumerable<Contact> GetAll()
        {
            return _contacts;
        }
        /// <summary>
        /// Hämtar en enskild kontakt som matchar ett villkor.
        /// </summary>
        /// <param name="predicate">Villkor för att hitta en kontakt.</param>
        /// <returns>En kontakt som matchar villkoret eller en tom kontakt om ingen hittades.</returns>
        public Contact GetOne(Func<Contact, bool> predicate)
        {
            return _contacts.FirstOrDefault(predicate) ?? new Contact();
        }  
        /// <summary>
        /// Uppdaterar en befintlig kontakt eller lägger till en ny om den inte finns.
        /// </summary>
        /// <param name="contactToUpdate">Kontakten som ska uppdateras eller läggas till.</param>
        public void Update(Contact contactToUpdate)
        {
            var contact = _contacts.FirstOrDefault(c => c.Email == contactToUpdate.Email);
            if (contact != null)
            {
                // Uppdatera kontaktens information
                contact.FirstName = contactToUpdate.FirstName;
                contact.LastName = contactToUpdate.LastName;
                contact.PhoneNumber = contactToUpdate.PhoneNumber;
                contact.Address = contactToUpdate.Address;
                contact.Email = contactToUpdate.Email;

                SaveContacts(); // Spara den uppdaterade listan
            }
            else
            {
                _contacts.Add(contactToUpdate);
                SaveContacts();
            }
        }

        /// <summary>
        /// Tar bort en kontakt från listan och sparar ändringarna.
        /// </summary>
        /// <param name="contact">Kontakten som ska tas bort.</param>
        public void Delete(Contact contact)
        {
            _contacts.Remove(contact);
            SaveContacts();
        }

        private void LoadContacts()
        {
            if (_file.Exists(_jsonFilePath))
            {
                string json = _file.ReadAllText(_jsonFilePath);
                _contacts = JsonConvert.DeserializeObject<List<Contact>>(json) ?? [];
            }
        }

        private void SaveContacts()
        {
            string json = JsonConvert.SerializeObject(_contacts);
            _file.WriteAllText(_jsonFilePath, json);
        }
    }
}
