using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Media;
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
            commands.Add(new CommandItemBuilder
            {
                Command = new DelegateCommand(ToIDataState),
                Name = "实现状态数据(IStateData)接口",
                //Catalog = "字段",
                Signle=false,
                
                NoButton = true,
                IconName ="img_link"
            });
            commands.Add(new CommandItemBuilder
            {
                Command = new DelegateCommand(ToIHistory),
                Name = "实现历史数据(IHistoryData)接口",
                //Catalog = "字段",

                Signle = false,

                NoButton = true,
                IconName ="img_link"
            });
            commands.Add(new CommandItemBuilder
            {
                Command = new DelegateCommand(ToIAudit),
                Name = "实现审核(IAudit)接口",
                //Catalog = "字段",

                Signle = false,

                NoButton = true,
                IconName ="img_link"
            });
            commands.Add(new CommandItemBuilder
            {
                Command = new DelegateCommand(ToMemo),
                Name = "添加备注(Memo)字段",
                //Catalog = "字段",

                Signle = false,
                NoButton = true,
                IconName ="img_link"
            });
            commands.Add(new CommandItemBuilder
            {
                Command = new DelegateCommand(ToSelfRelation),
                Name = "添加上级关联(ParentId)字段",
                //Catalog = "字段",

                Signle = false,

                NoButton = true,
                IconName ="img_link"
            });
            //commands.Add(new CommandItemBuilder
            //{
            //    Command = new DelegateCommand(CheckIHistory),
            //    Name = "规范历史信息(IHistoryData)",
            //    Signle = false,
            //    NoButton = true,
            //    IconName ="img_link"
            //});
        }
        public void ToSelfRelation()
        {
            var cmd = new EntityInterfaceSeter();
            Foreach(cmd.ToSelfRelation);
        }

        public void ToMemo()
        {
            var cmd = new EntityInterfaceSeter();
            Foreach(cmd.ToMemo);
        }

        public void ToIDataState()
        {
            var cmd = new EntityInterfaceSeter();
            Foreach(cmd.ToIDataState);
        }

        public void ToIHistory()
        {
            var cmd = new EntityInterfaceSeter();
            Foreach(cmd.ToIHistory);
        }

        public void ToIAudit()
        {
            var cmd = new EntityInterfaceSeter();
            Foreach(cmd.ToIAudit);
        }


        #region 历史信息检查


        void CheckIHistory()
        {
            Foreach(CheckIHistory);
        }
        /*
         CreatedDate	datetime	Unchecked
CreatedBy	varchar(50)	Unchecked
UpdatedDate	datetime	Checked
UpdatedBy	varchar(50)	Checked
*/
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
            a.IsReference = true;
            a.ReferenceKey = b.Key;
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
                    _entity.Properties.Add(pr);
                }
                pr.CopyFrom(property);
                if (pr.Index <= 0)
                {
                    pr.Index = _entity.MaxIdentity;
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

        static EntityInterfaceSeter()
        {
            using (LoadingModeScope.CreateScope())
            {
                _dataState = new PropertyConfig
                {
                    IsPredefined = true,
                    Caption = "数据状态",
                    Description = "数据状态",
                    Index = -1,//100,
                    Name = "DataState",
                    CustomType = "DataStateType",
                    CsType = "int",
                    ColumnName = "data_state",
                    Initialization = "0",
                    DbType = "INT",
                    DbNullable = false,
                    IsSystemField = true,
                    IsInterfaceField = true,
                    Group = "系统_数据状态"
                };
                _isFreeze = new PropertyConfig
                {
                    IsPredefined = true,
                    Caption = "数据是否已冻结",
                    Description = "数据是否已冻结",
                    Index = -1,//101,
                    Name = "IsFreeze",
                    CsType = "bool",
                    ColumnName = "is_freeze",
                    Initialization = "0",
                    DbType = "BOOL",
                    DbNullable = false,
                    IsSystemField = true,
                    IsInterfaceField = true,
                    Group = "系统_数据状态"
                };
                _authorId = new PropertyConfig
                {
                    IsPredefined = true,
                    Caption = "制作人",
                    Description = "制作人",
                    Index = -1,//102,
                    Name = "AuthorID",
                    CsType = "int",
                    ColumnName = "author_id",

                    DbType = "int",
                    Initialization = "0",
                    DbNullable = false,
                    IsSystemField = true,
                    IsInterfaceField = true,
                    Group = "系统_历史"
                };
                _addDate = new PropertyConfig
                {
                    IsPredefined = true,
                    Caption = "制作时间",
                    Description = "制作时间",
                    Index = -1,//103,
                    Name = "AddDate",
                    CsType = "DateTime",
                    //IsCompute = true,
                    ColumnName = "add_date",
                    DbType = "DateTime",
                    DbNullable = true,
                    IsSystemField = true,
                    IsInterfaceField = true,
                    Group = "系统_历史"
                };
                _lastReviserId = new PropertyConfig
                {
                    IsPredefined = true,
                    Caption = "最后修改者",
                    Description = "最后修改者",
                    Index = -1,//104,
                    Name = "LastReviserID",
                    CsType = "int",
                    ColumnName = "last_reviser_id",
                    DbType = "int",
                    Initialization = "0",
                    DbNullable = false,
                    IsSystemField = true,
                    IsInterfaceField = true,
                    Group = "系统_历史"
                };
                _lastModifyDate = new PropertyConfig
                {
                    IsPredefined = true,
                    Caption = "最后修改日期",
                    Description = "最后修改日期",
                    Index = -1,//105,
                    Name = "LastModifyDate",
                    CsType = "DateTime",
                    ColumnName = "last_modify_date",
                    DbType = "DateTime",
                    DbNullable = true,
                    IsSystemField = true,
                    IsInterfaceField = true,
                    Group = "系统_历史"
                };
                _auditState = new PropertyConfig
                {
                    IsPredefined = true,
                    Caption = "审核状态",
                    Description = "审核状态",
                    Index = -1,//106,
                    Name = "AuditState",
                    CustomType = "AuditStateType",
                    CsType = "int",
                    ColumnName = "audit_state",
                    DbType = "int",
                    Initialization = "0",
                    DbNullable = false,
                    IsSystemField = true,
                    IsInterfaceField = true,
                    Group = "系统_审核"
                };
                _auditorId = new PropertyConfig
                {
                    IsPredefined = true,
                    Caption = "审核人",
                    Description = "审核人",
                    Index = -1,//107,
                    Name = "AuditorId",
                    CsType = "int",
                    ColumnName = "auditor_id",
                    DbType = "int",
                    Initialization = "0",
                    DbNullable = false,
                    IsSystemField = true,
                    IsInterfaceField = true,
                    Group = "系统_审核"
                };
                _auditDate = new PropertyConfig
                {
                    IsPredefined = true,
                    Caption = "审核时间",
                    Description = "审核时间",
                    Index = -1,//108,
                    Name = "AuditDate",
                    CsType = "DateTime",
                    ColumnName = "audit_date",
                    DbType = "DateTime",
                    DbNullable = true,
                    IsSystemField = true,
                    IsInterfaceField = true,
                    Group = "系统_审核"
                };
                _memo = new PropertyConfig
                {
                    IsPredefined = true,
                    Caption = "备注",
                    Description = "备注",
                    Index = -1,//27,
                    Name = "Memo",
                    CsType = "string",
                    CanEmpty = true,
                    ColumnName = "memo",
                    DbType = "TEXT",
                    DbNullable = true,
                    IsSystemField = false,
                    IsInterfaceField = false,
                    Group = "备注",
                    IsMemo = true
                };
                _parent = new PropertyConfig
                {
                    IsPredefined = true,
                    Caption = "上级标识",
                    Description = "上级标识，顶级为0",
                    Index = -1,//27,
                    Name = "ParentId",
                    CsType = "int",
                    ColumnName = "parent_id",
                    DbType = "int",
                    DbNullable = false,
                    IsSystemField = true,
                    IsInterfaceField = true,
                    Group = "树形"
                };
                _slaveOId = new PropertyConfig
                {
                    IsPredefined = true,
                    Caption = "下级机构ID",
                    Description = "用于下级机构ID查找到对应的上级",
                    Index = -1,//28,
                    Name = "SlaveOrgId",
                    CsType = "int",
                    ColumnName = "slave_org_id",
                    DbType = "int",
                    DbNullable = false,
                    IsSystemField = false,
                    IsInterfaceField = false,
                    Group = "行级权限"
                };
            }
        }

        #endregion
    }
}