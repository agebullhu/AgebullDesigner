using System.Text;
using Agebull.Common.LUA;
#pragma warning disable 168

namespace Gboxt.Common.DataAccess.Schemas
{
    partial class ApiItem
    {
        /// <summary>
        /// LUA结构支持
        /// </summary>
        /// <returns></returns>
        public override void GetLuaStruct(StringBuilder code)
        {
            base.GetLuaStruct(code);
            if (!string.IsNullOrWhiteSpace(Friend))
                code.AppendLine($@"['Friend'] = '{Friend.ToLuaString()}',");
            else
                code.AppendLine($@"['Friend'] = nil,");

            code.AppendLine($@"['FriendKey'] ='{FriendKey}',");

            code.AppendLine($@"['LocalCommand'] ={(LocalCommand.ToString().ToLower())},");

            if (!string.IsNullOrWhiteSpace(CallArg))
                code.AppendLine($@"['CallArg'] = '{CallArg.ToLuaString()}',");
            else
                code.AppendLine($@"['CallArg'] = nil,");

            if (!string.IsNullOrWhiteSpace(ResultArg))
                code.AppendLine($@"['ClientArg'] = '{ResultArg.ToLuaString()}',");
            else
                code.AppendLine($@"['ClientArg'] = nil,");

            code.AppendLine($@"['IsUserCommand'] ={(IsUserCommand.ToString().ToLower())},");

            if (!string.IsNullOrWhiteSpace(ResultArg))
                code.AppendLine($@"['ResultArg'] = '{ResultArg.ToLuaString()}',");
            else
                code.AppendLine($@"['ResultArg'] = nil,");

            if (!string.IsNullOrWhiteSpace(Org))
                code.AppendLine($@"['Org'] = '{Org.ToLuaString()}',");
            else
                code.AppendLine($@"['Org'] = nil,");

            if (EsEntity != null)
                code.AppendLine($@"['EsEntity'] = {EsEntity.GetLuaStruct()},");

            if (LocalEntity != null)
                code.AppendLine($@"['LocalEntity'] = {LocalEntity.GetLuaStruct()},");

            if (!string.IsNullOrWhiteSpace(CommandId))
                code.AppendLine($@"['CommandId'] = '{CommandId.ToLuaString()}',");
            else
                code.AppendLine($@"['CommandId'] = nil,");

        }

        public EntityConfig LocalEntity { get; set; }

        public EntityConfig EsEntity { get; set; }
    }

    partial class ClassifyConfig
    {
        /// <summary>
        /// LUA结构支持
        /// </summary>
        /// <returns></returns>
        public override void GetLuaStruct(StringBuilder code)
        {
            base.GetLuaStruct(code);
            int idx;
            if (!string.IsNullOrWhiteSpace(Classify))
                code.AppendLine($@"['Classify'] = '{Classify.ToLuaString()}',");
            else
                code.AppendLine($@"['Classify'] = nil,");

        }
    }

    partial class ClassifyItem<TConfig>
    {
        /// <summary>
        /// LUA结构支持
        /// </summary>
        /// <returns></returns>
        public override void GetLuaStruct(StringBuilder code)
        {
            base.GetLuaStruct(code);
            int idx;
            idx = 0;
            code.AppendLine("['Items'] ={");
            foreach (var val in Items)
            {
                if (idx++ > 0)
                    code.Append(',');
                code.AppendLine($@"{val.GetLuaStruct()}");
            }
            code.AppendLine("},");

        }
    }

    partial class ClassifyGroupConfig<TConfig>
    {
        /// <summary>
        /// LUA结构支持
        /// </summary>
        /// <returns></returns>
        public override void GetLuaStruct(StringBuilder code)
        {
            base.GetLuaStruct(code);
            int idx;
            idx = 0;
            code.AppendLine("['Classifies'] ={");
            foreach (var val in Classifies)
            {
                if (idx++ > 0)
                    code.Append(',');
                code.AppendLine($@"{val.GetLuaStruct()}");
            }
            code.AppendLine("},");

        }
    }

