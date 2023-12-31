
namespace MyMauiApp.ViewModels;

/// <summary>
/// ViewModel för att lägga till en ny kontakt.
/// </summary>
internal class ContactAddViewModel
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Address { get; set; } = null!;
}
