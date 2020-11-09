using Agebull.Common;
using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public override void CreateCommands(IList<CommandItemBase> commands)
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
                Action = RelationDiscovery,
                NoConfirm = true,
                Caption = "关系发现",
                Image = Application.Current.Resources["tree_item"] as ImageSource
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
            CommandCoefficient.CoefficientEditor<EntityConfig>(commands, EditorName);
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
        public void RelationDiscovery(object arg)
        {
            var model = Context.SelectModel;
            SolutionConfig.Current.Foreach<FieldConfig>(field =>
            {
                if (field.IsLinkKey && model.Entity.Name.Equals(field.LinkTable, StringComparison.OrdinalIgnoreCase))
                {
                    if (!model.Releations.Any(p => p.ForeignTable == field.Parent.Name && p.ForeignKey == field.Name))
                        model.Releations.Add(new ReleationConfig
                        {
                            Name = field.Parent.Name,
                            Caption = field.Parent.Caption,
                            PrimaryTable = model.Name,
                            PrimaryKey = model.Entity.PrimaryColumn.Name,
                            ForeignTable = field.Parent.Name,
                            ForeignKey = field.Name,
                            JoinType = EntityJoinType.none,
                            ModelType = ReleationModelType.Custom
                        });
                }
            });
        }

        public static void CheckReleation(ModelConfig model)
        {
            foreach (var field in model.Entity.Properties)
            {
                var pro = model.Properties.FirstOrDefault(p => p.Field == field);
                if (pro != null)
                {
                    continue;
                }

                pro = new PropertyConfig { Field = field };
                pro.Option.IsDiscard = true;
                model.Properties.Add(pro);
            }
            foreach (var re in model.Releations)
            {
                CheckReleation(re, model);
            }
        }
        public static void CheckReleation(ReleationConfig releation, ModelConfig model)
        {
            var name = GlobalConfig.ToLinkWordName(releation.ForeignTable, "_", false);
            if (releation.ModelType == ReleationModelType.ExtensionProperty)
                foreach (var field in releation.ForeignEntity.Properties)
                {
                    var pro = model.Properties.FirstOrDefault(p => p.Field == field);
                    if (field.LinkTable == model.Entity.Name)
                    {
                        if (pro != null)
                            pro.Option.IsDiscard = true;
                        continue;
                    }

                    if (pro == null)
                    {
                        pro = new PropertyConfig { Field = field };
                        pro.Option.IsDiscard = true;
                        model.Properties.Add(pro);
                    }
                    if (field.IsPrimaryKey || model.Properties.Any(p => p != pro && p.Name == pro.Name))
                    {
                        pro.Name = $"{field.Entity.Name}{field.Name}";
                        pro.JsonName = pro.Name.ToLWord();
                    }
                    if (model.Properties.Any(p => p != pro && p.Name == pro.Name))
                    {
                        pro.Name = $"{field.Entity.Name}{field.Name}_{model.Properties.Count}";
                        pro.JsonName = pro.Name.ToLWord();
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