using BarRating.Data;
using BarRating.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BarRating.Controllers
{
    [Authorize(Roles = "Admin,Member")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            int numberOfUsers = _context.Users.Count();
            ViewBag.NumberOfUsers = numberOfUsers;
            int numberOfBars = _context.Bars.Count();
            ViewBag.NumberOfBars = numberOfBars;
            int numberOfReviews = _context.Reviews.Count();
            ViewBag.NumberOfReviews = numberOfReviews;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}