using System.IO;

namespace Agebull.EntityModel.RobotCoder
{

    /// <summary>
    /// 实体代码生成器
    /// </summary>
    public sealed class EntityBuilder : ModelCoderBase
    {
        /// <summary>
        /// 名称
        /// </summary>
        protected override string FileSaveConfigName => "File_Model_Entity_Base_cs";
        /// <summary>
        /// 是否客户端代码
        /// </summary>
        protected override bool IsClient => false;

        /// <summary>
        ///     生成实体代码
        /// </summary>
        protected override void CreateDesignerCode(string path)
        {
            if (Model.IsInterface)
                return;
            var fileName = ".Designer.cs";
            var file = Path.Combine(path, Model.EntityName + fileName);
            if (!string.IsNullOrWhiteSpace(Model.Alias))
            {
                var oldFile = Path.Combine(path, Model.Alias + fileName);
                if (File.Exists(oldFile))
                {
                    Directory.Move(oldFile, file);
                }
            }

            string code = $@"#region
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.Serialization;
using Newtonsoft.Json;

using Agebull.EntityModel.Common;
using Agebull.EntityModel.Interfaces;

{Project.UsingNameSpaces}
#endregion

namespace {NameSpace}
{{
    /// <summary>
    /// {Model.Description}
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, ItemNullValueHandling = NullValueHandling.Ignore)]
    public partial class {Model.EntityName} {ExtendInterface()}
    {{
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public {Model.EntityName}()
        {{
            Initialize();
            InitEntityEditStatus();
        }}

        /// <summary>
        /// 初始化
        /// </summary>
        partial void Initialize();
        partial void InitEntityEditStatus();

        #endregion
{GetBaseCode<EntityPropertyBuilder>()}

    }}
}}";
            SaveCode(file, code);
        }

        /// <summary>
        ///     生成扩展代码
        /// </summary>
        protected override void CreateCustomCode(string path)
        {
            if (Model.IsInterface)
                return;
            var fileName = ".cs";
            var file = Path.Combine(path, $"{Model.EntityName}{fileName}");
            if (!string.IsNullOrWhiteSpace(Model.Alias))
            {
                var oldFile = Path.Combine(path, Model.Alias + fileName);
                if (File.Exists(oldFile))
                {
                    Directory.Move(oldFile, file);
                }
            }
            string code = $@"#region using
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
#endregion

namespace {NameSpace}
{{
    /// <summary>
    /// {Model.Description}
    /// </summary>
    [DataContract]
    sealed partial class {Model.EntityName}
    {{
        
        /// <summary>
        /// 初始化
        /// </summary>
        partial void Initialize()
        {{
/*{ DefaultValueCode()}*/
        }}

    }}
}}";
            SaveCode(file, code);
        }

    }

}

