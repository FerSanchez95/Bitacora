using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bitacora.Data;
using Bitacora.Models;

namespace Bitacora.Controllers
{
    public class BitacorasController : Controller
    {
        private readonly BitacoraDb _context;

        public BitacorasController(BitacoraDb context)
        {
            _context = context;
        }

        // GET: Bitacoras
        public async Task<IActionResult> Index()
        {
            return View(await _context.Bitacoras.ToListAsync());
        }

        // GET: Bitacoras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modeloBitacora = await _context.Bitacoras
                .FirstOrDefaultAsync(m => m.BitacoraId == id);
            if (modeloBitacora == null)
            {
                return NotFound();
            }

            return View(modeloBitacora);
        }

        // GET: Bitacoras/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bitacoras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BitacoraId,NombreDeBitacora,FechaDeCreacion,HoraDeCreacion")] ModeloBitacora modeloBitacora)
        {
            if (ModelState.IsValid)
            {
                _context.Add(modeloBitacora);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(modeloBitacora);
        }

        // GET: Bitacoras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modeloBitacora = await _context.Bitacoras.FindAsync(id);
            if (modeloBitacora == null)
            {
                return NotFound();
            }
            return View(modeloBitacora);
        }

        // POST: Bitacoras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BitacoraId,NombreDeBitacora,FechaDeCreacion,HoraDeCreacion")] ModeloBitacora modeloBitacora)
        {
            if (id != modeloBitacora.BitacoraId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(modeloBitacora);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModeloBitacoraExists(modeloBitacora.BitacoraId))
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
            return View(modeloBitacora);
        }

        // GET: Bitacoras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modeloBitacora = await _context.Bitacoras
                .FirstOrDefaultAsync(m => m.BitacoraId == id);
            if (modeloBitacora == null)
            {
                return NotFound();
            }

            return View(modeloBitacora);
        }

        // POST: Bitacoras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var modeloBitacora = await _context.Bitacoras.FindAsync(id);
            if (modeloBitacora != null)
            {
                _context.Bitacoras.Remove(modeloBitacora);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModeloBitacoraExists(int id)
        {
            return _context.Bitacoras.Any(e => e.BitacoraId == id);
        }
    }
}
