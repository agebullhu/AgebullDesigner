using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows;
using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// ������ز�������
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal class PropertyModel : DesignCommondBase<PropertyConfig>
    {
        #region ��������

        /// <summary>
        /// �����������
        /// </summary>
        /// <returns></returns>
        protected override void CreateCommands(List<ICommandItemBuilder> commands)
        {
            commands.Add(new CommandItemBuilder
            {
                NoButton = true,
                Signle = false,
                Catalog = "�༭",

                Command = new DelegateCommand(ToEnglish),
                Caption = "�ֶη���",
                Description = "ͨ���ٶȷ���ӿ�,���ֶε�Ӣ�����Ʒ��������(��Ҫ��������)",
                IconName = "imgBaidu"
            });

            commands.Add(new CommandItemBuilder
            {
                NoButton = true,
                Signle = false,
                Catalog = "�ֶ�",
                Command = new DelegateCommand(CheckName),
                Caption = "�ֶ����ƹ淶",
                Description= "��һ��[����/����]�����Ϊ˵��",
                IconName = "tree_item"
            });
        }

        #endregion

        public void CheckName()
        {
            if (MessageBox.Show("ȷ��ִ�С��ֶ����ƹ淶���Ĳ�����?", "�ֶα༭", MessageBoxButton.YesNo) !=
                MessageBoxResult.Yes)
            {
                return;
            }
            Foreach(CheckName);
        }
        public void CheckName(PropertyConfig property)
        {
            if (string.IsNullOrWhiteSpace(property.Caption))
                return;
            var words = property.Caption.Split(new[] { ',', '��', '(', '��' }, 2, StringSplitOptions.RemoveEmptyEntries);
            if (words.Length > 1)
            {
                property.Caption = words[0];
                property.Description = words[1];
            }
        }

        public void ToEnglish()
        {
            if (MessageBox.Show("ȷ��ִ�С��ֶη��롿�Ĳ�����?", "�ֶα༭", MessageBoxButton.YesNo) !=
                MessageBoxResult.Yes)
            {
                return;
            }
            Foreach(ToEnglish);
        }

        public void ToEnglish(PropertyConfig property)
        {
            try
            {
                var tables = Context.GetSelectEntities();
                foreach (var entity in tables)
                {
                    var model = new EntityBusinessModel { Entity = entity };
                    model.EnglishToChiness();
                }
            }
            catch (Exception ex)
            {
                Context.CurrentTrace.TraceMessage.Status = ex.ToString();
            }
        }
    }
}