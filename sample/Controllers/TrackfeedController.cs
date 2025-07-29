using Microsoft.AspNetCore.Mvc;
using PoultryProject.BL.Bl;
using PoultryProject.BL.Models;
using PoultryProject.DL;

namespace sample.Controllers
{
    public class Trackfeed : Controller
    {
        private ITrackfeed ibl=new trackfeedBL();
      
        public IActionResult Index()
        {
          var list=  ibl.getAllTracks();
            
            return View(list);
        }
        [HttpGet]
        public IActionResult create()
        {
            var batchnames=ibl.GetChickBatchNames();
            ViewBag.batchnames = batchnames;
            return View();
        }
        [HttpPost]
        public IActionResult create(trackfeed dl)
        {
            if(ModelState.IsValid)
            {
                var result=ibl.addtrack(dl);
                TempData["Message"] = "Batch added successfully!";
                TempData["MessageType"] = "success";
                return RedirectToAction("Index");
            }
            var list=ibl.GetChickBatchNames();
            return View(dl);
        }

    }
}
