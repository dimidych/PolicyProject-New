using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PolicySetService.Models
{
    public interface IPolicySetRepository
    {
        Task<Dictionary<Guid, Guid[]>> GetGroupIdAndPolicyIdDct();
        Task<IEnumerable<PolicySet>> GetPolicySets(PolicySet policySet = null);
        Task<IEnumerable<PolicySet>> GetPolicySetsForGroup(Guid groupId);
        Task<IEnumerable<PolicySet>> GetPolicySetsForLogin(Guid loginId);
        Task<PolicySet> AddPolicySet(PolicySet newPolicySet);
        Task<bool> UpdatePolicySet(PolicySet policySet);
        Task<bool> DeletePolicySet(Guid policySetId);
        Task<bool> UpdatePolicySetsForGroup(Guid groupId, PolicySet[] policySetList);
        Task<bool> UpdatePolicySetsForLogin(Guid loginId, PolicySet[] policySetList);
    }
}