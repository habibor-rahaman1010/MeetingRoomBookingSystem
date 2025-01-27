namespace Domain.Entities
{
    public class MeetingRoom : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public string Facilities { get; set; } = string.Empty;

        public int Capacity { get; set; }

        public string Color { get; set; } = string.Empty;

        public string QRCodeData { get; set; } = string.Empty;

        public bool Status { get; set; } = false;

        public DayOfWeek AvailableDay { get; set; }

        public TimeSpan AvailableTime { get; set; }

        public string ImageUrl { get; set; } = string.Empty;
    }
}
