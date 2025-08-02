using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using sample.Interfaces;
using sample.Models;

namespace sample.dL
{
    public class CustomersBillsDl : ICrudDL<CustomerBills>
    {
        public bool Add(CustomerBills t)
        {
            try
            {
                string query = @"insert into customerbills (BillID,CustomerId,SaleDate,weight,TotalAmount,Notes) values (@billID,@customerID,,@saleDate,@weight,@totalAmount,@notes)";
                var parameterDict = new Dictionary<string, object>
                {
                    {"@billID", t.BillId },
                    {"@customerID", t.CustomerId },
                    {"@saleDate", t.SaleDate },
                    {"@weight", t.Weight },
                    {"@totalAmount", t.TotalAmount },
                    {"@notes", t.Notes }

                };

                MySqlParameter[] parameters = DatabaseHelper.CreateMySqlParameters(parameterDict);

                DatabaseHelper.ExecuteNonQuery(query, parameters);
                return true;
            }
            catch (SqlException) { return false; }
            catch (InvalidOperationException) { return false; }
            catch (Exception) { return false; }
        }

        public bool Delete(int id)
        {

            try
            {
                string query = @"delete from customerbills where BillID=@ID";
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

        public List<CustomerBills> GetAllList()
        {

            string query = "SELECT BillID,CustomerId,SaleDate,weight,TotalAmount,Notes FROM customerbills";
            List<CustomerBills> cus = new List<CustomerBills>();

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CustomerBills s = new CustomerBills
                        {
                            BillId = reader["BillID"] != DBNull.Value ? Convert.ToInt32(reader["BillID"]) : 0,
                            CustomerId = reader["CustomerId"] != DBNull.Value ? Convert.ToInt32(reader["CustomerId"]) : 0,
                            SaleDate = reader["SaleDate"] != DBNull.Value ? Convert.ToDateTime(reader["SaleDate"]) : DateTime.MinValue,

                            Weight = reader["weight"] != DBNull.Value ? Convert.ToInt32(reader["weight"]) : 0,
                            TotalAmount = reader["TotalAmount"] != DBNull.Value ? Convert.ToInt32(reader["TotalAmount"]) : 0,
                            Notes = reader["Notes"].ToString()
                        };
                        cus.Add(s);
                    }
                }
            }

            return cus;
        }        

        public bool Update(CustomerBills t, int id)
        {

            try
            {
                string query = @"update customerbills 
                                 set CustomerId = @customerID,               
                                     SaleDate = @saleDate,
                                    weight = @weight    ,
                                    TotalAmount = @totalAmount,
                                       Notes = @notes
                                 where BillID = @id";

                var parameterDict = new Dictionary<string, object>
                {
                    {"@customerID", t.CustomerId },
                    {"@saleDate", t.SaleDate },
                    {"@weight", t.Weight },
                    {"@totalAmount", t.TotalAmount },
                    {"@notes", t.Notes },
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
        public CustomerBills GetById(int id)
        {
            string query = "SELECT BillID, CustomerId, SaleDate, weight, TotalAmount, Notes FROM customerbills WHERE BillID = @id";
            CustomerBills bill = null;

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
                            bill = new CustomerBills
                            {
                                BillId = reader["BillID"] != DBNull.Value ? Convert.ToInt32(reader["BillID"]) : 0,
                                CustomerId = reader["CustomerId"] != DBNull.Value ? Convert.ToInt32(reader["CustomerId"]) : 0,
                                SaleDate = reader["SaleDate"] != DBNull.Value ? Convert.ToDateTime(reader["SaleDate"]) : DateTime.MinValue,
                                Weight = reader["weight"] != DBNull.Value ? Convert.ToInt32(reader["weight"]) : 0,
                                TotalAmount = reader["TotalAmount"] != DBNull.Value ? Convert.ToInt32(reader["TotalAmount"]) : 0,
                                Notes = reader["Notes"] != DBNull.Value ? reader["Notes"].ToString() : string.Empty
                            };
                        }
                    }
                }
            }

            return bill;
        }

    }
}