    partial class EntityConfig
    {
        /// <summary>
        /// LUA结构支持
        /// </summary>
        /// <returns></returns>
        public override void GetLuaStruct(StringBuilder code)
        {
            base.GetLuaStruct(code);
            int idx;
            code.AppendLine($@"['ColumnIndexStart'] ={ColumnIndexStart},");

            code.AppendLine($@"['IsUniqueUnion'] ={(IsUniqueUnion.ToString().ToLower())},");

            if (PrimaryColumn != null)
                code.AppendLine($@"['PrimaryColumn'] = {PrimaryColumn.GetLuaStruct()},");

            if (!string.IsNullOrWhiteSpace(PrimaryField))
                code.AppendLine($@"['PrimaryField'] = '{PrimaryField.ToLuaString()}',");
            else
                code.AppendLine($@"['PrimaryField'] = nil,");

            if (!string.IsNullOrWhiteSpace(RedisKey))
                code.AppendLine($@"['RedisKey'] = '{RedisKey.ToLuaString()}',");
            else
                code.AppendLine($@"['RedisKey'] = nil,");

            if (!string.IsNullOrWhiteSpace(ModelInclude))
                code.AppendLine($@"['ModelInclude'] = '{ModelInclude.ToLuaString()}',");
            else
                code.AppendLine($@"['ModelInclude'] = nil,");

            if (!string.IsNullOrWhiteSpace(ModelBase))
                code.AppendLine($@"['ModelBase'] = '{ModelBase.ToLuaString()}',");
            else
                code.AppendLine($@"['ModelBase'] = nil,");

            code.AppendLine($@"['DataVersion'] ={DataVersion},");

            code.AppendLine($@"['IsInternal'] ={(IsInternal.ToString().ToLower())},");

            if (!string.IsNullOrWhiteSpace(Name))
                code.AppendLine($@"['Name'] = '{Name.ToLuaString()}',");
            else
                code.AppendLine($@"['Name'] = nil,");

            code.AppendLine($@"['IsClass'] ={(IsClass.ToString().ToLower())},");

            if (!string.IsNullOrWhiteSpace(Interfaces))
                code.AppendLine($@"['Interfaces'] = '{Interfaces.ToLuaString()}',");
            else
                code.AppendLine($@"['Interfaces'] = nil,");

            if (!string.IsNullOrWhiteSpace(Project))
                code.AppendLine($@"['Project'] = '{Project.ToLuaString()}',");
            else
                code.AppendLine($@"['Project'] = nil,");

            code.AppendLine($@"['MaxIdentity'] ={MaxIdentity},");

            idx = 0;
            code.AppendLine("['Properties'] ={");
            foreach (var val in Properties)
            {
                if (idx++ > 0)
                    code.Append(',');
                code.AppendLine($@"{val.GetLuaStruct()}");
            }
            code.AppendLine("},");

            //idx = 0;
            //code.AppendLine("['LastProperties'] ={");
            //foreach (var val in LastProperties)
            //{
            //    if (idx++ > 0)
            //        code.Append(',');
            //    code.AppendLine($@"{val.GetLuaStruct()}");
            //}
            //code.AppendLine("},");

            idx = 0;
            code.AppendLine("['Commands'] ={");
            foreach (var val in Commands)
            {
                if (idx++ > 0)
                    code.Append(',');
                code.AppendLine($@"{val.GetLuaStruct()}");
            }
            code.AppendLine("},");

            idx = 0;
            code.AppendLine("['Releations'] ={");
            foreach (var val in Releations)
            {
                if (idx++ > 0)
                    code.Append(',');
                code.AppendLine($@"{val.GetLuaStruct()}");
            }
            code.AppendLine("},");

            code.AppendLine($@"['MaxForm'] ={(MaxForm.ToString().ToLower())},");

            code.AppendLine($@"['ListDetails'] ={(ListDetails.ToString().ToLower())},");

            if (!string.IsNullOrWhiteSpace(ReadTableName))
                code.AppendLine($@"['TableName'] = '{ReadTableName.ToLuaString()}',");
            else
                code.AppendLine($@"['TableName'] = nil,");

            if (!string.IsNullOrWhiteSpace(SaveTableName))
                code.AppendLine($@"['SaveTableName'] = '{SaveTableName.ToLuaString()}',");
            else
                code.AppendLine($@"['SaveTableName'] = nil,");

            if (!string.IsNullOrWhiteSpace(SaveTable))
                code.AppendLine($@"['SaveTable'] = '{SaveTable.ToLuaString()}',");
            else
                code.AppendLine($@"['SaveTable'] = nil,");

            code.AppendLine($@"['DbIndex'] ={DbIndex},");

            code.AppendLine($@"['UpdateByModified'] ={(UpdateByModified.ToString().ToLower())},");

            if (!string.IsNullOrWhiteSpace(CppName))
                code.AppendLine($@"['CppName'] = '{CppName.ToLuaString()}',");
            else
                code.AppendLine($@"['CppName'] = nil,");

            if (!string.IsNullOrWhiteSpace(DisplayName))
                code.AppendLine($@"['DisplayName'] = '{DisplayName.ToLuaString()}',");
            else
                code.AppendLine($@"['DisplayName'] = nil,");

            //idx = 0;
            //code.AppendLine("['PublishProperty'] ={");
            //foreach (var val in PublishProperty)
            //{
            //    if (idx++ > 0)
            //        code.Append(',');
            //    code.AppendLine($@"{val.GetLuaStruct()}");
            //}
            //code.AppendLine("},");

            //idx = 0;
            //code.AppendLine("['CppProperty'] ={");
            //foreach (var val in CppProperty)
            //{
            //    if (idx++ > 0)
            //        code.Append(',');
            //    code.AppendLine($@"{val.GetLuaStruct()}");
            //}
            //code.AppendLine("},");

            //idx = 0;
            //code.AppendLine("['ClientProperty'] ={");
            //foreach (var val in ClientProperty)
            //{
            //    if (idx++ > 0)
            //        code.Append(',');
            //    code.AppendLine($@"{val.GetLuaStruct()}");
            //}
            //code.AppendLine("},");

            //idx = 0;
            //code.AppendLine("['UserProperty'] ={");
            //foreach (var val in UserProperty)
            //{
            //    if (idx++ > 0)
            //        code.Append(',');
            //    code.AppendLine($@"{val.GetLuaStruct()}");
            //}
            //code.AppendLine("},");

            //idx = 0;
            //code.AppendLine("['PublishDbFields'] ={");
            //foreach (var val in PublishDbFields)
            //{
            //    if (idx++ > 0)
            //        code.Append(',');
            //    code.AppendLine($@"{val.GetLuaStruct()}");
            //}
            //code.AppendLine("},");

            //idx = 0;
            //code.AppendLine("['DbFields'] ={");
            //foreach (var val in DbFields)
            //{
            //    if (idx++ > 0)
            //        code.Append(',');
            //    code.AppendLine($@"{val.GetLuaStruct()}");
            //}
            //code.AppendLine("},");

            //idx = 0;
            //code.AppendLine("['ReadCoreCodes'] ={");
            //foreach (var val in ReadCoreCodes.Values)
            //{
            //    if (idx++ > 0)
            //        code.Append(',');
            //    code.AppendLine($@"{val.GetLuaStruct()}");
            //}
            //code.AppendLine("},");

        }
    }

