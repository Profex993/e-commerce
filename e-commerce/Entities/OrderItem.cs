using e_commerce.Database;

namespace e_commerce.Entities
{
    public class OrderItem
    {
        //class representing orderItem table
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }


        public OrderItem(int id, int orderId, int productId, int quantity, double price)
        {
            Id = id;
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            Price = price;
        }

        public OrderItem(int orderId, int productId, int quantity, double price)
        {
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            Price = price;
        }

        public override string ToString()
        {
            return $"Order Code: {OrderDAO.Instance.GetById(OrderId).OrderCode}, " +
                $"Product Name: {ProductDAO.Instance.GetById(ProductId).Name}, " +
                $"Quantity: {Quantity}, Price: ${Price:F2}";
        }

        //factory method, returns object from user input
        public static OrderItem CreateFromInput()
        {
            Console.WriteLine("Enter OrderItem details:");

            Console.Write("Order Code: ");
            Order order = OrderDAO.Instance.GetByCode(Convert.ToInt32(Console.ReadLine()));

            if (order == null)
            {
                throw new Exception("Order doesnt exist");
            }

            Console.Write("Product Name: ");
            Product product = ProductDAO.Instance.GetByName(Console.ReadLine());

            if (product == null)
            {
                throw new Exception("Order doesnt exist");
            }

            Console.Write("Quantity: ");
            int quantity = int.Parse(Console.ReadLine());

            Console.Write("Price: ");
            double price = double.Parse(Console.ReadLine());

            return new OrderItem(order.Id, product.Id, quantity, price);
        }

        //factory method, returns object from user input with known order code
        public static OrderItem CreateFromInput(int orderCode)
        {
            Console.WriteLine("Enter OrderItem details:");

            Order order = OrderDAO.Instance.GetByCode(orderCode);
            if (order == null)
            {
                throw new Exception("Order doesnt exist");
            }

            Console.Write("Product Name: ");
            Product product = ProductDAO.Instance.GetByName(Console.ReadLine());

            if (product == null)
            {
                throw new Exception("Order doesnt exist");
            }

            Console.Write("Quantity: ");
            int quantity = int.Parse(Console.ReadLine());

            Console.Write("Price: ");
            double price = double.Parse(Console.ReadLine());

            return new OrderItem(order.Id, product.Id, quantity, price);
        }
    }
}
