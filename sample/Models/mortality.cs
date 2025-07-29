using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poultary.BL.Models
{
    public class mortality
    {
        private string name;

        public int mortalityId { get; set; }
        public int batchId { get; set; }
        public string batchName { get; set; }
        public int count { get; set; }
        public int remainingCount { get; set; }
        public DateTime date { get; set; }
        public string reason { get; set; }
        public mortality(int mortalityId,int batchId,string batchname,int count,int remainingcount,DateTime date,string reason)
        {
            this.mortalityId=mortalityId;
            this.batchId=batchId;
            this.batchName=batchname;
            this.count=count;
            this.remainingCount = remainingcount;
            this.date=date;
            this.reason=reason;

        }

        public mortality(string name, int count, DateTime date, string reason)
        {
            this.name = name;
            this.count = count;
            this.date = date;
            this.reason = reason;
        }
        public mortality(string batchname,int batchId)
        {
            this.batchName = batchname;
            this.batchId = batchId;
        }

        public mortality(int v1, string batchName, int count, int v2, DateTime date, string reason)
        {
            this.batchName = batchName;
            this.count = count;
            this.date = date;
            this.reason = reason;
        }
        public mortality()
        {
            // Parameterless constructor for model binding
        }
    }
}
