﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LoginService.Models
{
    public class LoginRepository : ILoginRepository
    {
        private readonly ILoginDbContext _dbContext;

        public LoginRepository(ILoginDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Login>> GetLogin(string login = null)
        {
            var result = new List<Login>();

            if (string.IsNullOrEmpty(login))
                result = await _dbContext.Logins.AsNoTracking().ToListAsync();
            else
                result.Add(await _dbContext.Logins.AsNoTracking().FirstOrDefaultAsync(x =>
                    x.LogIn.Equals(login, StringComparison.InvariantCultureIgnoreCase)));

            return result;
        }

        public async Task<string[]> GetCertificate(Guid loginId)
        {
            var login = await _dbContext.Logins.AsNoTracking().FirstOrDefaultAsync(x => x.LoginId == loginId);

            if (login == null)
                throw new Exception("Логин не найден");

            var result = new[] {login.LogIn, login.Certificate};
            return result;
        }

        public async Task<Login> AddLogin(Login newLogin)
        {
            if (newLogin == null || string.IsNullOrEmpty(newLogin.LogIn))
                throw new ArgumentException("Логин не задан");

            if (string.IsNullOrEmpty(newLogin.Password))
                throw new ArgumentException("Пароль не задан");

            if (newLogin.UserId == Guid.Empty)
                throw new ArgumentException("Пользователь не задан");

            if (newLogin.GroupId == Guid.Empty)
                throw new ArgumentException("Группа не заданa");

            var existed = await _dbContext.Logins.AsNoTracking().FirstOrDefaultAsync(x =>
                x.LogIn.Equals(newLogin.LogIn, StringComparison.InvariantCultureIgnoreCase));

            if (existed != null)
                throw new Exception($"Логин {newLogin.LogIn} уже существует");

            newLogin.LoginId = Guid.NewGuid();
            newLogin.Certificate = CertificateWorker.CreateCertificate();
            var result = await _dbContext.Logins.AddAsync(newLogin);
            await (_dbContext as DbContext).SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> UpdateLogin(Login login)
        {
            if (login == null || string.IsNullOrEmpty(login.LogIn))
                throw new ArgumentException("Логин не задан");

            if (string.IsNullOrEmpty(login.Password))
                throw new ArgumentException("Пароль не задан");

            if (login.UserId == Guid.Empty)
                throw new ArgumentException("Пользователь не задан");

            if (login.GroupId == Guid.Empty)
                throw new ArgumentException("Группа не задана");

            var existed = await _dbContext.Logins.FirstOrDefaultAsync(x =>
                x.LogIn.Equals(login.LogIn, StringComparison.InvariantCultureIgnoreCase));

            if (existed == null)
                throw new Exception($"Логин {login.LogIn} не существует");

            existed.Certificate = CertificateWorker.CreateCertificate();
            existed.LogIn = login.LogIn;
            existed.Password = login.Password;
            existed.GroupId = login.GroupId;
            existed.UserId = login.UserId;
            var updated = await (_dbContext as DbContext).SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> DeleteLogin(Guid loginId)
        {
            var existedLogin = await _dbContext.Logins.FirstOrDefaultAsync(x => x.LoginId == loginId);

            if (existedLogin == null)
                throw new Exception($"Логин c id {loginId} не существует");

            _dbContext.Logins.Remove(existedLogin);
            var deleted = await (_dbContext as DbContext).SaveChangesAsync();
            return deleted > 0;
        }
    }
}