// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// ����:bull2
// ����:CodeRefactor-Agebull.CodeRefactor.CodeAnalyze.Application
// ����:2014-11-20
// �޸�:2014-11-29
// *****************************************************/

using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config.V2021;
using Agebull.EntityModel.Designer;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Agebull.Common.Config.Designer.DataBase.Mysql
{
    internal class DataBaseViewModel : EditorViewModelBase<DataBaseModel>
    {
        public DataBaseViewModel()
        {
            EditorName = "DataBase";
        }
    }

    internal class DataBaseModel : EntityExtendModel<DataTableConfig, DataBaseFieldConfig>
    {

        /// <summary>
        /// ��ʼ��
        /// </summary>
        protected override void DoInitialize()
        {
            base.DoInitialize();
            Extend = Entity?.DataTable;

        }
        #region ��������

        /// <summary>
        /// �����������
        /// </summary>
        /// <returns></returns>
        public override void CreateCommands(IList<CommandItemBase> commands)
        {
            commands.Add(new CommandItem<DataTableConfig>
            {
                Action = UpperHump,
                IsButton = true,
                Caption = "���շ�����",
                Image = Application.Current.Resources["tree_Assembly"] as ImageSource
            });
            commands.Add(new CommandItem<DataTableConfig>
            {
                Action = LowerHump,
                IsButton = true,
                Caption = "С�շ�����",
                Image = Application.Current.Resources["tree_Assembly"] as ImageSource
            });
            commands.Add(new CommandItem<DataTableConfig>
            {
                Action = Underlined,
                IsButton = true,
                Caption = "Сд�»�������(C���)",
                Image = Application.Current.Resources["tree_Assembly"] as ImageSource
            });
            base.CreateCommands(commands);
        }
        #endregion

        #region ��չ����
        /// <summary>
        /// ���շ�
        /// </summary>
        /// <param name="arg"></param>
        public void UpperHump(DataTableConfig dataTable)
        {
            foreach (var property in dataTable.Fields)
            {
                property.DbFieldName = property.Name;
            }
            if (string.IsNullOrWhiteSpace(dataTable.SaveTableName))
                dataTable.SaveTableName = "tb_" + dataTable.Name;
        }

        /// <summary>
        /// С�շ�����
        /// </summary>
        /// <param name="arg"></param>
        internal void LowerHump(DataTableConfig dataTable)
        {
            foreach (var property in dataTable.Fields)
            {
                property.DbFieldName = property.Name.ToLWord();
            }
            if (string.IsNullOrWhiteSpace(dataTable.SaveTableName))
                dataTable.SaveTableName = "tb_" + dataTable.Name.ToLWord();
        }

        /// <summary>
        /// Сд�»�������
        /// </summary>
        /// <param name="arg"></param>
        internal void Underlined(DataTableConfig dataTable)
        {
            foreach (var property in dataTable.Fields)
            {
                property.DbFieldName = NameHelper.ToLinkWordName(property.Name, "_", false);
            }
            if (string.IsNullOrWhiteSpace(dataTable.SaveTableName))
                dataTable.SaveTableName = "tb_" + NameHelper.ToLinkWordName(dataTable.Name, "_", false);
        }
        #endregion
    }
}