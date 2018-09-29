﻿using System;
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
        /// 检查字段关联
        /// </summary>
        public static bool CheckFieldLink(EntityConfig entity)
        {
            bool hase = false;
            foreach (var field in entity.Properties)
            {
                if (entity.NoDataBase || string.IsNullOrWhiteSpace(field.LinkTable) ||
                    field.LinkTable == entity.Name || field.LinkTable == entity.ReadTableName ||
                    field.LinkTable == entity.SaveTableName)
                {
                    SetNoLink(field);
                    continue;
                }

                PropertyConfig pro = null;
                if (field.Option.ReferenceKey != Guid.Empty)
                {
                    pro = GlobalConfig.GetConfig<PropertyConfig>(field.Option.ReferenceKey);
                }

                if (pro == null || pro == field || pro.Parent == entity)
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

                if (pro?.Parent == null || pro == field || pro.Parent == entity || pro.Parent.IsInterface)
                {
                    SetNoLink(field);
                    continue;
                }
                field.Option.IsLink = true;
                field.Option.ReferenceConfig = pro;

                field.IsLinkField = true;
                field.IsLinkKey = pro.IsPrimaryKey;
                if (field.IsLinkKey)
                    field.IsCompute = false;
                field.IsLinkCaption = pro.IsCaption;
                field.LinkTable = pro.Parent.Name;
                field.LinkField = pro.Name;
                hase = true;
            }
            return hase;
        }

        private static void SetNoLink(PropertyConfig field)
        {
            field.LinkTable = field.LinkField = null;
            if (!field.Option.IsReference)
            {
                field.Option.ReferenceKey = Guid.Empty;
            }
            field.Option.IsLink = false;
            field.IsLinkField = field.IsLinkKey = field.IsLinkCaption = false;
        }
    }
}
