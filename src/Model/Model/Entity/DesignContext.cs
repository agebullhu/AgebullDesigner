using Agebull.CodeRefactor.CodeRefactor;
using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    ///     �����������
    /// </summary>
    public class DesignContext : DesignModelBase, IGridSelectionBinding
    {
        #region ��ƶ���

        public static DesignContext Instance { get; set; }
        public DesignContext()
        {
            Instance = this;
        }

        /// <summary>
        ///     ��ǰ�ļ���
        /// </summary>
        public string FileName => DataModelDesignModel.Screen.LastFile;

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
                GlobalConfig.SetCurrentSolution(_solution);
                return _solution;
            }
            set
            {
                if (Equals(_solution, value))
                    return;
                _solution = value;
                GlobalConfig.SetCurrentSolution(_solution);
                RaisePropertyChanged(() => Solution);
                RaisePropertyChanged(() => Entities);
                RaisePropertyChanged(() => FileName);
            }
        }

        /// <summary>
        ///     ��ǰ���������ʵ��
        /// </summary>
        public IEnumerable<EntityConfig> Entities => Solution.Entities;

        private TraceModel _trace;

        /// <summary>
        /// ��ǰѡ�ж������Ϣ������
        /// </summary>
        public TraceModel CurrentTrace => _trace ??= new TraceModel();
        #endregion

        #region ѡ�ж���

        private IList _selectColumns2;

        /// <summary>
        ///     ��ǰѡ��
        /// </summary>
        public IList SelectConfigs
        {
            get => _selectColumns2;
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
            get => _selectColumns;
            set
            {
                _selectColumns = value;
                if (SelectColumns != null && SelectColumns.Count > 0 && SelectColumns[0] is IPropertyConfig field)
                {
                    SelectField = field;
                }
                RaisePropertyChanged(() => SelectColumns);
                /*try
                {
                    if (value != null && value.Count > 0)
                    {
                        var array = new object[value.Count];
                        var idx = 0;
                        foreach (var item in value)
                        {
                            if (item is FieldConfig config)
                                SelectField = config;
                            if (item is PropertyConfig pro)
                                SelectField = pro.Field;
                            array[idx++] = item;
                        }
                        Editor.PropertyGrid.SelectedObjects = array;
                    }
                }
                catch
                {
                }*/

            }
        }

        private IEntityConfig _selectEntityConfig;
        private IPropertyConfig _selectFieldConfig;
        private ProjectConfig _selectProjectConfig;
        private IEnumerable _selectItemChildrens;

        public ConfigBase PreSelectConfig;
        private ConfigBase _selectConfig;
        /// <summary>
        ///     ��ǰ����
        /// </summary>
        public ConfigBase SelectConfig
        {
            get => _selectConfig;
            internal set
            {

                if (_selectConfig == value)
                    return;
                PreSelectConfig = _selectConfig;
                _selectConfig = value;
                ValueRecords = value?.ValueRecords;
                RaisePropertyChanged(nameof(SelectConfig));
            }
        }

        /// <summary>
        ///     ��ǰ����
        /// </summary>
        public ProjectConfig SelectProject
        {
            get => _selectProjectConfig;
            private set
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
        public ModelConfig SelectModel => SelectEntity as ModelConfig;

        /// <summary>
        ///     ��ǰ����
        /// </summary>
        public IEntityConfig SelectEntity
        {
            get => _selectEntityConfig;
            set
            {
                if (_selectEntityConfig == value)
                    return;
                _selectEntityConfig = value;
                if (value != null)
                    SelectProject = value.Project;

                RaisePropertyChanged(() => SelectEntity);
                RaisePropertyChanged(() => SelectModel);
                RaisePropertyChanged(() => RelationVisibility);
            }
        }

        /// <summary>
        ///     ��ǰ����
        /// </summary>
        public IPropertyConfig SelectField
        {
            get => _selectFieldConfig;
            set
            {
                if (_selectFieldConfig == value)
                    return;
                //if (value != null)
                //    SelectEntity = value.Parent;
                _selectFieldConfig = value;
                RaisePropertyChanged(() => SelectField);
                RaisePropertyChanged(() => RelationVisibility);
            }
        }

        /// <summary>
        ///     ��ǰѡ��
        /// </summary>
        public IEnumerable SelectItemChildrens
        {
            get => _selectItemChildrens;
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
            get => _selectChildrens;
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
            switch (SelectConfig)
            {
                case FieldConfig property:
                    tables.Add(property.Entity);
                    break;
                case EntityConfig entity:
                    tables.Add(entity);
                    break;
                case ProjectConfig project:
                    tables.AddRange(project.Entities);
                    break;
                default:
                    foreach (var project in Solution.Projects)
                        tables.AddRange(project.Entities);
                    break;
            }
            tables = tables.ToList();
            return tables;
        }

        #endregion

        #region ״̬

        private string _stateMessage;

        /// <summary>
        ///     ״̬��Ϣ
        /// </summary>
        public string StateMessage
        {
            get => _stateMessage;
            set
            {
                _stateMessage = value;
                if (CurrentTrace != null)
                    CurrentTrace.TraceMessage.Track = value;
                RaisePropertyChanged(() => StateMessage);
            }
        }

        #endregion

        #region ��ǰѡ����

        void SetSelect(ConfigBase value)
        {
            if (value != null)
            {
                switch (value)
                {
                    case ProjectConfig project:
                        SelectProject = project;
                        if (SelectEntity != null && SelectEntity.Project != project)
                        {
                            SelectEntity = null;
                        }
                        SelectField = null;
                        SelectChildrens = SelectProject?.Entities;
                        break;
                    case IEntityConfig entity:
                        SelectEntity = entity;
                        if (SelectField != null && SelectField.Parent != entity)
                        {
                            SelectField = null;
                        }
                        SelectItemChildrens = SelectEntity?.Properties;
                        SelectChildrens = SelectItemChildrens;
                        FindKey = SelectEntity.Option.ReferenceTag;
                        break;
                    case IPropertyConfig field:
                        SelectEntity = field.Parent;
                        SelectField = field;
                        SelectItemChildrens = SelectEntity?.Properties;
                        SelectChildrens = SelectField?.EnumConfig?.Items;
                        FindKey = SelectField?.CppLastType;
                        break;
                    case ProjectChildConfigBase child:
                        SelectField = null;
                        SelectEntity = null;
                        SelectProject = child.Project;
                        break;
                    case SolutionConfig _:
                        SelectChildrens = Solution.Projects;
                        break;
                }
            }
            else
            {
                SelectItemChildrens = null;
                SelectChildrens = null;
            }
            StateMessage = $"��ǰ��Ŀ��{SelectProject?.Caption ?? "δѡ��"} ��ǰʵ�壺{SelectEntity?.Caption ?? "δѡ��"} ��ǰ����:{value?.Caption}({value?.Name})=>({value?.GetType()})";
            SelectConfig = value;
        }

        internal void OnTreeSelectItemChanged(TreeItem item)
        {
            ConfigBase cfg = item?.Source as ConfigBase;
            SelectTag = item?.Tag;
            SetSelect(cfg);
            RaisePropertyChanged(() => SelectTag);
        }
        public string SelectTag { get; set; }

        private FieldConfig _selectRelationColumn;
        private EntityConfig _selectRelationTable;

        /// <summary>
        ///     ��ǰѡ��
        /// </summary>
        public Visibility RelationVisibility
            => SelectField == null ? Visibility.Collapsed : Visibility.Visible;

        /// <summary>
        ///     ��ǰѡ��
        /// </summary>
        public EntityConfig SelectRelationTable
        {
            get => _selectRelationTable;
            set
            {
                if (Equals(_selectRelationTable, value))
                    return;
                _selectRelationTable = value;
                RaisePropertyChanged(() => SelectRelationTable);
                RaisePropertyChanged(() => CurrentRelationColumns);
                if (value != null)
                    SelectRelationColumn = value.PrimaryColumn.Field;
            }
        }

        /// <summary>
        ///     ��ǰѡ��
        /// </summary>
        public IEnumerable<FieldConfig> CurrentRelationColumns => _selectRelationTable?.Properties;

        /// <summary>
        ///     ��ǰѡ��
        /// </summary>
        public FieldConfig SelectRelationColumn
        {
            get => _selectRelationColumn;
            set
            {
                if (Equals(_selectRelationColumn, value))
                    return;
                _selectRelationColumn = value;
                RaisePropertyChanged(() => SelectRelationColumn);
            }
        }

        #endregion

        #region �������

        private string _findKey;

        public string FindKey
        {
            get => _findKey;
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
        public List<FieldConfig> CopyColumns { get; set; }

        /// <summary>
        /// ճ������ť�Ƿ���ʾ
        /// </summary>
        public Visibility PasteVisiable => CopiedTables.Count > 0 ? Visibility.Visible : Visibility.Collapsed;

        /// <summary>
        /// ��ǰ�ɸ��Ƶı�
        /// </summary>
        public EntityConfig CopiedTable
        {
            get => CopiedTables.LastOrDefault();
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