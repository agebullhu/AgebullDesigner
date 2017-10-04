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
    public class EntityInterfaceCommand : IAutoRegister
    {
        #region 命令注入

        /// <summary>
        /// 注册代码
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            CommandCoefficient.RegisterCommand<EntityConfig, ActionItem>(new ActionItem
            {
                Action = ToIDataState,
                Name = "实现IStateData接口",
                Catalog = "数据模型",
                Tag = "Model,Struct",
                Signle = true,
                NoButton = true,
                Image = Application.Current.Resources["img_link"] as ImageSource
            });
            CommandCoefficient.RegisterCommand<EntityConfig, ActionItem>(new ActionItem
            {
                Action = ToIHistory,
                Name = "实现IHistory接口",
                Catalog = "数据模型",
                Tag = "Model,Struct",
                Signle = true,
                NoButton = true,
                Image = Application.Current.Resources["img_link"] as ImageSource
            });
            CommandCoefficient.RegisterCommand<EntityConfig, ActionItem>(new ActionItem
            {
                Action = ToIAudit,
                Name = "实现IAudit接口",
                Catalog = "数据模型",
                Tag = "Model,Struct",
                Signle = true,
                NoButton = true,
                Image = Application.Current.Resources["img_link"] as ImageSource
            });
            CommandCoefficient.RegisterCommand<EntityConfig, ActionItem>(new ActionItem
            {
                Action = ToMemo,
                Name = "添加备注字段",
                Catalog = "数据模型",
                Tag = "Model,Struct",
                Signle = true,
                NoButton = true,
                Image = Application.Current.Resources["img_link"] as ImageSource
            });
            CommandCoefficient.RegisterCommand<EntityConfig, ActionItem>(new ActionItem
            {
                Action = ToSelfRelation,
                Name = "添加同表上级关联字段",
                Catalog = "数据模型",
                Tag = "Model,Struct",
                Signle = true,
                NoButton = true,
                Image = Application.Current.Resources["img_link"] as ImageSource
            });
        }
        public static void ToSelfRelation(RuntimeActionItem item, object arg)
        {
            var cmd = new EntityInterfaceCommand { _entity = arg as EntityConfig };
            cmd.ToSelfRelation();
        }

        public static void ToMemo(RuntimeActionItem item, object arg)
        {
            var cmd = new EntityInterfaceCommand { _entity = arg as EntityConfig };
            cmd.ToMemo();
        }

        public static void ToIDataState(RuntimeActionItem item, object arg)
        {
            var cmd = new EntityInterfaceCommand { _entity = arg as EntityConfig };
            cmd.ToIDataState();
        }

        public static void ToIHistory(RuntimeActionItem item, object arg)
        {
            var cmd = new EntityInterfaceCommand { _entity = arg as EntityConfig };
            cmd.ToIHistory();
        }

        public static void ToIAudit(RuntimeActionItem item, object arg)
        {
            var cmd = new EntityInterfaceCommand { _entity = arg as EntityConfig };
            cmd.ToIAudit();
        }

        private EntityConfig _entity;

        private void ToMemo()
        {
            CheckGroup();
            TryAdd(_memo);
        }
        private void ToSelfRelation()
        {
            CheckGroup();
            TryAdd(_parent);
        }

        private void ToIDataState()
        {
            CheckGroup();
            TryAdd(_dataState, _isFreeze);
            TrySetInterface("IStateData");
        }

        private void ToIHistory()
        {
            CheckGroup();
            TryAdd(_dataState, _isFreeze, _authorId, _addDate, _lastReviserId, _lastModifyDate);
            TrySetInterface("IStateData", "IHistoryData");
        }

        private void ToIAudit()
        {
            CheckGroup();
            TryAdd(_dataState,_isFreeze, _authorId, _addDate, _lastReviserId, _lastModifyDate, _auditState, _auditorId, _auditDate);
            TrySetInterface("IStateData", "IHistoryData", "IAuditData");
        }

        private void CheckGroup()
        {
            _entity.Properties.Foreach(p =>
            {
                if (p.Group == null) p.Group = "数据";
            });
        }

        private void TrySetInterface(params string[] faces)
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

        private void TryAdd(params  PropertyConfig[] properties)
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

        static EntityInterfaceCommand()
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