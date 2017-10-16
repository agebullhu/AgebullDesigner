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

namespace Agebull.EntityModel.Designer
{
    internal class FieldsViewModel : ExtendViewModelBase<FieldsModel>
    {
        public FieldsViewModel()
        {
            Catalog = "基本";
        }
        /// <summary>
        /// 主面板
        /// </summary>
        public override FrameworkElement Body { get; } = new FieldsPanel();
    }

    internal class FieldsModel : EntityDesignModelEx
    {
        #region 操作命令

        private List<CommandItem> _exCommands;
        public List<CommandItem> ExCommands => _exCommands ?? (_exCommands = new List<CommandItem>
        {
            new CommandItem
            {
                Command = new AsyncCommand<string, List<PropertyConfig>>
                    (CheckFieldesPrepare, DoCheckFieldes, CheckFieldesEnd)
                    {
                        Detect = Model
                    },
                Name = "分析文本",
                Image = Application.Current.Resources["tree_Assembly"] as ImageSource
            },
            new CommandItem
            {
                Command = new AsyncCommand<string, string>
                    (Format1Prepare, DoFormat1, Format1End)
                    {
                        Detect = Model
                    },
                Name = "格式化(类型 名称)",
                Image = Application.Current.Resources["tree_Assembly"] as ImageSource
            },
            new CommandItem
            {
                Command = new AsyncCommand<string, string>
                    (Format2Prepare, DoFormat2, Format2End)
                    {
                        Detect = Model
                    },
                Name = "格式化(名称 类型 标题)",
                Image = Application.Current.Resources["tree_Assembly"] as ImageSource
            },
            new CommandItem
            {
                Command = new AsyncCommand<string, string>
                    (Format3Prepare, DoFormat3, Format3End)
                    {
                        Detect = Model
                    },
                Name = "格式化(MySql数据库)",
                Image = Application.Current.Resources["tree_Assembly"] as ImageSource
            },
            new CommandItem
            {
                Command = new AsyncCommand<string, string>
                    (FormatSqlServerPrepare, DoFormatSqlServer, FormatSqlServerEnd)
                    {
                        Detect = Model
                    },
                Name = "格式化(SqlServer数据库)",
                Image = Application.Current.Resources["tree_Assembly"] as ImageSource
            },
            new CommandItem
            {
                Command = new DelegateCommand(DoClose),
                Name = "接收字段",
                Image = Application.Current.Resources["tree_Assembly"] as ImageSource
            }
        });
        private void DoClose()
        {
            Context.SelectEntity.Properties.AddRange(Columns);
        }
        #endregion

        #region 设计对象

        /// <summary>
        ///     当前选择
        /// </summary>
        ObservableCollection<PropertyConfig> Columns { get; } = new ConfigCollection<PropertyConfig>();


        private string _fields = "******";
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

        internal bool Format1Prepare(string arg, Action<string> setArg)
        {
            return !string.IsNullOrWhiteSpace(Fields);
        }


        public string DoFormat1(string arg)
        {
            string[] lines = Fields.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            StringBuilder code = new StringBuilder();
            foreach (string line in lines)
            {
                if (string.IsNullOrEmpty(line))
                    continue;
                string[] words = line.Trim().Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                StringBuilder sb = new StringBuilder();
                for (int i = 1; i < words.Length; i++)
                {
                    sb.Append(words[i].Trim(CoderBase.NoneLanguageChar).ToUWord());
                }
                var name = sb.ToString().Trim(CoderBase.NoneLanguageChar);
                if (name[0] >= '0' && name[0] <= '9')
                    name = "m" + name;
                StringBuilder sb2 = new StringBuilder();
                for (int i = 1; i < words.Length; i++)
                {
                    sb2.Append(words[i].ToUWord());
                    sb2.Append(' ');
                }
                code.Append($"{name},{words[0]},{sb2},{BaiduFanYi.FanYi(sb2.ToString())}\r\n");

            }
            return code.ToString();
        }


