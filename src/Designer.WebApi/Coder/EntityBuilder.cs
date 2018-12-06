using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.RobotCoder;

namespace Agebull.EntityModel.Designer.WebApi
{


    public sealed class EntityBuilder : EntityCoderBase
    {

        #region �������
        /// <summary>
        /// �Ƿ��д
        /// </summary>
        protected override bool CanWrite => true;

        /// <summary>
        /// ����
        /// </summary>
        protected override string FileSaveConfigName => "File_API_Entity_Base_cs";
        /// <summary>
        /// �Ƿ�ͻ��˴���
        /// </summary>
        protected override bool IsClient => true;

        /// <summary>
        ///     ����ʵ�����
        /// </summary>
        protected override void CreateBaCode(string path)
        {
            string code = $@"
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using Newtonsoft.Json;

namespace {NameSpace}
{{
    /// <summary>
    /// {Entity.Description}
    /// </summary>
    [DataContract,JsonObject(MemberSerialization.OptIn)]
    public partial class {Entity.Name}
    {{
        {Properties()}
    }}
}}";
            var file = ConfigPath(Project, FileSaveConfigName, path, Entity.Classify, Entity.Name);
            WriteFile(file + ".cs", code);
        }

        /// <summary>
        ///     ������չ����
        /// </summary>
        protected override void CreateExCode(string path)
        {
            string code = $@"
#if API_EXTEND
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using Newtonsoft.Json;

using Agebull.Common;
using Agebull.Common.DataModel;
using Gboxt.Common.DataModel;
using Agebull.Common.WebApi;

{Project.UsingNameSpaces}

namespace {NameSpace}.WebApi
{{
    sealed partial class {Entity.Name} : IApiResultData , IApiArgument
    {{
        {ToData()}
        {ValidateCode()}
    }}
}}
#endif";
            var file = ConfigPath(Project, FileSaveConfigName, path, Entity.Classify, Entity.Name);
            WriteFile(file + ".api.cs", code);
        }


        #endregion

        #region ���Զ���


        private string Properties()
        {
            var code = new StringBuilder();
            code.Append(@"
        #region ����");
            foreach (PropertyConfig property in Columns)//.Where(p => !p.NoneApiArgument)
            {
                string type = property.DataType == "ByteArray" ? "string" : property.LastCsType;

                code.Append(RemCode(property));
                code.Append(@"
        [DataMember");
                code.Append(property.NoneJson
                    ? " , JsonIgnore"
                    : $@" , JsonProperty(""{property.JsonName}"", NullValueHandling = NullValueHandling.Ignore)");
                code.Append("]");
                code.Append($@"
        public {type} {property.Name}
        {{
            get;
            set;
        }}");
            }
            code.Append(@"

        #endregion");

            return code.ToString();
        }

        #endregion

        #region תת


        private string ToData()
        {
            if (Entity.NoDataBase)
                return "";
            var code = new StringBuilder();
            int len = Columns.Max(p => p.Name.Length) + 2;
            code.Append($@"
        #region ���ڲ����ݿ������ת��

        /// <summary>תΪ�ڲ����ݿ����</summary>
        /// <returns>�ڲ����ݿ����</returns>
        public {Entity.EntityName} ToData => new {Entity.EntityName}
        {{");
            bool isFirst = true;
            foreach (PropertyConfig property in Columns.Where(p => !p.NoneApiArgument))
            {
                if (isFirst)
                    isFirst = false;
                else
                    code.Append(",");
                code.Append($@"
            {property.Name}");
                code.Append(' ', len - property.Name.Length);
                code.Append("= ");
                code.Append(property.DataType == "ByteArray"
                    ? $@"{property.Name}==null || {property.Name}.Length == 0 ? null : Convert.FromBase64String({property.Name})"
                    : property.Name);
            }
            code.Append($@"
        }};

        /// <summary>תΪ��������</summary>
        /// <returns>��������</returns>
        public void FromData ({Entity.EntityName} data) 
        {{");
            foreach (PropertyConfig property in Columns.Where(p => !p.NoneApiArgument))
            {
                code.Append($@"
            {property.Name}");
                code.Append(' ', len - property.Name.Length);
                code.Append("= ");
                code.Append(property.DataType == "ByteArray"
                    ? $@"data.{property.Name}==null || data.{property.Name}.Length == 0 ? null : Convert.ToBase64String(data.{property.Name})"
                    : property.Name);
                code.Append(';');
            }
            code.Append($@"
        }}

        /// <summary>תΪ��������</summary>
        /// <returns>��������</returns>
        public static {Entity.Name} ToArgument ({Entity.EntityName} data) => new {Entity.Name}
        {{");
            isFirst = true;
            foreach (PropertyConfig property in Columns.Where(p => !p.NoneApiArgument))
            {
                if (isFirst)
                    isFirst = false;
                else
                    code.Append(",");

                code.Append($@"
            {property.Name}");
                code.Append(' ', len - property.Name.Length);
                code.Append("= ");
                code.Append(property.DataType == "ByteArray"
                    ? $@"data.{property.Name}==null || data.{property.Name}.Length == 0 ? null : Convert.ToBase64String(data.{property.Name})"
                    : $@"data.{property.Name}");
            }
            code.Append(@"
        };

        #endregion");

            return code.ToString();
        }

        #endregion

        #region ����У��

        public string ValidateCode()
        {
            var coder = new EntityValidateCoder { Entity = Entity };
            var code = coder.Code(Columns.Where(p => !p.NoneApiArgument));
            return $@"
        #region ����У��
        /// <summary>����У��</summary>
        /// <param name=""message"">���ص���Ϣ</param>
        /// <returns>�ɹ��򷵻���</returns>
        bool IApiArgument.Validate(out string message)
        {{
            var result = Validate();
            message = result.Messages.LinkToString('��');
            return result.succeed;
        }}

        /// <summary>
        /// ��չУ��
        /// </summary>
        /// <param name=""result"">�����Ŵ�</param>
        partial void ValidateEx(ValidateResult result);

        /// <summary>
        /// ����У��
        /// </summary>
        /// <returns>����У�����</returns>
        public ValidateResult Validate()
        {{
            var result = new ValidateResult();{code}
            ValidateEx(result);
            return result;
        }}
        #endregion";
        }

        #endregion
    }

}

