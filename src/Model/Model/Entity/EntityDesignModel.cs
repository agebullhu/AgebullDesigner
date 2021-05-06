using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Config.V2021;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 实体配置相关模型
    /// </summary>
    public class ModelDesignModel : DesignModelBase
    {
        #region 操作命令

        public ModelDesignModel()
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
            commands.Add(new SimpleCommandItem
            {
                IsButton = true,
                Action = CopyColumns,
                Caption = "复制列",
                IconName = "复制"
            });
            commands.Add(new SimpleCommandItem
            {
                IsButton = true,
                Action = PasteColumns,
                Caption = "粘贴列",
                IconName = "粘贴"
            });
            commands.Add(new SimpleCommandItem
            {
                IsButton = true,
                Action = ClearColumns,
                Caption = "清除列",
                IconName = "清除"
            });
            commands.Add(new SimpleCommandItem
            {
                IsButton = true,
                Action = DeleteColumns,
                Caption = "删除所选列",
                IconName = "删除"
            });

            base.CreateCommands(commands);
        }

        #endregion

        /// <summary>
        /// 复制字段
        /// </summary>
        public void CopyColumns()
        {
            if (Context.SelectEntity is EntityConfig entity)
            {
                Context.CopiedTable = entity;
                Context.CopyColumns = Context.SelectColumns.OfType<FieldConfig>().OrderBy(p => p.Index).ToList();
                Context.StateMessage = $"复制了{Context.CopyColumns.Count}行";
            }
        }

        /// <summary>
        /// 复制字段
        /// </summary>
        public void PasteColumns()
        {
            if (Context.CopyColumns == null || Context.CopyColumns.Count == 0 ||
                    Context.CopiedTable == null || Context.CopiedTable == Context.SelectEntity ||
                    Context.SelectEntity is not EntityConfig entity)
            {
                Context.StateMessage = "没可粘贴的行";
                return;
            }
            var yes = MessageBox.Show("是否粘贴关系信息?", "粘贴行", MessageBoxButton.YesNo) ==
                      MessageBoxResult.Yes;

            PateFields(yes, Context.CopiedTable, entity, Context.CopyColumns);
            //this.Context.CopiedTable = null;
            //this.Context.CopiedTables.Clear();
            //Context.SelectColumns = null;
            //this.RaisePropertyChanged(() => this.Context.CopiedTableCounts);
        }
        public void ClearColumns()
        {
            if (Context.SelectEntity is not EntityConfig entity ||
                MessageBox.Show("确认删除所有字段吗?", "对象编辑", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
            {
                return;
            }
            Context.SelectColumns = null;
            entity.Properties.Clear();
        }

        public void DeleteColumns()
        {
            if (Context.SelectEntity is not EntityConfig entity || Context.SelectColumns == null ||
                MessageBox.Show("确认删除所选字段吗?", "对象编辑", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
            {
                return;
            }
            foreach (var col in Context.SelectColumns.OfType<FieldConfig>().ToArray())
            {
                entity.Remove(col);
            }
            Context.SelectColumns = null;
        }

        #region 复制

        public void PateFields(bool toReference, EntityConfig source, EntityConfig Entity, List<FieldConfig> columns)
        {
            foreach (var copyProperty in columns.OrderBy(p => p.Index))
            {
                var refe = toReference && !copyProperty.Entity.IsInterface;
                FieldConfig newProperty = null;
                if (refe)
                {
                    newProperty = Entity.Find(p => p.ReferenceKey == copyProperty.Key);
                    if (newProperty == null)
                    {
                        string name = copyProperty.Entity.Name;
                        if (copyProperty.IsPrimaryKey)
                        {
                            newProperty = Entity.DataTable.Find(p => p.LinkTable == name && p.IsLinkKey)?.Property.Field;
                        }
                        else if (copyProperty.IsCaption)
                        {
                            newProperty = Entity.DataTable.Find(p => p.LinkTable == name && p.IsLinkCaption)?.Property.Field;
                        }
                        else
                        {
                            newProperty = Entity.DataTable.Find(p => name.IsMe(p.LinkTable) &&
                                p.LinkField.IsOnce(p.LinkField, copyProperty.DataBaseField?.DbFieldName))?.Property.Field;
                        }
                    }
                }
                var newField = newProperty?.DataBaseField;
                if (newProperty == null)
                {
                    newProperty = new FieldConfig
                    {
                        Entity = Entity,
                    };
                    newProperty.CopyProperty(copyProperty, false);
                    newProperty.Entity = source;
                    newProperty.Option.Copy(copyProperty.Option, false);
                    newProperty.Option.Index = newProperty.Option.Identity = 0;
                    newProperty.Entity = source;
                    Entity.Add(newProperty);

                    newField = newProperty.DataBaseField;
                    newField.CopyProperty(copyProperty);

                    if (refe && !copyProperty.IsLinkField)
                    {
                        if (copyProperty.IsPrimaryKey)
                        {
                            newProperty.Name = copyProperty.Entity.Name + "Id";
                            newProperty.Caption = copyProperty.Entity.Caption + "ID";
                            newField.DbFieldName = copyProperty.Name.ToLinkWordName("_", false) + "_id";
                        }
                        else if (copyProperty.IsCaption)
                        {
                            newProperty.Name = copyProperty.Entity.Name;
                            newProperty.Caption = copyProperty.Entity.Caption;
                        }
                        newProperty.Option.IsLink = true;
                        newProperty.Option.ReferenceConfig = copyProperty;
                        newField.DbFieldName = DataBaseHelper.ToDbFieldName(newProperty);
                        newProperty.JsonName = newProperty.Name.ToLWord();
                    }
                }
                if (refe)
                {
                    if (copyProperty.IsLinkField)
                    {
                        newProperty.IsReadonly = false;
                        newProperty.Option.IsLink = true;
                        newProperty.Option.ReferenceConfig = copyProperty.Option.ReferenceConfig;
                    }
                    else
                    {
                        newProperty.IsLinkField = true;
                        newProperty.Option.ReferenceConfig = copyProperty;
                        newField.LinkTable = source.Name;
                        newField.LinkField = copyProperty.Name;
                        if (newField.IsLinkKey || copyProperty.IsPrimaryKey)
                        {
                            newField.IsLinkKey = true;
                            newField.CanUpdate = false;
                        }
                        else if (copyProperty.IsCaption)
                        {
                            newField.IsLinkCaption = true;
                            newField.KeepStorageScreen = StorageScreenType.Update;
                        }
                    }
                }
                newProperty.Entity = Entity;
                newProperty.IsPrimaryKey = false;
            }
        }

        #endregion
    }
}