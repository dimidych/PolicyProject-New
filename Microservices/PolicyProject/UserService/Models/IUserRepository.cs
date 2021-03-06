﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UserService.Models
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUser(Guid? userId = null);
        Task<User> AddUser(User newUser);
        Task<bool> UpdateUser(User user);
        Task<bool> DeleteUser(Guid userId);
    }
}