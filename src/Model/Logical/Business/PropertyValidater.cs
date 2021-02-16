using Agebull.EntityModel.Config.Mysql;
using Agebull.EntityModel.Config.SqlServer;

namespace Agebull.EntityModel.Config
{
    public class PropertyValidater : ConfigValidaterBase
    {
        #region 定义
        public DataBaseType DataBaseType { get; set; }

        public FieldConfig Field { get; set; }
        bool IsClass => Field.Entity.EnableDataBase;
        bool IsReference => Field.Entity.IsReference;

        #endregion

        #region 校验

        /// <summary>
        ///     数据校验
        /// </summary>
        protected override bool Validate()
        {
            var result = true;
            if (string.IsNullOrWhiteSpace(Field.Name))
            {
                result = false;
                Message.Track = "====>属性名称不能为空";
            }
            else if (Field.Name == "NewField" || Field.Name[0] >= '0' && Field.Name[0] <= '9')
            {
                result = false;
                Message.Track = "====>属性名称不正确:" + Field.Name;
            }
            else
            {
                Field.Name = Field.Name.Trim();
            }
            if (IsReference)
                return result;
            string cstype = Field.CsType;
            if (cstype.Length > 4 && cstype.Substring(cstype.Length - 4, 4) == "Data")
                cstype = cstype.Substring(0, cstype.Length - 4);
            if (!CsharpHelper.IsCsType(cstype))
            {
                result = false;
                Message.Track = "====>字段类型不正确" + Field.CsType;
            }

            if (IsClass)
                return result;
            if (Field.IsPrimaryKey && Field.Nullable)
            {
                result = false;
                Message.Track = "====>主键被设置为可为空";
            }
            if (Field.NoStorage)
                return result;
            if (string.IsNullOrWhiteSpace(Field.DbFieldName))
            {
                result = false;
                Message.Track = "====>字段存储名称不能为空";
            }
            else if (Field.DbFieldName == "NewField" ||
                     Field.DbFieldName[0] >= '0' && Field.DbFieldName[0] <= '9')
            {
                result = false;
                Message.Track = "====>字段存储名称不正确:" + Field.DbFieldName;
            }
            //if (CreateIndex && IsPrimaryKey)
            //{
            //    result = false;
            //    trace.Track = "====>主键不需要建立索引";
            //}
            //if (IsUserId && CsType != "int")
            //{
            //    result = false;
            //    trace.Track = "====>字段为用户ID映射而字段类型不是Int型";
            //}
            bool ist = DataBaseType == DataBaseType.SqlServer
                        ? SqlServerHelper.IsDataBaseType(Field.FieldType)
                        : MySqlDataBaseHelper.IsDataBaseType(Field.FieldType);
            if (!ist)
            {
                result = false;
                Message.Track = "====>字段存储类型不正确" + Field.FieldType;
            }

            if (Field.IsLinkKey)
                Field.DbNullable = false;
            if (Field.IsCompute)
                Field.DbNullable = true;
            if (Field.IsPrimaryKey)
                Field.DbNullable = false;
            //if (Property.CreateIndex && Property.Nullable)
            //{
            //    result = false;
            //    this.Message.Track = "====>字段需要建立索引而字段被设置为可为空";
            //}
            return result;
        }
        #endregion
    }
}
/*
 

        //void FindOld()
        //{
        //    PropertyConfig friend = null;
        //    var entity = CoderBase.GetEntity( Property.Parent.CppName);
        //    if (entity != null)
        //    {
        //        friend = entity.Find(p => p.Name == Property.Name || Property.Alias?.Contains(p.Name) == true);

        //    }
        //    if (friend != null)
        //    {
        //        Property.Tag = $"ES3.0,{friend.Name},{friend.CppType}";
        //        Property.CppType = friend.CppType;
        //        CheckCppType();
        //    }
        //    else
        //    {
        //        Property.Tag = "None";
        //        Property.CppType = Property.CppLastType = CppTypeHelper.CsTypeToCppType(Property);
        //    }
        //}
     */
