using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoultryProject.BL.Models
{
   public  class feedinfo
    {
        public int id { get; set; }
        public string name { get; set; }
        public int sacksUsed { get; set; }
        public int totalsacks { get; set; }
        public int remaining { get; set; }
        public string batchname { get; set; }
        public string suppliername { get; set; }
        public feedinfo()
        {

        }
    }
  
}
