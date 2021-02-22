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
    internal class RelationViewModel : EditorViewModelBase<ModelRelationDesignModel>
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
                Caption = "增加行",
                IconName = "新增"
            });
            commands.Add(new CommandItem
            {
                IsButton = true,
                Action = RelationDiscovery,
                Caption = "向下关系发现",
                IconName = "向下"
            });
            commands.Add(new CommandItem
            {
                IsButton = true,
                Action = LinkDiscovery,
                Caption = "向上关系发现",
                IconName = "向上"
            });
            commands.Add(new CommandItem
            {
                IsButton = true,
                Action = PasteColumns,
                Caption = "粘贴关系",
                IconName = "粘贴"
            });
            commands.Add(new CommandItem
            {
                IsButton = true,
                Action = DeleteColumns,
                Caption = "删除行",
                IconName = "删除"
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
            foreach (var field in model.Where(p => p.DataBaseField.IsLinkKey))
            {
                if (model.Releations.Any(p => p.PrimaryTable == field.DataBaseField.LinkTable))
                    return;
                var parent = GlobalConfig.GetEntity(field.DataBaseField.LinkTable);
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
                if (field.NoStorage || !field.DataBaseField.IsLinkKey || !model.Entity.Name.Equals(field.DataBaseField.LinkTable, StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }
                if (model.Releations.Any(p => p.ForeignTable == field.Entity.Name && p.ForeignKey == field.Name))
                    return;
                model.Releations.Add(new ReleationConfig
                {
                    Name = field.Entity.Name,
                    Caption = field.Entity.Caption,
                    PrimaryTable = model.Name,
                    PrimaryKey = model.Entity.PrimaryColumn.Name,
                    ForeignTable = field.Entity.Name,
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
                var pro = model.Find(p => p.Field == field);
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
                foreach (var pro in model.Where(p => p.Entity == entity).ToArray())
                {
                    model.Properties.Remove(pro);
                }
                return;
            }
            foreach (var property in entity.Properties)
            {
                if (property.IsPrimaryKey && model.DataTable.Any(p => p.IsLinkKey && p.LinkTable == entity.Name))
                    continue;

                var modelProperty = model.Find(p => p.Field == property);

                if (modelProperty != null)
                    continue;
                modelProperty = new PropertyConfig
                {
                    Field = property
                };
                modelProperty.Option.IsDiscard = true;
                var name = property.Name;
                if (property.IsPrimaryKey || model.Properties.Any(p => p.Name == name))
                {
                    name = $"{entity.Name}{property.Name}";
                }
                var count = model.Properties.Count(p => p.Name == name);
                if (count > 0)
                {
                    name = $"{entity.Name}{property.Name}{count}";
                }
                if (modelProperty.Name != name)
                    modelProperty.Name = name;

                var prefix = GlobalConfig.ToLinkWordName(entity.Name, "_", false);
                name = property.DataBaseField.DbFieldName;
                if (property.IsPrimaryKey || model.DataTable.Any(p => p.DbFieldName == name))
                {
                    name = $"{prefix}_{name}";
                }
                count = model.DataTable.Fields.Count(p => p.DbFieldName == name);
                if (count > 0)
                {
                    name = $"{prefix}_{name}_{count}";
                }
                if (property.DataBaseField.DbFieldName != name)
                    property.DataBaseField.DbFieldName = name;

                prefix = entity.Name.ToLWord();
                name = property.Name.ToLWord();
                if (property.IsPrimaryKey || model.Properties.Any(p => p.JsonName == name))
                {
                    name = $"{prefix}{property.Name}";
                }
                count = model.Properties.Count(p => p.JsonName == name);
                if (count > 0)
                {
                    name = $"{prefix}_{name}_{count}";
                }
                if (modelProperty.JsonName != name)
                    modelProperty.JsonName = name;

                model.Properties.Add(modelProperty);
            }
        }
        #endregion
    }

}