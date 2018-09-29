using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// 解决方案的基础逻辑
    /// </summary>
    public class SolutionModel : ConfigModelBase
    {
        /// <summary>
        /// 解决方案
        /// </summary>
        public SolutionConfig Solution { get; set; }

        #region 基础修复

        /// <summary>
        ///     重置状态
        /// </summary>
        public void ResetStatus()
        {
        }

        #endregion


        #region 初始处理
        /// <summary>
        /// 载入的基础修复
        /// </summary>
        public void RepairByLoaded()
        {
            
            int projectid = Solution.ProjectList.Count == 0 ? 0 : Solution.Projects.Max(p => p.Index);
            foreach (var project in Solution.Projects)
            {
                if (project.Option.Index == 0)
                    project.Option.Index = ++projectid;
                if (project.Name.Contains("ES_"))
                    project.ReadOnly = true;
                int typeid = project.Entities.Count == 0 ? 0 : project.Entities.Max(p => p.Index);
                foreach (var entity in project.Entities)
                {
                    RepairIdentity(entity, project, ref typeid);
                }
                foreach (var entity in project.Entities.Where(p => !p.IsReference))
                {
                    RepairEntityByLoad(entity, project);
                }
                foreach (var cfg in project.ApiItems)
                {
                    cfg.Parent = project;
                }
                foreach (var cfg in project.Enums)
                {
                    cfg.Parent = project;
                }
                if (project.Classifies.Count == 0)
                {
                    var cls = project.Entities.Select(p => p.Classify).Distinct().ToArray();
                    if (!cls.All(string.IsNullOrEmpty))
                    {
                        foreach (var cl in project.Entities.GroupBy(p => p.Classify))
                        {
                            var name = cl.Key ?? "None";
                            var item = new ClassifyItem<EntityConfig>
                            {
                                Classify = name,
                                Description = name,
                                Caption = name
                            };
                            project.Classifies.Add(item);
                            foreach (var entity in cl)
                            {
                                entity.Classify = name;
                                item.Items.Add(entity);
                            }
                        }
                    }
                }

                //int apiid = project.ApiItems.Count == 0 ? 0 : project.ApiItems.Max(p => p.Index);
                //foreach (var item in project.ApiItems)
                //{
                //    if (item.Index <= 0)
                //        item.Index = ++apiid;
                //}
            }
            RepairTypedefByLoad();
        }

        private void RepairEntityByLoad(EntityConfig entity, ProjectConfig project)
        {
            foreach (var key in keys)
            {
                if (entity.Caption == null)
                    entity.Caption = entity.Name;
                if (entity.Caption == null || entity.Caption.Length < key.Length)
                    continue;
                var last = entity.Caption.Substring(entity.Caption.Length - key.Length, key.Length);
                if (last == key)
                {
                    entity.Caption = entity.Caption.Substring(0, entity.Caption.Length - key.Length) + "数据";
                    break;
                }
            }
            if (entity.Tag == null)
                entity.Tag = project.Tag + "," + entity.Name;

            if (entity.Properties.Count == 0)
                return;
            var array = entity.Properties.OrderBy(p => p.Index).ToArray();

            entity.Properties.Clear();
            foreach (var field in array)
            {
                if (field.Name == null || field.Name == "NewField")
                    continue;
                foreach (var key in repairs)
                {
                    if (field.Caption == null || field.Caption.Length < key.Key.Length)
                        continue;

                    var last = field.Caption.Substring(field.Caption.Length - key.Key.Length, key.Key.Length);
                    if (last == key.Key)
                    {
                        field.Caption = field.Caption.Substring(0, field.Caption.Length - key.Key.Length) + key.Value;
                        break;
                    }
                    if (field.Description == null || field.Description.Length < key.Key.Length)
                        continue;
                    last = field.Description.Substring(field.Description.Length - key.Key.Length, key.Key.Length);
                    if (last == key.Key)
                    {
                        field.Description = field.Description.Substring(0, field.Description.Length - key.Key.Length) + key.Value;
                        break;
                    }
                }
                entity.Add(field);
                if (field["user_help"] == null)
                    field["user_help"] = field.Description ?? field.Caption;
                //if (field.Tag == null)
                //    field.Tag = entity.Tag + "," + field.Name;
                //if (field.LinkTable != null && field.LinkField == null)
                //    field.LinkField = field.Name;

                field.EnumConfig = Solution.Enums.FirstOrDefault(p => p.Name == field.CustomType);

            }
            foreach (var cmd in entity.Commands)
            {
                cmd.Parent = entity;
            }
        }
        private void RepairIdentity(EntityConfig entity, ProjectConfig project, ref int typeid)
        {
            if (entity.Option.Index == 0)
                entity.Option.Index = (project.Option.Index << 16) | ++typeid;
            if (entity.Option.Identity <= 0)
            {
                entity.Option.Identity = entity.Option.Index;
            }

            if (entity.Properties.Count == 0)
                return;
            var idx = entity.Properties.Max(p => p.Index) + 1;

            if (idx == 1 && entity.PrimaryColumn != null)
                idx = 2;
            entity.MaxIdentity = entity.Properties.Max(p => p.Identity);

            foreach (var field in entity.Properties)
            {
                if (field.IsPrimaryKey)
                    field.Option.Index = 1;
                else if (field.Index <= 0)
                    field.Option.Index = ++idx;
                if (field.Identity <= 0)
                {
                    field.Option.Identity = ++entity.MaxIdentity;
                }
            }
        }

        private readonly string[] keys = {
            "查询应答",
            "推送通知" ,
            "通知"
        };

        private readonly Dictionary<string, string> repairs = new Dictionary<string, string>
        {
            {"没有","编号"},
            {"无","编号"}
        };

        private void RepairTypedefByLoad()
        {
            //var typedefItems = new List<TypedefItem>();
            //foreach (var typedef in Solution.TypedefItems.GroupBy(p => p.Name).ToArray())
            //{
            //    var array = typedef.ToArray();
            //    if (array.Length <= 1)
            //    {
            //        typedefItems.Add(array[0]);
            //        continue;
            //    }
            //    typedefItems.Add(array.FirstOrDefault(p => p.Items.Count > 0) ?? array[array.Length - 1]);
            //}
            //foreach (var typedef in typedefItems)
            //{
            //    if (typedef.Tag == null)
            //        typedef.Tag = "ES3.0";
            //    typedef.Description = typedef.Description?.Trim(' ', '\t', '\\', '/', '*', '.', ',', ';', '，', '；', '。',
            //        '．',
            //        '!', '！');
            //    if (typedef.Caption == null)
            //        typedef.Caption = typedef.Description;
            //    foreach (var con in typedef.Items.Values)
            //        con.Name = con.Name?.Trim(' ', '\t', '\\', '/', '*', '.', ',', ';', '，', '；', '。', '．', '!', '！');
            //}
            //Solution.TypedefItems.Clear();
            //Solution.TypedefItems.AddRange(typedefItems);

            //foreach (var en in Solution.TypedefItems.Where(p => !p.IsReference))
            //{
            //    var items = en.Items.Values.Where(p => !p.IsDelete).ToArray();
            //    en.Items.Clear();
            //    foreach (var item in items)
            //    {
            //        en.Items.Add(item.Name, item);
            //    }
            //}
            foreach (var en in Solution.Enums.Where(p => !p.IsReference))
            {
                var items = en.Items.Where(p => !p.IsDelete).OrderBy(p => p.Value).ToArray();
                en.Items.Clear();
                foreach (var item in items)
                {
                    var old = en.Items.FirstOrDefault(p => p.Name == item.Name);
                    if (old != null)
                        en.Remove(old);
                    en.Add(item);
                }
            }
            //var group = Solution.TypedefItems.GroupBy(p => p.Tag);
            //foreach (var g in group.ToArray())
            //{
            //    var project = Solution.Projects.FirstOrDefault(p => p.Tag == g.Key);
            //    if (project != null)
            //    {
            //        foreach (var item in g)
            //        {
            //            project.TypedefItems.Add(item);
            //        }
            //    }
            //}
        }
        #endregion


        #region 全局对象同步

        /// <summary>
        /// 重置全局对象
        /// </summary>
        public static void ResetGloblaCollection()
        {
            Entities.Clear();
            Projects.Clear();
            Enums.Clear();
            ApiItems.Clear();
            foreach (var solution in Solutions)
            {
                var model = new SolutionModel
                {
                    Solution = solution
                };
                model.OnSolutionLoad();
            }
        }
        /// <summary>
        /// 载入处理
        /// </summary>
        public void OnSolutionLoad()
        {
            if (!Solutions.Contains(Solution))
                Solutions.Add(Solution);
            TryAdd(Entities, Solution.Entities);
            TryAdd(Projects, Solution.Projects);
            TryAdd(Enums, Solution.Enums);
            TryAdd(ApiItems, Solution.ApiItems);

            Solution.EntityList.CollectionChanged += (s, e) => CollectionChanged(Entities, e);
            Solution.ProjectList.CollectionChanged += (s, e) => CollectionChanged(Projects, e);
            Solution.EnumList.CollectionChanged += (s, e) => CollectionChanged(Projects, e);
            Solution.ApiList.CollectionChanged += (s, e) => CollectionChanged(Projects, e);
        }
        private static void CollectionChanged<TConfig>(NotificationList<TConfig> collection, NotifyCollectionChangedEventArgs e)
            where TConfig : ConfigBase
        {
            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                ResetGloblaCollection();
                return;
            }
            if (e.OldItems != null)
                foreach (var item in e.OldItems.OfType<TConfig>())
                    collection.Remove(item);
            if (e.NewItems != null)
                collection.AddRange(e.NewItems.OfType<TConfig>());
        }

        #endregion

        #region 信息
        /// <summary>
        /// 检查项目路径
        /// </summary>
        /// <param name="old"></param>
        /// <param name="path"></param>
        public void CheckProjectPath(string old, string path)
        {
            if (string.IsNullOrEmpty(old) || string.IsNullOrEmpty(path))
                return;
            foreach (var project in Solution.Projects)
            {
                if (project.BusinessPath != null)
                    project.BusinessPath = project.BusinessPath.Replace(old, path);
                if (project.MobileCsPath != null)
                    project.MobileCsPath = project.MobileCsPath.Replace(old, path);
                else
                    project.MobileCsPath = path.Trim('\\') + @"\TradeApp\TradeApp\Model\" + project.Name;
                if (project.CppCodePath != null)
                    project.CppCodePath = project.CppCodePath.Replace(old, path);
            }
        }
        #endregion
    }
}