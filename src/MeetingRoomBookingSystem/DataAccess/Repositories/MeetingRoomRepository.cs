using DataAccess.Data;
using Domain.Entities;
using Domain.RepositoryContracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class MeetingRoomRepository : Repository<MeetingRoom, Guid>, IMeetingRoomRepository
    {
        public MeetingRoomRepository(MRBSDbContext mrbsDbcontext) : base(mrbsDbcontext)
        {
        }
    }
}
