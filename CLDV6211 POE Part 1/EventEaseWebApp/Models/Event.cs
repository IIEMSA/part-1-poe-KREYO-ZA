namespace EventEaseWebApp.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public string EventName { get; set; } = string.Empty;
        public DateTime EventDate { get; set; }
        public string Description { get; set; } = string.Empty;

        public int VenueId { get; set; }
        public Venue? Venue { get; set; }  // Nullable to avoid scaffold errors

        public ICollection<Booking>? Bookings { get; set; }  // Nullable to avoid null reference warnings
    }
}
