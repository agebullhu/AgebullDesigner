using System;
using System.IO;
using System.Linq;
using System.Text;
using Agebull.Common.Access.ADO.Sqlite;
using Gboxt.Common.DataAccess.Schemas;

namespace Agebull.Common.SimpleDesign
{
    public sealed class EntitySerializableBuilder : SchemaCodeBuilder
    {
        private string FieldCode()
        {
            var code = new StringBuilder();
            bool isFirst = true;
            foreach (PropertyConfig field in Table.PublishProperty)
            {
                code.AppendFormat(@"
                jsonBuilder.AppendValueProperty(""_{0}"",{1});"
                    , field.PropertyName.ToLower()
                    , PropertyName2(field, null, isFirst ? ",true" : string.Empty));
                if (isFirst)
                    isFirst = false;
            }

            return code.ToString();
        }

        public string PropertyCode()
        {
            var code = new StringBuilder();
            bool isFirst = true;
            foreach (PropertyConfig field in Table.PublishProperty)
            {
                code.AppendFormat(@"
                jsonBuilder.AppendValueProperty(""{0}"",{1});"
                    , string.IsNullOrWhiteSpace(field.ClientName) ? field.PropertyName : field.ClientName
                    , PropertyName2(field, null, isFirst ? ",true" : string.Empty));
                if (isFirst)
                    isFirst = false;
            }

            return code.ToString();
        }

        public override void CreateBaCode(string path)
        {
            string code = string.Format(@"using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Runtime.Serialization;
using System.Security.Permissions;

using HY.Model;
using HY.GameApi.Model;

namespace {0}
{{
	partial class {1}Data
	{{
        #region 实体结构
{2}
        #endregion
        
    }}
}}"
                , NameSpace
                , Table.EntityName
                , EntityStruct());
            string file = Path.Combine(path, Table.EntityName + ".Binary.cs");
            this.SaveCode(file, code);
        }

