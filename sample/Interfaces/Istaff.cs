using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pro.BL.Model;

namespace pro.Interface
{
    public interface Istaff
    {
        bool Add(Staffs s);
        bool Update(Staffs s, int id);
        bool delete(int id);
        List<Staffs> GetStaff();

        List<Staffs> GetStaffbyName(string text);
    }
}
