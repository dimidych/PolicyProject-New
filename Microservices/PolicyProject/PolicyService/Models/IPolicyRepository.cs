using System.Collections.Generic;
using System.Threading.Tasks;

namespace PolicyService.Models
{
    public interface IPolicyRepository
    {
        Task<IEnumerable<DevicePlatform>> GetDevicePlatform(short? devicePlatformId = null);
        Task<IEnumerable<Policy>> GetPolicy(long? policyId = null);
        Task<Policy> AddPolicy(Policy newPolicy);
        Task<bool> UpdatePolicy(Policy policy);
        Task<bool> DeletePolicy(long policyId);
    }
}