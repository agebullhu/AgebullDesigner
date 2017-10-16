using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Agebull.EntityModel.RobotCoder
{


    public sealed class EntityBuilder : EntityCoderBase
    {

        #region 主体代码

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
            string file = Path.Combine(path, Entity.Name + ".Designer.cs");

            string code = $@"using System;
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

//using {NameSpace}.DataAccess;
//using Gboxt.Common.DataModel.SqlServer;
using Newtonsoft.Json;

namespace {NameSpace}
{{
    /// <summary>
    /// {Entity.Description}
    /// </summary>
    [Table(""GL_OAuth_ClientManager"")]
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
            string code = $@"
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Gboxt.Common.DataModel;

namespace {NameSpace}
{{
    /// <summary>
    /// {Entity.Description}
    /// </summary>
    [DataContract]
    sealed partial class {Entity.EntityName} : {(Entity.IsClass ? "NotificationObject" : "EditDataObject")}
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
            if (Entity.IsClass)
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
            if (!Entity.IsClass)
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

        #endregion
    }

}

