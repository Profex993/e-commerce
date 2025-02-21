namespace e_commerce.Entities
{
    public class Product
    {
        //class representing product table
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public int StockQuantity { get; set; }

        public Product(int id, string name, string? description, double price, int stockQuantity)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            StockQuantity = stockQuantity;
        }

        public Product(string name, string? description, double price, int stockQuantity)
        {
            Name = name;
            Description = description;
            Price = price;
            StockQuantity = stockQuantity;
        }


        public override string ToString()
        {
            return $"Name: {Name}, Description: {Description ?? "N/A"}, Price: ${Price:F2}, Stock Quantity: {StockQuantity}";
        }

        //factory method, returns object from user input
        public static Product CreateFromInput()
        {
            Console.WriteLine("Enter Product details:");

            Console.Write("Name: ");
            string name = Console.ReadLine();

            Console.Write("Description (optional): ");
            string? description = Console.ReadLine();

            Console.Write("Price: ");
            double price = double.Parse(Console.ReadLine());

            Console.Write("Stock Quantity: ");
            int stockQuantity = int.Parse(Console.ReadLine());

            return new Product(name, description, price, stockQuantity);
        }
    }
}
