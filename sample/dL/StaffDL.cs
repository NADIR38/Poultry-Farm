using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using pro.BL.Model;
using Microsoft.Data.SqlClient;
namespace pro.DL
{
    public class StaffDL 
    {
        public static bool AddStaff(Staffs staff)
        {
            try
            {
                string query = @"insert into staff (Name,ContactInfo,Role,cnic) values (@name,@contact,@type,@cnic)";
                var parameterDict = new Dictionary<string, object>
            {
                {"@name",staff.Name },
                { "@contact", staff.Contact },
                 { "@type", staff.Role },
                    { "@cnic", staff.CNIC },

            };
                MySqlParameter[] parameters = parameterDict
               .Select(p => new MySqlParameter(p.Key, p.Value ?? DBNull.Value))
               .ToArray();
                DatabaseHelper.ExecuteNonQuery(query, parameters);
                return true;
            }

            catch (SqlException)
            {
                return false;

            }
            catch (InvalidOperationException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool UpdateStaff(Staffs staff, int StaffID)
        {
            try
            {
                string query = @"update staff 
                             set Name = @name, 
                                 ContactInfo = @contact, 
                                  Role = @type,   
                                 cnic = @cnic                                 
                             where StaffID = @id";
                var parameterDict = new Dictionary<string, object>
            {
                {"@name", staff.Name },

                {"@contact", staff.Contact },

                {"@cnic" , staff.CNIC },

                {"@type", staff.Role },
                    {"@id", StaffID }
            };
                MySqlParameter[] parameters = parameterDict
              .Select(p => new MySqlParameter(p.Key, p.Value ?? DBNull.Value))
              .ToArray();
                int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, parameters);

                // Check if update was successful (1 or more rows affected)
                return rowsAffected > 0;
            }



            catch (SqlException)
            {
                return false;

            }
            catch (InvalidOperationException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool DeleteStaff(int id)
        {
            try
            {
                string query = @"delete from staff where StaffID=@ID";
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
            catch (SqlException)
            {

                return false;
            }

        }

        public static List<Staffs> GetStaff()
        {
            string query = "SELECT StaffID, Name, ContactInfo,Role, cnic FROM staff";
            List<Staffs> staff = new List<Staffs>();

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Staffs s = new Staffs
                        {
                            StaffID = Convert.ToInt32(reader["StaffID"]),
                            Name = reader["Name"].ToString(),
                            Contact = reader["ContactInfo"].ToString(),
                            Role = reader["Role"].ToString(),
                            CNIC = reader["cnic"].ToString()
                        };
                        staff.Add(s);
                    }
                }
            }

            return staff;
        }

        public static List<Staffs> SearchStaffByName(string name)
        {
            string query = @"SELECT StaffID, Name, ContactInfo,Role, cnic 
                     FROM staff 
                     WHERE Name LIKE @name";

            List<Staffs> staff = new List<Staffs>();

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", "%" + name + "%"); // wildcard search

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Staffs s = new Staffs
                            {
                                StaffID = Convert.ToInt32(reader["StaffID"]),
                                Name = reader["Name"].ToString(),
                                Contact = reader["ContactInfo"].ToString(),
                                Role = reader["Role"].ToString(),
                                CNIC = reader["cnic"].ToString()
                            };
                            staff.Add(s);
                        }
                    }
                }
            }

            return staff;
        }

    }
}
