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
            commands.Add(new CommandItemBuilder
            {
                Signle = true,
                NoButton = true,
                Command = new DelegateCommand(CheckDouble),
                Caption = "�޸����ݾ���",
                Editor = "C++�ֶ�",
                IconName = "tree_item"
            });
            commands.Add(new CommandItemBuilder
            {
                Signle = true,
                NoButton = true,
                Editor = "C++�ֶ�",
                Command = new DelegateCommand(RepairByArrayLen),
                Caption = "�޸��ı�����",
                IconName = "tree_item"
            });
            commands.Add(new CommandItemBuilder
            {
                Command = new DelegateCommand(RepairRegular),
                Caption = "C++�����޸�",
                Signle = true,
                NoButton = true,
                Editor = "C++�ֶ�",
                IconName = "tree_item"
            });
        }

        #region �޸�
        public void RepairRegular()
        {
            var result = MessageBox.Show("ѡ���ǡ�����ѡ��,���񡿽���鲢�޸�����ȷ��������", "C++�����޸�", MessageBoxButton.YesNoCancel);
            if (result == MessageBoxResult.Cancel)
            {
                return;
            }
            var tables = Context.GetSelectEntities();
            foreach (var entity in tables)
            {
                EntityBusinessModel business = new EntityBusinessModel
                {
                    Entity = entity
                };
                business.RepairCpp(result == MessageBoxResult.Yes);
            }
        }
        public void CheckDouble()
        {
            if (MessageBox.Show("ȷ�Ͻ�DoubleתΪ��ȷֵ��?\nҪ֪�������һ���ƻ���!", "����༭", MessageBoxButton.YesNo) !=
                MessageBoxResult.Yes)
            {
                return;
            }
            var tables = Context.GetSelectEntities();
            foreach (var entity in tables)
            {
                EntityBusinessModel business = new EntityBusinessModel
                {
                    Entity = entity
                };
                business.CheckDouble();
            }
        }

        public void RepairByArrayLen()
        {
            if (MessageBox.Show("ȷ���޸��ı�������?\nҪ֪�������һ���ƻ���!", "����༭", MessageBoxButton.YesNo) !=
                MessageBoxResult.Yes)
            {
                return;
            }
            var tables = Context.GetSelectEntities();
            foreach (var entity in tables)
            {
                EntityBusinessModel business = new EntityBusinessModel
                {
                    Entity = entity
                };
                business.RepairByArrayLen();
            }
        }

        #endregion

    }
}