using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoultryProject.BL.Models
{
    public class trackfeed
    {
        public int id { get; set; }            // Assuming a primary key column
        public string name { get; set; }       // Feed batch name (used to find batchid)
        public int batchid { get; set; }
        public int sacksUsed { get; set; }
        public DateTime date { get; set; }
        public trackfeed(int id,string name,int batchid,int sacksUsed,DateTime date) 
        {
        this.id = id;
            this.name = name;
            this.batchid = batchid;
            this.sacksUsed=sacksUsed;
            this.date = date;
        
        
        }
        public trackfeed() { } // Default constructor for EF
        public trackfeed(string batchName, int count, DateTime date)
        {
            name = batchName;
            sacksUsed = count;
            this.date = date;
        }
    }

}
