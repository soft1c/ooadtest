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
        public async Task<IActionResult> Create([Bind("IdResursa,IdVlasnika,Naziv,Opis,Ocjena,Cijena")] Resurs resurs)
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
        public async Task<IActionResult> Edit(long id, [Bind("IdResursa,IdVlasnika,Naziv,Opis,Ocjena,Cijena")] Resurs resurs)
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

        // GET: Resurs/Filter
        public IActionResult Filter()
        {
            var model = new Filtriranje();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Filter(Filtriranje model)
        {
            IQueryable<Resurs> query = _context.Resurs;

            if (!string.IsNullOrEmpty(model.TipResursa))
            {
                if (model.TipResursa == "Vozilo")
                {
                    query = query.OfType<Vozilo>();

                    if (model.OdabraniTipoviGoriva != null && model.OdabraniTipoviGoriva.Any())
                    {
                        var tipoviGoriva = model.OdabraniTipoviGoriva.Select(t => Enum.Parse<TipGoriva>(t)).ToList();
                        query = query.Where(r => tipoviGoriva.Contains(((Vozilo)r).TipGoriva));
                    }
                    if (model.BrojSjedistaOd.HasValue)
                    {
                        query = query.Where(r => ((Vozilo)r).BrojSjedista >= model.BrojSjedistaOd);
                    }
                    if (model.BrojSjedistaDo.HasValue)
                    {
                        query = query.Where(r => ((Vozilo)r).BrojSjedista <= model.BrojSjedistaDo);
                    }
                    if (model.GodisteOd.HasValue)
                    {
                        query = query.Where(r => ((Vozilo)r).Godiste >= model.GodisteOd);
                    }
                    if (model.GodisteDo.HasValue)
                    {
                        query = query.Where(r => ((Vozilo)r).Godiste <= model.GodisteDo);
                    }
                    if (model.OdabraniTipoviVozila != null && model.OdabraniTipoviVozila.Any())
                    {
                        var tipoviVozila = model.OdabraniTipoviVozila.Select(t => Enum.Parse<TipVozila>(t)).ToList();
                        query = query.Where(r => tipoviVozila.Contains(((Vozilo)r).Tip));
                    }
                }
                else if (model.TipResursa == "Nekretnina")
                {
                    query = query.OfType<Nekretnina>();

                    if (model.PovrsinaOd.HasValue)
                    {
                        query = query.Where(r => ((Nekretnina)r).Povrsina >= model.PovrsinaOd);
                    }
                    if (model.PovrsinaDo.HasValue)
                    {
                        query = query.Where(r => ((Nekretnina)r).Povrsina <= model.PovrsinaDo);
                    }
                    if (model.BrojSobaOd.HasValue)
                    {
                        query = query.Where(r => ((Nekretnina)r).BrojSoba >= model.BrojSobaOd);
                    }
                    if (model.BrojSobaDo.HasValue)
                    {
                        query = query.Where(r => ((Nekretnina)r).BrojSoba <= model.BrojSobaDo);
                    }
                    if (model.OdabraniTipoviNekretnina != null && model.OdabraniTipoviNekretnina.Any())
                    {
                        var tipoviNekretnina = model.OdabraniTipoviNekretnina.Select(t => Enum.Parse<TipNekretnine>(t)).ToList();
                        query = query.Where(r => tipoviNekretnina.Contains(((Nekretnina)r).Tip));
                    }
                }
            }

            
            if (!string.IsNullOrEmpty(model.SortirajPo))
            {
                switch (model.SortirajPo)
                {
                    case "Najjeftinije":
                        query = query.OrderBy(r => r.Cijena);
                        break;
                    case "Najskuplje":
                        query = query.OrderByDescending(r => r.Cijena);
                        break;
                    case "Najnovije":
                        query = query.OrderByDescending(r => r.IdResursa);  // Pretpostavljajući da IdResursa raste sa novim unosima
                        break;
                    case "Najstarije":
                        query = query.OrderBy(r => r.IdResursa);
                        break;
                }
            }

            model.Rezultati = await query.ToListAsync();
            return View(model);
        }

            
    }
}
