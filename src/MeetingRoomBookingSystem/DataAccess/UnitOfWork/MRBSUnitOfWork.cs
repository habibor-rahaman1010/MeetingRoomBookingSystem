using DataAccess.Data;
using Domain.RepositoryContracts;
using Domain.UnitOfWorkContracts;

namespace DataAccess.UnitOfWork
{
    public class MRBSUnitOfWork : UnitOfWork, IMRBSUnitOfWork
    {
        public IDepartmentRepository DepartmentRepository { get; private set; }

        public IMeetingRoomRepository MeetingRoomRepository { get; private set; }

        public MRBSUnitOfWork(MRBSDbContext mrbsDbContext,
            IDepartmentRepository departmentRepository,
            IMeetingRoomRepository meetingRoomRepository)
            : base(mrbsDbContext)
        {
            DepartmentRepository = departmentRepository;
            MeetingRoomRepository = meetingRoomRepository;
        }

    }
}
