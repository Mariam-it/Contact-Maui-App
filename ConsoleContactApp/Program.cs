using Shared.Models;
using Shared.Services;

namespace ConsoleApp
{
    class Program
    {
        static void Main()
        {
            var contactService = new ContactService();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n" + new string('#', 60));
            Console.WriteLine(String.Format("{0,44}", "Välkommen till kontaktappen!"));
            Console.WriteLine(new string('#', 60));
            Console.ResetColor();


            while (true)
            {
                Console.WriteLine("1. Visa kontakter");
                Console.WriteLine("2. Lägg till en kontakt");
                Console.WriteLine("3. Ta bort en kontakt");
                Console.WriteLine("4. Hämta en specifik kontakt");
                Console.WriteLine("5. Avsluta");

                Console.Write("Välj en option (1-4): ");

                string choice = Console.ReadLine()!;

                switch (choice)
                {
                    case "1":
                        // Visa befintliga kontakter

                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("\n" + new string('#', 50));
                        Console.WriteLine(String.Format("{0,30}", " Kontakter "));
                        Console.WriteLine(new string('#', 50));
                        Console.ResetColor();

                        var contacts = contactService.GetContacsFromList();
                        var count = 0;
                        foreach (var contact in contacts)
                        {
                            count++;
                            Console.WriteLine($"{count}: {contact.FirstName} {contact.LastName}");
                        }
                        break;

                    case "2":
                        // Lägg till en ny kontakt
                        Console.Write("Ange förnamn: ");
                        string firstName = Console.ReadLine()!;

                        Console.Write("Ange efternamn: ");
                        string lastName = Console.ReadLine()!;

                        Console.Write("Ange e-post: ");
                        string email = Console.ReadLine()!;

                        Console.Write("Ange telefonnummer: ");
                        string phone = Console.ReadLine()!;

                        Console.Write("Ange address: ");
                        string address = Console.ReadLine()!;

                        var newContact = new Contact
                        {
                            FirstName = firstName,
                            LastName = lastName,
                            Email = email,
                            PhoneNumber = phone,
                            Address = address
                        };

                        contactService.AddContactToList(newContact);
                        Console.WriteLine("Kontakt tillagd!");
                        break;

                    case "3":
                        // Ta bort en kontakt
                        Console.Write("Ange e-postadress för att ta bort en kontakt: ");
                        string emailToRemove = Console.ReadLine()!;

                        contactService.RemoveContactFromList(emailToRemove);
                        break;
                    case "4":
                        Console.Write("Ange namn för att se kontaktinformation: ");
                        string emailToView = Console.ReadLine()!;
                        contactService.ShowContactDetails(emailToView);
                        break;


                    case "5":
                        // Avsluta programmet

                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("\n══════════════════════════════════════════════════════════");
                        Console.WriteLine("  Programmet avslutas. ");
                        Console.WriteLine("══════════════════════════════════════════════════════════");
                        Console.ResetColor();
                        Environment.Exit(0);
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("!!Ogiltigt val. Försök igen!!.");
                        Console.ResetColor();
                        break;
                }

                Console.WriteLine(); // Tom rad för att skilja mellan varje val i menyn
            }
        }
    }
}
