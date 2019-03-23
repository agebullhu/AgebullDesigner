/*design by:agebull designer date:2018/9/2 14:50:37*/

using System;
using System.Collections.Generic;
using Agebull.Common.Organizations.DataAccess;
using Agebull.EntityModel.Common;
using Agebull.EntityModel.MySql.BusinessLogic;

namespace Agebull.Common.Organizations.BusinessLogic
{
    /// <summary>
    /// 行级权限关联
    /// </summary>
    internal sealed partial class SubjectionBusinessLogic : UiBusinessLogicBase<SubjectionData, SubjectionDataAccess, UserCenterDb>
    {
        private OrganizationDataAccess oAccess;
        /// <summary>
        /// 重构层级关系
        /// </summary>
        public void SyncSubjection()
        {
            Access.DataBase.Clear(((IDataTable)Access).WriteTableName);
            oAccess = new OrganizationDataAccess { DataBase = Access.DataBase };
            SyncSubjection(0, null);
        }

        private void SyncSubjection(long parId, List<long> pars)
        {
            if (parId > 0)
                Access.Insert(new SubjectionData
                {
                    MasterId = parId,
                    SlaveId = parId
                });
            var childs = oAccess.LoadValues(p => p.Id,Convert.ToInt64, p => p.ParentId == parId);
            if (childs.Count == 0)
                return;
            var nps = new List<long>();
            if (pars != null)
                nps.AddRange(pars);
            if (parId > 0)
                nps.Add(parId);
            foreach (var cId in childs)
            {
                if (nps.Contains(cId))
                    continue;//循环截断
                foreach (var pId in nps)
                {
                    Access.Insert(new SubjectionData
                    {
                        MasterId = pId,
                        SlaveId = cId
                    });
                }
                SyncSubjection(cId, nps);
            }
        }
    }
}