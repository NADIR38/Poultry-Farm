using Microsoft.AspNetCore.Mvc;
using Poultary.BL.Bl;
using Poultary.BL.Models;
using Poultary.DL;
using Poultary.Interfaces;
using pro.DL;

namespace sample.Controllers
{
    public class MortalityController : Controller
    {
        mortalityinterface m=new mortalityBl();
        public IActionResult Index()
        {
            var records = m.getmortality();
            var dailyDeaths = mortalityDL.GetDailyDeathsForCurrentMonth(); // Dictionary<DateTime, int>

            ViewBag.MonthLabels = dailyDeaths.Keys
                .OrderBy(d => d)
                .Select(d => d.ToString("dd MMM")) // Format as "28 Jun"
                .ToList();

            ViewBag.MonthData = dailyDeaths
                .OrderBy(kvp => kvp.Key)
                .Select(kvp => kvp.Value)
                .ToList();

            return View(records);
        }
        [HttpGet]
       public IActionResult Create()
        {
            var batchnames = m.getbatchnames("");
            ViewBag.batchnames = batchnames;
            return View( );
        }

        [HttpPost]
        public IActionResult Create(mortality mortality)
        {
            if (ModelState.IsValid)
            {
                var result= m.addmortality(mortality);
                TempData["Message"] = "Mortality record created successfully!";
                TempData["MessageType"] = "success";
                return RedirectToAction("Index");
            }
            TempData["Message"] = "Failed to create mortality record. Please check the input data.";
            TempData["MessageType"] = "error";
            return View(mortality);
        }
        public IActionResult Edit(int id)
        {
            var batch=m.getmortality().FirstOrDefault(b =>b.mortalityId==id);
            if (batch == null)
            {
                TempData["Message"] = "Batch not found.";
                TempData["MessageType"] = "error";
                return RedirectToAction("Index");
            }

            return View(batch);
        }
        [HttpPost]
        public IActionResult Edit(mortality mortality)
        {
            if (ModelState.IsValid)
            {
                var batch = m.updatemortality(mortality);
                TempData["Message"] = "Mortality record updated successfully!";
                TempData["MessageType"] = "success";
                return RedirectToAction("Index");
            }
            TempData["Message"] = "Failed to update mortality record. Please check the input data.";
            TempData["MessageType"] = "error";
            return View(mortality);
        }
        public IActionResult SearchBatches(string term)
        {
            var matches = m.getbatchnames(term );
            return Json(matches);
        }

    }
}
