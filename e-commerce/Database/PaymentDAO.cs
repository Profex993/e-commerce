using e_commerce.Entities;
using System.Data.SqlClient;

namespace e_commerce.Database
{
    internal class PaymentDAO
    {
        // Database Access Object for payments table

        // Singleton instance
        private static PaymentDAO _instance;
        private PaymentDAO() { }
        public static PaymentDAO Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PaymentDAO();
                }
                return _instance;
            }
        }

        // Add payment to database
        // input Payment object to be added
        public bool Add(Payment payment)
        {
            SqlConnection conn = Database.GetConnection();

            using (SqlCommand command = new("INSERT INTO Payments (order_id, payment_date, amount, payment_method)" +
                " VALUES (@order_id, @payment_date, @amount, @payment_method)", conn))
            {
                command.Parameters.AddWithValue("@order_id", payment.OrderId);
                command.Parameters.AddWithValue("@payment_date", payment.PaymentDate ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@amount", payment.Amount);
                command.Parameters.AddWithValue("@payment_method", payment.PaymentMethod);

                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        // Update method for Payments table
        // input Payment object to be updated
        public bool Update(Payment payment)
        {
            SqlConnection conn = Database.GetConnection();

            using (SqlCommand command = new("UPDATE Payments " +
                "SET order_id = @order_id, payment_date = @payment_date, amount = @amount, payment_method = @payment_method" +
                " WHERE id = @id", conn))
            {
                command.Parameters.AddWithValue("@id", payment.Id);
                command.Parameters.AddWithValue("@order_id", payment.OrderId);
                command.Parameters.AddWithValue("@payment_date", payment.PaymentDate ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@amount", payment.Amount);
                command.Parameters.AddWithValue("@payment_method", payment.PaymentMethod);

                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        // Delete method for Payments table
        // input int id of the payment to be deleted
        public bool Delete(int id)
        {
            SqlConnection conn = Database.GetConnection();

            using (SqlCommand command = new("DELETE FROM Payments WHERE id = @id", conn))
            {
                command.Parameters.AddWithValue("@id", id);
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        // Get all payments from Payments table
        // returns list of all payments
        public List<Payment> GetAll()
        {
            SqlConnection conn = Database.GetConnection();
            List<Payment> payments = new();

            using (SqlCommand command = new("SELECT * FROM Payments", conn))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Payment payment = new(
                        (int)reader["id"],
                        (int)reader["order_id"],
                        reader["payment_date"] != DBNull.Value
                            ? DateTime.Parse(reader["payment_date"].ToString())
                            : (DateTime?)null,
                        (double)reader["amount"],
                        reader["payment_method"].ToString()
                    );

                    payments.Add(payment);
                }
                reader.Close();
            }
            return payments;
        }

        // Get payment by ID
        // input int id of the payment
        // returns Payment object if found, otherwise null
        public Payment GetById(int id)
        {
            SqlConnection conn = Database.GetConnection();

            using (SqlCommand command = new("SELECT * FROM Payments WHERE id = @id", conn))
            {
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = command.ExecuteReader();
                Payment payment = null;

                if (reader.Read())
                {
                    payment = new(
                        (int)reader["id"],
                        (int)reader["order_id"],
                        reader["payment_date"] != DBNull.Value ? (DateTime)reader["payment_date"] : null,
                        (double)reader["amount"],
                        reader["payment_method"].ToString()
                    );
                }
                reader.Close();
                return payment;
            }
        }
    }
}
