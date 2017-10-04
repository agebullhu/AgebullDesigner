using Agebull.Common.Defaults;

namespace Gboxt.Common.DataAccess.Schemas
{
    public class PropertyValidater : ConfigValidaterBase
    {
        #region 定义

        public PropertyConfig Property { get; set; }
        bool IsClass => Property.Parent.IsClass;
        bool IsReference => Property.Parent.IsReference;

        #endregion

        #region 校验

        /// <summary>
        ///     数据校验
        /// </summary>
        protected override bool Validate()
        {
            var result = true;
            if (string.IsNullOrWhiteSpace(Property.Name))
            {
                result = false;
                Message.Track = "====>属性名称不能为空";
            }
            else if (Property.Name == "NewField" || (Property.Name[0] >= '0' && Property.Name[0] <= '9'))
            {
                result = false;
                Message.Track = "====>属性名称不正确:" + Property.Name;
            }
            else
            {
                Property.Name = Property.Name.Trim();
            }
            if (IsReference)
                return result;
            string cstype = Property.CsType;
            if (cstype.Length > 4 && cstype.Substring(cstype.Length - 4, 4) == "Data")
                cstype = cstype.Substring(0, cstype.Length - 4);
            if (!CsharpHelper.IsCsType(cstype))
            {
                result = false;
                Message.Track = "====>字段类型不正确" + Property.CsType;
            }

            if (IsClass)
                return result;
            if (Property.IsPrimaryKey && Property.Nullable)
            {
                result = false;
                Message.Track = "====>主键被设置为可为空";
            }
            if (Property.NoStorage)
                return result;
            if (string.IsNullOrWhiteSpace(Property.ColumnName))
            {
                result = false;
                Message.Track = "====>字段存储名称不能为空";
            }
            else if (Property.ColumnName == "NewField" ||
                     (Property.ColumnName[0] >= '0' && Property.ColumnName[0] <= '9'))
            {
                result = false;
                Message.Track = "====>字段存储名称不正确:" + Property.ColumnName;
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
            if (!DataBaseHelper.IsDataBaseType(Property.DbType))
            {
                result = false;
                Message.Track = "====>字段存储类型不正确" + Property.DbType;
            }

            if (Property.IsLinkKey)
                Property.DbNullable = false;
            if (Property.IsCompute)
                Property.DbNullable = true;
            if (Property.IsPrimaryKey)
                Property.DbNullable = false;
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
        //        friend = entity.Properties.FirstOrDefault(p => p.Name == Property.Name || Property.Alias?.Contains(p.Name) == true);

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
