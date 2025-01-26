using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServicesContract
{
    public interface IMeetingRoomManagementService
    {
        Task<IList<MeetingRoom>> GetMeetingRoomsAsync();
        Task AddMeetingRoomAsync(MeetingRoom meetingRoom);
        Task DeleteMeetingRoomAsync(Guid id);
        Task UpdateMeetingRoomAsync(MeetingRoom meetingRoom);
    }
}