    partial class EnumConfig
    {
        /// <summary>
        /// LUA结构支持
        /// </summary>
        /// <returns></returns>
        public override void GetLuaStruct(StringBuilder code)
        {
            base.GetLuaStruct(code);
            int idx;
            code.AppendLine($@"['IsFlagEnum'] ={(IsFlagEnum.ToString().ToLower())},");

            code.AppendLine($@"['LinkField'] ='{LinkField}',");

            idx = 0;
            code.AppendLine("['Items'] ={");
            foreach (var val in Items)
            {
                if (idx++ > 0)
                    code.Append(',');
                code.AppendLine($@"{val.GetLuaStruct()}");
            }
            code.AppendLine("},");

        }
    }

    partial class EnumItem
    {
        /// <summary>
        /// LUA结构支持
        /// </summary>
        /// <returns></returns>
        public override void GetLuaStruct(StringBuilder code)
        {
            base.GetLuaStruct(code);
            int idx;
            if (!string.IsNullOrWhiteSpace(Value))
                code.AppendLine($@"['Value'] = '{Value.ToLuaString()}',");
            else
                code.AppendLine($@"['Value'] = nil,");

        }
    }

    partial class FileConfigBase
    {
        /// <summary>
        /// LUA结构支持
        /// </summary>
        /// <returns></returns>
        public override void GetLuaStruct(StringBuilder code)
        {
            base.GetLuaStruct(code);
            int idx;
            if (!string.IsNullOrWhiteSpace(FileName))
                code.AppendLine($@"['FileName'] = '{FileName.ToLuaString()}',");
            else
                code.AppendLine($@"['FileName'] = nil,");

        }
    }

    partial class NotifyItem
    {
        /// <summary>
        /// LUA结构支持
        /// </summary>
        /// <returns></returns>
        public override void GetLuaStruct(StringBuilder code)
        {
            base.GetLuaStruct(code);
            int idx;
            if (!string.IsNullOrWhiteSpace(Friend))
                code.AppendLine($@"['Friend'] = '{Friend.ToLuaString()}',");
            else
                code.AppendLine($@"['Friend'] = nil,");

            code.AppendLine($@"['FriendKey'] ='{FriendKey}',");

            code.AppendLine($@"['LocalCommand'] ={(LocalCommand.ToString().ToLower())},");

            if (!string.IsNullOrWhiteSpace(CommandId))
                code.AppendLine($@"['CommandId'] = '{CommandId.ToLuaString()}',");
            else
                code.AppendLine($@"['CommandId'] = nil,");

            if (!string.IsNullOrWhiteSpace(NotifyEntity))
                code.AppendLine($@"['NotifyEntity'] = '{NotifyEntity.ToLuaString()}',");
            else
                code.AppendLine($@"['NotifyEntity'] = nil,");

            if (!string.IsNullOrWhiteSpace(ClientEntity))
                code.AppendLine($@"['ClientEntity'] = '{ClientEntity.ToLuaString()}',");
            else
                code.AppendLine($@"['ClientEntity'] = nil,");

            code.AppendLine($@"['IsCommandResult'] ={(IsCommandResult.ToString().ToLower())},");

            code.AppendLine($@"['IsMulit'] ={(IsMulit.ToString().ToLower())},");

            if (!string.IsNullOrWhiteSpace(Org))
                code.AppendLine($@"['Org'] = '{Org.ToLuaString()}',");
            else
                code.AppendLine($@"['Org'] = nil,");

            if (EsEntity != null)
                code.AppendLine($@"['EsEntity'] = {EsEntity.GetLuaStruct()},");

            if (LocalEntity != null)
                code.AppendLine($@"['LocalEntity'] = {LocalEntity.GetLuaStruct()},");

        }
    }

