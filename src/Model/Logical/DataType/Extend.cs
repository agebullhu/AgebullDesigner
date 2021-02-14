namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// C++与C#类型转换辅助类
    /// </summary>
    public static class TypeExtend
    {

        /// <summary>
        /// 是否为空分类
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsEmptyClassify(this string classify)
        {
            return string.IsNullOrWhiteSpace(classify)
                   || classify == "数据实体"
                   || classify == "None";
        }

        /// <summary>
        /// 是否数字类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsNumberType(this string type)
        {
            if (type == null)
                return false;
            switch (type.ToLower())
            {
                case "byte":
                case "sbyte":
                case "int16":
                case "int32":
                case "int64":
                case "uint16":
                case "uint32":
                case "uint64":
                case "single":
                case "float":
                case "double":
                case "decimal":
                case "long":
                case "ulong":
                case "short":
                case "ushort":
                case "int":
                case "uint":
                    return true;
                default:
                    return false;
            }
        }
    }
}
