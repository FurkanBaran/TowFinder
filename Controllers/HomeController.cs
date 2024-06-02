using Microsoft.AspNetCore.Mvc;
using TowFinder.ViewModels;
using TowFinder.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var cities = _context.TowOperators.Select(t => t.City).Distinct().ToList();
        return View(new HomeViewModel { Cities = cities });
    }



    [HttpGet]
    public IActionResult GetDistricts(string city)
    {
        var districts = _context.TowOperators
            .Where(t => t.City == city)
            .Select(t => t.District)
            .Distinct()
            .ToList();

        return Json(districts);
    }

    [HttpGet]
    public IActionResult GetTowOperators(string city, string district)
    {
        var towOperatorsQuery = _context.TowOperators.AsQueryable();

        if (!string.IsNullOrEmpty(city))
        {
            towOperatorsQuery = towOperatorsQuery.Where(t => t.City == city);
        }

        if (!string.IsNullOrEmpty(district))
        {
            towOperatorsQuery = towOperatorsQuery.Where(t => t.District == district);
        }

        var towOperators = towOperatorsQuery.Where(t => t.ApprovalStatus).ToList();

        return Json(towOperators);
    }


    public IActionResult Error()
    {
        return View();
    }
}
