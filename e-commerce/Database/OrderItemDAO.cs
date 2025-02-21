using e_commerce.Entities;
using System.Data.SqlClient;

namespace e_commerce.Database
{
    internal class OrderItemDAO
    {
        // Database Access Object for order_items table

        // Singleton instance
        private static OrderItemDAO _instance;
        private OrderItemDAO() { }
        public static OrderItemDAO Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new OrderItemDAO();
                }
                return _instance;
            }
        }

        // Add order item to database
        // input OrderItem object to be added
        public bool Add(OrderItem orderItem)
        {
            SqlConnection conn = Database.GetConnection();

            using (SqlCommand command = new("INSERT INTO OrderItems (order_id, product_id, quantity, price) VALUES" +
                " (@order_id, @product_id, @quantity, @price)", conn))
            {
                command.Parameters.AddWithValue("@order_id", orderItem.OrderId);
                command.Parameters.AddWithValue("@product_id", orderItem.ProductId);
                command.Parameters.AddWithValue("@quantity", orderItem.Quantity);
                command.Parameters.AddWithValue("@price", orderItem.Price);

                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        // Update method for OrderItems table
        // input OrderItem object to be updated
        public bool Update(OrderItem orderItem)
        {
            SqlConnection conn = Database.GetConnection();

            using (SqlCommand command = new("UPDATE OrderItems " +
                "SET order_id = @order_id, " + "product_id = @product_id, quantity = @quantity, price = @price " +
                "WHERE id = @id", conn))
            {
                command.Parameters.AddWithValue("@id", orderItem.Id);
                command.Parameters.AddWithValue("@order_id", orderItem.OrderId);
                command.Parameters.AddWithValue("@product_id", orderItem.ProductId);
                command.Parameters.AddWithValue("@quantity", orderItem.Quantity);
                command.Parameters.AddWithValue("@price", orderItem.Price);

                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        // Delete method for OrderItems table
        // input int id of the order item to be deleted
        public bool Delete(int id)
        {
            SqlConnection conn = Database.GetConnection();

            using (SqlCommand command = new("DELETE FROM OrderItems WHERE id = @id", conn))
            {
                command.Parameters.AddWithValue("@id", id);
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        // Get all order items from OrderItems table
        // returns list of all order items
        public List<OrderItem> GetAll()
        {
            SqlConnection conn = Database.GetConnection();
            List<OrderItem> orderItems = new();

            using (SqlCommand command = new("SELECT * FROM OrderItems", conn))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    OrderItem orderItem = new(
                        (int)reader["id"],
                        (int)reader["order_id"],
                        (int)reader["product_id"],
                        (int)reader["quantity"],
                        (double)reader["price"]
                    );
                    orderItems.Add(orderItem);
                }
                reader.Close();
            }
            return orderItems;
        }

        // Get order item by ID
        // input int id of the order item
        // returns OrderItem object if found, otherwise null
        public OrderItem GetById(int id)
        {
            SqlConnection conn = Database.GetConnection();

            using (SqlCommand command = new("SELECT * FROM OrderItems WHERE id = @id", conn))
            {
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = command.ExecuteReader();
                OrderItem? orderItem = null;

                if (reader.Read())
                {
                    orderItem = new OrderItem(
                        (int)reader["id"],
                        (int)reader["order_id"],
                        (int)reader["product_id"],
                        (int)reader["quantity"],
                        (double)reader["price"]
                    );
                }
                reader.Close();
                return orderItem;
            }
        }
    }
}
