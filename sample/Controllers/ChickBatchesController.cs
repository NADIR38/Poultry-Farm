using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Poultary.BL.Models;
using Poultary.Interfaces;
using pro.DL;
using System;
using System.Linq;

namespace PoultryFarm.Web.Controllers
{
    public class ChickBatchesController : Controller
    {
        private readonly ChickenBatchInterface _bl;

        public ChickBatchesController(ChickenBatchInterface bl)
        {
            _bl = bl;
        }

        // GET: ChickBatches
        public IActionResult Index()
        {
            try
            {
                var batches = _bl.GetChickenBatches();
                return View(batches);
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Error loading batches.";
                TempData["MessageType"] = "error";
                return View(Enumerable.Empty<ChickenBatches>());
            }
        }

        // GET: ChickBatches/Create
        [HttpGet]
        public IActionResult Create()
        {
            try
            {
                LoadSuppliers();
                return View(new ChickenBatches());
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Error loading suppliers.";
                TempData["MessageType"] = "error";
                return RedirectToAction("Index");
            }
        }

        // POST: ChickBatches/Create
        [HttpPost]
        public IActionResult Create(ChickenBatches batch)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _bl.AddChickenBatch(batch);
                    TempData["Message"] = "Batch added successfully!";
                    TempData["MessageType"] = "success";
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", "Validation failed. Please check the inputs.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while saving the batch.");
            }

            LoadSuppliers(); // Refill dropdown on error
            return View(batch);
        }

        // GET: ChickBatches/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                var batch = _bl.GetChickenBatches().FirstOrDefault(b => b.BatchId == id);
                if (batch == null)
                {
                    TempData["Message"] = "Batch not found.";
                    TempData["MessageType"] = "error";
                    return RedirectToAction("Index");
                }

                return View(batch);
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Error loading batch.";
                TempData["MessageType"] = "error";
                return RedirectToAction("Index");
            }
        }

        // POST: ChickBatches/Edit/5
        [HttpPost]
        public IActionResult Edit(ChickenBatches batch)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _bl.UpdateChickenBatch(batch);
                    TempData["Message"] = "Batch updated successfully!";
                    TempData["MessageType"] = "success";
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", "Validation failed. Please check the inputs.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while updating the batch.");
            }

            return View(batch);
        }

        // POST: ChickBatches/Delete/5
        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                var success = _bl.DeleteChickenBatch(id);
                if (success)
                {
                    TempData["Message"] = "Batch deleted successfully!";
                    TempData["MessageType"] = "success";
                }
                else
                {
                    TempData["Message"] = "Failed to delete batch.";
                    TempData["MessageType"] = "error";
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Error occurred while deleting batch.";
                TempData["MessageType"] = "error";
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult SearchSuppliers(string term)
        {
            try
            {
                var matches = SupplierDL.GetSupplierNamesLike(term ?? "", "Chick");
                return Json(matches);
            }
            catch
            {
                return Json(new[] { "Error retrieving suppliers" });
            }
        }

        // Helper method to load suppliers
        private void LoadSuppliers()
        {
            var supplierNames = SupplierDL.GetSupplierNamesLike("", "Chick");
            ViewBag.Suppliers = supplierNames
                .Select(name => new SelectListItem
                {
                    Value = name,
                    Text = name
                })
                .ToList();
        }
    }
}
