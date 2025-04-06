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
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                ViewBag.ErrorDeCarga = "No se puede cargar la bitácora solicitada.";
				return View();
			}

            var bitacora = await _context.Bitacoras.FirstOrDefaultAsync(b => b.BitacoraId == id);
            ViewBag.NombreBitacora = bitacora.NombreDeBitacora;

            ViewBag.BitacoraId = id;
			var postsPorBitacora = await _context.Posts.Where(p => p.BitacoraId == id).OrderBy(p => p.FechaDeCreacion).ToListAsync();
            return View(postsPorBitacora);
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
        //public IActionResult Create()
        //{
        //    //ViewData["BitacoraId"] = new SelectList(_context.Bitacoras, "BitacoraId", "NombreDeBitacora");
        //    return View();
        //}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(String? nota, int id)
        {
			if (string.IsNullOrWhiteSpace(nota)) 
            {
                return BadRequest("La nota no puede estar vacía.");
			}

			ModeloPost nuevaEntrada = new ModeloPost
			{
				Notas = nota,
				FechaDeCreacion = DateOnly.FromDateTime(DateTime.Now),
				HoraDeCreacion = TimeOnly.FromDateTime(DateTime.Now),
				BitacoraId = id
			};

			_context.Add(nuevaEntrada);
			await _context.SaveChangesAsync();

			return RedirectToAction(nameof(Index), new { id });
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Notas,FechaDeCreacion,HoraDeCreacion,BitacoraId")] ModeloPost modeloPost)
        //{
        //    // Registrar la propiedad "BitacoraAsociada" como entrada invalida. OK
        //    // agregar el número ID de la bitacora antes de guardar el modelo en la DB.

        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(modeloPost);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["BitacoraId"] = new SelectList(_context.Bitacoras, "BitacoraId", "NombreDeBitacora", modeloPost.BitacoraId);
        //    return View(modeloPost);
        //}

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
