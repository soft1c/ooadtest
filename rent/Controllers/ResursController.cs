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
    public class ResursController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ResursController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Resurs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Resurs.ToListAsync());
        }

        // GET: Resurs/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resurs = await _context.Resurs
                .FirstOrDefaultAsync(m => m.IdResursa == id);
            if (resurs == null)
            {
                return NotFound();
            }

            return View(resurs);
        }

        // GET: Resurs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Resurs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdResursa,IdVlasnika,Naziv,Opis,Ocjena")] Resurs resurs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(resurs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(resurs);
        }

        // GET: Resurs/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resurs = await _context.Resurs.FindAsync(id);
            if (resurs == null)
            {
                return NotFound();
            }
            return View(resurs);
        }

        // POST: Resurs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("IdResursa,IdVlasnika,Naziv,Opis,Ocjena")] Resurs resurs)
        {
            if (id != resurs.IdResursa)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(resurs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResursExists(resurs.IdResursa))
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
            return View(resurs);
        }

        // GET: Resurs/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resurs = await _context.Resurs
                .FirstOrDefaultAsync(m => m.IdResursa == id);
            if (resurs == null)
            {
                return NotFound();
            }

            return View(resurs);
        }

        // POST: Resurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var resurs = await _context.Resurs.FindAsync(id);
            if (resurs != null)
            {
                _context.Resurs.Remove(resurs);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResursExists(long id)
        {
            return _context.Resurs.Any(e => e.IdResursa == id);
        }
    }
}
