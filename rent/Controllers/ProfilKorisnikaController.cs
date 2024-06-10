using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using rent.Data;
using rent.Models;

namespace rent.Controllers
{
    public class ProfilKorisnikaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProfilKorisnikaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProfilKorisnika
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProfiliKorisnika.ToListAsync());
        }

        // GET: ProfilKorisnika/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profilKorisnika = await _context.ProfiliKorisnika
                .FirstOrDefaultAsync(m => m.IdKorisnika == id);
            if (profilKorisnika == null)
            {
                return NotFound();
            }

            return View(profilKorisnika);
        }

        // GET: ProfilKorisnika/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProfilKorisnika/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdKorisnika,Ime,Prezime,BrojTelefona,Adresa")] ProfilKorisnika profilKorisnika)
        {
            if (ModelState.IsValid)
            {
                _context.Add(profilKorisnika);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(profilKorisnika);
        }

        // GET: ProfilKorisnika/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profilKorisnika = await _context.ProfiliKorisnika.FindAsync(id);
            if (profilKorisnika == null)
            {
                return NotFound();
            }
            return View(profilKorisnika);
        }

        // POST: ProfilKorisnika/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("IdKorisnika,Ime,Prezime,BrojTelefona,Adresa")] ProfilKorisnika profilKorisnika)
        {
            if (id != profilKorisnika.IdKorisnika)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(profilKorisnika);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfilKorisnikaExists(profilKorisnika.IdKorisnika))
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
            return View(profilKorisnika);
        }

        // GET: ProfilKorisnika/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profilKorisnika = await _context.ProfiliKorisnika
                .FirstOrDefaultAsync(m => m.IdKorisnika == id);
            if (profilKorisnika == null)
            {
                return NotFound();
            }

            return View(profilKorisnika);
        }

        // POST: ProfilKorisnika/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var profilKorisnika = await _context.ProfiliKorisnika.FindAsync(id);
            if (profilKorisnika != null)
            {
                _context.ProfiliKorisnika.Remove(profilKorisnika);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfilKorisnikaExists(long id)
        {
            return _context.ProfiliKorisnika.Any(e => e.IdKorisnika == id);
        }
    }
}
