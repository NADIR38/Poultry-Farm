using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using sample.Interfaces;
using sample.Models;
using System.Data;

namespace sample.dL
{
    public class CustomersPaymentDl : ICrudDL<CustomerPayments>
    {
        public static DataTable LoadCustomerPayments()
        {
            DataTable dt = new DataTable();

            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = @"
                    SELECT 
                        b.BillID AS 'Bill ID',
                        c.Name AS 'Name',
                        b.weight AS 'Weight',
                        b.TotalAmount AS 'Total Amount',
                        p.`Payed Amount` AS 'Paid Amount',
                        p.`Dueamount` AS 'Remaining Amount',
                        b.SaleDate AS 'SaleDate'
                    FROM customerbills b
                    JOIN customers c ON b.CustomerID = c.CustomerID
                    JOIN customerpayments p ON p.BillID = b.BillID
                    ORDER BY b.BillID DESC;
                ";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }
            }

            return dt;
        }

        public static DataTable SearchCustomerPayments(string customerName)
        {
            DataTable dt = new DataTable();

            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = @"
            SELECT 
                r.BillID AS 'Bill ID',
                c.Name AS 'Customer Name',
                r.Date AS 'Payment Date',
                r.Payment AS 'Paid Amount'
            FROM customerpricerecord r
            JOIN customers c ON r.Customer_ID = c.CustomerID
            WHERE c.Name LIKE @name
            ORDER BY r.Date DESC;
        ";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", "%" + customerName + "%");

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public static double GetTotalDueTocustomer()
        {
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT SUM(`Dueamount`) FROM customerpayments;";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        object result = cmd.ExecuteScalar();
                        return result != DBNull.Value && result != null ? Convert.ToDouble(result) : 0.0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching total customer dues: " + ex.Message);
                return 0.0;
            }
        }

        public bool Add(CustomerPayments t)
        {

            try
            {
                string query = @"insert into customerpayments (CustomerId,BillID,`payed amount`,Dueamount,Notes) values (@customerID,@billID,@payedAmount,@dueamount,@notes)";
                var parameterDict = new Dictionary<string, object>
                {
                    {"@customerID", t.CustomerId },
                    {"@billID", t.BillId },
                    {"@payedAmount", t.PayedAmount },
                    {"@dueamount", t.DueAmount },
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

        public bool Update(CustomerPayments t, int id)
        {

            try
            {
                string query = @"update customerpayments 
                                 set CustomerId = @customerID, 
                                     BillId = @billID,               
                                     `payed amount` = @payedAmount,
                                    DueAmount = @dueamount    ,
                                    notes = @Notes    
                                 where PaymentId = @id";

                var parameterDict = new Dictionary<string, object>
                {
                    {"@customerID", t.CustomerId },
                    {"@billID", t.BillId },
                    {"@payedAmount", t.PayedAmount },
                    {"@dueamount", t.DueAmount },
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

        public bool Delete(int id)
        {
            try
            {
                string query = @"delete from customerpayments where PaymentId=@ID";
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

        public List<CustomerPayments> GetAllList()
        {

            string query = "SELECT CustomerId,BillID,`payed amount`,Dueamount,Notes FROM customerpayments";
            List<CustomerPayments> cus = new List<CustomerPayments>();

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CustomerPayments s = new CustomerPayments
                        {
                            CustomerId = reader["CustomerId"] != DBNull.Value ? Convert.ToInt32(reader["CustomerId"]) : 0,
                            BillId = reader["BillID"] != DBNull.Value ? Convert.ToInt32(reader["BillID"]) : 0,
                            PayedAmount = reader["payed amount"] != DBNull.Value ? Convert.ToInt32(reader["payed amount"]) : 0,
                            DueAmount = reader["Dueamount"] != DBNull.Value ? Convert.ToInt32(reader["Dueamount"]) : 0,
                            
                            Notes = reader["Notes"] != DBNull.Value ? reader["Notes"].ToString() : string.Empty
                        };
                        cus.Add(s);
                    }
                }
            }

            return cus;
        }

        public CustomerPayments GetById(int id)
        {
            string query = "SELECT CustomerId,BillID,`payed amount`,Dueamount,Notes FROM customerbills WHERE PaymentId=@id";
            CustomerPayments payments = null;

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
                            payments = new CustomerPayments
                            {
                                CustomerId = reader["CustomerId"] != DBNull.Value ? Convert.ToInt32(reader["CustomerId"]) : 0,
                                BillId = reader["BillID"] != DBNull.Value ? Convert.ToInt32(reader["BillID"]) : 0,
                                PayedAmount = reader["payed amount"] != DBNull.Value ? Convert.ToInt32(reader["payed amount"]) : 0,
                                DueAmount = reader["Dueamount"] != DBNull.Value ? Convert.ToInt32(reader["Dueamount"]) : 0,
                                Notes = reader["Notes"] != DBNull.Value ? reader["Notes"].ToString() : string.Empty
                            };
                        }
                    }
                }
            }

            return payments;
        }
    }
    }
}
