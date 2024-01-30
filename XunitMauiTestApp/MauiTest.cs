using Moq;
using MyMauiApp.ViewModels;
using Shared.Services;
namespace XunitMauiTestApp;

public class MauiTest
{
    [Fact]
    public void ContactListPage_OnAppearing_LoadsContacts()
    {
        // Arrange
        var mockContactService = new Mock<IContactService>();
        var mockContacts = new List<Shared.Models.Contact>
        {
            new() { FirstName = "Test", LastName = "User", Email = "test@example.com" }
        };

        mockContactService.Setup(service => service.GetContacsFromList()).Returns(mockContacts);

        var viewModel = new ContactsViewModel(mockContactService.Object);

        // Act
        viewModel.LoadContacts();

        // Assert
        Assert.NotEmpty(viewModel.Contacts);
        Assert.Equal(mockContacts.Count, viewModel.Contacts.Count);
    }
    [Fact]
    public void UpdateContact_ShouldUpdateContactInfo()
    {
        // Arrange
        var mockContactService = new Mock<IContactService>();
        var viewModel = new ContactViewModel(mockContactService.Object)
        {
            CurrentContact = new Shared.Models.Contact
            {
                Email = "test@example.com",
                FirstName = "OldName"
            }
        };

        var updatedContactInfo = new Shared.Models.Contact
        {
            Email = "test@example.com",
            FirstName = "NewName"
        };

        // Act
        viewModel.CurrentContact.FirstName = updatedContactInfo.FirstName;
        viewModel.UpdateContact(viewModel.CurrentContact);

        // Assert
        mockContactService.Verify(s => s.UpdateContact(viewModel.CurrentContact), Times.Once);
        Assert.Equal(updatedContactInfo.FirstName, viewModel.CurrentContact.FirstName);
    }
    [Fact]
    public void RemoveContact_ShouldRemoveContactFromList()
    {
        // Arrange
        var mockContactService = new Mock<IContactService>();
        var viewModel = new ContactsViewModel(mockContactService.Object);
        var contactToRemove = new Shared.Models.Contact { Email = "contact@example.com" };
        viewModel.Contacts.Add(contactToRemove);

        // Act
        viewModel.RemoveContact(contactToRemove.Email);

        // Assert
        Assert.DoesNotContain(contactToRemove, viewModel.Contacts);
        mockContactService.Verify(s => s.RemoveContactFromList(contactToRemove.Email), Times.Once);
    }

}