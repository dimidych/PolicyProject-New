using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PolicyService.Models
{
    public class PolicyRepository : IPolicyRepository
    {
        private readonly IPolicyDbContext _dbContext;

        public PolicyRepository(IPolicyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Policy>> GetPolicy(Guid? policyId = null)
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

            newPolicy.PolicyId = Guid.NewGuid();
            var result = await _dbContext.Policies.AddAsync(newPolicy);
            await (_dbContext as DbContext).SaveChangesAsync();
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
            var updated = await (_dbContext as DbContext).SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> DeletePolicy(Guid policyId)
        {
            var existedLogin = await _dbContext.Policies.FirstOrDefaultAsync(x => x.PolicyId == policyId);

            if (existedLogin == null)
                throw new Exception($"Политика c id {policyId} не существует");

            _dbContext.Policies.Remove(existedLogin);
            var deleted = await (_dbContext as DbContext).SaveChangesAsync();
            return deleted > 0;
        }
    }
}