    partial class ParentConfigBase
    {
        /// <summary>
        /// LUA结构支持
        /// </summary>
        /// <returns></returns>
        public override void GetLuaStruct(StringBuilder code)
        {
            base.GetLuaStruct(code);
            int idx;
            if (!string.IsNullOrWhiteSpace(Abbreviation))
                code.AppendLine($@"['Abbreviation'] = '{Abbreviation.ToLuaString()}',");
            else
                code.AppendLine($@"['Abbreviation'] = nil,");

        }
    }

    partial class ProjectConfig
    {
        /// <summary>
        /// LUA结构支持
        /// </summary>
        /// <returns></returns>
        public override void GetLuaStruct(StringBuilder code)
        {
            base.GetLuaStruct(code);
            int idx;
            idx = 0;
            code.AppendLine("['ApiItems'] ={");
            foreach (var val in ApiItems)
            {
                if (idx++ > 0)
                    code.Append(',');
                code.AppendLine($@"{val.GetLuaStruct()}");
            }
            code.AppendLine("},");
            
            idx = 0;
            code.AppendLine("['Entities'] ={");
            foreach (var val in Entities)
            {
                if (idx++ > 0)
                    code.Append(',');
                code.AppendLine($@"{val.GetLuaStruct()}");
            }
            code.AppendLine("},");

            code.AppendLine($@"['ReadOnly'] ={(ReadOnly.ToString().ToLower())},");

            if (!string.IsNullOrWhiteSpace(DataBaseObjectName))
                code.AppendLine($@"['DataBaseObjectName'] = '{DataBaseObjectName.ToLuaString()}',");
            else
                code.AppendLine($@"['DataBaseObjectName'] = nil,");

            if (!string.IsNullOrWhiteSpace(NameSpace))
                code.AppendLine($@"['NameSpace'] = '{NameSpace.ToLuaString()}',");
            else
                code.AppendLine($@"['NameSpace'] = nil,");

            if (!string.IsNullOrWhiteSpace(PagePath))
                code.AppendLine($@"['PagePath'] = '{PagePath.ToLuaString()}',");
            else
                code.AppendLine($@"['PagePath'] = nil,");

            if (!string.IsNullOrWhiteSpace(BusinessPath))
                code.AppendLine($@"['BusinessPath'] = '{BusinessPath.ToLuaString()}',");
            else
                code.AppendLine($@"['BusinessPath'] = nil,");

            if (!string.IsNullOrWhiteSpace(ClientCsPath))
                code.AppendLine($@"['ClientCsPath'] = '{ClientCsPath.ToLuaString()}',");
            else
                code.AppendLine($@"['ClientCsPath'] = nil,");

            if (!string.IsNullOrWhiteSpace(ModelPath))
                code.AppendLine($@"['ModelPath'] = '{ModelPath.ToLuaString()}',");
            else
                code.AppendLine($@"['ModelPath'] = nil,");

            if (!string.IsNullOrWhiteSpace(CodePath))
                code.AppendLine($@"['CodePath'] = '{CodePath.ToLuaString()}',");
            else
                code.AppendLine($@"['CodePath'] = nil,");

            code.AppendLine($@"['DbType'] ='{DbType}',");

            if (!string.IsNullOrWhiteSpace(DbHost))
                code.AppendLine($@"['DbHost'] = '{DbHost.ToLuaString()}',");
            else
                code.AppendLine($@"['DbHost'] = nil,");

            if (!string.IsNullOrWhiteSpace(DbSoruce))
                code.AppendLine($@"['DbSoruce'] = '{DbSoruce.ToLuaString()}',");
            else
                code.AppendLine($@"['DbSoruce'] = nil,");

            if (!string.IsNullOrWhiteSpace(DbPassWord))
                code.AppendLine($@"['DbPassWord'] = '{DbPassWord.ToLuaString()}',");
            else
                code.AppendLine($@"['DbPassWord'] = nil,");

            if (!string.IsNullOrWhiteSpace(DbUser))
                code.AppendLine($@"['DbUser'] = '{DbUser.ToLuaString()}',");
            else
                code.AppendLine($@"['DbUser'] = nil,");
        }
    }


