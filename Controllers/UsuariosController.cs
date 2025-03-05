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
    public class UsuariosController : Controller
    {
        private readonly BitacoraDb _context;

        public UsuariosController(BitacoraDb context)
        {
            _context = context;
        }

        // GET: ModeloUsuarios
        public async Task<IActionResult> Index()
        {
            var bitacoraDb = _context.Usuarios.Include(m => m.PerfilDeAutenticacion);
            return View(await bitacoraDb.ToListAsync());
        }

        // GET: ModeloUsuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modeloUsuario = await _context.Usuarios
                .Include(m => m.PerfilDeAutenticacion)
                .FirstOrDefaultAsync(m => m.UsuarioId == id);
            if (modeloUsuario == null)
            {
                return NotFound();
            }

            return View(modeloUsuario);
        }

        // GET: ModeloUsuarios/Create
        public IActionResult Create()
        {
            ViewData["AutenticacionId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: ModeloUsuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UsuarioId,NombreUsuario,CantidadDeBitacoras,AutenticacionId")] ModeloUsuario modeloUsuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(modeloUsuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AutenticacionId"] = new SelectList(_context.Users, "Id", "Id", modeloUsuario.AutenticacionId);
            return View(modeloUsuario);
        }

        // GET: ModeloUsuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modeloUsuario = await _context.Usuarios.FindAsync(id);
            if (modeloUsuario == null)
            {
                return NotFound();
            }
            ViewData["AutenticacionId"] = new SelectList(_context.Users, "Id", "Id", modeloUsuario.AutenticacionId);
            return View(modeloUsuario);
        }

        // POST: ModeloUsuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UsuarioId,NombreUsuario,CantidadDeBitacoras,AutenticacionId")] ModeloUsuario modeloUsuario)
        {
            if (id != modeloUsuario.UsuarioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(modeloUsuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModeloUsuarioExists(modeloUsuario.UsuarioId))
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
            ViewData["AutenticacionId"] = new SelectList(_context.Users, "Id", "Id", modeloUsuario.AutenticacionId);
            return View(modeloUsuario);
        }

        // GET: ModeloUsuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modeloUsuario = await _context.Usuarios
                .Include(m => m.PerfilDeAutenticacion)
                .FirstOrDefaultAsync(m => m.UsuarioId == id);
            if (modeloUsuario == null)
            {
                return NotFound();
            }

            return View(modeloUsuario);
        }

        // POST: ModeloUsuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var modeloUsuario = await _context.Usuarios.FindAsync(id);
            if (modeloUsuario != null)
            {
                _context.Usuarios.Remove(modeloUsuario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModeloUsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.UsuarioId == id);
        }
    }
}
