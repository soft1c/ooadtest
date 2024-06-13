using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rent.Data;
using rent.Models;
using System.Security.Claims;
using rent.Models.ViewModels;

namespace rent.Controllers
{
    public class RezervacijaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RezervacijaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Rezervacija
        public async Task<IActionResult> Index()
        {
            return View(await _context.Rezervacija.ToListAsync());
        }

        // GET: Rezervacija/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rezervacija = await _context.Rezervacija
                .FirstOrDefaultAsync(m => m.IdRezervacije == id);
            if (rezervacija == null)
            {
                return NotFound();
            }

            return View(rezervacija);
        }

        // GET: Rezervacija/Create
        public IActionResult Create()
        {
            return View();
        }   

        // POST: Rezervacija/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdRezervacije,IdOsobe,IdResursa,Pocetak,Kraj,Status")] Rezervacija rezervacija)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rezervacija);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rezervacija);
        }

        // GET: Rezervacija/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rezervacija = await _context.Rezervacija.FindAsync(id);
            if (rezervacija == null)
            {
                return NotFound();
            }
            return View(rezervacija);
        }

        // POST: Rezervacija/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("IdRezervacije,IdOsobe,IdResursa,Pocetak,Kraj,Status")] Rezervacija rezervacija)
        {
            if (id != rezervacija.IdRezervacije)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rezervacija);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RezervacijaExists(rezervacija.IdRezervacije))
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
            return View(rezervacija);
        }

        // GET: Rezervacija/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rezervacija = await _context.Rezervacija
                .FirstOrDefaultAsync(m => m.IdRezervacije == id);
            if (rezervacija == null)
            {
                return NotFound();
            }

            return View(rezervacija);
        }

        // POST: Rezervacija/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var rezervacija = await _context.Rezervacija.FindAsync(id);
            if (rezervacija != null)
            {
                _context.Rezervacija.Remove(rezervacija);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RezervacijaExists(long id)
        {
            return _context.Rezervacija.Any(e => e.IdRezervacije == id);
        }

        // GET: Rezervacija/MyReservations
        public async Task<IActionResult> MyReservations()
{
    var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
    if (string.IsNullOrEmpty(currentUserId))
    {
        return Unauthorized();
    }

    long userIdHash = HashHelper.GetLongHash(currentUserId);

    var mojeRezervacije = await _context.Rezervacija
        .Include(r => r.Resurs)
        .Where(r => r.IdOsobe == userIdHash)
        .ToListAsync();

    var rezervacijeOdMene = await _context.Rezervacija
        .Include(r => r.Resurs)
        .Where(r => _context.Resurs.Any(v => v.IdResursa == r.IdResursa && v.IdVlasnika == userIdHash) && r.Status == Status.UObradi)
        .ToListAsync();

    var model = new ReservationsViewModel
    {
        MojeRezervacije = mojeRezervacije,
        RezervacijeOdMene = rezervacijeOdMene
    };

    return View(model);
}


        [HttpPost]
        public async Task<IActionResult> UpdateReservationStatus(long id, bool accept)
        {
            var rezervacija = await _context.Rezervacija.FindAsync(id);
            if (rezervacija == null)
            {
                return NotFound();
            }

            rezervacija.Status = accept ? Status.Potvrdjeno : Status.Odbijeno;
            _context.Update(rezervacija);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(MyReservations));
        }
    }
}
