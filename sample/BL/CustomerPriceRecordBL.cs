using sample.Interfaces;
using sample.Models;

namespace sample.BL
{
    public class CustomerPriceRecordBL : ICrudBL<CustomerPriceRecord>
    {
        private readonly ICrudDL<CustomerPayments> _customerPaymentsDL;

        public CustomerPriceRecordBL(ICrudDL<CustomerPayments> customerPaymentsDL)
        {
            _customerPaymentsDL = customerPaymentsDL;
        }
        public bool Add(CustomerPriceRecord t)
        {

            if (t.CustomerId <= 0) return false;
            if (t.BillId <= 0) return false;
            if (t.PayedAmount <= 0) return false;
            if (t.DueAmount < 0) return false;
            if (string.IsNullOrWhiteSpace(t.Notes)) return false;

            return _customerPaymentsDL.Add(t);
        }

        public bool Delete(int id)
        {
            return _customerPaymentsDL.Delete(id);
        }

        public List<CustomerPriceRecord> GetList()
        {
            return _customerPaymentsDL.GetAllList();
        }


        public bool Update(CustomerPriceRecord t, int id)
        {

            if (t.CustomerId <= 0) return false;
            if (t.BillId <= 0) return false;
            if (t.PayedAmount <= 0) return false;
            if (t.DueAmount < 0) return false;
            if (string.IsNullOrWhiteSpace(t.Notes)) return false;

            return _customerPaymentsDL.Update(t, id);
        }
    }
}
