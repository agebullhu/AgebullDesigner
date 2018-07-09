using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;

namespace Agebull.EntityModel.RobotCoder
{
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    public class ExtendBuilder : MomentCoderBase, IAutoRegister
    {
        #region 注册

        /// <summary>
        /// 注册代码
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            MomentCoder.RegisteCoder("其它","新增 CS代码","cs", cfg => DoCode(cfg, NewCsCode));
            MomentCoder.RegisteCoder("其它","修改 CS代码","cs", cfg => DoCode(cfg, EditCsCode));
            MomentCoder.RegisteCoder("其它","数据库测试","cs", cfg => DoCode(cfg, DbTestCode));
            MomentCoder.RegisteCoder("其它","用户子级操作","cs", cfg => DoCode(cfg, UserChildProcessMothes));
            MomentCoder.RegisteCoder("其它","用户子级删除","cs", cfg => DoCode(cfg, UserChildDefaution));
            MomentCoder.RegisteCoder("其它","用户子级对象","cs", cfg => DoCode(cfg, UserChildDefaution));
            MomentCoder.RegisteCoder("其它","用户子级保存","cs", cfg => DoCode(cfg, UserChildSave));
            MomentCoder.RegisteCoder("其它","用户子级模板","cs", cfg => DoCode(cfg, UserSwitchUid));
            MomentCoder.RegisteCoder("其它","保存Redis到数据库","cs", cfg => DoCode(cfg, SaveToDb));
            MomentCoder.RegisteCoder("其它", "字段静态化","cs", cfg => DoCode(cfg, ToCSharpCode));
        }
        #endregion

