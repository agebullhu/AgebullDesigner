using System;
using System.IO;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Designer;

namespace Agebull.EntityModel.Config
{
    internal class FundsSolutionModel : ConfigModelBase
    {
        /// <summary>
        /// 解决方案
        /// </summary>
        public SolutionConfig Solution { get; set; }

        #region API

        public void ApiArgToClient()
        {
            var apiProject = Solution.Projects.FirstOrDefault(p => p?.Name == "CmdArg");
            if (apiProject == null)
            {
                Solution.Add(apiProject = new ProjectConfig
                {
                    Name = "CmdArg",
                    Caption = "命令调用参数"
                });
            }
            foreach (var item in Solution.ApiItems)
            {
                var friend = GetEntity(item.CallArg);
                if (friend == null)
                {
                    item.ResultArg = null;
                    continue;
                }
                var kw = friend.Caption.MulitReplace2("", "查询", "请求", "结构", "应答", "操作");
                var result = GetEntity(p => p != friend && p.Caption.Contains(kw));
                if (result != null)
                    item.ResultArg = result.Name;
                friend.Tag = Solution.Tag + "," + friend.Name;
                if (friend.Properties.Count == 0)
                {
                    item.ResultArg = null;
                    continue;
                }
                if (friend.Properties.Any(p => p.Name == "ClientNo"))
                {
                    item.IsUserCommand = true;
                }
                FindApiClientArg(friend, item, apiProject);
            }
            ConfigWriter writer = new ConfigWriter
            {
                Solution = Solution
            };
            writer.SaveProject(apiProject, Path.GetDirectoryName(Solution.SaveFileName));
        }

        private void FindApiClientArg(EntityConfig friend, ApiItem item, ProjectConfig apiProject)
        {
            var entity = GetEntity(p => p != friend && p.ReferenceKey == friend.Key);
            var count = friend.Properties.Count(p => p.Name != "ClientNo");
            switch (count)
            {
                case 0:
                    item.ResultArg = null;
                    if (entity != null)
                        entity.Option .IsDelete = true;
                    return;
                case 1:
                    item.ResultArg =
                        CppTypeHelper.CppTypeToCsType(friend.Properties.FirstOrDefault(p => p.Name != "ClientNo"));
                    if (entity != null)
                        entity.Option.IsDelete = true;
                    return;
            }
            if (entity == null)
            {
                entity = new EntityConfig
                {
                    Parent = apiProject,
                    Project = apiProject.Name,
                    CppName = item.CallArg,
                    Name = ToClientName(item.CallArg),
                    Tag = friend.Tag
                };
                apiProject.Add(entity);
            }
            entity.NoDataBase = true;
            entity.ReferenceKey = friend.Key;
            entity.Caption = item.Caption + "命令调用参数";
            entity.Description = item.Caption + "命令调用参数";
            item.ResultArg = entity.Name;

            foreach (var property in friend.Properties)
            {
                var fp = entity.Properties.FirstOrDefault(p => p.ReferenceKey ==  property.Key)
                         ?? entity.Properties.FirstOrDefault(p => p.Name == property.Name);
                if (fp == null)
                {
                    fp = new IFieldConfig();
                    fp.CopyFromProperty(property,false,true,false);
                    fp.Entity = entity;
                    entity.Add(fp);
                }
                fp.Entity = entity;
                fp.Option.ReferenceKey = property.Option.Key;
                fp.Caption = property.Caption;
                fp.Tag = entity.Tag + "," + property.Name;
            }
        }

        private string ToClientName(string esName)
        {
            if (esName.IndexOf("TEs", StringComparison.Ordinal) == 0)
                esName = esName.Substring(3);
            if (esName.IndexOf("TapAPI", StringComparison.Ordinal) == 0)
                esName = esName.Substring(6);
            if (esName.Length > 5 && esName.IndexOf("Field", StringComparison.Ordinal) == esName.Length - 5)
                esName = esName.Substring(0, esName.Length - 5);
            if (esName.Length > 3 && esName.IndexOf("Req", StringComparison.Ordinal) == esName.Length - 3)
                esName = esName.Substring(0, esName.Length - 3) + "Arg";
            else if (esName.Length > 3 && esName.IndexOf("Rsp", StringComparison.Ordinal) == esName.Length - 3)
                esName = esName.Substring(0, esName.Length - 3) + "Item";

            esName = esName.Replace("Qry", "Query");
            return esName;
        }

        #endregion

