using Domain.Entities;

namespace Presentation.Models
{
    public class MeetingRoomDashboardModel
    {
        public List<MeetingRoom> MeetingRooms { get; set; }

        public MeetingRoomDashboardModel()
        {
            MeetingRooms = new List<MeetingRoom>
            {
                new MeetingRoom {  Name = "Meeting Room 1", Color = "primary" },
                new MeetingRoom {  Name = "Meeting Room 2", Color = "secondary" },
                new MeetingRoom {  Name = "Meeting Room 3", Color = "success" },
                new MeetingRoom {  Name = "Meeting Room 4", Color = "danger" },
                new MeetingRoom {  Name = "Meeting Room 5", Color = "warning" },
                new MeetingRoom {  Name = "Meeting Room 6", Color = "info" },
                new MeetingRoom {  Name = "Meeting Room 7", Color = "dark" }
            };
        }


    }
}

