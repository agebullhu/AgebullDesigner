using System;
using System.Diagnostics;
using System.Linq;
using Agebull.EntityModel.Config;

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
                var fields = entity.Properties.Where(p => 
                    /*p.CsType == "long" && */p.Name.Length > 3 &&
                    string.Equals(p.Name.Substring(p.Name.Length - 2), "ID",StringComparison.OrdinalIgnoreCase)).ToArray();
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
            foreach (var field in entity.Properties.Where(p => p.IsLink).ToArray())
            {
                var friend = GlobalConfig.GetEntity(field.LinkTable);
                var link = friend?.Properties.FirstOrDefault(p =>
                    (string.Equals(p.Name, field.LinkField, StringComparison.OrdinalIgnoreCase)));
                if (link == null)
                    continue;
                field.LinkField = link.Name;
                field.DataType = link.DataType;
                field.CsType = link.CsType;
                field.DbType = link.DbType;
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
        internal static void DoLink(EntityConfig entity)
        {
            bool hase = false;
            Trace.WriteLine($"**********{entity.Caption ?? entity.Name}**********");
            try
            {
                foreach (var field in entity.Properties.Where(p => p.IsLinkKey).ToArray())
                {
                    var re = GlobalConfig.GetEntity(field.LinkTable);
                    var caption = re?.Properties.FirstOrDefault(p => p.IsCaption);
                    if (caption == null)
                        continue;
                    hase = true;

                    field.LinkTable = re.Name;
                    field.LinkField = re.PrimaryColumn.Name;
                    field.IsLinkKey = true;
                    field.Option.ReferenceConfig = re.PrimaryColumn;
                    field.Option.IsLink = true;
                    field.IsCompute = false;
                    var cf = entity.Properties.FirstOrDefault(p => (p.LinkField == caption.Name&& p.LinkTable == re.Name) || (p.Name == caption.Name && p.NoStorage));
                    if (cf == null)
                    {
                        entity.Add(cf = new FieldConfig
                        {
                            Name = caption.Name
                        });
                        cf.CopyFromProperty(caption, false, true, false);
                        
                    }
                    cf.NoStorage = false;
                    cf.Caption = $"{re.Caption}{caption.Caption}";
                    cf.Index = field.Index;
                    cf.LinkTable = re.Name;
                    cf.LinkField = caption.Name;
                    cf.IsCompute = true;
                    cf.IsLinkCaption = true;
                    cf.IsLinkKey = false;
                    cf.Option.ReferenceConfig = caption;
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
            catch (Exception e)
            {
                Trace.WriteLine(e);
            }
        }
    }
}
