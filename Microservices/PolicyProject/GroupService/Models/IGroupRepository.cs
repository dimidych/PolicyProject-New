using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroupService
{
    public interface IGroupRepository
    {
        Task<IEnumerable<Group>> GetGroup(Guid? groupId = null);
        Task<Group> AddGroup(Group newGroup);
        Task<bool> UpdateGroup(Group group);
        Task<bool> DeleteGroup(Guid groupId);
    }
}