        string DoCode(ConfigBase config, Func<string> coder)
        {
            Entity = config as EntityConfig;
            if (Entity == null)
                return null;
            return coder();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ApiSwitch()
        {
            var isApi = Entity.Properties.FirstOrDefault(p => p.IsCaption);
            if (isApi != null)
            {
                return $@"
                case ""{Entity.ReadTableName.ToLower()}"":
                    Get{Entity.Name}();
                    break;";
            }
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ApiCode()
        {
            var isApi = Entity.Properties.FirstOrDefault(p => p.IsCaption);
            if (isApi != null)
            {
                return $@"

        /// <summary>
        /// 取{Entity.Caption}的下拉列表数据
        /// </summary>
        private void Get{Entity.Name}()
        {{
            var access = new {Entity.Name}DataAccess();
            var result = access.All().Select(p => new EasyComboValues(p.{Entity.PrimaryColumn.Name}, p.{isApi.Name})).ToList();
            result.Insert(0, new EasyComboValues
            {{
                Key = 0,
                Value = ""-""
            }});
            SetCustomJsonResult(result);
        }}";
            }
            return null;
        }

        public string ToCSharpCode()
        {
            var code = new StringBuilder();
            foreach (var field in Entity.Properties)
            {
                code.AppendLine(ToCSharpCode(field));
            }
            return code.ToString();
        }

        public string ToCSharpCode(PropertyConfig property)
        {
            return $@"static PropertyConfig _{property.Name.ToLWord()} = new PropertyConfig
            {{
                Caption = ""{property.Caption}"",
                Description = ""{property.Description}"",
                Index = {property.Index},
                Name = ""{property.Name}"",
                Alias = ""{property.Alias}"",
                Discard = {property.Discard.ToString().ToLower()},
                CreateIndex = {property.CreateIndex.ToString().ToLower()},
                IsPrimaryKey = {property.IsPrimaryKey.ToString().ToLower()},
                IsExtendKey = {property.IsExtendKey.ToString().ToLower()},
                IsIdentity =  {property.IsIdentity.ToString().ToLower()},
                IsGlobalKey =  {property.IsGlobalKey.ToString().ToLower()},
                UniqueIndex = {property.UniqueIndex},
                CppName = ""{property.CppName}"",
                CppType = ""{property.CppType}"",
                Type = ""{property.CustomType}"",
                CsType = ""{property.CsType}"",                
                IsCompute = {property.IsCompute.ToString().ToLower()},
                InnerField = {property.InnerField.ToString().ToLower()},
                Initialization = ""{property.Initialization}"",
                EmptyValue = ""{property.EmptyValue}"",
                ExtendRole = ""{property.ExtendRole}"",
                ValueSeparate = ""{property.ValueSeparate}"",
                ArraySeparate = ""{property.ArraySeparate}"",
                ExtendArray = {property.ExtendArray.ToString().ToLower()},
                IsKeyValueArray = {property.IsKeyValueArray.ToString().ToLower()},
                IsRelation = {property.IsRelation.ToString().ToLower()},
                ExtendPropertyName = ""{property.ExtendPropertyName}"",
                ExtendClassName = ""{property.ExtendClassName}"",
                ExtendClassIsPredestinate = {property.ExtendClassIsPredestinate.ToString().ToLower()},
                Nullable = {property.Nullable.ToString().ToLower()},
                Max = {property.Max},
                Min = {property.Min},
                UniqueString = {property.UniqueString.ToString().ToLower()},
                ColumnName = ""{property.ColumnName}"",
                DbType = ""{property.DbType}"",
                DbIndex = {property.DbIndex},
                Unicode = {property.Unicode.ToString().ToLower()},
                Precision = {property.Datalen},
                Scale = {property.Scale},
                FixedLength ={property.FixedLength.ToString().ToLower()},
                IsBLOB = {property.IsBlob.ToString().ToLower()},
                DbNullable = {property.DbNullable.ToString().ToLower()},
                StorageProperty = ""{property.StorageProperty}"",
                IsSystemField ={property.IsSystemField.ToString().ToLower()},
                CustomWrite = {property.CustomWrite.ToString().ToLower()},
                IsInterfaceField = {property.IsInterfaceField.ToString().ToLower()},
                Group = ""{property.Group}"",
                IsMemo = {property.IsMemo.ToString().ToLower()},
            }};";
        }

        public string NewCsCode()
        {
            var builder = new StringBuilder();
            builder.Append($@"
            var data = new {Entity.EntityName}
            {{");
            bool first = true;
            foreach (PropertyConfig field in Entity.PublishProperty)
            {
                if (first)
                    first = false;
                else builder.Append(',');
                if (field.HelloCode != null)
                {
                    builder.Append(field.CsType == "string"
                        ? $@"
                {field.Name} = ""{field.HelloCode}"""
                        : $@"
                {field.Name} = {field.HelloCode}");
                }
                else
                {
                    builder.Append($@"
                {field.Name} = default({field.LastCsType})");
                }
            }
            builder.Append(@"
            };");
            return builder.ToString();
        }
        public string EditCsCode()
        {
            var builder = new StringBuilder();
            foreach (PropertyConfig field in Entity.PublishProperty)
            {
                builder.AppendFormat(@"
            data.{0} = default({1})", field.Name, field.LastCsType);
            }
            return builder.ToString();
        }

        public string EasyUiInfo()
        {
            var jsonBuilder = new StringBuilder();
            foreach (PropertyConfig field in Entity.PublishProperty)
            {
                string ext = null;
                if (field.CsType.ToLower() == "bool")
                    ext = " ? \"是\" : \"否\"";
                jsonBuilder.AppendFormat(@"
                <div class='infoField'>
                    <div class='infoLabel' title='{3}'>{0}:</div>
                    <div class='infoValue_s'>@(Model.{1}{2})</div>
                </div>"
                    , field.Caption ?? field.Name
                    , field.Name
                    , ext
                    , field.Description);
            }
            return jsonBuilder.ToString();
        }


        public string MvcMenu()
        {
            return
                $@"
           <div iconcls='icon-page' onclick=""javascript:location.href = '@Url.Action(""Index"", ""{Entity.Name}"")'"">
           {Entity.Caption ?? Entity.Description ?? Entity.Name
                    }
           </div>";
        }


        private string SaveToDb()
        {
            return string.Format(@"
            if(user._{0} != null)
                {0}.LoadValue();", Entity.ReadTableName);
        }


        private string UserSwitchUid()
        {
            return
                $@"
            foreach(var item in this.{Entity.Name})
            {{
                item.UId = Uid;
                item.Id = 0;
                item.SetIsAdded();
            }}";
        }

        private string DbTestCode()
        {
            return string.Format($@"
            Console.WriteLine(""{Entity.Name}"");
            LocalDataBase.{Entity.Name.ToPluralism()}.All();");
        }

        private string UserChildSave()
        {
            PropertyConfig uf = Entity.PublishProperty.FirstOrDefault(p => p.IsUserId);
            if (uf == null)
                return null;
            return string.Format($@"
            if(_{Entity.Name} != null)
                _{Entity.Name}.SaveValue();");
            //            return string.Format(@"
            //            _{0} = new RelationList<{0}>(UserDataBase.Default.{1}.Select(p => p.{2} == Uid), Uid);"
            //                , this.Entity.EntityName
            //                , this.Entity.EntityName.ToPluralism()
            //                , uf.Name);
        }

        private string UserChildDefaution()
        {
            return $@"
                    UserChildList<{Entity.Name}> _{Entity.Name};
                    public UserChildList<{Entity.Name}> {Entity.Name}
                    {{
                        get
                        {{
                            return _{Entity.Name} ?? ( this._{Entity.Name} = UserChildList<{Entity.Name}>.Load(_uid) );
                        }}
                    }}";
        }

        private string UserChildProcessMothes()
        {
            return string.Format($@"

        #region {Entity.Name}

        public bool Update_{Entity.Name}(Model.{Entity.Name} t)
        {{
            return true;
        }}
        public bool Update_{Entity.Name}(List<Model.{Entity.Name}> t)
        {{
            return true;
        }}
        public int Add_{Entity.Name}(Model.{Entity.Name} t)
        {{
            {Entity.Name}.AddNew(t);
            return t.Id;
        }}
        public bool Delete_{Entity.Name}(Model.{Entity.Name} t)
        {{
            return Delete_{Entity.Name}(t.ID);
        }}
        public bool Delete_{Entity.Name}(int ID)
        {{
            return EntityPool<{Entity.Name}>.Current.DeleteById(ID);
        }}

        #endregion");
        }
    }
}