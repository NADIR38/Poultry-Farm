using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pro.BL.Bl;
using Poultary.BL.Models;

namespace pro.Interface
{
    public interface Isupplier
    {
        bool Add(Supplier s);
        bool Update(Supplier s, int id);
        bool delete(int id);
        List<Supplier> GetSuppliers();

        List<Supplier> GetSuppliersbyName(string text);
        List<string> getsupplierbytype(string type);
    }
}
