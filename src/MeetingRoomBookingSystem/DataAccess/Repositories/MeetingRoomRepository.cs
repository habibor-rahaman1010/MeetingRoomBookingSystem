using DataAccess.Data;
using Domain;
using Domain.Entities;
using Domain.RepositoryContracts;

namespace DataAccess.Repositories
{
    public class MeetingRoomRepository : Repository<MeetingRoom, Guid>, IMeetingRoomRepository
    {
        public MeetingRoomRepository(MRBSDbContext mrbsDbcontext) : base(mrbsDbcontext)
        {
        }

        public async Task<(IList<MeetingRoom> data, int total, int totalDisplay)> GetPagedMeetingRoomAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            if (string.IsNullOrWhiteSpace(search.Value))
            {
                return await GetDynamicAsync(null, order, null, pageIndex, pageSize, true);
            }
            else
            {
                return await GetDynamicAsync(x => x.Name.Contains(search.Value) || x.Description.Contains(search.Value), order, null, pageIndex, pageSize, true);
            }
        }


        public async Task<bool> IsMeetingRoomDuplicateAsync(string meetingRoomeName, Guid? id = null)
        {
            if (id.HasValue)
            {
                return await GetCountAsync(x => x.Id != id.Value && x.Name == meetingRoomeName) > 0;
            }
            else
            {
                return await GetCountAsync(x => x.Name == meetingRoomeName) > 0;
            }
        }
    }
}
