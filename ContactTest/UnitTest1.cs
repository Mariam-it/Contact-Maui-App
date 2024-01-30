using Moq;
using Newtonsoft.Json;


namespace ContactTest;

public class UnitTest1
{
    [Fact]
    public void AddContactToList_ShouldAddContact()
    {
        // Arrange
        var fileMock = new Mock<IFile>();
        var contactService = new ContactService();
        contactService.SetFileServiceForTesting(fileMock.Object);

        var testContact = new Contact { Email = "test@example.com" };
        string emptyContactsJson = JsonConvert.SerializeObject(new List<Contact>());
        string updatedContactsJson = JsonConvert.SerializeObject(new List<Contact> { testContact });

        fileMock.Setup(f => f.GetContentFormFile()).Returns(emptyContactsJson);
        fileMock.Setup(f => f.SaveContentToFile(It.IsAny<string>()))
                .Callback<string>(content => fileMock.Setup(f => f.GetContentFormFile()).Returns(content));

        // Act
        contactService.AddContactToList(testContact);

        // Assert
        var contacts = contactService.GetContacsFromList().ToList();
        Assert.Contains(contacts, c => c.Email == testContact.Email);
        fileMock.Verify(f => f.SaveContentToFile(It.IsAny<string>()), Times.Once);

    }

}