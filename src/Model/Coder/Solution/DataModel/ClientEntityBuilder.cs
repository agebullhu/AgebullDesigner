using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Agebull.Common.Access.ADO.Sqlite;

namespace Agebull.Common.SimpleDesign
{
    public sealed class ClientEntityBuilder : SchemaCodeBuilder
    {
        private string Properties()
        {
            var code = new StringBuilder();
            bool isUser = Table.PublishProperty.Any(p => p.IsUserId);
            foreach (var col in Table.PublishProperty)
            {
                if (isUser && col.IsPrimaryKey)
                {
                    col.ClientType = col.CsType;
                }
                else if (string.IsNullOrWhiteSpace(col.ClientType))
                {
                    continue;
                }
                string type = col.ClientType;
                string name = string.IsNullOrEmpty(col.ClientName) ? col.PropertyName : col.ClientName;
                string description = col.Description;
                if (string.IsNullOrWhiteSpace(description))
                {
                    description = col.Caption;
                }
                if (string.IsNullOrWhiteSpace(description))
                {
                    description = col.PropertyName;
                }
                string caption = col.Caption;
                if (string.IsNullOrWhiteSpace(caption))
                {
                    caption = col.PropertyName;
                }
                code.AppendFormat(@"


        /// <summary>
        /// {4}
        /// </summary>
        private {3} _{1};

        /// <summary>
        /// {4}
        /// </summary>
        /// <remarks>
        /// {0}
        /// </remarks>
        public {3} {2}
        {{
            get
            {{
                return this._{1};
            }}
            set
            {{
                this._{1} = value;
            }}
        }}"
                    , ToRemString(description)
                    , name.ToLower()
                    , name
                    , type
                    , ToRemString(caption));
                foreach(var alias in col.GetAliasPropertys())
                {
                    code.AppendFormat(@"
        /// <summary>
        /// {0}
        /// </summary>
        /// <remarks>
        /// {1}
        /// </remarks>
        public {2} {3}
        {{
            get
            {{
                return this.{4};
            }}
            /*set
            {{
                this.{4} = value;
            }}*/
        }}"
                        , ToRemString(caption)
                        , ToRemString(description)
                        , type
                        , alias
                        , name);
                }
            }
            return code.ToString();
        }

        /// <summary>
        ///     生成实体代码
        /// </summary>
        public override void CreateBaCode(string path)
        {
            string code = string.Format(@"
using System;
using System.Text;
using System.Runtime.Serialization;

namespace {0}
{{
    /// <summary>
    /// {1}
    /// </summary>
    [Serializable]
    public partial class {2}
    {{
{3}
    }}
}}"
                , NameSpace
                , Table.Description
                , Table.ClientName ?? Table.EntityName
                , Properties());
            string file = Path.Combine(path, Table.EntityName + ".cs");
            File.WriteAllText(file, code);
        }

        /// <summary>
        ///     生成扩展代码
        /// </summary>
        public override void CreateExCode(string path)
        {
            string ptype = Table.PrimaryColumn.ClientType;
            if (string.IsNullOrWhiteSpace(ptype))
                ptype = Table.PrimaryColumn.CsType;
            string code = string.Format(@"
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace {0}
{{
    partial class {1}
    {{
        private static DateTime GetTime(object obj)
        {{
            try
            {{
                return System.Convert.ToDateTime(obj);
            }}
            catch
            {{
                DateTime time = new DateTime(1970, 1, 1);
                long ms = long.Parse((obj + string.Empty).Split('(', '+')[1]);
                time = time.AddMilliseconds(ms).ToLocalTime();
                return time;
            }}
        }}

        /// <summary>
        /// 反序列化Json为列表
        /// </summary>
        /// <param name=""values"">Json字典值数组</param>
        /// <returns>数据(列表)</returns>
        public static List<{1}> ReadToList(Dictionary<string, object>[] values)
        {{
            var results = new List<{1}>();
            if (values == null || values.Length == 0)
            {{
#if DEBUG
                throw new Exception(""{1}表没有任何数据"");
#endif
            }}
            else
            {{
                foreach (var value in values)
                {{
                    results.Add(Read(value));
                }}
            }}
            return results;
        }}

        /// <summary>
        /// 反序列化Json为字典
        /// </summary>
        /// <param name=""values"">Json字典值数组</param>
        /// <returns>数据(字典)</returns>
        public static Dictionary<long, {1}> ReadToDictionary(Dictionary<string, object>[] values)
        {{
            var results = new Dictionary<long, {1}>();
            if (values == null || values.Length == 0)
            {{
#if DEBUG
                throw new Exception(""{1}表没有任何数据"");
#endif
            }}
            else
            {{
                foreach (var value in values)
                {{
                    var data = Read(value);
                    results.Add(Convert.ToInt64(data.{2}), data);
                }}
            }}
            return results;
        }}

        /// <summary>
        /// 反序列化Json为一条数据
        /// </summary>
        /// <param name=""value"">Json字典值</param>
        /// <returns>数据</returns>
        public static {1} Read(Dictionary<string, object> value)
        {{
            var entity = new {1}();
            if (value == null || value.Count == 0)
            {{
#if DEBUG
                  throw new Exception(""{1}表内容为空"");
#endif
                  return entity;
            }}{3}
            return entity;
        }}
    }}
}}"
                , NameSpace
                , Table.ClientName ?? Table.EntityName
                , Table.PrimaryColumn
                , DJson());
            string file = Path.Combine(path, "Serializable", Table.EntityName + ".cs");
            File.WriteAllText(file, code);
        }

        private string DJson()
        {
            var code = new StringBuilder();
            bool isUser = Table.PublishProperty.Any(p => p.IsUserId);
            foreach (var col in Table.PublishProperty)
            {
                if (isUser && col.IsPrimaryKey)
                {
                    col.ClientType = col.CsType;
                }
                else if (string.IsNullOrEmpty(col.ClientType))
                {
                    continue;
                }
                string type = col.ClientType.Trim();
                string name = string.IsNullOrEmpty(col.ClientName) ? col.PropertyName : col.ClientName;
                if (type[type.Length - 1] == '?')
                {
                    if (type.Contains("DateTime"))
                        code.AppendFormat(@"
            if (value.ContainsKey(""{0}""))
            {{
                var v1 = value[""{0}""];
                if(v1 != null && !string.IsNullOrEmpty(v1.ToString()))
                    entity.{0} =GetTime(v1);
            }}"
                            , name);
                    else
                        code.AppendFormat(@"
            if (value.ContainsKey(""{0}""))
            {{
                var v1 = value[""{0}""];
                if(v1 != null && !string.IsNullOrEmpty(v1.ToString()))
                    entity.{0} = Convert.To{1}(v1);
            }}"
                            , name
                            , CodeBuilderDefault.ToNetFrameType(type));
                }
                else if (type.Contains("DateTime"))
                    code.AppendFormat(@"
            if (value.ContainsKey(""{0}""))
                entity.{0} =GetTime(value[""{0}""]);"
                        , name);
                else
                    code.AppendFormat(@"
            if (value.ContainsKey(""{0}""))
                entity.{0} = Convert.To{1}(value[""{0}""]);"
                        , name
                        , CodeBuilderDefault.ToNetFrameType(type));
                if (!col.Nullable)
                {
                    code.AppendFormat(@"
#if DEBUG
            else
                throw new Exception(""{1}表{0}字段值丢失"");
#endif"
                    , Table.EntityName
                    , name);

                }
            }
            return code.ToString();
        }
    }
}