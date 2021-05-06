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
        /// 是否布尔类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsDateTimeType(this string type)
        {
            if (type == null)
                return false;
            return type.ToLower() switch
            {
                "datetime" => true,
                _ => false,
            };
        }

        /// <summary>
        /// 是否布尔类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsBooleanType(this string type)
        {
            if (type == null)
                return false;
            return type.ToLower() switch
            {
                "bool" or "boolean" => true,
                _ => false,
            };
        }
        /// <summary>
        /// 是否数字类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsStringType(this string type)
        {
            if (type == null)
                return false;
            return type.ToLower() switch
            {
                "string" => true,
                _ => false,
            };
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
            return type.ToLower() switch
            {
                "byte" or "sbyte" or "int16" or "int32" or "int64" or "uint16" or "uint32" or "uint64" or "single" or "float" or "double" or "decimal" or "long" or "ulong" or "short" or "ushort" or "int" or "uint" => true,
                _ => false,
            };
        }
    }
}
