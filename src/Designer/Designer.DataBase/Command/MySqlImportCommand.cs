using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;
using System;
using System.ComponentModel.Composition;
using System.Windows;

namespace Agebull.Common.Config.Designer
{
    /// <summary>
    /// 命令注册器
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal sealed class MySqlImportCommand : IAutoRegister
    {
        /// <summary>
        /// 注册代码
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            CommandCoefficient.RegisterItem<ProjectConfig>(new CommandItemBuilder<ProjectConfig, string>(ImportStructParpare, ImportStruct, ImportStructEnd)
            {
                Caption = "导入MySql数据库",
                Catalog = "数据库",
                WorkView = "database",
                SoruceView = "entity",
                IconName = "tree_Assembly"
            });
            CommandCoefficient.RegisterItem<EntityConfig>(new CommandItemBuilder<EntityConfig>
            {
                Caption = "读取MySql表信息",
                Action = ImportEntity,
                SignleSoruce = true,
                Catalog = "数据库",
                WorkView = "database",
                SoruceView = "entity",
                IconName = "tree_Assembly"
            });
        }


        public bool ImportStructParpare(ProjectConfig arg)
        {
            if (DataModelDesignModel.Current.Context.SelectProject == null)
            {
                MessageBox.Show("请选择一个项目并正确设置连接信息后继续");
                return false;
            }
            if (MessageBox.Show($"确认在【{DataModelDesignModel.Current.Context.SelectProject.Caption}】中执行【导入MySql数据库】操作吗?",
                           "对象编辑", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                return false;
            DataModelDesignModel.Current.Editor.ShowTrace();
            return true;
        }


        /// <summary>
        ///     导入MySql数据库
        /// </summary>
        /// <returns></returns>
        internal string ImportStruct(ProjectConfig arg)
        {
            var ctx = DataModelDesignModel.Current.Context;
            new MySqlImport().Import(ctx.CurrentTrace.TraceMessage, ctx.SelectProject, DataModelDesignModel.Current.Dispatcher);
            return string.Empty;
        }

        /// <summary>
        ///     导入MySql数据库
        /// </summary>
        /// <returns></returns>
        internal void ImportEntity(EntityConfig entity)
        {
            var ctx = DataModelDesignModel.Current.Context;
            var import = new MySqlImport();
            import.Import(ctx.CurrentTrace.TraceMessage, entity, DataModelDesignModel.Current.Dispatcher);
        }

        internal void ImportStructEnd(CommandStatus status, Exception ex, string code)
        {
            DataModelDesignModel.Current.Editor.ShowTrace();
            if (ex != null)
                DataModelDesignModel.Current.Context.CurrentTrace.TraceMessage.Track = ex.ToString();
        }
    }
}