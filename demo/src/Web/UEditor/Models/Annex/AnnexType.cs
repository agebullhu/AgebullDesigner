namespace  Agebull.ZeroNet.ManageApplication
{
    /// <summary>
    /// 附件枚举类型
    /// </summary>
    /// <remark>
    /// 附件类型
    /// </remark>
    public enum AnnexType
    {
        /// <summary>
        /// 未知
        /// </summary>
        None = 0x0,
        /// <summary>
        /// Word文档
        /// </summary>
        Wrod = 0x1,
        /// <summary>
        /// Excel文档
        /// </summary>
        Excel = 0x2,
        /// <summary>
        /// PDF文档
        /// </summary>
        Pdf = 0x3,
        /// <summary>
        /// 声音文件
        /// </summary>
        Audio = 0x4,
        /// <summary>
        /// 视频文件
        /// </summary>
        Video = 0x5,
        /// <summary>
        /// 图片文件
        /// </summary>
        Image = 0x6,
        /// <summary>
        /// PPT文件
        /// </summary>
        Ppt = 0x7,
        /// <summary>
        /// WPS文件
        /// </summary>
        WPS = 0x8,
        /// <summary>
        /// 文本文件
        /// </summary>
        Text = 0x9,
    }

    internal static class EnumHelper
    {

        /// <summary>
        ///     附件枚举类型名称转换
        /// </summary>
        public static string ToCaption(this AnnexType value)
        {
            switch (value)
            {
                case AnnexType.None:
                    return "未知";
                case AnnexType.Wrod:
                    return "Word文档";
                case AnnexType.Excel:
                    return "Excel文档";
                case AnnexType.Pdf:
                    return "PDF文档";
                case AnnexType.Audio:
                    return "声音文件";
                case AnnexType.Video:
                    return "视频文件";
                case AnnexType.Image:
                    return "图片文件";
                case AnnexType.Ppt:
                    return "PPT文件";
                case AnnexType.WPS:
                    return "WPS文件";
                case AnnexType.Text:
                    return "文本文件";
                default:
                    return "附件枚举类型(未知)";
            }
        }
    }
}