using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;
using Agebull.EntityModel.RobotCoder;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    ///     实体实现接口的命令
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    public class InterfaceCommand : DesignCommondBase<EntityConfig>
    {
        /// <summary>
        /// 生成命令对象
        /// </summary>
        /// <returns></returns>
        protected override void CreateCommands(List<ICommandItemBuilder> commands)
        {
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Catalog = "修复",
                Action = ClearOldInterface,
                Caption = "清理无效接口绑定",
                IconName = "tree_sum"
            });
            //commands.Add(new CommandItemBuilder<EntityConfig>
            //{
            //    Action = InterfaceHelper.CheckInterface,
            //    Caption = "刷新接口引用",
            //    SignleSoruce = false,
            //    Catalog = "接口",
            //    IconName = "img_link"
            //});
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Action = InterfaceHelper.ClearInterface,
                Caption = "清理接口引用字段",
                SignleSoruce = false,
                Catalog = "接口",
                IconName = "img_link"
            }); 
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Action = ToIDataState,
                Caption = "实现状态数据(IStateData)接口",
                SignleSoruce = false,
                Catalog = "接口",
                IconName = "img_link"
            });
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Action = ToIHistory,
                Caption = "实现历史数据(IHistoryData)接口",
                SignleSoruce = false,
                Catalog = "接口",
                IconName = "img_link"
            });
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Action = ToIAudit,
                Caption = "实现审核(IAudit)接口",
                SignleSoruce = false,
                Catalog = "接口",
                IconName = "img_link"
            });
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Action = ToMemo,
                Caption = "添加备注(Memo)字段",
                SignleSoruce = false,
                Catalog = "接口",
                IconName = "img_link"
            });
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Action = ToSelfRelation,
                Caption = "实现内联树形接口(IInnerTree)",
                Catalog = "接口",
                SignleSoruce = false,
                IconName = "img_link"
            });
        }
        public void ToSelfRelation(EntityConfig entity)
        {
            ToInterface(entity, "IInnerTree");
        }

        public void ToMemo(EntityConfig entity)
        {
            ToInterface(entity, "IMemo");
        }

        public void ToIDataState(EntityConfig entity)
        {
            ToInterface(entity, "IDataState");
        }
        public void ToIHistory(EntityConfig entity)
        {
            ToInterface(entity, "IHistoryData");
        }

        public void ToIAudit(EntityConfig entity)
        {
            ToInterface(entity, "IAudit");
        }
        public void CheckISiteData(EntityConfig entity)
        {
            var site = entity.Properties.FirstOrDefault(p => p.Name.Equals("SiteSID"));
            if (site == null || !site.IsLinkKey)
                return;
            ToInterface(entity, "ISiteData");
        }
        public void CheckISiteOrgData(EntityConfig entity)
        {
            var site = entity.Properties.FirstOrDefault(p => p.Name.Equals("SiteSID"));
            if (site == null || !site.IsLinkKey)
                return;
            var org = entity.Properties.FirstOrDefault(p => p.Name.Equals("OrgOID"));
            if (org == null || !site.IsLinkKey)
                return;
            ToInterface(entity, "ISiteOrgData");
        }
        

        public void ClearOldInterface(EntityConfig entity)
        {
            foreach (var field in entity.Properties.ToArray())
            {
                if (field.Option.ReferenceConfig is FieldConfig refer && refer.Entity.Parent != null &&
                    refer.Entity.IsInterface && !entity.Interfaces.Contains(refer.Entity.Name))
                    entity.Remove(field);
            }
        }

        public void ToInterface(EntityConfig entity, string it)
        {
            if (string.IsNullOrWhiteSpace(entity.Interfaces))
                entity.Interfaces = it;
            else if (!entity.Interfaces.Contains(it))
                entity.Interfaces += "," + it;
        }
    }
}