    partial class PropertyConfig
    {
        /// <summary>
        /// LUA结构支持
        /// </summary>
        /// <returns></returns>
        public override void GetLuaStruct(StringBuilder code)
        {
            base.GetLuaStruct(code);
            int idx;
            if (!string.IsNullOrWhiteSpace(PropertyName))
                code.AppendLine($@"['PropertyName'] = '{PropertyName.ToLuaString()}',");
            else
                code.AppendLine($@"['PropertyName'] = nil,");

            code.AppendLine($@"['IsCaption'] ={(IsCaption.ToString().ToLower())},");

            if (!string.IsNullOrWhiteSpace(Alias))
                code.AppendLine($@"['Alias'] = '{Alias.ToLuaString()}',");
            else
                code.AppendLine($@"['Alias'] = nil,");

            if (!string.IsNullOrWhiteSpace(Group))
                code.AppendLine($@"['Group'] = '{Group.ToLuaString()}',");
            else
                code.AppendLine($@"['Group'] = nil,");

            code.AppendLine($@"['CreateIndex'] ={(CreateIndex.ToString().ToLower())},");

            code.AppendLine($@"['IsPrimaryKey'] ={(IsPrimaryKey.ToString().ToLower())},");

            code.AppendLine($@"['IsExtendKey'] ={(IsExtendKey.ToString().ToLower())},");

            code.AppendLine($@"['IsIdentity'] ={(IsIdentity.ToString().ToLower())},");

            code.AppendLine($@"['IsGlobalKey'] ={(IsGlobalKey.ToString().ToLower())},");

            code.AppendLine($@"['UniqueIndex'] ={UniqueIndex},");

            code.AppendLine($@"['IsRequired'] ={(IsRequired.ToString().ToLower())},");

            code.AppendLine($@"['IsUserReadOnly'] ={(IsUserReadOnly.ToString().ToLower())},");

            code.AppendLine($@"['IsMemo'] ={(IsMemo.ToString().ToLower())},");
            

            code.AppendLine($@"['DenyClient'] ={(DenyClient.ToString().ToLower())},");

            if (!string.IsNullOrWhiteSpace(Prefix))
                code.AppendLine($@"['Prefix'] = '{Prefix.ToLuaString()}',");
            else
                code.AppendLine($@"['Prefix'] = nil,");

            if (!string.IsNullOrWhiteSpace(Suffix))
                code.AppendLine($@"['Suffix'] = '{Suffix.ToLuaString()}',");
            else
                code.AppendLine($@"['Suffix'] = nil,");

            if (!string.IsNullOrWhiteSpace(InputType))
                code.AppendLine($@"['InputType'] = '{InputType.ToLuaString()}',");
            else
                code.AppendLine($@"['InputType'] = nil,");

            if (!string.IsNullOrWhiteSpace(ComboBoxUrl))
                code.AppendLine($@"['ComboBoxUrl'] = '{ComboBoxUrl.ToLuaString()}',");
            else
                code.AppendLine($@"['ComboBoxUrl'] = nil,");

            code.AppendLine($@"['IsMoney'] ={(IsMoney.ToString().ToLower())},");

            if (!string.IsNullOrWhiteSpace(GridAlign))
                code.AppendLine($@"['GridAlign'] = '{GridAlign.ToLuaString()}',");
            else
                code.AppendLine($@"['GridAlign'] = nil,");

            if (!string.IsNullOrWhiteSpace(DataFormater))
                code.AppendLine($@"['DataFormater'] = '{DataFormater.ToLuaString()}',");
            else
                code.AppendLine($@"['DataFormater'] = nil,");

            code.AppendLine($@"['DenyClient'] ={(DenyClient.ToString().ToLower())},");

            code.AppendLine($@"['GridDetails'] ={(GridDetails.ToString().ToLower())},");

            code.AppendLine($@"['NoneGrid'] ={(NoneGrid.ToString().ToLower())},");

            code.AppendLine($@"['NoneDetails'] ={(NoneDetails.ToString().ToLower())},");

            if (!string.IsNullOrWhiteSpace(GridDetailsCode))
                code.AppendLine($@"['GridDetailsCode'] = '{GridDetailsCode.ToLuaString()}',");
            else
                code.AppendLine($@"['GridDetailsCode'] = nil,");

            if (!string.IsNullOrWhiteSpace(CppType))
                code.AppendLine($@"['CppType'] = '{CppType.ToLuaString()}',");
            else
                code.AppendLine($@"['CppType'] = nil,");

            if (CppTypeObject != null)
                code.AppendLine($@"['CppTypeObject'] ='{CppTypeObject}',");

            if (!string.IsNullOrWhiteSpace(CppName))
                code.AppendLine($@"['CppName'] = '{CppName.ToLuaString()}',");
            else
                code.AppendLine($@"['CppName'] = nil,");

            if (!string.IsNullOrWhiteSpace(CppLastType))
                code.AppendLine($@"['CppLastType'] = '{CppLastType.ToLuaString()}',");
            else
                code.AppendLine($@"['CppLastType'] = nil,");

            code.AppendLine($@"['IsIntDecimal'] ={(IsIntDecimal.ToString().ToLower())},");

            if (!string.IsNullOrWhiteSpace(CsType))
                code.AppendLine($@"['CsType'] = '{CsType.ToLuaString()}',");
            else
                code.AppendLine($@"['CsType'] = nil,");

            if (!string.IsNullOrWhiteSpace(CustomType))
                code.AppendLine($@"['CustomType'] = '{CustomType.ToLuaString()}',");
            else
                code.AppendLine($@"['CustomType'] = nil,");

            if (!string.IsNullOrWhiteSpace(LastCsType))
                code.AppendLine($@"['LastCsType'] = '{LastCsType.ToLuaString()}',");
            else
                code.AppendLine($@"['LastCsType'] = nil,");

            if (EnumConfig != null)
                code.AppendLine($@"['EnumConfig'] = {EnumConfig.GetLuaStruct()},");

            code.AppendLine($@"['IsCompute'] ={(IsCompute.ToString().ToLower())},");

            if (!string.IsNullOrWhiteSpace(ComputeGetCode))
                code.AppendLine($@"['ComputeGetCode'] = '{ComputeGetCode.ToLuaString()}',");
            else
                code.AppendLine($@"['ComputeGetCode'] = nil,");

            if (!string.IsNullOrWhiteSpace(ComputeSetCode))
                code.AppendLine($@"['ComputeSetCode'] = '{ComputeSetCode.ToLuaString()}',");
            else
                code.AppendLine($@"['ComputeSetCode'] = nil,");

            code.AppendLine($@"['IsMiddleField'] ={(IsMiddleField.ToString().ToLower())},");

            code.AppendLine($@"['InnerField'] ={(InnerField.ToString().ToLower())},");

            code.AppendLine($@"['IsSystemField'] ={(IsSystemField.ToString().ToLower())},");

            code.AppendLine($@"['IsInterfaceField'] ={(IsInterfaceField.ToString().ToLower())},");

            if (!string.IsNullOrWhiteSpace(Initialization))
                code.AppendLine($@"['Initialization'] = '{Initialization.ToLuaString()}',");
            else
                code.AppendLine($@"['Initialization'] = nil,");

            if (!string.IsNullOrWhiteSpace(EmptyValue))
                code.AppendLine($@"['EmptyValue'] = '{EmptyValue.ToLuaString()}',");
            else
                code.AppendLine($@"['EmptyValue'] = nil,");

            code.AppendLine($@"['DenyScope'] ='{DenyScope}',");

            code.AppendLine($@"['Nullable'] ={(Nullable.ToString().ToLower())},");

            if (!string.IsNullOrWhiteSpace(Max))
                code.AppendLine($@"['Max'] = '{Max.ToLuaString()}',");
            else
                code.AppendLine($@"['Max'] = nil,");

            if (!string.IsNullOrWhiteSpace(Min))
                code.AppendLine($@"['Min'] = '{Min.ToLuaString()}',");
            else
                code.AppendLine($@"['Min'] = nil,");

            code.AppendLine($@"['UniqueString'] ={(UniqueString.ToString().ToLower())},");

            if (!string.IsNullOrWhiteSpace(ColumnName))
                code.AppendLine($@"['ColumnName'] = '{ColumnName.ToLuaString()}',");
            else
                code.AppendLine($@"['ColumnName'] = nil,");

            code.AppendLine($@"['DbNullable'] ={(DbNullable.ToString().ToLower())},");

            if (!string.IsNullOrWhiteSpace(DbType))
                code.AppendLine($@"['DbType'] = '{DbType.ToLuaString()}',");
            else
                code.AppendLine($@"['DbType'] = nil,");

            code.AppendLine($@"['Precision'] ={Datalen},");

            if (!string.IsNullOrWhiteSpace(ArrayLen))
                code.AppendLine($@"['ArrayLen'] = '{ArrayLen.ToLuaString()}',");
            else
                code.AppendLine($@"['ArrayLen'] = nil,");

            code.AppendLine($@"['Scale'] ={Scale},");

            code.AppendLine($@"['DbIndex'] ={DbIndex},");

            code.AppendLine($@"['Unicode'] ={(Unicode.ToString().ToLower())},");

            code.AppendLine($@"['FixedLength'] ={(FixedLength.ToString().ToLower())},");

            code.AppendLine($@"['IsBlob'] ={(IsBlob.ToString().ToLower())},");

            if (!string.IsNullOrWhiteSpace(StorageProperty))
                code.AppendLine($@"['StorageProperty'] = '{StorageProperty.ToLuaString()}',");
            else
                code.AppendLine($@"['StorageProperty'] = nil,");

            code.AppendLine($@"['DbInnerField'] ={(DbInnerField.ToString().ToLower())},");

            code.AppendLine($@"['NoStorage'] ={(NoStorage.ToString().ToLower())},");

            code.AppendLine($@"['KeepStorageScreen'] ='{KeepStorageScreen}',");

            code.AppendLine($@"['CustomWrite'] ={(CustomWrite.ToString().ToLower())},");

            code.AppendLine($@"['IsLinkField'] ={(IsLinkField.ToString().ToLower())},");

            if (!string.IsNullOrWhiteSpace(LinkTable))
                code.AppendLine($@"['LinkTable'] = '{LinkTable.ToLuaString()}',");
            else
                code.AppendLine($@"['LinkTable'] = nil,");

            code.AppendLine($@"['IsLinkKey'] ={(IsLinkKey.ToString().ToLower())},");

            code.AppendLine($@"['IsLinkCaption'] ={(IsLinkCaption.ToString().ToLower())},");

            code.AppendLine($@"['IsUserId'] ={(IsUserId.ToString().ToLower())},");

            if (!string.IsNullOrWhiteSpace(LinkField))
                code.AppendLine($@"['LinkField'] = '{LinkField.ToLuaString()}',");
            else
                code.AppendLine($@"['LinkField'] = nil,");

            code.AppendLine($@"['IsCustomCompute'] ={(IsCustomCompute.ToString().ToLower())},");

            code.AppendLine($@"['CanGet'] ={(CanGet.ToString().ToLower())},");

            code.AppendLine($@"['CanSet'] ={(CanSet.ToString().ToLower())},");

            if (!string.IsNullOrWhiteSpace(AccessType))
                code.AppendLine($@"['AccessType'] = '{AccessType.ToLuaString()}',");
            else
                code.AppendLine($@"['AccessType'] = nil,");

            code.AppendLine($@"['ReadOnly'] ={(ReadOnly.ToString().ToLower())},");

            code.AppendLine($@"['CanInput'] ={(CanUserInput.ToString().ToLower())},");

            if (!string.IsNullOrWhiteSpace(ExtendRole))
                code.AppendLine($@"['ExtendRole'] = '{ExtendRole.ToLuaString()}',");
            else
                code.AppendLine($@"['ExtendRole'] = nil,");

            if (!string.IsNullOrWhiteSpace(ValueSeparate))
                code.AppendLine($@"['ValueSeparate'] = '{ValueSeparate.ToLuaString()}',");
            else
                code.AppendLine($@"['ValueSeparate'] = nil,");

            if (!string.IsNullOrWhiteSpace(ArraySeparate))
                code.AppendLine($@"['ArraySeparate'] = '{ArraySeparate.ToLuaString()}',");
            else
                code.AppendLine($@"['ArraySeparate'] = nil,");

            code.AppendLine($@"['ExtendArray'] ={(ExtendArray.ToString().ToLower())},");

            code.AppendLine($@"['IsKeyValueArray'] ={(IsKeyValueArray.ToString().ToLower())},");

            code.AppendLine($@"['IsRelation'] ={(IsRelation.ToString().ToLower())},");

            if (!string.IsNullOrWhiteSpace(ExtendPropertyName))
                code.AppendLine($@"['ExtendPropertyName'] = '{ExtendPropertyName.ToLuaString()}',");
            else
                code.AppendLine($@"['ExtendPropertyName'] = nil,");

            if (!string.IsNullOrWhiteSpace(ExtendClassName))
                code.AppendLine($@"['ExtendClassName'] = '{ExtendClassName.ToLuaString()}',");
            else
                code.AppendLine($@"['ExtendClassName'] = nil,");

            code.AppendLine($@"['ExtendClassIsPredestinate'] ={(ExtendClassIsPredestinate.ToString().ToLower())},");

            code.AppendLine($@"['IsRelationField'] ={(IsRelationField.ToString().ToLower())},");

            code.AppendLine($@"['IsRelationValue'] ={(IsRelationValue.ToString().ToLower())},");

            code.AppendLine($@"['IsRelationArray'] ={(IsRelationArray.ToString().ToLower())},");

            code.AppendLine($@"['IsExtendArray'] ={(IsExtendArray.ToString().ToLower())},");

            code.AppendLine($@"['IsExtendValue'] ={(IsExtendValue.ToString().ToLower())},");

        }
    }

