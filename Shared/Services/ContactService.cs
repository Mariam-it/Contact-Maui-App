using Newtonsoft.Json;
using Shared.Models;
using System.Diagnostics;

namespace Shared.Services
{
    public class ContactService : IContactService
    {

        private IFile _fileService = new FileService(@"C:\Projects\newcontent.json");
        private List<Contact> _contacts = [];
        public ContactService()
        {
            GetContacsFromList();
        }

        // Används endast för testning
        public void SetFileServiceForTesting(IFile fileService)
        {
            _fileService = fileService;
        }
        /// <summary>
        /// Lägger till en ny kontakt i listan och sparar ändringarna.
        /// </summary>
        /// <param name="contact">Kontakt som ska läggas till.</param>
        public void AddContactToList(Contact contact)
        {
            try
            {
                if (!_contacts.Any(x => x.Email == contact.Email))
                {
                    _contacts.Add(contact);
                    _fileService.SaveContentToFile(JsonConvert.SerializeObject(_contacts));
                }
            }catch (Exception ex) { Debug.WriteLine(ex.Message); }

        }
        /// <summary>
        /// Hämtar en kontakt.
        /// </summary>
        /// <returns> en specefik kontakt.</returns>
        public void ShowContactDetails(string name)
        {
 

            var contact = _contacts.FirstOrDefault(c => c.FirstName == name);
            if (contact != null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n" + new string('#', 50));
                Console.WriteLine(String.Format("{0,30}", "Kontakt Detalj"));
                Console.WriteLine(new string('#', 50));
                Console.ResetColor();
                Console.WriteLine($" Namn: {contact.FirstName} {contact.LastName}");
                Console.WriteLine($" Telefonnummer: {contact.PhoneNumber}");
                Console.WriteLine($" E-postadress: {contact.Email}");
                Console.WriteLine($" Adress: {contact.Address}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n" + new string('¤', 60));
                Console.WriteLine(String.Format("{0,49}", "  Ingen kontakt hittades med den namn. "));
                Console.WriteLine(new string('¤', 60));
                Console.ResetColor();

            }
        }
        /// <summary>
        /// Hämtar alla kontakter.
        /// </summary>
        /// <returns>En lista med alla kontakter.</returns>
        public IEnumerable<Contact> GetContacsFromList()
        {
            try
            {
                var content = _fileService.GetContentFormFile();
                if(!string.IsNullOrEmpty(content))
                {
                    _contacts = JsonConvert.DeserializeObject<List<Contact>>(content)!;
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return _contacts;

        }
        /// <summary>
        /// Tar bort en kontakt från listan och sparar ändringarna.
        /// </summary>
        /// <param name="contact">Kontakten som ska tas bort.</param>
        public void RemoveContactFromList(string email)
        {
            try
            {
                var contactToRemove = _contacts.FirstOrDefault(x => x.Email == email);

                if (contactToRemove != null)
                {
                    _contacts.Remove(contactToRemove);
                    _fileService.SaveContentToFile(JsonConvert.SerializeObject(_contacts));
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n" + new string('¤', 60));
                    Console.WriteLine(String.Format("{0,49}", $"  Kontakt med e-post {email} har tagits bort. "));
                    Console.WriteLine(new string('¤', 60));
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n" + new string('¤', 60));
                    Console.WriteLine(String.Format("{0,49}", $"  Ingen kontakt hittades med den e-post {email}. "));
                    Console.WriteLine(new string('¤', 60));
                    Console.ResetColor();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// Uppdaterar en befintlig kontakt eller lägger till en ny om den inte finns.
        /// </summary>
        /// <param name="contactToUpdate">Kontakten som ska uppdateras eller läggas till.</param>
        public void UpdateContact(Contact contact)
        {
            try
            {
                var existingContact = _contacts.FirstOrDefault(x => x.Email == contact.Email);

                if (existingContact != null)
                {
                    // Uppdatera egenskaperna för den befintliga kontakten med de nya värdena
                    existingContact.FirstName = contact.FirstName;
                    existingContact.LastName = contact.LastName;
                    existingContact.Email = contact.Email;
                    existingContact.Address = contact.Address;
                    existingContact.PhoneNumber = contact.PhoneNumber;

                    // Spara den uppdaterade listan till filen
                    _fileService.SaveContentToFile(JsonConvert.SerializeObject(_contacts));

                    Console.WriteLine($"Kontakt med e-post {contact.Email} har uppdaterats.");
                }
                else
                {
                    Console.WriteLine($"Ingen kontakt hittades med e-post {contact.Email}.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
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
    }
}
