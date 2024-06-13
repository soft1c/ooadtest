using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rent.Data;
using rent.Models;
using rent.Models.ViewModels;

namespace rent.Controllers
{
    public class RecenzijaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RecenzijaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Recenzija
        public async Task<IActionResult> Index()
        {
            return View(await _context.Recenzija.ToListAsync());
        }

        // GET: Recenzija/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recenzija = await _context.Recenzija
                .FirstOrDefaultAsync(m => m.IdRecenzije == id);
            if (recenzija == null)
            {
                return NotFound();
            }

            return View(recenzija);
        }

        // GET: Recenzija/Create
        public IActionResult Create(long id)
        {
            var viewModel = new CreateRecenzijaViewModel
            {
                IdResursa = id
            };
            return View(viewModel);
        }

        // POST: Recenzija/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdResursa,Ocjena,Komentar")] CreateRecenzijaViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(currentUserId))
                {
                    return Unauthorized();
                }

                var recenzija = new Recenzija
                {
                    IdAutora = HashHelper.GetLongHash(currentUserId),
                    IdResursa = viewModel.IdResursa,
                    Ocjena = viewModel.Ocjena,
                    Komentar = viewModel.Komentar
                };

                _context.Add(recenzija);
                await _context.SaveChangesAsync();

                // Update average rating for the resource
                await UpdateResursOcjena(recenzija.IdResursa);

                return RedirectToAction("MyReservations", "Rezervacija");
            }

            return View(viewModel);
        }

        private async Task UpdateResursOcjena(long resursId)
        {
            var resurs = await _context.Resurs.FindAsync(resursId);
            if (resurs != null)
            {
                var recenzije = await _context.Recenzija
                    .Where(r => r.IdResursa == resursId)
                    .ToListAsync();

                resurs.Ocjena = recenzije.Average(r => r.Ocjena);

                _context.Resurs.Update(resurs);
                await _context.SaveChangesAsync();
            }
        }

        // GET: Recenzija/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recenzija = await _context.Recenzija.FindAsync(id);
            if (recenzija == null)
            {
                return NotFound();
            }
            return View(recenzija);
        }

        // POST: Recenzija/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("IdRecenzije,IdAutora,IdResursa,Ocjena,Komentar")] Recenzija recenzija)
        {
            if (id != recenzija.IdRecenzije)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recenzija);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecenzijaExists(recenzija.IdRecenzije))
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
            return View(recenzija);
        }

        // GET: Recenzija/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recenzija = await _context.Recenzija
                .FirstOrDefaultAsync(m => m.IdRecenzije == id);
            if (recenzija == null)
            {
                return NotFound();
            }

            return View(recenzija);
        }

        // POST: Recenzija/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var recenzija = await _context.Recenzija.FindAsync(id);
            if (recenzija != null)
            {
                _context.Recenzija.Remove(recenzija);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecenzijaExists(long id)
        {
            return _context.Recenzija.Any(e => e.IdRecenzije == id);
        }
    }
}
