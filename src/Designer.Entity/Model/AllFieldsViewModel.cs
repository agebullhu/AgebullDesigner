// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 配置:CodeRefactor-Agebull.CodeRefactor.CodeAnalyze.Application
// 建立:2014-11-20
// 修改:2014-11-29
// *****************************************************/

#region 引用

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using Agebull.Common;
using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.RobotCoder;

#endregion

namespace Agebull.EntityModel.Designer
{
    internal class AllFieldsViewModel : ExtendViewModelBase<EntityDesignModelEx>
    {
        /// <summary>
        /// 主面板
        /// </summary>
        public override FrameworkElement Body { get; } = new AllEntityFieldsControl();
    }

}
