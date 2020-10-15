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

        #region 设计对象

        /// <summary>
        ///     分类
        /// </summary>
        public override void SetContextConfig(object cfg)
        {
            Config = cfg as EnumConfig;
        }
        protected override void DoInitialize()
        {
            Config = Context.SelectConfig as EnumConfig ?? Context.SelectConfig.Friend as EnumConfig;
            SyncSelect();
            base.DoInitialize();
            Context.PropertyChanged += Context_PropertyChanged;
        }
        private void Context_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(Context.SelectConfig))
                return;
            Config = Context.SelectConfig as EnumConfig ?? Context.SelectConfig?.Friend as EnumConfig;
            SyncSelect();
        }

        void SyncSelect()
        {
            if (Config == null)
            {
                _fields = "";
                Items = new NotificationList<EnumItem>();
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                if (Config.Items == null)
                    Config.Items = new NotificationList<EnumItem>();
                Items = Config.Items;
                foreach (var item in Items.OrderBy(p => p.Number))
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
        /// 生成的表格对象
        /// </summary>
        public EnumConfig Config
        {
            get => _config;
            set
            {
                _config = value;
                RaisePropertyChanged(nameof(Config));
            }
        }

        /// <summary>
        /// 生成的表格对象
        /// </summary>
        public NotificationList<EnumItem> Items
        {
            get => _items;
            private set
            {
                _items = value;
                RaisePropertyChanged(nameof(Items));
            }
        }


        private string _fields;
        private EnumConfig _config;
        private NotificationList<EnumItem> _items;

        /// <summary>
        ///     当前文件名
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

        #region 扩展代码

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public void DoFormatCSharp(object arg)
        {
            if (Config == null || string.IsNullOrWhiteSpace(Fields))
                return;

            StringBuilder code = new StringBuilder();
            var columns = new List<EnumItem>();
            var lines = Fields.Split(new[] { '\r', '\n', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            string descript = null, caption = null;
            int next = 0;
            int barket = 0;
            int value = 0;
            foreach (var l in lines)
            {
                if (string.IsNullOrWhiteSpace(l))
                    continue;
                var line = l.Trim().TrimEnd(',');
                if (barket > 0)
                    continue;
                var baseLine = line.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                var words = baseLine[0].Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (words[0][0] == '/')
                {
                    if (words[1] == "<summary>")
                    {
                        caption = null;
                        next = 1;
                        continue;
                    }
                    if (words[1] == "<remark>")
                    {
                        descript = null;
                        next = 2;
                        continue;
                    }
                    if (words[1][0] == '<')
                    {
                        next = 0;
                        continue;
                    }
                    if (next <= 1)
                        caption = words.Skip(1).LinkToString();
                    if (next == 2)
                        descript = words.Skip(1).LinkToString();
                    continue;
                }
                if (!char.IsLetter(line[0]))
                    continue;
                if (baseLine.Length > 1)
                {
                    if (baseLine[1].IndexOf("0X", StringComparison.OrdinalIgnoreCase) == 0 && baseLine[1].Length > 2)
                    {
                        value = Convert.ToInt32(baseLine[1].Substring(2), 16);
                    }
                    else if (int.TryParse(baseLine[1], out var vl))
                    {
                        value = vl;
                    }
                }
                var name = words[words.Length - 1];
                code.Append($"{value}\t{name}");
                if (caption != null)
                    code.Append($"\t{caption}");
                if (descript != null)
                    code.Append($"\t{descript}");
                code.AppendLine();

                columns.Add(new EnumItem
                {
                    Name = name,
                    Value = value.ToString(),
                    Description = descript,
                    Caption = caption
                });
                caption = null;
                descript = null;
                next = 0;
                ++value;
            }

            Fields = code.ToString();
            OnPropertyChanged(nameof(Fields));

            MessageBox.Show("处理成功");
            Items.Clear();
            foreach (var col in columns)
            {
                Items.Add(col);
            }
            RaisePropertyChanged(() => Items);
        }

        public void DoCheckFieldes(object arg)
        {
            if (Config == null || string.IsNullOrWhiteSpace(Fields))
                return;

            StringBuilder code = new StringBuilder();
            var columns = new List<EnumItem>();
            string[] lines = Fields.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            bool error = false;
            foreach (string l in lines)
            {
                if (string.IsNullOrWhiteSpace(l))
                    continue;
                var line = l.Trim().Split('>')[0];
                code.Append(line);
                string[] words = line.Split(new[] { ' ', '\t', '-' }, StringSplitOptions.RemoveEmptyEntries);
                if (words.Length < 2)
                {
                    code.AppendLine(" ! 长度不足以解析");
                    error = true;
                    continue;
                }
                if (!int.TryParse(words[0], out var _))
                {
                    code.AppendLine(" ! 第一个单词无法转为数字");
                }
                if (!char.IsLetter(words[1][0]))
                {
                    code.AppendLine(" ! 第二个单词首字母不是字母(请自行保证命名规范)");
                    error = true;
                }
                code.AppendLine();
                /*
                 文本说明:
                 * 1 每行为一条数据
                 * 2 每个单词用空格,逗号分开
                 * 3 第一个单词 代码名称; 第二个单词 数据类型;第三个单词 说明文本
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
                MessageBox.Show("发生错误");
                return;
            }
            MessageBox.Show("处理成功");
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