        internal void Format1End(CommandStatus status, Exception ex, string code)
        {
            if (status != CommandStatus.Succeed)
                return;
            Fields = code;
        }

        #endregion
        #region 扩展代码

        internal bool Format2Prepare(string arg, Action<string> setArg)
        {
            return !string.IsNullOrWhiteSpace(Fields);
        }


        public string DoFormat2(string arg)
        {
            string[] lines = Fields.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            StringBuilder code = new StringBuilder();
            foreach (string line in lines)
            {
                if (string.IsNullOrEmpty(line))
                    continue;
                string[] words = line.Trim().Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                code.AppendLine(words.LinkToString(","));
            }
            return code.ToString();
        }


        internal void Format2End(CommandStatus status, Exception ex, string code)
        {
            if (status != CommandStatus.Succeed)
                return;
            Fields = code;
        }

        #endregion

        #region SQLSERVER格式

        internal bool FormatSqlServerPrepare(string arg, Action<string> setArg)
        {
            return !string.IsNullOrWhiteSpace(Fields);
        }


        public string DoFormatSqlServer(string arg)
        {
            string[] lines = Fields.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            StringBuilder code = new StringBuilder();
            foreach (string line in lines)
            {
                if (string.IsNullOrEmpty(line))
                    continue;
                string[] words = line.Trim().Split(new[] { ' ', '\t', '[', ']', '`', ',' }, StringSplitOptions.RemoveEmptyEntries);
                code.Append(words[0]);
                switch (words[1].ToLower())
                {
                    case "int":
                        code.Append(",i");
                        break;
                    case "datetime":
                        code.Append(",DateTime");
                        break;
                    case "bit":
                        code.Append(",bool");
                        break;
                    case "bigint":
                        code.Append(",long");
                        break;
                    case "decimal":
                    case "numbic":
                        code.Append(",decimal");
                        break;
                    case "text":
                        code.Append(",ls");
                        break;
                    default://
                        code.Append(",s");
                        break;
                }
                if (words.Length > 2 && words[2][0] == '(')
                {
                    var len = words[2].Trim('(', ')');
                    code.Append($"-{len}");
                }
                else
                {
                    code.Append("-#");
                }
                var def = words.FirstOrDefault(p => p.IndexOf("default", StringComparison.OrdinalIgnoreCase) >= 0);
                var defs = def?.Split(new[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
                if (defs?.Length > 1)
                {
                    code.Append($"-{defs[1]}");
                }
                code.Append(",");
                var desc = words.FirstOrDefault(p => p.IndexOf("--", StringComparison.OrdinalIgnoreCase) >= 0);
                code.Append(desc?.Trim('-') ?? words[0]);
                code.AppendLine();
            }
            return code.ToString();
        }


        internal void FormatSqlServerEnd(CommandStatus status, Exception ex, string code)
        {
            if (status != CommandStatus.Succeed)
                return;
            Fields = code;
        }

        #endregion
        #region 扩展代码

        internal bool Format3Prepare(string arg, Action<string> setArg)
        {
            return !string.IsNullOrWhiteSpace(Fields);
        }


        public string DoFormat3(string arg)
        {
            string[] lines = Fields.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            StringBuilder code = new StringBuilder();
            foreach (string line in lines)
            {
                if (string.IsNullOrEmpty(line))
                    continue;
                string[] words = line.Trim().Split(new[] { ' ', '\t', '[', ']', '`', ',' }, StringSplitOptions.RemoveEmptyEntries);
                code.Append(words[0]);
                switch (words[1].ToLower())
                {
                    case "int":
                        code.Append(",i,");
                        break;
                    case "datetime":
                        code.Append(",DateTime,");
                        break;
                    case "bit":
                        code.Append(",bool,");
                        break;
                    case "bigint":
                        code.Append(",long,");
                        break;
                    case "decimal":
                    case "numbic":
                        code.Append(",decimal,");
                        break;
                    default:
                        code.Append(",s,");
                        break;
                }
                code.AppendLine(words[0]);
            }
            return code.ToString();
        }


        internal void Format3End(CommandStatus status, Exception ex, string code)
        {
            if (status != CommandStatus.Succeed)
                return;
            Fields = code;
        }

        #endregion
        #region 扩展代码

        internal bool CheckFieldesPrepare(string arg, Action<string> setArg)
        {
            return !string.IsNullOrWhiteSpace(Fields);
        }


        public List<PropertyConfig> DoCheckFieldes(string arg)
        {
            var columns = new List<PropertyConfig>();
            string[] lines = Fields.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            int idx = 0;
            foreach (string line in lines)
            {
                if (string.IsNullOrEmpty(line))
                    continue;
                string[] words = line.Trim().Split(new[] { ',', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                var name = words[0].ToUWord();

                /*
                文本说明:
                * 1 每行为一条数据
                * 2 每个单词用空格,逗号分开
                * 3 第一个单词 代码名称; 第二个单词 数据类型;第三个单词 说明文本
                */
                PropertyConfig column;
                columns.Add(column = new PropertyConfig
                {
                    Index = idx++,
                    IsPrimaryKey = name.Equals("ID", StringComparison.OrdinalIgnoreCase),
                    ColumnName = name,
                    Name = name,
                    CsType = "string",
                    DbType = "nvarchar"
                });
                if (words.Length > 1)
                {
                    CheckType(column, words[1]);
                }
                if (words.Length <= 2)
                    continue;
                column.Caption = words[2];
                column.Description = words.Length < 4 ? words[2] : words.Skip(3).LinkToString(",");
            }
            return columns;
        }


        internal void CheckFieldesEnd(CommandStatus status, Exception ex, List<PropertyConfig> columns)
        {
            if (status != CommandStatus.Succeed)
                return;
            Columns.Clear();
            foreach (PropertyConfig col in columns)
            {
                Columns.Add(col);
            }
        }

        #endregion
        #region 实现方法

        private void CheckType(PropertyConfig column, string type)
        {
            if (type[type.Length - 1] == '?')
            {
                column.Nullable = true;
                type = type.TrimEnd('?');
            }
            var tp = type.TrimEnd('?').ToLower();
            var strs = tp.Split('-');
            switch (strs[0])
            {
                case "t":
                case "s":
                case "string":
                case "nvarchar":
                    column.CsType = "string";
                    column.DbType = "NVARCHAR";
                    break;
                case "ls":
                    column.CsType = "string";
                    column.DbType = "TEXT";
                    column.IsBlob = true;
                    break;
                case "b":
                case "bit":
                case "bool":
                    column.CsType = "bool";
                    column.DbType = "BIT";
                    break;
                case "i":
                case "int":
                    column.CsType = "int";
                    column.DbType = "int";
                    break;
                case "l":
                case "long":
                case "bigint":
                    column.CsType = "long";
                    column.DbType = "BIGINT";
                    break;
                case "d":
                case "decimal":
                case "money":
                case "numeric":
                    column.CsType = "decimal";
                    column.DbType = "decimal";
                    break;
                case "f":
                case "float":
                    column.CsType = "double";
                    column.DbType = "double";
                    break;
                case "p":
                    column.IsPrimaryKey = true;
                    column.CsType = "int";
                    column.DbType = "int";
                    break;
                case "u":
                    column.IsUserId = true;
                    column.CsType = "int";
                    column.DbType = "int";
                    break;
                case "datetime":
                    column.IsUserId = true;
                    column.CsType = "DateTime";
                    column.DbType = "DateTime";
                    break;
                default:
                    column.CsType = tp;
                    column.DbType = tp;
                    break;
            }
            if (strs.Length > 1 && strs[1] != "#")
                column.Datalen = int.Parse(strs[1]);
            if (strs.Length > 2)
                column.Initialization = strs[2];
        }

        #endregion
    }
}