using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.Designer
{
    public sealed class FilterWriter
    {
        /// <summary>
        ///     XML����edmx�����ռ�
        /// </summary>
        private XNamespace _nsEdmx;

        /// <summary>
        ///     EDMX��ʽ�ĸ�
        /// </summary>
        private XElement _xRuntime;

        /// <summary>
        ///     �������ı�ṹ
        /// </summary>
        public List<EntityConfig> Tables { get; private set; }

        /// <summary>
        ///     �򿪲������ļ�
        /// </summary>
        /// <param name="file"></param>
        public void Open(string file)
        {
            using (FileStream stream = File.OpenRead(file))
            {
                XElement root = XElement.Load(stream);
                _nsEdmx = root.GetNamespaceOfPrefix("xmlns");
                _xRuntime = root.Element(_nsEdmx + "Runtime");
                Tables = new List<EntityConfig>();
                ReadMapping();
                ReadTables();
                ReadEntities();
            }
        }

        /// <summary>
        ///     ��ȡ����ʵ�������,��ʼ����ṹ
        /// </summary>
        private void ReadMapping()
        {
            // ReSharper disable PossibleNullReferenceException
            XElement xMappings = _xRuntime.Element(_nsEdmx + "Mappings");
            var xMapping = xMappings.FirstNode as XElement;
            XNamespace nsDef = xMapping.GetDefaultNamespace();
            XElement xEntityContainerMapping = xMapping.Element(nsDef + "EntityContainerMapping");
            Tables.Clear();
            foreach (XElement xMap in xEntityContainerMapping.Elements(nsDef + "EntitySetMapping"))
            {
                XElement xEntityTypeMapping = xMap.Element(nsDef + "EntityTypeMapping");
                string entityName = GetAttribute(xEntityTypeMapping, "TypeName").Split('.').Last();

                XElement xMappingFragment = xEntityTypeMapping.Element(nsDef + "MappingFragment");
                EntityConfig entity;
                Tables.Add(entity = new EntityConfig
                {
                    Name = entityName,
                    ReadTableName = GetAttribute(xMappingFragment, "StoreEntitySet")
                });
                foreach (XElement xProperty in xMappingFragment.Elements(nsDef + "ScalarProperty"))
                {
                    string name = GetAttribute(xProperty,nameof(entity.Name));
                    entity.Properties.Add(new PropertyConfig
                    {
                        Name = name,
                        ColumnName = GetAttribute(xProperty, "ColumnName")
                    });
                }
            }
            // ReSharper restore PossibleNullReferenceException
        }

        /// <summary>
        ///     ��ȡ���ݱ�ṹ
        /// </summary>
        private void ReadTables()
        {
            XElement xStorageModels = _xRuntime.Element(_nsEdmx + "StorageModels");
            // ReSharper disable PossibleNullReferenceException
            var xSchema = xStorageModels.FirstNode as XElement;
            XNamespace ns = xSchema.GetDefaultNamespace();
            foreach (XElement xMap in xSchema.Elements(ns + "EntityType"))
            {
                string name = GetAttribute(xMap, "Name");
                EntityConfig entity = Tables.FirstOrDefault(p => p.ReadTableName == name);
                if (entity == null)
                {
                    Tables.Add(entity = new EntityConfig
                    {
                        ReadTableName = name,
                        Name = name
                    });
                }

                foreach (XElement xProperty in xMap.Elements(ns + "Property"))
                {
                    name = GetAttribute(xProperty, "Name");
                    PropertyConfig column = entity.Properties.FirstOrDefault(p => p.ColumnName == name);
                    if (column == null)
                    {
                        entity.Properties.Add(column = new PropertyConfig
                        {
                            ColumnName = name,
                            Name = name
                        });
                    }
                    string storeGeneratedPattern = GetAttribute(xProperty, "StoreGeneratedPattern");
                    column.IsCompute = storeGeneratedPattern == "Compute";
                    column.IsIdentity = storeGeneratedPattern == "Identity";

                    column.DbType = GetAttribute(xProperty, "Type");
                    column.Datalen = GetAttribute(xProperty, "MaxLength", int.MaxValue);
                    column.Min = GetAttribute(xProperty, "MinLength");
                    column.Nullable = GetAttribute(xProperty, "Nullable", true);
                    column.FixedLength = GetAttribute(xProperty, "FixedLength", false);
                    column.Unicode = GetAttribute(xProperty, "Unicode", false);
                }

                foreach (XElement xProperty in xMap.Elements(ns + "Key"))
                {
                    XElement xPropertyRef = xProperty.Element(ns + "PropertyRef");
                    name = GetAttribute(xPropertyRef, "Name");
                    PropertyConfig column = entity.Properties.FirstOrDefault(p => p.ColumnName == name);
                    if (column != null)
                    {
                        column.IsPrimaryKey = true;
                    }
                    if (column.IsIdentity)
                    {
                        column.DbType = "INT";
                        column.Nullable = false;
                    }
                }
            }
            // ReSharper restore PossibleNullReferenceException
        }

        /// <summary>
        ///     ��ȡʵ��ṹ
        /// </summary>
        private void ReadEntities()
        {
            // ReSharper disable PossibleNullReferenceException
            XElement xConceptualModels = _xRuntime.Element(_nsEdmx + "ConceptualModels");
            var xSchema = xConceptualModels.FirstNode as XElement;
            XNamespace nsDef = xSchema.GetDefaultNamespace();
            foreach (XElement xMap in xSchema.Elements(nsDef + "EntityType"))
            {
                string name = GetAttribute(xMap, "Name");
                EntityConfig entity = Tables.FirstOrDefault(p => p.Name == name);
                if (entity == null)
                {
                    Tables.Add(entity = new EntityConfig
                    {
                        ReadTableName = name,
                        Name = name
                    });
                }

                foreach (XElement xProperty in xMap.Elements(nsDef + "Property"))
                {
                    name = GetAttribute(xProperty, "Name");
                    PropertyConfig column = entity.Properties.FirstOrDefault(p => p.Name == name || p.ColumnName == name);
                    if (column == null)
                    {
                        entity.Properties.Add(column = new PropertyConfig
                        {
                            ColumnName = name,
                            Name = name
                        });
                    }
                    column.CsType = GetAttribute(xProperty, "Type");
                    column.Datalen = GetAttribute(xProperty, "MaxLength", int.MaxValue);
                    column.Min = GetAttribute(xProperty, "MinLength");

                    if (!column.IsPrimaryKey)
                    {
                        continue;
                    }
                    if (column.CsType == "Guid")
                    {
                        column.IsIdentity = false;
                        column.IsGlobalKey = true;
                    }
                    else if (column.IsIdentity)
                    {
                        column.CsType = "int";
                        column.Nullable = false;
                    }
                }
            }
        }

        /// <summary>
        ///     ��ȡ�ı�ֵ����
        /// </summary>
        /// <param name="element">�ڵ�</param>
        /// <param name="attribute">��������</param>
        /// <returns>�ı�ֵ</returns>
        private string GetAttribute(XElement element, string attribute)
        {
            XAttribute att = element.Attribute(attribute);
            return att == null ? null : att.Value;
        }

        /// <summary>
        ///     ��ȡ��������
        /// </summary>
        /// <param name="element">�ڵ�</param>
        /// <param name="attribute">��������</param>
        /// <param name="def">ȱʡ����</param>
        /// <returns>����</returns>
        private int GetAttribute(XElement element, string attribute, int def)
        {
            XAttribute att = element.Attribute(attribute);
            if (att == null)
            {
                return def;
            }
            int.TryParse(att.Value, out def);
            return def;
        }

        /// <summary>
        ///     ��ȡ����ֵ����
        /// </summary>
        /// <param name="element">�ڵ�</param>
        /// <param name="attribute">��������</param>
        /// <param name="def">ȱʡ����ֵ</param>
        /// <returns>����ֵ</returns>
        private bool GetAttribute(XElement element, string attribute, bool def)
        {
            XAttribute att = element.Attribute(attribute);
            if (att == null)
            {
                return def;
            }
            bool.TryParse(att.Value, out def);
            return def;
        }
    }
}