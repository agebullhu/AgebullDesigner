using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows;
using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// ��չ�ڵ�
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal sealed class EntityModel : DesignCommondBase<EntityConfig>
    {
        /// <summary>
        /// �����������
        /// </summary>
        /// <returns></returns>
        protected override void CreateCommands(List<ICommandItemBuilder> commands)
        {
            //if (SolutionConfig.Current.SolutionType != SolutionType.Cpp)
            //    return;
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                SignleSoruce = true,
                
                Action = (CheckDouble),
                Caption = "�޸����ݾ���",
                Editor = "C++�ֶ�",
                IconName = "tree_item",
                ConfirmMessage= "ȷ�Ͻ�DoubleתΪ��ȷֵ��?\nҪ֪�������һ���ƻ���!"
            });
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                SignleSoruce = true,
                
                Editor = "C++�ֶ�",
                Action = (RepairByArrayLen),
                Caption = "�޸��ı�����",
                IconName = "tree_item",
                ConfirmMessage = "ȷ���޸��ı�������?\nҪ֪�������һ���ƻ���!"
            });
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Action = (RepairRegular),
                Caption = "C++�����޸�",
                SignleSoruce = true,
                
                Editor = "C++�ֶ�",
                IconName = "tree_item",
                ConfirmMessage = "ȷ���޸�C++������?"
            });
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Action = (ResetRegular),
                Caption = "C++��������",
                SignleSoruce = true,
                
                Editor = "C++�ֶ�",
                IconName = "tree_item",
                ConfirmMessage = "ȷ������C++������?\nҪ֪�������һ���ƻ���!"
            });
        }

        #region �޸�
        public void RepairRegular(EntityConfig entity)
        {
            EntityBusinessModel business = new EntityBusinessModel
            {
                Entity = entity
            };
            business.RepairCpp(false);
        }
        public void ResetRegular(EntityConfig entity)
        {
            EntityBusinessModel business = new EntityBusinessModel
            {
                Entity = entity
            };
            business.RepairCpp(true);
        }
        public void CheckDouble(EntityConfig entity)
        {
            EntityBusinessModel business = new EntityBusinessModel
            {
                Entity = entity
            };
            business.CheckDouble();
        }

        public void RepairByArrayLen(EntityConfig entity)
        {
            EntityBusinessModel business = new EntityBusinessModel
            {
                Entity = entity
            };
            business.RepairByArrayLen();
        }

        #endregion

    }
}