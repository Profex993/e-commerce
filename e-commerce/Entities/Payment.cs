using e_commerce.Database;

namespace e_commerce.Entities
{
    public class Payment
    {
        //class representing payment table
        public int Id { get; set; }
        public int OrderId { get; set; }
        public DateTime? PaymentDate { get; set; }
        public double Amount { get; set; }
        public string PaymentMethod { get; set; }

        public Payment(int id, int orderId, DateTime? paymentDate, double amount, string paymentMethod)
        {
            Id = id;
            OrderId = orderId;
            PaymentDate = paymentDate;
            Amount = amount;
            PaymentMethod = paymentMethod;
        }

        public Payment(int orderId, DateTime? paymentDate, double amount, string paymentMethod)
        {
            OrderId = orderId;
            PaymentDate = paymentDate;
            Amount = amount;
            PaymentMethod = paymentMethod;
        }

        public override string ToString()
        {
            return $"Order Code: {OrderDAO.Instance.GetById(OrderId).OrderCode}, " +
                $"Payment Date: {PaymentDate?.ToString("yyyy-MM-dd") ?? "N/A"}, " +
                $"Amount: ${Amount:F2}, Payment Method: {PaymentMethod}";
        }

        //factory method, returns object from user input
        public static Payment CreateFromInput()
        {
            Console.WriteLine("Enter Payment details:");

            Console.Write("Order Code: ");
            Order order = OrderDAO.Instance.GetByCode(Convert.ToInt32(Console.ReadLine()));

            if (order == null)
            {
                throw new Exception("Order doesnt exist");
            }

            Console.Write("Payment Date (optional - yyyy-mm-dd): ");
            string? paymentDateInput = Console.ReadLine();
            DateTime? paymentDate = string.IsNullOrWhiteSpace(paymentDateInput) ? (DateTime?)null : DateTime.Parse(paymentDateInput);

            Console.Write("Amount: ");
            double amount = double.Parse(Console.ReadLine());

            Console.Write("Payment Method (e.g., Credit Card, PayPal): ");
            string paymentMethod = Console.ReadLine();

            return new Payment(order.Id, paymentDate, amount, paymentMethod);
        }

        //factory method, returns object from user input with known order_id
        public static Payment CreateFromInput(int orderId)
        {
            Console.WriteLine("Enter Payment details:");

            Order order = OrderDAO.Instance.GetByCode(orderId);

            if (order == null)
            {
                throw new Exception("Order doesnt exist");
            }

            Console.Write("Payment Date (optional - yyyy-mm-dd): ");
            DateTime paymentDate;
            string input = Console.ReadLine();
            if (input == string.Empty)
            {
                paymentDate = DateTime.UtcNow;
            }
            else
            {
                paymentDate = DateTime.Parse(input);
            }

            Console.Write("Amount: ");
            double amount = double.Parse(Console.ReadLine());

            Console.Write("Payment Method (e.g., Credit Card, PayPal): ");
            string paymentMethod = Console.ReadLine();

            return new Payment(order.Id, paymentDate, amount, paymentMethod);
        }
    }
}
