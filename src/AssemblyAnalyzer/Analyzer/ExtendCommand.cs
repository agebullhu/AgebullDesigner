/*using System.Windows;
using Agebull.EntityModel.Designer;
using Agebull.EntityModel.Designer.View;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder.Upgrade
{
    /// <summary>
    /// 自升级代码
    /// </summary>

    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal class ExtendCommand : DesignCommondBase
    {
        #region 注册

        /// <summary>
        /// 注册代码
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            MomentCoder.RegisteCoder("自升级代码","配置类升级", cfg => NewConfigDefault());
        }
        #endregion

        /// <summary>
        ///     C++代码分析
        /// </summary>
        /// <returns></returns>
        internal void CppRefactor()
        {
            if (Model.Context.SelectProject == null)
            {
                MessageBox.Show("选一个项目吧");
                return;
            }
            CommandIoc.CppRefactorCommand(Model.Context.SelectProject);
        }



        /// <summary>
        ///    API代码分析
        /// </summary>
        /// <returns></returns>
        internal void ApiRefactor()
        {
            CommandIoc.ApiRefactorCommand();
            Model.Tree.SelectItem.ReShow();
            Model.Tree.SelectItem.IsExpanded = true;
        }


        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        internal void AddNotify()
        {
            CommandIoc.AddNotifyCommand();
            Model.Tree.SelectItem.ReShow();
            Model.Tree.SelectItem.IsExpanded = true;
        }
        /// <summary>
        ///     C++代码分析
        /// </summary>
        /// <returns></returns>
        internal void NotifyRefactor()
        {
            CommandIoc.NotifyRefactorCommand();
            Model.Tree.SelectItem.ReShow();
            Model.Tree.SelectItem.IsExpanded = true;
        }
        #region C++代码分析


        /// <summary>
        ///     C++代码分析
        /// </summary>
        /// <returns></returns>
        private static void CppRefactor(ProjectConfig project)
        {
            var dlg = new CppRefactorWindow();
            var ctx = (CppRefactorViewModel)dlg.DataContext;
            ctx.Model.Project = project;
            dlg.ShowDialog();
        }



        /// <summary>
        ///    API代码分析
        /// </summary>
        /// <returns></returns>
        private static void ApiRefactor()
        {
            var win = new ApiRefactorWindow();
            var vm = (ApiRefactorViewModel)win.DataContext;
            vm.Model.Solution = SolutionConfig.Current;
            win.Show();
        }

        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        private static void AddNotify()
        {
            var win = new NotifyRefactorWindow();
            var vm = (NotifyRefactorViewModel)win.DataContext;
            vm.Model.Solution = SolutionConfig.Current;
            win.Show();
        }
        /// <summary>
        ///     C++代码分析
        /// </summary>
        /// <returns></returns>
        private static void NotifyRefactor()
        {
            var win = new NotifyRefactorWindow();
            var vm = (NotifyRefactorViewModel)win.DataContext;
            vm.Model.Solution = SolutionConfig.Current;
            win.Show();
        }

        #endregion
    }

}*/