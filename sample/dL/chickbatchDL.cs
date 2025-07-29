using MySql.Data.MySqlClient;
using Poultary.BL.Bl;
using Poultary.BL.Models;

public class chickbatchDL
{
   

    public bool AddChickBatch(ChickenBatches c)
    {
        int id = GetConditionIdByName(c.SupplierName);
        string query = "INSERT INTO chickbatches (BatchName, purchaseDate, batchprice, batchweight, Quantity, supplier_id) " +
                       "VALUES (@BatchName, @purchaseDate, @batchprice, @batchweight, @batchquantity, @supplier_id)";

        MySqlParameter[] parameters = {
            new("@BatchName", c.BatchName),
            new("@purchaseDate", c.PurchaseDate),
            new("@batchprice", c.BatchPrice),
            new("@batchweight", c.BatchWeight),
            new("@batchquantity", c.BatchQuantity),
            new("@supplier_id", id)
        };

        return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
    }

    public List<ChickenBatches> GetChickBatches()
    {
        var list = new List<ChickenBatches>();
        string query = "SELECT cb.BatchId, cb.BatchName, cb.purchaseDate, cb.batchprice, cb.batchweight, cb.Quantity, s.Name " +
                       "FROM chickbatches cb JOIN suppliers s ON cb.supplier_id = s.SupplierID WHERE s.SupplierType = 'Chick'";

        using var reader =DatabaseHelper.ExecuteReader(query);
        while (reader.Read())
        {
            list.Add(new ChickenBatches(
                reader.GetInt32("BatchId"),
                reader.GetString("BatchName"),
                reader.GetDateTime("purchaseDate"),
                reader.GetInt32("batchprice"),
                reader.GetDouble("batchweight"),
                reader.GetInt32("Quantity"),
                reader.GetString("Name"),
                GetConditionIdByName(reader.GetString("Name"))
            ));
        }

        return list;
    }

    private int GetConditionIdByName(string name)
    {
        string query = "SELECT SupplierID FROM suppliers WHERE SupplierType = 'Chick' AND Name = @name";
        var param = new[] { new MySqlParameter("@name", name) };
        var result = DatabaseHelper.ExecuteScalar(query, param);
        return result != null ? Convert.ToInt32(result) : -1;
    }
    public  bool updatebatch(ChickenBatches c)
    {
        int id = GetConditionIdByName(c.SupplierName);
        using (var conn = DatabaseHelper.GetConnection())
        {
            conn.Open();
            string query = "UPDATE chickbatches SET BatchName = @BatchName, purchaseDate = @purchaseDate, batchprice = @batchprice, batchweight = @batchweight, Quantity = @Quantity, supplier_id = @supplier_id WHERE BatchId = @BatchId";
            using (var cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@BatchName", c.BatchName);
                cmd.Parameters.AddWithValue("@purchaseDate", c.PurchaseDate);
                cmd.Parameters.AddWithValue("@batchprice", c.BatchPrice);
                cmd.Parameters.AddWithValue("@batchweight", c.BatchWeight);
                cmd.Parameters.AddWithValue("@Quantity", c.BatchQuantity);
                cmd.Parameters.AddWithValue("@supplier_id", id);
                cmd.Parameters.AddWithValue("@BatchId", c.BatchId);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
    public  bool deletebatch(int batchId)
    {
        using (var conn = DatabaseHelper.GetConnection())
        {
            conn.Open();
            string query = "DELETE FROM chickbatches WHERE BatchId = @BatchId";
            using (var cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@BatchId", batchId);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

    }
    public List<(string Month, int TotalQuantity)> GetMonthlyBatchQuantity()
    {
        var result = new List<(string, int)>();
        string query = @"
        SELECT DATE_FORMAT(purchaseDate, '%Y-%m') AS Month, SUM(Quantity) AS TotalQuantity
        FROM chickbatches
        GROUP BY Month
        ORDER BY Month;
    ";

        using var reader = DatabaseHelper.ExecuteReader(query);
        while (reader.Read())
        {
            string month = reader.GetString("Month");
            int total = reader.GetInt32("TotalQuantity");
            result.Add((month, total));
        }

        return result;
    }
    public List<(string SupplierName, int TotalChicks)> GetChickCountBySupplier()
    {
        var result = new List<(string, int)>();
        string query = @"
        SELECT s.Name AS SupplierName, SUM(cb.Quantity) AS TotalChicks
        FROM chickbatches cb
        JOIN suppliers s ON cb.supplier_id = s.SupplierID
        WHERE s.SupplierType = 'Chick'
        GROUP BY s.Name
        ORDER BY TotalChicks DESC;
    ";

        using var reader = DatabaseHelper.ExecuteReader(query);
        while (reader.Read())
        {
            string supplier = reader.GetString("SupplierName");
            int count = reader.GetInt32("TotalChicks");
            result.Add((supplier, count));
        }

        return result;
    }

}
