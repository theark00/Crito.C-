using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Xml.Linq;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;


namespace ConsoleApp;
public class Program
{
    static List<Contact> contacts = new List<Contact>();
    static string jsonFilePath = "contacts.json";
    

    static void Main()
        {
        LoadContactsFromJson();
        Console.ForegroundColor = ConsoleColor.Green;

            while (true) 
            {
                
                Console.Title = "Adressbok";
                Console.WriteLine();
                Console.WriteLine("Welcome to your contact book");
                Console.WriteLine();
                Console.WriteLine("Menu");
                Console.WriteLine();
                Console.WriteLine("1. Add Contact");
                Console.WriteLine("2. Remove Contact");
                Console.WriteLine("3. Update Contact Information");
                Console.WriteLine("4. View all Contacts");
                Console.WriteLine("5. Search for Contact");
                Console.WriteLine("6. Exit");
                Console.WriteLine();
                Console.Write("Enter your choice: ");
                string option = Console.ReadLine()!;
                
                switch (option)
                {
                case "1":
                    AddContact();
                    break;
                case "2":
                    RemoveContact();
                    break;
                case "3":
                    UpdateContactInformation();
                    break;
                case "4":
                    ViewAllContacts();
                    break;
                case "5":
                    Search();
                    break;
                case "6":
                    SaveContactsToJson();
                    Environment.Exit(0);
                    break;

                    default: 
                        Console.WriteLine("Oops something wrong. Try again.");
                        Console.ReadKey();
                    break;

                   
                }
                
               
            }

        
        }
        
    static void AddContact()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("Enter First Name: ");
        string firstName = Console.ReadLine()!;
        
        Console.Write("Enter Last Name: ");
        string lastName = Console.ReadLine()!;
        
        Console.Write("Enter Phone Number: ");
        string phoneNumber = Console.ReadLine()!;
        
        Console.Write("Enter Email: ");
        string email = Console.ReadLine()!;
        
        Console.Write("Enter Address Information: ");
        string addressInformation = Console.ReadLine()!;
        
        Contact newContact = new Contact(firstName, lastName, phoneNumber, email, addressInformation)!;
        contacts.Add(newContact);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.Clear();
    }

    static void RemoveContact()
    {   
        Console.ForegroundColor= ConsoleColor.Red;
        Console.Write("Enter Email of contact to delete: ");
        string deleteEmail = Console.ReadLine()!;

        Contact foundContact = contacts.Find(c  => c.Email.Equals(deleteEmail, StringComparison.OrdinalIgnoreCase))!;

        if (foundContact != null)
        {
            contacts.Remove(foundContact);
            Console.WriteLine("Contact has been deleted.");

        }
        else 
        {

            Console.WriteLine("Could not find Contact.");
        }
        Console.ForegroundColor = ConsoleColor.Green;
    }
    static void UpdateContactInformation()
    {
        Console.Write("Enter Name to update contact: ");
        string updateName = Console.ReadLine()!;

        Contact foundContact = contacts.Find(c => c.FirstName.Equals(updateName, StringComparison.OrdinalIgnoreCase))!;

        if (foundContact != null)
        {
            Console.WriteLine("Enter new contact information");

            Console.Write("Enter new First Name: ");
            foundContact.FirstName = Console.ReadLine()!;

            Console.Write("Enter new Last Name: ");
            foundContact.LastName = Console.ReadLine()!;

            Console.Write("Enter new Phone Number: ");
            foundContact.PhoneNumber = Console.ReadLine()!;

            Console.Write("Enter new Email: ");
            foundContact.Email = Console.ReadLine()!;

            Console.Write("Enter new Address Information: ");
            foundContact.AddressInformation = Console.ReadLine()!;

            
        }
        else
        {
            Console.WriteLine("Contact not found.");

        }

    }
    static void ViewAllContacts()
    {   
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine();
        Console.WriteLine("All Contacts:");
        Console.WriteLine();
        if (contacts.Count == 0)
        {
            Console.WriteLine("No Contacts.");
        }
        else
        {
            foreach (var contact in contacts)
            {
                Console.WriteLine(contact);
            }
        }
        Console.ForegroundColor = ConsoleColor.Green;
    }
    static void Search()
    {
        Console.Write("Enter Name: ");
        string search = Console.ReadLine()!;

        Contact foundContact = contacts.Find(c => c.FirstName.Equals(search, StringComparison.OrdinalIgnoreCase))!;

        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor= ConsoleColor.Black;

        if (foundContact != null)
        {
            Console.WriteLine();
            Console.WriteLine(foundContact);
            
            
        }
        else
        {
            Console.WriteLine("Could not find contact by that name. Try again!");
        }
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.Green;
         
    }
    static void SaveContactsToJson()
    {
        string jsonString = JsonSerializer.Serialize(contacts, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(jsonFilePath, jsonString);
        Console.WriteLine(jsonString);
    }
    static void LoadContactsFromJson()
    {
        if (File.Exists(jsonFilePath))
        {
            
            string jsonString = File.ReadAllText(jsonFilePath);
            contacts = JsonSerializer.Deserialize<List<Contact>>(jsonString)!;
            
            
        }
        else
        {
            Console.WriteLine("No file");
        }
    }
}
// Funktion för testning
public class ContactBook
{
    public List<Contact> Contacts { get; } = new List<Contact>();
    public string jsonFilePath = "contacts.json";
    public void AddContact(Contact contact)
    {
        Contacts.Add(contact);
    }

