// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// ����:bull2
// ����:CodeRefactor-Agebull.CodeRefactor.CodeAnalyze.Application
// ����:2014-11-20
// �޸�:2014-11-29
// *****************************************************/

using System.Text;
using System.Windows;
using System.Windows.Media;
using Agebull.Common.Mvvm;
using Agebull.EntityModel;
using Agebull.EntityModel.Designer;
using Agebull.EntityModel.RobotCoder;

namespace Agebull.Common.Config.Designer.DataBase.Mysql
{
    internal class DataBaseViewModel : ExtendViewModelBase<DataBaseModel>
    {
        public DataBaseViewModel()
        {
            EditorName = "DataBase";
        }

    }
    internal class DataBaseModel : ModelDesignModel
    {
        #region ��������

        public override NotificationList<CommandItemBase> CreateCommands()
        {
            var items = CreateCommands(false, true, true);
            items.Add(new CommandItem
            {
                Action = UpperHump,
                IsButton = true,
                Caption = "���շ�����",
                Image = Application.Current.Resources["tree_Assembly"] as ImageSource
            });
            items.Add(new CommandItem
            {
                Action = LowerHump,
                IsButton = true,
                Caption = "С�շ�����",
                Image = Application.Current.Resources["tree_Assembly"] as ImageSource
            });
            items.Add(new CommandItem
            {
                Action = Underlined,
                IsButton = true,
                Caption = "Сд�»�������(C���)",
                Image = Application.Current.Resources["tree_Assembly"] as ImageSource
            });
            return items;
        }
        #endregion

        #region ��չ����
        /// <summary>
        /// ���շ�
        /// </summary>
        /// <param name="arg"></param>
        public void UpperHump(object arg)
        {
            foreach (var property in Context.SelectEntity.Properties)
            {
                property.DbFieldName = property.Name;
            }
            if (string.IsNullOrWhiteSpace(Context.SelectEntity.SaveTableName))
                Context.SelectEntity.SaveTableName = "tb_" + Context.SelectEntity.Name;
        }
        /// <summary>
        /// С�շ�����
        /// </summary>
        /// <param name="arg"></param>
        internal void LowerHump(object arg)
        {
            foreach (var property in Context.SelectEntity.Properties)
            {
                property.DbFieldName = property.Name.ToLWord();
            }
            if (string.IsNullOrWhiteSpace(Context.SelectEntity.SaveTableName))
                Context.SelectEntity.SaveTableName = "tb_" + Context.SelectEntity.Name.ToLWord();
        }
        /// <summary>
        /// Сд�»�������
        /// </summary>
        /// <param name="arg"></param>
        internal void Underlined(object arg)
        {
            foreach (var property in Context.SelectEntity.Properties)
            {
                property.DbFieldName = NameHelper.ToLinkWordName(property.Name, "_", false);
            }
            if (string.IsNullOrWhiteSpace(Context.SelectEntity.SaveTableName))
                Context.SelectEntity.SaveTableName = "tb_" + NameHelper.ToLinkWordName(Context.SelectEntity.Name, "_", false);
        }
        #endregion
    }
}