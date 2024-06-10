using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using rent.Data;
using rent.Models;

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
            return View(await _context.Vozilo.ToListAsync());
        }

        // GET: Vozilo/Details/5
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

            return View(vozilo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reserve(long id, DateTime Pocetak, DateTime Kraj)
        {
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            long parsedUserId = currentUserId.GetHashCode();

            var rezervacija = new Rezervacija
            {
                IdOsobe = parsedUserId,
                IdResursa = id,
                Pocetak = Pocetak,
                Kraj = Kraj,
                Status = Status.UObradi
            };

            _context.Rezervacija.Add(rezervacija);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Rezervacija je uspješno napravljena!";
            return RedirectToAction(nameof(Details), new { id });
        }

        // GET: Vozilo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vozilo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // POST: Vozilo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Tip,Marka,Model,Godiste,BrojSjedista,TipGoriva,Naziv,Opis,Ocjena,ImagePath,IdVlasnika")] Vozilo vozilo)
        {
            if (ModelState.IsValid)
            {
                // Ovdje dobijamo ID trenutno prijavljenog korisnika
                var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                // Generišemo jedinstveni identifikator korisnika iz GUID-a
                long korisnikId = currentUserId.GetHashCode();

                // Postavljamo ID vlasnika na generisani ID korisnika
                vozilo.IdVlasnika = korisnikId;

                // Dodajemo vozilo u bazu
                _context.Add(vozilo);
                await _context.SaveChangesAsync();

                // Kreiramo novi resurs koristeći informacije iz vozila
                Resurs resurs = new Resurs
                {
                    IdVlasnika = korisnikId,
                    Naziv = vozilo.Naziv,
                    Opis = vozilo.Opis,
                    Ocjena = vozilo.Ocjena
                    // Dodajte dodatna svojstva resursa ovdje ako je potrebno
                };

                // Dodajemo novi resurs u bazu
                _context.Add(resurs);
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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Tip,Marka,Model,Godiste,BrojSjedista,TipGoriva,ImagePath,IdResursa,IdVlasnika,Naziv,Opis,Ocjena")] Vozilo vozilo)
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
