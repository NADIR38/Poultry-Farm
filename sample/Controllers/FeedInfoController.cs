using Microsoft.AspNetCore.Mvc;
using PoultryProject.BL.Bl;
using PoultryProject.DL;
using PoultryProject.Interfaces;

namespace sample.Controllers
{
    public class FeedInfoController : Controller
    {
        public Ifeedinfo ibl = new feedinfoBL();
        public IActionResult Index()
        {
            var list=ibl.getinfo();
            var usagePercentages = feedinfoDL.GetFeedUsagePercentage();
            ViewBag.PercentLabels = usagePercentages.Keys.ToList();
            ViewBag.PercentData = usagePercentages.Values.ToList();
            var feedData = feedinfoDL.GetFeedEfficiency();

            ViewBag.BarLabels = feedData.Select(f => f.name).ToList();
            ViewBag.UsedData = feedData.Select(f => f.sacksUsed).ToList();
            ViewBag.RemainingData = feedData.Select(f => f.remaining).ToList();
            return View(list);

        }
    }
}