        #region Notify
        /// <summary>
        /// 通知参数处理
        /// </summary>
        public void NotifyArgToClient()
        {
            var apiProject = Solution.Projects.FirstOrDefault(p => p.Name == "NofityData");
            if (apiProject == null)
            {
                Solution.Add(apiProject = new ProjectConfig
                {
                    Name = "NofityData",
                    Caption = "消息通知数据"
                });
            }
            foreach (var item in CppProject.Instance.NotifyItems)
            {
                var friend = GetEntity(item.NotifyEntity);
                if (friend == null)
                {
                    item.ClientEntity = null;
                    continue;
                }
                if (item.IsCommandResult)
                {
                    FindNityfyApi(item, friend);
                }
                else
                {
                    item.CommandId = null;
                }
                friend.Tag = Solution.Tag + "," + friend.Name;
                if (friend.Properties.Count == 0)
                {
                    item.ClientEntity = null;
                    continue;
                }
                FindNitifyClientEntity(friend, apiProject, item);
            }

            ConfigWriter writer = new ConfigWriter
            {
                Solution = Solution,
            };
            writer.SaveProject(apiProject, Path.GetDirectoryName(Solution.SaveFileName));
        }

        private void FindNitifyClientEntity(EntityConfig friend, ProjectConfig apiProject, NotifyItem item)
        {
            var entity = GetEntity(p => p != friend && p.Tag == friend.Tag);
            if (entity == null)
            {
                entity = new EntityConfig
                {
                    Parent = apiProject,
                    Project = apiProject.Name,
                    CppName = friend.Name,
                    Name = ToClientName(friend.Name),
                    Caption = item.Caption,
                    Description = item.Caption + "(消息通知)",
                    NoDataBase = false,
                    Classify = friend.Classify,
                    ReferenceKey = friend.Key
                };
                apiProject.Add(entity);
            }
            item.ClientEntity = entity.Name;
            if (entity.PrimaryColumn == null)
            {
                entity.Add(new IFieldConfig
                {
                    Name = entity.Name + "Id",
                    Caption = entity.Caption + "ID",
                    Description = entity.Caption + "ID",
                    IsPrimaryKey = true,
                    IsIdentity = true,
                    CsType = "int",
                    CppType = "int",
                    Entity = entity
                });
            }
            foreach (var property in friend.Properties)
            {
                property.CsType = CppTypeHelper.CppTypeToCsType(property);
                property.Tag = friend.Tag + "," + property.Name;
                var fp = entity.Properties.FirstOrDefault(p => p.ReferenceKey == property.Key)
                         ?? entity.Properties.FirstOrDefault(p => p.Name == property.Name);
                if (fp == null)
                {
                    fp = new IFieldConfig();
                    fp.CopyFromProperty(property,false,true,true);
                    fp.Entity = entity;
                    entity.Add(fp);
                }
                fp.Entity = entity;
                fp.Tag = property.Tag;
                fp.ReferenceKey = property.Key;
                fp.Caption = property.Caption;
            }
        }

        private static void FindNityfyApi(NotifyItem item, EntityConfig friend)
        {
            var api = GetApi(p => p.Name == item.CommandId || p.ResultArg == item.NotifyEntity);
            if (api != null)
            {
                item.CommandId = api.Name;
                return;
            }
            var kw = item.Name.MulitReplace2("", "On", "Rsp");
            var kw1 = kw;
            api = GetApi(p => p.Name == kw1);
            if (api != null)
            {
                item.CommandId = api.Name;
                return;
            }
            kw = kw.MulitReplace2("", "Qry");

            var kw2 = kw;
            api = GetApi(p => p.Name == kw2);
            if (api != null)
            {
                item.CommandId = api.Name;
                return;
            }
            kw = "Qry" + kw;

            var kw3 = kw;
            api = GetApi(p => p.Name == kw3);

            if (api != null)
            {
                item.CommandId = api.Name;
                return;
            }
            kw = item.Caption.MulitReplace2("", "时", "请求", "结构", "应答", "操作");

            var kw4 = kw;
            api = GetApi(p => p.Caption == kw4);
            if (api != null)
            {
                item.CommandId = api.Name;
                return;
            }
            kw = friend.Caption.MulitReplace2("", "时", "请求", "结构", "应答", "操作");
            var result = GetEntity(p => p != friend && p.Caption != null && p.Caption.Contains(kw));
            if (result != null)
            {
                api = GetApi(p => p.CallArg == result.Name);
                item.CommandId = api?.Name;
            }
            item.CommandId = null;
        }

        #endregion

    }
}