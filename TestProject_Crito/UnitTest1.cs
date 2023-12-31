

using Xunit;
using ConsoleApp;
using System.Reflection.Metadata;
using static ConsoleApp.ContactBook;


namespace TestProject_Crito;



    public class ContactBookTests
    {
        [Fact]
        public void AddContact_WithValidData_ShouldAddContact()
        {
            // Arrange
            var contactBook = new ContactBook();
            var contact = new Contact("kevin", "ark", "123456789", "ark@gmail.com", "knivsta"); 
            // Act
            contactBook.AddContact(contact);
            

            
            // Assert
            Assert.Contains(contact, contactBook.GetAllContacts());
        }
        [Fact]
        public void RemoveContact_WithExistingContact_ShouldRemoveContact()
        { 
            // Arrange
            var contactBook = new ContactBook();
            var contact = new Contact("kevin", "ark", "123456789", "ark@gmail.com", "knivsta");
            // Act
            contactBook.RemoveContact(contact);
            // Assert
            Assert.DoesNotContain(contact, contactBook.GetAllContacts());
        }
        [Fact]
        public void RemoveContact_WithNonExistingContact_ShouldNotChangeContacts()
        {
            // Arrange
            var contactBook = new ContactBook();
            var contact = new Contact("kevin", "ark", "123456789", "ark@gmail.com", "knivsta");
            // Act
            contactBook.RemoveContact(contact);
            // Assert
            Assert.Empty(contactBook.Contacts);
        }
        [Fact]
        public void UpdateContactInformation_ForExistingContact_ShouldUpdateContact() 
        {
        // Arrange
        var contactBook = new ContactBook();
        var contact = new Contact("kevin", "ark", "123456789", "ark@gmail.com", "knivsta");
        contactBook.AddContact(contact);

        //Act
        var updatedContactData = new Contact("kevin", "newark", "111111111", "new@gmail.com", "newknivsta");
        contactBook.UpdateContactInformation(updatedContactData);

        //Assert
        var updatedContact = contactBook.GetContact("new@gmail.com");
        Assert.NotNull(updatedContact);
        Assert.Equal("newark", updatedContact.LastName);
        Assert.Equal("111111111", updatedContact.PhoneNumber);
        Assert.Equal("new@gmail.com", updatedContact.Email);
        Assert.Equal("newknivsta", updatedContact.AddressInformation);
        }

    [Fact]
    public void LoadContactsFromJson_ShouldLoadExistingContactsFomFile()
    {
        // Arrange
        var contactBook = new ContactBook();
        var contact = new Contact("kevin", "newark", "111111111", "new@gmail.com", "newknivsta");
        contactBook.AddContact(contact);
        contactBook.SaveContactsToJson();

        //Act
        contactBook.LoadContactsFromJson();
        // Assert
        Assert.Contains(contact, contactBook.GetAllContacts());

    }
    [Fact]
    public void ViewAllContacts_ShouldReturnContacts()
    {   
        //Arrange
        var contactBook = new ContactBook();
        var contact1 = new Contact("kevin", "newark", "111111111", "new@gmail.com", "newknivsta");
        var contact2 = new Contact("2evin", "2ewark", "211111111", "2ew@gmail.com", "2ewknivsta");
        

        //Act
        //Lägger till kontakter
        contactBook.AddContact(contact1);
        contactBook.AddContact(contact2);
        
        var result = contactBook.ViewAllContacts();
        //Assert
        
        //Contact 1
        Assert.Contains("kevin", result);
        Assert.Contains("newark", result);
        Assert.Contains("111111111", result);
        Assert.Contains("new@gmail.com", result);
        Assert.Contains("newknivsta", result);
        //Contact 2
        Assert.Contains("2evin", result);
        Assert.Contains("2ewark", result);
        Assert.Contains("211111111", result);
        Assert.Contains("2ew@gmail.com", result);
        Assert.Contains("2ewknivsta", result);
        
    }
    [Fact]
    public void SerachContactByName_ShouldReturnContact_IfEmailExists()
    {
        //Arrange
        var contacts = new List<Contact>
        {
            new Contact("kevin", "newark", "111111111", "new@gmail.com", "newknivsta"),
            new Contact("2evin", "2ewark", "211111111", "2ew@gmail.com", "2ewknivsta"),
        };

        //Act
        var searchService = new ContactSearchService(contacts);
        var foundContact = searchService.FindContactByEmail("new@gmail.com");
        //Assert
        Assert.NotNull(foundContact);
        Assert.Equal("new@gmail.com", foundContact.Email);
    }





}
