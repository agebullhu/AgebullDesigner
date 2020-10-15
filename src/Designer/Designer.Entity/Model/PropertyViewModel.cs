﻿using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Agebull.EntityModel.Designer
{
    internal class PropertyViewModel : ExtendViewModelBase<ModelFieldDesignModel>
    {
        public PropertyViewModel()
        {
            EditorName = "Property";
        }
    }

    /// <summary>
    /// 模型配置相关
    /// </summary>
    public class ModelFieldDesignModel : DesignModelBase
    {
        #region 操作命令

        public ModelFieldDesignModel()
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
                    Action = RelationColumns,
                    NoConfirm = true,
                    Caption = "更新关系列",
                    Image = Application.Current.Resources["tree_item"] as ImageSource
                });
                commands.Add(new CommandItem
                {
                    IsButton = true,
                    Action = PasteColumns,
                    NoConfirm = true,
                    Caption = "粘贴列",
                    Image = Application.Current.Resources["tree_item"] as ImageSource
                });
                commands.Add(new CommandItem
                {
                    IsButton = true,
                    Action = DeleteColumns,
                    Caption = "删除所选列",
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
        
        public void RelationColumns(object arg)
        {
            foreach(var re in Context.SelectModel.Releations)
            {
                ModelRelationDesignModel.CheckReleation(re, Context.SelectModel);
            }
        }
        public void DeleteColumns(object arg)
        {
            if (Context.SelectModel == null || Context.SelectColumns == null ||
                MessageBox.Show("确认删除所选字段吗?", "对象编辑", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
            {
                return;
            }
            foreach (var col in Context.SelectColumns.OfType<PropertyConfig>().ToArray())
            {
                Context.SelectModel.Remove(col);
            }
            Context.SelectColumns = null;
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
            PateFields(Context.SelectModel, Context.CopyColumns);
        }

        public void PateFields(ModelConfig model, List<FieldConfig> columns)
        {
            foreach (var copyColumn in columns)
            {
                PropertyConfig newColumn = null;
                newColumn = model.Properties.FirstOrDefault(p => p.Field == copyColumn);
                if (newColumn == null)
                {
                    newColumn = new PropertyConfig();
                    newColumn.CopyField(copyColumn);
                    newColumn.Option.Index = newColumn.Option.Identity = 0;
                    model.Add(newColumn);
                }
            }
            model.IsModify = true;
        }

        #endregion
    }

}