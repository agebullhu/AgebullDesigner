using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Agebull.CodeRefactor.CodeRefactor;
using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    ///     �����������
    /// </summary>
    public class DesignContext : DesignModelBase, IGridSelectionBinding
    {
        #region ��ƶ���

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
                        Editor.PropertyGrid.SelectedObjects = array;
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

                if (Equals(_selectColumns, value))
                    return;
                _selectColumns = value;
                RaisePropertyChanged(() => SelectColumns);
            }
        }

        private EntityConfig _selectEntityConfig;
        private ModelConfig _selectModelConfig;
        private ProjectConfig _selectProjectConfig;
        private FieldConfig _selectFieldConfig;
        private PropertyConfig _selectPropertyConfig;
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
        public ModelConfig SelectModel
        {
            get => _selectModelConfig;
            set
            {
                if (_selectModelConfig == value)
                    return;
                _selectModelConfig = value;
                if (value != null)
                    SelectEntity = value.Entity;

                RaisePropertyChanged(() => SelectModel);

                RaisePropertyChanged(() => RelationVisibility);
            }
        }


        /// <summary>
        ///     ��ǰ����
        /// </summary>
        public EntityConfig SelectEntity
        {
            get => _selectEntityConfig;
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
        public FieldConfig SelectField
        {
            get => _selectFieldConfig;
            set
            {
                if (_selectFieldConfig == value)
                    return;
                if (value != null)
                    SelectEntity = value.Entity;
                _selectFieldConfig = value;
                RaisePropertyChanged(() => SelectField);
                RaisePropertyChanged(() => RelationVisibility);
            }
        }

        /// <summary>
        ///     ��ǰ����
        /// </summary>
        public PropertyConfig SelectProperty
        {
            get => _selectPropertyConfig;
            set
            {
                if (_selectPropertyConfig == value)
                    return;
                if (value != null)
                {
                    SelectModel = value.Model;
                    SelectField = value.Field;
                }
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
                        if (SelectEntity != null && SelectEntity.Parent != project)
                        {
                            SelectEntity = null;
                        }
                        SelectField = null;
                        if (SelectModel != null && SelectModel.Parent != project)
                        {
                            SelectModel = null;
                        }
                        SelectProperty = null;
                        SelectChildrens = SelectProject?.Entities;
                        break;
                    case EntityConfig entity:
                        SelectEntity = entity;
                        if (SelectField != null && SelectField.Entity != entity)
                        {
                            SelectField = null;
                        }
                        SelectProperty = null;
                        SelectModel = null;
                        SelectItemChildrens = SelectEntity?.Properties;
                        SelectChildrens = SelectItemChildrens;
                        FindKey = SelectEntity.Option.ReferenceTag;
                        break;
                    case FieldConfig field:
                        SelectModel = null;
                        SelectProperty = null;
                        SelectField = field;
                        SelectItemChildrens = SelectEntity?.Properties;
                        SelectChildrens = SelectField?.EnumConfig?.Items;
                        FindKey = SelectField?.CppLastType;
                        break;
                    case ModelConfig model:
                        SelectModel = model;
                        SelectProperty = null;
                        SelectField = null;
                        SelectItemChildrens = SelectModel?.Properties;
                        SelectChildrens = SelectItemChildrens;
                        FindKey = SelectModel.Option.ReferenceTag;
                        break;
                    case PropertyConfig property:
                        SelectProperty = property;
                        SelectItemChildrens = SelectModel?.Properties;
                        SelectChildrens = SelectField?.EnumConfig?.Items;
                        FindKey = SelectProperty?.Field?.CppLastType;
                        break;
                    case ProjectChildConfigBase child:
                        SelectField = null;
                        SelectEntity = null;
                        SelectProject = child.Parent;
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
            if (Editor.PropertyGrid != null)
                Editor.PropertyGrid.SelectedObject = SelectConfig;
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
                    SelectRelationColumn = value.PrimaryColumn;
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
        /// �ɸ��Ƶı�����
        /// </summary>
        public List<EntityConfig> CopiedTables { get; } = new List<EntityConfig>();

        /// <summary>
        /// �ɸ��Ƶı�������
        /// </summary>
        public int CopiedTableCounts => CopiedTables.Count;

        #endregion
    }
}