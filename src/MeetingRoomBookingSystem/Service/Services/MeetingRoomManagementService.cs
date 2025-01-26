using Domain.Entities;
using Domain.UnitOfWorkContracts;
using Service.ServicesContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Task DeleteMeetingRoomAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<MeetingRoom>> GetMeetingRoomsAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateMeetingRoomAsync(MeetingRoom meetingRoom)
        {
            throw new NotImplementedException();
        }
    }
}
