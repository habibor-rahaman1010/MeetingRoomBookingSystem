using Domain;
using Domain.Entities;

namespace Service.ServicesContract
{
    public interface IMeetingRoomManagementService
    {
        Task<MeetingRoom> GetMeetingRoomByIdAsync(Guid id);
        Task<(IList<MeetingRoom> data, int total, int totalDisplay)> GetMeetingRoomsAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order);
        Task AddMeetingRoomAsync(MeetingRoom meetingRoom);
        Task DeleteMeetingRoomAsync(Guid id);
        Task UpdateMeetingRoomAsync(MeetingRoom meetingRoom);
    }
}
