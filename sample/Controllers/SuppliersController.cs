using Microsoft.AspNetCore.Mvc;
using Poultary.BL.Models;
using pro.Interface;

namespace sample.Controllers
{
    public class SuppliersController : Controller
    {
        private readonly Isupplier _supplierBL;

        public SuppliersController(Isupplier supplierBL)
        {
            _supplierBL = supplierBL;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var suppliers = _supplierBL.GetSuppliers();
            return View(suppliers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Supplier s)
        {
            if (ModelState.IsValid)
            {
                if (_supplierBL.Add(s))
                {
                    TempData["Message"] = "Supplier added successfully!";
                    TempData["MessageType"] = "success";
                    return RedirectToAction("Index");
                }

                TempData["Message"] = "Failed to add supplier.";
                TempData["MessageType"] = "error";
            }
            return View(s);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var supplier = _supplierBL.GetSuppliers().FirstOrDefault(s => s.SupplierID == id);
            if (supplier == null)
            {
                TempData["Message"] = "Supplier not found.";
                TempData["MessageType"] = "error";
                return RedirectToAction("Index");
            }
            return View(supplier);
        }

        [HttpPost]
        public IActionResult Edit(Supplier s)
        {
            if (ModelState.IsValid)
            {
                if (_supplierBL.Update(s, s.SupplierID))
                {
                    TempData["Message"] = "Supplier updated successfully!";
                    TempData["MessageType"] = "success";
                    return RedirectToAction("Index");
                }

                TempData["Message"] = "Failed to update supplier.";
                TempData["MessageType"] = "error";
            }
            return View(s);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var success = _supplierBL.delete(id);
            if (success)
            {
                TempData["Message"] = "Supplier deleted successfully!";
                TempData["MessageType"] = "success";
            }
            else
            {
                TempData["Message"] = "Failed to delete supplier.";
                TempData["MessageType"] = "error";
            }
            return RedirectToAction("Index");
        }
    }
}
