using System;
using System.Collections.Generic;
using System.Linq;
using Poultary.BL.Models;

namespace Poultary.BL.Bl
{
    public class ChickenbatchBL : Interfaces.ChickenBatchInterface
    {
        private chickbatchDL DL; 
        public ChickenbatchBL(chickbatchDL dL)
        {
            DL = dL;
        }


        public bool AddChickenBatch(ChickenBatches chickenBatch)
        {
            if (chickenBatch == null)
                throw new ArgumentNullException(nameof(chickenBatch), "Chicken batch cannot be null.");

            if (string.IsNullOrWhiteSpace(chickenBatch.BatchName))
                throw new ArgumentException("Batch name is required.");

            if (chickenBatch.BatchQuantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.");



            return DL.AddChickBatch(chickenBatch);
        }

        public List<ChickenBatches> GetChickenBatches()
        {
            return DL.GetChickBatches();
        }
        public bool UpdateChickenBatch(ChickenBatches chickenBatch)
        {

            if (chickenBatch == null)
                throw new ArgumentNullException(nameof(chickenBatch), "Chicken batch cannot be null.");
            if (chickenBatch.BatchId <= 0)
                throw new ArgumentException("Valid BatchId is required for update.");
            if (string.IsNullOrWhiteSpace(chickenBatch.BatchName))
                throw new ArgumentException("Batch name is required.");
            if (chickenBatch.BatchQuantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.");
            return DL.updatebatch(chickenBatch);
        }
        public bool DeleteChickenBatch(int batchId)
        {

            if (batchId <= 0)
                throw new ArgumentException("Valid BatchId is required for deletion.");
            return DL.deletebatch(batchId);
        }
        //public List<ChickenBatch> getsearchitem(string name)
        //{
        //    return DL.chickbatchDL.SearchBatchesByName(name);
        //}

    }
}

