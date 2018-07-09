// // /*****************************************************
// // (c)2016-2016 Copy right Agebull.hu
// // 作者:
// // 工程:CodeRefactor
// // 建立:2016-09-18
// // 修改:2016-09-18
// // *****************************************************/


using Agebull.Common.Mvvm;
using Agebull.EntityModel.Designer;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace Agebull.EntityModel.Config
{
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal class ProjectBusinessModel : DesignCommondBase<ProjectConfig>
    {
        protected override void CreateCommands(List<ICommandItemBuilder> commands)
        {
            commands.Add(new CommandItemBuilder
            {
                NoButton = true,
                Command = new DelegateCommand<ProjectConfig>(Lock),
                Caption = "锁定",
                Catalog = "设计",
                IconName = "img_lock"
            });
            commands.Add(new CommandItemBuilder
            {
                NoButton = true,
                Command = new DelegateCommand<ProjectConfig>(UnLock),
                Caption = "解锁",
                Catalog = "设计",
                IconName = "img_no_modify"
            });
        }
        
        void UnLock(ProjectConfig project)
        {
            project.IsFreeze = false;
            foreach (var entity in project.Entities)
            {
                entity.IsFreeze = false;
                foreach (var field in entity.Properties)
                {
                    field.IsFreeze = false;
                    if (field.EnumConfig != null)
                        field.EnumConfig.IsFreeze = false;
                }
            }
        }

        void Lock(ProjectConfig project)
        {
            project.IsFreeze = true;
            foreach (var entity in project.Entities)
            {
                entity.IsFreeze = true;
                foreach (var field in entity.Properties)
                {
                    field.IsFreeze = true;
                    if (field.EnumConfig != null)
                        field.EnumConfig.IsFreeze = true;
                }
            }
        }

    }
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal class EntityCommand : DesignCommondBase<EntityConfig>
    {
        protected override void CreateCommands(List<ICommandItemBuilder> commands)
        {
            commands.Add(new CommandItemBuilder
            {
                NoButton = true,
                
                Command = new DelegateCommand<ProjectConfig>(ToClass),
                Caption = "全部设置为类",
                Catalog = "设计",
                IconName = "tree_Type"
            });
            commands.Add(new CommandItemBuilder
            {
                NoButton = true,
                
                Command = new DelegateCommand<ProjectConfig>(ToReference),
                Caption = "全部设置为引用",
                Catalog = "设计",
                IconName = "img_ref"
            });
            commands.Add(new CommandItemBuilder
            {
                NoButton = true,
                
                Command = new DelegateCommand<ProjectConfig>(ToModify),
                Caption = "强制已修改",
                Catalog = "设计",
                IconName = "img_modify"
            });
        }
        public void ToModify(ProjectConfig project)
        {
            foreach (var entity in project.Entities)
            {
                entity.IsModify = true;
            }
        }

        void ToReference(ProjectConfig project)
        {
            Foreach(entity =>
            {
                entity.IsReference = true;
            });
        }

        void ToClass(ProjectConfig project)
        {
            Foreach(entity =>
            {
                entity.IsClass = true;
            });
        }
    }
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal class FieldCommand : DesignCommondBase<PropertyConfig>
    {
        protected override void CreateCommands(List<ICommandItemBuilder> commands)
        {
            commands.Add(new CommandItemBuilder
            {
                NoButton = true,
                Catalog = "工具",
                Command = new DelegateCommand<ProjectConfig>(UpdateCustomType),
                Caption = "修复用户类型",
                IconName = "img_modify"
            });
        }
        #region 编辑方法

        /// <summary>
        /// 项目对象
        /// </summary>
        public SolutionConfig Solution { get; set; }

        public void UpdateCustomType(ProjectConfig project)
        {
            Foreach(field =>
            {
                if (string.IsNullOrWhiteSpace(field.CustomType))
                {
                    field.IsEnum = false;
                    field.CustomType = null;
                }
                else if (field.CustomType.Contains("[]"))
                {
                    field.IsArray = false;
                    field.IsEnum = false;
                    field.CsType = field.CustomType.Split('[')[0];
                }
                else if (field.CustomType.IndexOf("List<") >= 0)
                {
                    field.IsArray = true;
                    field.IsEnum = false;
                    field.CsType = field.CustomType.Split('<', '>')[1];
                }
                else
                {
                    field.EnumConfig = SolutionConfig.Current.Enums.FirstOrDefault(p => p.Name == field.CustomType);
                    field.IsEnum = field.EnumConfig != null;

                }
            });
        }


        #endregion

    }
}