    partial class SolutionConfig
    {
        /// <summary>
        /// LUA结构支持
        /// </summary>
        /// <returns></returns>
        public override void GetLuaStruct(StringBuilder code)
        {
            base.GetLuaStruct(code);
            int idx;
            idx = 0;
            code.AppendLine("['Enums'] ={");
            foreach (var val in Enums)
            {
                if (idx++ > 0)
                    code.Append(',');
                code.AppendLine($@"{val.GetLuaStruct()}");
            }
            code.AppendLine("},");

            idx = 0;
            code.AppendLine("['TypedefItems'] ={");
            foreach (var val in TypedefItems)
            {
                if (idx++ > 0)
                    code.Append(',');
                code.AppendLine($@"{val.GetLuaStruct()}");
            }
            code.AppendLine("},");

            idx = 0;
            code.AppendLine("['Entities'] ={");
            foreach (var val in Entities)
            {
                if (idx++ > 0)
                    code.Append(',');
                code.AppendLine($@"{val.GetLuaStruct()}");
            }
            code.AppendLine("},");

            code.AppendLine($@"['IsOrther'] ={(IsWeb.ToString().ToLower())},");


            idx = 0;
            code.AppendLine("['Projects'] ={");
            foreach (var val in Projects)
            {
                if (idx++ > 0)
                    code.Append(',');
                code.AppendLine($@"{val.GetLuaStruct()}");
            }
            code.AppendLine("},");

            idx = 0;
            code.AppendLine("['ApiItems'] ={");
            foreach (var val in ApiItems)
            {
                if (idx++ > 0)
                    code.Append(',');
                code.AppendLine($@"{val.GetLuaStruct()}");
            }
            code.AppendLine("},");

            idx = 0;
            code.AppendLine("['NotifyItems'] ={");
            foreach (var val in NotifyItems)
            {
                if (idx++ > 0)
                    code.Append(',');
                code.AppendLine($@"{val.GetLuaStruct()}");
            }
            code.AppendLine("},");

        }
    }

