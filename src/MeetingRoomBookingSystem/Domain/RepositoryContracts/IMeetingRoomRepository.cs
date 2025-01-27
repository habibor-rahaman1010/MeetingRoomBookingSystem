using Domain.Entities;

namespace Domain.RepositoryContracts
{
    public interface IMeetingRoomRepository : IRepositoryBase<MeetingRoom, Guid>
    {
        Task<(IList<MeetingRoom> data, int total, int totalDisplay)> GetPagedMeetingRoomAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order);
    }
}
