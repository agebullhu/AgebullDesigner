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

        private readonly string _programRoot;

        internal DesignGlobal()
        {
            _programRoot = Path.GetDirectoryName(Path.GetDirectoryName(typeof(GlobalConfig).Assembly.Location));
        }

        /// <summary>
        ///     状态消息
        /// </summary>
        string IDesignGlobal.StateMessage
        {
            get => Context.StateMessage;
            set => Context.StateMessage = value;
        }

        /// <inheritdoc />
        /// <summary>
        /// 应用根目录
        /// </summary>
        string IDesignGlobal.ProgramRoot => _programRoot;

        /// <summary>
        /// 解决方案
        /// </summary>
        SolutionConfig IDesignGlobal.GlobalSolution { get; set; }

        /// <summary>
        /// 解决方案
        /// </summary>
        SolutionConfig IDesignGlobal.CurrentSolution
        {
            get => Context.Solution;
            set => Context.Solution = value;
        }

        /// <summary>
        /// 当前选择
        /// </summary>
        ConfigBase IDesignGlobal.CurrentConfig
        {
            get => Context.SelectConfig;
            set => Context.SelectConfig = value;
        }


        /// <summary>
        ///     枚举集合
        /// </summary>
        private ObservableCollection<SolutionConfig> _solutions;

        /// <summary>
        ///     解决方案集合
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
        ///     枚举集合
        /// </summary>
        private ObservableCollection<EnumConfig> _enums;

        /// <summary>
        ///     枚举集合
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
        ///     实体集合
        /// </summary>
        private ObservableCollection<EntityConfig> _entities;

        /// <summary>
        ///     实体集合
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
        ///     项目集合
        /// </summary>
        private ObservableCollection<ProjectConfig> _projects;

        /// <summary>
        ///     项目集合
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
        ///     API集合
        /// </summary>
        private ObservableCollection<ApiItem> _apiItems;

        /// <summary>
        ///     API集合
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