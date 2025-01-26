namespace Presentation.Models
{
    public class MeetingRoomCreateModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public string Facilities { get; set; } = string.Empty;

        public int Capacity { get; set; }

        public string Color { get; set; } = string.Empty;

        public DayOfWeek AvailableDay { get; set; }

        public TimeSpan AvailableTime { get; set; }

        public IFormFile? ImageFile { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
    }
}
