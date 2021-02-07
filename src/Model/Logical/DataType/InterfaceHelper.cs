using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agebull.Common;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder
{
    /// <summary>
    /// 数据类型辅助类
    /// </summary>
    public class InterfaceHelper
    {
        /// <summary>
        /// 检查字段连接
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="field"></param>
        public static void CheckLinkField(IEntityConfig entity, IFieldConfig field)
        {
            var link = field.IsLinkField ? entity.Project.Find(field.LinkTable) : null;
            var linkField = link?.Find(field.LinkField);
            if (linkField != null)
            {
                field.Option.ReferenceConfig = linkField;
                field.Option.IsLink = true;
                return;
            }
            field.IsLinkCaption = false;
            field.IsLinkField = false;
            field.IsLinkKey = false;
            field.IsCompute = false;
        }

        /// <summary>
        /// 检查接口，用于确定最终属性
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="properties"></param>
        public static void CheckLastInterface(IEntityConfig entity, int idx)
        {
            if (string.IsNullOrWhiteSpace(entity.Interfaces))
                return;
            var interfaces = entity.Interfaces.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var inf in interfaces)
            {
                var ie = GlobalConfig.GetEntity(inf);
                if (ie == null)
                    continue;
                foreach (var iField in ie.Properties.ToArray())
                {
                    if (iField.IsDelete || iField.IsDiscard)
                        continue;
                    if (!entity.TryGet(out var field, iField.Name, iField.DbFieldName))
                    {
                        var f = new FieldConfig();
                        f.Copy(iField);
                        entity.LastProperties.TryAdd(f);
                        field = f;
                        field.Index = ++idx;
                    }
                    field.IsInterfaceField = true;
                    field.Option.ReferenceKey = iField.Key;
                    field.Entity = entity.Entity;
                    field.Option.ReferenceConfig = iField;
                    field.Option.IsReference = true;
                    field.LinkTable = entity.Name;
                    field.LinkField = iField.Name;
                }
            }
        }

        public static void RemoveInterface(EntityConfig entity, string name)
        {
            var words = entity.Interfaces?.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
            var interfaces = words == null || words.Length == 0 ? new List<string>() : new List<string>(words);
            interfaces.Remove(name);
            entity.Interfaces = interfaces.Count == 0 ? null : interfaces.LinkToString(",");
        }

        public static void AddInterface(EntityConfig entity, string name)
        {
            var words = entity.Interfaces?.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
            var interfaces = words == null || words.Length == 0 ? new List<string>() : new List<string>(words);
            var iEntity = GlobalConfig.Find(name);
            if (iEntity == null)
            {
                interfaces.Remove(name);
                entity.Interfaces = interfaces.Count == 0 ? null : entity.Interfaces.LinkToString(",");
                return;
            }
            if (interfaces.Exist(name))
            {
                return;
            }
            interfaces.Add(iEntity.Name);
            var array = iEntity.Properties.Where(p => !p.IsDiscard).ToArray();
            var existFields = new List<(FieldConfig inf, FieldConfig field)>();
            foreach (var iField in array)
            {
                if (entity.TryGet(out var field, iField.Name, iField.DbFieldName))
                {
                    existFields.Add((iField, field));
                }
            }
            if (existFields.Count == array.Length)
            {
                foreach ((FieldConfig iField, FieldConfig field) in existFields)
                {
                    if (iField.NoProperty)
                        entity.Remove(field);
                    else
                    {
                        field.Option.ReferenceConfig = iField;
                        field.IsInterfaceField = true;
                    }
                }
            }
            entity.Interfaces = interfaces.Count == 0 ? null : interfaces.LinkToString(",");
        }

        public static void FindInterface(EntityConfig entity)
        {
            var iEntities = GlobalConfig.Entities.Where(p => p.IsInterface);
            var words = entity.Interfaces?.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
            var interfaces = words == null || words.Length == 0 ? new List<string>() : new List<string>(words);
            foreach (var iEntity in iEntities)
            {
                var array = iEntity.Properties.Where(p => !p.IsDiscard).ToArray();
                var existFields = new List<(FieldConfig, FieldConfig)>();
                foreach (var iField in array)
                {
                    if (entity.TryGet(out var field, iField.Name, iField.DbFieldName))
                    {
                        existFields.Add((iField, field));
                    }
                }
                if (existFields.Count == array.Length)
                {
                    if (!interfaces.Exist(iEntity.Name))
                        interfaces.Add(iEntity.Name);
                    foreach ((FieldConfig iField, FieldConfig field) in existFields)
                    {
                        if (iField.NoProperty)
                            entity.Remove(field);
                        else
                        {
                            field.Option.ReferenceConfig = iField;
                            field.IsInterfaceField = true;
                        }
                    }
                }
                entity.Interfaces = interfaces.Count == 0 ? null : interfaces.LinkToString(",");
            }
        }

        public static void ClearInterface(EntityConfig entity)
        {
            var interfaces = entity.Interfaces.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var iField in entity.Properties.ToArray())
            {
                if (iField.IsInterfaceField ||
                    iField.Option.ReferenceConfig is FieldConfig refField &&
                    (refField.Entity.IsInterface || interfaces.Contains(refField.Entity.Name)))
                {
                    entity.Remove(iField);
                }
            }
            entity.Interfaces = null;
        }
    }
}