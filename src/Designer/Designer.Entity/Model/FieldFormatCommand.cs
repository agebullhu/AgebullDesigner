using Agebull.EntityModel.Common;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.RobotCoder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agebull.EntityModel.Designer
{
    public class FieldFormatCommand
    {
        private string fields = @"Id,l,主键
Name,s,名称


Memo,s,备注";

        /// <summary>
        /// 字段文本内容
        /// </summary>
        public string Fields { get => fields; set => fields = value; }

        /// <summary>
        /// 生成的表格对象
        /// </summary>
        private EntityConfig Entity
        {
            get;
            set;
        }


        #region 规整文本(CSharp 类型 名称)
        static readonly List<string> CSharpKeyWords = new() { "public", "private", "protected", "internal", "readonly", "readonly" };
        List<NameValue> ToCSharpWord(string code)
        {
            var words = new List<NameValue>();
            var builder = new StringBuilder();

            string type = "word";

            void AddWord()
            {
                if (builder.Length > 0)
                {
                    words.Add(new NameValue(type, builder.ToString()));
                    builder.Clear();
                }
            }

            int inCodes = 0;
            char pre = '\0';
            int rem = 0;
            foreach (var ch in code)
            {
                #region 注释
                if (rem != 0)
                {
                    if (rem == 1 && ch == '\n')
                    {
                        AddWord();
                        rem = 0;
                        pre = '\0';
                        type = "word";
                        continue;
                    }
                    if (rem == 2 && pre == '*' && ch == '/')
                    {
                        AddWord();
                        rem = 0;
                        pre = '\0';
                        type = "word";
                        continue;
                    }
                    if (rem == 3 && ch == ']')
                    {
                        AddWord();
                        rem = 0;
                        pre = '\0';
                        type = "word";
                        continue;
                    }

                    if (ch == '>')
                    {
                        AddWord();
                        type = "rem";
                        pre = ch;
                        continue;
                    }
                    else if (ch == '<')
                    {
                        AddWord();
                        type = "rem-start";
                        pre = ch;
                        continue;
                    }
                    else if (pre == '<' && ch == '/')
                    {
                        AddWord();
                        type = "rem-end";
                        pre = ch;
                        continue;
                    }
                    if (ch != '/' || builder.Length > 0)//注释除开始的/其它都要
                        builder.Append(ch);
                    continue;
                }
                if (rem == 0 && pre == '/' && ch == '/')
                {
                    type = "rem";
                    rem = 1;
                    pre = '\0';
                    continue;
                }
                if (rem == 0 && pre == '/' && ch == '*')
                {
                    type = "rem";
                    rem = 2;
                    pre = '\0';
                    continue;
                }
                if (rem == 0 && ch == '[')
                {
                    type = "attr";
                    rem = 3;
                    pre = '\0';
                    continue;
                }
                #endregion
                #region 代码块
                if (ch == '{')
                {
                    AddWord();
                    inCodes++;
                    type = "code";
                    pre = '\0';
                    continue;
                }
                else if (ch == '}')
                {
                    AddWord();
                    inCodes--;
                    if (inCodes == 0)
                        type = "word";
                    pre = '\0';
                    continue;
                }
                if (char.IsWhiteSpace(ch))
                {
                    AddWord();
                    continue;
                }

                if (!char.IsPunctuation(ch))
                {
                    builder.Append(ch);
                    pre = '\0';
                    continue;
                }
                if (inCodes == 0)
                {
                    if (ch == '=')
                    {
                        AddWord();
                        type = "value";
                        pre = '\0';
                    }
                    else if (ch == ';')
                    {
                        AddWord();
                        type = "close";
                        builder.Append(ch);
                        AddWord();
                        pre = '\0';
                        continue;
                    }
                }
                if (type == "word" && (ch == '.' || ch == '<' || ch == '>' || ch == '[' || ch == ']' || ch == '(' || ch == ')'))//类型
                {
                    builder.Append(ch);
                }
                else
                {
                    AddWord();
                }
                pre = ch;
                #endregion
            }
            AddWord();
            return words;
        }

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
            var words = ToCSharpWord(Fields);
            var fields = new List<FieldConfig>();

            FieldConfig field = null;
            int inRem = 0;
            int step = -1;
            foreach (var word in words)
            {
                if (word.Name == "code" || word.Name == "value")
                {
                    step = -1;
                    continue;
                }
                if (word.Name == "attr")
                {
                    continue;
                }
                if (word.Name == "close")
                {
                    if (field != null)
                        fields.Remove(field);
                    step = -1;//跳过字段定义
                    continue;
                }
                if (word.Name == "rem-end")
                {
                    if (inRem > 1)
                        inRem = 1;
                    continue;
                }
                if (word.Name == "rem-start")
                {
                    if (word.Value == "summary")
                    {
                        inRem = 2;
                    }
                    else if (word.Value == "remark")
                    {
                        inRem = 3;
                    }
                    else
                    {
                        inRem = 1;
                    }
                    continue;
                }
                if (word.Name == "rem")
                {
                    if (inRem == 2)
                    {
                        if (step < 0)
                        {
                            fields.Add(field = new FieldConfig
                            {
                                Name = "",
                                Caption = "",
                                Description = "",
                                CsType = ""
                            });
                            step = 0;
                        }
                        field.Caption += word.Value;
                    }
                    else if (inRem == 3)
                    {
                        if (step < 0)
                        {
                            fields.Add(field = new FieldConfig
                            {
                                Name = "",
                                Caption = "",
                                Description = "",
                                CsType = ""
                            });
                            step = 0;
                        }
                        field.Description += word.Value;
                    }
                    else
                    {
                        inRem = 0;
                    }
                    continue;
                }
                if (word.Name != "word")
                {
                    continue;
                }
                if (CSharpKeyWords.Contains(word.Value))
                    continue;
                if (step < 0)
                {
                    fields.Add(field = new FieldConfig
                    {
                        Name = "",
                        Caption = "",
                        Description = "",
                        CsType = ""
                    });
                    step = 0;
                }
                if (step == 0)
                {
                    field.CsType += word.Value;
                    step = 1;
                }
                else if (step == 1)
                {
                    var ch = word.Value[0];
                    if (ch == '.' || ch == '<')
                    {
                        field.CsType += word.Value;
                        if (word.Value.Length == 1)
                            step = 0;//还是类型逻辑
                    }
                    else if (ch == '>')
                    {
                        field.CsType += word.Value;
                    }
                    else
                    {
                        field.Name = word.Value;
                        step = 2;
                    }
                }
                else if (step == 2)
                {
                    var ch = word.Value[0];
                    if (ch == '.' || ch == '<')
                    {
                        field.Name += word.Value;
                        if (word.Value.Length == 1)
                            step = 1;//还是名称逻辑
                    }
                    else if (ch == '>')
                    {
                        field.Name += word.Value;
                    }
                    else
                    {
                        step = 3;
                    }
                }
            }
            return fields.Select(field => $"{field.Name},{field.CsType},{field.Caption},{field.Description}").LinkToString("\r\n");
        }

        #endregion

        #region 规整文本(名称 是否必填 类型 标题)

        public string DoFormatDocument(string arg)
        {
            var lines = Fields.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            var code = new StringBuilder();
            foreach (var line in lines)
            {

                if (string.IsNullOrWhiteSpace(line))
                    continue;
                var words = line.Trim().Split(new[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (words.Length < 3)
                {
                    code.Append(line);
                    continue;
                }
                code.AppendLine();
                var list = words.Select(p => p.Trim().Replace(',', '，')).Where(p => !string.IsNullOrWhiteSpace(p)).ToList();
                if (list.Count > 1)
                {
                    words = list[1].Split('(', ')');
                    list[1] = words.LinkToString("-");
                }
                if (list.Count > 3)
                {
                    if (list[2][0] == '必' || list[2][0] == '是')
                        list[1] += '!';
                    list.RemoveAt(2);
                }
                code.Append(list.LinkToString(","));
            }

            return code.ToString();
        }

        public string DoFormatDocument2(string arg)
        {
            var lines = Fields.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            var code = new StringBuilder();
            foreach (var line in lines)
            {

                if (string.IsNullOrWhiteSpace(line))
                    continue;
                var words = line.Trim().Split(new[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (words.Length < 3)
                {
                    //code.Append(line);
                    continue;
                }
                //var list = words.Select(p => p.Trim().Replace(',', '，')).Where(p => !string.IsNullOrWhiteSpace(p)).ToList();
                //if (list.Count > 1)
                //{
                //    words = list[1].Split('(', ')');
                //    list[1] = words.LinkToString("-");
                //}
                //if (list.Count > 3)
                //{
                //    if (list[2][0] == '必' || list[2][0] == '是')
                //        list[1] += '!';
                //    list.RemoveAt(2);
                //}
                code.AppendLine($"{words[2]},{words[3]},{words[0]}");
            }

            return code.ToString();
        }

        public string DoFormatDocument3(string arg)
        {
            var lines = Fields.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            var code = new StringBuilder();
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;
                var words = line.Trim().Split(new[] { '\t' });
                var list = new List<string>();
                Add(list, words[0]);
                if (words.Length > 2)
                {
                    var type = words[2];
                    if (!string.IsNullOrEmpty(words[2]))
                    {
                        var types = type.Split('(', ')');
                        type = types.LinkToString("-");
                    }
                    Add(list, type);
                    Add(list, string.IsNullOrEmpty(words[1]) ? words[0] : words[1]);
                }
                string desc = words.Length > 5 && !string.IsNullOrEmpty(words[5]) ? words[5] : null;

                if (list[2].Contains("（"))
                {
                    if (desc == null)
                    {
                        desc = list[2];
                        list[2] = list[2].Split('（')[0];
                    }
                    else if (desc == list[2])
                    {
                        list[2] = list[2].Split('（')[0];
                    }
                    else
                    {
                        desc = list[2] + "。" + desc;
                        list[2] = list[2].Split('（')[0];
                    }
                }
                Add(list, desc);
                if (words.Length > 4 && string.IsNullOrEmpty(words[4]))
                {
                    list[1] += "#";
                }
                code.AppendLine(desc.LinkToString(","));
            }

            return code.ToString();
        }

        void Add(List<string> list, string word)
        {
            if (string.IsNullOrEmpty(word))
                list.Add(string.Empty);
            else
                list.Add(word?.Replace(',', '，'));
        }
        #endregion

        #region 规整文本(名称 类型 标题)

        public string DoFormat2(string arg)
        {
            var lines = Fields.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            var code = new StringBuilder();
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
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
                if (string.IsNullOrWhiteSpace(line))
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
                if (string.IsNullOrWhiteSpace(line))
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

        public List<FieldConfig> DoCheckFieldes(string arg)
        {
            var columns = new List<FieldConfig>();
            var lines = Fields.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var idx = Entity?.MaxIdentity ?? 1;
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;
                var words = line.Trim().Split(new[] { ',', '，' }, StringSplitOptions.RemoveEmptyEntries);

                var name = GlobalConfig.ToLinkWordName(words[0], "", true);


                /*
                文本说明:
                * 1 每行为一条数据
                * 2 每个单词用逗号分开
                * 3 第一个单词 代码名称; 第二个单词 数据类型;第三个单词 说明文本
                */
                var property = new FieldConfig
                {
                    Name = name,
                    CanUserQuery = true,
                    IsPrimaryKey = name.Equals("ID", StringComparison.OrdinalIgnoreCase),
                    ApiArgumentName = words[0],
                    DataBaseField = new Config.V2021.DataBaseFieldConfig
                    {
                        Datalen = 200,
                        FieldType = "NVARCHAR",
                    },
                    DataType = "String",
                    CsType = "string",
                    Option =
                    {
                        Identity = idx++,
                        Index = idx++
                    }
                };
                if (words.Length > 1)
                    CsharpHelper.CheckType(property, words[1]);
                if (words.Length > 2 && words[2] != "@" && words[2] != "#")
                    property.Caption = words[2];
                if (words.Length > 3 && words[3] != "@" && words[3] != "#")
                    property.Description = words.Length < 4 ? null : words.Skip(3).LinkToString(",").TrimEnd('@', '#');


                var old = columns.FirstOrDefault(p => p != null && p.Name == name);
                if (old != null)
                {
                    old.Caption = property.Caption;
                    old.Description = property.Description;
                }
                else
                {
                    if (name == "Memo")
                    {
                        property.DataBaseField.Datalen = 0;
                        property.DataBaseField.IsText = true;
                        property.DataBaseField.FieldType = "TEXT";
                    }
                    else
                    {
                        DataTypeHelper.CsDataType(property);
                    }
                    columns.Add(property);
                }
                property.DataBaseField.DbFieldName = property.Name.ToName('_', false);
                property.JsonName = property.Name.ToLWord();
            }

            return columns;
        }


        #endregion
    }
}