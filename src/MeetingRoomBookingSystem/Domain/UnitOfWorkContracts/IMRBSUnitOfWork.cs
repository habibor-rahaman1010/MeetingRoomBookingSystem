using Domain.RepositoryContracts;

namespace Domain.UnitOfWorkContracts
{
    public interface IMRBSUnitOfWork : IUnitOfWork
    {
        IDepartmentRepository DepartmentRepository { get; }
        IMeetingRoomRepository MeetingRoomRepository { get; }
    }
}
