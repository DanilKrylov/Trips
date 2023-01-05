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
    public class TripClientsController : Controller
    {
        private readonly DatabaseStore _context;

        public TripClientsController(DatabaseStore context)
        {
            _context = context;
        }

        // GET: TripClients
        public async Task<IActionResult> Index()
        {
            var databaseStore = await _context.TripClient.Include(t => t.Client).Include(t => t.Trip).ToListAsync();
            var viewModel = new ClientTripsViewModel()
            {
                TripClients = databaseStore
            };
            
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(ClientTripsViewModel viewModel)
        {
            var databaseStore = await _context.TripClient.Include(t => t.Client).Include(t => t.Trip).ToListAsync();

            if(viewModel.MinDateTime != default(DateTime) || viewModel.MaxDateTime != default(DateTime))
            {
                databaseStore = databaseStore.Where(c => c.StartDate <= viewModel.MaxDateTime && c.StartDate >= viewModel.MinDateTime).ToList();
            }

            viewModel.TripClients = databaseStore;

            return View(viewModel);
        }

        // GET: TripClients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tripClient = await _context.TripClient
                .Include(t => t.Client)
                .Include(t => t.Trip)
                .FirstOrDefaultAsync(m => m.TripClientId == id);
            if (tripClient == null)
            {
                return NotFound();
            }

            return View(tripClient);
        }

        // GET: TripClients/Create
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "PassportCode");
            ViewData["TripId"] = new SelectList(_context.Trip, "TripId", "Name");
            return View();
        }

        // POST: TripClients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TripClientId,ClientId,TripId,StartDate")] TripClient tripClient)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tripClient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "PassportCode", tripClient.ClientId);
            ViewData["TripId"] = new SelectList(_context.Trip, "TripId", "Name", tripClient.TripId);
            return View(tripClient);
        }

        // GET: TripClients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tripClient = await _context.TripClient.FindAsync(id);
            if (tripClient == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "PassportCode", tripClient.ClientId);
            ViewData["TripId"] = new SelectList(_context.Trip, "TripId", "Name", tripClient.TripId);
            return View(tripClient);
        }

        // POST: TripClients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TripClientId,ClientId,TripId,StartDate")] TripClient tripClient)
        {
            if (id != tripClient.TripClientId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tripClient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TripClientExists(tripClient.TripClientId))
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
            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "PassportCode", tripClient.ClientId);
            ViewData["TripId"] = new SelectList(_context.Trip, "TripId", "Name", tripClient.TripId);
            return View(tripClient);
        }

        // GET: TripClients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tripClient = await _context.TripClient
                .Include(t => t.Client)
                .Include(t => t.Trip)
                .FirstOrDefaultAsync(m => m.TripClientId == id);
            if (tripClient == null)
            {
                return NotFound();
            }

            return View(tripClient);
        }

        // POST: TripClients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tripClient = await _context.TripClient.FindAsync(id);
            _context.TripClient.Remove(tripClient);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TripClientExists(int id)
        {
            return _context.TripClient.Any(e => e.TripClientId == id);
        }
    }
}
