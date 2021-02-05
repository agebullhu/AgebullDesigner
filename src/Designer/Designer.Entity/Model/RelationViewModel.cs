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
                Caption = "向下关系发现",
                Image = Application.Current.Resources["tree_item"] as ImageSource
            });
            commands.Add(new CommandItem
            {
                IsButton = true,
                Action = LinkDiscovery,
                NoConfirm = true,
                Caption = "向上关系发现",
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
            base.CreateCommands(commands);
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
        /// 粘贴字段
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
        public void LinkDiscovery(object arg)
        {
            var model = Context.SelectModel;
            foreach (var field in model.Properties.Where(p => p.IsLinkKey))
            {
                if (model.Releations.Any(p => p.PrimaryTable == field.LinkTable))
                    return;
                var parent = GlobalConfig.GetEntity(field.LinkTable);
                model.Releations.Add(new ReleationConfig
                {
                    Name = parent.Name,
                    Caption = parent.Caption,
                    PrimaryTable = parent.Name,
                    PrimaryKey = parent.PrimaryColumn.Name,
                    ForeignTable = model.Name,
                    ForeignKey = field.Name,
                    JoinType = EntityJoinType.Inner,
                    ModelType = ReleationModelType.ExtensionProperty
                });
            }
        }

        public void RelationDiscovery(object arg)
        {
            var model = Context.SelectModel;
            SolutionConfig.Current.Foreach<FieldConfig>(field =>
            {
                if (!field.IsLinkKey || !model.Entity.Name.Equals(field.LinkTable, StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }
                if (model.Releations.Any(p => p.ForeignTable == field.Parent.Name && p.ForeignKey == field.Name))
                    return;
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
            });
        }

        public static void CheckReleation(ModelConfig model)
        {
            if (model == null)
                return;
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
        public static void CheckReleation(ReleationConfig r, ModelConfig model)
        {
            EntityConfig friend = r.PrimaryEntity == model.Entity
                ? r.ForeignEntity
                : r.PrimaryEntity;
            SyncField(model, friend, r.ModelType == ReleationModelType.ExtensionProperty);
        }

        public static void SyncField(ModelConfig model, EntityConfig entity, bool join)
        {

            if (!join)
            {
                foreach (var pro in model.Properties.Where(p => p.Entity == entity).ToArray())
                {
                    model.Properties.Remove(pro);
                }
                return;
            }
            foreach (var field in entity.Properties)
            {
                if (field.IsPrimaryKey && model.Properties.Any(p => p.IsLinkKey && p.LinkTable == entity.Name))
                    continue;

                var pro = model.Properties.FirstOrDefault(p => p.Field == field);

                if (pro != null)
                    continue;
                pro = new PropertyConfig
                {
                    Field = field
                };
                pro.Option.IsDiscard = true;
                var name = field.Name;
                if (field.IsPrimaryKey || model.Properties.Any(p => p.Name == name))
                {
                    name = $"{entity.Name}{field.Name}";
                }
                var count = model.Properties.Count(p => p.Name == name);
                if (count > 0)
                {
                    name = $"{entity.Name}{field.Name}{count}";
                }
                if (pro.Name != name)
                    pro.Name = name;

                var prefix = GlobalConfig.ToLinkWordName(entity.Name, "_", false);
                name = field.DbFieldName;
                if (field.IsPrimaryKey || model.Properties.Any(p => p.DbFieldName == name))
                {
                    name = $"{prefix}_{name}";
                }
                count = model.Properties.Count(p => p.DbFieldName == name);
                if (count > 0)
                {
                    name = $"{prefix}_{name}_{count}";
                }
                if (pro.DbFieldName != name)
                    pro.DbFieldName = name;

                prefix = entity.Name.ToLWord();
                name = field.Name.ToLWord();
                if (field.IsPrimaryKey || model.Properties.Any(p => p.JsonName == name))
                {
                    name = $"{prefix}{field.Name}";
                }
                count = model.Properties.Count(p => p.JsonName == name);
                if (count > 0)
                {
                    name = $"{prefix}_{name}_{count}";
                }
                if (pro.JsonName != name)
                    pro.JsonName = name;

                model.Properties.Add(pro);
            }
        }
        #endregion
    }

}