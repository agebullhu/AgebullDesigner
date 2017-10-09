using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    ///     设计器上下文
    /// </summary>
    public class DesignContext : TraceModelBase, IGridSelectionBinding
    {
        #region 继承

        /// <summary>
        ///     初始化
        /// </summary>
        protected override void DoInitialize()
        {
            base.DoInitialize();
            LoadJob();
        }

        #endregion

        #region 设计对象

        /// <summary>
        ///     当前文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        ///     当前解决方案
        /// </summary>
        private SolutionConfig _solution;

        /// <summary>
        ///     当前解决方案
        /// </summary>
        public SolutionConfig Solution
        {
            get
            {
                if (_solution != null)
                    return _solution;
                _solution = new SolutionConfig();
                ConfigModelBase.SetCurrentSolution(_solution);
                return _solution;
            }
            set
            {
                if (Equals(_solution, value))
                    return;
                _solution = value;
                ConfigModelBase.SetCurrentSolution(_solution);
                RaisePropertyChanged(() => Solution);
                RaisePropertyChanged(() => Entities);
                RaisePropertyChanged(() => FileName);
            }
        }

        /// <summary>
        ///     当前解决方案的实体
        /// </summary>
        public ObservableCollection<EntityConfig> Entities => Solution.Entities;

        #endregion

        #region 状态

        private string _stateMessage;

        /// <summary>
        ///     状态消息
        /// </summary>
        public string StateMessage
        {
            get { return _stateMessage; }
            set
            {
                _stateMessage = value;
                if (CurrentTrace != null)
                    CurrentTrace.TraceMessage.Track = value;
                RaisePropertyChanged(() => StateMessage);
            }
        }

        #endregion

        #region 选中对象

        private IList _selectColumns2;

        /// <summary>
        ///     当前选择
        /// </summary>
        public IList SelectConfigs
        {
            get { return _selectColumns2; }
            set
            {
                if (Equals(_selectColumns2, value))
                {
                    if (value != null && value.Count > 0)
                    {
                        var array = new object[value.Count];
                        var idx = 0;
                        foreach (var item in value)
                            array[idx++] = item;
                        PropertyGrid.SelectedObjects = array;
                    }
                    return;
                }
                _selectColumns2 = value;
                RaisePropertyChanged(() => SelectConfigs);
            }
        }

        private IList _selectColumns;

        /// <summary>
        ///     当前选择
        /// </summary>
        public IList SelectColumns
        {
            get { return _selectColumns; }
            set
            {
                if (value != null && value.Count > 0)
                {
                    var array = new object[value.Count];
                    var idx = 0;
                    foreach (var item in value)
                    {
                        if (item is PropertyConfig)
                            SelectProperty = item as PropertyConfig;
                        array[idx++] = item;
                    }
                    PropertyGrid.SelectedObjects = array;
                }

                if (Equals(_selectColumns, value))
                    return;
                _selectColumns = value;
                RaisePropertyChanged(() => SelectColumns);
            }
        }

        private TableReleation _selectReleation;
        private EntityConfig _selectEntityConfig;
        private ProjectConfig _selectProjectConfig;
        private PropertyConfig _selectPropertyConfig;
        private IEnumerable _selectItemChildrens;

        public ConfigBase PreSelectConfig;

        /// <summary>
        ///     当前配置
        /// </summary>
        public ConfigBase SelectConfig
        {
            get { return GlobalConfig.CurrentConfig; }
            set
            {
                if (value == null)
                {
                    SelectProject = null;
                    SelectEntity = null;
                    SelectProperty = null;
                    SelecReleation = null;
                }
                else
                {
                    SelectProject = value as ProjectConfig;
                    SelectProperty = value as PropertyConfig;
                    SelectEntity = value as EntityConfig;
                    SelecReleation = value as TableReleation;
                }
                StateMessage = $"当前对象:{value?.Name}=>{value?.Caption}=>{value?.Description}=>({value?.GetType()})";

                if (GlobalConfig.CurrentConfig == value)
                    return;
                PreSelectConfig = GlobalConfig.CurrentConfig;
                GlobalConfig.CurrentConfig = value;
                RaisePropertyChanged(nameof(SelectConfig));
            }
        }

        /// <summary>
        ///     当前配置
        /// </summary>
        public TableReleation SelecReleation
        {
            get { return _selectReleation; }
            set
            {
                if (_selectReleation == value)
                    return;
                _selectReleation = value;
                RaisePropertyChanged(() => SelecReleation);
            }
        }

        /// <summary>
        ///     当前配置
        /// </summary>
        public ProjectConfig SelectProject
        {
            get { return _selectProjectConfig; }
            set
            {
                if (_selectProjectConfig == value)
                    return;
                _selectProjectConfig = value;
                RaisePropertyChanged(() => SelectProject);
            }
        }

        /// <summary>
        ///     当前配置
        /// </summary>
        public EntityConfig SelectEntity
        {
            get { return _selectEntityConfig; }
            set
            {
                if (_selectEntityConfig == value)
                    return;
                _selectEntityConfig = value;
                if (value != null)
                    SelectProject = value.Parent;

                RaisePropertyChanged(() => SelectEntity);

                RaisePropertyChanged(() => RelationVisibility);
            }
        }

        /// <summary>
        ///     当前配置
        /// </summary>
        public PropertyConfig SelectProperty
        {
            get { return _selectPropertyConfig; }
            set
            {
                if (_selectPropertyConfig == value)
                    return;
                if (value != null)
                    SelectEntity = value.Parent;
                _selectPropertyConfig = value;
                RaisePropertyChanged(() => SelectProperty);
                RaisePropertyChanged(() => RelationVisibility);
            }
        }


        /// <summary>
        ///     当前选择
        /// </summary>
        public IEnumerable SelectItemChildrens
        {
            get { return _selectItemChildrens; }
            set
            {
                if (Equals(_selectItemChildrens, value))
                    return;
                _selectItemChildrens = value;
                RaisePropertyChanged(() => SelectItemChildrens);
            }
        }

        private IEnumerable _selectChildrens;

        /// <summary>
        ///     当前选择
        /// </summary>
        public IEnumerable SelectChildrens
        {
            get { return _selectChildrens; }
            set
            {
                if (Equals(_selectChildrens, value))
                    return;
                _selectChildrens = value;
                RaisePropertyChanged(() => SelectChildrens);
            }
        }


        public List<EntityConfig> GetSelectEntities()
        {
            var tables = new List<EntityConfig>();
            var col = SelectConfig as PropertyConfig;
            if (col != null)
            {
                tables.Add(col.Parent);
            }
            else
            {
                var tableSchema = SelectConfig as EntityConfig;
                if (tableSchema != null)
                {
                    tables.Add(tableSchema);
                }
                else
                {
                    var otype = SelectConfig as ProjectConfig;
                    if (otype != null)
                        tables.AddRange(otype.Entities);
                    else
                        foreach (var project in Solution.Projects)
                            tables.AddRange(project.Entities);
                }
            }
            tables = tables.ToList();
            return tables;
        }

        #endregion

        #region 当前选择处理

        /// <summary>
        ///     属性表格
        /// </summary>
        internal PropertyGrid PropertyGrid { get; set; }


        internal void SetSelectItem(ConfigBase cfg, string title)
        {
            SelectConfig = cfg;

            if (SelectConfig == null)
            {
                NowJob = JobTrace;
                SelecReleation = null;
                SelectProperty = null;
                SelectEntity = null;
                CurrentTrace.TraceMessage = null;
                SelectItemChildrens = null;
                if (PropertyGrid != null)
                    PropertyGrid.SelectedObject = null;
                SelectChildrens = null;
                return;
            }
            if (PropertyGrid != null)
                PropertyGrid.SelectedObject = SelectConfig;
            if (cfg is SolutionConfig)
            {
                SelectChildrens = Solution.Projects;
                return;
            }

            if (cfg is ProjectConfig)
            {
                SelectChildrens = SelectProject?.Entities;
                return;
            }

            if (cfg is EntityConfig)
            {
                SelectItemChildrens = SelectEntity.Properties;
                SelectChildrens = SelectItemChildrens;
                FindKey = SelectEntity.Tag;
                SubJobIndex = SubJobIndex; 
                return;
            }

            NowJob = JobPropertyGrid;
            if (cfg is PropertyConfig)
            {
                SelectItemChildrens = SelectEntity?.Properties;

                SelectChildrens = SelectProperty?.EnumConfig?.Items;
                FindKey = SelectProperty?.CppLastType;
            }
            //if (cfg is TableReleation)
            //{
            //    SelectEntity = value.FindParentModel<EntityConfig>();
            //    SelectProperty = value.FindParentModel<PropertyConfig>();
            //}
            //SelectItemChildrens = null;
            //SelectChildrens = SelectItem.FriendItems;
        }

        private PropertyConfig _selectRelationColumn;
        private EntityConfig _selectRelationTable;

        /// <summary>
        ///     当前选择
        /// </summary>
        public Visibility RelationVisibility
            => SelectProperty == null ? Visibility.Collapsed : Visibility.Visible;

        /// <summary>
        ///     当前选择
        /// </summary>
        public EntityConfig SelectRelationTable
        {
            get { return _selectRelationTable; }
            set
            {
                if (Equals(_selectRelationTable, value))
                    return;
                _selectRelationTable = value;
                RaisePropertyChanged(() => SelectRelationTable);
                RaisePropertyChanged(() => CurrentRelationColumns);
                if (value != null)
                    SelectRelationColumn = value.PrimaryColumn;
            }
        }

        /// <summary>
        ///     当前选择
        /// </summary>
        public IEnumerable<PropertyConfig> CurrentRelationColumns => _selectRelationTable?.Properties;

        /// <summary>
        ///     当前选择
        /// </summary>
        public PropertyConfig SelectRelationColumn
        {
            get { return _selectRelationColumn; }
            set
            {
                if (Equals(_selectRelationColumn, value))
                    return;
                _selectRelationColumn = value;
                RaisePropertyChanged(() => SelectRelationColumn);
            }
        }

        #endregion

        #region 工作对象

        /// <summary>
        ///     可用工作列表
        /// </summary>
        public string[] Jobs => ChildrenJobs.Keys.ToArray();
        
        /// <summary>
        ///     可用工作列表
        /// </summary>
        public Dictionary<string, int> ChildrenJobs { get; } = new Dictionary<string, int>
        {
            {JobConfig,-1 },
            {JobPropertyGrid,-1 },
            {JobExtendCode,-1 },
            {JobTrace,-1 }
        };

        

        public const string JobPropertyGrid = "属性编辑";
        public const string JobExtendCode = "扩展代码";
        public const string JobTrace = "跟踪消息";
        public const string JobConfig = "基本信息"; 

        private int _rootJobIndex;

        /// <summary>
        ///     当前选中的TableControl页序号
        /// </summary>
        public int RootJobIndex
        {
            get { return _rootJobIndex; }
            set
            {
                if (!Equals(_rootJobIndex, value))
                {
                    _rootJobIndex = value;
                    RaisePropertyChanged(() => RootJobIndex);
                }
                switch (value)
                {
                    case 0:
                        NowJob = JobConfig;
                        break;
                    case 2:
                        NowJob = JobExtendCode;
                        break;
                    case 3:
                        NowJob = JobPropertyGrid;
                        break;
                    case 4:
                        NowJob = JobTrace;
                        break;
                }
            }
        }
        private int _subJobIndex;

        /// <summary>
        ///     当前选中的TableControl页序号
        /// </summary>
        public int SubJobIndex
        {
            get { return _subJobIndex; }
            set
            {
                if (!Equals(_subJobIndex, value))
                {
                    _subJobIndex = value;
                    RaisePropertyChanged(() => SubJobIndex);
                }
                var job = ChildrenJobs.FirstOrDefault(p => p.Value == value);
                if (job.Value >= 0)
                {
                    NowJob = job.Key;
                }
            }
        }


        private string _nowJob;

        public string NowJob
        {
            get { return _nowJob; }
            set
            {
                if (Equals(_nowJob, value))
                    return;
                _nowJob = value ?? JobPropertyGrid;
                switch (value)
                {
                    case JobConfig:
                        RootJobIndex = 0;
                        break;
                    default:
                        RootJobIndex = 1;
                        int idx = 0;
                        ChildrenJobs.TryGetValue(_nowJob, out idx);
                        SubJobIndex = idx;
                        break;
                    case JobExtendCode:
                        RootJobIndex = 2;
                        break;
                    case JobPropertyGrid:
                        RootJobIndex = 3;
                        break;
                    case JobTrace:
                        RootJobIndex = 4;
                        break;
                }
                SaveJob();
                RaisePropertyChanged(() => NowJob);
            }
        }

        private void LoadJob()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            var path = Path.Combine(GlobalConfig.ProgramRoot, "Config", "job.bin");
            if (!File.Exists(path))
                return;
            var text = File.ReadAllText(path);
            if (string.IsNullOrEmpty(text))
            {
                NowJob = "属性编辑";
                return;
            }
            var lines = text.Split('\n');
            if (lines.Length > 0)
                NowJob = lines[0].Trim();
        }

        private void SaveJob()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            var path = Path.Combine(GlobalConfig.ProgramRoot, "Config", "job.bin");
            File.WriteAllText(path, NowJob);
        }

        #endregion

        #region 对象查找

        private string _findKey;

        public string FindKey
        {
            get { return _findKey; }
            set
            {
                _findKey = value;
                RaisePropertyChanged(nameof(FindKey));
            }
        }

        #endregion

        #region 剪贴板

        /// <summary>
        /// 当前已复制的列
        /// </summary>
        public List<PropertyConfig> CopyColumns { get; set; }

        /// <summary>
        /// 粘贴近按钮是否显示
        /// </summary>
        public Visibility PasteVisiable => CopiedTables.Count > 0 ? Visibility.Visible : Visibility.Collapsed;

        /// <summary>
        /// 当前可复制的表
        /// </summary>
        public EntityConfig CopiedTable
        {
            get { return CopiedTables.LastOrDefault(); }
            set
            {
                if (value == null || CopiedTables.Contains(value))
                    return;
                CopiedTables.Add(value);
                RaisePropertyChanged(() => CopiedTable);
                RaisePropertyChanged(() => PasteVisiable);
                RaisePropertyChanged(() => CopiedTableCounts);
            }
        }
        /// <summary>
        /// 可复制的表集合
        /// </summary>
        public List<EntityConfig> CopiedTables { get; } = new List<EntityConfig>();

        /// <summary>
        /// 可复制的表的总数
        /// </summary>
        public int CopiedTableCounts => CopiedTables.Count;

        #endregion
    }
}