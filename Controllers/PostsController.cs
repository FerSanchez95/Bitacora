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
    public class PostsController : Controller
    {
        private readonly BitacoraDb _context;

        public PostsController(BitacoraDb context)
        {
            _context = context;
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            var bitacoraDb = _context.Posts.Include(m => m.BitacoraAsociada);
            return View(await bitacoraDb.ToListAsync());
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modeloPost = await _context.Posts
                .Include(m => m.BitacoraAsociada)
                .FirstOrDefaultAsync(m => m.PostID == id);
            if (modeloPost == null)
            {
                return NotFound();
            }

            return View(modeloPost);
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            ViewData["BitacoraId"] = new SelectList(_context.Bitacoras, "BitacoraId", "NombreDeBitacora");
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PostID,Notas,FechaDeCreación,HoraDeCreación,BitacoraId")] ModeloPost modeloPost)
        {
            if (ModelState.IsValid)
            {
                _context.Add(modeloPost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BitacoraId"] = new SelectList(_context.Bitacoras, "BitacoraId", "NombreDeBitacora", modeloPost.BitacoraId);
            return View(modeloPost);
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modeloPost = await _context.Posts.FindAsync(id);
            if (modeloPost == null)
            {
                return NotFound();
            }
            ViewData["BitacoraId"] = new SelectList(_context.Bitacoras, "BitacoraId", "NombreDeBitacora", modeloPost.BitacoraId);
            return View(modeloPost);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PostID,Notas,FechaDeCreación,HoraDeCreación,BitacoraId")] ModeloPost modeloPost)
        {
            if (id != modeloPost.PostID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(modeloPost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModeloPostExists(modeloPost.PostID))
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
            ViewData["BitacoraId"] = new SelectList(_context.Bitacoras, "BitacoraId", "NombreDeBitacora", modeloPost.BitacoraId);
            return View(modeloPost);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modeloPost = await _context.Posts
                .Include(m => m.BitacoraAsociada)
                .FirstOrDefaultAsync(m => m.PostID == id);
            if (modeloPost == null)
            {
                return NotFound();
            }

            return View(modeloPost);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var modeloPost = await _context.Posts.FindAsync(id);
            if (modeloPost != null)
            {
                _context.Posts.Remove(modeloPost);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModeloPostExists(int id)
        {
            return _context.Posts.Any(e => e.PostID == id);
        }
    }
}
