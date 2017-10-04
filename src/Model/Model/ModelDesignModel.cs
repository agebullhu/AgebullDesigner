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

        #region ��������

        /// <summary>
        ///     ����
        /// </summary>
        public ModelDesignModel()
        {
            Catalog = "ģ�����";
        } 

        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="commands"></param>
        protected override void CreateCommands(List<CommandItem> commands)
        {
            commands.Add(new CommandItem
            {
                Command = new DelegateCommand(DoMomentCode),
                Name = "���ɴ���Ƭ��",
                Image = Application.Current.Resources["cpp"] as ImageSource
            });

            commands.Add(new CommandItem
            {
                Command = new DelegateCommand(CopyCode),
                Name = "���ƴ���",
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

        #region ���β˵�
        /// <summary>
        /// ������������
        /// </summary>
        public TreeRoot CodeTreeRoot { get; } = new TreeRoot();


        /// <summary>
        /// ��ʼ��
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
        /// ѡ�еĴ���Ƭ�϶���ķ�����
        /// </summary>
        private Func<ConfigBase, string> SelectCodeModel;

        #endregion
        #region ����Ƭ��
        /// <summary>
        /// ���ƴ���
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
                MessageBox.Show("��Ϊ��" + e.Message + "��δ�ܸ���", "���ƴ���");
            }
        }

        /// <summary>
        /// ���ɵĴ���Ƭ��
        /// </summary>
        private string _extendCode;

        /// <summary>
        /// ���ɵĴ���Ƭ��
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
                MessageBox.Show("��ѡ��һ�����ɷ���", "���ɴ���Ƭ��");
                return;
            }
            try
            {
                ExtendCode = SelectCodeModel(Model.Context.SelectConfig);
            }
            catch (Exception e)
            {
                MessageBox.Show("��Ϊ��" + e.Message + "��δ�����ɶ�Ӧ�Ĵ���Ƭ��", "���ɴ���Ƭ��");
            }
        }
        #endregion
    }
}