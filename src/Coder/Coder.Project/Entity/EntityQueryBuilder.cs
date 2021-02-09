using Agebull.EntityModel.Config;
using System.Text;

namespace Agebull.EntityModel.RobotCoder
{
    public sealed class EntityQueryBuilder : ModelBuilderBase
    {
        #region 基础

        /// <summary>
        /// 是否客户端代码
        /// </summary>
        protected override bool IsClient => false;

        public override string BaseCode => $@"
        #region 实体查询
        {GetQueryCode()}
        #endregion";

        protected override string Folder => "Struct";

        #endregion


        #region 查询

        private string GetQueryCode()
        {
            StringBuilder code = new StringBuilder();
            if (Project.ReadOnly)
                code.AppendFormat(@"
        #region IEntityPoolSetting接口
        
        /// <summary>
        /// 返回配置
        /// </summary>
        HY.Model.EntityOption IEntityPoolSetting.Option()
        {{
            ReadOnlyEntityPool<{0}Entity>.CreateTableFunc = {0}DataAccess.CreateScope;
            return null;
        }}
        #endregion"
                    , Model.Name);
            else
                code.AppendFormat(@"
        #region IAutoAddEntity接口
        
        /// <summary>
        /// 返回配置
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
        /// Redis存储键
        /// </summary>
        [IgnoreDataMember]
        string __redisKey;

        /// <summary>
        /// Redis存储键
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
        /// Redis存储键
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
        /// Redis存储键
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
        /// 对象的Redis类型
        /// </summary>
        string IAutoAddEntity.RedisType
        {{
            get
            {{
                return EntityPool<{0}Entity>.PoolName;
            }}
        }}

        /// <summary>
        /// Sql Server保存列表对象
        /// </summary>
        TransList IAutoAddEntity.SqlSaveTransList
        {{
            get
            {{
                return null;//EntityPool<{0}Entity>.__SqlSaveTransList;
            }}
        }}

        #endregion"
                    , Model.Name
                    , Model.PrimaryColumn.Name
                    , "DataBase"
                    , Model.DataTable.DbIndex); ;
            code.Append(SqlServerQyeryCode());
            return code.ToString();
        }

        private string SqlServerQyeryCode()
        {
            return string.Format(@"

        #region 扩展对象操作
            
        /// <summary>
        /// 保存
        /// </summary>
        public void Save()
        {{
            using(var scope = {0}DataAccess.CreateScope())
            {{
                scope.Entity.Save(this);
            }}
        }}
                    
        /// <summary>
        /// 取得一个ID对应的对象
        /// </summary>
        /// <param name=""id"">ID</param>
        /// <returns>空或查找到的对象</returns>
        public static {0}Entity GetById(long id)
        {{
            using(var scope = {0}DataAccess.CreateScope())
            {{
                return scope.Entity.LoadByPrimaryKey(id);
            }}
        }}
            
        /// <summary>
        /// 取得一批ID对应的对象
        /// </summary>
        /// <param name=""ids"">ID集合</param>
        /// <returns>不为null的集合</returns>
        public static List<{0}Entity> GetByIds(IEnumerable<long> ids)
        {{
            using(var scope = {0}DataAccess.CreateScope())
            {{
                return scope.Entity.LoadByPrimaryKeies(ids);
            }}
        }}
            
        /// <summary>
        /// 取得所有对象
        /// </summary>
        /// <returns>不为null的集合</returns>
        public static List<{0}Entity> GetAll()
        {{
            using(var scope = {0}DataAccess.CreateScope())
            {{
                return scope.Entity.All();
            }}
        }}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public static bool Update({0}Entity model)
        {{
            return model.Update();
        }}

		/// <summary>
		/// 更新
		/// </summary>
		public bool Update()
        {{
            Save();
            return true;
        }}

		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public static {0}Entity GetModel(long id)
        {{
            return GetById(id);
        }}

        /// <summary>
        /// 设置为已增加,即设置唯一主键,设置为新增对象
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
        /// 设置为已增加,即设置唯一主键,设置为新增对象
        /// </summary>
        public static long Add({0}Entity value)
        {{
            return value.SetIsAdded();
        }}

        /// <summary>
        /// 全表查询
        /// </summary>
        /// <param name=""lambda"">查询条件</param>
        /// <returns>不为null的集合</returns>
        public static List<{0}Entity> GetModelList(Expression<Func<{0}Entity, bool>> lambda)
        {{
            using(var scope = {0}DataAccess.CreateScope())
            {{
                return scope.Entity.Select(lambda);
            }}
        }}

        /// <summary>
        /// 全表查询
        /// </summary>
        /// <param name=""lambda"">查询条件</param>
        /// <returns>不为null的集合</returns>
        public static List<{0}Entity> Find(Expression<Func<{0}Entity, bool>> lambda)
        {{
            using(var scope = {0}DataAccess.CreateScope())
            {{
                return scope.Entity.Select(lambda);
            }}
        }}

        /// <summary>
        /// 全表查询
        /// </summary>
        /// <param name=""lambda"">查询条件</param>
        /// <param name=""take"">要求最多的数据数量</param>
        /// <returns>不为null的集合</returns>
        public static List<{0}Entity> GetModelList(Expression<Func<{0}Entity, bool>> lambda,int take)
        {{
            using(var scope = {0}DataAccess.CreateScope())
            {{
                return scope.Entity.Select(lambda);
            }}
        }}

        /// <summary>
        /// 全表查询
        /// </summary>
        /// <param name=""lambda"">查询条件</param>
        /// <returns>可能为空</returns>
        public static {0}Entity First(Expression<Func<{0}Entity, bool>> lambda)
        {{
            using(var scope = {0}DataAccess.CreateScope())
            {{
                return scope.Entity.FirstOrDefault(lambda);
            }}
        }}

        /// <summary>
        /// 全表查询
        /// </summary>
        /// <param name=""lambda"">查询条件</param>
        /// <returns>不为null的集合</returns>
        public static {0}Entity FirstOrDefault(Expression<Func<{0}Entity, bool>> lambda)
        {{
            using(var scope = {0}DataAccess.CreateScope())
            {{
                return scope.Entity.FirstOrDefault(lambda);
            }}
        }}

        /// <summary>
        /// 全表查询
        /// </summary>
        /// <param name=""lambda"">查询条件</param>
        /// <returns>不为null的集合</returns>
        public static bool Any(Expression<Func<{0}Entity, bool>> lambda)
        {{
            using(var scope = {0}DataAccess.CreateScope())
            {{
                return scope.Entity.Any(lambda);
            }}
        }}

        /// <summary>
        /// 全表查询
        /// </summary>
        /// <param name=""lambda"">查询条件</param>
        /// <returns>不为null的集合</returns>
        public static long CountBy(Expression<Func<{0}Entity, bool>> lambda)
        {{
            using(var scope = {0}DataAccess.CreateScope())
            {{
                return scope.Entity.Count(lambda);
            }}
        }}

        #endregion
", Model.Name);
        }

        #endregion
    }
}