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
using Agebull.Common.DataModel;
using Agebull.Common.SimpleDesign;
using Gboxt.Common.DataAccess.Schemas;
using Gboxt.Common.WpfMvvmBase.Commands;

#endregion

namespace Agebull.CodeRefactor.SolutionManager
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
            get { return _code; }
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
            get { return _systemName; }
            set
            {
                if (_systemName == value)
                    return;
                _systemName = value;
                RaisePropertyChanged(() => SystemName);
            }
        }

        public ConfigCollection<NotifyItem> NotifyItems { get; } = new ConfigCollection<NotifyItem>();

        private SolutionConfig _selectProjectConfig;
        /// <summary>
        ///     当前配置
        /// </summary>
        public SolutionConfig Solution
        {
            get { return _selectProjectConfig; }
            set
            {
                if (_selectProjectConfig == value)
                {
                    return;
                }
                _selectProjectConfig = value;
                RaisePropertyChanged(() => Solution);
            }
        }
        #endregion

        #region 类型分析

        internal bool CheckNotifyPrepare(string arg, Action<string> setArg)
        {
            setArg(Code);
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
            Solution.NotifyItems.Clear();
            Solution.NotifyItems.AddRange(NotifyItems);
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
                    l = l.Trim(CoderBase.NoneLanguageChar);
                    if (string.IsNullOrWhiteSpace(l))
                        continue;
                    var str = l.Split(CoderBase.EmptyChar, 2);
                    if (str.Length >= 2)
                    {
                        des.Add(new NameValue(str[0], str[1]));
                    }
                    else if (des.Count > 0)
                    {
                        des.Last().value += l;
                    }
                    continue;
                }
                l = l.Trim(CoderBase.NoneNameChar);
                if (string.IsNullOrWhiteSpace(l))
                    continue;
                var item = CheckNotifyItem(l);
                if (des.Count > 1)
                {
                    var dess = des.FirstOrDefault(p => p.name == "brief");
                    item.Caption = dess?.value.Split(CoderBase.NoneNameChar, 2)[0];
                    item.Description = dess?.value;
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
            item.Name = strs[0].Split(CoderBase.EmptyChar, StringSplitOptions.RemoveEmptyEntries).Last();
            //参数检查(第一个)
            var words = strs[1].Split(CoderBase.NoneLanguageChar, StringSplitOptions.RemoveEmptyEntries);
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