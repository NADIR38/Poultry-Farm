using pro.DL;
using sample.Interfaces;
using sample.Models;

namespace sample.BL
{
    public class CustomerBillsBL : ICrudBL<CustomerBills>
    {
        private readonly ICrudDL<CustomerBills> _customerBillsDL;

        public CustomerBillsBL(ICrudDL<CustomerBills> customerBillsDL)
        {
            _customerBillsDL = customerBillsDL;
        }
        public bool Add(CustomerBills t)
        {
            if (t.BillId <= 0) return false;
            if (t.CustomerId <= 0) return false;
            if (t.SaleDate == DateTime.MinValue) return false;
            if (t.Weight < 0) return false;
            if (t.TotalAmount < 0) return false;
            if (string.IsNullOrWhiteSpace(t.Notes)) return false;

            return _customerBillsDL.Add(t);
        }

        public bool Delete(int id)
        {
            if (id <= 0) return false;

            return _customerBillsDL.Delete(id);
        }

        public CustomerBills GetById(int id)
        {
            if (id <= 0) return null;
            return _customerBillsDL.GetById(id);
        }

        public List<CustomerBills> GetList()
        {
            return _customerBillsDL.GetAllList();
        }

        public bool Update(CustomerBills t, int id)
        {
            if (t.BillId <= 0) return false;
            if (t.CustomerId <= 0) return false;
            if (t.SaleDate == DateTime.MinValue) return false;
            if (t.Weight < 0) return false;
            if (t.TotalAmount < 0) return false;
            if (string.IsNullOrWhiteSpace(t.Notes)) return false;

            return _customerBillsDL.Update(t, id);
        }
    }
}
