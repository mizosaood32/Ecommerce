using BLL.Contract;
using BLL.Managers;
using Ecommerce.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers;
public class ReportsController : Controller
{
    private readonly IReportManager _reportmana;
    public ReportsController(IReportManager reportmana)
    {
        _reportmana = reportmana;
    }
    // GET: ReportsController
    public async Task<ActionResult> TopFiveSellingBooks(DateTime? sDate = null, DateTime? eDate = null)
    {
        try
        {
            // by default, get last 7 days record
            DateTime startDate = sDate ?? DateTime.UtcNow.AddDays(-7);
            DateTime endDate = eDate ?? DateTime.UtcNow;
            var topFiveSellingBooks = await _reportmana.GetTopNSellingBooksByDate(startDate, endDate);
            var vm = new TopNSoldBooksVm(startDate, endDate, (IEnumerable<TopNSoldBookModel>)topFiveSellingBooks);
            return View(vm);
        }
        catch (Exception ex)
        {
            TempData["errorMessage"] = "Something went wrong";
            return RedirectToAction("Index", "Home");
        }
    }
}