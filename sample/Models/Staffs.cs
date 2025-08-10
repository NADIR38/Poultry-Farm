using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace pro.BL.Model
{
    public class Staffs
    {
        public int StaffID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Contact is required")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "Contact must be exactly 11 digits")]
        public string Contact { get; set; }

        [Required(ErrorMessage = "Role is required")]
        [RegularExpression("^(Admin|Employee)$", ErrorMessage = "Role must be either Admin or Employee")]
        public string Role { get; set; }

        [Required(ErrorMessage = "CNIC is required")]
        [RegularExpression(@"^\d{13}$", ErrorMessage = "CNIC must be exactly 13 digits")]
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