    partial class TableReleation
    {
        /// <summary>
        /// LUA结构支持
        /// </summary>
        /// <returns></returns>
        public override void GetLuaStruct(StringBuilder code)
        {
            base.GetLuaStruct(code);
            int idx;
            if (!string.IsNullOrWhiteSpace(DisplayName))
                code.AppendLine($@"['DisplayName'] = '{DisplayName.ToLuaString()}',");
            else
                code.AppendLine($@"['DisplayName'] = nil,");

            if (!string.IsNullOrWhiteSpace(ForeignKey))
                code.AppendLine($@"['ForeignKey'] = '{ForeignKey.ToLuaString()}',");
            else
                code.AppendLine($@"['ForeignKey'] = nil,");

            if (!string.IsNullOrWhiteSpace(PrimaryKey))
                code.AppendLine($@"['PrimaryKey'] = '{PrimaryKey.ToLuaString()}',");
            else
                code.AppendLine($@"['PrimaryKey'] = nil,");

            if (!string.IsNullOrWhiteSpace(Friend))
                code.AppendLine($@"['Friend'] = '{Friend.ToLuaString()}',");
            else
                code.AppendLine($@"['Friend'] = nil,");

            code.AppendLine($@"['Releation'] ={Releation},");

            if (!string.IsNullOrWhiteSpace(Condition))
                code.AppendLine($@"['Condition'] = '{Condition.ToLuaString()}',");
            else
                code.AppendLine($@"['Condition'] = nil,");

        }
    }

