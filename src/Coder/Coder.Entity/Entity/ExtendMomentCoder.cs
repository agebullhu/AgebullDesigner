using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;
using System;
using System.ComponentModel.Composition;
using System.Text;

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
            CoderManager.RegisteCoder("其它", "新增 CS代码", "cs", NewCsCode);
            CoderManager.RegisteCoder("其它", "新增 CS代码(复制)", "cs", NewCopyCsCode);
            CoderManager.RegisteCoder("其它", "修改 CS代码", "cs", EditCsCode);
            CoderManager.RegisteCoder("其它", "数据库测试", "cs", DbTestCode);
            CoderManager.RegisteCoder("其它", "用户子级模板", "cs", UserSwitchUid);
            CoderManager.RegisteCoder("其它", "保存Redis到数据库", "cs", SaveToDb);
            CoderManager.RegisteCoder("其它", "字段静态化", "cs", ToCSharpCode);
        }
        #endregion

        public string ToCSharpCode(EntityConfig entity)
        {
            var code = new StringBuilder();
            foreach (var field in entity.LastProperties)
            {
                code.AppendLine(ToCSharpCode(field));
            }
            return code.ToString();
        }

        public string ToCSharpCode(IPropertyConfig property)
        {
            return $@"static IFieldConfig _{property.Name.ToLWord()} = new IFieldConfig
            {{
                Caption = ""{property.Caption}"",
                Description = ""{property.Description}"",
                Index = {property.Index},
                Name = ""{property.Name}"",
                Alias = ""{property.Alias}"",
                Discard = {property.IsDiscard.ToString().ToLower()},
                IsPrimaryKey = {property.IsPrimaryKey.ToString().ToLower()},
                IsExtendKey = {property.IsExtendKey.ToString().ToLower()},
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
                IsInterfaceField = {property.IsInterfaceField.ToString().ToLower()},
                Group = ""{property.Group}"",

            }};";
            /*                CreateIndex = {property.IsDbIndex.ToString().ToLower()},
                IsIdentity =  {property.IsIdentity.ToString().ToLower()},
                DbFieldName = ""{property.DbFieldName}"",
                DbType = ""{property.FieldType}"",
                Precision = {property.Datalen},
                Scale = {property.Scale},
                FixedLength ={property.FixedLength.ToString().ToLower()},
                IsBLOB = {property.IsBlob.ToString().ToLower()},
                DbNullable = {property.DbNullable.ToString().ToLower()},
                StorageProperty = ""{property.StorageProperty}"",
                IsSystemField ={property.IsSystemField.ToString().ToLower()},
                CustomWrite = {property.CustomWrite.ToString().ToLower()},
                IsMemo = {property.IsMemo.ToString().ToLower()},
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
            foreach (var field in entity.PublishProperty)
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
            foreach (var field in entity.PublishProperty)
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
            foreach (var field in entity.PublishProperty)
            {
                builder.AppendFormat(@"
            data.{0} = default({1})", field.Name, field.LastCsType);
            }
            return builder.ToString();
        }

        public string EasyUiInfo(EntityConfig entity)
        {
            var jsonBuilder = new StringBuilder();
            foreach (var field in entity.PublishProperty)
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

    }
}