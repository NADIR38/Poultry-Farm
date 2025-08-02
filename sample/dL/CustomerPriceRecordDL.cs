using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using sample.Interfaces;
using sample.Models;

namespace sample.dL
{
    public class CustomerPriceRecordDL : ICrudDL<CustomerPriceRecord>
    {
        public bool Add(CustomerPriceRecord t)
        {
            try
            {
                string query = @"insert into customerpricerecord (record_id,customer_id,date,payment,BillID) values (@recordId,@customer_id,,@date,@payment,@billId)";
                var parameterDict = new Dictionary<string, object>
                {
                    {"@recordId", t.RecordId },
                    {"@customer_id", t.CustomerId },
                    {"@date", t.Date },
                    {"@payment", t.Payment },
                    {"@billId", t.BillId },

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
                string query = @"delete from customerpricerecord where record_id=@ID";
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

        public List<CustomerPriceRecord> GetAllList()
        {

            string query = "SELECT record_id,customer_id,date,payment,BillID FROM customerpricerecord";
            List<CustomerPriceRecord> cus = new List<CustomerPriceRecord>();

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CustomerPriceRecord s = new CustomerPriceRecord
                        {
                            RecordId = reader["record_id"] != DBNull.Value ? Convert.ToInt32(reader["record_id"]) : 0,
                            CustomerId = reader["customer_id"] != DBNull.Value ? Convert.ToInt32(reader["customer_id"]) : 0,
                            Date = reader["date"] != DBNull.Value ? Convert.ToDateTime(reader["date"]) : DateTime.MinValue,

                            Payment = reader["payment"] != DBNull.Value ? Convert.ToInt32(reader["payment"]) : 0,
                            BillId = reader["BillID"] != DBNull.Value ? Convert.ToInt32(reader["BillID"]) : 0,
                        };
                        cus.Add(s);
                    }
                }
                return cus;
            }
        }

        public CustomerPriceRecord GetById(int id)
        {
            string query = "SELECT record_id,customer_id,date,payment,BillID FROM customerpricerecord WHERE record_id = @id";
            CustomerPriceRecord record = null;

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
                            record = new CustomerPriceRecord
                            {
                                RecordId = reader["record_id"] != DBNull.Value ? Convert.ToInt32(reader["record_id"]) : 0,
                                CustomerId = reader["customer_id"] != DBNull.Value ? Convert.ToInt32(reader["customer_id"]) : 0,
                                Date = reader["date"] != DBNull.Value ? Convert.ToDateTime(reader["date"]) : DateTime.MinValue,

                                Payment = reader["payment"] != DBNull.Value ? Convert.ToInt32(reader["payment"]) : 0,
                                BillId = reader["BillID"] != DBNull.Value ? Convert.ToInt32(reader["BillID"]) : 0,
                            };
                        }
                    }
                }
            }

            return record;
        }        

        public bool Update(CustomerPriceRecord t, int id)
        {

            try
            {
                string query = @"update customerpayments 
                                 set customer_id = @customerID,               
                                     date = @date,
                                    payment = @payment    ,
                                    BillID = @BillId,
                                 where record_id = @id";

                var parameterDict = new Dictionary<string, object>
                {
                    {"@customerID", t.CustomerId },
                    {"@date", t.Date },
                    {"@payment", t.Payment },
                    {"@BillId", t.BillId },
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
    }
}
