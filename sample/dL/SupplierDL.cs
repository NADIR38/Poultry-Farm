using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using Poultary.BL.Models;
namespace pro.DL
{
    public class SupplierDL
    {
        
        public static  bool AddSupplier(Supplier supplier)
        {
            try
            {
                string query = @"insert into suppliers (Name,ContactInfo,SupplierType,Address) values (@name,@contact,@type,@address)";
                var parameterDict = new Dictionary<string, object>
            {
                {"@name",supplier.Name },
                { "@contact", supplier.Contact },
                 { "@type", supplier.SupplierType },
                    { "@address", supplier.Address },

            };
                MySqlParameter[] parameters = parameterDict
               .Select(p => new MySqlParameter(p.Key, p.Value ?? DBNull.Value))
               .ToArray();
                DatabaseHelper.ExecuteNonQuery(query, parameters);
                return true;
            }

            catch
            {
                throw new ArgumentException("Unexpected error occured");
            }
          
        }

        public static  bool UpdateSupplier(Supplier supplier, int supplierID)
        {
            try
            {
                string query = @"update suppliers 
                             set Name = @name, 
                                 ContactInfo = @contact, 
                                  SupplierType = @type,   
                                 Address = @address                                 
                             where SupplierID = @id";
                var parameterDict = new Dictionary<string, object>
            {
                {"@name", supplier.Name },

                {"@contact", supplier.Contact },

                {"@address", supplier.Address },

                {"@type", supplier.SupplierType },
                    {"@id", supplierID }
            };
                MySqlParameter[] parameters = parameterDict
              .Select(p => new MySqlParameter(p.Key, p.Value ?? DBNull.Value))
              .ToArray();
                int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, parameters);

                // Check if update was successful (1 or more rows affected)
                return rowsAffected > 0;
            }

            catch(Exception ex)
            {
                throw new ArgumentException("Unexpected error occurred: " + ex.Message);
            }

           
        }
        public static  bool DeleteSupplier(int id)
        {
            try
            {
                string query = @"delete from suppliers where SupplierID=@ID";
                var parameterDict = new Dictionary<string, object>
                {
                    {"@ID",id }
                };
                MySqlParameter[] parameters = parameterDict
                  .Select(p => new MySqlParameter(p.Key, p.Value ?? DBNull.Value))
                  .ToArray();
                int rows = DatabaseHelper.ExecuteNonQuery(query, parameters);
                return rows > 0;
            }
            catch (Exception sqlEx)
            {
                throw new ArgumentException("unexpected error occured" + sqlEx.Message);
                //MessageBox.Show("Database error occurred: " + sqlEx.Message, "SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public static List<Supplier> GetSuppliers()
        {
            string query = "SELECT SupplierID, Name, ContactInfo,SupplierType, Address FROM suppliers";
            List<Supplier> suppliers = new List<Supplier>();

            using (var conn =DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Supplier s = new Supplier
                        {
                            SupplierID = Convert.ToInt32(reader["SupplierID"]),
                            Name = reader["Name"].ToString(),
                            Contact = reader["ContactInfo"].ToString(),
                            SupplierType = reader["SupplierType"].ToString(),
                            Address = reader["Address"].ToString()
                        };
                        suppliers.Add(s);
                    }
                }
            }

            return suppliers;
        }

        public static List<Supplier> SearchSuppliersByName(string keyword)
        {
            string query = @"SELECT SupplierID, Name, ContactInfo, SupplierType, Address 
                     FROM suppliers 
                     WHERE Name LIKE @keyword OR SupplierType LIKE @keyword";

            List<Supplier> suppliers = new List<Supplier>();

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Supplier s = new Supplier
                            {
                                SupplierID = Convert.ToInt32(reader["SupplierID"]),
                                Name = reader["Name"].ToString(),
                                Contact = reader["ContactInfo"].ToString(),
                                SupplierType = reader["SupplierType"].ToString(),
                                Address = reader["Address"].ToString()
                            };
                            suppliers.Add(s);
                        }
                    }
                }
            }

            return suppliers;
        }

        public static List<string> GetSupplierNamesByType(string supplierType)
        {
            List<string> names = new List<string>();

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "SELECT Name FROM suppliers WHERE SupplierType = @type";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@type", supplierType);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            names.Add(reader.GetString("Name"));
                        }
                    }
                }
            }

            return names;
        }
        public static List<string> GetSupplierNamesLike(string partialName, string type)
        {
            List<string> names = new List<string>();

            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT Name FROM suppliers WHERE Name LIKE @name AND SupplierType = @type";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", $"%{partialName}%");
                        cmd.Parameters.AddWithValue("@type", type);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                names.Add(reader.GetString("Name"));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching supplier names: {ex.Message}");
            }

            return names;
        }
    }

}
