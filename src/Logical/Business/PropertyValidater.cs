using Agebull.Common.Defaults;

namespace Gboxt.Common.DataAccess.Schemas
{
    public class PropertyValidater : ConfigValidaterBase
    {
        #region ����

        public PropertyConfig Property { get; set; }
        bool IsClass => Property.Parent.IsClass;
        bool IsReference => Property.Parent.IsReference;

        #endregion

        #region У��

        /// <summary>
        ///     ����У��
        /// </summary>
        protected override bool Validate()
        {
            var result = true;
            if (string.IsNullOrWhiteSpace(Property.Name))
            {
                result = false;
                Message.Track = "====>�������Ʋ���Ϊ��";
            }
            else if (Property.Name == "NewField" || (Property.Name[0] >= '0' && Property.Name[0] <= '9'))
            {
                result = false;
                Message.Track = "====>�������Ʋ���ȷ:" + Property.Name;
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
                Message.Track = "====>�ֶ����Ͳ���ȷ" + Property.CsType;
            }

            if (IsClass)
                return result;
            if (Property.IsPrimaryKey && Property.Nullable)
            {
                result = false;
                Message.Track = "====>����������Ϊ��Ϊ��";
            }
            if (Property.NoStorage)
                return result;
            if (string.IsNullOrWhiteSpace(Property.ColumnName))
            {
                result = false;
                Message.Track = "====>�ֶδ洢���Ʋ���Ϊ��";
            }
            else if (Property.ColumnName == "NewField" ||
                     (Property.ColumnName[0] >= '0' && Property.ColumnName[0] <= '9'))
            {
                result = false;
                Message.Track = "====>�ֶδ洢���Ʋ���ȷ:" + Property.ColumnName;
            }
            //if (CreateIndex && IsPrimaryKey)
            //{
            //    result = false;
            //    trace.Track = "====>��������Ҫ��������";
            //}
            //if (IsUserId && CsType != "int")
            //{
            //    result = false;
            //    trace.Track = "====>�ֶ�Ϊ�û�IDӳ����ֶ����Ͳ���Int��";
            //}
            if (!DataBaseHelper.IsDataBaseType(Property.DbType))
            {
                result = false;
                Message.Track = "====>�ֶδ洢���Ͳ���ȷ" + Property.DbType;
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
            //    this.Message.Track = "====>�ֶ���Ҫ�����������ֶα�����Ϊ��Ϊ��";
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
