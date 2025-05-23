﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bitacora.Data;
using Bitacora.Models;
using Bitacora.Helpers;



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
                ViewBag.ErrorDeCarga = Mensajes.BitacoraNoSeleccionada;
				return View();
			}

            var bitacora = await _context.Bitacoras.FirstOrDefaultAsync(b => b.BitacoraId == id);
            if (bitacora == null)
            {
                ViewBag.ErrorDeCarga = Mensajes.BitacoraNoCargada;
            }

            ViewBag.NombreBitacora = bitacora.NombreDeBitacora;

            ViewBag.BitacoraId = id;
			var postsPorBitacora = await _context.Posts.Where(p => p.BitacoraId == id)
                .OrderByDescending(p => p.TimeStamp)
                .ToListAsync();
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
                TimeStamp = DateTime.Now,
                UltimaModificacion = DateTime.Now,
				BitacoraId = id
			};

			_context.Add(nuevaEntrada);
			await _context.SaveChangesAsync();

			return RedirectToAction(nameof(Index), new { id });
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PostID,Notas,BitacoraId")] ModeloPost modeloPost)
        {
            if (id != modeloPost.PostID)
            {
                return NotFound();
            }

            modeloPost.UltimaModificacion = DateTime.Now;

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
