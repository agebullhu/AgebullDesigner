using Agebull.EntityModel.Config;
using Agebull.EntityModel.Config.V2021;
using System;
using System.Diagnostics;
using System.Linq;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 实体关联关系检查器
    /// </summary>
    public class RelationChecker
    {
        /// <summary>
        ///     检查
        /// </summary>
        /// <returns></returns>
        internal static void DoCheck(IEntityConfig entity)
        {
            Trace.WriteLine($"**********{entity.Caption ?? entity.Name}**********");
            try
            {
                var fields = entity.DataTable.Where(p =>
                    /*p.CsType == "long" && */p.Name.Length > 3 &&
                    string.Equals(p.Name.Substring(p.Name.Length - 2), "ID", StringComparison.OrdinalIgnoreCase)).ToArray();
                foreach (var field in fields)
                {
                    var words = GlobalConfig.SplitWords(field.Name);
                    if (words.Count > 2)
                        continue;
                    var re = GlobalConfig.GetEntity(p => p.PrimaryColumn != null
                        && ((string.Equals(p.Name, words[0], StringComparison.OrdinalIgnoreCase)
                         && string.Equals(p.PrimaryField, words[1], StringComparison.OrdinalIgnoreCase))
                        || p.PrimaryColumn.Alias == field.Name));
                    if (re == null)
                    {
                        continue;
                    }
                    field.IsLinkCaption = false;
                    field.IsLinkField = true;
                    field.LinkTable = re.Name;
                    field.LinkField = re.PrimaryColumn.Name;
                    field.IsLinkKey = true;
                    Trace.WriteLine($"{entity.Caption ?? entity.Name}:{field.Name} => {re.Caption ?? re.Name}:{re.PrimaryField }");
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
            }
        }

        /// <summary>
        ///     检查
        /// </summary>
        /// <returns></returns>
        internal static void CheckLinkType(IEntityConfig entity)
        {
            foreach (var field in entity.DataTable.Where(p => p.Option.IsLink).ToArray())
            {
                var friend = GlobalConfig.GetEntity(field.LinkTable);
                var link = friend?.DataTable?.Find(p =>
                    (string.Equals(p.Name, field.LinkField, StringComparison.OrdinalIgnoreCase)));
                if (link == null)
                    continue;
                field.LinkField = link.Name;
                field.Property.DataType = link.DataType;
                field.Property.CsType = link.CsType;
                field.FieldType = link.FieldType;
                field.Datalen = link.Datalen;
            }
        }
        /// <summary>
        ///     检查
        /// </summary>
        /// <returns></returns>
        internal static void ClearLink(IEntityConfig entity)
        {
            DataTableConfig dataTable = entity.DataTable;
            Trace.WriteLine($"[ClearLink] {entity.Caption ?? entity.Name}");
            try
            {
                foreach (var field in dataTable.Fields)
                {
                    field.LinkTable = null;
                    field.LinkField = null;
                    field.IsLinkKey = false;
                    field.IsLinkCaption = false;
                    field.IsLinkField = false;
                    field.Option.ReferenceConfig = null;
                    field.Option.IsLink = false;
                    field.CanInsert=field.CanUpdate = true;
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
            }
        }
        /// <summary>
        ///     链接标题字段
        /// </summary>
        /// <returns></returns>
        internal static void DoLinkCaption(IEntityConfig entity)
        {
            DataTableConfig dataTable = entity.DataTable;
            bool hase = false;
            Trace.WriteLine($"[DoLink] {entity.Caption ?? entity.Name}");
            try
            {
                foreach (var linkKey in dataTable.FindAndToArray(p => p.IsLinkKey))
                {
                    var linkTable = GlobalConfig.GetEntity(linkKey.LinkTable);
                    var caption = linkTable?.CaptionColumn;
                    if (caption == null)
                        continue;
                    hase = true;

                    linkKey.LinkTable = linkTable.Name;
                    linkKey.LinkField = linkTable.PrimaryColumn.Name;
                    linkKey.IsLinkKey = true;
                    linkKey.Option.ReferenceConfig = linkTable.PrimaryColumn.Field;
                    linkKey.Option.IsLink = true;
                    
                    var linkCaption = dataTable.Find(p => p.LinkField == caption.Name && p.LinkTable == linkTable.Name);
                    if (linkCaption == null)
                    {
                        var property = new FieldConfig
                        {
                            Name = caption.Name
                        };
                        dataTable.Entity.Entity.Add(property);
                        property.Copy(caption.Field);
                        linkCaption = property.DataBaseField;
                        linkCaption.Copy(caption.Field);
                        linkCaption.LinkTable = linkTable.Name;
                        linkCaption.LinkField = caption.Name;
                    }
                    linkCaption.NoStorage = false;
                    linkCaption.Caption = $"{linkTable.Caption}{caption.Caption}";
                    linkCaption.Index = linkKey.Index;
                    linkCaption.IsLinkCaption = true;
                    linkCaption.IsLinkKey = false;
                    linkCaption.Option.ReferenceConfig = caption.Field;
                    linkCaption.Option.IsLink = true;
                    Trace.WriteLine($"{dataTable.Caption ?? dataTable.Name}:{linkKey.Name} => {linkTable.Caption ?? linkTable.Name}:{linkTable.PrimaryField }");
                }

                if (!hase)
                {
                    dataTable.ReadTableName = dataTable.SaveTableName;
                    return;
                }
                //if (string.IsNullOrWhiteSpace(entity.ReadTableName) || entity.ReadTableName == entity.SaveTable)
                //{
                //    entity.ReadTableName = DataBaseHelper.ToViewName(entity);
                //}
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
        }
    }
}
