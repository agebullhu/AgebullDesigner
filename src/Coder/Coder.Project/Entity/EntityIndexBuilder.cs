using Agebull.EntityModel.Config;
using System.Text;

namespace Agebull.EntityModel.RobotCoder
{
    public sealed class EntityIndexBuilder : ModelBuilderBase
    {
        #region 基础

        /// <summary>
        /// 是否客户端代码
        /// </summary>
        protected override bool IsClient => false;

        public override string BaseCode => @"
        #region 索引
        /*代码已过时*/
        #endregion";

        protected override string Folder => "Struct";

        #endregion


        #region 索引
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
        /// 基于{5}的查找
        /// </summary>
        /// <param name=""{4}"">{5}</param>
        /// <param name=""take"">要求最多的数据数量</param>
        /// <returns>查找结果</returns>
        public static List<{0}Entity> FindBy{2}({3} {4})
        {{
            return __Index_{1}[{4}];
        }}

        /// <summary>
        /// 基于{1}的查找
        /// </summary>
        /// <param name=""{4}"">{5}</param>
        /// <param name=""lambda"">查询表达式</param>
        /// <returns>查找结果</returns>
        public static IEnumerable<{0}Entity> FindBy{2}({3} {4}, Func<{0}Entity, bool> lambda)
        {{
            var ids = __Index_{1}[{4}];
            if (ids.Count == 0)
                return new List<{0}Entity>();
            return ids.Where(lambda);
        }}
        
        /// <summary>
        /// 基于{1}的查找
        /// </summary>
        /// <param name=""{4}"">{5}</param>
        /// <returns>查找结果</returns>
        public static {0}Entity FirstOrDefaultBy{2}({3} {4})
        {{
            return __Index_{1}[{4}].FirstOrDefault();
        }}

        /// <summary>
        /// 基于{1}的查找
        /// </summary>
        /// <param name=""{4}"">{5}</param>
        /// <param name=""lambda"">查询表达式</param>
        /// <returns>查找结果</returns>
        public static {0}Entity FirstOrDefaultBy{2}({3} {4}, Func<{0}Entity, bool> lambda)
        {{
            return __Index_{1}[{4}].FirstOrDefault(lambda);
        }}
        
        /// <summary>
        /// 基于{1}的查找
        /// </summary>
        /// <param name=""{4}"">{5}</param>
        /// <returns>查找结果</returns>
        public static bool AnyBy{2}({3} {4})
        {{
            return __Index_{1}[{4}].Count > 0;
        }}

        /// <summary>
        /// 基于{1}的查找
        /// </summary>
        /// <param name=""{4}"">{5}</param>
        /// <param name=""lambda"">查询表达式</param>
        /// <returns>查找结果</returns>
        public static bool AnyBy{2}({3} {4}, Func<{0}Entity, bool> lambda)
        {{
            var ids = __Index_{1}[{4}];
            return ids.Count > 0 && ids.Any(lambda);
        }}

        /// <summary>
        /// 基{1}的随机查找第一个
        /// </summary>
        /// <param name=""{4}"">{5}</param>
        /// <returns>查找结果</returns>
        public static {0}Entity RandomBy{2}({3} {4})
        {{
            var ids = __Index_{1}[{4}];
            if (ids.Count == 0)
                return null;
            Random random =  new Random((int)(DateTime.Now.Ticks % int.MaxValue));
            return ids[random.Next(0, ids.Count)];
        }}

