using Microsoft.AspNetCore.Mvc;
using sample.Models;
using sample.Services;

namespace sample.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _service;
        public CustomerController(ICustomerService service) 
        {
            _service = service; 
        }

        public IActionResult Index(string search)
        {
            List<Customers> model = string.IsNullOrWhiteSpace(search)
                ? _service.GetAll()
                : _service.SearchByName(search);
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(Customers vm)
        {
            _service.Add(vm);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Update(Customers vm)
        {
            _service.Update(vm, vm.CustomerId);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
