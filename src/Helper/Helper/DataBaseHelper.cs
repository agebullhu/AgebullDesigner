using System;
using System.Collections.Generic;
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

                FieldConfig pro = null;
                if (field.Option.ReferenceKey != Guid.Empty)
                {
                    pro = GlobalConfig.GetConfig<FieldConfig>(field.Option.ReferenceKey);
                }
                //if (pro != null && field.LinkField != null && field.LinkField != pro.Name)
                //    pro = null;
                if (pro == null || pro == field || pro.Entity == entity)
                {
                    var table = GlobalConfig.GetEntity(
                        p => string.Equals(p.Name, field.LinkTable, StringComparison.OrdinalIgnoreCase) ||
                             string.Equals(p.SaveTableName, field.LinkTable, StringComparison.OrdinalIgnoreCase) ||
                             string.Equals(p.ReadTableName, field.LinkTable, StringComparison.OrdinalIgnoreCase));
                    if (table != null && table != entity)
                    {
                        pro = table.Properties.FirstOrDefault(p =>
                            string.Equals(p.Name, field.LinkField, StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(p.DbFieldName, field.LinkField, StringComparison.OrdinalIgnoreCase));
                    }
                }

                if (pro?.Entity == null || pro == field || pro.Entity == entity || pro.Entity.IsInterface)
                {
                    SetNoLink(field);
                    continue;
                }
                field.Option.IsLink = true;
                field.Option.ReferenceConfig = pro;

                field.IsLinkField = true;
                field.IsLinkKey = pro.IsPrimaryKey;
                field.IsCompute = !field.IsLinkKey;
                field.IsLinkCaption = pro.IsCaption;
                field.LinkTable = pro.Entity.Name;
                field.LinkField = pro.Name;
                field.NoStorage = false;
                hase = true;
            }
            return hase;
        }

        private static void SetNoLink(FieldConfig field)
        {
            field.LinkTable = field.LinkField = null;
            field.Option.ReferenceKey = Guid.Empty;
            field.IsLinkField = field.IsLinkKey = field.IsLinkCaption = false;
        }
    }
}
