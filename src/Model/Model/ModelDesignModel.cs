using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Agebull.Common.DataModel;
using Agebull.Common.SimpleDesign;
using Gboxt.Common.DataAccess.Schemas;
using Gboxt.Common.WpfMvvmBase.Commands;

namespace Agebull.CodeRefactor.SolutionManager
{
    public class ModelDesignModel : DesignModelBase
    {

        #region 操作命令

        /// <summary>
        ///     分类
        /// </summary>
        public ModelDesignModel()
        {
            Catalog = "模型设计";
        } 

        /// <summary>
        /// 生成命令对象
        /// </summary>
        /// <param name="commands"></param>
        protected override void CreateCommands(List<CommandItem> commands)
        {
            commands.Add(new CommandItem
            {
                Command = new DelegateCommand(DoMomentCode),
                Name = "生成代码片断",
                Image = Application.Current.Resources["cpp"] as ImageSource
            });

            commands.Add(new CommandItem
            {
                Command = new DelegateCommand(CopyCode),
                Name = "复制代码",
                Image = Application.Current.Resources["img_file"] as ImageSource
            });
            foreach (var builder in ProjectBuilder.Builders.Values)
            {
                var b = builder();
                commands.Add(new ProjectCodeCommand(builder)
                {
                    Name = b.Caption,
                    IconName = b.Icon,
                    NoButton = true
                }.ToCommand(null));
            }
        }

        #endregion

        #region 树形菜单
        /// <summary>
        /// 代码命令树根
        /// </summary>
        public TreeRoot CodeTreeRoot { get; } = new TreeRoot();


        /// <summary>
        /// 初始化
        /// </summary>
        protected override void DoInitialize()
        {
            base.DoInitialize();

            foreach (var clasf in MomentCoder.coders)
            {
                if (clasf.Value == null || clasf.Value.Count == 0)
                    continue;
                TreeItemBase parent = new TreeItem(clasf.Key)
                {
                    IsExpanded = false,
                    SoruceTypeIcon = Application.Current.Resources["tree_Folder"] as BitmapImage
                };
                CodeTreeRoot.Items.Add((TreeItem)parent);
                foreach (var item in clasf.Value)
                {
                    parent.Items.Add(new TreeItem<Func<ConfigBase, string>>(item.Value)
                    {
                        Header = item.Key,
                        SoruceTypeIcon = Application.Current.Resources["img_code"] as BitmapImage
                    });
                }
            }
            CodeTreeRoot.SelectItemChanged += OnTreeSelectItemChanged;
        }
        private void OnTreeSelectItemChanged(object sender, EventArgs e)
        {
            var value = sender as TreeItem<Func<ConfigBase, string>>;
            SelectCodeModel = value?.Model;
            if (SelectCodeModel != null)
            {
                DoMomentCode();
            }
        }


        /// <summary>
        /// 选中的代码片断对象的方法体
        /// </summary>
        private Func<ConfigBase, string> SelectCodeModel;

        #endregion
        #region 代码片断
        /// <summary>
        /// 复制代码
        /// </summary>
        private void CopyCode()
        {
            if (string.IsNullOrWhiteSpace(ExtendCode)) return;
            try
            {
                Clipboard.SetText(ExtendCode);
            }
            catch (Exception e)
            {
                MessageBox.Show("因为【" + e.Message + "】未能复制", "复制代码");
            }
        }

        /// <summary>
        /// 生成的代码片断
        /// </summary>
        private string _extendCode;

        /// <summary>
        /// 生成的代码片断
        /// </summary>
        public string ExtendCode
        {
            get { return _extendCode; }
            set
            {
                if (Equals(_extendCode, value))
                    return;
                _extendCode = value;
                RaisePropertyChanged(() => ExtendCode);
                Context.NowJob = DesignContext.JobExtendCode;
            }
        }

        private void DoMomentCode()
        {
            if (SelectCodeModel == null)
            {
                MessageBox.Show("请选择一个生成方法", "生成代码片断");
                return;
            }
            try
            {
                ExtendCode = SelectCodeModel(Model.Context.SelectConfig);
            }
            catch (Exception e)
            {
                MessageBox.Show("因为【" + e.Message + "】未能生成对应的代码片断", "生成代码片断");
            }
        }
        #endregion
    }
}