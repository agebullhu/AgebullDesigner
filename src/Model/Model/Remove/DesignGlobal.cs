using System.Collections.ObjectModel;
using System.IO;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 设计器全局对象
    /// </summary>
    public class DesignGlobal : DesignModelBase, IDesignGlobal
    {
        /// <summary>
        ///     状态消息
        /// </summary>
        public string StateMessage
        {
            get { return Context.StateMessage; }
            set { Context.StateMessage = value; }
        }

        /// <summary>
        /// 应用根目录
        /// </summary>
        private string _programRoot;

        /// <summary>
        /// 应用根目录
        /// </summary>
        public string ProgramRoot => _programRoot ?? (_programRoot =
                                                Path.GetDirectoryName(Path.GetDirectoryName(typeof(GlobalConfig).Assembly.Location)));

        /// <summary>
        /// 解决方案
        /// </summary>
        public SolutionConfig GlobalSolution { get; set; }

        /// <summary>
        /// 解决方案
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
        /// 当前选择
        /// </summary>
        public ConfigBase CurrentConfig { get; set; }


        /// <summary>
        ///     枚举集合
        /// </summary>
        private ObservableCollection<SolutionConfig> _solutions;

        /// <summary>
        ///     解决方案集合
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
        ///     枚举集合
        /// </summary>
        private ObservableCollection<EnumConfig> _enums;

        /// <summary>
        ///     枚举集合
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
        ///     类型(C++)集合
        /// </summary>
        private ObservableCollection<TypedefItem> _typedefItems;

        /// <summary>
        ///     类型(C++)集合
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
        ///     实体集合
        /// </summary>
        private ObservableCollection<EntityConfig> _entities;

        /// <summary>
        ///     实体集合
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
        ///     项目集合
        /// </summary>
        private ObservableCollection<ProjectConfig> _projects;

        /// <summary>
        ///     项目集合
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
        ///     API集合
        /// </summary>
        private ObservableCollection<ApiItem> _apiItems;

        /// <summary>
        ///     API集合
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
        ///     通知集合
        /// </summary>
        private ObservableCollection<NotifyItem> _notifyItems;

        /// <summary>
        ///     通知集合
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