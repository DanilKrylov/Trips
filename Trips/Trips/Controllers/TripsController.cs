using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Trips;
using Trips.Models;
using Trips.ViewModels;

namespace Trips.Controllers
{
    public class TripsController : Controller
    {
        private readonly DatabaseStore _context;

        public TripsController(DatabaseStore context)
        {
            _context = context;
        }

        // GET: Trips
        public async Task<IActionResult> Index()
        {
            var databaseStore = await _context.Trip.Include(t => t.TripPlace).Include(t => t.TripType).Include(t => t.WayOfTrip).ToListAsync();
            var viewModel = new TripsViewModel()
            {
                Trips = databaseStore
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(TripsViewModel viewModel)
        {
            var databaseStore = await _context.Trip.Include(t => t.TripPlace).Include(t => t.TripType).Include(t => t.WayOfTrip).ToListAsync();

            if (!string.IsNullOrEmpty(viewModel.NameSearch))
            {
                databaseStore = databaseStore.Where(c => c.Name.Contains(viewModel.NameSearch)).ToList();
            }

            if(viewModel.MaximumCost > 0)
            {
                databaseStore = databaseStore.Where(c => c.Cost >= viewModel.MinimumCost && c.Cost <= viewModel.MaximumCost).ToList();
            }

            databaseStore = databaseStore.OrderBy(c => c.Cost).ToList();

            if(viewModel.CostSort == OrderParams.Desc)
            {
                databaseStore.Reverse();
            }

            viewModel.Trips = databaseStore;

            return View(viewModel);
        }

        // GET: Trips/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trip = await _context.Trip
                .Include(t => t.TripPlace)
                .Include(t => t.TripType)
                .Include(t => t.WayOfTrip)
                .Include(t => t.TripClients)
                .ThenInclude(t => t.Client)
                .FirstOrDefaultAsync(m => m.TripId == id);
            if (trip == null)
            {
                return NotFound();
            }

            return View(trip);
        }

        // GET: Trips/Create
        public IActionResult Create()
        {
            ViewData["TripPlaceId"] = new SelectList(_context.Set<TripPlace>(), "TripPlaceId", "Locality");
            ViewData["TripTypeId"] = new SelectList(_context.Set<TripType>(), "TripTypeId", "Name");
            ViewData["WayOfTripId"] = new SelectList(_context.Set<WayOfTrip>(), "WayOfTripId", "TransportName");
            return View();
        }

        // POST: Trips/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TripId,Name,Cost,Description,DaysCount,LivingType,FoodAvailable,TripTypeId,WayOfTripId,TripPlaceId")] Trip trip)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trip);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TripPlaceId"] = new SelectList(_context.Set<TripPlace>(), "TripPlaceId", "Locality", trip.TripPlaceId);
            ViewData["TripTypeId"] = new SelectList(_context.Set<TripType>(), "TripTypeId", "Name", trip.TripTypeId);
            ViewData["WayOfTripId"] = new SelectList(_context.Set<WayOfTrip>(), "WayOfTripId", "TransportName", trip.WayOfTripId);
            return View(trip);
        }

        // GET: Trips/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trip = await _context.Trip.FindAsync(id);
            if (trip == null)
            {
                return NotFound();
            }
            ViewData["TripPlaceId"] = new SelectList(_context.Set<TripPlace>(), "TripPlaceId", "Locality", trip.TripPlaceId);
            ViewData["TripTypeId"] = new SelectList(_context.Set<TripType>(), "TripTypeId", "Name", trip.TripTypeId);
            ViewData["WayOfTripId"] = new SelectList(_context.Set<WayOfTrip>(), "WayOfTripId", "TransportName", trip.WayOfTripId);
            return View(trip);
        }

        // POST: Trips/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TripId,Name,Cost,Description,DaysCount,LivingType,FoodAvailable,TripTypeId,WayOfTripId,TripPlaceId")] Trip trip)
        {
            if (id != trip.TripId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trip);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TripExists(trip.TripId))
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
            ViewData["TripPlaceId"] = new SelectList(_context.Set<TripPlace>(), "TripPlaceId", "Locality", trip.TripPlaceId);
            ViewData["TripTypeId"] = new SelectList(_context.Set<TripType>(), "TripTypeId", "Name", trip.TripTypeId);
            ViewData["WayOfTripId"] = new SelectList(_context.Set<WayOfTrip>(), "WayOfTripId", "TransportName", trip.WayOfTripId);
            return View(trip);
        }

        // GET: Trips/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trip = await _context.Trip
                .Include(t => t.TripPlace)
                .Include(t => t.TripType)
                .Include(t => t.WayOfTrip)
                .FirstOrDefaultAsync(m => m.TripId == id);
            if (trip == null)
            {
                return NotFound();
            }

            return View(trip);
        }

        // POST: Trips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trip = await _context.Trip.FindAsync(id);
            _context.Trip.Remove(trip);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TripExists(int id)
        {
            return _context.Trip.Any(e => e.TripId == id);
        }
    }
}
