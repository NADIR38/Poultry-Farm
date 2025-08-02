using sample.Interfaces;
using sample.Models;

namespace sample.BL
{
    public class CustomerPriceRecordBL : ICrudBL<CustomerPriceRecord>
    {
        private readonly ICrudDL<CustomerPriceRecord> _customerPriceRecordDL;

        public CustomerPriceRecordBL(ICrudDL<CustomerPriceRecord> customerPriceRecordDL)
        {
            _customerPriceRecordDL = customerPriceRecordDL;
        }
        public bool Add(CustomerPriceRecord t)
        {

            if (t.CustomerId <= 0) return false;
            if (t.Date <= DateTime.MinValue) return false;
            if (t.Payment <= 0) return false;
            if (t.BillId < 0) return false;
            return _customerPriceRecordDL.Add(t);
        }

        public bool Delete(int id)
        {
            if (id <= 0) return false;

            return _customerPriceRecordDL.Delete(id);
        }

        public CustomerPriceRecord GetById(int id)
        {
            if (id <= 0) return null;

            return _customerPriceRecordDL.GetById(id);
        }

        public List<CustomerPriceRecord> GetList()
        {
            return _customerPriceRecordDL.GetAllList();
        }


        public bool Update(CustomerPriceRecord t, int id)
        {

            if (t.CustomerId <= 0) return false;
            if (t.Date <= DateTime.MinValue) return false;
            if (t.Payment <= 0) return false;
            if (t.BillId < 0) return false;

            return _customerPriceRecordDL.Update(t, id);
        }
    }
}
