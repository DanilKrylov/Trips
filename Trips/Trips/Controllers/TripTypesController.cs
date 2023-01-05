using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Trips;
using Trips.Models;

namespace Trips.Controllers
{
    public class TripTypesController : Controller
    {
        private readonly DatabaseStore _context;

        public TripTypesController(DatabaseStore context)
        {
            _context = context;
        }

        // GET: TripTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.TripType.ToListAsync());
        }

        // GET: TripTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tripType = await _context.TripType
                .FirstOrDefaultAsync(m => m.TripTypeId == id);
            if (tripType == null)
            {
                return NotFound();
            }

            return View(tripType);
        }

        // GET: TripTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TripTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TripTypeId,Name,Description,DangerLevel,Preparation")] TripType tripType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tripType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tripType);
        }

        // GET: TripTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tripType = await _context.TripType.FindAsync(id);
            if (tripType == null)
            {
                return NotFound();
            }
            return View(tripType);
        }

        // POST: TripTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TripTypeId,Name,Description,DangerLevel,Preparation")] TripType tripType)
        {
            if (id != tripType.TripTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tripType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TripTypeExists(tripType.TripTypeId))
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
            return View(tripType);
        }

        // GET: TripTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tripType = await _context.TripType
                .FirstOrDefaultAsync(m => m.TripTypeId == id);
            if (tripType == null)
            {
                return NotFound();
            }

            return View(tripType);
        }

        // POST: TripTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tripType = await _context.TripType.FindAsync(id);
            _context.TripType.Remove(tripType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TripTypeExists(int id)
        {
            return _context.TripType.Any(e => e.TripTypeId == id);
        }
    }
}
