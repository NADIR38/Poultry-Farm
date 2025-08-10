using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pro.BL.Model
{
    public class Staffs
    {
        public int StaffID { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public string Role { get; set; }
        public string CNIC { get; set; }

        public Staffs() { }
        public Staffs(string name, string contact, string role, string cnin)
        {
            Name = name;
            Contact = contact;
            Role = role;
            CNIC = cnin;
        }
    }
}
