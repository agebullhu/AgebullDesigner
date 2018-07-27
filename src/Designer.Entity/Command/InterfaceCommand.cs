using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    ///     实体实现接口的命令
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    public class InterfaceCommand : DesignCommondBase<EntityConfig>
    {
        #region 命令注入

        /// <summary>
        /// 生成命令对象
        /// </summary>
        /// <returns></returns>
        protected override void CreateCommands(List<ICommandItemBuilder> commands)
        {
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Action = (ToIDataState),
                Caption = "实现状态数据(IStateData)接口",
                Editor = "Entity",
                SignleSoruce=false,
                Catalog = "工具",
                IconName ="img_link"
            });
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Action = (ToIHistory),
                Caption = "实现历史数据(IHistoryData)接口",
                Editor = "Entity",
                SignleSoruce = true,
                Catalog = "工具",
                IconName = "img_link"
            });
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Action = (ToIAudit),
                Caption = "实现审核(IAudit)接口",
                Editor = "Entity",
                SignleSoruce = true,
                Catalog = "工具",
                IconName = "img_link"
            });
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Action = (ToMemo),
                Caption = "添加备注(Memo)字段",
                Editor = "Entity",
                SignleSoruce = true,
                Catalog = "工具",
                IconName = "img_link"
            });
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Action = (ToSelfRelation),
                Caption = "添加上级关联(ParentId)字段",
                Catalog = "工具",
                Editor = "Entity",
                SignleSoruce = true,
                IconName = "img_link"
            });
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Action = (CheckIHistory),
                Caption = "规范历史信息(IHistoryData)",
                Catalog = "工具",
                SignleSoruce = true,
                IconName = "img_link"
            });
        }
        public void ToSelfRelation(EntityConfig entity)
        {
            var cmd = new EntityInterfaceSeter();
            cmd.ToSelfRelation(entity);
        }

        public void ToMemo(EntityConfig entity)
        {
            var cmd = new EntityInterfaceSeter();
            cmd.ToMemo(entity);
        }

        public void ToIDataState(EntityConfig entity)
        {
            var cmd = new EntityInterfaceSeter();
            cmd.ToIDataState(entity);
        }

        public void ToIHistory(EntityConfig entity)
        {
            var cmd = new EntityInterfaceSeter();
            cmd.ToIHistory(entity);
        }

        public void ToIAudit(EntityConfig entity)
        {
            var cmd = new EntityInterfaceSeter();
            cmd.ToIAudit(entity);
        }


        #region 历史信息检查

        void CheckIHistory(EntityConfig entity)
        {
            if (entity.Properties.Count == 0)
                return;
            if (entity.Interfaces == null || !entity.Interfaces.Contains("IHistoryData"))
            {
                if (!entity.Properties.Any(p => string.Equals(p.Name, "CreatedDate", StringComparison.OrdinalIgnoreCase)))
                    return;
                if (!entity.Properties.Any(p => string.Equals(p.Name, "CreatedBy", StringComparison.OrdinalIgnoreCase)))
                    return;
                if (!entity.Properties.Any(p => string.Equals(p.Name, "UpdatedDate", StringComparison.OrdinalIgnoreCase)))
                    return;
                if (!entity.Properties.Any(p => string.Equals(p.Name, "UpdatedBy", StringComparison.OrdinalIgnoreCase)))
                    return;
                //TrySetInterface("IHistoryData");
            }
            var history = GlobalConfig.Entities.Where(p => p.IsInterface && p.Name == "IHistoryData").ToArray();
            CheckField(entity, history[0], "CreatedDate", "AddDate");
            CheckField(entity, history[0], "CreatedBy", "Author");
            CheckField(entity, history[0], "UpdatedDate", "LastModifyDate");
            CheckField(entity, history[0], "UpdatedBy", "LastReviser");
        }

        void CheckField(EntityConfig entity, EntityConfig history, string f1, string f2)
        {
            var a = entity.Properties.FirstOrDefault(p => string.Equals(p.Name, f1, StringComparison.OrdinalIgnoreCase)) ?? entity.Properties.First(p => string.Equals(p.Name, f2, StringComparison.OrdinalIgnoreCase));
            var b = history.Properties.First(p => string.Equals(p.Name, f2, StringComparison.OrdinalIgnoreCase));
            
            a.CopyFrom(b);

            a.IsSystemField = true;
            a.IsInterfaceField = true;
            a.Option.IsReference = true;
            a.Option.ReferenceKey = b.Key;
        }
        #endregion
    }

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

        #endregion

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
    }
}