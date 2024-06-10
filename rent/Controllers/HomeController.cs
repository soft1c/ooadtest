using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using rent.Data;
using rent.Models;
using System.Diagnostics;
using System.Linq;

namespace rent.Controllers
{
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
            var nekretnine = _context.Nekretnina.AsEnumerable();
            var vozila = _context.Vozilo.AsEnumerable();
            return View((nekretnine, vozila));
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
