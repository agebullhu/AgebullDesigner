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
using System.Linq;
using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;
using Agebull.EntityModel.RobotCoder;

#endregion

namespace Agebull.EntityModel.Designer
{
    public sealed class CppRefactorModel : TraceModelBase
    {
        #region 设计对象

        private string _code;

        /// <summary>
        ///     当前文件名
        /// </summary>
        public string Code
        {
            get => _code;
            set
            {
                if (_code == value)
                    return;
                _code = value;
                RaisePropertyChanged(() => Code);
            }
        }
        private string _systemName = "Es_Quote_90";

        /// <summary>
        ///     当前文件名
        /// </summary>
        public string SystemName
        {
            get => _systemName;
            set
            {
                if (_systemName == value)
                    return;
                _systemName = value;
                RaisePropertyChanged(() => SystemName);
            }
        }

        public ConfigCollection<TypedefItem> TypedefItems { get; } = new ConfigCollection<TypedefItem>();

        public ConfigCollection<EntityConfig> Entities { get; } = new ConfigCollection<EntityConfig>();
        private ProjectConfig _selectProjectConfig;
        /// <summary>
        ///     当前配置
        /// </summary>
        public ProjectConfig Project
        {
            get => _selectProjectConfig;
            set
            {
                if (_selectProjectConfig == value)
                {
                    return;
                }
                _selectProjectConfig = value;
                SystemName = value?.Option.ReferenceTag;
                RaisePropertyChanged(() => Project);
            }
        }
        #endregion


        #region 类型分析

        internal bool CheckTypedefPrepare(string arg)
        {
            return !string.IsNullOrWhiteSpace(Code);
        }


        public List<TypedefItem> DoCheckTypedef(string arg)
        {
            return CheckTypedef(arg);
        }

        internal void CheckTypedefEnd(CommandStatus status, Exception ex, List<TypedefItem> items)
        {
            if (status == CommandStatus.Succeed)
            {
                TypedefItems.AddRange(items);
            }
        }

        #endregion

        #region 类型分析

        internal bool CheckCppPrepare(string arg)
        {
            return !string.IsNullOrWhiteSpace(Code);
        }


        public List<EntityConfig> DoCheckCpp(string arg)
        {
            return CheckCppFieldes(arg);
        }

        internal void CheckCppEnd(CommandStatus status, Exception ex, List<EntityConfig> tables)
        {
            if (status == CommandStatus.Succeed)
            {
                Entities.AddRange(tables);
            }

        }

        #endregion


        #region 代码分析



        public List<TypedefItem> CheckTypedef(string txt)
        {
            var result = new List<TypedefItem>();

            var lines = txt.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            TypedefItem item = new TypedefItem();
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;
                var l = line.Trim();
                if (l.IndexOf("//", StringComparison.Ordinal) == 0)//注释
                {
                    item = new TypedefItem
                    {
                        Tag = SystemName,
                        Description = line.Trim(NameHelper.NoneLanguageChar)
                    };
                    item.Caption = item.Description;
                }
                else
                {
                    item = CheckTypedef(result, ref item, l);
                }
            }
            foreach (var it in result)
            {
                CoderBase.RepairConfigName(it, true);
                foreach (var pro in it.Items.Values)
                {
                    CoderBase.RepairConfigName(pro, true);
                }
            }
            return result;
        }

