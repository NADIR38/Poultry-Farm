using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Poultary.BL.Models;

namespace Poultary.DL
{
    public class feedDl
    {
        public static bool addfeed(feed c)
        {
            try
            {
                int supplierid = GetConditionIdByName(c.suppliername);
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = "INSERT INTO feedbatches (BatchName, PurchaseDate, batchprice, weight, QuantitySacks, SupplierID) " +
                                   "VALUES (@BatchName, @purchaseDate, @batchprice, @batchweight, @batchquantity, @supplier_id)";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@BatchName", c.name);
                        cmd.Parameters.AddWithValue("@purchaseDate", c.purchasedate);
                        cmd.Parameters.AddWithValue("@batchprice", c.price);
                        cmd.Parameters.AddWithValue("@batchweight", c.weight);
                        cmd.Parameters.AddWithValue("@batchquantity", c.quantity);
                        cmd.Parameters.AddWithValue("@supplier_id",supplierid);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in addfeed: " + ex.Message);
                return false;
            }
        }

        public static bool updatefeed(feed c)
        {
            try
            {
                int supplierid = GetConditionIdByName(c.suppliername);
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = "UPDATE feedbatches SET BatchName = @BatchName, PurchaseDate = @purchaseDate, " +
                                   "batchprice = @batchprice, weight = @batchweight, QuantitySacks = @batchquantity, " +
                                   "SupplierID = @supplier_id WHERE FeedBatchID = @BatchId";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@BatchId", c.id);
                        cmd.Parameters.AddWithValue("@BatchName", c.name);
                        cmd.Parameters.AddWithValue("@purchaseDate", c.purchasedate);
                        cmd.Parameters.AddWithValue("@batchprice", c.price);
                        cmd.Parameters.AddWithValue("@batchweight", c.weight);
                        cmd.Parameters.AddWithValue("@batchquantity", c.quantity);
                        cmd.Parameters.AddWithValue("@supplier_id", supplierid);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in updatefeed: " + ex.Message);
                return false;
            }
        }

        public static bool deletefeed(int id)
        {
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = "DELETE FROM feedbatches WHERE FeedBatchID = @BatchId";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@BatchId", id);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in deletefeed: " + ex.Message);
                return false;
            }
        }

        public static List<feed> getfeed()
        {
            List<feed> batches = new List<feed>();
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT fb.FeedBatchID, fb.BatchName, fb.PurchaseDate,s.SupplierID, fb.batchprice, fb.weight, fb.QuantitySacks, s.Name " +
                                   "FROM feedbatches fb JOIN suppliers s ON fb.SupplierID = s.SupplierID ";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                feed batch = new feed(
                                    reader.GetInt32("FeedBAtchID"),
                                    reader.GetString("BatchName"),
                                    reader.GetFloat("weight"),
                                    reader.GetInt32("QuantitySacks"),
                                    reader.GetInt32("SupplierID"),
                                    reader.GetString("Name"),
                                    reader.GetDateTime("PurchaseDate"),
                                    reader.GetInt32("batchprice")
                                );
                                batches.Add(batch);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in getfeed: " + ex.Message);
            }
            return batches;
        }

        private static int GetConditionIdByName(string name)
        {
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT SupplierID FROM suppliers WHERE SupplierType = 'feed' AND Name = @name";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", name);
                        object result = cmd.ExecuteScalar();
                        return result != null ? Convert.ToInt32(result) : -1;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in GetConditionIdByName: " + ex.Message);
                return -1;
            }
        }
    }
}
