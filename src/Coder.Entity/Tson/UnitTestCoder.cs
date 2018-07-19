using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;

namespace Agebull.EntityModel.RobotCoder
{
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    public class TsonCoder : MomentCoderBase, IAutoRegister
    {
        #region 注册

        /// <summary>
        /// 注册代码
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            MomentCoder.RegisteCoder("TSON", "序列化代码(C#)", "cs", arg => CreateCode<EntityConfig>(arg,CreateCode));
        }
        #endregion


        string CreateCode(EntityConfig entity)
        { 
            StringBuilder code1 = new StringBuilder();
            foreach (PropertyConfig property in entity.PublishProperty.Where(p => !p.NoneJson))
            {
                code1.AppendFormat(@"
        public const byte Index_{0} = {1};", property.Name, property.Identity);
            }

            StringBuilder code2 = new StringBuilder();
            foreach (PropertyConfig property in entity.PublishProperty.Where(p => !p.NoneJson))
            {
                if (property.IsEnum)
                    code2.Append($@"
            serializer.Write(Index_{property.Name}, (int)data.{property.Name});");
                else if (property.CustomType != null)
                {
                    code2.Append($@"
            serializer.WriteIndex(Index_{property.Name});
            {{
                var tsonOperator = new {property.CustomType}TsonOperator();
                tsonOperator.ToTson(serializer, data.{property.Name});
            }}");
                }
                else
                    code2.Append($@"
            serializer.Write(Index_{property.Name}, data.{property.Name});");
            }
            StringBuilder code3 = new StringBuilder();
            foreach (PropertyConfig property in entity.PublishProperty.Where(p => !p.NoneJson))
            {
                if (property.IsEnum)
                    code3.Append($@"
                    case Index_{property.Name}:
                        data.{property.Name} = ({property.CustomType})serializer.ReadInt();
                        break;");
                else if (property.CustomType != null)
                {
                    code3.Append($@"
                    case Index_{property.Name}:
                    {{
                        var item = new {property.CustomType}();
                        var tsonOperator = new {property.CustomType}TsonOperator();
                        tsonOperator.FromTson(serializer, item);
                        data.{property.Name} = item;
                        break;
                    }}");
                }
                else
                {
                    code3.Append($@"
                    case Index_{property.Name}:
                        data.{property.Name} = serializer.Read{property.CsType.ToUWord()}();
                        break;");
                }
            }
            return $@"using System;
using Agebull.ZeroNet.ZeroApi;
using Agebull.Common.Tson;

namespace 
{{
    public class {entity.EntityName}TsonOperator: ITsonOperator<{entity.EntityName}>
    {{{code1}
        
        /// <summary>
        /// 序列化
        /// </summary>
        public void ToTson(ITsonSerializer serializer, {entity.EntityName} data)
        {{{code2}
        }}

        /// <summary>
        /// 反序列化
        /// </summary>
        public void FromTson(ITsonDeserializer serializer, {entity.EntityName} data)
        {{
            while (!serializer.IsEof)
            {{
                int idx = serializer.ReadIndex();
                switch (idx)
                {{
                    case Index_IsInner:
                        data.IsInner = serializer.ReadBool();
                        break;
                    case Index_Title:
                        data.Title = serializer.ReadString();
                        break;
                    case Index_Start:
                        data.Start = serializer.ReadLong();
                        break;
                    case Index_End:
                        data.End = serializer.ReadLong();
                        break;
                    case Index_ToId:
                        data.ToId = serializer.ReadString();
                        break;
                    case Index_FromId:
                        data.FromId = serializer.ReadString();
                        break;
                    case Index_Requester:
                        data.Requester = serializer.ReadString();
                        break;
                    case Index_HostName:
                        data.HostName = serializer.ReadString();
                        break;
                    case Index_ApiName:
                        data.ApiName = serializer.ReadString();
                        break;
                    case Index_Status:
                        data.Status = (OperatorStatus)serializer.ReadInt();
                        break;
                    case Index_Machine:
                        data.Machine = serializer.ReadString();
                        break;
                    case Index_Station:
                        data.Station = serializer.ReadString();
                        break;
                    case Index_User:
                        data.User = serializer.ReadString();
                        break;
                    case Index_RequestId:
                        data.RequestId = serializer.ReadString();
                        break;
                }}
            }}
        }}
    }}
}}";
        }
    }
}