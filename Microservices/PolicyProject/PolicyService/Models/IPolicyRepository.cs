using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PolicyService.Models
{
    public interface IPolicyRepository
    {
        Task<IEnumerable<Policy>> GetPolicy(Guid? policyId = null);
        Task<Policy> AddPolicy(Policy newPolicy);
        Task<bool> UpdatePolicy(Policy policy);
        Task<bool> DeletePolicy(Guid policyId);
    }
}