using _01_Assignment_.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace _01_Assignment_.Services
{

    internal interface IMenuManager // Detta är ett interface (kontrakt av vad som ska finnas med) som acessbart inom det egna projektet, och mellan klasser i projektet.
    {
        public void ViewMenuOptions(); // Method
        public void ViewCreateMember(); // Method
        public void ViewMemberCatalog(); // Method
        public void ViewMemberDetails(string id); // Method
        public void ViewUpdateMemberDetails();// Method
        public void ViewRemoveMember(); // Method
    }
    internal class MenuManager : IMenuManager // detta är en klass som har ärvt från ett interface
    {
        private IFileManager _fileManager = new FileManager();
        private List<Member> _member = new(); // lista
        private string _filePath = @$"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\file.json"; //platsen för filen

        public void ViewCreateMember() // hämtar metod ovanför som skapar en ny medlem enlig reglerna nedan
        {
            var member = new Member();

            Console.Clear();
            Console.WriteLine(" ------- CREATE NEW MEMBER ------- ");
            Console.Write("First name: ");
            member.FirstName = Console.ReadLine() ?? "";

            Console.Write("Last name: ");
            member.LastName = Console.ReadLine() ?? "";

            Console.Write("Email: ");
            member.Email = Console.ReadLine() ?? "".ToLower().Trim();

            Console.Write("Street name: ");
            member.StreetName = Console.ReadLine() ?? "";

            Console.Write("Postal code: ");
            member.PostalCode = Console.ReadLine() ?? "";

            Console.Write("City: ");
            member.City = Console.ReadLine() ?? "";

            Console.WriteLine("");
            Console.WriteLine(" ----- MEMBER CREATED -----");

            _member.Add(member); // sparas till listan
            _fileManager.Save(_filePath, JsonConvert.SerializeObject(_member, Formatting.Indented)); //sparar i listan i fil

        }

        public void ViewMemberCatalog() // visar lista på medlemmar
        {
            try { _member = JsonConvert.DeserializeObject<List<Member>>(_fileManager.Read(_filePath)); }
            catch { } //tvingar terminalen att köra igenom listan oavsett vad som systemet stötter på

            Console.Clear();
            Console.WriteLine(" ------- MEMBER CATALOG ------- ");

            foreach(var member in _member) //loop för att få fram listan
                Console.WriteLine($"{member.Id} {member.FirstName} {member.LastName}");
            if(_member.Count > 0) // if-sats vad valet blir
            {
                Console.WriteLine();
                Console.Write("View member details? (y/n): ");
                var option = Console.ReadLine();

                if (option?.ToLower() == "y")
                {
                    Console.Write("Enter Member Id: ");
                    var id = Console.ReadLine();
                    if (!string.IsNullOrEmpty(id))
                        ViewMemberDetails(id);
                }
            }
        }

        public void ViewMemberDetails(string id)
        {
            var member = _member.FirstOrDefault(x => x.Id == new Guid(id)); // hämtar baserad på guid-id detalijerad information på en medlem från listan.

            Console.Clear();
            Console.WriteLine(" ------- MEMBER DETAILS ------- ");
            Console.WriteLine($"ID: \t              {member?.Id}");
            Console.WriteLine($"FIRST NAME: \t      {member?.FirstName}");
            Console.WriteLine($"LAST NAME: \t      {member?.LastName}");
            Console.WriteLine($"EMAIL: \t              {member?.Email}");
            Console.WriteLine($"STREET: \t      {member?.StreetName}");
            Console.WriteLine($"POSTAL CODE: \t      {member?.PostalCode}");
            Console.WriteLine($"CITY: \t              {member?.City}");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("1. Edit Member");
            Console.WriteLine("2. Remove Member");
            Console.Write("Enter option: ");
            var option = Console.ReadLine();

            switch (option) // En switch, i switchen skickas användaren till två olika alternativ 
            {
                case "1":
                    ViewUpdateMember(member);
                    break;

                case "2":
                    RemoveMember(member.Id);
                    break;
            }
        }

        public void ViewMenuOptions() // Huvudmeny som är skapad i en switch
        {
            Console.WriteLine(" ------- MENU ------- ");
            Console.WriteLine("1. Create new Member");
            Console.WriteLine("2. View Member Catalog");
            Console.WriteLine("Q. Exit Application");
            Console.WriteLine("");
            Console.Write("Choose one option (1-2): ");
            var option = Console.ReadLine() ?? "".ToLower();

            switch (option)
            {
                case "1":
                    ViewCreateMember();
                    break;

                case "2":
                    ViewMemberCatalog();
                    break;

                case "Q":
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Invalid menu option selected.");
                    break;
            }

        }

        private void RemoveMember(Guid id)
        {
            Console.WriteLine("");
            Console.WriteLine(" ----- MEMBER REMOVED -----");

            _member = _member.Where(x => x.Id != id).ToList(); //Lambda expression pekar med hjälp av guid vilken medlem som ska raderas
            _fileManager.Save(_filePath, JsonConvert.SerializeObject(_member, Formatting.Indented)); // sparar listan i filen
        }

        public void ViewUpdateMember(Member member)
        {
            var index = _member.IndexOf(member); // pekar på vilken medlem som ska uppdateras eller ändra uppgifter på

            Console.Write("Frist Name: ");
            var firstName = Console.ReadLine();
            if (!string .IsNullOrEmpty(firstName))
                member.FirstName = firstName;

            Console.Write("Last Name: ");
            var lastName = Console.ReadLine();
            if (!string.IsNullOrEmpty(lastName))
                member.LastName = lastName;

            Console.Write("Email: ");
            var email = Console.ReadLine();
            if (!string.IsNullOrEmpty(email))
                member.Email = email;

            Console.Write("Street Name: ");
            var streetName = Console.ReadLine();
            if (!string.IsNullOrEmpty(streetName))
                member.StreetName = streetName;

            Console.Write("Postal Code: ");
            var postalCode = Console.ReadLine();
            if (!string.IsNullOrEmpty(postalCode))
                member.PostalCode = postalCode;

            Console.Write("City: ");
            var city = Console.ReadLine();
            if (!string.IsNullOrEmpty(city))
                member.City = city;

            Console.WriteLine("");
            Console.WriteLine(" ----- MEMBER UPDATED -----");

            _member[index] = member;
            _fileManager.Save(_filePath, JsonConvert.SerializeObject(_member, Formatting.Indented));
        }

        public void ViewUpdateMemberDetails()
        {
            throw new NotImplementedException();
        }

        public void ViewRemoveMember()
        {
            throw new NotImplementedException();
        }
    }
}
