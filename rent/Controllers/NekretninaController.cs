using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using rent.Data;
using rent.Models;

namespace rent.Controllers
{
    [Authorize]
    public class NekretninaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NekretninaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Nekretnina
        public async Task<IActionResult> Index()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(currentUserId))
            {
                return Unauthorized();
            }

           

            long userIdHash = HashHelper.GetLongHash(currentUserId);


            var nekretnine = await _context.Nekretnina
                .Where(v => v.IdVlasnika == userIdHash)
                .ToListAsync();

            return View(nekretnine);
            
        }

        // GET: Nekretnina/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nekretnina = await _context.Nekretnina
                .FirstOrDefaultAsync(m => m.IdResursa == id);
            if (nekretnina == null)
            {
                return NotFound();
            }

            return View(nekretnina);
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





        // GET: Nekretnina/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Nekretnina/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Tip,Povrsina,BrojSoba,Parking,ImagePath, IdResursa,IdVlasnika,Naziv,Opis,Ocjena,Cijena")] Nekretnina nekretnina)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nekretnina);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nekretnina);
        }

        // GET: Nekretnina/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nekretnina = await _context.Nekretnina.FindAsync(id);
            if (nekretnina == null)
            {
                return NotFound();
            }
            return View(nekretnina);
        }

        // POST: Nekretnina/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Tip,Povrsina,BrojSoba,Parking,ImagePath,IdResursa,IdVlasnika,Naziv,Opis,Ocjena,Cijena")] Nekretnina nekretnina)
        {
            if (id != nekretnina.IdResursa)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nekretnina);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NekretninaExists(nekretnina.IdResursa))
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
            return View(nekretnina);
        }

        // GET: Nekretnina/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nekretnina = await _context.Nekretnina
                .FirstOrDefaultAsync(m => m.IdResursa == id);
            if (nekretnina == null)
            {
                return NotFound();
            }

            return View(nekretnina);
        }

        // POST: Nekretnina/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var nekretnina = await _context.Nekretnina.FindAsync(id);
            if (nekretnina != null)
            {
                _context.Nekretnina.Remove(nekretnina);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NekretninaExists(long id)
        {
            return _context.Nekretnina.Any(e => e.IdResursa == id);
        }
    }
}
