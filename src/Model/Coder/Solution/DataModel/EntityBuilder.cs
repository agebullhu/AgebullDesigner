using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Agebull.Common.Defaults;
using Gboxt.Common.DataAccess.Schemas;

namespace Agebull.Common.SimpleDesign.WebApplition
{
    public sealed class EntityBuilder : EntityCoderBase
    {

        #region �������

        /// <summary>
        /// ����
        /// </summary>
        protected override string Name => "File_Model_Entity_Base_cs";
        /// <summary>
        /// �Ƿ�ͻ��˴���
        /// </summary>
        protected override bool IsClient => false;

        /// <summary>
        ///     ����ʵ�����
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
        #region ����
        
        /// <summary>
        /// ����
        /// </summary>
        public {Entity.EntityName}()
        {{
            Initialize();
        }}

        /// <summary>
        /// ��ʼ��
        /// </summary>
        partial void Initialize();
        #endregion


        #region ��������
{Properties()}
        #endregion
{FullCode()}
    }}
}}";
            SaveCode(file, code);
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

namespace {NameSpace}
{{
    /// <summary>
    /// {Entity.Description}
    /// </summary>
    [DataContract]
    sealed partial class {Entity.EntityName} : {(Entity.IsClass ? "NotificationObject" : "EditDataObject")}
    {{
        
        /// <summary>
        /// ��ʼ��
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
        ///     ����ʵ�����
        /// </summary>
        private string FullCode()
        {
            if (Entity.IsClass)
                return null;
            return $@"
        #region IIdentityData�ӿ�
{ ExtendProperty()}
        #endregion
        #region ������չ
{AccessProperties()}
{GetSetValues()}
        #endregion

        #region ����
{Releations()}
        #endregion

        #region ����
{Copy()}
        #endregion
{GetOnLaterPeriodBySignleModified()}
        #region ʵ��ṹ
{EntityStruct()}
        #endregion";
        }

        #endregion

        #region ��ѯ

        private string GetQueryCode()
        {
            StringBuilder code = new StringBuilder();
            if (this.Project.ReadOnly)
                code.AppendFormat(@"
        #region IEntityPoolSetting�ӿ�
        
        /// <summary>
        /// ��������
        /// </summary>
        HY.Model.EntityOption IEntityPoolSetting.Option()
        {{
            ReadOnlyEntityPool<{0}Entity>.CreateTableFunc = {0}DataAccess.CreateScope;
            return null;
        }}
        #endregion"
                    , Entity.Name);
            else
                code.AppendFormat(@"
        #region IAutoAddEntity�ӿ�
        
        /// <summary>
        /// ��������
        /// </summary>
        HY.Model.EntityOption IEntityPoolSetting.Option()
        {{
            EntityPool<{0}Entity>.CreateTableFunc = {0}DataAccess.CreateScope;
            return new HY.Model.EntityOption
            {{
                Storage         = HY.Model.EntityOption.StorageType.{2},
                RedisDbIndex    = {3}
            }};
        }}
        
        /// <summary>
        /// Redis�洢��
        /// </summary>
        [IgnoreDataMember]
        string __redisKey;

        /// <summary>
        /// Redis�洢��
        /// </summary>
        [IgnoreDataMember,Browsable(false)]
        string ICacheEntity.RedisKey
        {{
            get
            {{
                return this.__RedisKey;
            }}
        }}

        /// <summary>
        /// Redis�洢��
        /// </summary>
        [IgnoreDataMember,Browsable(false)]
        public string __RedisKey
        {{
            get
            {{
                return this.__redisKey ?? (this.__redisKey = RedisKeyBuilder.ToEntityKey(""{0}"", {1}));
            }}
        }}

        /// <summary>
        /// Redis�洢��
        /// </summary>
        [IgnoreDataMember, Browsable(false)]
        public string __RedisType
        {{
            get
            {{
                return EntityPool<{0}Entity>.PoolName;
            }}
        }}

        /// <summary>
        /// �����Redis����
        /// </summary>
        string IAutoAddEntity.RedisType
        {{
            get
            {{
                return EntityPool<{0}Entity>.PoolName;
            }}
        }}

        /// <summary>
        /// Sql Server�����б����
        /// </summary>
        TransList IAutoAddEntity.SqlSaveTransList
        {{
            get
            {{
                return null;//EntityPool<{0}Entity>.__SqlSaveTransList;
            }}
        }}

        #endregion"
                    , Entity.Name
                    , Entity.PrimaryColumn.Name
                    , "DataBase"
                    , Entity.DbIndex); ;
            code.Append(SqlServerQyeryCode());
            return code.ToString();
        }

        private string SqlServerQyeryCode()
        {
            return string.Format(@"

        #region ��չ�������
            
        /// <summary>
        /// ����
        /// </summary>
        public void Save()
        {{
            using(var scope = {0}DataAccess.CreateScope())
            {{
                scope.Entity.Save(this);
            }}
        }}
                    
        /// <summary>
        /// ȡ��һ��ID��Ӧ�Ķ���
        /// </summary>
        /// <param name=""id"">ID</param>
        /// <returns>�ջ���ҵ��Ķ���</returns>
        public static {0}Entity GetById(long id)
        {{
            using(var scope = {0}DataAccess.CreateScope())
            {{
                return scope.Entity.LoadByPrimaryKey(id);
            }}
        }}
            
        /// <summary>
        /// ȡ��һ��ID��Ӧ�Ķ���
        /// </summary>
        /// <param name=""ids"">ID����</param>
        /// <returns>��Ϊnull�ļ���</returns>
        public static List<{0}Entity> GetByIds(IEnumerable<long> ids)
        {{
            using(var scope = {0}DataAccess.CreateScope())
            {{
                return scope.Entity.LoadByPrimaryKeies(ids);
            }}
        }}
            
        /// <summary>
        /// ȡ�����ж���
        /// </summary>
        /// <returns>��Ϊnull�ļ���</returns>
        public static List<{0}Entity> GetAll()
        {{
            using(var scope = {0}DataAccess.CreateScope())
            {{
                return scope.Entity.All();
            }}
        }}

		/// <summary>
		/// ����һ������
		/// </summary>
		public static bool Update({0}Entity model)
        {{
            return model.Update();
        }}

		/// <summary>
		/// ����
		/// </summary>
		public bool Update()
        {{
            Save();
            return true;
        }}

		/// <summary>
		/// ��ҳ��ȡ�����б�
		/// </summary>
		public static {0}Entity GetModel(long id)
        {{
            return GetById(id);
        }}

        /// <summary>
        /// ����Ϊ������,������Ψһ����,����Ϊ��������
        /// </summary>
        public long SetIsAdded()
        {{
            using(var scope = {0}DataAccess.CreateScope())
            {{
                scope.Entity.Insert(this);
            }}
            return Id;
        }}


        /// <summary>
        /// ����Ϊ������,������Ψһ����,����Ϊ��������
        /// </summary>
        public static long Add({0}Entity value)
        {{
            return value.SetIsAdded();
        }}

        /// <summary>
        /// ȫ���ѯ
        /// </summary>
        /// <param name=""lambda"">��ѯ����</param>
        /// <returns>��Ϊnull�ļ���</returns>
        public static List<{0}Entity> GetModelList(Expression<Func<{0}Entity, bool>> lambda)
        {{
            using(var scope = {0}DataAccess.CreateScope())
            {{
                return scope.Entity.Select(lambda);
            }}
        }}

        /// <summary>
        /// ȫ���ѯ
        /// </summary>
        /// <param name=""lambda"">��ѯ����</param>
        /// <returns>��Ϊnull�ļ���</returns>
        public static List<{0}Entity> Find(Expression<Func<{0}Entity, bool>> lambda)
        {{
            using(var scope = {0}DataAccess.CreateScope())
            {{
                return scope.Entity.Select(lambda);
            }}
        }}

        /// <summary>
        /// ȫ���ѯ
        /// </summary>
        /// <param name=""lambda"">��ѯ����</param>
        /// <param name=""take"">Ҫ��������������</param>
        /// <returns>��Ϊnull�ļ���</returns>
        public static List<{0}Entity> GetModelList(Expression<Func<{0}Entity, bool>> lambda,int take)
        {{
            using(var scope = {0}DataAccess.CreateScope())
            {{
                return scope.Entity.Select(lambda);
            }}
        }}

        /// <summary>
        /// ȫ���ѯ
        /// </summary>
        /// <param name=""lambda"">��ѯ����</param>
        /// <returns>����Ϊ��</returns>
        public static {0}Entity First(Expression<Func<{0}Entity, bool>> lambda)
        {{
            using(var scope = {0}DataAccess.CreateScope())
            {{
                return scope.Entity.FirstOrDefault(lambda);
            }}
        }}

        /// <summary>
        /// ȫ���ѯ
        /// </summary>
        /// <param name=""lambda"">��ѯ����</param>
        /// <returns>��Ϊnull�ļ���</returns>
        public static {0}Entity FirstOrDefault(Expression<Func<{0}Entity, bool>> lambda)
        {{
            using(var scope = {0}DataAccess.CreateScope())
            {{
                return scope.Entity.FirstOrDefault(lambda);
            }}
        }}

        /// <summary>
        /// ȫ���ѯ
        /// </summary>
        /// <param name=""lambda"">��ѯ����</param>
        /// <returns>��Ϊnull�ļ���</returns>
        public static bool Any(Expression<Func<{0}Entity, bool>> lambda)
        {{
            using(var scope = {0}DataAccess.CreateScope())
            {{
                return scope.Entity.Any(lambda);
            }}
        }}

        /// <summary>
        /// ȫ���ѯ
        /// </summary>
        /// <param name=""lambda"">��ѯ����</param>
        /// <returns>��Ϊnull�ļ���</returns>
        public static long CountBy(Expression<Func<{0}Entity, bool>> lambda)
        {{
            using(var scope = {0}DataAccess.CreateScope())
            {{
                return scope.Entity.Count(lambda);
            }}
        }}

        #endregion
", Entity.Name);
        }

        #endregion

        #region ����
        /*

        private string FieldIndexs()
        {
            if (Entity.IsLog)
                return null;
            return !DataBase.ReadOnly ? RedisFieldIndexs() : MemoryFieldIndexs();
        }

        #region Memory

        private void MemoryIndexsMethod(ColumnSchema property, StringBuilder code)
        {
            code.AppendFormat(@"
                
        /// <summary>
        /// ����{5}�Ĳ���
        /// </summary>
        /// <param name=""{4}"">{5}</param>
        /// <param name=""take"">Ҫ��������������</param>
        /// <returns>���ҽ��</returns>
        public static List<{0}Entity> FindBy{2}({3} {4})
        {{
            return __Index_{1}[{4}];
        }}

        /// <summary>
        /// ����{1}�Ĳ���
        /// </summary>
        /// <param name=""{4}"">{5}</param>
        /// <param name=""lambda"">��ѯ���ʽ</param>
        /// <returns>���ҽ��</returns>
        public static IEnumerable<{0}Entity> FindBy{2}({3} {4}, Func<{0}Entity, bool> lambda)
        {{
            var ids = __Index_{1}[{4}];
            if (ids.Count == 0)
                return new List<{0}Entity>();
            return ids.Where(lambda);
        }}
        
        /// <summary>
        /// ����{1}�Ĳ���
        /// </summary>
        /// <param name=""{4}"">{5}</param>
        /// <returns>���ҽ��</returns>
        public static {0}Entity FirstOrDefaultBy{2}({3} {4})
        {{
            return __Index_{1}[{4}].FirstOrDefault();
        }}

        /// <summary>
        /// ����{1}�Ĳ���
        /// </summary>
        /// <param name=""{4}"">{5}</param>
        /// <param name=""lambda"">��ѯ���ʽ</param>
        /// <returns>���ҽ��</returns>
        public static {0}Entity FirstOrDefaultBy{2}({3} {4}, Func<{0}Entity, bool> lambda)
        {{
            return __Index_{1}[{4}].FirstOrDefault(lambda);
        }}
        
        /// <summary>
        /// ����{1}�Ĳ���
        /// </summary>
        /// <param name=""{4}"">{5}</param>
        /// <returns>���ҽ��</returns>
        public static bool AnyBy{2}({3} {4})
        {{
            return __Index_{1}[{4}].Count > 0;
        }}

        /// <summary>
        /// ����{1}�Ĳ���
        /// </summary>
        /// <param name=""{4}"">{5}</param>
        /// <param name=""lambda"">��ѯ���ʽ</param>
        /// <returns>���ҽ��</returns>
        public static bool AnyBy{2}({3} {4}, Func<{0}Entity, bool> lambda)
        {{
            var ids = __Index_{1}[{4}];
            return ids.Count > 0 && ids.Any(lambda);
        }}

        /// <summary>
        /// ��{1}��������ҵ�һ��
        /// </summary>
        /// <param name=""{4}"">{5}</param>
        /// <returns>���ҽ��</returns>
        public static {0}Entity RandomBy{2}({3} {4})
        {{
            var ids = __Index_{1}[{4}];
            if (ids.Count == 0)
                return null;
            Random random =  new Random((int)(DateTime.Now.Ticks % int.MaxValue));
            return ids[random.Next(0, ids.Count)];
        }}

        /// <summary>
        /// ��{1}��������ҵ�һ��
        /// </summary>
        /// <param name=""{4}"">{5}</param>
        /// <param name=""lambda"">��ѯ���ʽ</param>
        /// <returns>���ҽ��</returns>
        public static {0}Entity RandomBy{2}({3} {4}, Func<{0}Entity, bool> lambda)
        {{
            var ids = __Index_{1}[{4}];
            if (ids.Count == 0)
                return null;
            Random random =  new Random((int)(DateTime.Now.Ticks % int.MaxValue));
            return ids[random.Next(0, ids.Count)];
        }}", Entity.Name, property.Name, property.Name.ToUWord(), property.CsType, property.Name.ToLWord(), property.Caption);
        }


        private string MemoryFieldIndexs()
        {
            if (Entity.IsLog)
                return null;
            var code = new StringBuilder();

            code.Append(@"
        #region �ֶ�����");
            foreach (ColumnSchema property in Entity.LastColumns.Where(p => !p.IsPrimaryKey && (p.CreateIndex || p.UniqueIndex > 0)))
            {
                property.CreateIndex = true;
                code.AppendFormat(@"
        
        /// <summary>
        ///     {1}��Redis����
        /// </summary>
        public static readonly ReadMemoryIndex<{3}Entity,{2}> __Index_{0} = new ReadMemoryIndex<{3}Entity,{2}>
        {{
            GetEntityValue = e => e.{0}
        }};", property.Name, property.Caption, property.CsType, Entity.Name);
                if (property.UniqueString)
                    continue;
                MemoryIndexsMethod(property, code);
            }
            code.Append(@"

        #endregion");
            return code.ToString();
        }
        #endregion

        #region Redis
        
        private void RedisIndexsMethod(ColumnSchema property, StringBuilder code)
        {
            code.AppendFormat(@"
                
        /// <summary>
        /// ����{5}�Ĳ���
        /// </summary>
        /// <param name=""{4}"">{5}</param>
        /// <param name=""take"">Ҫ��������������</param>
        /// <returns>���ҽ��</returns>
        public static LazyEntityList<{0}Entity> FindBy{2}({3} {4})
        {{
            var ids = __Index_{1}.GetEqualsIds({4});
            if (ids.Count == 0)
                return LazyEntityList<{0}Entity>.Empty;
            return new LazyEntityList<{0}Entity>(ids);
        }}

        /// <summary>
        /// ����{1}�Ĳ���
        /// </summary>
        /// <param name=""{4}"">{5}</param>
        /// <param name=""lambda"">��ѯ���ʽ</param>
        /// <returns>���ҽ��</returns>
        public static IEnumerable<{0}Entity> FindBy{2}({3} {4}, Func<{0}Entity, bool> lambda)
        {{
            var ids = __Index_{1}.GetEqualsIds({4});
            if (ids.Count == 0)
                return new List<{0}Entity>();
            return new LazyEntityList<{0}Entity>(ids).Where(lambda);
        }}
        
        /// <summary>
        /// ����{1}�Ĳ���
        /// </summary>
        /// <param name=""{4}"">{5}</param>
        /// <returns>���ҽ��</returns>
        public static {0}Entity FirstOrDefaultBy{2}({3} {4})
        {{
            var ids = __Index_{1}.GetEqualsIds({4});
            return ids.Count == 0 ? null : GetById(ids[0]);
        }}

        /// <summary>
        /// ����{1}�Ĳ���
        /// </summary>
        /// <param name=""{4}"">{5}</param>
        /// <param name=""lambda"">��ѯ���ʽ</param>
        /// <returns>���ҽ��</returns>
        public static {0}Entity FirstOrDefaultBy{2}({3} {4}, Func<{0}Entity, bool> lambda)
        {{
            var ids = __Index_{1}.GetEqualsIds({4});
            if (ids.Count == 0)
                return null;
            return new LazyEntityList<{0}Entity>(ids).FirstOrDefault(lambda);
        }}
        
        /// <summary>
        /// ����{1}�Ĳ���
        /// </summary>
        /// <param name=""{4}"">{5}</param>
        /// <returns>���ҽ��</returns>
        public static bool AnyBy{2}({3} {4})
        {{
            var ids = __Index_{1}.GetEqualsIds({4});
            return ids.Count > 0;
        }}

        /// <summary>
        /// ����{1}�Ĳ���
        /// </summary>
        /// <param name=""{4}"">{5}</param>
        /// <param name=""lambda"">��ѯ���ʽ</param>
        /// <returns>���ҽ��</returns>
        public static bool AnyBy{2}({3} {4}, Func<{0}Entity, bool> lambda)
        {{
            var ids = __Index_{1}.GetEqualsIds({4});
            if (ids.Count == 0)
                return false;
            return GetByIds(ids).Any(lambda);
        }}

        /// <summary>
        /// ��{1}��������ҵ�һ��
        /// </summary>
        /// <param name=""{4}"">{5}</param>
        /// <returns>���ҽ��</returns>
        public static {0}Entity RandomBy{2}({3} {4})
        {{
            var ids = __Index_{1}.GetEqualsIds({4});
            if (ids.Count == 0)
                return null;
            Random random =  new Random((int)(DateTime.Now.Ticks % int.MaxValue));
            {0}Entity entitiy = null;
            int idx = 0;
            while (idx ++ < ids.Count)
            {{
                entitiy = GetById(ids[random.Next(0, ids.Count)]);
                if (entitiy != null)
                    return entitiy;
            }}
            return new LazyEntityList<{0}Entity>(ids)[0];
        }}

        /// <summary>
        /// ��{1}��������ҵ�һ��
        /// </summary>
        /// <param name=""{4}"">{5}</param>
        /// <param name=""lambda"">��ѯ���ʽ</param>
        /// <returns>���ҽ��</returns>
        public static {0}Entity RandomBy{2}({3} {4}, Func<{0}Entity, bool> lambda)
        {{
            var ids = __Index_{1}.GetEqualsIds({4});
            if (ids.Count == 0)
                return null;
            Random random =  new Random((int)(DateTime.Now.Ticks % int.MaxValue));
            {0}Entity entitiy = null;
            int idx = 0;
            while (idx ++ < ids.Count)
            {{
                entitiy = GetById(ids[random.Next(0, ids.Count)]);
                if (entitiy != null && lambda(entitiy))
                    return entitiy;
            }}
            return new LazyEntityList<{0}Entity>(ids).FirstOrDefault(lambda);
        }}", Entity.Name, property.Name, property.Name.ToUWord(), property.CsType, property.Name.ToLWord(), property.Caption);
        }


        private void RedisIndexsMethod2(ColumnSchema property, StringBuilder code)
        {
            code.AppendFormat(@"
                
        /// <summary>
        /// ����{5}�Ĳ���
        /// </summary>
        /// <param name=""{4}"">{5}</param>
        /// <param name=""take"">Ҫ��������������</param>
        /// <returns>���ҽ��</returns>
        public static LazyEntityList<{0}Entity> FindBy{2}(int uid,{3} {4})
        {{
            var ids = __Index_{1}.GetEqualsIds(uid , {4});
            if (ids.Count == 0)
                return LazyEntityList<{0}Entity>.Empty;
            return new LazyEntityList<{0}Entity>(ids);
        }}

        /// <summary>
        /// ����{1}�Ĳ���
        /// </summary>
        /// <param name=""{4}"">{5}</param>
        /// <param name=""lambda"">��ѯ���ʽ</param>
        /// <returns>���ҽ��</returns>
        public static IEnumerable<{0}Entity> FindBy{2}(int uid,{3} {4}, Func<{0}Entity, bool> lambda)
        {{
            var ids = __Index_{1}.GetEqualsIds(uid , {4});
            if (ids.Count == 0)
                return new List<{0}Entity>();
            return new LazyEntityList<{0}Entity>(ids).Where(lambda);
        }}
        
        /// <summary>
        /// ����{1}�Ĳ���
        /// </summary>
        /// <param name=""{4}"">{5}</param>
        /// <returns>���ҽ��</returns>
        public static {0}Entity FirstOrDefaultBy{2}(int uid,{3} {4})
        {{
            var ids = __Index_{1}.GetEqualsIds(uid , {4});
            return ids.Count == 0 ? null : GetById(ids[0]);
        }}

        /// <summary>
        /// ����{1}�Ĳ���
        /// </summary>
        /// <param name=""{4}"">{5}</param>
        /// <param name=""lambda"">��ѯ���ʽ</param>
        /// <returns>���ҽ��</returns>
        public static {0}Entity FirstOrDefaultBy{2}(int uid,{3} {4}, Func<{0}Entity, bool> lambda)
        {{
            var ids = __Index_{1}.GetEqualsIds(uid , {4});
            if (ids.Count == 0)
                return null;
            return new LazyEntityList<{0}Entity>(ids).FirstOrDefault(lambda);
        }}
        
        /// <summary>
        /// ����{1}�Ĳ���
        /// </summary>
        /// <param name=""{4}"">{5}</param>
        /// <returns>���ҽ��</returns>
        public static bool AnyBy{2}(int uid,{3} {4})
        {{
            var ids = __Index_{1}.GetEqualsIds(uid , {4});
            return ids.Count > 0;
        }}

        /// <summary>
        /// ����{1}�Ĳ���
        /// </summary>
        /// <param name=""{4}"">{5}</param>
        /// <param name=""lambda"">��ѯ���ʽ</param>
        /// <returns>���ҽ��</returns>
        public static bool AnyBy{2}(int uid,{3} {4}, Func<{0}Entity, bool> lambda)
        {{
            var ids = __Index_{1}.GetEqualsIds(uid , {4});
            if (ids.Count == 0)
                return false;
            return GetByIds(ids).Any(lambda);
        }}", Entity.Name, property.Name, property.Name.ToUWord(), property.CsType, property.Name.ToLWord(), property.Caption);
        }

        private string RedisFieldIndexs()
        {
            if (Entity.IsLog)
                return null;
            var code = new StringBuilder();

            code.Append(@"
        #region �ֶ�����");
            bool isUser = Entity.LastColumns.Any(p => p.IsUserId);
            foreach (ColumnSchema property in Entity.LastColumns.Where(p => !p.IsPrimaryKey && (p.CreateIndex || p.UniqueIndex > 0)))
            {
                property.CreateIndex = true;
                string type = null;
                if (property.UniqueString)
                {
                    type = "EntityUniqueStringIndex";
                }
                else if (string.Equals(property.CsType, "string", StringComparison.OrdinalIgnoreCase))
                {
                    type = "EntityFieldStringIndex";
                }
                else if (string.Equals(property.CsType, "int", StringComparison.OrdinalIgnoreCase) ||
                         string.Equals(property.CsType, "long", StringComparison.OrdinalIgnoreCase))
                {
                    type = "EntityFieldIntIndex";
                }
                else if (string.Equals(property.CsType, "DateTime", StringComparison.OrdinalIgnoreCase))
                {
                    type = "EntityFieldDateIndex";
                }
                else if (string.Equals(property.CsType, "Decimal", StringComparison.OrdinalIgnoreCase))
                {
                    type = "EntityFieldDecimalIndex";
                }
                else
                {
                    type = "EntityFieldDoubleIndex";
                }
                code.AppendFormat(@"
        
        /// <summary>
        ///     {2}��Redis����
        /// </summary>
        public static readonly {3} __Index_{1} = new {3}
        {{
            Name = ""{0}"",
            FieldName  = ""{1}"",
            IsReadOnly = {4},
            Db         = {5}
        }};", Entity.Name, property.Name, property.Caption, type, DataBase.ReadOnly ? "true" : "false", Entity.DbIndex);
                if (property.UniqueString)
                    continue;
                if (property.IsUserId || !isUser || property.UniqueIndex == 0 || property.CsType != "int")
                    RedisIndexsMethod(property, code);
                else
                    RedisIndexsMethod2(property, code);
            }
            code.Append(@"

        #endregion");
            return code.ToString();
        }
        #endregion
        
        */
        #region SqlServer

        private void SqlServerIndexsMethod(PropertyConfig property, StringBuilder code)
        {
            code.AppendFormat(@"
                
        /// <summary>
        /// ����{5}�Ĳ���
        /// </summary>
        /// <param name=""{4}"">{5}</param>
        /// <param name=""take"">Ҫ��������������</param>
        /// <returns>���ҽ��</returns>
        public static LazyEntityList<{0}Entity> FindBy{2}({3} {4})
        {{
            using(var scope = {0}DataAccess.CreateScope())
            {{
                var list = scope.Entity.Select(p=>p.{2} == {4});
                return new LazyEntityList<{0}Entity>(list);
            }}
        }}

        /// <summary>
        /// ����{1}�Ĳ���
        /// </summary>
        /// <param name=""{4}"">{5}</param>
        /// <param name=""lambda"">��ѯ���ʽ</param>
        /// <returns>���ҽ��</returns>
        public static List<{0}Entity> FindBy{2}({3} {4}, Func<{0}Entity, bool> lambda)
        {{
            using(var scope = {0}DataAccess.CreateScope())
            {{
                return scope.Entity.Select(p=>p.{2} == {4},lambda);
            }}
        }}
        
        /// <summary>
        /// ����{1}�Ĳ���
        /// </summary>
        /// <param name=""{4}"">{5}</param>
        /// <returns>���ҽ��</returns>
        public static {0}Entity FirstOrDefaultBy{2}({3} {4})
        {{
            using(var scope = {0}DataAccess.CreateScope())
            {{
                return scope.Entity.FirstOrDefaultBy(p=>p.{2} == {4});
            }}
        }}

        /// <summary>
        /// ����{1}�Ĳ���
        /// </summary>
        /// <param name=""{4}"">{5}</param>
        /// <param name=""lambda"">��ѯ���ʽ</param>
        /// <returns>���ҽ��</returns>
        public static {0}Entity FirstOrDefaultBy{2}({3} {4}, Func<{0}Entity, bool> lambda)
        {{
            using(var scope = {0}DataAccess.CreateScope())
            {{
                return scope.Entity.FirstOrDefaultBy(p=>p.{2} == {4},lambda);
            }}
        }}
        
        /// <summary>
        /// ����{1}�Ĳ���
        /// </summary>
        /// <param name=""{4}"">{5}</param>
        /// <returns>���ҽ��</returns>
        public static bool AnyBy{2}({3} {4})
        {{
            using(var scope = {0}DataAccess.CreateScope())
            {{
                return scope.Entity.Any(p=>p.{2} == {4});
            }}
        }}

        /// <summary>
        /// ����{1}�Ĳ���
        /// </summary>
        /// <param name=""{4}"">{5}</param>
        /// <param name=""lambda"">��ѯ���ʽ</param>
        /// <returns>���ҽ��</returns>
        public static bool AnyBy{2}({3} {4}, Func<{0}Entity, bool> lambda)
        {{
            using(var scope = {0}DataAccess.CreateScope())
            {{
                return scope.Entity.Any(p=>p.{2} == {4} ,lambda);
            }}
        }}

        /// <summary>
        /// ��{1}��������ҵ�һ��
        /// </summary>
        /// <param name=""{4}"">{5}</param>
        /// <returns>���ҽ��</returns>
        public static {0}Entity RandomBy{2}({3} {4})
        {{
            using(var scope = {0}DataAccess.CreateScope())
            {{
                return scope.Entity.FirstOrDefault(p=>p.{2} == {4});
            }}
        }}

        /// <summary>
        /// ��{1}��������ҵ�һ��
        /// </summary>
        /// <param name=""{4}"">{5}</param>
        /// <param name=""lambda"">��ѯ���ʽ</param>
        /// <returns>���ҽ��</returns>
        public static {0}Entity RandomBy{2}({3} {4}, Func<{0}Entity, bool> lambda)
        {{
            using(var scope = {0}DataAccess.CreateScope())
            {{
                return scope.Entity.FirstOrDefault(p=>p.{2} == {4},lambda);
            }}
        }}", Entity.Name, property.Name, property.Name.ToUWord()
           , property.CsType, property.Name.ToLWord(), property.Caption);
        }

        #endregion

        #endregion

        #region ��չ

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

        private string ExtendProperty()
        {
            var code = new StringBuilder();
            if (Entity.PrimaryColumn != null)
            {
                if (Entity.PrimaryColumn.Name != "Id")
                {
                    code.AppendFormat(@"

        /// <summary>
        /// �����ʶ
        /// </summary>
        [IgnoreDataMember,Browsable(false)]
        public {0} Id
        {{
            get
            {{
                return this.{1};
            }}
            set
            {{
                this.{1} = value;
            }}
        }}", Entity.PrimaryColumn.LastCsType, Entity.PrimaryColumn.Name);
                }

                code.AppendFormat(@"

        /// <summary>
        /// Id��
        /// </summary>
        int IIdentityData.Id
        {{
            get
            {{
                return (int)this.{1};
            }}
            set
            {{
                this.{1} = {0}value;
            }}
        }}", Entity.PrimaryColumn.LastCsType == "int" ? string.Empty : "(int)", Entity.PrimaryColumn.Name);
                if (Entity.PrimaryColumn.IsGlobalKey)
                {
                    code.AppendFormat(@"

        /// <summary>
        /// Key��
        /// </summary>
        Guid IKey.Key
        {{
            get
            {{
                return this.{0};
            }}
        }}", Entity.PrimaryColumn.Name);
                }
            }
            IEnumerable<PropertyConfig> idp = Columns.Where(p => p.UniqueIndex > 0);
            var columnSchemata = idp as PropertyConfig[] ?? idp.ToArray();
            int cnt = columnSchemata.Count();
            if (cnt <= 1)
            {
                return code.ToString();
            }
            code.Append(@"

        /// <summary>
        /// ��Ϻ��Ψһֵ
        /// </summary>
        public string UniqueValue
        {
            get
            {
                return string.Format(""");

            for (int idx = 0; idx < cnt; idx++)
            {
                if (idx > 0)
                    code.Append(':');
                code.AppendFormat("{{{0}}}", idx);
            }
            code.AppendFormat("\" ");
            foreach (PropertyConfig property in columnSchemata.OrderBy(p => p.UniqueIndex))
            {
                code.AppendFormat(" , {0}", property.Name);
            }
            code.Append(@");
            }
        }");
            return code.ToString();
        }

        #endregion

        #region ��ϵ

        /// <summary>
        ///     ��Friend��ƽ�ȹ�ϵ��1��1
        /// </summary>
        /// <param name="releation"></param>
        /// <returns></returns>
        private string Releation1V1Code(TableReleation releation)
        {
            return string.Format(@"

        /// <summary>
        /// {0}������
        /// </summary>
        [IgnoreDataMember]
        private ObjectFriend<{1}, {1}DataAccess> _{1}Helper;

        /// <summary>
        /// {0}������
        /// </summary>
        [IgnoreDataMember,Browsable(false)]
        public ObjectFriend<{1}, {1}DataAccess> {1}Helper
        {{
            get
            {{
                return this._{1}Helper ?? (this._{1}Helper = new ObjectFriend<{1}, {1}DataAccess>(this,""{3}"",""{4}"",true,{6}.Default.{5}));
            }}
        }}

        /// <summary>
        /// {0}
        /// </summary>
        [IgnoreDataMember,Browsable(false)]
        public {1} {2}
        {{
            get
            {{
                return this.{1}Helper.Friend;
            }}
        }}"
                , releation.Description
                , releation.Friend
                , releation.Name ?? releation.Friend
                , releation.PrimaryKey
                , releation.ForeignKey
                , releation.Friend.ToPluralism()
                , this.Project.DataBaseObjectName);
        }


        private string Releationx1V1reversionCode(TableReleation releation, EntityConfig friend)
        {
            return string.Format(@"

        /// <summary>
        /// {0}������
        /// </summary>
        [IgnoreDataMember]
        private ObjectFriend<{1}, {1}DataAccess> _{1}Helper;

        /// <summary>
        /// {0}������
        /// </summary>
        [IgnoreDataMember,Browsable(false)]
        public ObjectFriend<{1}, {1}DataAccess> {1}Helper
        {{
            get
            {{
                return this._{1}Helper ?? (this._{1}Helper = new ObjectFriend<{1}, {1}DataAccess>(this,""{2}"",""{3}"",{4},{6}.Default.{5}));
            }}
        }}

        /// <summary>
        /// {0}
        /// </summary>
        [IgnoreDataMember,Browsable(false)]
        public {1} {1}
        {{
            get
            {{
                return this.{1}Helper.Friend;
            }}
        }}"
                , friend.Description
                , friend.Name
                , releation.ForeignKey
                , releation.PrimaryKey
                , releation.Releation == 1 ? "false" : "true"
                , friend.Name.ToPluralism()
                , this.Project.DataBaseObjectName);
        }

        private string ReleationCode2(TableReleation releation)
        {
            return string.Format(@"

        /// <summary>
        /// {0}
        /// </summary>
        [IgnoreDataMember]
        private DataChildren<{1}, {1}DataAccess> _{1}DataView;

        /// <summary>
        /// {0}������
        /// </summary>
        [IgnoreDataMember,Browsable(false)]
        public DataChildren<{1}, {1}DataAccess> {1}DataView
        {{
            get
            {{
                return this._{1}DataView ?? (this._{1}DataView = new DataChildren<{1}, {1}DataAccess>(this,""{3}"",""{4}"",{5},{7}.Default.{6}));
            }}
        }}

        /// <summary>
        /// {0}
        /// </summary>
        [IgnoreDataMember,Browsable(false)]
        public bool Hase{2}
        {{
            get
            {{
                return this.{1}DataView.HaseValues();
            }}
        }}

        /// <summary>
        /// {0}
        /// </summary>
        [IgnoreDataMember,Browsable(false)]
        public ObservableCollection<{1}> {2}
        {{
            get
            {{
                if (!this.{1}DataView._isLoaded)
                    this.{1}DataView.LoadValues();
                return this.{1}DataView;
            }}
        }}"
                , releation.Description
                , releation.Friend
                , releation.Name.ToPluralism()
                , releation.PrimaryKey
                , releation.ForeignKey
                , string.IsNullOrEmpty(releation.Condition) ? "null" : $@"""{releation.Condition}"""
                , releation.Friend.ToPluralism()
                , this.Project.DataBaseObjectName);
        }


        private string Releations()
        {
            var code = new StringBuilder();
            foreach (TableReleation rl in Entity.Releations)
            {
                TableReleation releation = rl;
                EntityConfig friend = Entities.FirstOrDefault(p => p.Name == releation.Friend);
                if (friend == null)
                    continue;
                switch (releation.Releation)
                {
                    case 0:
                    case 1:
                        code.Append(Releation1V1Code(releation));
                        break;
                    case 2:
                        code.Append(ReleationCode2(releation));
                        break;
                }
            }
            foreach (EntityConfig friend in Entities.Where(p => p != Entity))
            {
                foreach (TableReleation releation in friend.Releations.Where(p => p.Friend == Entity.Name))
                {
                    code.Append(Releationx1V1reversionCode(releation, friend));
                }
            }
            return code.ToString();
        }

        #endregion

        #region ����

        private string PrimaryKeyPropertyCode()
        {
            var property = Entity.PrimaryColumn;
            if (property == null)
                return null;//"\nû�����������ֶΣ����ɵĴ����Ǵ����";
            return string.Format(@"

        /// <summary>
        /// �޸�����
        /// </summary>
        public void ChangePrimaryKey({3} {1})
        {{
            _{1} = {1};
        }}
        
        /// <summary>
        /// {0}��ʵʱ��¼˳��
        /// </summary>
        internal const int Real_{2} = 0;

        /// <summary>
        /// {0}
        /// </summary>
        [DataMember,JsonIgnore]
        internal {3} _{1};

        partial void On{2}Get();

        partial void On{2}Set(ref {3} value);

        partial void On{2}Load(ref {3} value);

        partial void On{2}Seted();

        /// <summary>
        /// {0}
        /// </summary>
        /// <remarks>
        /// {5}
        /// </remarks>
        {4}[Key]
        public {3} {2}
        {{
            get
            {{
                On{2}Get();
                return this._{1};
            }}
            set
            {{
                if(this._{1} == value)
                    return;
                //if(this._{1} > 0)
                //    throw new Exception(""����һ�����þͲ������޸�"");
                On{2}Set(ref value);
                this._{1} = value;
                {6}this.OnPropertyChanged(Real_{2});
                On{2}Seted();
            }}
        }}"
                        , ToRemString(property.Caption + ":" + property.Description)
                        , property.Name.ToLower()
                        , property.Name
                        , property.LastCsType
                        , Attribute(property)
                        , ToRemString(property.Description)
                        , null/*Entity.UpdateByModified ? "//" : property.IsIdentity ? "//" : ""*/);
        }

        private void PropertyCode(PropertyConfig property, int index, StringBuilder code)
        {
            code.AppendFormat(@"
        /// <summary>
        /// {0}��ʵʱ��¼˳��
        /// </summary>
        internal const int Real_{2} = {6};

        /// <summary>
        /// {0}
        /// </summary>
        [DataMember,JsonIgnore]
        internal {3} _{1};

        partial void On{2}Get();

        partial void On{2}Set(ref {3} value);

        partial void On{2}Seted();

        /// <summary>
        /// {0}
        /// </summary>
        /// <remarks>
        /// {5}
        /// </remarks>
        {4}
        {7} {3} {2}
        {{
            get
            {{
                On{2}Get();
                return this._{1};
            }}
            set
            {{
                if(this._{1} == value)
                    return;
                On{2}Set(ref value);
                this._{1} = value;
                On{2}Seted();
                {8}this.OnPropertyChanged(Real_{2});
            }}
        }}", ToRemString(property.Caption + ":" + property.Description), property.Name.ToLower(), property.Name, property.LastCsType, Attribute(property), ToRemString(property.Description), index, property.AccessType, null
/*Entity.UpdateByModified ? "//" : ""*/);
            EnumContentProperty(property, code);
        }

        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="property"></param>
        /// <param name="index"></param>
        /// <param name="code"></param>
        private void ComputePropertyCode(PropertyConfig property, int index, StringBuilder code)
        {
            EnumContentProperty(property, code);
            code.Append($@"
        /// <summary>
        /// {ToRemString(property.Caption + ":" + property.Description)}��ʵʱ��¼˳��
        /// </summary>
        internal const int Real_{property.Name} = {index};
");
            if (string.IsNullOrWhiteSpace(property.ComputeGetCode) && string.IsNullOrWhiteSpace(property.ComputeSetCode))
            {
                code.Append($@"
        /// <summary>
        /// {ToRemString(property.Caption + ":" + property.Description)}
        /// </summary>
        [DataMember,JsonIgnore]
        internal {property.LastCsType} _{property.Name.ToLower()};

        /// <summary>
        /// {ToRemString(property.Caption + ":" + property.Description)}
        /// </summary>
        /// <remarks>
        /// {ToRemString(property.Description)}
        /// </remarks>
        {Attribute(property)}
        {property.AccessType} {property.LastCsType} {property.Name}
        {{
            get
            {{
                return this._{property.Name.ToLower()};
            }}
            set
            {{
                this._{property.Name.ToLower()} = value;
            }}
        }}");
            }
            else if (string.IsNullOrWhiteSpace(property.ComputeSetCode))
            {
                code.Append($@"
        /// <summary>
        /// {ToRemString(property.Caption + ":" + property.Description)}
        /// </summary>
        /// <remarks>
        /// {ToRemString(property.Description)}
        /// </remarks>
        {Attribute(property)}
        {property.AccessType} {property.LastCsType} {property.Name}
        {{
            get
            {{
                {property.ComputeGetCode}
            }}
        }}");
            }
            else if (string.IsNullOrWhiteSpace(property.ComputeGetCode))
            {
                code.Append($@"
        /// <summary>
        /// {ToRemString(property.Caption)}
        /// </summary>
        /// <remarks>
        /// {ToRemString(property.Description)}
        /// </remarks>
        {Attribute(property)}
        {property.AccessType} {property.LastCsType} {property.Name}
        {{
            set
            {{
                {property.ComputeSetCode}
            }}
        }}");
            }
            else
            {
                code.Append($@"
        /// <summary>
        /// {ToRemString(property.Caption + ":" + property.Description)}
        /// </summary>
        /// <remarks>
        /// {ToRemString(property.Description)}
        /// </remarks>
        {Attribute(property)}
        {property.AccessType} {property.LastCsType} {property.Name}
        {{
            set
            {{
                {property.ComputeSetCode}
            }}
            get
            {{
                {property.ComputeGetCode}
            }}
        }}");
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="property"></param>
        /// <param name="code"></param>
        private void AliasPropertyCode(PropertyConfig property, StringBuilder code)
        {
            if (property == null)
                return;
            foreach (var alias in property.GetAliasPropertys())
            {
                code.Append($@"
        /// <summary>
        /// {ToRemString(property.Caption + ":" + property.Description)}
        /// </summary>
        /// <remarks>
        /// {ToRemString(property.Description)}
        /// </remarks>
        {Attribute(property).Replace($"\"{property.Name}\"", $"\"{alias}\"")}
        public {property.LastCsType} {alias}
        {{
            get
            {{
                return this.{property.Name};
            }}
            set
            {{
                this.{property.Name} = value;
            }}
        }}");
            }
        }
        private string Properties()
        {
            var code = new StringBuilder();
            code.Append(PrimaryKeyPropertyCode());
            AliasPropertyCode(Entity.PrimaryColumn, code);
            int index = 1;
            foreach (PropertyConfig property in Columns.Where(p => !p.IsPrimaryKey))
            {
                if (property.IsCompute)
                    ComputePropertyCode(property, index++, code);
                else
                    PropertyCode(property, index++, code);
                AliasPropertyCode(property, code);
            }
            foreach (PropertyConfig property in Entity.Properties.Where(p => !p.Discard && p.DbInnerField))
            {
                this.DbInnerProperty(property, code);
            }
            return code.ToString();
        }
        private void DbInnerProperty(PropertyConfig property, StringBuilder code)
        {
            code.Append($@"

        /// <summary>
        /// {ToRemString(property.Caption + ":" + property.Description)}
        /// </summary>
        /// <remarks>
        /// �������ڲ�ѯ��Lambda���ʽʹ��
        /// </remarks>
        [IgnoreDataMember , Browsable(false),JsonIgnore]
        public {property.LastCsType} {property.Name}
        {{
            get
            {{
                throw new Exception(""{ToRemString(property.Caption + ":" + property.Description)}���Խ������ڲ�ѯ��Lambda���ʽʹ��"");
            }}
        }}");
        }

        private string AccessProperties()
        {
            var code = new StringBuilder();
            foreach (PropertyConfig property in Entity.Properties.Where(p => !string.IsNullOrWhiteSpace(p.StorageProperty)))
            {
                code.Append($@"

        partial void On{property.StorageProperty}Get();

        partial void On{property.StorageProperty}Set(ref string value);

        partial void On{property.StorageProperty}Seted();

        /// <summary>
        /// {ToRemString(property.Caption + ":" + property.Description)}�Ĵ洢ֵ��д�ֶ�
        /// </summary>
        /// <remarks>
        /// ���洢ʹ��
        /// </remarks>
        [DataMember , Browsable(false),JsonIgnore]
        internal string {property.StorageProperty}
        {{
            get
            {{
                On{property.StorageProperty}Get();
                return this.{property.Name} == null ? null : IOHelper.XMLSerializer(this.{property.Name});
            }}
            set
            {{
                On{property.StorageProperty}Set(ref value);
                this.{property.Name} = IOHelper.XMLDeSerializer<{property.LastCsType}>(value);
                On{property.StorageProperty}Seted();
            }}
        }}");
            }
            return code.ToString();
        }

        private string GetOnLaterPeriodBySignleModified()
        {
            var code = new StringBuilder();
            code.Append(@"
        #region ���ڴ���");

            code.Append(@"

        /// <summary>
        /// ���������޸ĵĺ��ڴ���(�����)
        /// </summary>
        /// <param name=""subsist"">��ǰʵ������״̬</param>
        /// <param name=""modifieds"">�޸��б�</param>
        /// <remarks>
        /// �Ե�ǰ��������Եĸ���,�����б���,���򽫶�ʧ
        /// </remarks>
        protected override void OnLaterPeriodBySignleModified(EntitySubsist subsist,byte[] modifieds)
        {");
            if (!string.IsNullOrWhiteSpace(Entity.ModelBase))
                code.AppendLine(@"
            base.OnLaterPeriodBySignleModified(subsist,modifieds);");

            code.Append(@"
            if (subsist == EntitySubsist.Deleting)
            {");
            foreach (PropertyConfig property in ReadWriteColumns)
            {
                code.Append($@"
                On{property.Name}Modified(subsist,false);");
                //if (!Entity.IsLog && property.CreateIndex && !property.IsPrimaryKey)
                //{
                //    if (DataBase.ReadOnly)
                //        code.AppendFormat(@"
                //__Index_{0}.RemoveIndex(this);", property.Name);
                //    else
                //        code.AppendFormat(@"
                //__Index_{0}.RemoveIndex(Id);", property.Name);
                //}
            }
            code.Append(@"
                return;
            }
            else if (subsist == EntitySubsist.Adding || subsist == EntitySubsist.Added)
            {");
            foreach (PropertyConfig property in ReadWriteColumns)
            {
                code.AppendFormat(@"
                On{0}Modified(subsist,true);", property.Name);
                //if (Entity.IsLog || !property.CreateIndex || property.IsPrimaryKey)
                //    continue;
                //if (DataBase.ReadOnly)
                //    code.AppendFormat(@"
                //__Index_{0}.AddToIndex(this);", property.Name);
                //else if (property.UniqueIndex == 0 || property.CsType != "int")
                //    code.AppendFormat(@"
                //__Index_{0}.AddToIndex(Id,{0});", property.Name);
                //else
                //    code.AppendFormat(@"
                //__Index_{0}.AddToIndex(Id,UId , {0});", property.Name);
            }
            code.Append($@"
                return;
            }}
            else if(modifieds != null && modifieds[{Columns.Length}] > 0)
            {{");

            foreach (PropertyConfig property in ReadWriteColumns)
            {
                code.Append($@"
                On{property.Name}Modified(subsist,modifieds[Real_{property.Name}] == 1);");

                //code.AppendFormat(@"
                //if(modifieds[Real_{0}] == 1)
                //{{
                //    On{0}Modified(subsist,modifieds[Real_{0}] == 1);", property.Name);
                //if (!Entity.IsLog && property.CreateIndex && !property.IsPrimaryKey)
                //{
                //    if (DataBase.ReadOnly)
                //        code.AppendFormat(@"
                //    __Index_{0}.AddToIndex(this);", property.Name);
                //    else if (property.UniqueIndex == 0 || property.CsType != "int")
                //        code.AppendFormat(@"
                //    __Index_{0}.AddToIndex(Id,{0});", property.Name);
                //    else
                //        code.AppendFormat(@"
                //    __Index_{0}.AddToIndex(Id,UId , {0});", property.Name);
                //}
                //code.Append(@"
                //}");
            }
            code.Append(@"
            }
        }

        #region ���Ժ����޸ĵķֲ�����");
            foreach (PropertyConfig property in ReadWriteColumns)
            {
                code.AppendFormat(@"

        /// <summary>
        /// {1}�޸ĵĺ��ڴ���(����ǰ)
        /// </summary>
        /// <param name=""subsist"">��ǰ����״̬</param>
        /// <param name=""isModified"">�Ƿ��޸�</param>
        /// <remarks>
        /// �Թ��������Եĸ���,�����б���,������ܶ�ʧ
        /// </remarks>
        partial void On{0}Modified(EntitySubsist subsist,bool isModified);", property.Name, property.Caption);
            }
            code.AppendLine(@"

        #endregion

        #endregion");
            return code.ToString();
        }



        #endregion
        
        #region ����

        private string Copy()
        {
            var code = new StringBuilder();
            var type = IsClient ? "EntityObjectBase" : "DataObjectBase";
            code.Append($@"

        partial void CopyExtendValue({Entity.EntityName} source);

        /// <summary>
        /// ����ֵ
        /// </summary>
        /// <param name=""source"">���Ƶ�Դ�ֶ�</param>
        protected override void CopyValueInner({type} source)
        {{");

            if (!string.IsNullOrWhiteSpace(Entity.ModelBase))
                code.AppendLine(@"
            base.CopyValueInner(source);");

            code.Append($@"
            var sourceEntity = source as {Entity.EntityName};
            if(sourceEntity == null)
                return;");

            foreach (PropertyConfig property in ReadWriteColumns)
            {
                if (property.IsCustomCompute)
                {
                    if (property.CanSet)
                        code.Append($@"
            this.{property.Name} = sourceEntity.{property.Name};");
                    continue;
                }
                code.Append($@"
            this._{property.Name.ToLower()} = sourceEntity._{property.Name.ToLower()};");
            }
            code.Append(@"
            CopyExtendValue(sourceEntity);");
            if (!IsClient)
                code.Append(@"
            this.__EntityStatus.SetModified();");
            code.Append(@"
        }");
            //return code.ToString();
            code.Append($@"

        /// <summary>
        /// ����
        /// </summary>
        /// <param name=""source"">���Ƶ�Դ�ֶ�</param>
        public void Copy({Entity.EntityName} source)
        {{");

            if (!string.IsNullOrWhiteSpace(Entity.ModelBase))
                code.AppendLine(@"
            base.CopyValueInner(source);");


            foreach (PropertyConfig property in ReadWriteColumns)
            {
                code.Append($@"
                this.{property.Name} = source.{property.Name};");
            }
            code.Append(@"
        }");


            return code.ToString();
        }
        #endregion

        #region ���ݽṹ

        private string EntityStruct()
        {
            if (Entity.PrimaryColumn == null)
                return null;
            bool isFirst = true;
            var code = new StringBuilder();
            var code2 = new StringBuilder();
            EntityStruct(Entity, code, code2, ref isFirst);
            return $@"
        {code2}

        /// <summary>
        /// ʵ��ṹ
        /// </summary>
        [IgnoreDataMember,Browsable (false)]
        public override EntitySturct __Struct
        {{
            get
            {{
                return __struct;
            }}
        }}

        /// <summary>
        /// ʵ��ṹ
        /// </summary>
        [IgnoreDataMember]
        static readonly EntitySturct __struct = new EntitySturct
        {{
            EntityName = ""{Entity.Name}"",
            PrimaryKey = ""{Entity.PrimaryColumn.Name}"",
            EntityType = 0x{Entity.Identity:X},
            Properties = new Dictionary<int, PropertySturct>
            {{{code}
            }}
        }};
";
        }

        private void EntityStruct(EntityConfig table, StringBuilder code, StringBuilder code2, ref bool isFirst)
        {
            if (table == null)
                return;
            if (!string.IsNullOrEmpty(table.ModelBase))
                EntityStruct(this.Project.Entities.FirstOrDefault(p => p.Name == table.ModelBase), code, code2, ref isFirst);

            foreach (PropertyConfig property in table.PublishProperty)
            {
                if (isFirst)
                    isFirst = false;
                else
                    code.Append(',');

                code.AppendFormat(@"
                {{
                    Real_{0},
                    new PropertySturct
                    {{
                        Index = Index_{0},
                        Name = ""{0}"",
                        Title = ""{5}"",
                        ColumnName = ""{4}"",
                        PropertyType = typeof({1}),
                        CanNull = {2},
                        ValueType = PropertyValueType.{3},
                        CanImport = {6},
                        CanExport = {7}
                    }}
                }}", property.Name
                    , property.CustomType ?? property.CsType
                    , property.Nullable ? "true" : "false"
                    , CsharpHelper.PropertyValueType(property)
                    , property.ColumnName
                    , property.Caption
                    , property["CanImport"] == "1" ? "true" : "false"
                    , property["CanExport"] == "1" ? "true" : "false");
            }
            code2.Clear();
            foreach (PropertyConfig property in table.PublishProperty)
            {
                code2.AppendFormat(@"
        public const byte Index_{0} = {1};", property.Name, property.Index);
            }

        }

        #endregion
    }
}

/*

        string MemonyQueryCode()
        {
            return string.Format(@"
        #region ��չ�������

		/// <summary>
		/// ��ҳ��ȡ�����б�
		/// </summary>
		public static {0}Entity GetModel(long id)
        {{
            return GetById(id);
        }}

        /// <summary>
        /// ȡ��һ��ID��Ӧ�Ķ���
        /// </summary>
        /// <param name=""id"">ID</param>
        /// <returns>�ջ���ҵ��Ķ���</returns>
        public static {0}Entity GetById(long id)
        {{
            return ReadOnlyEntityPool<{0}Entity>.GetById(id);
        }}

        /// <summary>
        /// ȡ��һ��ID��Ӧ�Ķ���
        /// </summary>
        /// <param name=""ids"">ID����</param>
        /// <returns>��Ϊnull�ļ���</returns>
        public static List<{0}Entity> GetByIds(IEnumerable<long> ids)
        {{
            return ReadOnlyEntityPool<{0}Entity>.GetByIds(ids);
        }}

        /// <summary>
        /// ȡ�����ж���
        /// </summary>
        /// <returns>��Ϊnull�ļ���</returns>
        public static IEnumerable<{0}Entity> GetAll()
        {{
            return ReadOnlyEntityPool<{0}Entity>.GetAll();
        }}

        /// <summary>
        /// ȫ���ѯ
        /// </summary>
        /// <param name=""lambda"">��ѯ����</param>
        /// <returns>��Ϊnull�ļ���</returns>
		[Obsolete]
        public static List<{0}Entity> GetModelList(Func<{0}Entity, bool> lambda)
        {{
            return GetAll().Where(lambda).ToList();
        }}

        /// <summary>
        /// ȫ���ѯ
        /// </summary>
        /// <param name=""lambda"">��ѯ����</param>
        /// <returns>��Ϊnull�ļ���</returns>
        public static IEnumerable<{0}Entity> Find(Func<{0}Entity, bool> lambda)
        {{
            return GetAll().Where(lambda);
        }}

        /// <summary>
        /// ȫ���ѯ
        /// </summary>
        /// <param name=""lambda"">��ѯ����</param>
        /// <param name=""take"">Ҫ��������������</param>
        /// <returns>��Ϊnull�ļ���</returns>
		[Obsolete]
        public static List<{0}Entity> GetModelList(Func<{0}Entity, bool> lambda,int take)
        {{
            var all = GetAll().Where(lambda).Take(take).ToArray();
            return all.Length > 0 ? all.ToList() : new List<{0}Entity>();
        }}

        /// <summary>
        /// ȫ���ѯ
        /// </summary>
        /// <param name=""lambda"">��ѯ����</param>
        /// <returns>����Ϊ��</returns>
		[Obsolete]
        public static {0}Entity First(Func<{0}Entity, bool> lambda)
        {{
            return GetAll().First(lambda);
        }}

        /// <summary>
        /// ȫ���ѯ
        /// </summary>
        /// <param name=""lambda"">��ѯ����</param>
        /// <returns>��Ϊnull�ļ���</returns>
		[Obsolete]
        public static {0}Entity FirstOrDefault(Func<{0}Entity, bool> lambda)
        {{
            return GetAll().FirstOrDefault(lambda);
        }}

        #endregion
", Entity.Name);
        }

        string RedisQueryCode()
        {
            return string.Format(@"
        #region ��չ�������

		/// <summary>
		/// ����һ������
		/// </summary>
		public static bool Update({0}Entity entity)
        {{
            return entity.Update();
        }}

		/// <summary>
		/// ����
		/// </summary>
		public bool Update()
        {{
            EntityPool<{0}Entity>.Current.Update(this);
            return true;
        }}

		/// <summary>
		/// ��ҳ��ȡ�����б�
		/// </summary>
		[Obsolete]
public static int GetRecordCount(string strWhere)
{
    {
        return EntityPool <{ 0}
        Entity >.Current.Count;
    }
}

/// <summary>
/// ��ҳ��ȡ�����б�
/// </summary>
[Obsolete]
public static {0}Entity GetModel(string strWhere)
{
    {
        return null;
    }
}

/// <summary>
/// ȫ���ѯ
/// </summary>
/// <param name=""lambda"">��ѯ����</param>
/// <returns>��Ϊnull�ļ���</returns>
[Obsolete]
public static List<{0}Entity> GetModelList(string lambda)
{
    {
        return EntityPool <{ 0}
        Entity >.EmptyList;
    }
}

		/// <summary>
		/// ��ҳ��ȡ�����б�
		/// </summary>
		public static {0}Entity GetModel(long id)
{
    {
        return GetById(id);
    }
}

/// <summary>
/// ����Ϊ������,������Ψһ����,����Ϊ��������
/// </summary>
public long SetIsAdded()
{
    {
        EntityPool <{ 0}
        Entity >.Current.Add(this);
        return Id;
    }
}


/// <summary>
/// ����Ϊ������,������Ψһ����,����Ϊ��������
/// </summary>
public static long Add({ 0}
Entity value)
        {{
            return value.SetIsAdded();
        }}

        /// <summary>
        /// ȡ��һ��ID��Ӧ�Ķ���
        /// </summary>
        /// <param name=""id"">ID</param>
        /// <returns>�ջ���ҵ��Ķ���</returns>
        public static {0}Entity GetById(long id)
{
    {
        return EntityPool <{ 0}
        Entity >.Current.GetById(id);
    }
}

/// <summary>
/// ȡ��һ��ID��Ӧ�Ķ���
/// </summary>
/// <param name=""ids"">ID����</param>
/// <returns>��Ϊnull�ļ���</returns>
public static LazyEntityList<{0}Entity> GetByIds(IEnumerable<long> ids)
{
    {
        return new LazyEntityList<{ 0 }Entity > (ids);
    }
}

/// <summary>
/// ȡ�����ж���
/// </summary>
/// <returns>��Ϊnull�ļ���</returns>
public static IList<{0}Entity> GetAll()
{
    {
        return EntityPool <{ 0}
        Entity >.Current.GetAll();
    }
}

/// <summary>
/// ȫ���ѯ
/// </summary>
/// <param name=""lambda"">��ѯ����</param>
/// <returns>��Ϊnull�ļ���</returns>
[Obsolete]
public static List<{0}Entity> GetModelList(Func<{ 0}
Entity, bool> lambda)
        {{
            return GetAll().Where(lambda).ToList();
        }}

        /// <summary>
        /// ȫ���ѯ
        /// </summary>
        /// <param name=""lambda"">��ѯ����</param>
        /// <returns>��Ϊnull�ļ���</returns>
        public static IEnumerable<{0}Entity> Find(Func<{ 0}
Entity, bool> lambda)
        {{
            return GetAll().Where(lambda);
        }}

        /// <summary>
        /// ȫ���ѯ
        /// </summary>
        /// <param name=""lambda"">��ѯ����</param>
        /// <param name=""take"">Ҫ��������������</param>
        /// <returns>��Ϊnull�ļ���</returns>
		[Obsolete]
public static List<{0}Entity> GetModelList(Func<{ 0}
Entity, bool> lambda,int take)
        {{
            var all = GetAll().Where(lambda).Take(take).ToArray();
            return all.Length > 0 ? all.ToList() : EntityPool<{0}Entity>.EmptyList;
        }}

        /// <summary>
        /// ȫ���ѯ
        /// </summary>
        /// <param name=""lambda"">��ѯ����</param>
        /// <returns>����Ϊ��</returns>
		[Obsolete]
public static {0}Entity First(Func<{ 0}
Entity, bool> lambda)
        {{
            return GetAll().First(lambda);
        }}

        /// <summary>
        /// ȫ���ѯ
        /// </summary>
        /// <param name=""lambda"">��ѯ����</param>
        /// <returns>��Ϊnull�ļ���</returns>
		[Obsolete]
public static {0}Entity FirstOrDefault(Func<{ 0}
Entity, bool> lambda)
        {{
            return GetAll().FirstOrDefault(lambda);
        }}

        #endregion
", Entity.Name);
        }
*/