        /// <summary>
        /// 基{1}的随机查找第一个
        /// </summary>
        /// <param name=""{4}"">{5}</param>
        /// <param name=""lambda"">查询表达式</param>
        /// <returns>查找结果</returns>
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
        #region 字段索引");
            foreach (ColumnSchema property in Entity.LastColumns.Where(p => !p.IsPrimaryKey && (p.CreateIndex || p.UniqueIndex)))
            {
                property.CreateIndex = true;
                code.AppendFormat(@"
        
        /// <summary>
        ///     {1}的Redis索引
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
        /// 基于{5}的查找
        /// </summary>
        /// <param name=""{4}"">{5}</param>
        /// <param name=""take"">要求最多的数据数量</param>
        /// <returns>查找结果</returns>
        public static LazyEntityList<{0}Entity> FindBy{2}({3} {4})
        {{
            var ids = __Index_{1}.GetEqualsIds({4});
            if (ids.Count == 0)
                return LazyEntityList<{0}Entity>.Empty;
            return new LazyEntityList<{0}Entity>(ids);
        }}

        /// <summary>
        /// 基于{1}的查找
        /// </summary>
        /// <param name=""{4}"">{5}</param>
        /// <param name=""lambda"">查询表达式</param>
        /// <returns>查找结果</returns>
        public static IEnumerable<{0}Entity> FindBy{2}({3} {4}, Func<{0}Entity, bool> lambda)
        {{
            var ids = __Index_{1}.GetEqualsIds({4});
            if (ids.Count == 0)
                return new List<{0}Entity>();
            return new LazyEntityList<{0}Entity>(ids).Where(lambda);
        }}
        
        /// <summary>
        /// 基于{1}的查找
        /// </summary>
        /// <param name=""{4}"">{5}</param>
        /// <returns>查找结果</returns>
        public static {0}Entity FirstOrDefaultBy{2}({3} {4})
        {{
            var ids = __Index_{1}.GetEqualsIds({4});
            return ids.Count == 0 ? null : GetById(ids[0]);
        }}

        /// <summary>
        /// 基于{1}的查找
        /// </summary>
        /// <param name=""{4}"">{5}</param>
        /// <param name=""lambda"">查询表达式</param>
        /// <returns>查找结果</returns>
        public static {0}Entity FirstOrDefaultBy{2}({3} {4}, Func<{0}Entity, bool> lambda)
        {{
            var ids = __Index_{1}.GetEqualsIds({4});
            if (ids.Count == 0)
                return null;
            return new LazyEntityList<{0}Entity>(ids).FirstOrDefault(lambda);
        }}
        
        /// <summary>
        /// 基于{1}的查找
        /// </summary>
        /// <param name=""{4}"">{5}</param>
        /// <returns>查找结果</returns>
        public static bool AnyBy{2}({3} {4})
        {{
            var ids = __Index_{1}.GetEqualsIds({4});
            return ids.Count > 0;
        }}

        /// <summary>
        /// 基于{1}的查找
        /// </summary>
        /// <param name=""{4}"">{5}</param>
        /// <param name=""lambda"">查询表达式</param>
        /// <returns>查找结果</returns>
        public static bool AnyBy{2}({3} {4}, Func<{0}Entity, bool> lambda)
        {{
            var ids = __Index_{1}.GetEqualsIds({4});
            if (ids.Count == 0)
                return false;
            return GetByIds(ids).Any(lambda);
        }}

        /// <summary>
        /// 基{1}的随机查找第一个
        /// </summary>
        /// <param name=""{4}"">{5}</param>
        /// <returns>查找结果</returns>
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
        /// 基{1}的随机查找第一个
        /// </summary>
        /// <param name=""{4}"">{5}</param>
        /// <param name=""lambda"">查询表达式</param>
        /// <returns>查找结果</returns>
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
        /// 基于{5}的查找
        /// </summary>
        /// <param name=""{4}"">{5}</param>
        /// <param name=""take"">要求最多的数据数量</param>
        /// <returns>查找结果</returns>
        public static LazyEntityList<{0}Entity> FindBy{2}(int uid,{3} {4})
        {{
            var ids = __Index_{1}.GetEqualsIds(uid , {4});
            if (ids.Count == 0)
                return LazyEntityList<{0}Entity>.Empty;
            return new LazyEntityList<{0}Entity>(ids);
        }}

        /// <summary>
        /// 基于{1}的查找
        /// </summary>
        /// <param name=""{4}"">{5}</param>
        /// <param name=""lambda"">查询表达式</param>
        /// <returns>查找结果</returns>
        public static IEnumerable<{0}Entity> FindBy{2}(int uid,{3} {4}, Func<{0}Entity, bool> lambda)
        {{
            var ids = __Index_{1}.GetEqualsIds(uid , {4});
            if (ids.Count == 0)
                return new List<{0}Entity>();
            return new LazyEntityList<{0}Entity>(ids).Where(lambda);
        }}
        
        /// <summary>
        /// 基于{1}的查找
        /// </summary>
        /// <param name=""{4}"">{5}</param>
        /// <returns>查找结果</returns>
        public static {0}Entity FirstOrDefaultBy{2}(int uid,{3} {4})
        {{
            var ids = __Index_{1}.GetEqualsIds(uid , {4});
            return ids.Count == 0 ? null : GetById(ids[0]);
        }}

        /// <summary>
        /// 基于{1}的查找
        /// </summary>
        /// <param name=""{4}"">{5}</param>
        /// <param name=""lambda"">查询表达式</param>
        /// <returns>查找结果</returns>
        public static {0}Entity FirstOrDefaultBy{2}(int uid,{3} {4}, Func<{0}Entity, bool> lambda)
        {{
            var ids = __Index_{1}.GetEqualsIds(uid , {4});
            if (ids.Count == 0)
                return null;
            return new LazyEntityList<{0}Entity>(ids).FirstOrDefault(lambda);
        }}
        
        /// <summary>
        /// 基于{1}的查找
        /// </summary>
        /// <param name=""{4}"">{5}</param>
        /// <returns>查找结果</returns>
        public static bool AnyBy{2}(int uid,{3} {4})
        {{
            var ids = __Index_{1}.GetEqualsIds(uid , {4});
            return ids.Count > 0;
        }}

        /// <summary>
        /// 基于{1}的查找
        /// </summary>
        /// <param name=""{4}"">{5}</param>
        /// <param name=""lambda"">查询表达式</param>
        /// <returns>查找结果</returns>
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
        #region 字段索引");
            bool isUser = Entity.LastColumns.Any(p => p.IsUserId);
            foreach (ColumnSchema property in Entity.LastColumns.Where(p => !p.IsPrimaryKey && (p.CreateIndex || p.UniqueIndex)))
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
        ///     {2}的Redis索引
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

        private void SqlServerIndexsMethod(FieldConfig property, StringBuilder code)
        {
            code.AppendFormat(@"
                
        /// <summary>
        /// 基于{5}的查找
        /// </summary>
        /// <param name=""{4}"">{5}</param>
        /// <param name=""take"">要求最多的数据数量</param>
        /// <returns>查找结果</returns>
        public static LazyEntityList<{0}Entity> FindBy{2}({3} {4})
        {{
            using(var scope = {0}DataAccess.CreateScope())
            {{
                var list = scope.Entity.Select(p=>p.{2} == {4});
                return new LazyEntityList<{0}Entity>(list);
            }}
        }}

        /// <summary>
        /// 基于{1}的查找
        /// </summary>
        /// <param name=""{4}"">{5}</param>
        /// <param name=""lambda"">查询表达式</param>
        /// <returns>查找结果</returns>
        public static List<{0}Entity> FindBy{2}({3} {4}, Func<{0}Entity, bool> lambda)
        {{
            using(var scope = {0}DataAccess.CreateScope())
            {{
                return scope.Entity.Select(p=>p.{2} == {4},lambda);
            }}
        }}
        
        /// <summary>
        /// 基于{1}的查找
        /// </summary>
        /// <param name=""{4}"">{5}</param>
        /// <returns>查找结果</returns>
        public static {0}Entity FirstOrDefaultBy{2}({3} {4})
        {{
            using(var scope = {0}DataAccess.CreateScope())
            {{
                return scope.Entity.FirstOrDefaultBy(p=>p.{2} == {4});
            }}
        }}

        /// <summary>
        /// 基于{1}的查找
        /// </summary>
        /// <param name=""{4}"">{5}</param>
        /// <param name=""lambda"">查询表达式</param>
        /// <returns>查找结果</returns>
        public static {0}Entity FirstOrDefaultBy{2}({3} {4}, Func<{0}Entity, bool> lambda)
        {{
            using(var scope = {0}DataAccess.CreateScope())
            {{
                return scope.Entity.FirstOrDefaultBy(p=>p.{2} == {4},lambda);
            }}
        }}
        
        /// <summary>
        /// 基于{1}的查找
        /// </summary>
        /// <param name=""{4}"">{5}</param>
        /// <returns>查找结果</returns>
        public static bool AnyBy{2}({3} {4})
        {{
            using(var scope = {0}DataAccess.CreateScope())
            {{
                return scope.Entity.Any(p=>p.{2} == {4});
            }}
        }}

        /// <summary>
        /// 基于{1}的查找
        /// </summary>
        /// <param name=""{4}"">{5}</param>
        /// <param name=""lambda"">查询表达式</param>
        /// <returns>查找结果</returns>
        public static bool AnyBy{2}({3} {4}, Func<{0}Entity, bool> lambda)
        {{
            using(var scope = {0}DataAccess.CreateScope())
            {{
                return scope.Entity.Any(p=>p.{2} == {4} ,lambda);
            }}
        }}

        /// <summary>
        /// 基{1}的随机查找第一个
        /// </summary>
        /// <param name=""{4}"">{5}</param>
        /// <returns>查找结果</returns>
        public static {0}Entity RandomBy{2}({3} {4})
        {{
            using(var scope = {0}DataAccess.CreateScope())
            {{
                return scope.Entity.FirstOrDefault(p=>p.{2} == {4});
            }}
        }}

        /// <summary>
        /// 基{1}的随机查找第一个
        /// </summary>
        /// <param name=""{4}"">{5}</param>
        /// <param name=""lambda"">查询表达式</param>
        /// <returns>查找结果</returns>
        public static {0}Entity RandomBy{2}({3} {4}, Func<{0}Entity, bool> lambda)
        {{
            using(var scope = {0}DataAccess.CreateScope())
            {{
                return scope.Entity.FirstOrDefault(p=>p.{2} == {4},lambda);
            }}
        }}", Model.Name, property.Name, property.Name.ToUWord()
                , property.CsType, property.Name.ToLWord(), property.Caption);
        }

        #endregion

        #endregion
    }
}