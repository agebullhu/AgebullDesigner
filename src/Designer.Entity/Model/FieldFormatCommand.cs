using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.RobotCoder;

namespace Agebull.EntityModel.Designer
{
    public class FieldFormatCommand
    {
        /// <summary>
        /// 字段文本内容
        /// </summary>
        public string Fields { get; set; }

        /// <summary>
        /// 生成的表格对象
        /// </summary>
        public EntityConfig Entity
        {
            get;
            set;
        }


        #region 规整文本(CSharp 类型 名称)

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public string DoFormatCSharp(string arg)
        {
            if (string.IsNullOrWhiteSpace(Fields))
                return null;

            var lines = Fields.Replace("{", "\n{").Replace("}", "\n}")
                .Split(new[] { '\r', '\n', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            string descript = null, caption = null;
            int next = 0;
            int barket = 0;
            var code = new StringBuilder();
            foreach (var l in lines)
            {
                if (string.IsNullOrEmpty(l))
                    continue;
                var line = l.Trim().TrimEnd(';');
                if (line[0] == '{')
                {
                    barket++;
                    continue;
                }
                if (line[0] != '/' && line[line.Length - 1] == '}')
                {
                    barket--;
                    continue;
                }
                if (barket > 0)
                    continue;
                var baseLine = line.Split(new[] { '{', '=', ';', ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (baseLine[0].Contains('('))
                    continue;
                var words = baseLine[0].Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (words.Length == 1)
                    continue;
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
                var name = words[words.Length - 1];
                var type = words[words.Length - 2];
                code.Append($"{name},{type}");
                if (caption != null)
                    code.Append($",{caption}");
                if (descript != null)
                    code.Append($",{descript}");
                code.AppendLine();
                caption = null;
                descript = null;
                next = 0;
            }

            return code.ToString();
        }

        #endregion

        #region 规整文本(名称 是否必填 类型 标题)

        public string DoFormatDocument(string arg)
        {
            var lines = Fields.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            var code = new StringBuilder();
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                    continue;
                var words = line.Trim().Split(new[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (words.Length < 3)
                {
                    code.Append(line);
                    continue;
                }
                code.AppendLine();
                var list = words.Select(p => p.Trim()).Where(p => !string.IsNullOrEmpty(p)).ToList();
                if (words[1][0] > 128)
                {
                    list.RemoveAt(1);
                    if (words[1][0] == '必')
                        list[1] += '!';
                }
                code.Append(list.LinkToString(","));
            }

            return code.ToString();
        }

        #endregion

        #region 规整文本(名称 类型 标题)

        public string DoFormat2(string arg)
        {
            var lines = Fields.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            var code = new StringBuilder();
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                    continue;
                var words = line.Trim().Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                code.AppendLine(words.LinkToString(","));
            }

            return code.ToString();
        }

        #endregion

        #region SQLSERVER  字段定义样式


        public string DoFormatSqlServer(string arg)
        {
            var lines = Fields.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            var code = new StringBuilder();
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                    continue;
                var words = line.Trim().Split(new[] { ' ', '\t', '[', ']', '`', ',' },
                    StringSplitOptions.RemoveEmptyEntries);
                if (words.Length == 1)
                    continue;
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
                    case "double":
                    case "float":
                        code.Append(",decimal");
                        break;
                    case "text":
                        code.Append(",ls");
                        break;
                    default: //
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
                if (defs?.Length > 1) code.Append($"-{defs[1]}");
                code.Append(",");
                var desc = words.FirstOrDefault(p => p.IndexOf("--", StringComparison.OrdinalIgnoreCase) >= 0);
                code.Append(desc?.Trim('-') ?? words[0]);
                code.AppendLine();
            }

            return code.ToString();
        }

        #endregion

        #region MySql 字段定义样式


        public string DoFormatMySql(string arg)
        {
            var lines = Fields.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            var code = new StringBuilder();
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                    continue;
                var words = line.Trim(' ', '\t', ',').Replace("(", " (").Split(new[] { ' ', '\t', '[', ']', '`', ',' },
                    StringSplitOptions.RemoveEmptyEntries);
                if (words.Length == 1)
                    continue;
                code.Append(words[0]);
                switch (words[1].ToLower())
                {
                    case "int":
                    case "bool":
                    case "smallint":
                        code.Append(",i");
                        break;
                    case "date":
                    case "datetime":
                        code.Append(",DateTime");
                        break;
                    case "bit":
                        code.Append(",bool");
                        break;
                    case "bigint":
                    case "largeint":
                        code.Append(",long");
                        break;
                    case "decimal":
                    case "double":
                    case "float":
                    case "numbic":
                        code.Append(",decimal");
                        break;
                    case "text":
                        code.Append(",ls");
                        break;
                    default:
                        code.Append(",s");
                        break;
                }
                if (words.Length > 2 && words[2][0] == '(')
                {
                    var len = words[2].Trim('(', ')');
                    code.Append($"-{len}");
                }
                for (int i = 2; i < words.Length; i++)
                {
                    if (string.Equals(words[i], "COMMENT", StringComparison.OrdinalIgnoreCase) && i + 1 < words.Length)
                    {
                        code.Append(",");
                        code.Append(words[i + 1].Trim('\''));
                    }
                }
                for (int i = 2; i < words.Length; i++)
                {
                    if (string.Equals(words[i], "NULL", StringComparison.OrdinalIgnoreCase) && string.Equals(words[i - 1], "NOT", StringComparison.OrdinalIgnoreCase))
                    {
                        code.Append(",#");
                    }
                }
                for (int i = 2; i < words.Length; i++)
                {
                    if (string.Equals(words[i], "AUTO_INCREMENT", StringComparison.OrdinalIgnoreCase))
                    {
                        code.Append(",@");
                    }
                }
                code.AppendLine();
            }

            return code.ToString();
        }


        #endregion

        #region 分析文本

        public List<PropertyConfig> DoCheckFieldes(string arg)
        {
            var columns = new List<PropertyConfig>();
            var lines = Fields.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var idx = Entity?.MaxIdentity ?? 1;
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                    continue;
                var words = line.Trim().Split(new[] { ',', '，' }, StringSplitOptions.RemoveEmptyEntries);

                var name = GlobalConfig.ToLinkWordName(words[0], "", true);

                var dbName = GlobalConfig.ToLinkWordName(words[0], "_",false);

                /*
                文本说明:
                * 1 每行为一条数据
                * 2 每个单词用逗号分开
                * 3 第一个单词 代码名称; 第二个单词 数据类型;第三个单词 说明文本
                */
                PropertyConfig column = new PropertyConfig
                {
                    Name = name,
                    IsPrimaryKey = name.Equals("ID", StringComparison.OrdinalIgnoreCase),
                    DbFieldName = dbName,
                    JsonName = words[0],
                    ApiArgumentName = words[0],
                    Datalen = 200,
                    DataType = "String",
                    CsType = "string",
                    DbType = "NVARCHAR",
                    Option =
                    {
                        Identity = idx++,
                        Index = idx++
                    }
                };
                if (words.Length > 1)
                    CsharpHelper.CheckType(column, words[1]);
                if (words.Length > 2 && words[2] != "@" && words[2] != "#")
                    column.Caption = words[2];
                if (words.Length > 3 && words[3] != "@" && words[3] != "#")
                    column.Description = words.Length < 4 ? null : words.Skip(3).LinkToString(",").TrimEnd('@', '#');
                var old = columns.FirstOrDefault(p => p != null && p.Name == name);
                if (old != null)
                {
                    columns.Remove(old);
                }

                DataTypeHelper.CsDataType(column);
                columns.Add(column);
            }

            return columns;
        }


        #endregion
    }
}