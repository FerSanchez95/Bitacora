using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bitacora.Data;
using Bitacora.Models;
using System.Security.Claims;

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
            string? claimUsuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int usuarioId;
            
            if (claimUsuarioId == null) 
            { 
                return NotFound(); 
            }
            
            if (!int.TryParse(claimUsuarioId, out usuarioId))
            {
                return BadRequest(usuarioId.ToString());
            }

            var bitacorasUsuario = await _context.Bitacoras.Where(b => b.UsuarioId == usuarioId).ToListAsync();
            return View(bitacorasUsuario);
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
        public async Task<IActionResult> Create([Bind("NombreDeBitacora,FechaDeCreacion,HoraDeCreacion")] ModeloBitacora modeloBitacora)
        {
            int usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            modeloBitacora.UsuarioId = usuarioId;

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

        public async Task<IActionResult> PostsInput(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var postsRealizados = await _context.Posts
                .Where(p => p.BitacoraId == id)
                .OrderBy(p => p.FechaDeCreacion)
                .ToListAsync();
            return View(postsRealizados);
        }

        private bool ModeloBitacoraExists(int id)
        {
            return _context.Bitacoras.Any(e => e.BitacoraId == id);
        }
    }
}