        private TypedefItem CheckTypedef(List<TypedefItem> result, ref TypedefItem item, string l)
        {
            var words = l.Split(new[] { '\t', ' ', ';', '=', '\\' }, StringSplitOptions.RemoveEmptyEntries);
            switch (words[0])
            {
                case "typedef":
                    if (item == null)
                    {
                        item = new TypedefItem
                        {
                            Tag = SystemName
                        };
                    }
                    var idx = l.IndexOf('[');
                    if (idx < 0)
                    {
                        item.Name = words[words.Length - 1];
                    }
                    else
                    {
                        var ks = l.Split(new[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
                        item.ArrayLen = ks[1];
                        words = ks[0].Split(new[] { '\t', ' ', ';', '=' }, StringSplitOptions.RemoveEmptyEntries);
                    }

                    item.Name = words[words.Length - 1];
                    item.KeyWork = string.Empty;
                    for (int index = 1; index < words.Length - 1; index++)
                    {
                        if (index > 1)
                            item.KeyWork += " ";
                        item.KeyWork += words[index];
                    }
                    result.Add(item);
                    item = null;
                    break;
                case "const":
                    var name = words[1];
                    var tp = result.FirstOrDefault(p => p.Name == name) ?? FindTypedef(name);
                    if (tp != null && !tp.Items.ContainsKey(words[2]))
                    {
                        tp.Items.Add(words[2], new EnumItem
                        {
                            ReferenceKey = item == null ? Guid.Empty : item.Key,
                            Name = words[2].Trim(NameHelper.NoneLanguageChar),
                            Value = words[3],
                            Caption = words.Length > 4 ? words[4].Trim(NameHelper.NoneLanguageChar) : item?.Description.Trim(NameHelper.NoneLanguageChar),
                            Description = words.Length > 4 ? words[4].Trim(NameHelper.NoneLanguageChar) : item?.Description.Trim(NameHelper.NoneLanguageChar)
                        });
                    }
                    item = null;
                    break;
            }

            return item;
        }

        public List<EntityConfig> CheckCppFieldes(string text)
        {
            List<EntityConfig> tables = new List<EntityConfig>();
            string[] lines = text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            int idx = 0;
            EntityConfig entity = new EntityConfig();
            bool isNewTable = true;
            bool isInStruct = false;
            foreach (string l in lines)
            {
                if (string.IsNullOrWhiteSpace(l))
                    continue;
                var line = l.Trim(' ', '\t', ';');
                if (string.IsNullOrWhiteSpace(line))
                    continue;
                switch (line)
                {
                    case "{":
                        isInStruct = true;
                        continue;
                    case "}":
                        isNewTable = true;
                        isInStruct = false;
                        entity = new EntityConfig();
                        continue;
                }
                if (line.IndexOf("//", StringComparison.Ordinal) == 0) //注释
                {
                    if (isNewTable || line.Contains("//!"))
                    {
                        entity.Caption = line.Trim(NameHelper.NoneLanguageChar);
                        isNewTable = false;
                    }
                    else
                    {
                        entity.Description = line.Trim(NameHelper.NoneLanguageChar);
                    }
                    continue;
                }
                string[] words = line.Split(new[] { ' ', ';', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                switch (words[0])
                {
                    case "typedef":
                        var item = new TypedefItem
                        {
                            Tag = SystemName
                        };
                        var i = l.IndexOf('[');
                        if (i < 0)
                        {
                            item.Name = words[words.Length - 1];
                        }
                        else
                        {
                            var ks = l.Split(new[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
                            item.ArrayLen = ks[1];
                            words = ks[0].Split(new[] { '\t', ' ', ';', '=' }, StringSplitOptions.RemoveEmptyEntries);
                        }
                        item.Description = entity.Caption;
                        item.Name = words[words.Length - 1].Trim(NameHelper.NoneLanguageChar);
                        item.KeyWork = string.Empty;
                        for (int index = 1; index < words.Length - 1; index++)
                        {
                            item.KeyWork += " " + words[index];
                        }
                        InvokeInUiThread(() => TypedefItems.Add(item));
                        continue;
                    case "struct":
                        entity.Name = words[1];
                        if (words.Length > 2)
                            entity.Caption = words[2].Trim(NameHelper.NoneNameChar);
                        tables.Add(entity);
                        isInStruct = false;
                        idx = 0;
                        continue;
                    case "private":
                    case "protected":
                    case "public":
                        continue;
                }
                if (!isInStruct)
                    continue;
                FieldConfig column;
                entity.Add(column = new FieldConfig
                {
                    DbType = "nvarchar"
                });
                column.Option.Index = idx++;
                words = line.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                if (words.Length > 1)
                {
                    column.Description = column.Caption = words[1].Trim('/', '\t', ' ');
                }

                words = words[0].Split(new[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
                if (words.Length > 1)
                {
                    column.Datalen = int.Parse(words[1].Trim('/', '\t', ' '));
                }
                words = words[0].Split(new[] { '\t', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                int nameIdx = words.Length - 1;
                column.Name = column.DbFieldName = words[nameIdx].Trim(NameHelper.NoneLanguageChar);
                column.CppType = "";
                for (int index = 0; index < nameIdx; index++)
                {
                    if (index > 0)
                        column.CppType += " " + words[index];
                    else
                        column.CppType = words[index];
                }
            }
            foreach (var t in tables)
            {
                t.Parent = Project;
                t.Option.ReferenceTag = SystemName + "," + t.Name;
                t.CppName = t.Name;
                CoderBase.RepairConfigName(t, true);
                foreach (var pro in t.Properties)
                {
                    pro.Entity = t;
                    pro.CppName = pro.CppName;
                    CoderBase.RepairConfigName(pro, true);
                    pro.CppLastType = CppTypeHelper2.CppLastType(pro.CppType);
                    pro.CsType = CppTypeHelper2.CppTypeToCsType(pro);
                }
                t.NoDataBase = true;

                //EntityBusinessModel business = new EntityBusinessModel
                //{
                //    Entity = t
                //};
                //t.IsReference = true;
                //business.RepairByModel(true);
                //t.IsReference = false;
            }
            return tables;
        }
        #endregion

        public void End()
        {
            if (Entities != null)
            {
                foreach (var item in Entities)
                {
                    var old = SolutionConfig.Current.Entities.FirstOrDefault(p => p.Name == item.Name);
                    if (old == null)
                    {
                        Project.Add(item);
                    }
                }
                Entities.Clear();
            }
            //if (TypedefItems != null)
            //{
            //    foreach (var item in TypedefItems)
            //    {
            //        var old = SolutionConfig.Current.TypedefItems.FirstOrDefault(p => p.Name == item.Name);
            //        if (old != null)
            //        {
            //            SolutionConfig.Current.TypedefItems.Remove(old);
            //        }
            //        Project.TypedefItems.Add(item);
            //        SolutionConfig.Current.TypedefItems.Add(item);
            //    }
            //    TypedefItems.Clear();
            //}
        }

        private EntityConfig FindEntity(string name)
        {
            return SolutionConfig.Current.Entities.FirstOrDefault(p => p.Name == name) ?? Entities.FirstOrDefault(p => p.Name == name);
        }

        private TypedefItem FindTypedef(string name)
        {
            return CppProject.Instance.TypedefItems.FirstOrDefault(p => p.Name == name) ?? TypedefItems.FirstOrDefault(p => p.Name == name);
        }
    }

}