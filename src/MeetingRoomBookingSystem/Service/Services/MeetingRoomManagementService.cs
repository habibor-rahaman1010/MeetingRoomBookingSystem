using Domain;
using Domain.Entities;
using Domain.UnitOfWorkContracts;
using Service.ServicesContract;

namespace Service.Services
{
    public class MeetingRoomManagementService : IMeetingRoomManagementService
    {
        private readonly IMRBSUnitOfWork _meetingRoomUnitOfWork;
        public MeetingRoomManagementService(IMRBSUnitOfWork meetingRoomUnitOfWork)
        {
            _meetingRoomUnitOfWork = meetingRoomUnitOfWork;
        }

        public async Task AddMeetingRoomAsync(MeetingRoom meetingRoom)
        {
            await _meetingRoomUnitOfWork.MeetingRoomRepository.AddAsync(meetingRoom);
            await _meetingRoomUnitOfWork.SaveAsync();
        }

        public async Task<(IList<MeetingRoom> data, int total, int totalDisplay)> GetMeetingRoomsAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            return await _meetingRoomUnitOfWork.MeetingRoomRepository.GetPagedMeetingRoomAsync(pageIndex, pageSize, search, order);
        }

        public async Task DeleteMeetingRoomAsync(Guid id)
        {
            await _meetingRoomUnitOfWork.MeetingRoomRepository.RemoveAsync(id);
            await _meetingRoomUnitOfWork.SaveAsync();   
        }

        public async Task UpdateMeetingRoomAsync(MeetingRoom meetingRoom)
        {
            await _meetingRoomUnitOfWork.MeetingRoomRepository.EditAsync(meetingRoom);
            await _meetingRoomUnitOfWork.SaveAsync();
        }

        public async Task<MeetingRoom> GetMeetingRoomByIdAsync(Guid id)
        {
            return await _meetingRoomUnitOfWork.MeetingRoomRepository.GetByIdAsync(id);
        }
    }
}
