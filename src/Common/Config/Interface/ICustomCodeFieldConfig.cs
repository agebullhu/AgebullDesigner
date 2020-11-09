using System.Collections.Generic;

namespace Agebull.EntityModel.Config
{
    public interface ICustomCodeFieldConfig : IConfig
    {

        #region 计算列
        List<string> GetAliasPropertys();

        /// <summary>
        /// 自定义代码(get)
        /// </summary>
        /// <remark>
        /// 自定义代码Get部分代码,仅用于C#
        /// </remark>
        string ComputeGetCode
        {
            get;
            set;
        }
        /// <summary>
        /// 自定义代码(set)
        /// </summary>
        /// <remark>
        /// 自定义代码Set部分代码,仅用于C#
        /// </remark>
        string ComputeSetCode
        {
            get;
            set;
        }
        /// <summary>
        /// 自定义读写代码
        /// </summary>
        /// <remark>
        /// 自定义读写代码,即不使用代码生成,而使用录入的代码
        /// </remark>
        bool IsCustomCompute
        {
            get;
            set;
        }
        #endregion
    }
}