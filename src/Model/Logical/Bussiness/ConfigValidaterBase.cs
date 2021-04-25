namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// ��������У��������
    /// </summary>
    public abstract class ConfigValidaterBase : GlobalConfig
    {
        /// <summary>
        /// ��Ϣ������
        /// </summary>
        public TraceMessage Message { get; set; }

        /// <summary>
        ///     ����У��
        /// </summary>
        public bool Validate(TraceMessage trace)
        {
            Message = trace;
            return Validate();
        }

        /// <summary>
        ///     ����У��
        /// </summary>
        protected abstract bool Validate();
    }
}