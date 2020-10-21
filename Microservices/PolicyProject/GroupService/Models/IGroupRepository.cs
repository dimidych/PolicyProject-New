using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroupService
{
    public interface IGroupRepository
    {
        Task<IEnumerable<Group>> GetGroup(int? groupId = null);
        Task<Group> AddGroup(Group newGroup);
        Task<bool> UpdateGroup(Group group);
        Task<bool> DeleteGroup(int groupId);
    }
}