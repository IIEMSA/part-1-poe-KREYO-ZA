using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using EventEaseWebApp.Data;
using EventEaseWebApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EventEaseWebApp.Controllers
{
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Bookings
        public async Task<IActionResult> Index()
        {
            var bookings = await _context.Bookings
                .FromSqlRaw(@"
                    SELECT b.BookingId, b.BookingDate, b.EventId, b.VenueId
                    FROM Bookings b
                ").ToListAsync();

            foreach (var booking in bookings)
            {
                booking.Event = await _context.Events.FindAsync(booking.EventId);
                booking.Venue = await _context.Venues.FindAsync(booking.VenueId);
            }

            return View(bookings);
        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var booking = await _context.Bookings
                .FromSqlRaw("SELECT * FROM Bookings WHERE BookingId = {0}", id)
                .FirstOrDefaultAsync();

            if (booking == null) return NotFound();

            booking.Event = await _context.Events.FindAsync(booking.EventId);
            booking.Venue = await _context.Venues.FindAsync(booking.VenueId);

            return View(booking);
        }

        // GET: Bookings/Create
        public IActionResult Create()
        {
            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "EventName");
            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "VenueName");
            return View();
        }

        // POST: Bookings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookingId,EventId,VenueId,BookingDate")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                string sql = @"
                    INSERT INTO Bookings (EventId, VenueId, BookingDate)
                    VALUES (@EventId, @VenueId, @BookingDate);
                    SELECT CAST(SCOPE_IDENTITY() as int);
                ";

                var id = await _context.Database.ExecuteSqlRawAsync(sql,
                    new SqlParameter("@EventId", booking.EventId),
                    new SqlParameter("@VenueId", booking.VenueId),
                    new SqlParameter("@BookingDate", booking.BookingDate)
                );

                TempData["LastBookingId"] = id;
                return RedirectToAction("Index", "Home");
            }

            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "EventName", booking.EventId);
            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "VenueName", booking.VenueId);
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var booking = await _context.Bookings
                .FromSqlRaw("SELECT * FROM Bookings WHERE BookingId = {0}", id)
                .FirstOrDefaultAsync();

            if (booking == null) return NotFound();

            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "EventName", booking.EventId);
            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "VenueName", booking.VenueId);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookingId,EventId,VenueId,BookingDate")] Booking booking)
        {
            if (id != booking.BookingId) return NotFound();

            if (ModelState.IsValid)
            {
                string sql = @"
                    UPDATE Bookings
                    SET EventId = @EventId,
                        VenueId = @VenueId,
                        BookingDate = @BookingDate
                    WHERE BookingId = @BookingId
                ";

                await _context.Database.ExecuteSqlRawAsync(sql,
                    new SqlParameter("@EventId", booking.EventId),
                    new SqlParameter("@VenueId", booking.VenueId),
                    new SqlParameter("@BookingDate", booking.BookingDate),
                    new SqlParameter("@BookingId", booking.BookingId)
                );

                return RedirectToAction(nameof(Index));
            }

            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "EventName", booking.EventId);
            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "VenueName", booking.VenueId);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var booking = await _context.Bookings
                .FromSqlRaw("SELECT * FROM Bookings WHERE BookingId = {0}", id)
                .FirstOrDefaultAsync();

            if (booking == null) return NotFound();

            booking.Event = await _context.Events.FindAsync(booking.EventId);
            booking.Venue = await _context.Venues.FindAsync(booking.VenueId);

            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string sql = "DELETE FROM Bookings WHERE BookingId = @BookingId";

            await _context.Database.ExecuteSqlRawAsync(sql,
                new SqlParameter("@BookingId", id)
            );

            return RedirectToAction(nameof(Index));
        }
    }
}
