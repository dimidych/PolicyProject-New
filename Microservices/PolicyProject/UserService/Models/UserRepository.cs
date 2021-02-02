using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace UserService.Models
{
    public class UserRepository : IUserRepository
    {
        private readonly IUserDbContext _dbContext;

        public UserRepository(IUserDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<User>> GetUser(Guid? userId = null)
        {
            var result = new List<User>();

            if (userId == null)
                result = await _dbContext.Users.AsNoTracking().ToListAsync();
            else
                result.Add(await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == userId));

            return result;
        }

        public async Task<User> AddUser(User newUser)
        {
            if (newUser == null || string.IsNullOrEmpty(newUser.UserFirstName) ||
                string.IsNullOrEmpty(newUser.UserLastName))
                throw new ArgumentException("Пользователь не задан");

            var existedUser = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(x =>
                x.UserLastName.Equals(newUser.UserLastName, StringComparison.InvariantCultureIgnoreCase) &&
                x.UserFirstName.Equals(newUser.UserFirstName, StringComparison.InvariantCultureIgnoreCase));

            if (existedUser != null)
                throw new Exception("Пользователь уже существует");

            newUser.UserId = Guid.NewGuid();
            var result = await _dbContext.Users.AddAsync(newUser);
            await (_dbContext as DbContext).SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> UpdateUser(User user)
        {
            if (user == null || string.IsNullOrEmpty(user.UserFirstName) ||
                string.IsNullOrEmpty(user.UserLastName))
                throw new Exception("Пользователь не задан");

            var existedUser = await _dbContext.Users.FirstOrDefaultAsync(x =>
                x.UserId == user.UserId);

            if (existedUser == null)
                throw new Exception("Пользователь не найден");

            existedUser.UserFirstName = user.UserFirstName;
            existedUser.UserLastName = user.UserLastName;
            existedUser.UserMiddleName = user.UserMiddleName;
            var updated = await (_dbContext as DbContext).SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> DeleteUser(Guid userId)
        {
            var existedUser = await _dbContext.Users.FirstOrDefaultAsync(x =>
                x.UserId == userId);

            if (existedUser == null)
                throw new Exception("Пользователь не найден");

            _dbContext.Users.Remove(existedUser);
            var deleted = await (_dbContext as DbContext).SaveChangesAsync();
            return deleted > 0;
        }
    }
}