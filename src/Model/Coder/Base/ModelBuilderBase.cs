using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;

namespace Agebull.EntityModel.RobotCoder
{
    /// <summary>
    /// 实体扩展代码生成基类
    /// </summary>
    public abstract class ModelBuilderBase : ModelCoderBase
    {
        #region 注册

        public string GetBaseCode(IEntityConfig config)
        {
            Model = config;
            Project = Model.Parent;
            return CreateBaCode();
        }

        public string GetExtendCode(IEntityConfig config)
        {
            Model = config;
            Project = Model.Parent;
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
        protected virtual string ClassHead => EntityRemHeader;

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
        protected override string Folder => null;


        /// <summary>
        ///     生成基本代码
        /// </summary>
        protected sealed override void CreateDesignerCode(string path)
        {
            SaveCode(Folder == null
                ? Path.Combine(path, Model.Name + ".Designer.cs")
                : Path.Combine(path, Folder, Model.Name + ".Designer.cs"), CreateBaCode());
        }

        /// <summary>
        ///     生成扩展代码
        /// </summary>
        protected sealed override void CreateCustomCode(string path)
        {

            SaveCode(Folder == null
                ? Path.Combine(path, Model.Name + ".cs")
                : Path.Combine(path, Folder, Model.Name + ".cs"), CreateExCode());
        }

        /// <summary>
        ///     生成基本代码
        /// </summary>
        protected virtual string CreateBaCode()
        {
            return $@"
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
using Agebull.EntityModel.Common;
using Agebull.EntityModel.Interfaces;

{Project.UsingNameSpaces}

{ExtendUsing}

namespace {NameSpace}
{{    
    {EntityRemHeader}
    partial class {Model.EntityName}
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

using Agebull.Common;
using Agebull.EntityModel.Common;
using Agebull.EntityModel.Interfaces;

{Project.UsingNameSpaces}

namespace {NameSpace}
{{
    {ClassHead}
    partial class {Model.EntityName}{ClassExtend}
    {{{ExtendCode}
    }}
}}";
        }

    }
}