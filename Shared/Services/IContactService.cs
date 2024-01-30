using Shared.Models;

namespace Shared.Services;

public interface IContactService
{
    void AddContactToList(Contact contact);
    void ShowContactDetails(string name);
    IEnumerable<Contact> GetContacsFromList();
    void RemoveContactFromList(string email);
    void UpdateContact(Contact contact);
    Contact GetOne(Func<Contact, bool> predicate);
}
