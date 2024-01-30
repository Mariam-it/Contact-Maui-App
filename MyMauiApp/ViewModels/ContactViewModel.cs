using Shared.Services;
using System.ComponentModel;
using System.Diagnostics;

namespace MyMauiApp.ViewModels;

public class ContactViewModel : INotifyPropertyChanged
{
    private readonly IContactService _contactService;
    private Shared.Models.Contact? _currentContact { get; set; }

    public ContactViewModel(IContactService contactService)
    {
        _contactService = contactService ?? throw new ArgumentNullException(nameof(contactService));
        CurrentContact = new Shared.Models.Contact();
    }
    public Shared.Models.Contact CurrentContact
    {
        get => _currentContact!;
        set
        {
            if (_currentContact != value)
            {
                _currentContact = value;
                OnPropertyChanged(nameof(CurrentContact));
            }
        }
    }

    public bool DeleteContact(string email)
    {
        try
        {
            _contactService.RemoveContactFromList(email);
            return true;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;

    }
    public void UpdateContact(Shared.Models.Contact currentContact)
    {
        // Logik för att uppdatera kontakten
        _contactService.UpdateContact(currentContact);

    }

    //Implementera INotifyPropertyChanged
    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}


