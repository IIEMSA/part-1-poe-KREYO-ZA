using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using EventEaseWebApp.Data;
using EventEaseWebApp.Models;

namespace EventEaseWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Home/Index
        public IActionResult Index()
        {
            // 🔢 Get total counts using SQL
            int venueCount = _context.Venues
                .FromSqlRaw("SELECT * FROM Venues")
                .Count();

            int eventCount = _context.Events
                .FromSqlRaw("SELECT * FROM Events")
                .Count();

            int bookingCount = _context.Bookings
                .FromSqlRaw("SELECT * FROM Bookings")
                .Count();

            ViewData["VenueCount"] = venueCount;
            ViewData["EventCount"] = eventCount;
            ViewData["BookingCount"] = bookingCount;

            // 📌 If redirected from booking creation, show booking summary
            if (TempData["LastBookingId"] is int lastId)
            {
                var booking = _context.Bookings
                    .FromSqlRaw("SELECT * FROM Bookings WHERE BookingId = {0}", lastId)
                    .AsEnumerable()
                    .FirstOrDefault();

                if (booking != null)
                {
                    booking.Event = _context.Events.Find(booking.EventId);
                    booking.Venue = _context.Venues.Find(booking.VenueId);
                    ViewData["LastBooking"] = booking;
                }
            }

            return View();
        }
    }
}