        private string EntityStruct()
        {
            var code = new StringBuilder();
            bool isFirst = true;
            foreach (PropertyConfig col in Table.PublishProperty)
            {
                if (isFirst)
                    isFirst = false;
                else code.Append(',');
                code.AppendFormat(@"
                {{
                    Real_{0},
                    new PropertySturct
                    {{
                        Index = Index_{0},
                        PropertyName = ""{0}"",
                        PropertyType = typeof({1}),
                        CanNull = {2},
                        ValueType = PropertyValueType.{3}
                    }}
                }}", col.PropertyName
                    , col.CustomType ?? col.CsType
                    , col.Nullable ? "true" : "false"
                    , CodeBuilderDefault.PropertyValueType(col));
            }
            var code2 = new StringBuilder();
            foreach (PropertyConfig col in Table.PublishProperty)
            {
                code2.AppendFormat(@"
        public const byte Index_{0} = {1};", col.PropertyName, col.Index);
            }
            return string.Format(@"
        {3}

        /// <summary>
        /// 实体结构
        /// </summary>
        [IgnoreDataMember,Browsable (false)]
        public override EntitySturct __Struct
        {{
            get
            {{
                return __struct;
            }}
         }}

        /// <summary>
        /// 实体结构
        /// </summary>
        [IgnoreDataMember]
        static readonly EntitySturct __struct = new EntitySturct
        {{
            EntityName = ""{0}"",
            PrimaryKey = ""{1}"",
            Properties = new Dictionary<int, PropertySturct>
            {{{2}
            }}
         }};
"
                , Table.EntityName, Table.PrimaryColumn.PropertyName, code, code2);
        }

        string PropertyName2(PropertyConfig col, string pre = null, string end = null)
        {
            if (col.CustomType == null)
                return string.Format("{0}{1}{2}", pre, col.PropertyName, end);
            return string.Format("({0}){1}{2}{3}", col.CsType, pre, col.PropertyName, end);
        }
        

        /// <summary>
        ///     生成代码
        /// </summary>
        public override void CreateExCode(string path)
        {
            string file = Path.Combine(path, Table.EntityName + ".cs");
            //if (File.Exists(file))
            //    return;
            string code = string.Format(@"
using System;
using System.ComponentModel;
using System.Text;
using System.Runtime.Serialization;
using System.Security.Permissions;
using HY.Model;
namespace {0}
{{
	partial class {1}Entity : IPropertyJson, IFieldJson
	{{
        partial void OnToFieldJson(StringBuilder jsonBuilder);
        /// <summary>
        /// 使用将目标对象序列化为以字段为基础的JSON
        /// </summary>
        public string ToFieldJson()
        {{
            return ToPropertyJson();
            /*StringBuilder jsonBuilder = new StringBuilder();
            using(JsonTypeScope.CreateScope(jsonBuilder ))
            {{{2}
                OnToFieldJson(jsonBuilder);
            }}
            return jsonBuilder.ToString();*/
        }}
        partial void OnToPropertyJson(StringBuilder jsonBuilder);
        /// <summary>
        /// 使用将目标对象序列化为以属性为基础的JSON
        /// </summary>
        public string ToPropertyJson()
        {{
            StringBuilder jsonBuilder = new StringBuilder();
            using(JsonTypeScope.CreateScope(jsonBuilder ))
            {{{3}
                OnToPropertyJson(jsonBuilder);
            }}
            return jsonBuilder.ToString();
        }}
    }}
}}"
                , NameSpace
                , Table.EntityName
                , FieldCode()
                , PropertyCode());
            this.SaveCode(file, code);
        }
    }
}

/*

        void ReadCoreCode()
        {
            StringBuilder code = new StringBuilder();


            code.Append(@"
#if DEBUG
            int pre = -1;
#endif
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                var index = reader.ReadByte();
                if(index == 0xFF)
                    break;
#if DEBUG
                if (pre >= index)
                    throw new Exception(string.Format(""{0}:ErrorId:{1}"" ,pre, index));
                pre = index;
                if(reader.BaseStream.Position == reader.BaseStream.Length)
                    break;
#endif
                var type = reader.ReadByte();
#if DEBUG
                if(reader.BaseStream.Position == reader.BaseStream.Length)
                    break;
#else
                bool isBad=false;
#endif
                switch (index)
                {");
            foreach (ColumnSchema col in Table.LastColumns.Where(p => !p.DenyScope.HasFlag(FieldScopeType.Server)))
            {
                code.AppendFormat(@"
                    case {2}://Index_{0}->{1}", col.PropertyName, col.Caption, col.Index);
                if (col.CsType == "DateTime")
                    code.AppendFormat(@"
#if DEBUG
                       if(type != {2})
                            throw new Exception(""{0}:{1}存储的数据类型错误"");
#endif
                        this._{1} = new DateTime(reader.ReadInt64());
                        break;"
                        , Table.EntityName
                        , col.PropertyName.ToLower()
                        , CodeBuilderDefault.ByteLen(col.CsType));
                else
                    code.AppendFormat(@"
#if DEBUG
                       if(type != {4})
                            throw new Exception(""{3}:{0}存储的数据类型错误"");
#endif
                        this._{0} = ({2})reader.Read{1}();
                        break;"
                        , col.PropertyName.ToLower()
                        , CodeBuilderDefault.ToNetFrameType(col.CsType)
                        , col.Type ?? col.CsType
                        , Table.EntityName
                        , CodeBuilderDefault.ByteLen(col.CsType));
            }
            code.AppendFormat(@"
                    default:
                        if(!ByteCommond.SkipEmpty(reader,type))
                        {{
#if DEBUG
                            throw new Exception(""{0}:"" + index + ""序号属性丢失"");
#else
                            isBad=true;
#endif
                        }}
                        break;
                }}
#if !DEBUG
                if(isBad)
                    break;
#endif
            }}
#if DEBUG
            var ts = reader.ReadInt64();
            timeStamp = new System.DateTime(ts);
            HY.Web.Units.Log.EntityTimespan(this,ts,2);
#else
            timeStamp = new System.DateTime(reader.ReadInt64());
#endif", Table.EntityName);


            string cd = code.ToString();
            if (Table.DataVersion == 0)
            {
                Table.DataVersion = 1;
                Table.ReadCoreCodes.Add(1, cd);
            }
            else if (!string.Equals(cd, Table.ReadCoreCodes[Table.DataVersion], StringComparison.OrdinalIgnoreCase))
            {
                Table.DataVersion += 1;
                Table.ReadCoreCodes.Add(Table.DataVersion, cd);
            }
        }

private string BinaryValue()
        {
            ReadCoreCode();
            var code = new StringBuilder();

            code.Append(@"

        #region 二进制序列化
        #region 读数据

        /// <summary>
        /// 读取二进制值
        /// </summary>
        /// <param name=""reader"">流对象</param>
        /// <param name=""ver"">数据版本号</param>
        /// <summary>
        public override void ReadBinaryValue(BinaryReader reader, int ver)
        {
            switch(ver)
            {");
            foreach (var kv in Table.ReadCoreCodes)
            {
                if (kv.Key == Table.DataVersion)
                {
                    code.AppendFormat(@"
                case {0}:
                    ReadBinaryValue(reader);
                    break;"
                        , kv.Key);
                }
                else
                {
                    code.AppendFormat(@"
                case {0}:
                    ReadVer{0}BinaryValue(reader);
                    break;"
                        , kv.Key);
                }
            }

            code.AppendFormat(@"
            }}
            this.__redisKey = RedisKeyBuilder.ToEntityKey(""{0}"" , {1});
        }}"
                , Table.EntityName
                , Table.PrimaryColumn.PropertyName);

            foreach (var kv in Table.ReadCoreCodes)
            {
                if (kv.Key == Table.DataVersion)
                {
                    code.AppendFormat(@"

        partial void OnDeSerialized();

        partial void OnDeSerializing();

        /// <summary>
        /// 读取二进制值,当前版本
        /// </summary>
        /// <param name=""reader"">流对象</param>
        /// <summary>
        private void ReadBinaryValue(BinaryReader reader)
        {{
            OnDeSerializing();{0}
            OnDeSerialized();
        }}"
                        , kv.Value);
                }
                else
                {
                    code.AppendFormat(@"
        /// <summary>
        /// 读取二进制值,第{0}版
        /// </summary>
        /// <param name=""reader"">流对象</param>
        /// <summary>
        private void ReadVer{0}BinaryValue(BinaryReader reader)
        {{{1}
        }}"
                        , kv.Key
                        , kv.Value);
                }
            }
            code.Append(@"
        #endregion

        #region 写数据");

            code.AppendFormat(@"

        partial void OnSerialized();

        partial void OnSerializing();

        /// <summary>
        /// 写入二进制流
        /// </summary>
        /// <param name=""writer"">流对象</param>
        public override void WriteBinaryValue(BinaryWriter writer)
        {{
            OnSerializing();
            writer.Write((byte){0});", Table.DataVersion);
            foreach (ColumnSchema col in Table.LastColumns.Where(p => !p.DenyScope.HasFlag(FieldScopeType.Server)))
            {
                if (col.CsType == "String" || col.CsType == "string")
                {
                    code.AppendFormat(@"
            //{1}
            if(!string.IsNullOrWhiteSpace(this._{0}))
            {{
                writer.Write(Index_{2});
                writer.Write((byte){3});
                writer.Write(this._{0});
            }}"
                        , col.PropertyName.ToLower()
                        , col.Caption
                        , col.PropertyName
                        , CodeBuilderDefault.ByteLen(col.CsType));
                }
                else if (col.Nullable)
                {
                    if (col.CsType == "DateTime")
                        code.AppendFormat(@"
            //{1}
            if(this._{0} != null)
            {{
                writer.Write((byte)Index_{2});
                writer.Write((byte){3});
                writer.Write(this._{0}.Value.Ticks);
            }}"
                            , col.PropertyName.ToLower()
                            , col.Caption
                            , col.PropertyName
                            , CodeBuilderDefault.ByteLen(col.CsType));
                    else
                        code.AppendFormat(@"
            //{1}
            if(this._{0} != null)
            {{
                writer.Write((byte)Index_{2});
                writer.Write((byte){4});
                writer.Write({3});
            }}"
                           , col.PropertyName.ToLower()
                           , col.Caption
                           , col.PropertyName
                           , PropertyName2(col, "this.", ".Value")
                           , CodeBuilderDefault.ByteLen(col.CsType));
                }
                else
                {
                    if (col.CsType == "DateTime")
                        code.AppendFormat(@"
            writer.Write((byte)Index_{1});
            writer.Write((byte){2});
            writer.Write(this._{0}.Ticks);"
                            , col.PropertyName.ToLower()
                            , col.PropertyName
                            , CodeBuilderDefault.ByteLen(col.CsType));
                    else
                        code.AppendFormat(@"
            writer.Write((byte)Index_{0});
            writer.Write((byte){2});
            writer.Write({1});"
                            , col.PropertyName
                            , PropertyName2(col, "this.")
                            , CodeBuilderDefault.ByteLen(col.CsType));
                }
            }
            code.Append(@"
            writer.Write((byte)0xFF);
#if DEBUG
            timeStamp =  DateTime.Now;
            writer.Write(timeStamp.Ticks);
            HY.Web.Units.Log.EntityTimespan(this,timeStamp.Ticks,1);
#else
            writer.Write(DateTime.Now.Ticks);
#endif
            OnSerialized();
        }
        #endregion

        #endregion");
            return code.ToString();
        }
*/
