namespace Presentation.Models
{
    public class MeetingRoomDashboardModel
    {
        public List<MeetingRoom> MeetingRooms { get; set; }

        public MeetingRoomDashboardModel()
        {
            MeetingRooms = new List<MeetingRoom>
            {
                new MeetingRoom { Id = 1, Name = "Meeting Room 1", Color = "primary" },
                new MeetingRoom { Id = 2, Name = "Meeting Room 2", Color = "secondary" },
                new MeetingRoom { Id = 3, Name = "Meeting Room 3", Color = "success" },
                new MeetingRoom { Id = 4, Name = "Meeting Room 4", Color = "danger" },
                new MeetingRoom { Id = 5, Name = "Meeting Room 5", Color = "warning" },
                new MeetingRoom { Id = 6, Name = "Meeting Room 6", Color = "info" },
                new MeetingRoom { Id = 7, Name = "Meeting Room 7", Color = "dark" }
            };
        }


    }

    public class MeetingRoom
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
    }
}

