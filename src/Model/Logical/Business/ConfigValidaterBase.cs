namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// 配置数据校验器基类
    /// </summary>
    public abstract class ConfigValidaterBase : GlobalConfig
    {
        /// <summary>
        /// 消息跟踪器
        /// </summary>
        public TraceMessage Message { get; set; }

        /// <summary>
        ///     数据校验
        /// </summary>
        public bool Validate(TraceMessage trace)
        {
            Message = trace;
            return Validate();
        }

        /// <summary>
        ///     数据校验
        /// </summary>
        protected abstract bool Validate();
    }
}