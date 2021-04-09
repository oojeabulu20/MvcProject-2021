using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcShoes.Data;
using MvcShoes.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace MvcShoes.Controllers
{
    [Authorize(Roles = "Manager")]
    public class ShoesController : Controller
    {
        private readonly MvcShoesContext _context;
       
        public ShoesController(MvcShoesContext context)
        {
            _context = context;
        }

        // GET: Shoes
        public async Task<IActionResult> Index(string searchString)
        {
            var shoes = from s in _context.Shoes
                         select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                shoes = shoes.Where(s => s.ShoeName.Contains(searchString));
            }

            return View(await shoes.ToListAsync());
        }

        // GET: Shoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoes = await _context.Shoes
                .FirstOrDefaultAsync(m => m.ShoesId == id);
            if (shoes == null)
            {
                return NotFound();
            }

            return View(shoes);
        }

        // GET: Shoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Shoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShoesId,ShoeName,Description,ShoeImage,Price")] Shoes shoes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shoes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shoes);
        }

        // GET: Shoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoes = await _context.Shoes.FindAsync(id);
            if (shoes == null)
            {
                return NotFound();
            }
            return View(shoes);
        }

        // POST: Shoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ShoesId,ShoeName,Description,ShoeImage,Price")] Shoes shoes)
        {
            if (id != shoes.ShoesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shoes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShoesExists(shoes.ShoesId))
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
            return View(shoes);
        }

        // GET: Shoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoes = await _context.Shoes
                .FirstOrDefaultAsync(m => m.ShoesId == id);
            if (shoes == null)
            {
                return NotFound();
            }

            return View(shoes);
        }

        // POST: Shoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shoes = await _context.Shoes.FindAsync(id);
            _context.Shoes.Remove(shoes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShoesExists(int id)
        {
            return _context.Shoes.Any(e => e.ShoesId == id);
        }
    }
}
