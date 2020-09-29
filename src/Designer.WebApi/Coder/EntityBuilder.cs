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
        protected override void CreateDesignerCode(string path)
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
    {EntityRemHeader}
    [DataContract,JsonObject(MemberSerialization.OptIn)]
    public partial class {Model.Name}
    {{
        {Properties()}
    }}
}}";
            var file = ConfigPath(Project, FileSaveConfigName, path, Model.Classify, Model.Name);
            WriteFile(file + ".cs", code);
        }

        /// <summary>
        ///     ������չ����
        /// </summary>
        protected override void CreateCustomCode(string path)
        {
            string code = $@"using System;
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

using Agebull.EntityModel.Common;

{Project.UsingNameSpaces}

namespace {NameSpace}
{{
    sealed partial class {Model.Name} : IApiResultData , IApiArgument
    {{
        {ToData()}
        {ValidateCode()}
    }}
}}";
            var file = ConfigPath(Project, FileSaveConfigName, path, Model.Classify, Model.Name);
            WriteFile(file + ".api.cs", code);
        }


        #endregion

        #region ���Զ���


        private string Properties()
        {
            var code = new StringBuilder();
            foreach (var property in Columns)//.Where(p => !p.NoneApiArgument)
            {
                string type = property.DataType == "ByteArray" ? "string" : property.LastCsType;
                code.Append(PropertyHeader(property));
                code.Append($@"
        public {type} {property.Name}
        {{
            get;
            set;
        }}");
            }

            return code.ToString();
        }

        #endregion

        #region תת


        private string ToData()
        {
            if (Model.NoDataBase)
                return "";
            var code = new StringBuilder();
            int len = Columns.Max(p => p.Name.Length) + 2;
            code.Append($@"
        #region ���ڲ����ݿ������ת��

        /// <summary>תΪ�ڲ����ݿ����</summary>
        /// <returns>�ڲ����ݿ����</returns>
        public {Model.EntityName} ToData => new {Model.EntityName}
        {{");
            bool isFirst = true;
            foreach (var property in Columns.Where(p => !p.NoneApiArgument))
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
        public void FromData ({Model.EntityName} data) 
        {{");
            foreach (var property in Columns.Where(p => !p.NoneApiArgument))
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
        public static {Model.Name} ToArgument ({Model.EntityName} data) => new {Model.Name}
        {{");
            isFirst = true;
            foreach (var property in Columns.Where(p => !p.NoneApiArgument))
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
            var coder = new EntityValidateCoder { Model = Model };
            var code = coder.Code(Columns.Where(p => !p.NoneApiArgument));
            return $@"
        #region ����У��
        /// <summary>����У��</summary>
        /// <param name=""message"">���ص���Ϣ</param>
        /// <returns>�ɹ��򷵻���</returns>
        public bool Validate(out string message)
        {{
            var result = Validate();
            message = result.succeed ? null : result.Items.Where(p => !p.succeed).Select(p => $""{{p.Caption}}:{{ p.Message}}"").LinkToString(';');
            return true;//result.succeed;
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

