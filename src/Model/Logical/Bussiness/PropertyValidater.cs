using Agebull.EntityModel.Config.Mysql;
using Agebull.EntityModel.Config.SqlServer;
using System;

namespace Agebull.EntityModel.Config
{
    public class PropertyValidater : ConfigValidaterBase
    {
        #region ����
        public DataBaseType DataBaseType { get; set; }

        public FieldConfig Property { get; set; } 
        bool IsClass => Property.Entity.EnableDataBase;
        bool IsReference => Property.Entity.IsReference;

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
            else if (Property.Name == "NewField" || Property.Name[0] >= '0' && Property.Name[0] <= '9')
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
            var field = Property.DataBaseField;
            if (field.DbFieldName.IsMissing())
            {
                result = false;
                Message.Track = "====>�ֶδ洢���Ʋ���Ϊ��";
            }
            else if (field.DbFieldName == "NewField" ||
                     field.DbFieldName[0] >= '0' && field.DbFieldName[0] <= '9')
            {
                result = false;
                Message.Track = "====>�ֶδ洢���Ʋ���ȷ:" + field.DbFieldName;
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
            bool ist = DataBaseType == DataBaseType.SqlServer
                        ? SqlServerHelper.IsDataBaseType(field.FieldType)
                        : MySqlDataBaseHelper.IsDataBaseType(field.FieldType);
            if (!ist)
            {
                result = false;
                Message.Track = "====>�ֶδ洢���Ͳ���ȷ" + field.FieldType;
            }

            if (field.IsLinkKey || field.IsPrimaryKey)
                field.DbNullable = false;
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
