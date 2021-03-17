using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PolicySetService.Models
{
    public class PolicySetRepository : IPolicySetRepository
    {
        private readonly IPolicySetDbContext _policySetDbContext;

        public PolicySetRepository(IPolicySetDbContext policySetDbContext)
        {
            _policySetDbContext = policySetDbContext;
        }

        public Task<Dictionary<Guid, Guid[]>> GetGroupIdAndPolicyIdDct()
        {
            var policySets = from policySet in _policySetDbContext.PolicySets
                where policySet.GroupId != null
                group policySet by policySet.GroupId
                into selectedPolicySet
                select
                    new
                    {
                        GroupId = selectedPolicySet.Key,
                        PolicySetId = selectedPolicySet.Select(x => x.PolicyId)
                    };
            var resultDct = new Dictionary<Guid, Guid[]>();

            foreach (var ps in policySets)
                if (ps.GroupId.HasValue)
                    resultDct.Add(ps.GroupId.Value, ps.PolicySetId.ToArray());

            return Task.FromResult(resultDct);
        }

        public async Task<IEnumerable<PolicySet>> GetPolicySets(PolicySet policySet = null)
        {
            var result = new List<PolicySet>();

            if (policySet == null)
                result = await _policySetDbContext.PolicySets.AsNoTracking().ToListAsync();
            else
                result.AddRange(_policySetDbContext.PolicySets.AsNoTracking().Where(x =>
                    (policySet.PolicySetId != Guid.Empty && x.PolicySetId == policySet.PolicySetId)
                    || (policySet.PolicyId != Guid.Empty && x.PolicyId == policySet.PolicyId)
                    || (policySet.LoginId != null && x.LoginId == policySet.LoginId)
                    || (policySet.GroupId != null && x.GroupId == policySet.GroupId)));

            return result;
        }

        public async Task<IEnumerable<PolicySet>> GetPolicySetsForGroup(Guid groupId)
        {
            var result = await _policySetDbContext.PolicySets.AsNoTracking().Where(x =>
                x.GroupId == groupId).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<PolicySet>> GetPolicySetsForLogin(Guid loginId)
        {
            var result = await _policySetDbContext.PolicySets.AsNoTracking().Where(x =>
                x.LoginId == loginId).ToListAsync();
            return result;
        }

        public async Task<PolicySet> AddPolicySet(PolicySet newPolicySet)
        {
            if (newPolicySet == null)
                throw new ArgumentException("Новый набор политик не задан");

            newPolicySet.PolicySetId = Guid.NewGuid();
            var result = await _policySetDbContext.PolicySets.AddAsync(newPolicySet);
            await (_policySetDbContext as DbContext).SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> UpdatePolicySet(PolicySet policySet)
        {
            if (policySet == null)
                throw new ArgumentException("Набор политик не задан");

            var existed = await _policySetDbContext.PolicySets.FirstOrDefaultAsync(x =>
                x.PolicySetId == policySet.PolicySetId);

            if (existed == null)
                throw new Exception($"Набора политик {policySet.PolicySetId} не существует");

            existed.GroupId = policySet.GroupId;
            existed.LoginId = policySet.LoginId;
            existed.PolicyId = policySet.PolicyId;
            existed.Selected = policySet.Selected;
            existed.PolicyParam = policySet.PolicyParam;
            var updated = await (_policySetDbContext as DbContext).SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> DeletePolicySet(Guid policySetId)
        {
            var existed = await _policySetDbContext.PolicySets.FirstOrDefaultAsync(x =>
                x.PolicySetId == policySetId);

            if (existed == null)
                throw new Exception($"Набора политик {policySetId} не существует");

            _policySetDbContext.PolicySets.Remove(existed);
            var deleted = await (_policySetDbContext as DbContext).SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<bool> UpdatePolicySetsForGroup(Guid groupId, PolicySet[] policySetList)
        {
            if (groupId == Guid.Empty)
                throw new ArgumentException("Не задана группа");

            if (policySetList == null || !policySetList.Any())
                throw new ArgumentException("Пустой набор политик");

            var nativeDbContext = _policySetDbContext as DbContext;

            using (var transaction = await nativeDbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var groupPolicySet = _policySetDbContext.PolicySets.Where(x => x.GroupId == groupId);

                    for (var i = 0; i < policySetList.Length; i++)
                    {
                        var containedGroup = await
                            groupPolicySet.FirstOrDefaultAsync(x => x.PolicyId == policySetList[i].PolicyId);

                        if (policySetList[i].Selected.HasValue && policySetList[i].Selected.Value)
                        {
                            if (containedGroup != null)
                                continue;

                            var newPolicySet = new PolicySet
                            {
                                PolicySetId = Guid.NewGuid(),
                                PolicyId = policySetList[i].PolicyId,
                                GroupId = groupId,
                                PolicyParam = policySetList[i].PolicyParam,
                                Selected = policySetList[i].Selected
                            };
                            _policySetDbContext.PolicySets.Add(newPolicySet);
                        }
                        else
                        {
                            if (containedGroup == null)
                                continue;

                            var deletedPolicySet =
                                await _policySetDbContext.PolicySets.FirstOrDefaultAsync(x =>
                                    x.PolicySetId == containedGroup.PolicySetId);

                            if (deletedPolicySet != null)
                                _policySetDbContext.PolicySets.Remove(deletedPolicySet);
                        }
                    }

                    var result = await nativeDbContext.SaveChangesAsync() == policySetList.Length;

                    if (result)
                        await transaction.CommitAsync();

                    return result;
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task<bool> UpdatePolicySetsForLogin(Guid loginId, PolicySet[] policySetList)
        {
            if (loginId == Guid.Empty)
                throw new ArgumentException("Не задан логин");

            if (policySetList == null || !policySetList.Any())
                throw new ArgumentException("Пустой набор политик");

            var nativeDbContext = _policySetDbContext as DbContext;

            using (var transaction = await nativeDbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var groupId = (await _policySetDbContext.Logins.FirstOrDefaultAsync(x => x.LoginId == loginId))
                        .GroupId;
                    var groupPolicySet = _policySetDbContext.PolicySets.Where(x => x.GroupId == groupId);
                    var policySetForLoginInTbl = _policySetDbContext.PolicySets.Where(x => x.LoginId == loginId);

                    for (var i = 0; i < policySetList.Length; i++)
                    {
                        var containedGroup = await
                            groupPolicySet.FirstOrDefaultAsync(x => x.PolicyId == policySetList[i].PolicyId);
                        var containedInTbl = await
                            policySetForLoginInTbl.FirstOrDefaultAsync(x => x.PolicyId == policySetList[i].PolicyId);

                        if (policySetList[i].Selected.HasValue && policySetList[i].Selected.Value)
                        {
                            if (containedGroup == null)
                            {
                                if (containedInTbl != null)
                                {
                                    containedInTbl.PolicyId = policySetList[i].PolicyId;
                                    containedInTbl.LoginId = policySetList[i].LoginId;
                                    containedInTbl.Selected = policySetList[i].Selected;
                                    containedInTbl.PolicyParam = policySetList[i].PolicyParam;
                                }
                                else
                                {
                                    var newPolicySet = new PolicySet
                                    {
                                        PolicySetId = Guid.NewGuid(),
                                        PolicyId = policySetList[i].PolicyId,
                                        LoginId = policySetList[i].LoginId,
                                        Selected = policySetList[i].Selected,
                                        PolicyParam = policySetList[i].PolicyParam
                                    };
                                    _policySetDbContext.PolicySets.Add(newPolicySet);
                                }
                            }
                            else
                            {
                                if (containedInTbl == null)
                                    continue;

                                var deletedPolicySet = await
                                    _policySetDbContext.PolicySets.FirstOrDefaultAsync(x =>
                                        x.PolicySetId == containedInTbl.PolicySetId);

                                if (deletedPolicySet != null)
                                    _policySetDbContext.PolicySets.Remove(deletedPolicySet);
                            }
                        }
                        else
                        {
                            if (containedGroup == null)
                            {
                                if (containedInTbl == null)
                                    continue;

                                var deletedPolicySet = await
                                    _policySetDbContext.PolicySets.FirstOrDefaultAsync(x =>
                                        x.PolicySetId == containedInTbl.PolicySetId);

                                if (deletedPolicySet != null)
                                    _policySetDbContext.PolicySets.Remove(deletedPolicySet);
                            }
                            else
                            {
                                if (containedInTbl != null)
                                {
                                    containedInTbl.PolicyId = policySetList[i].PolicyId;
                                    containedInTbl.LoginId = policySetList[i].LoginId;
                                    containedInTbl.Selected = policySetList[i].Selected;
                                    containedInTbl.PolicyParam = policySetList[i].PolicyParam;
                                }
                                else
                                {
                                    var newPolicySet = new PolicySet
                                    {
                                        PolicySetId = Guid.NewGuid(),
                                        PolicyId = policySetList[i].PolicyId,
                                        LoginId = policySetList[i].LoginId,
                                        Selected = policySetList[i].Selected,
                                        PolicyParam = policySetList[i].PolicyParam
                                    };
                                    _policySetDbContext.PolicySets.Add(newPolicySet);
                                }
                            }
                        }
                    }

                    var result = await nativeDbContext.SaveChangesAsync() == policySetList.Length;

                    if (result)
                        await transaction.CommitAsync();

                    return result;
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
    }
}