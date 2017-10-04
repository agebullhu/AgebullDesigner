using System.IO;
using Gboxt.Common.DataAccess.Schemas;

namespace Agebull.Common.SimpleDesign
{
    /// <summary>
    /// ʵ����չ�������ɻ���
    /// </summary>
    public abstract class EntityBuilderBase : EntityCoderBase, IAutoRegister
    {
        #region ע��

        /// <summary>
        /// ִ���Զ�ע��
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            MomentCoder.RegisteCoder(CodeType, "Action.cs", GetBaseCode);
            MomentCoder.RegisteCoder(CodeType, "Action.Designer.cs", GetExtendCode);
        }


        public string GetBaseCode(ConfigBase config)
        {
            Entity = config as EntityConfig;
            if (Entity == null)
                return null;
            Project = Entity.Parent;
            return CreateBaCode();
        }

        public string GetExtendCode(ConfigBase config)
        {
            Entity = config as EntityConfig;
            if (Entity == null)
                return null;
            Project = Entity.Parent;
            return CreateExCode();
        }
        #endregion

        /// <summary>
        /// �Ƿ�ͻ��˴���
        /// </summary>
        protected override bool IsClient => false;

        /// <summary>
        /// ����·��ʹ�õ�����
        /// </summary>
        protected sealed override string FileSaveConfigName => $"File_Model_Entity_{Folder}_cs";

        /// <summary>
        /// ��չ��Using
        /// </summary>
        protected virtual string ExtendUsing => null;

        /// <summary>
        /// ��������
        /// </summary>
        public abstract string BaseCode { get; }

        /// <summary>
        /// ��չ����
        /// </summary>
        public virtual string ExtendCode => null;

        /// <summary>
        /// �ඨ��֮ǰ�Ĵ���
        /// </summary>
        protected virtual string ClassHead => $@"/* {Entity.Description}*/
";

        /// <summary>
        /// ��̳е���չ
        /// </summary>
        protected virtual string ClassExtend => null;

        /// <summary>
        /// ��������
        /// </summary>
        protected virtual string CodeType => "ʵ�嶨��";

        /// <summary>
        /// �����ļ�������
        /// </summary>
        protected abstract string Folder { get; }


        /// <summary>
        ///     ���ɻ�������
        /// </summary>
        protected sealed override void CreateBaCode(string path)
        {
            SaveCode(Path.Combine(path, Folder, Entity.Name + ".Designer.cs"), CreateBaCode());
        }

        /// <summary>
        ///     ������չ����
        /// </summary>
        protected sealed override void CreateExCode(string path)
        {
            SaveCode(Path.Combine(path, Folder, Entity.Name + ".cs"), CreateExCode());
        }

        /// <summary>
        ///     ���ɻ�������
        /// </summary>
        protected virtual string CreateBaCode()
        {
            return $@"using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Runtime.Serialization;
using Agebull.Common;
using Gboxt.Common.DataModel;

{ExtendUsing}

namespace {NameSpace}
{{
    /* {Entity.Description}*/
    partial class {Entity.EntityName}
    {{{BaseCode}
    }}
}}";
        }

        /// <summary>
        ///     ������չ����
        /// </summary>
        public virtual string CreateExCode()
        {
            return $@"
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Gboxt.Common.DataModel;

namespace {NameSpace}
{{
    {ClassHead}partial class {Entity.EntityName}{ClassExtend}
    {{{ExtendCode}
    }}
}}";
        }

    }
}