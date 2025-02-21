using e_commerce.Entities;
using System.Data.SqlClient;

namespace e_commerce.Database
{
    internal class OrderDAO
    {
        // Database Access Object for orders table

        // Singleton instance
        private static OrderDAO _instance;
        private OrderDAO() { }
        public static OrderDAO Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new OrderDAO();
                }
                return _instance;
            }
        }

        // Add order to database
        // input Order object to be added
        public bool Add(Order order)
        {
            SqlConnection conn = Database.GetConnection();

            using (SqlCommand command = new("INSERT INTO Orders (order_code, customer_id, total_price, order_date) " +
                "values (@order_code, @customerId, @price, @order_date)", conn))
            {
                command.Parameters.AddWithValue("@order_code", order.OrderCode);
                command.Parameters.AddWithValue("@customerId", order.CustomerId);
                command.Parameters.AddWithValue("@price", order.TotalPrice);
                command.Parameters.AddWithValue("@order_date", order.OrderDate);
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        // Update method for Orders table
        // input Order object to be updated
        public bool Update(Order order)
        {
            SqlConnection conn = Database.GetConnection();

            using (SqlCommand command = new("UPDATE Orders " +
                "SET customer_id = @customer_id, order_date = @order_date, total_price = @total_price " +
                "WHERE id = @id", conn))
            {
                command.Parameters.AddWithValue("@customer_id", order.CustomerId);
                command.Parameters.AddWithValue("@order_date", order.OrderDate);
                command.Parameters.AddWithValue("@total_price", order.TotalPrice);
                command.Parameters.AddWithValue("@id", order.Id);

                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        // Delete method for Orders table
        // input int id of the order to be deleted
        public bool Delete(int id)
        {
            SqlConnection conn = Database.GetConnection();

            using (SqlCommand command = new("DELETE FROM Orders WHERE id = @orderId", conn))
            {
                command.Parameters.AddWithValue("@orderId", id);
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        // Get all orders from Orders table
        // returns list of all orders
        public List<Order> GetAll()
        {
            SqlConnection conn = Database.GetConnection();
            List<Order> orders = new();

            using (SqlCommand command = new("SELECT * FROM Orders", conn))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Order order = new Order(
                        (int)reader["id"],
                        (int)reader["customer_id"],
                        (DateTime)reader["order_date"],
                        (double)reader["total_price"]
                    );
                    orders.Add(order);
                }
                reader.Close();
            }
            return orders;
        }

        // Get order by ID
        // input int id of the order
        // returns Order object if found, otherwise null
        public Order GetById(int id)
        {
            SqlConnection conn = Database.GetConnection();

            using (SqlCommand command = new("SELECT * FROM Orders WHERE id = @orderId", conn))
            {
                command.Parameters.AddWithValue("@orderId", id);
                SqlDataReader reader = command.ExecuteReader();
                Order? order = null;
                if (reader.Read())
                {
                    order = new Order(
                        (int)reader["id"],
                        (int)reader["customer_id"],
                        (DateTime)reader["order_date"],
                        (double)reader["total_price"]
                    );
                }
                reader.Close();
                return order;
            }
        }

        // Get order by order code
        // input int order code
        // returns Order object if found, otherwise null
        public Order GetByCode(int code)
        {
            SqlConnection conn = Database.GetConnection();

            using (SqlCommand command = new("SELECT * FROM Orders WHERE order_code = @code", conn))
            {
                command.Parameters.AddWithValue("@code", code);
                SqlDataReader reader = command.ExecuteReader();
                Order? order = null;
                if (reader.Read())
                {
                    order = new Order(
                        (int)reader["id"],
                        (int)reader["order_code"],
                        (int)reader["customer_id"],
                        (DateTime)reader["order_date"],
                        (double)reader["total_price"]
                    );
                }
                reader.Close();
                return order;
            }
        }
    }
}
