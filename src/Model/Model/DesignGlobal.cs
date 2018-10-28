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
        SolutionConfig IDesignGlobal.LocalSolution { get; set; }

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
        private NotificationList<SolutionConfig> _solutions;

        /// <summary>
        ///     �����������
        /// </summary>
        NotificationList<SolutionConfig> IDesignGlobal.Solutions
        {
            get
            {
                if (_solutions != null)
                    return _solutions;
                _solutions = new NotificationList<SolutionConfig>();
                return _solutions;
            }
        }

        /// <summary>
        ///     ö�ټ���
        /// </summary>
        private NotificationList<EnumConfig> _enums;

        /// <summary>
        ///     ö�ټ���
        /// </summary>
        NotificationList<EnumConfig> IDesignGlobal.Enums
        {
            get
            {
                if (_enums != null)
                    return _enums;
                _enums = new NotificationList<EnumConfig>();
                return _enums;
            }
        }

        /// <summary>
        ///     ʵ�弯��
        /// </summary>
        private NotificationList<EntityConfig> _entities;

        /// <summary>
        ///     ʵ�弯��
        /// </summary>
        NotificationList<EntityConfig> IDesignGlobal.Entities
        {
            get
            {
                if (_entities != null)
                    return _entities;
                _entities = new NotificationList<EntityConfig>();
                return _entities;
            }
        }

        /// <summary>
        ///     ��Ŀ����
        /// </summary>
        private NotificationList<ProjectConfig> _projects;

        /// <summary>
        ///     ��Ŀ����
        /// </summary>
        NotificationList<ProjectConfig> IDesignGlobal.Projects
        {
            get
            {
                if (_projects != null)
                    return _projects;
                _projects = new NotificationList<ProjectConfig>();
                return _projects;
            }
        }

        /// <summary>
        ///     API����
        /// </summary>
        private NotificationList<ApiItem> _apiItems;

        /// <summary>
        ///     API����
        /// </summary>
        NotificationList<ApiItem> IDesignGlobal.ApiItems
        {
            get
            {
                if (_apiItems != null)
                    return _apiItems;
                _apiItems = new NotificationList<ApiItem>();
                return _apiItems;
            }
        }

    }
}