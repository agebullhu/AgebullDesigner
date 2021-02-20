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
            IconName = "取消"
        };

        public CommandItemBase OkCommand => new CommandItem
        {
            Action = arg => DoClose(),
            Caption = "完成",
            IconName  = "编辑成功"
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
            foreach (var property in Model.Columns)
            {
                if (!property.IsActive)
                    continue;
                var old = Model.Entity.Find(property.Name);
                if (old != null)
                {
                    old.CsType = property.CsType;
                    old.Caption = property.Caption;
                    old.Description = property.Description;
                    old.CustomType = property.CustomType;
                    old.Nullable = property.Nullable;
                    old.IsArray = property.IsArray;
                    old.IsDictionary = property.IsDictionary;
                    if (property.DataBaseField != null)
                    {
                        old.DataBaseField.Copy(property.DataBaseField);
                    }
                    if(!old.IsActive)
                    {
                        old.IsDelete = false;
                        old.IsDiscard = false;
                    }
                }
                else
                {
                    Model.Entity.Add(property);
                    if (property.DataBaseField == null)
                    {
                        property.DataBaseField = new Config.V2021.DataBaseFieldConfig
                        {
                            Property = property
                        };
                        Model.Entity.DataTable.Add(property.DataBaseField);
                        property.DataBaseField.Copy(property);
                    }
                    else
                    {
                        Model.Entity.DataTable.Add(property.DataBaseField);
                    }
                }
            }
            var window = (Window)View;
            window.Tag = Model.Entity;

            window.DialogResult = true;
        }

    }
}