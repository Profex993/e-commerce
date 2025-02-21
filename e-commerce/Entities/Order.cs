using e_commerce.Database;

namespace e_commerce.Entities
{
    public class Order
    {
        //class representing order table
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int OrderCode { get; set; }
        public DateTime? OrderDate { get; set; }
        public double TotalPrice { get; set; }


        public Order(int id, int customerId,  DateTime? orderDate, double totalPrice, int orderCode)
        {
            Id = id;
            CustomerId = customerId;
            OrderDate = orderDate;
            TotalPrice = totalPrice;
            OrderCode = orderCode;
        }

        public Order(int orderCode, int customerId, DateTime? orderDate, double totalPrice)
        {
            CustomerId = customerId;
            OrderDate = orderDate;
            TotalPrice = totalPrice;
            OrderCode = orderCode;
        }


        public override string ToString()
        {
            Console.WriteLine(CustomerId);
            return $"Order Code: {OrderCode}, Customer Name: {CustomerDAO.Instance.GetById(CustomerId).Name}," +
                $" Order Date: {OrderDate?.ToString("yyyy-MM-dd") ?? "N/A"}, Total Price: ${TotalPrice}";
        }

        //factory method, returns object from user input
        public static Order CreateFromInput()
        {
            Console.WriteLine("Enter Order details:");

            Console.Write("Order Code: ");
            int orderCode = int.Parse(Console.ReadLine());

            Console.Write("Customer name: ");
            Customer customer = CustomerDAO.Instance.GetByName(Console.ReadLine());

            if (customer == null)
            {
                throw new Exception("Customer doesnt exist");
            }

            Console.Write("Order Date (optional - yyyy-mm-dd): ");
            DateTime orderDate;
            string input = Console.ReadLine();
            if (input == string.Empty)
            {
                orderDate = DateTime.UtcNow;
            }
            else
            {
                orderDate = DateTime.Parse(input);
            }

            Console.Write("Total Price: ");
            double totalPrice = double.Parse(Console.ReadLine());

            return new Order(orderCode, customer.Id, orderDate, totalPrice);
        }

    }
}
