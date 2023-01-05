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
    public class TripPlacesController : Controller
    {
        private readonly DatabaseStore _context;

        public TripPlacesController(DatabaseStore context)
        {
            _context = context;
        }

        // GET: TripPlaces
        public async Task<IActionResult> Index()
        {
            var viewModel = new PlaceViewModel()
            {
                TripPlaces = await _context.TripPlace.ToListAsync()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(PlaceViewModel viewModel)
        {
            var tripPlaces = await _context.TripPlace.ToListAsync();

            if (!string.IsNullOrEmpty(viewModel.CountrySearch))
            {
                tripPlaces = tripPlaces.Where(c => c.Country.Contains(viewModel.CountrySearch)).ToList();
            }

            if (!string.IsNullOrEmpty(viewModel.LocalitySearch))
            {
                tripPlaces = tripPlaces.Where(c => c.Locality.Contains(viewModel.LocalitySearch)).ToList();
            }

            tripPlaces = tripPlaces.OrderBy(c => c.Country).ToList();

            if(viewModel.CountryOrder == OrderParams.Desc)
            {
                tripPlaces.Reverse();
            }

            viewModel.TripPlaces = tripPlaces;

            return View(viewModel);
        }

        // GET: TripPlaces/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tripPlace = await _context.TripPlace
                .FirstOrDefaultAsync(m => m.TripPlaceId == id);
            if (tripPlace == null)
            {
                return NotFound();
            }

            return View(tripPlace);
        }

        // GET: TripPlaces/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TripPlaceId,Country,Locality,PlaceType,PlaceName,Description")] TripPlace tripPlace)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tripPlace);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tripPlace);
        }

        // GET: TripPlaces/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tripPlace = await _context.TripPlace.FindAsync(id);
            if (tripPlace == null)
            {
                return NotFound();
            }
            return View(tripPlace);
        }

        // POST: TripPlaces/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TripPlaceId,Country,Locality,PlaceType,PlaceName,Description")] TripPlace tripPlace)
        {
            if (id != tripPlace.TripPlaceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tripPlace);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TripPlaceExists(tripPlace.TripPlaceId))
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
            return View(tripPlace);
        }

        // GET: TripPlaces/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tripPlace = await _context.TripPlace
                .FirstOrDefaultAsync(m => m.TripPlaceId == id);
            if (tripPlace == null)
            {
                return NotFound();
            }

            return View(tripPlace);
        }

        // POST: TripPlaces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tripPlace = await _context.TripPlace.FindAsync(id);
            _context.TripPlace.Remove(tripPlace);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TripPlaceExists(int id)
        {
            return _context.TripPlace.Any(e => e.TripPlaceId == id);
        }
    }
}
