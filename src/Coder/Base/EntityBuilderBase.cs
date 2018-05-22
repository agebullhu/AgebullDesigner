using System.IO;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;

namespace Agebull.EntityModel.RobotCoder
{
    /// <summary>
    /// 实体扩展代码生成基类
    /// </summary>
    public abstract class EntityBuilderBase : EntityCoderBase, IAutoRegister
    {
        #region 注册

        /// <summary>
        /// 执行自动注册
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
        /// 是否客户端代码
        /// </summary>
        protected override bool IsClient => false;

        /// <summary>
        /// 保存路径使用的名称
        /// </summary>
        protected sealed override string FileSaveConfigName => $"File_Model_Entity_{Folder}_cs";

        /// <summary>
        /// 扩展的Using
        /// </summary>
        protected virtual string ExtendUsing => null;

        /// <summary>
        /// 基本代码
        /// </summary>
        public abstract string BaseCode { get; }

        /// <summary>
        /// 扩展代码
        /// </summary>
        public virtual string ExtendCode => null;

        /// <summary>
        /// 类定义之前的代码
        /// </summary>
        protected virtual string ClassHead => $@"/* {Entity.Description}*/
";

        /// <summary>
        /// 类继承的扩展
        /// </summary>
        protected virtual string ClassExtend => null;

        /// <summary>
        /// 代码类型
        /// </summary>
        protected virtual string CodeType => "实体定义";

        /// <summary>
        /// 代码文件夹名称
        /// </summary>
        protected abstract string Folder { get; }


        /// <summary>
        ///     生成基本代码
        /// </summary>
        protected sealed override void CreateBaCode(string path)
        {
            SaveCode(Path.Combine(path, Folder, Entity.Name + ".Designer.cs"), CreateBaCode());
        }

        /// <summary>
        ///     生成扩展代码
        /// </summary>
        protected sealed override void CreateExCode(string path)
        {
            SaveCode(Path.Combine(path, Folder, Entity.Name + ".cs"), CreateExCode());
        }

        /// <summary>
        ///     生成基本代码
        /// </summary>
        protected virtual string CreateBaCode()
        {
            return $@"using System;
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
        ///     生成扩展代码
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