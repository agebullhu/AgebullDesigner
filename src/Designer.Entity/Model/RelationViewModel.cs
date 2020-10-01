using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Agebull.EntityModel.Designer
{
    internal class RelationViewModel : ExtendViewModelBase<ModelRelationDesignModel>
    {
        public RelationViewModel()
        {
            EditorName = "Relation";
        }
    }

    /// <summary>
    /// 模型配置相关
    /// </summary>
    public class ModelRelationDesignModel : DesignModelBase
    {
        #region 操作命令

        public ModelRelationDesignModel()
        {
            Model = DataModelDesignModel.Current;
            Context = DataModelDesignModel.Current?.Context;
        }

        /// <summary>
        /// 生成命令对象
        /// </summary>
        /// <returns></returns>
        public override NotificationList<CommandItemBase> CreateCommands()
        {
            return CreateCommands(true, true, true);
        }

        /// <summary>
        /// 生成命令对象
        /// </summary>
        /// <returns></returns>
        protected NotificationList<CommandItemBase> CreateCommands(bool edit, bool create, bool ext)
        {
            NotificationList<CommandItemBase> commands = new NotificationList<CommandItemBase>();
            if (edit)
            {
                commands.Add(new CommandItem
                {
                    IsButton = true,
                    Action = AddColumns,
                    NoConfirm = true,
                    Caption = "增加行",
                    Image = Application.Current.Resources["tree_add"] as ImageSource
                });
                commands.Add(new CommandItem
                {
                    IsButton = true,
                    Action = PasteColumns,
                    NoConfirm = true,
                    Caption = "粘贴关系",
                    Image = Application.Current.Resources["tree_item"] as ImageSource
                });
                commands.Add(new CommandItem
                {
                    IsButton = true,
                    Action = DeleteColumns,
                    Caption = "删除行",
                    Image = Application.Current.Resources["img_del"] as ImageSource
                });
            }
            if (create)
                CreateCommands(commands);
            if (ext)
            {
                var extends = CommandCoefficient.CoefficientEditor(typeof(EntityConfig), EditorName);
                if (extends.Count > 0)
                    commands.AddRange(extends);
            }
            return commands;
        }

        #endregion

        #region 操作

        public void DeleteColumns(object arg)
        {
            if (Context.SelectModel == null || Context.SelectColumns == null ||
                MessageBox.Show("确认删除所选行吗?", "对象编辑", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
            {
                return;
            }
            foreach (var col in Context.SelectColumns.OfType<ReleationConfig>().ToArray())
            {
                Context.SelectModel.Remove(col);
            }
            Context.SelectColumns = null;
        }

        /// <summary>
        /// 复制字段
        /// </summary>
        public void AddColumns(object arg)
        {
            Context.SelectModel.Releations.Add(new ReleationConfig
            {
                PrimaryTable = Context.SelectModel.Entity.Name,
                PrimaryKey = Context.SelectModel.Entity.PrimaryField
            });
        }

        /// <summary>
        /// 复制字段
        /// </summary>
        public void PasteColumns(object arg)
        {
            if (Context.CopyColumns == null || Context.CopyColumns.Count == 0 || Context.SelectModel == null || Context.CopiedTable == null)
            {
                Context.StateMessage = "没可粘贴的行";
                return;
            }
            foreach (var copyColumn in Context.CopyColumns)
            {
                var releation = Context.SelectModel.Releations.FirstOrDefault(p => p.ForeignKey == copyColumn.Name && p.ForeignTable == copyColumn.Entity.Name);
                if (releation == null)
                {
                    Context.SelectModel.Releations.Add(releation = new ReleationConfig
                    {
                        PrimaryTable = Context.SelectModel.Entity.Name,
                        PrimaryKey = Context.SelectModel.Entity.PrimaryField,
                        ForeignTable = copyColumn.Entity.Name,
                        ForeignKey = copyColumn.Name,
                        JoinType = EntityJoinType.Left,
                        ModelType = ReleationModelType.ExtensionProperty
                    });
                }
                releation.Copy(releation.ForeignEntity);
                CheckReleation(releation, Context.SelectModel);
            }
        }
        public static void CheckReleation(ReleationConfig releation, ModelConfig model)
        {
            var name = GlobalConfig.ToLinkWordName(releation.ForeignTable, "_", false);
            if (releation.ModelType == ReleationModelType.ExtensionProperty)
                foreach (var field in releation.ForeignEntity.Properties)
                {
                    if (model.Properties.Any(p => p.Field == field) ||
                        field.LinkTable == model.Entity.Name &&
                        field.LinkField == model.Entity.PrimaryField)
                        continue;

                    var pro = new PropertyConfig { Field = field };
                    pro.Option.IsDiscard = true;
                    model.Properties.Add(pro);
                    if (field.IsPrimaryKey || model.Properties.Any(p => p != pro && p.Name == pro.Name))
                    {
                        pro.Name = $"{field.Entity.Name}{field.Name}";
                    }
                    if (model.Properties.Any(p => p != pro && p.Name == pro.Name))
                    {
                        pro.Name = $"{field.Entity.Name}{field.Name}_{model.Properties.Count}";
                    }

                    if (field.IsPrimaryKey || model.Properties.Any(p => p != pro && p.DbFieldName == pro.DbFieldName))
                    {
                        pro.DbFieldName = $"{name}_{field.DbFieldName}";
                    }
                    if (model.Properties.Any(p => p != pro && p.DbFieldName == pro.DbFieldName))
                    {
                        pro.DbFieldName = $"{name}_{field.DbFieldName}_{model.Properties.Count}";
                    }
                }
            else
                foreach (var field in releation.ForeignEntity.Properties)
                {
                    var pro = model.Properties.FirstOrDefault(p => p.Field == field);
                    if (pro != null)
                        model.Properties.Remove(pro);
                }
        }
        #endregion
    }

}