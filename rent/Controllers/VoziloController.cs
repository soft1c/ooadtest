using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rent.Data;
using rent.Models;
using rent.Models.ViewModels;

namespace rent.Controllers
{
    [Authorize]
    public class VoziloController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VoziloController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Vozilo
        public async Task<IActionResult> Index()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(currentUserId))
            {
                return Unauthorized();
            }

            long userIdHash = HashHelper.GetLongHash(currentUserId);

            var vozila = await _context.Vozilo
                .Where(v => v.IdVlasnika == userIdHash)
                .ToListAsync();

            return View(vozila);
        }

        public async Task<IActionResult> Details(long? id)
{
    if (id == null)
    {
        return NotFound();
    }

    var vozilo = await _context.Vozilo
        .FirstOrDefaultAsync(m => m.IdResursa == id);
    if (vozilo == null)
    {
        return NotFound();
    }

    // Fetch the reviews for this vehicle
    var recenzije = await _context.Recenzija
        .Where(r => r.IdResursa == id)
        .ToListAsync();

    var viewModel = new VoziloDetailsViewModel
    {
        Vozilo = vozilo,
        Recenzije = recenzije
    };

    return View(viewModel);
}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reserve(long id, DateTime Pocetak, DateTime Kraj)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(currentUserId))
            {
                return Unauthorized();
            }

            if (Pocetak > Kraj)
            {
                TempData["ErrorMessage"] = "Pocetak rezervacije mora biti prije kraja rezervacije!";
                return RedirectToAction("Details", "Vozilo", new { id });
            }

            long userIdHash = HashHelper.GetLongHash(currentUserId);

            var rezervacija = new Rezervacija
            {
                IdOsobe = userIdHash,
                IdResursa = id,
                Pocetak = Pocetak,
                Kraj = Kraj,
                Status = Status.UObradi
            };

            _context.Rezervacija.Add(rezervacija);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Rezervacija je uspješno napravljena!";
            return RedirectToAction("Details", "Rezervacija", new { id = rezervacija.IdRezervacije });
        }

        // GET: Vozilo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vozilo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Tip,Marka,Model,Godiste,BrojSjedista,TipGoriva,Naziv,Opis,ImagePath,Cijena")] Vozilo vozilo)
        {
            if (ModelState.IsValid)
            {
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(currentUserId))
                {
                    return Unauthorized();
                }
                vozilo.Ocjena = 0;

                vozilo.IdVlasnika = HashHelper.GetLongHash(currentUserId);

                _context.Add(vozilo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vozilo);
        }

        // GET: Vozilo/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vozilo = await _context.Vozilo.FindAsync(id);
            if (vozilo == null)
            {
                return NotFound();
            }
            return View(vozilo);
        }

        // POST: Vozilo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Tip,Marka,Model,Godiste,BrojSjedista,TipGoriva,ImagePath,IdResursa,IdVlasnika,Naziv,Opis,Ocjena,Cijena")] Vozilo vozilo)
        {
            if (id != vozilo.IdResursa)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vozilo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VoziloExists(vozilo.IdResursa))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vozilo);
        }

        // GET: Vozilo/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vozilo = await _context.Vozilo
                .FirstOrDefaultAsync(m => m.IdResursa == id);
            if (vozilo == null)
            {
                return NotFound();
            }

            return View(vozilo);
        }

        // POST: Vozilo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var vozilo = await _context.Vozilo.FindAsync(id);
            if (vozilo != null)
            {
                _context.Vozilo.Remove(vozilo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VoziloExists(long id)
        {
            return _context.Vozilo.Any(e => e.IdResursa == id);
        }
    }

    
}
