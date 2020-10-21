using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GroupService
{
    public class GroupRepository : IGroupRepository
    {
        private readonly GroupDbContext _dbContext;

        public GroupRepository(GroupDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Group>> GetGroup(int? groupId = null)
        {
            var result = new List<Group>();

            if (groupId == null)
                result =
                    await _dbContext.Groups.AsNoTracking().ToListAsync();
            else
                result.Add(await _dbContext.Groups.AsNoTracking().FirstOrDefaultAsync(x => x.GroupId == groupId));

            return result;
        }

        public async Task<Group> AddGroup(Group newGroup)
        {
            if (newGroup == null || string.IsNullOrEmpty(newGroup.GroupName))
                throw new ArgumentException("Группа не задана");

            var existedGroup = await _dbContext.Groups.AsNoTracking().FirstOrDefaultAsync(x =>
                x.GroupName.Equals(newGroup.GroupName, StringComparison.InvariantCultureIgnoreCase));

            if (existedGroup != null)
                throw new Exception("Группа уже существует");

            newGroup.GroupId = _dbContext.Groups.AsNoTracking().Any()
                ? await _dbContext.Groups.AsNoTracking().MaxAsync(x => x.GroupId) + 1
                : 1;
            var result = await _dbContext.Groups.AddAsync(newGroup);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> UpdateGroup(Group group)
        {
            if (group == null || string.IsNullOrEmpty(group.GroupName))
                throw new Exception("Группа не задана");

            var existedGroup = await _dbContext.Groups.FirstOrDefaultAsync(x =>
                x.GroupId == group.GroupId);

            if (existedGroup == null)
                throw new Exception("Группа не найдена");

            existedGroup.GroupName = group.GroupName;
            var updated = await _dbContext.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> DeleteGroup(int groupId)
        {
            var existedGroup = await _dbContext.Groups.FirstOrDefaultAsync(x =>
                x.GroupId == groupId);

            if (existedGroup == null)
                throw new Exception("Группа не найдена");

            _dbContext.Remove(existedGroup);
            var deleted = await _dbContext.SaveChangesAsync();
            return deleted > 0;
        }
    }
}