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
        /// <summary>
        ///     ״̬��Ϣ
        /// </summary>
        public string StateMessage
        {
            get { return Context.StateMessage; }
            set { Context.StateMessage = value; }
        }

        /// <summary>
        /// Ӧ�ø�Ŀ¼
        /// </summary>
        private string _programRoot;

        /// <summary>
        /// Ӧ�ø�Ŀ¼
        /// </summary>
        public string ProgramRoot => _programRoot ?? (_programRoot =
                                                Path.GetDirectoryName(Path.GetDirectoryName(typeof(GlobalConfig).Assembly.Location)));

        /// <summary>
        /// �������
        /// </summary>
        public SolutionConfig GlobalSolution { get; set; }

        /// <summary>
        /// �������
        /// </summary>
        public SolutionConfig CurrentSolution
        {
            get { return Context.Solution; }
            set
            {
                Context.Solution = value;
            }
        }
        /// <summary>
        /// ��ǰѡ��
        /// </summary>
        public ConfigBase CurrentConfig { get; set; }


        /// <summary>
        ///     ö�ټ���
        /// </summary>
        private ObservableCollection<SolutionConfig> _solutions;

        /// <summary>
        ///     �����������
        /// </summary>
        public ObservableCollection<SolutionConfig> Solutions
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
        public ObservableCollection<EnumConfig> Enums
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
        ///     ����(C++)����
        /// </summary>
        private ObservableCollection<TypedefItem> _typedefItems;

        /// <summary>
        ///     ����(C++)����
        /// </summary>
        public ObservableCollection<TypedefItem> TypedefItems
        {
            get
            {
                if (_typedefItems != null)
                    return _typedefItems;
                _typedefItems = new ObservableCollection<TypedefItem>();
                return _typedefItems;
            }
        }

        /// <summary>
        ///     ʵ�弯��
        /// </summary>
        private ObservableCollection<EntityConfig> _entities;

        /// <summary>
        ///     ʵ�弯��
        /// </summary>
        public ObservableCollection<EntityConfig> Entities
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
        public ObservableCollection<ProjectConfig> Projects
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
        public ObservableCollection<ApiItem> ApiItems
        {
            get
            {
                if (_apiItems != null)
                    return _apiItems;
                _apiItems = new ObservableCollection<ApiItem>();
                return _apiItems;
            }
        }

        /// <summary>
        ///     ֪ͨ����
        /// </summary>
        private ObservableCollection<NotifyItem> _notifyItems;

        /// <summary>
        ///     ֪ͨ����
        /// </summary>
        public ObservableCollection<NotifyItem> NotifyItems
        {
            get
            {
                if (_notifyItems != null)
                    return _notifyItems;
                _notifyItems = new ObservableCollection<NotifyItem>();
                return _notifyItems;
            }
        }
    }
}