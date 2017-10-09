// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 工程:CodeRefactor-Agebull.CodeRefactor.CodeAnalyze.Application
// 建立:2014-11-20
// 修改:2014-11-27
// *****************************************************/

#region 引用

using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;

#endregion

namespace Agebull.EntityModel.Designer
{
    public sealed class NewEntityViewModel : ViewModelBase<NewEntityModel>
    {
        private List<CommandItem> _exCommands;
        private bool _isNew;

        public bool IsNew
        {
            get { return _isNew; }
            set
            {
                _isNew = value;
                RaisePropertyChanged(nameof(IsNew));
            }
        }

        public IEnumerable<CommandItem> ExCommands => _exCommands ?? (_exCommands = new List<CommandItem>
        {
            new CommandItem
            {
                Command = new AsyncCommand<string, List<PropertyConfig>>
                    (Model.CheckFieldesPrepare, Model.DoCheckFieldes, Model.CheckFieldesEnd)
                {
                    Detect = Model
                },
                Name = "分析文本",
                Image = Application.Current.Resources["tree_Assembly"] as ImageSource
            },
            new CommandItem
            {
                Command = new AsyncCommand<string, string>
                    (Model.Format1Prepare, Model.DoFormat1, Model.Format1End)
                {
                    Detect = Model
                },
                Name = "格式化(类型 名称)",
                Image = Application.Current.Resources["tree_Assembly"] as ImageSource
            },
            new CommandItem
            {
                Command = new AsyncCommand<string, string>
                    (Model.Format2Prepare, Model.DoFormat2, Model.Format2End)
                    {
                        Detect = Model
                    },
                Name = "格式化(名称 类型 标题)",
                Image = Application.Current.Resources["tree_Assembly"] as ImageSource
            },
            new CommandItem
            {
                Command = new AsyncCommand<string, string>
                    (Model.Format3Prepare, Model.DoFormat3, Model.Format3End)
                    {
                        Detect = Model
                    },
                Name = "格式化(MySql数据库)",
                Image = Application.Current.Resources["tree_Assembly"] as ImageSource
            },
            new CommandItem
            {
                Command = new AsyncCommand<string, string>
                    (Model.FormatSqlServerPrepare, Model.DoFormatSqlServer, Model.FormatSqlServerEnd)
                    {
                        Detect = Model
                    },
                Name = "格式化(SqlServer数据库)",
                Image = Application.Current.Resources["tree_Assembly"] as ImageSource
            },
            new CommandItem
            {
                Command = new DelegateCommand(DoClose),
                Name = "完成",
                Image = Application.Current.Resources["tree_Assembly"] as ImageSource
            }
        });

        private void DoClose()
        {
            if (IsNew && (string.IsNullOrWhiteSpace(Model.EntityName) || Model.Entity.Properties.Count == 0))
            {
                MessageBox.Show("实体名称为空或没有字段,请检查");
                return;
            }
            var window = (Window)View;
            window.Tag = Model.Entity;
            window.DialogResult = true;
        }
    }
}