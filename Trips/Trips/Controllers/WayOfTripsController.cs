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
    public class WayOfTripsController : Controller
    {
        private readonly DatabaseStore _context;

        public WayOfTripsController(DatabaseStore context)
        {
            _context = context;
        }

        // GET: WayOfTrips
        public async Task<IActionResult> Index()
        {
            return View(await _context.WayOfTrip.ToListAsync());
        }

        // GET: WayOfTrips/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wayOfTrip = await _context.WayOfTrip
                .FirstOrDefaultAsync(m => m.WayOfTripId == id);
            if (wayOfTrip == null)
            {
                return NotFound();
            }

            return View(wayOfTrip);
        }

        // GET: WayOfTrips/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WayOfTrips/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WayOfTripId,TransportType,TransportName,DriveTime")] WayOfTrip wayOfTrip)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wayOfTrip);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(wayOfTrip);
        }

        // GET: WayOfTrips/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wayOfTrip = await _context.WayOfTrip.FindAsync(id);
            if (wayOfTrip == null)
            {
                return NotFound();
            }
            return View(wayOfTrip);
        }

        // POST: WayOfTrips/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WayOfTripId,TransportType,TransportName,DriveTime")] WayOfTrip wayOfTrip)
        {
            if (id != wayOfTrip.WayOfTripId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wayOfTrip);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WayOfTripExists(wayOfTrip.WayOfTripId))
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
            return View(wayOfTrip);
        }

        // GET: WayOfTrips/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wayOfTrip = await _context.WayOfTrip
                .FirstOrDefaultAsync(m => m.WayOfTripId == id);
            if (wayOfTrip == null)
            {
                return NotFound();
            }

            return View(wayOfTrip);
        }

        // POST: WayOfTrips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wayOfTrip = await _context.WayOfTrip.FindAsync(id);
            _context.WayOfTrip.Remove(wayOfTrip);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WayOfTripExists(int id)
        {
            return _context.WayOfTrip.Any(e => e.WayOfTripId == id);
        }
    }
}
