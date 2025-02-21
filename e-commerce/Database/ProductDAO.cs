using e_commerce.Entities;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace e_commerce.Database
{
    internal class ProductDAO
    {
        //class for database acces to Product tables
        
        //singleton instance
        private static ProductDAO _instance;
        private ProductDAO() { }
        public static ProductDAO Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ProductDAO();
                }
                return _instance;
            }
        }

        //Add method for Product table
        //input Product to be added
        public bool Add(Product product)
        {
            SqlConnection conn = Database.GetConnection();

            using (SqlCommand command = new("INSERT INTO Products (name, description, price, stock_quantity) VALUES (@name, @description, @price, @stock_quantity)", conn))
            {
                command.Parameters.AddWithValue("@name", product.Name);
                command.Parameters.AddWithValue("@description", product.Description ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@price", product.Price);
                command.Parameters.AddWithValue("@stock_quantity", product.StockQuantity);

                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        //Update method for Product table
        //input Product object with updated values
        public bool Update(Product product)
        {
            SqlConnection conn = Database.GetConnection();

            using (SqlCommand command = new("UPDATE Products " +
                "SET name = @name, description = @description, price = @price, stock_quantity = @stock_quantity " +
                "WHERE id = @id", conn))
            {
                command.Parameters.AddWithValue("@id", product.Id);
                command.Parameters.AddWithValue("@name", product.Name);
                command.Parameters.AddWithValue("@description", product.Description ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@price", product.Price);
                command.Parameters.AddWithValue("@stock_quantity", product.StockQuantity);

                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        //Delete method for Product table
        //input int id of product to be deleted
        public bool Delete(int id)
        {
            SqlConnection conn = Database.GetConnection();

            using (SqlCommand command = new("DELETE FROM Products WHERE id = @id", conn))
            {
                command.Parameters.AddWithValue("@id", id);
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        //Returns list of Product objects of all products in products table
        public List<Product> GetAll()
        {
            SqlConnection conn = Database.GetConnection();
            List<Product> products = new();

            using (SqlCommand command = new("SELECT * FROM Products", conn))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Product product = new Product(
                        (int)reader["id"],
                        reader["name"].ToString(),
                        reader["description"] as string,
                        (double)reader["price"],
                        (int)reader["stock_quantity"]
                    );
                    products.Add(product);
                }
                reader.Close();
            }
            return products;
        }

        //Returns Product object by id
        //input int id of the product
        public Product GetById(int id)
        {
            SqlConnection conn = Database.GetConnection();

            using (SqlCommand command = new("SELECT * FROM Products WHERE id = @id", conn))
            {
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = command.ExecuteReader();
                Product product = null;

                if (reader.Read())
                {
                    product = new Product(
                        (int)reader["id"],
                        reader["name"].ToString(),
                        reader["description"] as string,
                        (double)reader["price"],
                        (int)reader["stock_quantity"]
                    );
                }
                reader.Close();
                return product;
            }
        }

        //Returns Product object by name
        //input string name of the product
        public Product GetByName(string name)
        {
            SqlConnection conn = Database.GetConnection();

            using (SqlCommand command = new("SELECT * FROM Products WHERE name = @name", conn))
            {
                command.Parameters.AddWithValue("@name", name);
                SqlDataReader reader = command.ExecuteReader();
                Product product = null;

                if (reader.Read())
                {
                    product = new Product(
                        (int)reader["id"],
                        reader["name"].ToString(),
                        reader["description"] as string,
                        (double)reader["price"],
                        (int)reader["stock_quantity"]
                    );
                }
                reader.Close();
                return product;
            }
        }

        //imports product from XML
        //input absolute path to the XML file
        public void AddFromXML(string file)
        {
            try
            {
                XDocument xmlDoc = XDocument.Load(file);

                foreach (var product in xmlDoc.Descendants("Product"))
                {
                    string name = product.Element("name")?.Value ?? "Unknown";
                    string description = product.Element("description")?.Value ?? "No description";
                    int price = product.Element("price") != null ? int.Parse(product.Element("price").Value) : 0;
                    int stock_quantity = product.Element("stock_quantity") != null ? int.Parse(product.Element("stock_quantity").Value) : 0;

                    Instance.Add(new Product(name, description, price, stock_quantity));
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
