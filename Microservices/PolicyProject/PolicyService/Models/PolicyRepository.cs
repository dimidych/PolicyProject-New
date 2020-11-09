using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PolicyService.Models
{
    public class PolicyRepository : IPolicyRepository
    {
        private readonly PolicyDbContext _dbContext;

        public PolicyRepository(PolicyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<DevicePlatform>> GetDevicePlatform(short? devicePlatformId = null)
        {
            var result = new List<DevicePlatform>();

            if (devicePlatformId == null)
                result = await _dbContext.DevicePlatforms.AsNoTracking().ToListAsync();
            else
                result.Add(await _dbContext.DevicePlatforms.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.DevicePlatformId == devicePlatformId));

            return result;
        }

        public async Task<IEnumerable<Policy>> GetPolicy(long? policyId = null)
        {
            var result = new List<Policy>();

            if (policyId == null)
                result = await _dbContext.Policies.AsNoTracking().ToListAsync();
            else
                result.Add(await _dbContext.Policies.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.PolicyId == policyId));

            return result;
        }

        public async Task<Policy> AddPolicy(Policy newPolicy)
        {
            if (newPolicy == null || string.IsNullOrEmpty(newPolicy.PolicyName))
                throw new ArgumentException("Политика не задана");

            if (newPolicy.PlatformId < 1)
                throw new ArgumentException("Платформа политики не задана");

            if (string.IsNullOrEmpty(newPolicy.PolicyInstruction))
                throw new ArgumentException("Инструкция политики не задана");

            var existed = await _dbContext.Policies.AsNoTracking().FirstOrDefaultAsync(x =>
                x.PolicyName.Equals(newPolicy.PolicyName, StringComparison.InvariantCultureIgnoreCase));

            if (existed != null)
                throw new Exception($"Политика {newPolicy.PolicyName} уже существует");

            newPolicy.PolicyId =
                _dbContext.Policies.Any() ? await _dbContext.Policies.MaxAsync(x => x.PolicyId) + 1 : 1;
            var result = await _dbContext.Policies.AddAsync(newPolicy);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> UpdatePolicy(Policy policy)
        {
            if (policy == null || string.IsNullOrEmpty(policy.PolicyName))
                throw new ArgumentException("Политика не задана");

            if (policy.PlatformId < 1)
                throw new ArgumentException("Платформа политики не задана");

            if (string.IsNullOrEmpty(policy.PolicyInstruction))
                throw new ArgumentException("Инструкция политики не задана");

            var existed = await _dbContext.Policies.AsNoTracking().FirstOrDefaultAsync(x =>
                x.PolicyName.Equals(policy.PolicyName, StringComparison.InvariantCultureIgnoreCase));

            if (existed == null)
                throw new Exception($"Политика {policy.PolicyName} не существует");

            existed.PolicyName = policy.PolicyName;
            existed.PlatformId = policy.PlatformId;
            existed.PolicyInstruction = policy.PolicyInstruction;
            existed.PolicyDefaultParam = policy.PolicyDefaultParam;
            var updated = await _dbContext.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> DeletePolicy(long policyId)
        {
            var existedLogin = await _dbContext.Policies.FirstOrDefaultAsync(x => x.PolicyId == policyId);

            if (existedLogin == null)
                throw new Exception($"Политика c id {policyId} не существует");

            _dbContext.Policies.Remove(existedLogin);
            var deleted = await _dbContext.SaveChangesAsync();
            return deleted > 0;
        }
    }
}