    partial class TypedefItem
    {
        /// <summary>
        /// LUA结构支持
        /// </summary>
        /// <returns></returns>
        public override void GetLuaStruct(StringBuilder code)
        {
            base.GetLuaStruct(code);
            int idx;
            if (!string.IsNullOrWhiteSpace(KeyWork))
                code.AppendLine($@"['KeyWork'] = '{KeyWork.ToLuaString()}',");
            else
                code.AppendLine($@"['KeyWork'] = nil,");

            if (!string.IsNullOrWhiteSpace(ArrayLen))
                code.AppendLine($@"['ArrayLen'] = '{ArrayLen.ToLuaString()}',");
            else
                code.AppendLine($@"['ArrayLen'] = nil,");

            idx = 0;
            code.AppendLine("['Items'] ={");
            foreach (var val in Items.Values)
            {
                if (idx++ > 0)
                    code.Append(',');
                code.AppendLine($@"{val.GetLuaStruct()}");
            }
            code.AppendLine("},");

        }
    }

    partial class UserCommandConfig
    {
        /// <summary>
        /// LUA结构支持
        /// </summary>
        /// <returns></returns>
        public override void GetLuaStruct(StringBuilder code)
        {
            base.GetLuaStruct(code);
            int idx;
            if (!string.IsNullOrWhiteSpace(Button))
                code.AppendLine($@"['Button'] = '{Button.ToLuaString()}',");
            else
                code.AppendLine($@"['Button'] = nil,");

            if (!string.IsNullOrWhiteSpace(Icon))
                code.AppendLine($@"['Icon'] = '{Icon.ToLuaString()}',");
            else
                code.AppendLine($@"['Icon'] = nil,");

            code.AppendLine($@"['IsLocalAction'] ={(IsLocalAction.ToString().ToLower())},");

            code.AppendLine($@"['IsSingleObject'] ={(IsSingleObject.ToString().ToLower())},");

        }
    }

}
