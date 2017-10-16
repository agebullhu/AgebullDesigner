using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
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
            string code = $@"using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Runtime.Serialization;
using Agebull.Common;
using Gboxt.Common.DataModel;
using Yizuan.Service.Api;

using Newtonsoft.Json;

namespace {NameSpace}.WebApi
{{
    /// <summary>
    /// {Entity.Description}
    /// </summary>
    [DataContract,JsonObject(MemberSerialization.OptIn)]
    public partial class {Entity.Name} : IApiResultData , IApiArgument
    {{
        {Properties()}
        {ToForm()}
        {ValidateCode()}
    }}
}}";
            var file = ConfigPath(Project, FileSaveConfigName, path, "Model", Entity.Name);
            SaveCode(file + ".Designer.cs", code);
        }

        /// <summary>
        ///     ������չ����
        /// </summary>
        protected override void CreateExCode(string path)
        {
            string code = $@"
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Gboxt.Common.DataModel;

namespace {NameSpace}.WebApi
{{
    sealed partial class {Entity.Name}
    {{
        /*// <summary>
        /// ��չУ��
        /// </summary>
        /// <param name=""result"">�����Ŵ�</param>
        partial void ValidateEx(ValidateResult result);*/
    }}
}}";
            var file = ConfigPath(Project, FileSaveConfigName, path, "Model", Entity.Name);
            SaveCode(file + ".cs", code);
        }


        #endregion

        #region ���Զ���


        private string Properties()
        {
            var code = new StringBuilder();
            code.Append(@"
        #region ����");
            foreach (PropertyConfig property in Columns.Where(p => p.CanUserInput))
            {
                var attribute = new StringBuilder();
                attribute.Append("[DataMember");
                if (!IsClient)
                {
                    attribute.Append(!property.NoneJson
                        ? $@" , JsonProperty(""{property.Name.ToLWord()}"", NullValueHandling = NullValueHandling.Ignore)"
                        : " , JsonIgnore");
                }
                attribute.Append("]");
                code.Append($@"

        /// <summary>
        /// {ToRemString(property.Caption ?? property.Description)}
        /// </summary>
        /// <remarks>
        /// {ToRemString(property.Description)}
        /// </remarks>
        {Attribute(property)}
        public {property?.LastCsType ?? "int"} {property.Name}
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

        #region תForm


        private string ToForm()
        {
            var code = new StringBuilder();
            code.Append(@"
        #region תΪ����HTTPЭ���FORM���ı�
        /// <summary>תΪ����HTTPЭ���FORM���ı�</summary>
        /// <returns>����HTTPЭ���FORM���ı�</returns>
        public string ToFormString()
        {
            return $@""");
            

            bool isFirst = true;
            foreach (PropertyConfig property in Columns.Where(p => p.CanUserInput))
            {
                if (isFirst)
                    isFirst = false;
                else
                    code.AppendLine("&");
                if (property.CsType == "string")
                {
                    code.Append($"{property.Name}={{{property.Name}}}");
                }
                else
                {
                    code.Append($"{property.Name}={{{property.Name}}}");
                }
            }
            code.Append(@""";
        }
        #endregion");

            return code.ToString();
        }

        #endregion
        #region ����У��

        public string ValidateCode()
        {
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
            var result = new ValidateResult();{ValidateInner()}
            ValidateEx(result);
            return result;
        }}
        #endregion";
        }

        public string ValidateInner()
        {
            var code = new StringBuilder();
            var fields = Entity.PublishProperty.Where(p => p.CanUserInput).ToArray();
            foreach (PropertyConfig field in fields.Where(p => !string.IsNullOrWhiteSpace(p.EmptyValue)))
            {
                EntityValidateBuilder.ConvertEmptyValue(code, field);
            }

            foreach (PropertyConfig field in fields.Where(p => string.IsNullOrWhiteSpace(p.ExtendRole)))
            {
                switch (field.CsType)
                {
                    case "string":
                        EntityValidateBuilder.StringCheck(code, field);
                        continue;
                    case "int":
                    case "long":
                    case "decimal":
                        EntityValidateBuilder.NumberCheck(code, field);
                        break;
                    case "DateTime":
                        EntityValidateBuilder.DateTimeCheck(code, field);
                        break;
                }
            }
            return code.ToString();
        }
        
        #endregion
    }

}

