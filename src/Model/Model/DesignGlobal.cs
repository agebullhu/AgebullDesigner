using System.Collections.ObjectModel;
using System.IO;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// �����ȫ�ֶ���
    /// </summary>
    public class DesignGlobal : DesignModelBase, IDesignGlobal
    {

        private readonly string _programRoot;

        internal DesignGlobal()
        {
            _programRoot = Path.GetDirectoryName(Path.GetDirectoryName(typeof(GlobalConfig).Assembly.Location));
        }

        /// <summary>
        ///     ״̬��Ϣ
        /// </summary>
        string IDesignGlobal.StateMessage
        {
            get => Context.StateMessage;
            set => Context.StateMessage = value;
        }

        /// <inheritdoc />
        /// <summary>
        /// Ӧ�ø�Ŀ¼
        /// </summary>
        string IDesignGlobal.ProgramRoot => _programRoot;

        /// <summary>
        /// �������
        /// </summary>
        SolutionConfig IDesignGlobal.GlobalSolution { get; set; }

        /// <summary>
        /// �������
        /// </summary>
        SolutionConfig IDesignGlobal.CurrentSolution
        {
            get => Context.Solution;
            set => Context.Solution = value;
        }

        /// <summary>
        /// ��ǰѡ��
        /// </summary>
        ConfigBase IDesignGlobal.CurrentConfig
        {
            get => Context.SelectConfig;
            set => Context.SelectConfig = value;
        }


        /// <summary>
        ///     ö�ټ���
        /// </summary>
        private ObservableCollection<SolutionConfig> _solutions;

        /// <summary>
        ///     �����������
        /// </summary>
        ObservableCollection<SolutionConfig> IDesignGlobal.Solutions
        {
            get
            {
                if (_solutions != null)
                    return _solutions;
                _solutions = new ObservableCollection<SolutionConfig>();
                return _solutions;
            }
        }

        /// <summary>
        ///     ö�ټ���
        /// </summary>
        private ObservableCollection<EnumConfig> _enums;

        /// <summary>
        ///     ö�ټ���
        /// </summary>
        ObservableCollection<EnumConfig> IDesignGlobal.Enums
        {
            get
            {
                if (_enums != null)
                    return _enums;
                _enums = new ObservableCollection<EnumConfig>();
                return _enums;
            }
        }

        /// <summary>
        ///     ʵ�弯��
        /// </summary>
        private ObservableCollection<EntityConfig> _entities;

        /// <summary>
        ///     ʵ�弯��
        /// </summary>
        ObservableCollection<EntityConfig> IDesignGlobal.Entities
        {
            get
            {
                if (_entities != null)
                    return _entities;
                _entities = new ObservableCollection<EntityConfig>();
                return _entities;
            }
        }

        /// <summary>
        ///     ��Ŀ����
        /// </summary>
        private ObservableCollection<ProjectConfig> _projects;

        /// <summary>
        ///     ��Ŀ����
        /// </summary>
        ObservableCollection<ProjectConfig> IDesignGlobal.Projects
        {
            get
            {
                if (_projects != null)
                    return _projects;
                _projects = new ObservableCollection<ProjectConfig>();
                return _projects;
            }
        }

        /// <summary>
        ///     API����
        /// </summary>
        private ObservableCollection<ApiItem> _apiItems;

        /// <summary>
        ///     API����
        /// </summary>
        ObservableCollection<ApiItem> IDesignGlobal.ApiItems
        {
            get
            {
                if (_apiItems != null)
                    return _apiItems;
                _apiItems = new ObservableCollection<ApiItem>();
                return _apiItems;
            }
        }

    }
}