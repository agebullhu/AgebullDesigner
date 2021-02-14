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
    /// <summary>
    /// ʵ���������ģ��
    /// </summary>
    public class ModelDesignModel : DesignModelBase
    {
        #region ��������

        public ModelDesignModel()
        {
            Model = DataModelDesignModel.Current;
            Context = DataModelDesignModel.Current?.Context;
        }

        /// <summary>
        /// �����������
        /// </summary>
        /// <returns></returns>
        public override void CreateCommands(IList<CommandItemBase> commands)
        {
            commands.Add(new CommandItem
            {
                IsButton = true,
                NoConfirm = true,
                Action = CopyColumns,
                Caption = "������",
                Image = Application.Current.Resources["tree_item"] as ImageSource
            });
            commands.Add(new CommandItem
            {
                IsButton = true,
                Action = PasteColumns,
                NoConfirm = true,
                Caption = "ճ����",
                Image = Application.Current.Resources["tree_item"] as ImageSource
            });
            commands.Add(new CommandItem
            {
                IsButton = true,
                Action = ClearColumns,
                Caption = "�����",
                Image = Application.Current.Resources["img_del"] as ImageSource
            });
            commands.Add(new CommandItem
            {
                IsButton = true,
                Action = DeleteColumns,
                Caption = "ɾ����ѡ��",
                Image = Application.Current.Resources["img_del"] as ImageSource
            });

            base.CreateCommands(commands);
        }

        #endregion

        /// <summary>
        /// �����ֶ�
        /// </summary>
        public void CopyColumns(object arg)
        {
            if (Context.SelectEntity is EntityConfig entity)
            {
                Context.CopiedTable = entity;
                Context.CopyColumns = Context.SelectColumns.OfType<FieldConfig>().OrderBy(p => p.Index).ToList();
                Context.StateMessage = $"������{Context.CopyColumns.Count}��";
            }
        }

        /// <summary>
        /// �����ֶ�
        /// </summary>
        public void PasteColumns(object arg)
        {
            if (Context.CopyColumns == null || Context.CopyColumns.Count == 0 ||
                    Context.CopiedTable == null || Context.CopiedTable == Context.SelectEntity ||
                    !(Context.SelectEntity is EntityConfig entity))
            {
                Context.StateMessage = "û��ճ������";
                return;
            }
            var yes = MessageBox.Show("�Ƿ�ճ����ϵ��Ϣ?", "ճ����", MessageBoxButton.YesNo) ==
                      MessageBoxResult.Yes;

            PateFields(yes, Context.CopiedTable, entity, Context.CopyColumns);
            //this.Context.CopiedTable = null;
            //this.Context.CopiedTables.Clear();
            //Context.SelectColumns = null;
            //this.RaisePropertyChanged(() => this.Context.CopiedTableCounts);
        }
        public void ClearColumns(object arg)
        {
            if (!(Context.SelectEntity is EntityConfig entity) ||
                MessageBox.Show("ȷ��ɾ�������ֶ���?", "����༭", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
            {
                return;
            }
            Context.SelectColumns = null;
            entity.Properties.Clear();
        }

        public void DeleteColumns(object arg)
        {
            if (!(Context.SelectEntity is EntityConfig entity) || Context.SelectColumns == null ||
                MessageBox.Show("ȷ��ɾ����ѡ�ֶ���?", "����༭", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
            {
                return;
            }
            foreach (var col in Context.SelectColumns.OfType<FieldConfig>().ToArray())
            {
                entity.Remove(col);
            }
            Context.SelectColumns = null;
        }

        #region ����

        public void PateFields(bool toReference, EntityConfig source, EntityConfig Entity, List<FieldConfig> columns)
        {
            foreach (var copyColumn in columns.OrderBy(p => p.Index))
            {
                var refe = toReference && !copyColumn.Entity.IsInterface;
                FieldConfig newColumn = null;
                if (refe)
                {
                    newColumn = Entity.Properties.FirstOrDefault(p => p.ReferenceKey == copyColumn.Key);
                    if (newColumn == null)
                    {
                        string name = copyColumn.Entity.Name;
                        if (copyColumn.IsPrimaryKey)
                        {
                            newColumn = Entity.Properties.FirstOrDefault(p => p.LinkTable == name && p.IsLinkKey);
                        }
                        else if (copyColumn.IsCaption)
                        {
                            newColumn = Entity.Properties.FirstOrDefault(p => p.LinkTable == name && p.IsLinkCaption);
                        }
                        else
                        {
                            newColumn = Entity.Properties.FirstOrDefault(
                                p => string.Equals(p.LinkTable, name, StringComparison.OrdinalIgnoreCase) && (
                                         string.Equals(p.LinkField, copyColumn.Name, StringComparison.OrdinalIgnoreCase) ||
                                         string.Equals(p.LinkField, copyColumn.DbFieldName, StringComparison.OrdinalIgnoreCase)));
                        }
                    }
                }
                if (newColumn == null)
                {
                    newColumn = new FieldConfig
                    {
                        Entity = Entity
                    };
                    newColumn.CopyProperty(copyColumn, false);
                    newColumn.Entity = source;
                    newColumn.Option.Copy(copyColumn.Option, false);
                    newColumn.Option.Index = newColumn.Option.Identity = 0;

                    if (refe && !copyColumn.IsLinkField)
                    {
                        if (copyColumn.IsPrimaryKey)
                        {
                            newColumn.Name = copyColumn.Entity.Name + "Id";
                            newColumn.Caption = copyColumn.Entity.Caption + "ID";
                            newColumn.DbFieldName = copyColumn.Name.ToLinkWordName("_", false) + "_id";
                        }
                        else if (copyColumn.IsCaption)
                        {
                            newColumn.Name = copyColumn.Entity.Name;
                            newColumn.Caption = copyColumn.Entity.Caption;
                        }
                        newColumn.Option.IsLink = true;
                        newColumn.Option.ReferenceConfig = copyColumn;
                        newColumn.DbFieldName = DataBaseHelper.ToDbFieldName(newColumn);
                        newColumn.JsonName = newColumn.Name.ToLWord();
                    }
                    Entity.Add(newColumn);
                }
                if (refe)
                {
                    if (copyColumn.IsLinkField)
                    {
                        newColumn.Option.IsLink = true;
                        newColumn.Option.ReferenceConfig = copyColumn.Option.ReferenceConfig;
                    }
                    else
                    {
                        newColumn.IsLinkField = true;
                        newColumn.Option.ReferenceConfig = copyColumn;
                        newColumn.LinkTable = source.Name;
                        newColumn.LinkField = copyColumn.Name;
                        if (copyColumn.IsLinkKey || copyColumn.IsPrimaryKey)
                        {
                            newColumn.IsLinkKey = true;
                            newColumn.KeepUpdate = true;
                        }
                        else if (copyColumn.IsCaption)
                        {
                            newColumn.IsLinkCaption = true;
                            newColumn.IsCompute = true;
                        }
                    }
                }
                newColumn.Entity = Entity;
                newColumn.IsPrimaryKey = false;
                newColumn.IsCaption = false;
                if (newColumn.IsLinkKey)
                    newColumn.NoneApiArgument = true;
            }
            Entity.IsModify = true;
        }

        #endregion
    }
}