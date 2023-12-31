using Moq;
using Newtonsoft.Json;

namespace ContactTest
{
    public class UnitTest1
    {
        [Fact]
        public void AddContact_ShouldAddContactToListAndSave()
        {
            // Arrange
            var fileMock = new Mock<IFile>();

            // Skapa en mockad lista med befintliga kontakter
            var existingContacts = new List<Contact>
            {
                new() { Email = "existing@example.com" }
            };

            // Skapa en instans av ContactService med mockade beroenden
            var contactService = new ContactService(fileMock.Object);

            // Mocka File.Exists-metoden för att returnera true
            fileMock.Setup(file => file.Exists(It.IsAny<string>())).Returns(true);

            // Mocka File.ReadAllText för att returnera befintliga kontakter som JSON
            fileMock.Setup(file => file.ReadAllText(It.IsAny<string>()))
                .Returns(() => JsonConvert.SerializeObject(existingContacts));

            // Mocka File.WriteAllText för att göra ingenting (det är en dummy-åtgärd)
            fileMock.Setup(file => file.WriteAllText(It.IsAny<string>(), It.IsAny<string>())).Verifiable();

            // Act
            var newContact = new Contact { Email = "new@example.com" };
            contactService.Add(newContact);

            // Assert
            Assert.Contains(newContact, contactService.GetAll());
            Assert.NotNull(existingContacts);
            Assert.NotNull(newContact);
            Assert.Single(existingContacts);
            // Verifiera att File.WriteAllText kallades exakt en gång
            fileMock.Verify(file => file.WriteAllText(It.IsAny<string>(), It.IsAny<string>()), Times.Once);


        }
    }
}