using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using EventEaseWebApp.Models;
using EventEaseWebApp.Data;

namespace EventEaseWebApp.Controllers
{
    public class VenuesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VenuesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Venues
        public async Task<IActionResult> Index()
        {
            var venues = await _context.Venues
                .FromSqlRaw("SELECT * FROM Venues")
                .ToListAsync();

            return View(venues);
        }

        // GET: Venues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var venue = await _context.Venues
                .FromSqlRaw("SELECT * FROM Venues WHERE VenueId = {0}", id)
                .FirstOrDefaultAsync();

            if (venue == null) return NotFound();

            return View(venue);
        }

        // GET: Venues/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Venues/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VenueId,VenueName,Location,Capacity,ImageUrl")] Venue venue)
        {
            if (ModelState.IsValid)
            {
                string sql = @"
                    INSERT INTO Venues (VenueName, Location, Capacity, ImageUrl)
                    VALUES (@VenueName, @Location, @Capacity, @ImageUrl);
                ";

                await _context.Database.ExecuteSqlRawAsync(sql,
                    new SqlParameter("@VenueName", venue.VenueName),
                    new SqlParameter("@Location", venue.Location),
                    new SqlParameter("@Capacity", venue.Capacity),
                    new SqlParameter("@ImageUrl", venue.ImageUrl)
                );

                return RedirectToAction(nameof(Index));
            }

            return View(venue);
        }

        // GET: Venues/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var venue = await _context.Venues
                .FromSqlRaw("SELECT * FROM Venues WHERE VenueId = {0}", id)
                .FirstOrDefaultAsync();

            if (venue == null) return NotFound();

            return View(venue);
        }

        // POST: Venues/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VenueId,VenueName,Location,Capacity,ImageUrl")] Venue venue)
        {
            if (id != venue.VenueId) return NotFound();

            if (ModelState.IsValid)
            {
                string sql = @"
                    UPDATE Venues
                    SET VenueName = @VenueName,
                        Location = @Location,
                        Capacity = @Capacity,
                        ImageUrl = @ImageUrl
                    WHERE VenueId = @VenueId;
                ";

                await _context.Database.ExecuteSqlRawAsync(sql,
                    new SqlParameter("@VenueId", venue.VenueId),
                    new SqlParameter("@VenueName", venue.VenueName),
                    new SqlParameter("@Location", venue.Location),
                    new SqlParameter("@Capacity", venue.Capacity),
                    new SqlParameter("@ImageUrl", venue.ImageUrl)
                );

                return RedirectToAction(nameof(Index));
            }

            return View(venue);
        }

        // GET: Venues/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var venue = await _context.Venues
                .FromSqlRaw("SELECT * FROM Venues WHERE VenueId = {0}", id)
                .FirstOrDefaultAsync();

            if (venue == null) return NotFound();

            return View(venue);
        }

        // POST: Venues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string sql = "DELETE FROM Venues WHERE VenueId = @VenueId";

            await _context.Database.ExecuteSqlRawAsync(sql,
                new SqlParameter("@VenueId", id)
            );

            return RedirectToAction(nameof(Index));
        }

        // Helper method to check if venue exists
        private bool VenueExists(int id)
        {
            return _context.Venues
                .FromSqlRaw("SELECT * FROM Venues WHERE VenueId = {0}", id)
                .Any();
        }
    }
}
