using System.Linq;

namespace Agebull.EntityModel.Config
{

    /// <summary>
    ///     表示代码风格
    /// </summary>
    public interface ICodeStyle
    {
        /// <summary>
        /// 风格名称
        /// </summary>
        string StyleName { get;  }

        /// <summary>
        /// 风格目标对象
        /// </summary>
        string StyleTarget { get;  }
    }
}
