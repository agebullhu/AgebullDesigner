// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 配置:CodeRefactor-Agebull.CodeRefactor.CodeAnalyze.Application
// 建立:2014-11-20
// 修改:2014-11-29
// *****************************************************/

#region 引用

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Agebull.EntityModel.RobotCoder.Upgrade;
using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;
using Microsoft.Win32;
using System.IO;
using System.Web.Http;
using Agebull.Common;

#endregion

namespace Agebull.EntityModel.Designer
{
    public sealed class ApiRefactorModel : TraceModelBase
    {
        #region 设计对象

        private string _code;

        /// <summary>
        ///     当前文件名
        /// </summary>
        public string Code
        {
            get { return _code; }
            set
            {
                if (_code == value)
                    return;
                _code = value;
                RaisePropertyChanged(() => Code);
            }
        }
        private string _systemName = "Es_Quote_30";

        /// <summary>m 
        ///     当前文件名
        /// </summary>
        public string SystemName
        {
            get { return _systemName; }
            set
            {
                if (_systemName == value)
                    return;
                _systemName = value;
                RaisePropertyChanged(() => SystemName);
            }
        }

        public ConfigCollection<ApiItem> ApiItems { get; } = new ConfigCollection<ApiItem>();

        private SolutionConfig _solution;
        /// <summary>
        ///     当前配置
        /// </summary>
        public SolutionConfig Solution
        {
            get { return _solution; }
            set
            {
                if (_solution == value)
                {
                    return;
                }
                _solution = value;
                RaisePropertyChanged(() => Solution);
            }
        }
        #endregion

        #region 类型分析

        internal bool CheckApiPrepare(string arg, Action<string> setArg)
        {
            var of = new OpenFileDialog();
            if (of.ShowDialog() != true)
                return false;
            setArg(of.FileName);
            return true;
        }


        public List<ApiItem> DoCheckApi(string arg)
        {
            return CheckApi(arg);
        }

        internal void CheckApiEnd(CommandStatus status, Exception ex, List<ApiItem> tables)
        {
            if (status == CommandStatus.Succeed)
            {
                ApiItems.AddRange(tables);
            }

        }

        #endregion


        #region 代码分析
        /// <summary>
        /// 读取的帮助XML
        /// </summary>
        private List<XmlMember> HelpXml;

        bool IsSub(Type type, string baseName)
        {
            if (type == typeof(object))
                return false;
            if (type.FullName == baseName)
                return true;
            return IsSub(type.BaseType, baseName);
        }


        private static List<string> loaded = new List<string>();
        Assembly LoadAssembly(string path, string name)
        {
            var file = Path.Combine(path, name);
            if (loaded.Contains(file))
                return null;
            loaded.Add(file);
            string path2 = file.Replace("dll", "xml");
            if (File.Exists(path2))
                HelpXml.AddRange(XmlMember.LoadHelpXml(path2));
            var p2 = Path.GetDirectoryName(GetType().Assembly.Location);
            var asm = Assembly.LoadFile(file);
            IOHelper.CopyPath(path, p2, "*.dll", false, false);
            System.Diagnostics.Trace.WriteLine(Path.Combine(path, name));
            foreach (var friend in asm.GetReferencedAssemblies())
            {
                file = Path.Combine(path, friend.Name + ".dll");
                if (File.Exists(file))
                    LoadAssembly(path, friend.Name + ".dll");
            }
            return asm;
        }
        public List<ApiItem> CheckApi(string txt)
        {
            HelpXml = new List<XmlMember>();
            var controlers = new List<Type>();
            var asm = LoadAssembly(Path.GetDirectoryName(txt), Path.GetFileName(txt)) ?? Assembly.LoadFile(txt);


            foreach (var type in asm.GetTypes())
            {
                if (type.IsSubclassOf(typeof(ApiController)))
                {
                    controlers.Add(type);
                }
            }
            var items = new List<ApiItem>();
            foreach (var controler in controlers)
            {
                CheckApiItem(controler, items);
            }
            return items;
        }

        private void GetInfo(ConfigBase config, Type type)
        {
            var member = HelpXml.FirstOrDefault(p => p.Name == type.FullName);
            if (member != null)
            {
                config.Description = member.Summary;
                config.Caption = member.DisplayName;
                config.Description = member.Remark ?? member.Summary;
            }
        }
        private void GetInfo(ConfigBase config, Type type, MethodInfo field)
        {
            var member = HelpXml.FirstOrDefault(p => p.Name == type.FullName + "." + field.Name);
            if (member != null)
            {
                config.Description = member.Summary;
                config.Caption = member.DisplayName;
                config.Description = member.Remark ?? member.Summary;
            }
        }
        private void GetInfo(ConfigBase config, Type type, FieldInfo field)
        {
            var member = HelpXml.FirstOrDefault(p => p.Name == type.FullName + "." + field.Name);
            if (member != null)
            {
                config.Description = member.Summary;
                config.Caption = member.DisplayName;
                config.Description = member.Remark ?? member.Summary;
            }
            var descript = field.GetCustomAttribute<DescriptionAttribute>();
            if (descript != null)
                config.Description = descript.Description;
            var displayName = field.GetCustomAttribute<DisplayNameAttribute>();
            if (displayName != null)
                config.Caption = displayName.DisplayName;
        }

