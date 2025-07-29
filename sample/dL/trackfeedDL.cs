using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using PoultryProject.BL.Models;

namespace PoultryProject.DL
{
    public class trackfeedDL
    {
        // Create
        public bool addtrack(trackfeed g)
        {
            try
            {
                int batchid = getbatchid(g.name);  

                if (g.batchid == -1)
                {
                    return false;  // Batch not found
                }

                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = "INSERT INTO feedusage (FeedBatchID, SacksUsed, Date) VALUES (@BatchID, @SacksUsed, @Date)";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@BatchID", batchid);
                        cmd.Parameters.AddWithValue("@SacksUsed", g.sacksUsed);
                        cmd.Parameters.AddWithValue("@Date", g.date);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in addtrack: {ex.Message}", ex);
            }

        }

        // Read
        public List<trackfeed> getAllTracks()
        {
            List<trackfeed> list = new List<trackfeed>();

            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = @"SELECT f.UsageID, f.FeedBatchID, fb.BatchName, f.SacksUsed, f.Date 
                             FROM feedusage f 
                             JOIN feedBatches fb ON f.FeedBatchID = fb.FeedBatchID";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Constructor: trackfeed(int id, string name, int batchid, int sacksUsed, DateTime date)
                                trackfeed tf = new trackfeed(
                                    reader.GetInt32("UsageID"),
                                    reader.GetString("BatchName"),
                                    reader.GetInt32("FeedBatchID"),
                                    reader.GetInt32("SacksUsed"),
                                    reader.GetDateTime("Date")
                                );

                                list.Add(tf);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in getAllTracks: {ex.Message}");
                // Optionally, you can log this or rethrow
            }

            return list;
        }





        // Update
        public bool updateTrack(trackfeed g)
        {
            try
            {
                int batchid = getbatchid(g.name);
                if (g.batchid == -1) return false;

                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = @"UPDATE feedusage 
                                     SET FeedBatchID = @batchid, SacksUsed = @sacksUsed, Date = @date 
                                     WHERE UsageID = @id";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@batchid", g.batchid);
                        cmd.Parameters.AddWithValue("@sacksUsed", g.sacksUsed);
                        cmd.Parameters.AddWithValue("@date", g.date);
                        cmd.Parameters.AddWithValue("@id", batchid);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in updateTrack: {ex.Message}");
                return false;
            }
        }

        // Delete
        public bool deleteTrack(int id)
        {
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = "DELETE FROM feedusage WHERE UsageID = @id";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in deleteTrack: {ex.Message}");
                return false;
            }
        }

        // Helper: Get BatchID from FeedBatchName
        private static int getbatchid(string name)
        {
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT FeedBatchID FROM feedBatches WHERE BatchName = @name";
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
                Console.WriteLine($"Error in getbatchid: {ex.Message}");
                return -1;
            }
        }
        public List<string> GetChickBatchNames()
        {
            List<string> names = new List<string>();
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT BatchName FROM feedbatches";

                    using (var cmd = new MySqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            names.Add(reader.GetString("BatchName"));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching chick batch names: {ex.Message}");
            }

            return names;
        }

        public static List<trackfeed> SearchTrackFeeds(string searchText)
        {
            List<trackfeed> list = new List<trackfeed>();

            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();

                    string query = @"SELECT f.UsageID, f.FeedBatchID, fb.BatchName, f.SacksUsed, f.Date 
                             FROM feedusage f
                             JOIN feedBatches fb ON f.FeedBatchID = fb.FeedBatchID
                             WHERE fb.BatchName LIKE @search
                                OR DATE_FORMAT(f.Date, '%Y-%m-%d') LIKE @search";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        string safeSearch = $"%{searchText.Replace('/', '-')}%";
                        cmd.Parameters.AddWithValue("@search", safeSearch);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                trackfeed tf = new trackfeed(
                                    reader.GetInt32("UsageID"),
                                    reader.GetString("BatchName"),
                                    reader.GetInt32("FeedBatchID"),
                                    reader.GetInt32("SacksUsed"),
                                    reader.GetDateTime("Date")
                                );

                                list.Add(tf);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error searching feed usage: {ex.Message}");
            }

            return list;
        }

    }
}
