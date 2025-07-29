using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using KIMS;
using MySql.Data.MySqlClient;
using Poultary.BL.Models;
//using Poultary.UI;

namespace Poultary.DL
{
    public class mortalityDL
    {
        public static bool addmortality(mortality m)
        {
            int id= getbatchid(m.batchName);
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "INSERT INTO chickmortality (BatchID, Date, Count, Reason) " +
                               "VALUES (@batchId, @mortalityDate, @mortalityQuantity, @mortalityReason)";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@batchId", id);
                    cmd.Parameters.AddWithValue("@mortalityDate", m.date);
                    cmd.Parameters.AddWithValue("@mortalityQuantity", m.count);
                    cmd.Parameters.AddWithValue("@mortalityReason", m.reason);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        public static bool updatemortality(mortality m)
        {
            int id = getbatchid(m.batchName);
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "UPDATE chickmortality SET batchId = @batchId, Date = @mortalityDate, count = @mortalityQuantity, reason = @mortalityReason " +
                               "WHERE MortalityID = @mortalityId";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@batchId", m.batchId);
                    cmd.Parameters.AddWithValue("@mortalityDate", m.date);
                    cmd.Parameters.AddWithValue("@mortalityQuantity", m.count);
                    cmd.Parameters.AddWithValue("@mortalityReason", m.reason);
                    cmd.Parameters.AddWithValue("@mortalityId", id);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        public static List<mortality> GetMortalities()
        {
            List<mortality> mortalities = new List<mortality>();
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "SELECT cm.MortalityID, cb.BatchId, cb.BatchName, cm.count, cm.Date, cm.reason " +
                             "FROM chickmortality cm JOIN chickbatches cb ON cm.BatchId = cb.BatchId";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            mortality m = new mortality(
                                reader.GetInt32("MortalityID"),
                                reader.GetInt32("BatchId"),
                                reader.GetString("BatchName"),
                                reader.GetInt32("count"),
                                0, 
                                reader.GetDateTime("Date"),
                                reader.GetString("reason")
                            );
                            mortalities.Add(m);
                        }
                    }
                }
            }
            return mortalities;
        }
        public static bool deletemortality(int id)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "DELETE FROM chickmortality WHERE MortalityID = @mortalityId";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@mortalityId", id);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        public static int getbatchid(string name)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "SELECT BatchID FROM chickbatches WHERE  BatchName = @name";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", name);
                    object result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : -1;
                }
            }
        }
        public static List<string> GetBatchNames(string text)
        {
            List<string> batches = new List<string>();

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "SELECT BatchName FROM chickbatches WHERE BatchName LIKE @searchText";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@searchText", "%" + text + "%");

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            batches.Add(reader.GetString("BatchName"));
                        }
                    }
                }
            }

            return batches;
        }


        public static List<mortality> SearchMortalityByDate(DateTime date)
        {
            List<mortality> mortalities = new List<mortality>();

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = @"SELECT cm.MortalityID, cb.BatchId, cb.BatchName, cm.count, cm.Date, cm.reason 
                         FROM chickmortality cm 
                         JOIN chickbatches cb ON cm.BatchId = cb.BatchId
                         WHERE DATE(cm.Date) = @searchDate";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@searchDate", date.Date);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            mortality m = new mortality(
                                reader.GetInt32("MortalityID"),
                                reader.GetInt32("BatchId"),
                                reader.GetString("BatchName"),
                                reader.GetInt32("count"),
                                0,
                                reader.GetDateTime("Date"),
                                reader.GetString("reason")
                            );
                            mortalities.Add(m);
                        }
                    }
                }
            }

            return mortalities;
        }
        public static List<mortality> SearchMortalities(string searchText)
        {
            List<mortality> results = new List<mortality>();

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();

                string query = @"SELECT cm.MortalityID, cb.BatchId, cb.BatchName, cm.count, cm.Date, cm.reason
                         FROM chickmortality cm
                         JOIN chickbatches cb ON cm.BatchId = cb.BatchId
                         WHERE cb.BatchName LIKE @searchName
                            OR DATE_FORMAT(cm.Date, '%Y-%m-%d') LIKE @searchDate";

                using (var cmd = new MySqlCommand(query, conn))
                {
               
                    string safeSearch = $"%{searchText.Replace('/', '-')}%";
                    cmd.Parameters.AddWithValue("@searchName", safeSearch);
                    cmd.Parameters.AddWithValue("@searchDate", safeSearch);  

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            mortality m = new mortality(
                                reader.GetInt32("MortalityID"),
                                reader.GetInt32("BatchId"),
                                reader.GetString("BatchName"),
                                reader.GetInt32("count"),
                                0,
                                reader.GetDateTime("Date"),
                                reader.GetString("reason")
                            );
                            results.Add(m);
                        }
                    }
                }
            }

            return results;
        }
        public static int GetTodayDeaths()
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = @"
            SELECT SUM(Count) AS total_died
            FROM chickmortality
            WHERE DATE(Date) = CURDATE();
        ";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    object result = cmd.ExecuteScalar();
                    return result != DBNull.Value ? Convert.ToInt32(result) : 0;
                }
            }
        }
        public static Dictionary<DateTime, int> GetDailyDeathsForCurrentMonth()
        {
            Dictionary<DateTime, int> deathsByDay = new Dictionary<DateTime, int>();

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = @"
            SELECT 
                DATE(Date) AS MortalityDate, 
                SUM(Count) AS TotalDeaths
            FROM 
                chickmortality
            WHERE 
                MONTH(Date) = MONTH(CURDATE()) 
                AND YEAR(Date) = YEAR(CURDATE())
            GROUP BY 
                DATE(Date)
            ORDER BY 
                DATE(Date);";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DateTime date = reader.GetDateTime("MortalityDate");
                            int deaths = reader.GetInt32("TotalDeaths");
                            deathsByDay[date] = deaths;
                        }
                    }
                }
            }

            return deathsByDay;
        }


    }
}