        private void GetInfo(ConfigBase config, Type type, PropertyInfo property)
        {
            var member = HelpXml.FirstOrDefault(p => p.Name == type.FullName + "." + property.Name);
            if (member != null)
            {
                config.Description = member.Summary;
                config.Caption = member.DisplayName;
                config.Description = member.Remark ?? member.Summary;
            }
            var descript = property.GetCustomAttribute<DescriptionAttribute>();
            if (descript != null)
                config.Description = descript.Description;
            var displayName = property.GetCustomAttribute<DisplayNameAttribute>();
            if (displayName != null)
                config.Caption = displayName.DisplayName;
        }
        private void CheckApiItem(Type controler, List<ApiItem> items)
        {
            AssemblyUpgrader upgrader = new AssemblyUpgrader
            {
                HelpXml = HelpXml
            };

            var project = new ProjectConfig
            {
                Name = controler.Name
            };
            GetInfo(project, controler);
            Solution.Projects.Add(project);
            var ctors = controler.GetConstructors();
            Dictionary<string, ApiItem> interfaces = new Dictionary<string, ApiItem>(StringComparer.OrdinalIgnoreCase);
            foreach (var method in ctors)
            {
                var p = method.GetParameters();
                if (p.Length != 1)
                    continue;
                interfaces = CheckInterface(p[0].ParameterType, project);
            }
            List<ApiItem> noItems = new List<ApiItem>();
            foreach (var method in controler.GetMethods())
            {
                var route = method.GetCustomAttribute<RouteAttribute>();
                if (route == null)
                    continue;
                var item = new ApiItem
                {
                    Name = method.Name,
                    Org = route.Template,
                    Project = project.Name
                };
                items.Add(item);
                project.ApiItems.Add(item);
                GetInfo(item, controler, method);
                var post = method.GetCustomAttribute<HttpPostAttribute>();
                if (post != null)
                {
                    item.Method = HttpMethod.POST;
                }
                else
                {
                    var getm = method.GetCustomAttribute<HttpGetAttribute>();
                    if (getm == null)
                    {
                        continue;
                    }
                }
                foreach (var para in method.GetParameters())
                {
                    var up = upgrader.GetConfig(para.ParameterType);
                    var entity = new EntityConfig
                    {
                        Name = up.Name,
                        Caption = up.Caption,
                        Description = up.Description,
                        IsClass = true
                    };
                    foreach (var pro in up.Properties.Values)
                    {
                        entity.Properties.Add(new PropertyConfig
                        {
                            Name = pro.Name,
                            Caption = pro.Caption,
                            Description = pro.Description,
                            CsType = pro.TypeName,
                            Parent = entity
                        });
                    }
                    Solution.Entities.Add(entity);
                    item.CallArg = entity.Name;
                    break;
                }
                if (interfaces.ContainsKey(method.Name))
                {
                    var m2 = interfaces[method.Name];
                    interfaces.Remove(method.Name);
                    item.ResultArg = m2.ResultArg;
                    continue;
                }
                noItems.Add(item);
                
                /*if (method.ReturnType == typeof(void))
                    continue;
                {
                    var up = upgrader.GetConfig(method.ReturnType);
                    var entity = new EntityConfig
                    {
                        Name = up.Name,
                        Caption = up.Caption,
                        Description = up.Description,
                        IsClass = true
                    };
                    foreach (var pro in up.Properties.Values)
                    {
                        entity.Properties.Add(new PropertyConfig
                        {
                            Name = pro.Name,
                            Caption = pro.Caption,
                            Description = pro.Description,
                            CsType = pro.TypeName
                        });
                    }
                    Solution.Entities.Add(entity);
                    item.ResultArg = entity.Name;
                }*/
            }
            if (noItems.Count == 1 && interfaces.Count == 1)
                noItems[0].ResultArg = interfaces.Values.First().ResultArg;
        }

        private Dictionary<string, ApiItem> CheckInterface(Type controler,ProjectConfig project)
        {
            Dictionary<string, ApiItem> items = new Dictionary<string, ApiItem>();
            AssemblyUpgrader upgrader = new AssemblyUpgrader
            {
                HelpXml = HelpXml
            };
            foreach (var method in controler.GetMethods())
            {
                var item = new ApiItem
                {
                    Name = method.Name
                };
                items.Add(method.Name, item);
                GetInfo(item, controler, method);
                foreach (var para in method.GetParameters())
                {
                    var up = upgrader.GetConfig(para.ParameterType);
                    var entity = new EntityConfig
                    {
                        Name = up.Name,
                        Caption = up.Caption,
                        Description = up.Description,
                        IsClass = true
                    };
                    foreach (var pro in up.Properties.Values)
                    {
                        entity.Properties.Add(new PropertyConfig
                        {
                            Name = pro.Name,
                            Caption = pro.Caption,
                            Description = pro.Description,
                            CsType = pro.TypeName,
                            Parent = entity
                        });
                    }
                    Solution.Entities.Add(entity);
                    item.CallArg = entity.Name;
                    break;
                }
                if (method.ReturnType == typeof(void))
                    continue;
                {
                    var up = upgrader.GetConfig(method.ReturnType);
                    var entity = new EntityConfig
                    {
                        Name = up.Name,
                        Caption = up.Caption,
                        Description = up.Description,
                        IsClass = true,
                        Parent = project
                    };
                    project.Entities.Add(entity);
                    item.ResultArg = entity.Name;
                    foreach (var pro in up.Properties.Values)
                    {
                        entity.Properties.Add(new PropertyConfig
                        {
                            Name = pro.Name,
                            Caption = pro.Caption,
                            Description = pro.Description,
                            CsType = pro.TypeName,
                            Parent = entity 
                        });
                    }
                }
            }
            return items;
        }
        #endregion

        public void End()
        {
            foreach (var item in ApiItems)
            {
                var old = Solution.Entities.FirstOrDefault(p => p.Name == item.Name);
                if (old == null)
                {
                    Solution.ApiItems.Add(item);
                }
            }
            ApiItems.Clear();
        }

    }

}