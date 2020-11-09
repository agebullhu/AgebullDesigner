namespace Agebull.EntityModel.Config
{
    public interface ICollectFieldConfig : IConfig
    {
        #region 汇总支持

        /// <summary>
        /// 汇总方法
        /// </summary>
        string Function
        {
            get;
            set;
        }

        /// <summary>
        /// 汇总条件
        /// </summary>
        string Having
        {
            get;
            set;
        }

        #endregion
    }
}