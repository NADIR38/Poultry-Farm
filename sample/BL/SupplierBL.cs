using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Windows.Forms;
using pro.BL.Bl;
using pro.Interface;
using Poultary.BL.Models;
using pro.DL;

namespace pro.BL.Bl
{
    public class SupplierBL:Isupplier
    {
        

        public bool Add(Supplier s)
        {
            if (string.IsNullOrWhiteSpace(s.Name))
            {
                //MessageBox.Show("Name is required.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(s.Contact))
            {
                //MessageBox.Show("Contact is required.");
                return false;
            }

          
            if (string.IsNullOrWhiteSpace(s.Address))
            {
                //MessageBox.Show("Address is required.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(s.SupplierType))
            {
                //MessageBox.Show("Please select an option from the ComboBox.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return SupplierDL.AddSupplier(s);
        }

        public bool Update(Supplier s, int id)
        {
            if (string.IsNullOrWhiteSpace(s.Name))
            {
                //MessageBox.Show("Name is required.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(s.Contact))
            {
                //MessageBox.Show("Contact is required.");
                return false;
            }

          

            if (string.IsNullOrWhiteSpace(s.Address))
            {
                //MessageBox.Show("Address is required.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(s.SupplierType))
            {
                //MessageBox.Show("Please select an option from the ComboBox.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }


            return SupplierDL.UpdateSupplier(s, id);
        }

        public bool delete(int id)
        {
            return SupplierDL.DeleteSupplier(id);
        }

        public List<Supplier> GetSuppliers()
        {
            return SupplierDL.GetSuppliers();
        }

        public List<Supplier> GetSuppliersbyName(string name)
        {
            return SupplierDL.SearchSuppliersByName(name);
        }
        public List<string> getsupplierbytype(string type)
        {
            return SupplierDL.GetSupplierNamesByType(type);
        }
    }
}

