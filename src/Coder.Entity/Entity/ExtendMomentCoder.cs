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
            MomentCoder.RegisteCoder("其它", "新增 CS代码", "cs", NewCsCode);
            MomentCoder.RegisteCoder("其它", "新增 CS代码(复制)", "cs", NewCopyCsCode);
            MomentCoder.RegisteCoder("其它","修改 CS代码","cs",  EditCsCode);
            MomentCoder.RegisteCoder("其它","数据库测试","cs",  DbTestCode);
            MomentCoder.RegisteCoder("其它","用户子级操作","cs",  UserChildProcessMothes);
            MomentCoder.RegisteCoder("其它","用户子级删除","cs",  UserChildDefaution);
            MomentCoder.RegisteCoder("其它","用户子级对象","cs",  UserChildDefaution);
            MomentCoder.RegisteCoder("其它","用户子级保存","cs",  UserChildSave);
            MomentCoder.RegisteCoder("其它","用户子级模板","cs",  UserSwitchUid);
            MomentCoder.RegisteCoder("其它","保存Redis到数据库","cs",  SaveToDb);
            MomentCoder.RegisteCoder("其它", "字段静态化","cs",  ToCSharpCode);
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ApiSwitch(EntityConfig entity)
        {
            var isApi = entity.LastProperties.FirstOrDefault(p => p.IsCaption);
            if (isApi != null)
            {
                return $@"
                case ""{entity.ReadTableName.ToLower()}"":
                    Get{entity.Name}();
                    break;";
            }
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ApiCode(EntityConfig entity)
        {
            var isApi = entity.LastProperties.FirstOrDefault(p => p.IsCaption);
            if (isApi != null)
            {
                return $@"

        /// <summary>
        /// 取{entity.Caption}的下拉列表数据
        /// </summary>
        private void Get{entity.Name}()
        {{
            var access = new {entity.Name}DataAccess();
            var result = access.All().Select(p => new EasyComboValues(p.{entity.PrimaryColumn.Name}, p.{isApi.Name})).ToList();
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

        public string ToCSharpCode(EntityConfig entity)
        {
            var code = new StringBuilder();
            foreach (var field in entity.LastProperties)
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
                Discard = {property.IsDiscard.ToString().ToLower()},
                CreateIndex = {property.IsDbIndex.ToString().ToLower()},
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
                Nullable = {property.Nullable.ToString().ToLower()},
                Max = {property.Max},
                Min = {property.Min},
                UniqueString = {property.UniqueString.ToString().ToLower()},
                DbFieldName = ""{property.DbFieldName}"",
                DbType = ""{property.DbType}"",
                DbIndex = {property.DbIndex},
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
            /*
             *  ExtendRole = ""{property.ExtendRole}"",
                ValueSeparate = ""{property.ValueSeparate}"",
                ArraySeparate = ""{property.ArraySeparate}"",
                ExtendArray = {property.ExtendArray.ToString().ToLower()},
                IsKeyValueArray = {property.IsKeyValueArray.ToString().ToLower()},
                IsRelation = {property.IsRelation.ToString().ToLower()},
                ExtendPropertyName = ""{property.ExtendPropertyName}"",
                ExtendClassName = ""{property.ExtendClassName}"",
                ExtendClassIsPredestinate = {property.ExtendClassIsPredestinate.ToString().ToLower()},

             */
        }

        public string NewCopyCsCode(EntityConfig entity)
        {
            var builder = new StringBuilder();
            builder.Append($@"
            var data = new {entity.EntityName}
            {{");
            bool first = true;
            foreach (PropertyConfig field in entity.PublishProperty)
            {
                if (first)
                    first = false;
                else builder.Append(',');
                builder.Append($@"
                {field.Name} = p.{field.Name}");
            }
            builder.Append(@"
            };");
            return builder.ToString();
        }

        public string NewCsCode(EntityConfig entity)
        {
            var builder = new StringBuilder();
            builder.Append($@"
            var data = new {entity.EntityName}
            {{");
            bool first = true;
            foreach (PropertyConfig field in entity.PublishProperty)
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
        public string EditCsCode(EntityConfig entity)
        {
            var builder = new StringBuilder();
            foreach (PropertyConfig field in entity.PublishProperty)
            {
                builder.AppendFormat(@"
            data.{0} = default({1})", field.Name, field.LastCsType);
            }
            return builder.ToString();
        }

        public string EasyUiInfo(EntityConfig entity)
        {
            var jsonBuilder = new StringBuilder();
            foreach (PropertyConfig field in entity.PublishProperty)
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


        public string MvcMenu(EntityConfig entity)
        {
            return
                $@"
           <div iconcls='icon-page' onclick=""javascript:location.href = '@Url.Action(""Index"", ""{entity.Name}"")'"">
           {entity.Caption ?? entity.Description ?? entity.Name
                    }
           </div>";
        }


        private string SaveToDb(EntityConfig entity)
        {
            return string.Format(@"
            if(user._{0} != null)
                {0}.LoadValue();", entity.ReadTableName);
        }


        private string UserSwitchUid(EntityConfig entity)
        {
            return
                $@"
            foreach(var item in this.{entity.Name})
            {{
                item.UId = Uid;
                item.Id = 0;
                item.SetIsAdded();
            }}";
        }

        private string DbTestCode(EntityConfig entity)
        {
            return ($@"
            Console.WriteLine(""{entity.Name}"");
            LocalDataBase.{entity.Name.ToPluralism()}.All();");
        }

        private string UserChildSave(EntityConfig entity)
        {
            PropertyConfig uf = entity.PublishProperty.FirstOrDefault(p => p.IsUserId);
            if (uf == null)
                return null;
            return ($@"
            if(_{entity.Name} != null)
                _{entity.Name}.SaveValue();");
            //            return string.Format(@"
            //            _{0} = new RelationList<{0}>(UserDataBase.Default.{1}.Select(p => p.{2} == Uid), Uid);"
            //                , this.entity.EntityName
            //                , this.entity.EntityName.ToPluralism()
            //                , uf.Name);
        }

        private string UserChildDefaution(EntityConfig entity)
        {
            return $@"
                    UserChildList<{entity.Name}> _{entity.Name};
                    public UserChildList<{entity.Name}> {entity.Name}
                    {{
                        get
                        {{
                            return _{entity.Name} ?? ( this._{entity.Name} = UserChildList<{entity.Name}>.Load(_uid) );
                        }}
                    }}";
        }

        private string UserChildProcessMothes(EntityConfig entity)
        {
            return ($@"

        #region {entity.Name}

        public bool Update_{entity.Name}(Model.{entity.Name} t)
        {{
            return true;
        }}
        public bool Update_{entity.Name}(List<Model.{entity.Name}> t)
        {{
            return true;
        }}
        public int Add_{entity.Name}(Model.{entity.Name} t)
        {{
            {entity.Name}.AddNew(t);
            return t.Id;
        }}
        public bool Delete_{entity.Name}(Model.{entity.Name} t)
        {{
            return Delete_{entity.Name}(t.ID);
        }}
        public bool Delete_{entity.Name}(int ID)
        {{
            return EntityPool<{entity.Name}>.Current.DeleteById(ID);
        }}

        #endregion");
        }
    }
}