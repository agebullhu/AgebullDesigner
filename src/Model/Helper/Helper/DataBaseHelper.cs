using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    ///     实体实现接口的命令
    /// </summary>
    public class DataBaseHelper
    {
        /// <summary>
        /// 到标准数据表名称
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string ToTableName(EntityConfig entity)
        {
            var head = new StringBuilder("tb_");
            //if (!string.IsNullOrWhiteSpace(entity.Parent.Abbreviation))
            //{
            //    head.Append(entity.Parent.Abbreviation.ToLWord());
            //    head.Append('_');
            //}
            if (entity.Classify != null/* && entity.Parent.Classifies.Count > 1*/)
            {
                var cls = entity.Parent.Classifies.FirstOrDefault(p => p.Name == entity.Classify);
                if (cls != null)
                {
                    head.Append(cls.Abbreviation.ToLWord());
                    head.Append('_');
                }
            }
            return GlobalConfig.SplitWords(entity.Name).Select(p => p.ToLower()).LinkToString(head.ToString(), "_");
        }

        /// <summary>
        /// 到标准数据表名称
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string ToViewName(EntityConfig entity) => "view_" + entity.Name.Replace("tb_", "");

        /// <summary>
        /// 到标准数据表名称
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string ToDbFieldName(string name)
        {
            return GlobalConfig.ToLinkWordName(name, "_", false);
        }

        /// <summary>
        /// 执行器
        /// </summary>
        public static void CheckFieldLink(EntityConfig entity)
        {
            CheckFieldLink(entity.Properties);
        }

        /// <summary>
        /// 检查字段关联
        /// </summary>
        public static bool CheckFieldLink(IEnumerable<FieldConfig> fields)
        {
            bool hase = false;
            foreach (var field in fields)
            {
                var entity = field.Entity;

                if (entity.NoDataBase || string.IsNullOrWhiteSpace(field.LinkTable) ||
                    field.LinkTable == entity.Name || field.LinkTable == entity.ReadTableName ||
                    field.LinkTable == entity.SaveTableName)
                {
                    SetNoLink(field);
                    continue;
                }

                var table = entity.Parent.Find(field.LinkTable) ?? GlobalConfig.GetEntity(field.LinkTable);

                if(table == null || table == entity)
                {
                    SetNoLink(field);
                    continue;
                }
                FieldConfig pro = field.IsLinkKey ? table.PrimaryColumn : table.Find(field.LinkField); 
                if(pro == null && field.Option.ReferenceKey != Guid.Empty)
                {
                    pro = GlobalConfig.GetConfig<FieldConfig>(field.Option.ReferenceKey);
                }
                if (pro == null || pro == field || pro.Entity == entity)
                {
                    SetNoLink(field);
                    continue;
                }
                hase = true;
                field.Option.ReferenceConfig = pro;
                field.Option.IsLink = true;

                field.IsLinkField = true;
                field.IsLinkCaption = pro.IsCaption;
                field.IsLinkKey = pro.IsPrimaryKey;
                field.IsCompute = !field.IsLinkKey;
                field.LinkTable = table.Name;
                field.LinkField = pro.Name;
                field.NoStorage = false;

                field.DbType = pro.DbType;
                field.ArrayLen = pro.ArrayLen;
                field.Datalen = pro.Datalen;
                field.Scale = pro.Scale;

                if (field.IsLinkKey)
                {
                    field.DbNullable = false;
                    field.IsDbIndex = true;
                    field.Nullable = false;
                }

                Trace.WriteLine($"    {(field.IsLinkKey ? 'F' : 'L')}:{field.Caption}({field.Name})");


            }
            return hase;
        }

        private static void SetNoLink(FieldConfig field)
        {
            Trace.WriteLine($"X    :{field.Caption}({field.Name})");
            field.LinkTable = field.LinkField = null;
            field.Option.ReferenceKey = Guid.Empty;
            field.IsLinkField = field.IsLinkKey = field.IsLinkCaption = false;
        }
    }
}
