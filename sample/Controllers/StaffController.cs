using Microsoft.AspNetCore.Mvc;
using pro.Interface;
using pro.BL.Model;
using Poultary.BL.Models;

namespace sample.Controllers
{
    public class StaffController : Controller
    {
        private readonly Istaff _istaff;

        public StaffController(Istaff istaff)
        {
             _istaff = istaff;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var Satff=_istaff.GetStaff();
            return View(Satff);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Staffs s)
        {
            if (ModelState.IsValid)
            {
                if(_istaff.Add(s))
                {
                    TempData["Message"] = "Staff added successfully!";
                    TempData["MessageType"] = "success";
                    return RedirectToAction("Index");
                }
                TempData["Message"] = "Failed to add Staff.";
                TempData["MessageType"] = "error";
            }
            return View(s);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var staff = _istaff.GetStaff().FirstOrDefault(s => s.StaffID == id);
            if (staff == null)
            {
                TempData["Message"] = "Staff not found.";
                TempData["MessageType"] = "error";
                return RedirectToAction("Index");
            }
            return View(staff);
        }

        [HttpPost]
        public IActionResult Edit(Staffs s)
        {
            if (ModelState.IsValid)
            {
                if (_istaff.Update(s, s.StaffID))
                {
                    TempData["Message"] = "Staff updated successfully!";
                    TempData["MessageType"] = "success";
                    return RedirectToAction("Index");
                }

                TempData["Message"] = "Failed to update Staff.";
                TempData["MessageType"] = "error";
            }
            return View(s);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var success = _istaff.delete(id);
            if (success)
            {
                TempData["Message"] = "Staff deleted successfully!";
                TempData["MessageType"] = "success";
            }
            else
            {
                TempData["Message"] = "Failed to delete Staff.";
                TempData["MessageType"] = "error";
            }
            return RedirectToAction("Index");
        }
    }
}
