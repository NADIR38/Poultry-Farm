using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Poultary.BL.Bl;
using Poultary.Interfaces;
using PoultryProject.DL;
using pro.DL;
using sample.Models;

namespace sample.Controllers
{
    public class FeedController : Controller
    {
        public feedinterface ibl = new feedBL();
        public IActionResult Index()
        {
            var list = ibl.getfeed();
            return View(list);
        }
        public IActionResult Create()
        {
            var supplierNames = SupplierDL.GetSupplierNamesLike("", "Feed");
            ViewBag.Suppliers = supplierNames
                .Select(name => new SelectListItem
                {
                    Value = name,
                    Text = name
                })
                .ToList();
            return View(new feed());
        }
        [HttpGet]
        public IActionResult SearchSuppliers(string term)
        {
            var matches = SupplierDL.GetSupplierNamesLike(term ?? "", "Feed");
            return Json(matches);
        }
        [HttpPost]
        public IActionResult Create(feed f)
        {
            if (ModelState.IsValid)
            {
                ibl.addfeed(f);
                TempData["Message"] = "Batch Added successfully!";
                TempData["MessageType"] = "success";
                return RedirectToAction("Index");
            }
          return View(f);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var feed = ibl.getfeed().FirstOrDefault(f => f.id == id);
            if (feed == null)
            {
                TempData["Message"] = "Batch not found.";
                TempData["MessageType"] = "error";
                return RedirectToAction("Index");
            }
            return View(feed);
        }
        [HttpPost]
            public IActionResult Edit(feed f)
        {
            var supplierNames = SupplierDL.GetSupplierNamesLike("", "Feed");
            ViewBag.Suppliers = supplierNames.Select(name => new SelectListItem
            {
                Value = name,
                Text = name
            }).ToList();

            if(ModelState.IsValid)
            {
                ibl.updatefeed(f);
                TempData["Message"] = "Batch updated successfully!";
                TempData["MessageType"] = "success";
                return RedirectToAction("Index");
            }
            return View(f);
        }

    }
}
