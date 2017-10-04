// /***********************************************************************************************************************
// 工程：Agebull.Common.SimpleDesign
// 项目：CodeRefactor
// 文件：DataAccessDesignModel.cs
// 作者：Administrator/
// 建立：2015－07－13 12:26
// ****************************************************文件说明**********************************************************
// 对应文档：
// 说明摘要：
// 作者备注：
// ****************************************************修改记录**********************************************************
// 日期：
// 人员：
// 说明：
// ************************************************************************************************************************
// 日期：
// 人员：
// 说明：
// ************************************************************************************************************************
// 日期：
// 人员：
// 说明：
// ***********************************************************************************************************************/

#region 命名空间引用

using System.Collections.Generic;
using Agebull.Common.DataModel;
using Agebull.Common.SimpleDesign;
using Gboxt.Common.DataAccess.Schemas;

#endregion

namespace Agebull.CodeRefactor.SolutionManager
{
    /// <summary>
    /// 数据模型设计模型
    /// </summary>
    public sealed class DataModelDesignModel : ModelBase
    {
        #region 设计对象 

        public DesignContext Context { get; }
        public DesignGlobal Global { get; }
        public TreeModel Tree { get; }
        public ConfigIoModel  ConfigIo { get; }
        public NormalCodeModel NormalCode { get; }

        
        public ExtendConfigModel ExtendConfig { get; }

        
        public static DataModelDesignModel Current { get; set; }

        /// <summary>
        /// 构造
        /// </summary>
        public DataModelDesignModel()
        {
            Current = this;
            Context = new DesignContext();

            Global = new DesignGlobal
            {
                Model = this,
                Dispatcher = Dispatcher,
                Context = Context
            };
            GlobalConfig.SetGlobal(Global);
            ExtendConfig = new ExtendConfigModel
            {
                Model = this,
                Dispatcher = Dispatcher,
                Context = Context
            };
            Tree = new TreeModel
            {
                Model = this,
                Dispatcher = Dispatcher,
                Context = Context
            };
            ConfigIo = new ConfigIoModel
            {
                Model = this,
                Dispatcher = Dispatcher,
                Context = Context
            };
            NormalCode = new NormalCodeModel
            {
                Model = this,
                Dispatcher = Dispatcher,
                Context = Context
            };
        }

        #endregion

        #region 扩展对象

        /// <summary>
        /// 扩展模型
        /// </summary>
        public Dictionary<string, DesignModelBase> ExtendModels { get; } = new Dictionary<string, DesignModelBase>();

        #endregion

        #region 初始化

        /// <summary>
        ///     初始化
        /// </summary>
        protected override void DoInitialize()
        {
            base.DoInitialize();
            ExtendConfig.Initialize();
            Context.Initialize();
            ConfigIo.Initialize();
            NormalCode.Initialize();

            foreach (var ex in ExtendModels.Values)
            {
                ex.Model = this;
                ex.Dispatcher = Dispatcher;
                ex.Context = Context;
                ex.Initialize();
            }
        }

        /// <summary>
        /// 新增对象
        /// </summary>
        /// <typeparam name="TConfig"></typeparam>
        /// <param name="config"></param>
        /// <returns></returns>
        public bool CreateNew<TConfig>(out TConfig config)
            where TConfig : ConfigBase, new()
        {
            config = new TConfig();
            return CommandIoc.NewConfigCommand(config);
        }

        #endregion

    }
}