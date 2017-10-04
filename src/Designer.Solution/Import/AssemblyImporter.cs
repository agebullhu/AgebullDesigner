// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 工程:CodeRefactor-Gboxt.Common.SimpleDataAccess
// 建立:2014-12-03
// 修改:2014-12-03
// *****************************************************/

#region 引用

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Agebull.Common.Defaults;
using Agebull.Common.Reflection;
using Gboxt.Common.DataAccess.Schemas;

#endregion

namespace Agebull.Common.SimpleDesign
{
    public sealed class AssemblyImporter
    {
        private Assembly _assembly;

        /// <summary>
        ///     分析出的表结构
        /// </summary>
        private SolutionConfig _project;

        /// <summary>
        ///     打开并分析文件
        /// </summary>
        /// <param name="file"></param>
        public static SolutionConfig Import(string file)
        {
            var importer = new AssemblyImporter
            {
                _assembly = Assembly.LoadFile(file),
                _project = new SolutionConfig
                {
                    Entities = new ObservableCollection<EntityConfig>()
                }
            };
            importer.ReadEntities();
            return importer._project;
        }

        /// <summary>
        ///     分析文件程序集
        /// </summary>
        /// <param name="assembly"></param>
        public static SolutionConfig Import(Assembly assembly)
        {
            var importer = new AssemblyImporter
            {
                _assembly = assembly,
                _project = new SolutionConfig
                {
                    Entities = new ObservableCollection<EntityConfig>()
                }
            };
            importer.ReadEntities();
            return importer._project;
        }
        /// <summary>
        ///     读取实体结构
        /// </summary>
        private void ReadEntities()
        {
            foreach (var type in _assembly.GetTypes())
            {
                if (type.IsGenericType || type.IsValueType || type.IsInterface || !type.IsPublic || type.GetAttribute<DataContractAttribute>() == null)
                    continue;

                ReadEntity(type);
            }
        }

        private void ReadEntity(Type type)
        {
            EntityConfig entity = new EntityConfig
            {
                Name = type.Name,
                ReadTableName = type.Name
            };
            foreach (var field in type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            {
                if (!IsField(field))
                {
                    continue;
                }
                if (entity.Properties.Any(p => string.Equals(field.Name, p.Caption, StringComparison.OrdinalIgnoreCase)))
                    continue;
                var pt = ReflectionHelper.GetTypeShowName(field.PropertyType.IsSubclassOf(typeof(Nullable<>)) ? field.PropertyType.GetGenericArguments()[0].Name : field.PropertyType.Name);
                var col = new PropertyConfig
                {
                    ColumnName = field.Name,
                    Name = field.Name,
                    CsType = pt,
                    Nullable = field.PropertyType.IsSubclassOf(typeof(Nullable<>)),
                    DbType = DataBaseHelper. ToDataBaseType(pt)
                };
                entity.Properties.Add(col);
                var ca = GetAttribute<DisplayNameAttribute>(field);
                if (ca != null)
                    col.Caption = ca.DisplayName;
                var ct = GetAttribute<CategoryAttribute>(field);
                if (ct != null)
                {
                    col.Description = ct.Category;
                }
                var de = GetAttribute<DescriptionAttribute>(field);
                if (de != null)
                {
                    col.Description = (ct != null) ? ct.Category + "," + de.Description : de.Description;
                }
                col.Index = entity.Properties.Count;
            }
            _project.Entities.Add(entity);
        }

        private TAttribute GetAttribute<TAttribute>(PropertyInfo prop) where TAttribute : Attribute
        {
            var arrs = prop.GetCustomAttributes(typeof(TAttribute), false);
            return arrs.Length == 0 ? null : (TAttribute)arrs[0];
        }

        private bool IsField(PropertyInfo property)
        {
            if (property.IsSpecialName)
            {
                return false;
            }
            var im = property.GetAttribute<DataMemberAttribute>();
            if (im == null && property.GetAttribute<IgnoreDataMemberAttribute>() == null)
            {
                return false;
            }
            var gm = property.GetGetMethod();
            if (gm == null || gm.IsPrivate)
                return false;
            var sm = property.GetSetMethod();
            if (sm == null || sm.IsPrivate)
                return false;
            return true;
        }
    }
}