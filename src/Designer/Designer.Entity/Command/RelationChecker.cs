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
        internal static void DoCheck(EntityConfig entity)
        {
            Trace.WriteLine($"**********{entity.Caption ?? entity.Name}**********");
            try
            {
                var fields = entity.Where(p =>
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
                    field.IsCompute = false;
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
        internal static void CheckLinkType(EntityConfig entity)
        {
            foreach (var field in entity.Where(p => p.Option.IsLink).ToArray())
            {
                var friend = GlobalConfig.GetEntity(field.LinkTable);
                var link = friend?.Find(p =>
                    (string.Equals(p.Name, field.LinkField, StringComparison.OrdinalIgnoreCase)));
                if (link == null)
                    continue;
                field.LinkField = link.Name;
                field.DataType = link.DataType;
                field.CsType = link.CsType;
                field.FieldType = link.FieldType;
                field.Datalen = link.Datalen;
            }
        }
        /// <summary>
        ///     检查
        /// </summary>
        /// <returns></returns>
        internal static void ClearLink(EntityConfig entity)
        {
            Trace.WriteLine($"**********{entity.Caption ?? entity.Name}**********");
            try
            {
                foreach (var field in entity.Properties)
                {
                    field.LinkTable = null;
                    field.LinkField = null;
                    field.IsLinkKey = false;
                    field.IsLinkCaption = false;
                    field.IsLinkField = false;
                    field.Option.ReferenceConfig = null;
                    field.Option.IsLink = false;
                    field.IsCompute = false;
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
        internal static void DoLink(EntityConfig e)
        {
            DataTableConfig entity = e.DataTable;
            bool hase = false;
            Trace.WriteLine($"**********{entity.Caption ?? entity.Name}**********");
            try
            {
                foreach (var field in entity.Where(p => p.IsLinkKey).ToArray())
                {
                    var re = GlobalConfig.GetEntity(field.LinkTable);
                    var caption = re?.CaptionColumn;
                    if (caption == null)
                        continue;
                    hase = true;

                    field.LinkTable = re.Name;
                    field.LinkField = re.PrimaryColumn.Name;
                    field.IsLinkKey = true;
                    field.Option.ReferenceConfig = re.PrimaryColumn;
                    field.Option.IsLink = true;
                    field.IsReadonly = true;
                    var cf = entity.Find(p => (p.LinkField == caption.Name && p.LinkTable == re.Name) || (p.Name == caption.Name && p.NoStorage));
                    if (cf == null)
                    {
                        var property = new FieldConfig
                        {
                            Name = caption.Name
                        };
                        entity.Entity.Entity.Add(property);
                        property.Copy(caption.Field);
                        entity.Add(cf = new DataBaseFieldConfig
                        {
                            Parent = entity,
                            Property = property
                        });
                        cf.Copy(property);
                    }
                    cf.NoStorage = false;
                    cf.Caption = $"{re.Caption}{caption.Caption}";
                    cf.Index = field.Index;
                    cf.LinkTable = re.Name;
                    cf.LinkField = caption.Name;
                    cf.IsReadonly = false;
                    cf.IsLinkCaption = true;
                    cf.IsLinkKey = false;
                    cf.Option.ReferenceConfig = caption.Field;
                    cf.Option.IsLink = true;
                    Trace.WriteLine($"{entity.Caption ?? entity.Name}:{field.Name} => {re.Caption ?? re.Name}:{re.PrimaryField }");
                }

                if (!hase)
                {
                    entity.ReadTableName = entity.SaveTableName;
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