    public void RemoveContact(Contact contact)
    {
        Contacts.Remove(contact);
    }
    public void UpdateContactInformation(Contact updatedContact)
    {
        var existingContact = Contacts.Find(c => c.FirstName.Equals(updatedContact.FirstName, StringComparison.OrdinalIgnoreCase));
        
        if (existingContact != null)
        {   
            existingContact.FirstName = updatedContact.FirstName;
            existingContact.LastName = updatedContact.LastName;
            existingContact.PhoneNumber = updatedContact.PhoneNumber;
            existingContact.Email = updatedContact.Email;
            existingContact.AddressInformation = updatedContact.AddressInformation;
        }
    }
    public Contact GetContact(string email)
    {
        return Contacts.Find(c => c.Email.Equals(email, StringComparison.OrdinalIgnoreCase))!;
    }
    public IEnumerable<Contact> GetAllContacts()
    {
        return Contacts;
    }
    public void SaveContactsToJson()
    {
        string jsonString = JsonSerializer.Serialize(Contacts, new JsonSerializerOptions { WriteIndented = true});
        System.IO.File.WriteAllText(jsonFilePath, jsonString);
    }
    public void LoadContactsFromJson()
    {
        if (System.IO.File.Exists(jsonFilePath))
        {
            string jsonString = System.IO.File.ReadAllText(jsonFilePath);
            var loadedContacts = JsonSerializer.Deserialize<List<Contact>>(jsonString) ?? new List<Contact>();
        }
    }
    public string ViewAllContacts()
    {
        var result = new StringBuilder();

        result.AppendLine("List Contacts");

        if (Contacts.Count == 0)
        {
            result.AppendLine("No Contacts yet!");
        }
        else
        {
            foreach (var contact in Contacts)
            {
                result.AppendLine(contact.ToString());
            }
        }
        return result.ToString();
    }
    public class ContactSearchService
    {
        public readonly List<Contact> Contacts;
        public ContactSearchService(List<Contact> contacts)
        {
            this.Contacts = contacts;
        }
        public Contact FindContactByEmail(string email)
        {
            return Contacts.Find(c => c.Email.Equals(email, StringComparison.OrdinalIgnoreCase))!;
        }
    }
}

// slut på testning
public class Contact
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }

    public string PhoneNumber { get; set; }

    public string Email { get; set; }

    public string AddressInformation { get; set; }
    
    public Contact(string firstName, string  lastName, string phoneNumber, string email, string addressInformation)
    {
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        Email = email;
        AddressInformation = addressInformation;
    }
    public override string ToString()
    {
        return $"{FirstName}, LastName: {LastName}, Phone: {PhoneNumber}, Email: {Email}, AddressInformation: {AddressInformation}";
    }

}

