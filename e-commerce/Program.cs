using e_commerce.Database;
using e_commerce.Entities;

class Program
{
    //method Main contains the logic behind UI
    static void Main()
    {
        Console.WriteLine("Welcome to E-Commerce Management System");
        while (true)
        {
            Console.WriteLine("1. Customers\n2. Products\n3. Orders\n4. Order Items\n5. Payments\n6. Import Product from XML\n7. Import Customer from XML\n8. Exit");
            Console.Write("Select an option: ");

            //check input
            int opt;
            if (!int.TryParse(Console.ReadLine(), out opt))
            {
                Console.WriteLine("Invalid input! Press any key to try again.");
                Console.ReadKey();
                continue;
            }

            //exit option
            if (opt == 8) break;

            //import from XML option
            int action = 0;
            if (opt != 6 && opt != 7)
            {
                Console.WriteLine("1. Add\n2. Delete\n3. Update\n4. List All");
                Console.Write("Select an action: ");

                if (!int.TryParse(Console.ReadLine(), out action))
                {
                    Console.WriteLine("Invalid input! Press any key to try again.");
                    Console.ReadKey();
                    continue;
                }
            }

            //other options
            switch (opt)
            {
                case 1:
                    HandleCustomerOperations(action);
                    break;
                case 2:
                    HandleProductOperations(action);
                    break;
                case 3:
                    HandleOrderOperations(action);
                    break;
                case 4:
                    HandleOrderItemOperations(action);
                    break;
                case 5:
                    HandlePaymentOperations(action);
                    break;
                case 6:
                    Console.WriteLine("Enter Absolute Path to XML File");
                    ProductDAO.Instance.AddFromXML(Console.ReadLine());
                    break;
                case 7:
                    Console.WriteLine("Enter Absolute Path to XML File");
                    CustomerDAO.Instance.AddFromXML(Console.ReadLine());
                    break;
                default:
                    Console.WriteLine("Unknown option. Press any key to continue.");
                    break;
            }
            Console.ReadKey();
        }
    }

    // CUSTOMER OPERATIONS
    static void HandleCustomerOperations(int action)
    {
        switch (action)
        {
            case 1:
                Customer customer = Customer.CreateFromInput();
                if (CustomerDAO.Instance.Add(customer))
                    Console.WriteLine("Customer added successfully!");
                else
                    Console.WriteLine("Failed to add customer.");
                break;

            case 2:
                Console.Write("Enter Customer Name to delete: ");
                if (CustomerDAO.Instance.Delete(Console.ReadLine()))
                    Console.WriteLine("Customer deleted successfully!");
                else
                    Console.WriteLine("Failed to delete customer.");
                break;

            case 3:
                Customer updatedCustomer = Customer.CreateFromInput();
                if (CustomerDAO.Instance.Update(updatedCustomer))
                    Console.WriteLine("Customer updated successfully!");
                else
                    Console.WriteLine("Failed to update customer.");
                break;

            case 4:
                Console.WriteLine("Listing all customers...");
                var customers = CustomerDAO.Instance.GetAll();
                foreach (var c in customers) Console.WriteLine(c);
                break;

            default:
                Console.WriteLine("Invalid action.");
                break;
        }
    }

    // PRODUCT OPERATIONS
    static void HandleProductOperations(int action)
    {
        switch (action)
        {
            case 1:
                Product product = Product.CreateFromInput();
                ProductDAO.Instance.Add(product);
                Console.WriteLine("Product added successfully!");
                break;

            case 2:
                Console.WriteLine("Not implemented");
                break;

            case 3:
                Product updatedProduct = Product.CreateFromInput();
                Console.WriteLine("Not implemented");
                break;

            case 4:
                Console.WriteLine("Listing all products...");
                var products = ProductDAO.Instance.GetAll();
                foreach (var c in products) Console.WriteLine(c);
                break;

            default:
                Console.WriteLine("Invalid action.");
                break;
        }
    }

    // ORDER OPERATIONS
    static void HandleOrderOperations(int action)
    {
        switch (action)
        {
            case 1:
                Order order = Order.CreateFromInput();
                OrderDAO.Instance.Add(order);
                while (true)
                {
                    Console.WriteLine("Add Items to the order? yes/no");
                    if (Console.ReadLine() == "yes")
                    {
                        OrderItemDAO.Instance.Add(OrderItem.CreateFromInput(order.OrderCode));
                    } 
                    else
                    {
                        break;
                    }
                }

                Console.WriteLine("Add Payment to the order? yes/no");
                if (Console.ReadLine() == "yes")
                {
                    PaymentDAO.Instance.Add(Payment.CreateFromInput(order.OrderCode));
                }

                Console.WriteLine("Order added successfully!");
                break;

            case 2:
                OrderDAO.Instance.Delete(OrderDAO.Instance.GetByCode(Convert.ToInt32(Console.ReadLine())).Id);
                Console.WriteLine("Order deleted successfully!");
                break;

            case 3:
                Console.WriteLine("Enter order code");
                Order originalOrder = OrderDAO.Instance.GetByCode(Convert.ToInt32(Console.ReadLine()));
                Order updatedOrder = Order.CreateFromInput();
                updatedOrder.Id = originalOrder.Id;
                OrderDAO.Instance.Update(updatedOrder);
                Console.WriteLine("Order updated successfully!");
                break;

            case 4:
                Console.WriteLine("Listing all orders...");
                var orders = OrderDAO.Instance.GetAll();
                foreach (var c in orders) Console.WriteLine(c);
                break;

            default:
                Console.WriteLine("Invalid action.");
                break;
        }
    }

    // ORDER ITEM OPERATIONS
    static void HandleOrderItemOperations(int action)
    {
        switch (action)
        {
            case 1:
                OrderItem orderItem = OrderItem.CreateFromInput();
                OrderItemDAO.Instance.Add(orderItem);
                Console.WriteLine("Order Item added successfully!");
                break;

            case 2:
                Console.WriteLine("Not implemented");
                break;

            case 3:
                Console.WriteLine("Not implemented");
                break;

            case 4:
                Console.WriteLine("Listing all order items...");
                var orders = OrderItemDAO.Instance.GetAll();
                foreach (var c in orders) Console.WriteLine(c);
                break;

            default:
                Console.WriteLine("Invalid action.");
                break;
        }
    }

    // PAYMENT OPERATIONS
    static void HandlePaymentOperations(int action)
    {
        switch (action)
        {
            case 1:
                Payment payment = Payment.CreateFromInput();
                PaymentDAO.Instance.Add(payment);
                Console.WriteLine("Payment added successfully!");
                break;

            case 2:
                Console.WriteLine("Not implemented");
                break;

            case 3:
                Console.WriteLine("Not implemented");
                break;

            case 4:
                Console.WriteLine("Listing all payments...");
                var payments = PaymentDAO.Instance.GetAll();
                foreach (var c in payments) Console.WriteLine(c);
                break;

            default:
                Console.WriteLine("Invalid action.");
                break;
        }
    }
}
