namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// 代码风格常量
    /// </summary>
    public class CodeStyleConst
    {
        /// <summary>
        /// 风格列表
        /// </summary>
        public static ComboItem[] StyleList = new[]
        {
            new ComboItem("标准风格",Style.General),
            new ComboItem("精简风格",Style.Succinct)
        };

        /// <summary>
        /// 风格
        /// </summary>
        public class Style
        {
            /// <summary>
            /// 标准风格
            /// </summary>
            public const string General = "标准风格";

            /// <summary>
            /// 精简风格
            /// </summary>
            public const string Succinct = "精简风格";

        }

        /// <summary>
        /// 目标
        /// </summary>
        public class Target
        {
            /// <summary>
            /// 数据库
            /// </summary>
            public const string DataBase = "DataBase";

            /// <summary>
            /// 模型
            /// </summary>
            public const string Model = "Model";

            /// <summary>
            /// JS
            /// </summary>
            public const string JavaScript = "JavaScript";

            /// <summary>
            /// JS
            /// </summary>
            public const string HTML = "HTML";

            /// <summary>
            /// C#
            /// </summary>
            public const string CSharp = "C#";
        }
    }
}
