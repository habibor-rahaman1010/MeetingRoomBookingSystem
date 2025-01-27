using System.ComponentModel.DataAnnotations;

namespace Presentation.Models
{
    public class MeetingRoomUpdateModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Meeting Room Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Facilities are required.")]
        public string Facilities { get; set; } = string.Empty;

        [Required(ErrorMessage = "Capacity is required.")]
        [Range(1, 1000, ErrorMessage = "Capacity must be between 1 and 1000.")]
        public int Capacity { get; set; }

        [Required(ErrorMessage = "Color is required.")]
        public string Color { get; set; } = string.Empty;

        [Required(ErrorMessage = "Available Day is required.")]
        public DayOfWeek AvailableDay { get; set; }

        [Required(ErrorMessage = "Available Time is required.")]
        public TimeSpan AvailableTime { get; set; }

        [Required(ErrorMessage = "Image file is required.")]
        [DataType(DataType.Upload)]
        public IFormFile? ImageFile { get; set; }

        public string ImageUrl { get; set; } = string.Empty;
    }
}
