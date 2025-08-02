using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using sample.Interfaces;
using sample.Models;

namespace pro.DL
{
    public class CustomerDL : ICrudDL<Customers>
    {
        public bool Add(Customers t)
        {
            try
            {
                string query = @"insert into customers (Name,ContactInfo,Address) values (@name,@contact,@address)";
                var parameterDict = new Dictionary<string, object>
                {
                    {"@name", t.Name },
                    {"@contact", t.Contact },
                    {"@address", t.Address },
                };

                MySqlParameter[] parameters = DatabaseHelper.CreateMySqlParameters(parameterDict);

                DatabaseHelper.ExecuteNonQuery(query, parameters);
                return true;
            }
            catch (SqlException) { return false; }
            catch (InvalidOperationException) { return false; }
            catch (Exception) { return false; }
        }

        public bool Update(Customers t, int id)
        {
            try
            {
                string query = @"update customers 
                                 set Name = @name, 
                                     ContactInfo = @contact,               
                                     Address = @address                                 
                                 where CustomerID = @id";

                var parameterDict = new Dictionary<string, object>
                {
                    {"@name", t.Name },
                    {"@contact", t.Contact },
                    {"@address", t  .Address },
                    {"@id", id }
                };

                MySqlParameter[] parameters = DatabaseHelper.CreateMySqlParameters(parameterDict);


                int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, parameters);
                return rowsAffected > 0;
            }
            catch (SqlException) { return false; }
            catch (InvalidOperationException) { return false; }
            catch (Exception) { return false; }
        }

            public bool Delete(int id)
        {
            try
            {
                string query = @"delete from customers where CustomerID=@ID";
                var parameterDict = new Dictionary<string, object>
                {
                    {"@ID", id }
                };

                MySqlParameter[] parameters = DatabaseHelper.CreateMySqlParameters(parameterDict);


                int rows = DatabaseHelper.ExecuteNonQuery(query, parameters);
                return rows > 0;
            }
            catch (SqlException) { return false; }
            catch (Exception) { return false; }
        }

            public List<Customers> GetAllList()
        {
            string query = "SELECT CustomerID, Name, ContactInfo, Address FROM customers";
            List<Customers> cus = new List<Customers>();

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Customers s = new Customers
                        {

                            CustomerId = reader["CustomerID"] != DBNull.Value ? Convert.ToInt32(reader["CustomerID"]) : 0,
                            Name = reader["Name"] != DBNull.Value ? reader["Name"].ToString() : string.Empty,
                            Contact = reader["ContactInfo"] != DBNull.Value ? reader["ContactInfo"].ToString() : string.Empty,
                            Address = reader["Address"] != DBNull.Value ? reader["Address"].ToString() : string.Empty
                        };
                        cus.Add(s);
                    }
                }
            }

            return cus;
        }

        public static List<Customers> SearchCustomersByName(string name)
        {
            string query = @"SELECT CustomerID, Name, ContactInfo, Address 
                             FROM customers 
                             WHERE Name LIKE @name";

            List<Customers> cus = new List<Customers>();

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", "%" + name + "%");

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Customers s = new Customers
                            {
                                CustomerId = reader["CustomerID"] != DBNull.Value ? Convert.ToInt32(reader["CustomerID"]) : 0,
                                Name = reader["Name"] != DBNull.Value ? reader["Name"].ToString() : string.Empty,
                                Contact = reader["ContactInfo"] != DBNull.Value ? reader["ContactInfo"].ToString() : string.Empty,
                                Address = reader["Address"] != DBNull.Value ? reader["Address"].ToString() : string.Empty
                            };
                            cus.Add(s);
                        }
                    }
                }
            }

            return cus;
        }

        public Customers GetById(int id)
        {
            string query = "SELECT Name, ContactInfo, Address  FROM customers WHERE CustomerID = @id";
            Customers customer = null;

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            customer = new Customers
                            {
                                CustomerId = reader["CustomerID"] != DBNull.Value ? Convert.ToInt32(reader["CustomerID"]) : 0,

                                Name = reader["Name"] != DBNull.Value ? reader["Name"].ToString() : string.Empty,
                                Contact = reader["ContactInfo"] != DBNull.Value ? reader["ContactInfo"].ToString() : string.Empty,

                                Address = reader["Address"] != DBNull.Value ? reader["Address"].ToString() : string.Empty
                            };
                        }
                    }
                }
            }

            return customer;
        }
    }
    
}
