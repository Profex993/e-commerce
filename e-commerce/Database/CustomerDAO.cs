using e_commerce.Entities;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace e_commerce.Database
{
    internal class CustomerDAO
    {
        //Database Access Object for customers table

        //Singleton instance
        private static CustomerDAO? _instance;
        private CustomerDAO() { }
        public static CustomerDAO Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CustomerDAO();
                }
                return _instance;
            }
        }

        //Add customer to database
        //input Customer object to be added
        public bool Add(Customer customer)
        {
            SqlConnection conn = Database.GetConnection();

            string query = "INSERT INTO customers (name, email, phone, address, registration_date) " +
                           "VALUES (@name, @email, @phone, @address, @registration_date)";

            using (SqlCommand command = new SqlCommand(query, conn))
            {
                command.Parameters.AddWithValue("@name", customer.Name);
                command.Parameters.AddWithValue("@email", customer.Email);
                command.Parameters.AddWithValue("@phone", customer.Phone ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@address", customer.Address ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@registration_date", customer.RegistrationDate);

                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        //Update method for Customer table
        //input Customer object to be updated
        public bool Update(Customer customer)
        {
            SqlConnection conn = Database.GetConnection();

            using (SqlCommand command = new SqlCommand("UPDATE customers " +
                "SET name = @name, email = @email, phone = @phone, address = @address " +
                "WHERE id = @id", conn))
            {
                command.Parameters.AddWithValue("@name", customer.Name);
                command.Parameters.AddWithValue("@email", customer.Email);
                command.Parameters.AddWithValue("@phone", customer.Phone ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@address", customer.Address ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@id", customer.Id);

                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        //Delete method for customer table
        //input string name of customer to be deleted
        public bool Delete(string name)
        {
            SqlConnection conn = Database.GetConnection();

            using (SqlCommand command = new SqlCommand("DELETE FROM customers WHERE name = @name", conn))
            {
                command.Parameters.AddWithValue("@id", name);
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        //Get all from customer tables
        //returns list of all customers from customers table
        public List<Customer> GetAll()
        {
            SqlConnection conn = Database.GetConnection();
            List<Customer> customers = new();

            using (SqlCommand command = new("SELECT * FROM customers", conn))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Customer customer = new(
                        (int)reader["id"],
                        reader["name"].ToString(),
                        reader["email"].ToString(),
                        reader["phone"].ToString(),
                        reader["address"].ToString(),
                        DateTime.Parse(reader["registration_date"].ToString())
                    );
                    customers.Add(customer);
                }
                reader.Close();
            }
            return customers;
        }

        //returns Customer object by id
        //input int id
        public Customer GetById(int id)
        {
            SqlConnection conn = Database.GetConnection();

            using (SqlCommand command = new("SELECT * FROM customers WHERE id = @id", conn))
            {
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = command.ExecuteReader();
                Customer? customer = null;
                if (reader.Read())
                {
                    customer = new Customer(
                        (int)reader["id"],
                        reader["name"].ToString(),
                        reader["email"].ToString(),
                        reader["phone"].ToString(),
                        reader["address"].ToString(),
                        DateTime.Parse(reader["registration_date"].ToString())
                    );
                }
                reader.Close();
                return customer;
            }
        }

        //returns Customer object by name
        //input string customer name
        public Customer GetByName(string name)
        {
            SqlConnection conn = Database.GetConnection();

            using (SqlCommand command = new SqlCommand("SELECT * FROM customers WHERE name = @name", conn))
            {
                command.Parameters.AddWithValue("@name", name);
                SqlDataReader reader = command.ExecuteReader();
                Customer? customer = null;
                if (reader.Read())
                {
                    customer = new Customer(
                        (int)reader["id"],
                        reader["name"].ToString(),
                        reader["email"].ToString(),
                        reader["phone"].ToString(),
                        reader["address"].ToString(),
                        DateTime.Parse(reader["registration_date"].ToString())
                    );
                }
                reader.Close();
                return customer;
            }
        }

        //import from XML
        public void AddFromXML(string file)
        {
            try
            {
                XDocument xmlDoc = XDocument.Load(file);

                foreach (var customer in xmlDoc.Descendants("Customer"))
                {
                    string name = customer.Element("name")?.Value ?? "Unknown";
                    string email = customer.Element("email")?.Value ?? "No email";
                    string phone = customer.Element("phone").Value;
                    string address = customer.Element("stock_quantity").Value;
                    DateTime date = DateTime.Parse(customer.Element("registration_date").Value);
                    Instance.Add(new Customer(name, email, phone, address, date));
                }
                Console.WriteLine("Done.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing XML file: {ex.Message}");
            }
        }
    }
}
