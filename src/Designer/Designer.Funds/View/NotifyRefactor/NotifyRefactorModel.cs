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
using Agebull.EntityModel.Common;

#endregion

namespace Agebull.EntityModel.Designer
{
    public sealed class NotifyRefactorModel : TraceModelBase
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

        public ConfigCollection<NotifyItem> NotifyItems { get; } = new ConfigCollection<NotifyItem>();

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
                RaisePropertyChanged(() => Project);
            }
        }
        #endregion

        #region 类型分析

        internal bool CheckNotifyPrepare(string arg)
        {
            return !string.IsNullOrWhiteSpace(Code);
        }


        public List<NotifyItem> DoCheckNotify(string arg)
        {
            return CheckNotify(arg);
        }

        internal void CheckNotifyEnd(CommandStatus status, Exception ex, List<NotifyItem> tables)
        {
            if (status == CommandStatus.Succeed)
            {
                NotifyItems.AddRange(tables);
            }

        }

        public void End()
        {
            CppProject.Instance.NotifyItems.Clear();
            CppProject.Instance.NotifyItems.AddRange(NotifyItems);
            NotifyItems.Clear();
        }

        #endregion


        #region 代码分析



        public List<NotifyItem> CheckNotify(string txt)
        {
            var result = new List<NotifyItem>();

            var lines = txt.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            List<NameValue> des = new List<NameValue>();
            List<string> api_line = new List<string>();
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;
                api_line.Add(line);
                var l = line.Trim();
                if (l.IndexOf("//", StringComparison.Ordinal) == 0)//注释
                {
                    l = l.Trim(NameHelper.NoneLanguageChar);
                    if (string.IsNullOrWhiteSpace(l))
                        continue;
                    var str = l.Split(NameHelper.EmptyChar, 2);
                    if (str.Length >= 2)
                    {
                        des.Add(new NameValue(str[0], str[1]));
                    }
                    else if (des.Count > 0)
                    {
                        des.Last().Value += l;
                    }
                    continue;
                }
                l = l.Trim(NameHelper.NoneNameChar);
                if (string.IsNullOrWhiteSpace(l))
                    continue;
                var item = CheckNotifyItem(l);
                if (des.Count > 1)
                {
                    var dess = des.FirstOrDefault(p => p.Name == "brief");
                    item.Caption = dess?.Value.Split(NameHelper.NoneNameChar, 2)[0];
                    item.Description = dess?.Value;
                }
                item.Org = api_line.LinkToString("\n");
                result.Add(item);
                api_line.Clear();
                des.Clear();
            }
            foreach (var it in result)
            {
                CoderBase.RepairConfigName(it, true);
            }
            return result;
        }

        private NotifyItem CheckNotifyItem(string line)
        {
            NotifyItem item = new NotifyItem();
            var strs = line.Split(new[] { '(' }, 2, StringSplitOptions.RemoveEmptyEntries);
            //检查API名称
            item.Name = strs[0].Split(NameHelper.EmptyChar, StringSplitOptions.RemoveEmptyEntries).Last();
            //参数检查(第一个)
            var words = strs[1].Split(NameHelper.NoneLanguageChar, StringSplitOptions.RemoveEmptyEntries);
            foreach (var word in words)
            {
                switch (word)
                {
                    case "virtual":
                    case "void":
                    case "__cdecl":
                    case "const":
                    case "int":
                    case "ESForeign":
                    case "TEsIsLastType":
                        continue;
                    case "islast":
                        item.IsMulit = true;
                        continue;
                    case "errCode":
                    case "iReqID":
                        item.IsCommandResult = true;
                        continue;
                }
                if (item.NotifyEntity == null)
                    item.NotifyEntity = word;
            }
            return item;
        }

        #endregion

    }

}