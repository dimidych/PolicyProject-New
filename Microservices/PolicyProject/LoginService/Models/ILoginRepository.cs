using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoginService.Models
{
    public interface ILoginRepository
    {
        Task<IEnumerable<Login>> GetLogin(string login = null);
        Task<string[]> GetCertificate(Guid loginId);
        Task<Login> AddLogin(Login newLogin);
        Task<bool> UpdateLogin(Login login);
        Task<bool> DeleteLogin(Guid loginId);
    }
}