using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Agebull.EntityModel.RobotCoder
{


    public sealed class EntityBuilder : EntityCoderBase
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
        protected override void CreateBaCode(string path)
        {
            var fileName = ".Designer.cs";
            var file = Path.Combine(path, Entity.Name + fileName);
            if (!string.IsNullOrWhiteSpace(Entity.Alias))
            {
                var oldFile = Path.Combine(path, Entity.Alias + fileName);
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
    /// {Entity.Description}
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public partial class {Entity.EntityName} {ExtendInterface()}
    {{
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public {Entity.EntityName}()
        {{
            Initialize();
        }}

        /// <summary>
        /// 初始化
        /// </summary>
        partial void Initialize();
        #endregion
{GetBaseCode<EntityPropertyBuilder>()}
{FullCode()}
    }}
}}";
            SaveCode(file, code);
        }

        /// <summary>
        ///     生成扩展代码
        /// </summary>
        protected override void CreateExCode(string path)
        {
            var fileName = ".cs";
            var file = Path.Combine(path, Entity.Name + fileName);
            if (!string.IsNullOrWhiteSpace(Entity.Alias))
            {
                var oldFile = Path.Combine(path, Entity.Alias + fileName);
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
    /// {Entity.Description}
    /// </summary>
    [DataContract]
    sealed partial class {Entity.EntityName} : {(Entity.NoDataBase ? "NotificationObject" : "EditDataObject")}
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
            SaveCode(Path.Combine(path, Entity.Name + ".cs"), code);
        }


        /// <summary>
        ///     生成实体代码
        /// </summary>
        private string FullCode()
        {
           if (Entity.NoDataBase)
                return null;
            return $@"
{GetBaseCode<EntityDictionaryBuilder>()}
{GetBaseCode<EntityCopyBuilder>()}
{GetBaseCode<EntityLaterPeriodBuilder>()}
{GetBaseCode<EntityStructBuilder>()}";
        }

        private string ExtendInterface()
        {
            List<string> list = new List<string>();
            if (Entity.Interfaces != null)
            {
                list.AddRange(Entity.Interfaces.Split(NoneLanguageChar, StringSplitOptions.RemoveEmptyEntries));
            }
            //code.Append("IEntityPoolSetting");
            if (!Entity.NoDataBase && Entity.PrimaryColumn?.CsType == "long")
                list.Add("IIdentityData");
            //if (!Entity.IsLog)
            //{
            //    code.Append(" , IFieldJson , IPropertyJson");
            //}
            if (Entity.PrimaryColumn != null && Entity.PrimaryColumn.IsGlobalKey)
            {
                list.Add("IKey");
            }
            if (Entity.IsUniqueUnion)
            {
                list.Add("IUnionUniqueEntity");
            }

            //if (Entity.LastColumns.Any(p => p.IsUserId))
            //{
            //    code.Append(" , IUserChildEntity");
            //}
            return list.Count == 0 ? null : list.DistinctBy().LinkToString(" : ", " , ");
        }
    }

}

