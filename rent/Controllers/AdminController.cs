using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using rent.Data;
using System.Linq;
using System.Threading.Tasks;
using rent.Models;

namespace rent.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var resources = _context.Resurs.ToList();
            var reviews = _context.Recenzija.ToList();
            var reservations = _context.Rezervacija.ToList();

            var model = new AdminViewModel
            {
                Resources = resources,
                Reviews = reviews,
                Reservations = reservations
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteResource(long id)
        {
            var resource = await _context.Resurs.FindAsync(id);
            if (resource != null)
            {
                _context.Resurs.Remove(resource);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteReview(long id)
        {
            var review = await _context.Recenzija.FindAsync(id);
            if (review != null)
            {
                _context.Recenzija.Remove(review);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> CancelReservation(long id)
        {
            var reservation = await _context.Rezervacija.FindAsync(id);
            if (reservation != null)
            {
                _context.Rezervacija.Remove(reservation);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
