using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.Designer
{
    public sealed class EnumEditModel : DesignModelBase
    {

        #region ��ƶ���

        protected override void DoInitialize()
        {
            Config = Context.SelectConfig as EnumConfig;
            SyncSelect();
            base.DoInitialize();
            Context.PropertyChanged += Context_PropertyChanged;
        }
        private void Context_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(Context.SelectConfig))
                return;
            Config = Context.SelectConfig as EnumConfig;
            SyncSelect();
        }

        void SyncSelect()
        {
            if (Config == null)
            {
                _fields = "";
                Items = new ObservableCollection<EnumItem>();
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                if (Config.Items == null)
                    Config.Items = new ObservableCollection<EnumItem>();
                Items = Config.Items;
                foreach (var item in Items.OrderBy(p=>p.Number))
                {
                    if (item.Caption == item.Description)
                        sb.AppendFormat("{0}\t{1}\t{2}", item.Value, item.Name, item.Caption);
                    else
                        sb.AppendFormat("{0}\t{1}\t{2}\t{3}", item.Value, item.Name, item.Caption, item.Description);
                    sb.AppendLine();
                }
                _fields = sb.ToString();
            }
            RaisePropertyChanged(() => Config);
            RaisePropertyChanged(() => Items);
            RaisePropertyChanged(() => Fields);
        }

        /// <summary>
        /// ���ɵı�����
        /// </summary>
        public EnumConfig Config
        {
            get;
            set;
        }

        /// <summary>
        /// ���ɵı�����
        /// </summary>
        public ObservableCollection<EnumItem> Items { get; private set; }


        private string _fields;
        /// <summary>
        ///     ��ǰ�ļ���
        /// </summary>
        public string Fields
        {
            get => _fields;
            set
            {
                if (_fields == value)
                    return;
                _fields = value;
                RaisePropertyChanged(() => Fields);
            }
        }
        #endregion

        #region ��չ����

        public void DoCheckFieldes()
        {
            if (Config == null || string.IsNullOrWhiteSpace(Fields))
                return;

            StringBuilder code = new StringBuilder();
            var columns = new List<EnumItem>();
            string[] lines = Fields.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            bool error = false;
            foreach (string l in lines)
            {
                if (string.IsNullOrEmpty(l))
                    continue;
                var  line = l.Trim().Split('>')[0];
                code.Append(line);
                string[] words = line.Split(new[] { ' ', '\t', '-' }, StringSplitOptions.RemoveEmptyEntries);
                if (words.Length < 2)
                {
                    code.AppendLine(" ! ���Ȳ����Խ���");
                    error = true;
                    continue;
                }
                if(!int.TryParse(words[0],out var _))
                {
                    code.AppendLine(" ! ��һ�������޷�תΪ����");
                }
                if (!char.IsLetter(words[1][0]))
                {
                    code.AppendLine(" ! �ڶ�����������ĸ������ĸ(�����б�֤�����淶)");
                    error = true;
                }
                code.AppendLine();
                /*
                 �ı�˵��:
                 * 1 ÿ��Ϊһ������
                 * 2 ÿ�������ÿո�,���ŷֿ�
                 * 3 ��һ������ ��������; �ڶ������� ��������;���������� ˵���ı�
                 */
                columns.Add(new EnumItem
                {
                    Value = words[0],
                    Name = words[1],
                    Caption = words.Length > 2 ? words[2] : words[1],
                    Description = words.Length > 3 ? words[3] : null
                });
            }

            Fields = code.ToString();
            OnPropertyChanged(nameof(Fields));
            if (error)
            {
                MessageBox.Show("��������");
                return;
            }
            MessageBox.Show("����ɹ�");
            Items.Clear();
            foreach (var col in columns)
            {
                Items.Add(col);
            }
            RaisePropertyChanged(() => Items);
        }

        #endregion

    }
}