namespace e_commerce.Entities
{
    public class Customer
    {
        //class representing customers table
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public DateTime RegistrationDate { get; set; }

        public Customer(int id, string name, string email, string? phone, string? address, DateTime registrationDate)
        {
            Id = id;
            Name = name;
            Email = email;
            Phone = phone;
            Address = address;
            RegistrationDate = registrationDate;
        }

        public Customer(string name, string email, string? phone, string? address, DateTime registrationDate)
        {
            Name = name;
            Email = email;
            Phone = phone;
            Address = address;
            RegistrationDate = registrationDate;
        }


        public override string ToString()
        {
            return $"Name: {Name}, Email: {Email}, Phone: {Phone ?? "N/A"}, Address: {Address ?? "N/A"}," +
                $" Registration Date: {RegistrationDate:yyyy-MM-dd}";
        }

        //factory method, returns object from user input
        public static Customer CreateFromInput()
        {
            Console.WriteLine("Enter Customer details:");

            Console.Write("Name: ");
            string name = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("Phone (optional): ");
            string? phone = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(phone)) phone = null;

            Console.Write("Address (optional): ");
            string? address = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(address)) address = null;
            DateTime registrationDate;
            Console.Write("Registration Date (yyyy-mm-dd): ");
            string input = Console.ReadLine();
            if (input == string.Empty)
            {
                registrationDate = DateTime.UtcNow;
            } 
            else
            {
                registrationDate = DateTime.Parse(input);
            }
            

            return new Customer(name, email, phone, address, registrationDate);
        }
    }
}
