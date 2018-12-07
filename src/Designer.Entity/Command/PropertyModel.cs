using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
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
            commands.Add(new CommandItemBuilder<PropertyConfig>
            {
                
                SignleSoruce = false,
                Catalog = "�ֶ�",
                Action = CheckName,
                Caption = "�ֶ����ƹ淶",
                Description= "��һ��[����/����]�����Ϊ˵��",
                IconName = "tree_item",
                ConfirmMessage= "ȷ��ִ�С��ֶ����ƹ淶���Ĳ�����?"
            });
            commands.Add(new CommandItemBuilder<PropertyConfig>
            {
                Catalog = "����",
                Action = UpdateCustomType,
                Caption = "�޸��û�����",
                IconName = "img_modify"
            });
            commands.Add(new CommandItemBuilder<PropertyConfig>
            {
                Action = p => p.Parent.Remove(p),
                Catalog = "�༭",
                Caption = "ɾ���ֶ�",
                SignleSoruce = true,
                IconName = "img_del"
            });
        }

        #endregion

        public void UpdateCustomType(PropertyConfig field)
        {
            if (string.IsNullOrWhiteSpace(field.CustomType))
            {
                field.CustomType = null;
            }
            else if (field.CustomType.Contains("[]"))
            {
                field.IsArray = true;
                field.CsType = field.CustomType.Split('[')[0];
            }
            else if (field.CustomType.IndexOf("List<", StringComparison.Ordinal) >= 0)
            {
                field.IsArray = true;
                field.CsType = field.CustomType.Split('<', '>')[1];
            }
            else
            {
                field.EnumConfig = SolutionConfig.Current.Enums.FirstOrDefault(p => p.Name == field.CustomType);
            }
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

    }
}