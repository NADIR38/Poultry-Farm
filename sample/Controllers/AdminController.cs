using Microsoft.AspNetCore.Mvc;
using Poultary.BL.Models; // Adjust namespace as per your folder structure
using System.Linq;

public class AdminController : Controller
{
    private readonly chickbatchDL _dl;

    public AdminController(chickbatchDL dl)
    {
        _dl = dl;
    }

    public IActionResult Dashboard()
    {
        // AREA CHART: Monthly chick quantities
        var monthlyData = _dl.GetMonthlyBatchQuantity();
        ViewBag.MonthLabels = monthlyData.Select(x => x.Month).ToList();
        ViewBag.MonthData = monthlyData.Select(x => x.TotalQuantity).ToList();

        // PIE CHART: Supplier vs Total Chicks provided
        var supplierData = _dl.GetChickCountBySupplier();
        ViewBag.PieLabels = supplierData.Select(x => x.SupplierName).ToList();
        ViewBag.PieValues = supplierData.Select(x => x.TotalChicks).ToList();

        return View();
    }
}
