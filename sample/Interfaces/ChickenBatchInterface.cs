using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Poultary.BL.Models;


namespace Poultary.Interfaces
{
   public interface ChickenBatchInterface
    {
        bool AddChickenBatch(ChickenBatches chickenBatch);
        List<ChickenBatches> GetChickenBatches();
        bool UpdateChickenBatch(ChickenBatches chickenBatch);
        bool DeleteChickenBatch(int batchId);
        //List<ChickenBatches> getsearchitem(string word);
    }
}
