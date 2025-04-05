namespace EventEaseWebApp.Models
{
    public class Booking
    {
        public int BookingId { get; set; }

        public int EventId { get; set; }
        public Event? Event { get; set; }  // Nullable to avoid build-time warning

        public int VenueId { get; set; }
        public Venue? Venue { get; set; }  // Nullable to avoid build-time warning

        public DateTime BookingDate { get; set; }
    }
}

