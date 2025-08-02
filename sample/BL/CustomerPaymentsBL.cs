using pro.DL;
using sample.Interfaces;
using sample.Models;

namespace sample.BL
{
    public class CustomerPaymentsBL : ICrudBL<CustomerPayments>
    {
        private readonly ICrudDL<CustomerPayments> _customerPaymentsDL;

        public CustomerPaymentsBL(ICrudDL<CustomerPayments> customerPaymentsDL)
        {
            _customerPaymentsDL = customerPaymentsDL;
        }
        public bool Add(CustomerPayments t)
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
            if (id <= 0) return false;

            return _customerPaymentsDL.Delete(id);
        }

        public CustomerPayments GetById(int id)
        {
            if (id <= 0) return null;

            return _customerPaymentsDL.GetById(id);
        }

        public List<CustomerPayments> GetList()
        {
            return _customerPaymentsDL.GetAllList();
        }


        public bool Update(CustomerPayments t, int id)
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
