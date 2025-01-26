namespace Domain.Entities
{
    internal class MeetingRoomBooking : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public string MeetingTitle { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public string RepeatBooking { get; set; }

        public string Department { get; set; }

        public string Remarks { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string Pin { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public Guid MeetingRoomId { get; set; }


    }
}
