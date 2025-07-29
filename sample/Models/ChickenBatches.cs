using System;

namespace Poultary.BL.Models
{
    public class ChickenBatches
    {
        public int BatchId { get; set; }
        public string BatchName { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int BatchPrice { get; set; }
        public double BatchWeight { get; set; }
        public int BatchQuantity { get; set; }
        public string SupplierName { get; set; }
        public int SupplierId { get; set; }

        // Full constructor (used when BatchId and SupplierId are known)
        public ChickenBatches(int batchId, string batchName, DateTime purchaseDate, int batchPrice, double batchWeight, int batchQuantity, string supplierName, int supplierId)
        {
            BatchId = batchId;
            BatchName = batchName;
            PurchaseDate = purchaseDate;
            BatchPrice = batchPrice;
            BatchWeight = batchWeight;
            BatchQuantity = batchQuantity;
            SupplierName = supplierName;
            SupplierId = supplierId;
        }

        // Constructor for new batch (without BatchId and SupplierId)
        public ChickenBatches(string batchName, DateTime purchaseDate, int batchPrice, double batchWeight, int batchQuantity, string supplierName)
        {
            BatchName = batchName;
            PurchaseDate = purchaseDate;
            BatchPrice = batchPrice;
            BatchWeight = batchWeight;
            BatchQuantity = batchQuantity;
            SupplierName = supplierName;
        }

        // Parameterless constructor for model binding
        public ChickenBatches() { }
    }
}
