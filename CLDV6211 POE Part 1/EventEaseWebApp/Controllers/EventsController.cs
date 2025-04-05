using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using EventEaseWebApp.Data;
using EventEaseWebApp.Models;

namespace EventEaseWebApp.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Events
        public async Task<IActionResult> Index()
        {
            var events = await _context.Events
                .FromSqlRaw("SELECT * FROM Events")
                .ToListAsync();

            foreach (var e in events)
            {
                e.Venue = await _context.Venues.FindAsync(e.VenueId);
            }

            return View(events);
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var evt = await _context.Events
                .FromSqlRaw("SELECT * FROM Events WHERE EventId = {0}", id)
                .FirstOrDefaultAsync();

            if (evt == null) return NotFound();

            evt.Venue = await _context.Venues.FindAsync(evt.VenueId);
            return View(evt);
        }

        // GET: Events/Create
        public IActionResult Create()
        {
            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "VenueName");
            return View();
        }

        // POST: Events/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventId,EventName,EventDate,Description,VenueId")] Event @event)
        {
            if (ModelState.IsValid)
            {
                string sql = @"
                    INSERT INTO Events (EventName, EventDate, Description, VenueId)
                    VALUES (@EventName, @EventDate, @Description, @VenueId);
                ";

                await _context.Database.ExecuteSqlRawAsync(sql,
                    new SqlParameter("@EventName", @event.EventName),
                    new SqlParameter("@EventDate", @event.EventDate),
                    new SqlParameter("@Description", @event.Description),
                    new SqlParameter("@VenueId", @event.VenueId)
                );

                return RedirectToAction(nameof(Index));
            }

            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "VenueName", @event.VenueId);
            return View(@event);
        }

        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var evt = await _context.Events
                .FromSqlRaw("SELECT * FROM Events WHERE EventId = {0}", id)
                .FirstOrDefaultAsync();

            if (evt == null) return NotFound();

            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "VenueName", evt.VenueId);
            return View(evt);
        }

        // POST: Events/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventId,EventName,EventDate,Description,VenueId")] Event @event)
        {
            if (id != @event.EventId) return NotFound();

            if (ModelState.IsValid)
            {
                string sql = @"
                    UPDATE Events
                    SET EventName = @EventName,
                        EventDate = @EventDate,
                        Description = @Description,
                        VenueId = @VenueId
                    WHERE EventId = @EventId;
                ";

                await _context.Database.ExecuteSqlRawAsync(sql,
                    new SqlParameter("@EventId", @event.EventId),
                    new SqlParameter("@EventName", @event.EventName),
                    new SqlParameter("@EventDate", @event.EventDate),
                    new SqlParameter("@Description", @event.Description),
                    new SqlParameter("@VenueId", @event.VenueId)
                );

                return RedirectToAction(nameof(Index));
            }

            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "VenueName", @event.VenueId);
            return View(@event);
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var evt = await _context.Events
                .FromSqlRaw("SELECT * FROM Events WHERE EventId = {0}", id)
                .FirstOrDefaultAsync();

            if (evt == null) return NotFound();

            evt.Venue = await _context.Venues.FindAsync(evt.VenueId);
            return View(evt);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string sql = "DELETE FROM Events WHERE EventId = @EventId";

            await _context.Database.ExecuteSqlRawAsync(sql,
                new SqlParameter("@EventId", id)
            );

            return RedirectToAction(nameof(Index));
        }

        // Checks if event exists
        private bool EventExists(int id)
        {
            return _context.Events.FromSqlRaw("SELECT * FROM Events WHERE EventId = {0}", id).Any();
        }
    }
}
