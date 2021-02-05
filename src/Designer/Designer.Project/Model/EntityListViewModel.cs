// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 工程:CodeRefactor-Agebull.CodeRefactor.CodeAnalyze.Application
// 建立:2014-11-20
// 修改:2014-11-27
// *****************************************************/

#region 引用

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;

#endregion

namespace Agebull.EntityModel.Designer
{
    public sealed class EntityListViewModel : ExtendViewModelBase<EntityListModel>
    {
    }

    public sealed class EntityListModel : DesignModelBase
    {
        #region 设计对象

        private bool _selectAll;
        /// <summary>
        /// 生成的表格对象
        /// </summary>
        public bool SelectAll
        {
            get => _selectAll;
            set
            {
                if (_selectAll == value)
                    return;
                _selectAll = value;
                RaisePropertyChanged(nameof(SelectAll));
                Context.SelectProject?.Entities.Foreach(p => p.Option.IsSelect = value);
            }
        }

        #endregion

        public EntityListModel()
        {
            CommondFilter = cmd => true;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        protected override void DoInitialize()
        {
            DesignModelType = typeof(EntityConfig);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        protected override bool FilterCommand(CommandItemBase command)
        {
            command.OnPrepare = OnCommandExec;
            return base.FilterCommand(command);
        }

        internal bool OnCommandExec(CommandItemBase cmd)
        {
            if (Context.SelectProject == null)
                return true;
            foreach (var entity in Context.SelectProject.Entities.ToArray())
            {
                if (entity.Option.IsSelect)
                {
                    cmd.Execute(entity);
                }
            }
            return false;
        }
    }
}