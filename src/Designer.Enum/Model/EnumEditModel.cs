using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;

namespace Agebull.EntityModel.Designer
{
    public sealed class EnumEditModel : TraceModelBase
    {
        protected override void DoInitialize()
        {
            base.DoInitialize();
            StringBuilder sb = new StringBuilder();
            if (Config.Items == null)
                Config.Items = new ObservableCollection<EnumItem>();
            foreach (var item in Items)
            {
                sb.AppendFormat("{0} {1} {2} {3}", item.Value, item.Name, item.Caption, item.Description);
                sb.AppendLine();
            }
            Fields = sb.ToString();
        }

        #region 设计对象

        private EnumConfig _config;
        /// <summary>
        /// 生成的表格对象
        /// </summary>
        public EnumConfig Config
        {
            get { return _config; }
            set
            {
                _config = value;
                RaisePropertyChanged(() => Config);
                RaisePropertyChanged(() => Items);
            }
        }


        /// <summary>
        /// 生成的表格对象
        /// </summary>
        public ObservableCollection<EnumItem> Items => _config == null ? null : Config.Items;


        private string _fields;
        /// <summary>
        ///     当前文件名
        /// </summary>
        public string Fields
        {
            get { return _fields; }
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

        internal bool CheckFieldesPrepare(string arg, Action<string> setArg)
        {
            return !string.IsNullOrWhiteSpace(Fields);
        }


        public List<EnumItem> DoCheckFieldes(string arg)
        {
            var columns = new List<EnumItem>();
            string[] lines = Fields.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in lines)
            {
                if (string.IsNullOrEmpty(line))
                    continue;
                string[] words = line.Trim().Split(new[] { ' ', '\t', '-' }, StringSplitOptions.RemoveEmptyEntries);
                if (words.Length < 2)
                    continue;
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
            return columns;
        }

        internal void CheckFieldesEnd(CommandStatus status, Exception ex, List<EnumItem> columns)
        {
            if (status != CommandStatus.Succeed)
                return;
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