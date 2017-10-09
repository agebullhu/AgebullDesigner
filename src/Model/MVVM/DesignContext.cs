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
    ///     �����������
    /// </summary>
    public class DesignContext : TraceModelBase, IGridSelectionBinding
    {
        #region �̳�

        /// <summary>
        ///     ��ʼ��
        /// </summary>
        protected override void DoInitialize()
        {
            base.DoInitialize();
            LoadJob();
        }

        #endregion

        #region ��ƶ���

        /// <summary>
        ///     ��ǰ�ļ���
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        ///     ��ǰ�������
        /// </summary>
        private SolutionConfig _solution;

        /// <summary>
        ///     ��ǰ�������
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
        ///     ��ǰ���������ʵ��
        /// </summary>
        public ObservableCollection<EntityConfig> Entities => Solution.Entities;

        #endregion

        #region ״̬

        private string _stateMessage;

        /// <summary>
        ///     ״̬��Ϣ
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

        #region ѡ�ж���

        private IList _selectColumns2;

        /// <summary>
        ///     ��ǰѡ��
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
        ///     ��ǰѡ��
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
        ///     ��ǰ����
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
                StateMessage = $"��ǰ����:{value?.Name}=>{value?.Caption}=>{value?.Description}=>({value?.GetType()})";

                if (GlobalConfig.CurrentConfig == value)
                    return;
                PreSelectConfig = GlobalConfig.CurrentConfig;
                GlobalConfig.CurrentConfig = value;
                RaisePropertyChanged(nameof(SelectConfig));
            }
        }

        /// <summary>
        ///     ��ǰ����
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
        ///     ��ǰ����
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
        ///     ��ǰ����
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
        ///     ��ǰ����
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
        ///     ��ǰѡ��
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
        ///     ��ǰѡ��
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

        #region ��ǰѡ����

        /// <summary>
        ///     ���Ա��
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
        ///     ��ǰѡ��
        /// </summary>
        public Visibility RelationVisibility
            => SelectProperty == null ? Visibility.Collapsed : Visibility.Visible;

        /// <summary>
        ///     ��ǰѡ��
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
        ///     ��ǰѡ��
        /// </summary>
        public IEnumerable<PropertyConfig> CurrentRelationColumns => _selectRelationTable?.Properties;

        /// <summary>
        ///     ��ǰѡ��
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

        #region ��������

        /// <summary>
        ///     ���ù����б�
        /// </summary>
        public string[] Jobs => ChildrenJobs.Keys.ToArray();
        
        /// <summary>
        ///     ���ù����б�
        /// </summary>
        public Dictionary<string, int> ChildrenJobs { get; } = new Dictionary<string, int>
        {
            {JobConfig,-1 },
            {JobPropertyGrid,-1 },
            {JobExtendCode,-1 },
            {JobTrace,-1 }
        };

        

        public const string JobPropertyGrid = "���Ա༭";
        public const string JobExtendCode = "��չ����";
        public const string JobTrace = "������Ϣ";
        public const string JobConfig = "������Ϣ"; 

        private int _rootJobIndex;

        /// <summary>
        ///     ��ǰѡ�е�TableControlҳ���
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
        ///     ��ǰѡ�е�TableControlҳ���
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
                NowJob = "���Ա༭";
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

        #region �������

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

        #region ������

        /// <summary>
        /// ��ǰ�Ѹ��Ƶ���
        /// </summary>
        public List<PropertyConfig> CopyColumns { get; set; }

        /// <summary>
        /// ճ������ť�Ƿ���ʾ
        /// </summary>
        public Visibility PasteVisiable => CopiedTables.Count > 0 ? Visibility.Visible : Visibility.Collapsed;

        /// <summary>
        /// ��ǰ�ɸ��Ƶı�
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
        /// �ɸ��Ƶı���
        /// </summary>
        public List<EntityConfig> CopiedTables { get; } = new List<EntityConfig>();

        /// <summary>
        /// �ɸ��Ƶı������
        /// </summary>
        public int CopiedTableCounts => CopiedTables.Count;

        #endregion
    }
}