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

            // Mocka File.Exists-metoden f�r att returnera true
            fileMock.Setup(file => file.Exists(It.IsAny<string>())).Returns(true);

            // Mocka File.ReadAllText f�r att returnera befintliga kontakter som JSON
            fileMock.Setup(file => file.ReadAllText(It.IsAny<string>()))
                .Returns(() => JsonConvert.SerializeObject(existingContacts));

            // Mocka File.WriteAllText f�r att g�ra ingenting (det �r en dummy-�tg�rd)
            fileMock.Setup(file => file.WriteAllText(It.IsAny<string>(), It.IsAny<string>())).Verifiable();

            // Act
            var newContact = new Contact { Email = "new@example.com" };
            contactService.Add(newContact);

            // Assert
            Assert.Contains(newContact, contactService.GetAll());
            Assert.NotNull(existingContacts);
            Assert.NotNull(newContact);
            Assert.Single(existingContacts);
            // Verifiera att File.WriteAllText kallades exakt en g�ng
            fileMock.Verify(file => file.WriteAllText(It.IsAny<string>(), It.IsAny<string>()), Times.Once);


        }
    }
}