using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.DataModel
{
    
    /// <summary>
    /// 商品类型类型
    /// </summary>
    /// <remark>
    /// 商品类型类型
    /// </remark>
    public enum ProductType
    {
        /// <summary>
        /// 不确定
        /// </summary>
        None = 0x0,
        /// <summary>
        /// 食品
        /// </summary>
        Food = 0x1,
        /// <summary>
        /// 电子产品
        /// </summary>
        Electronic = 0x2,
        /// <summary>
        /// 服装
        /// </summary>
        Clothing = 0x3,
    }

    public static class EnumExtend
    {

        /// <summary>
        ///     商品类型类型名称转换
        /// </summary>
        public static string ToCaption(this ProductType value)
        {
            switch (value)
            {
                case ProductType.None:
                    return "不确定";
                case ProductType.Food:
                    return "食品";
                case ProductType.Electronic:
                    return "电子产品";
                case ProductType.Clothing:
                    return "服装";
                default:
                    return "商品类型类型(错误)";
            }
        }
    }
}
