// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 配置:CodeRefactor-Agebull.CodeRefactor.CodeAnalyze.Application
// 建立:2014-11-20
// 修改:2014-11-29
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
        #region 操作命令

        public override NotificationList<CommandItemBase> CreateCommands()
        {
            var items = CreateCommands(false, true, true);
            items.Add(new CommandItem
            {
                Action = UpperHump,
                IsButton = true,
                Caption = "大驼峰名称",
                Image = Application.Current.Resources["tree_Assembly"] as ImageSource
            });
            items.Add(new CommandItem
            {
                Action = LowerHump,
                IsButton = true,
                Caption = "小驼峰名称",
                Image = Application.Current.Resources["tree_Assembly"] as ImageSource
            });
            items.Add(new CommandItem
            {
                Action = Underlined,
                IsButton = true,
                Caption = "小写下划线名称(C风格)",
                Image = Application.Current.Resources["tree_Assembly"] as ImageSource
            });
            return items;
        }
        #endregion

        #region 扩展代码
        /// <summary>
        /// 大驼峰
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
        /// 小驼峰名称
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
        /// 小写下划线名称
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