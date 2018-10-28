using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// ʵ���������ģ��
    /// </summary>
    public class EntityDesignModel : DesignModelBase
    {
        #region ��������

        public EntityDesignModel()
        {
            Model = DataModelDesignModel.Current;
            Context = DataModelDesignModel.Current?.Context;
        }

        /// <summary>
        /// �����������
        /// </summary>
        /// <returns></returns>
        public override NotificationList<CommandItemBase> CreateCommands()
        {
            return CreateCommands(true, true, true);
        }

        /// <summary>
        /// �����������
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
                commands.Add(new CommandItem
                {
                    IsButton = true,
                    Action = AddProperty,
                    NoConfirm = true,
                    Caption = "�����ֶ�",
                    Image = Application.Current.Resources["tree_Open"] as ImageSource
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
        /// <summary>
        /// �����ֶ�
        /// </summary>
        public void AddProperty(object arg)
        {
            var perperty = new PropertyConfig();
            if (CommandIoc.NewConfigCommand("�����ֶ�", perperty))
                Context.SelectEntity.Add(perperty);
        }

        /// <summary>
        /// �����ֶ�
        /// </summary>
        public void CopyColumns(object arg)
        {
            Context.CopiedTable = Context.SelectEntity;
            Context.CopyColumns = Context.SelectColumns.OfType<PropertyConfig>().ToList();
            Context.StateMessage = $"������{Context.CopyColumns.Count}��";
        }

        /// <summary>
        /// �����ֶ�
        /// </summary>
        public void PasteColumns(object arg)
        {
            if (Context.CopyColumns == null || Context.CopyColumns.Count == 0 || Context.CopiedTable == Context.SelectEntity ||
                Context.SelectEntity == null || Context.CopiedTable == null)
            {
                Context.StateMessage = "û��ճ������";
                return;
            }
            var yes = MessageBox.Show("�Ƿ�ճ����ϵ��Ϣ?", "ճ����", MessageBoxButton.YesNo) ==
                      MessageBoxResult.Yes;

            PateFields(yes, Context.CopiedTable, Context.SelectEntity, Context.CopyColumns);

            //this.Context.CopiedTable = null;
            //this.Context.CopiedTables.Clear();
            //Context.SelectColumns = null;
            //this.RaisePropertyChanged(() => this.Context.CopiedTableCounts);
        }
        public void ClearColumns(object arg)
        {
            if (Context.SelectEntity == null ||
                MessageBox.Show("ȷ��ɾ�������ֶ���?", "����༭", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
            {
                return;
            }
            Context.SelectColumns = null;
            Context.SelectEntity.Properties.Clear();
        }

        public void DeleteColumns(object arg)
        {
            if (Context.SelectEntity == null || Context.SelectColumns == null ||
                MessageBox.Show("ȷ��ɾ����ѡ�ֶ���?", "����༭", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
            {
                return;
            }
            foreach (var col in Context.SelectColumns.OfType<PropertyConfig>().ToArray())
            {
                Context.SelectEntity.Properties.Remove(col);
            }
            Context.SelectColumns = null;
        }

        #region ����

        public void PateFields(bool toReference, EntityConfig source, EntityConfig Entity, List<PropertyConfig> columns)
        {
            foreach (var copyColumn in columns)
            {
                var refe = toReference && !copyColumn.Parent.IsInterface;
                PropertyConfig newColumn = null;
                if (refe)
                {
                    newColumn = Entity.Properties.FirstOrDefault(p => p.ReferenceKey == copyColumn.Key);
                    if (newColumn == null)
                    {
                        string name = copyColumn.Parent.Name;
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
                    newColumn = new PropertyConfig();
                    newColumn.CopyFromProperty(copyColumn, false, true, true);
                    newColumn.Option.Index = newColumn.Option.Identity = 0;
                    
                    if (refe && !copyColumn.IsLinkField)
                    {
                        if (copyColumn.IsPrimaryKey)
                        {
                            newColumn.Name = copyColumn.Parent.Name + "Id";
                            newColumn.Caption = copyColumn.Parent.Caption + "���";
                        }
                        else if (copyColumn.IsCaption)
                        {
                            newColumn.Name = copyColumn.Parent.Name;
                            newColumn.Caption = copyColumn.Parent.Caption;
                        }
                        newColumn.Option.IsLink = true;
                        newColumn.Option.ReferenceConfig = copyColumn;
                        newColumn.DbFieldName = DataBaseHelper.ToDbFieldName(newColumn.Name);
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
                        }
                        else if (copyColumn.IsCaption)
                        {
                            newColumn.IsLinkCaption = true;
                            newColumn.IsCompute = true;
                        }
                    }
                }
                newColumn.Parent = Entity;
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