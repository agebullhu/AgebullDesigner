using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Agebull.EntityModel.Config.SqlServer;

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
        /// <param name="Entity"></param>
        /// <returns></returns>
        public static string ToTableName(EntityConfig Entity)
        {
            string head = "tb_";
            if (Entity.Classify != null)
            {
                var cls = Entity.Parent.Classifies.FirstOrDefault(p => p.Name == Entity.Classify);
                if (cls != null && !string.IsNullOrEmpty(cls.Abbreviation))
                    head = cls.Abbreviation?.ToLower() + "_";
            }
            if (!string.IsNullOrWhiteSpace(Entity.Parent.Abbreviation))
                head += Entity.Parent.Abbreviation.ToLower() + "_";
            return GlobalConfig.SplitWords(Entity.Name).Select(p => p.ToLower()).LinkToString(head, "_");
        }
        /// <summary>
        /// 到标准数据表名称
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string ToColumnName(string name)
        {
            return GlobalConfig.ToName(GlobalConfig.SplitWords(name).Select(p => p.ToLower()).ToList());
        }

        /// <summary>
        /// 执行器
        /// </summary>
        public static bool CheckFieldLink(EntityConfig entity)
        {
            bool hase = false;
            foreach (var field in entity.Properties)
            {
                if (entity.NoDataBase || field.IsPrimaryKey || field.Name == "Memo" || field.Name == "ParentId" || field.LinkTable == "UiElement" || field.LinkTable == "tb_UiElement" || field.LinkTable == entity.Name || field.LinkTable == entity.ReadTableName || field.LinkTable == entity.SaveTableName)
                {
                    field.LinkTable = field.LinkField = null;
                    field.IsLinkField = field.IsLinkKey = field.IsLinkCaption = false;
                    continue;
                }
                if (!string.IsNullOrWhiteSpace(field.LinkTable))
                {
                    PropertyConfig pro = null;
                    if (field.Option.ReferenceKey != Guid.Empty)
                    {
                        pro = GlobalConfig.GetConfig<PropertyConfig>(field.Option.ReferenceKey);
                    }
                    if(pro == null || pro == field || pro.Parent == entity)
                    {
                        var table = GlobalConfig.GetEntity(
                            p => string.Equals(p.Name, field.LinkTable, StringComparison.OrdinalIgnoreCase) ||
                                 string.Equals(p.SaveTable, field.LinkTable, StringComparison.OrdinalIgnoreCase) ||
                                 string.Equals(p.ReadTableName, field.LinkTable, StringComparison.OrdinalIgnoreCase));
                        if (table != null && table != entity)
                        {
                            pro = table.Properties.FirstOrDefault(p =>
                                string.Equals(p.Name, field.LinkField, StringComparison.OrdinalIgnoreCase) ||
                                string.Equals(p.ColumnName, field.LinkField, StringComparison.OrdinalIgnoreCase));
                        }
                    }
                    if (pro?.Parent != null && pro != field && pro.Parent != entity && !pro.Parent.IsInterface)
                    {
                        field.IsLinkField = true;
                        field.IsLinkKey = pro.IsPrimaryKey;
                        field.IsLinkCaption = pro.IsCaption;
                        if (field.IsLinkKey)
                            field.IsCompute = false;
                        field.Option.ReferenceConfig = pro;
                        field.LinkTable = pro.Parent.Name;
                        field.LinkField = pro.Name;
                        hase = true;
                        continue;
                    }
                }
                field.LinkTable = field.LinkField = null;
                field.IsLinkField = field.IsLinkKey = field.IsLinkCaption = false;
            }
            return hase;
        }
    }
}
