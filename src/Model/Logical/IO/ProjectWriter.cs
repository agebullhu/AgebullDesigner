﻿using Agebull.Common;
using Agebull.EntityModel.Config;
using System.IO;
using System.Linq;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 配置读写器
    /// </summary>
    public class ProjectWriter : ConfigWriter
    {
        /// <summary>
        /// 保存解决方案
        /// </summary>
        public static string SaveProject(ProjectConfig project, string directory, bool checkState = true)
        {
            var dir = GlobalConfig.CheckPath(directory, project.Name);
            foreach (var cls in project.Classifies.ToArray())
            {
                foreach (var ch in cls.Items)
                {
                    ch.Classify = cls.IsDelete ? null : cls.Name;
                }

                if (cls.IsDelete)
                    project.Classifies.Remove(cls);
            }
            SavEntities(project, dir, checkState);
            SavModels(project, dir, checkState);
            SaveEnums(project, dir, checkState);
            SaveApies(project, dir, checkState);
            if (project.IsDelete)
            {
                SolutionConfig.Current.ProjectList.Remove(project);
            }
            SaveConfig(Path.Combine(dir, "project.json"), project, checkState);
            return dir;
        }

        static void SavEntities(ProjectConfig project, string dir, bool checkState)
        {
            var path = GlobalConfig.CheckPath(dir, "Entity");
            foreach (var entity in project.Entities.ToArray())
            {
                SaveEntity(entity, path, checkState);
            }
        }

        static void SavModels(ProjectConfig project, string dir, bool checkState)
        {
            var path = GlobalConfig.CheckPath(dir, "Model");
            foreach (var model in project.Models.ToArray())
            {
                SaveModel(model, path, checkState);
            }
        }

        static void SaveEnums(ProjectConfig project, string dir, bool checkState)
        {
            var path = GlobalConfig.CheckPath(dir, "Enum");
            foreach (var type in project.Enums.ToArray())
            {
                if (type.IsDelete)
                {
                    project.Remove(type);
                }
                else
                {
                    //删除字段处理
                    foreach (var field in type.Items.Where(p => p.IsDelete).ToArray())
                    {
                        type.Remove(field);
                    }
                }

                SaveConfig(type, path, checkState);
            }
        }


        /// <summary>
        /// 保存API对象
        /// </summary>
        static void SaveApies(ProjectConfig project, string dir, bool checkState)
        {
            var path = GlobalConfig.CheckPath(dir, "Api");
            foreach (var api in project.ApiItems.ToArray())
            {
                if (api.IsDelete)
                {
                    project.Remove(api);
                }
                SaveConfig(api, path, checkState);
            }
        }

        /// <summary>
        /// 保存实体
        /// </summary>
        public static void SaveEntity(EntityConfig entity, string dir, bool checkState)
        {
            if (entity.IsDelete)
            {
                entity.Project.Remove(entity);
            }
            else
            {
                //删除字段处理
                foreach (var field in entity.Where(p => p.IsDelete).ToArray())
                {
                    entity.Remove(field);
                }
            }
            SaveModelExtend(entity, dir, checkState);
            SaveConfig(entity, dir, checkState);
        }

        /// <summary>
        /// 保存实体
        /// </summary>
        public static void SaveModel(ModelConfig model, string dir, bool checkState)
        {
            if (model.IsDelete)
            {
                model.Project.Remove(model);
            }
            else
            {
                //删除字段处理
                foreach (var property in model.Where(p => p.IsDelete).ToArray())
                {
                    model.Remove(property);
                }

                //删除命令处理
                foreach (var field in model.Commands.Where(p => p.IsDelete).ToArray())
                {
                    model.Remove(field);
                }
            }

            SaveModelExtend(model, dir, checkState);
            SaveConfig(model, dir, checkState);
        }


        /// <summary>
        /// 保存实体
        /// </summary>
        static void SaveModelExtend(IEntityConfig entity, string dir, bool checkState)
        {
            var path = IOHelper.CheckPath(Path.GetDirectoryName(dir), "DataBase");
            //if (entity.Page != null)
            //    SaveConfig(entity.Page, path, checkState);

            // ReSharper disable once AssignNullToNotNullAttribute
            if (entity.DataTable != null)
                SaveConfig(entity.DataTable, path, checkState);
        }
    }
}