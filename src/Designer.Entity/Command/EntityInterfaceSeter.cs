using System.Linq;
using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;

namespace Agebull.EntityModel.Designer
{/*
    public class EntityInterfaceSeter
    {
        private EntityConfig _entity;

        public void ToMemo(EntityConfig entity)
        {
            _entity = entity;

            CheckGroup();
            TryAdd(_memo);
        }

        public void ToSelfRelation(EntityConfig entity)
        {
            _entity = entity;
            CheckGroup();
            TryAdd(_parent);
        }

        public void ToIDataState(EntityConfig entity)
        {
            _entity = entity;
            CheckGroup();
            TryAdd(_dataState, _isFreeze);
            TrySetInterface("IStateData");
        }

        public void ToIHistory(EntityConfig entity)
        {
            _entity = entity;
            CheckGroup();
            TryAdd(_dataState, _isFreeze, _authorId, _addDate, _lastReviserId, _lastModifyDate);
            TrySetInterface("IStateData", "IHistoryData");
        }

        public void ToIAudit(EntityConfig entity)
        {
            _entity = entity;
            CheckGroup();
            TryAdd(_dataState, _isFreeze, _authorId, _addDate, _lastReviserId, _lastModifyDate, _auditState, _auditorId, _auditDate);
            TrySetInterface("IStateData", "IHistoryData", "IAuditData");
        }

        private void CheckGroup()
        {
            _entity.Properties.Foreach(p =>
            {
                if (p.Group == null) p.Group = "数据";
            });
        }

        public void TrySetInterface(params string[] faces)
        {
            if (_entity.Interfaces == null)
            {
                _entity.Interfaces = faces.LinkToString(",");
                return;
            }
            foreach (var face in faces)
            {
                if (!_entity.Interfaces.Contains(face))
                    _entity.Interfaces += "," + face;
            }
        }

        public void TryAdd(params PropertyConfig[] properties)
        {
            foreach (var property in properties)
            {
                var pr = _entity.Properties.FirstOrDefault(p => p.Name == property.Name);
                if (pr == null)
                {
                    pr = new PropertyConfig();
                    _entity.Add(pr);
                }
                pr.CopyFrom(property);
                if (pr.Index <= 0)
                {
                    pr.Option.Index = _entity.MaxIdentity;
                    _entity.MaxIdentity++;
                }
            }
        }

        #region 预定义
        /// <summary>
        /// 数据状态
        /// </summary>
        public static PropertyConfig _dataState;
        /// <summary>
        /// 数据是否已冻结
        /// </summary>
        public static PropertyConfig _isFreeze;
        /// <summary>
        /// 制作人
        /// </summary>
        public static PropertyConfig _authorId;
        /// <summary>
        /// 制作时间
        /// </summary>
        public static PropertyConfig _addDate;
        /// <summary>
        /// 最后修改者
        /// </summary>
        public static PropertyConfig _lastReviserId;
        /// <summary>
        /// 最后修改日期
        /// </summary>
        public static PropertyConfig _lastModifyDate;
        /// <summary>
        /// 审核状态
        /// </summary>
        public static PropertyConfig _auditState;
        /// <summary>
        /// 审核人
        /// </summary>
        public static PropertyConfig _auditorId;
        /// <summary>
        /// 审核时间
        /// </summary>
        public static PropertyConfig _auditDate;
        /// <summary>
        /// 备注
        /// </summary>
        public static PropertyConfig _memo;
        /// <summary>
        /// 上级标识
        /// </summary>
        public static PropertyConfig _parent;
        /// <summary>
        /// 下级机构ID
        /// </summary>
        public static PropertyConfig _slaveOId;


        #endregion
    }*/
}