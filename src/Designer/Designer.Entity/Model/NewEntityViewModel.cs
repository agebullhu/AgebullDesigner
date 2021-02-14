// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 工程:CodeRefactor-Agebull.CodeRefactor.CodeAnalyze.Application
// 建立:2014-11-20
// 修改:2014-11-27
// *****************************************************/

#region 引用

using Agebull.Common.Mvvm;
using System.Linq;
using System.Windows;
using System.Windows.Media;

#endregion

namespace Agebull.EntityModel.Designer
{
    public sealed class NewEntityViewModel : ViewModelBase<NewEntityModel>
    {
        private bool _isNew;
        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                RaisePropertyChanged(nameof(Title));
            }
        }

        public bool IsNew
        {
            get => _isNew;
            set
            {
                _isNew = value;
                RaisePropertyChanged(nameof(IsNew));
            }
        }

        public CommandItemBase CancelCommand => new CommandItem
        {
            Action = arg => DoCancel(),
            Caption = "取消",
            Image = Application.Current.Resources["img_error"] as ImageSource
        };

        public CommandItemBase OkCommand => new CommandItem
        {
            Action = arg => DoClose(),
            Caption = "完成",
            Image = Application.Current.Resources["imgSave"] as ImageSource
        };

        private void DoCancel()
        {
            var window = (Window)View;
            window.DialogResult = false;
        }
        private void DoClose()
        {
            if (IsNew && (string.IsNullOrWhiteSpace(Model.Entity.Name) || Model.Columns.Count == 0))
            {
                MessageBox.Show("实体名称为空或没有字段,请检查");
                return;
            }
            foreach (var field in Model.Columns)
            {
                if (field.IsDelete)
                    continue;
                var old = Model.Entity.Properties.FirstOrDefault(p => p != null && p.Name == field.Name);
                if (old != null)
                {
                    old.CsType = field.CsType;
                    old.Caption = field.Caption;
                    old.Description = field.Description;
                    old.CustomType = field.CustomType;
                    old.Nullable = field.Nullable;
                    old.IsArray = field.IsArray;
                    old.IsDictionary = field.IsDictionary;
                    old.Datalen = field.Datalen;
                }
                else
                {
                    Model.Entity.Add(field);
                }
            }
            var window = (Window)View;
            window.Tag = Model.Entity;

            window.DialogResult = true;
        }

    }
}