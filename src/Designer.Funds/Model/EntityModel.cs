using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows;
using Agebull.Common.SimpleDesign;
using Gboxt.Common.DataAccess.Schemas;
using Gboxt.Common.WpfMvvmBase.Commands;

namespace Agebull.CodeRefactor.SolutionManager
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
            commands.Add(new CommandItemBuilder
            {
                Signle = true,
                NoButton = true,
                Command = new DelegateCommand(CheckDouble),
                Name = "�޸����ݾ���",
                Catalog = "����",
                IconName = "tree_item"
            });
            commands.Add(new CommandItemBuilder
            {
                Signle = true,
                NoButton = true,
                Catalog = "����",
                Command = new DelegateCommand(RepairByArrayLen),
                Name = "�޸��ı�����",
                IconName = "tree_item"
            });
            commands.Add(new CommandItemBuilder
            {
                Command = new DelegateCommand(RepairRegular),
                Name = "C++�����޸�",
                Signle = true,
                NoButton = true,
                Catalog = "C++",
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