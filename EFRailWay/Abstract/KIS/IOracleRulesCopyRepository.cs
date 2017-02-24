using EFRailWay.Entities;
using EFRailWay.Entities.KIS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailWay.Abstract.KIS
{
    public interface IOracleRulesCopyRepository : IDBRepository
    {
        IQueryable<Oracle_RulesCopy> Oracle_RulesCopy { get; }
        int SaveOracle_RulesCopy(Oracle_RulesCopy Oracle_RulesCopy);
        Oracle_RulesCopy DeleteOracle_RulesCopy(int IDRulesCopy);
    }
}
