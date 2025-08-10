using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pro.BL.Model;

namespace pro.BL.Bl
{
    public class StaffBL : Interface.Istaff
    {
        public bool Add(Staffs s)
        {
            if (string.IsNullOrWhiteSpace(s.Name)) return false;
            if (string.IsNullOrWhiteSpace(s.Contact)) return false;
            if (string.IsNullOrWhiteSpace(s.CNIC) || s.CNIC.Length != 13) return false;
            if (string.IsNullOrWhiteSpace(s.Role)) return false;

            return DL.StaffDL.AddStaff(s);
        }

        public bool Update(Staffs s, int id)
        {
            if (string.IsNullOrWhiteSpace(s.Name)) return false;
            if (string.IsNullOrWhiteSpace(s.Contact)) return false;
            if (string.IsNullOrWhiteSpace(s.CNIC) || s.CNIC.Length != 13) return false;
            if (string.IsNullOrWhiteSpace(s.Role)) return false;

            return DL.StaffDL.UpdateStaff(s, id);
        }

        public bool delete(int id)
        {
            return DL.StaffDL.DeleteStaff(id);
        }

        public List<Staffs> GetStaff()
        {
            return DL.StaffDL.GetStaff();
        }

        public List<Staffs> GetStaffbyName(string name)
        {
            return DL.StaffDL.SearchStaffByName(name);
        }
    }
}
