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
        public override ObservableCollection<CommandItemBase> CreateCommands()
        {
            ObservableCollection<CommandItemBase> commands = new ObservableCollection<CommandItemBase>
            {
                new CommandItem
                {
                    IsButton=true,
                    Action = (CopyColumns),
                    Caption = "������",
                    Image = Application.Current.Resources["tree_item"] as ImageSource
                },
                new CommandItem
                {
                    IsButton=true,
                    Action = (PasteColumns),
                    Caption = "ճ����",
                    Image = Application.Current.Resources["tree_item"] as ImageSource
                },
                new CommandItem
                {
                    IsButton=true,
                    Action = (ClearColumns),
                    Caption = "�����",
                    Image = Application.Current.Resources["img_del"] as ImageSource
                },
                new CommandItem
                {
                    IsButton=true,
                    Action = (DeleteColumns),
                    Caption = "ɾ����ѡ��",
                    Image = Application.Current.Resources["img_del"] as ImageSource
                },
                new CommandItem
                {
                IsButton=true,
                Action = (AddProperty),
                Caption = "�����ֶ�",
                Image = Application.Current.Resources["tree_Open"] as ImageSource
            }
            };
            CreateCommands(commands);
            var extends = CommandCoefficient.CoefficientEditor(typeof(EntityConfig), EditorName);
            if (extends.Count > 0)
                commands.AddRange(extends);
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
            if (Context.SelectEntity == null || Context.SelectColumns ==null||
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

        public void PateFields(bool yes, EntityConfig source, EntityConfig Entity, List<PropertyConfig> columns)
        {
            foreach (var copyColumn in columns)
            {
                PropertyConfig newColumn;
                if (copyColumn.IsPrimaryKey && copyColumn.Name.ToLower() == "id")
                {
                    string name = copyColumn.Parent.SaveTableName ?? copyColumn.Parent.ReadTableName;
                    newColumn = Entity.Properties.FirstOrDefault(
                        p => p.LinkTable == name && p.IsExtendKey);
                }
                else if (copyColumn.IsCaption)
                {
                    string name = copyColumn.Parent.SaveTableName ?? copyColumn.Parent.ReadTableName;
                    newColumn = Entity.Properties.FirstOrDefault(
                        p => p.LinkTable == name && p.IsLinkCaption);
                }
                else
                {
                    newColumn = Entity.Properties.FirstOrDefault(p =>
                        string.Equals(p.Name, copyColumn.Name, StringComparison.OrdinalIgnoreCase)
                        || string.Equals(p.Alias, copyColumn.Name, StringComparison.OrdinalIgnoreCase)
                        || string.Equals(p.Name, copyColumn.Alias, StringComparison.OrdinalIgnoreCase));
                }

                if (newColumn == null)
                {
                    newColumn = new PropertyConfig();
                    newColumn.CopyFrom(copyColumn);

                    if (copyColumn.IsPrimaryKey && copyColumn.Name.ToLower() == "id")
                    {
                        newColumn.Name = copyColumn.Parent.Name + "Id";
                        newColumn.Caption = copyColumn.Parent.Caption + "���";
                        newColumn.ColumnName = GlobalConfig.SplitWords(newColumn.Name).Select(p => p.ToLower()).LinkToString("_");
                    }
                    else if (copyColumn.IsCaption)
                    {
                        newColumn.Name = copyColumn.Parent.Name;
                        newColumn.Caption = copyColumn.Parent.Caption;
                        newColumn.ColumnName = GlobalConfig.SplitWords(newColumn.Name).Select(p => p.ToLower()).LinkToString("_");
                    }
                    Entity.Add(newColumn);
                }
                newColumn.Parent = Entity;
                if (yes)
                {
                    newColumn.LinkTable = string.IsNullOrEmpty(copyColumn.LinkTable)
                        ? source.SaveTableName
                        : copyColumn.LinkTable;
                    newColumn.LinkField = string.IsNullOrEmpty(copyColumn.LinkField)
                        ? copyColumn.Name
                        : copyColumn.LinkField;
                    if (copyColumn.IsLinkKey || copyColumn.IsPrimaryKey)
                    {
                        newColumn.IsLinkKey = true;
                    }
                    else
                    {
                        newColumn.IsLinkCaption = copyColumn.IsCaption || copyColumn.IsLinkCaption;
                        newColumn.IsCompute = copyColumn.IsCompute;
                    }
                    newColumn.IsCompute = !newColumn.IsLinkKey;
                }
                else
                {
                    newColumn.IsLinkField = false;
                    newColumn.LinkTable = null;
                    newColumn.IsLinkKey = false;
                    newColumn.IsLinkCaption = false;
                    newColumn.IsUserId = false;
                    newColumn.LinkField = null;
                    newColumn.IsCompute = false;

                }
                newColumn.IsPrimaryKey = false;
                newColumn.IsCaption = false;
            }
            Entity.IsModify = true;
        }

        #endregion
    }

}