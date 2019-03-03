using Newtonsoft.Json;

namespace Agebull.ZeroNet.ManageApplication
{
    /// <summary>
    ///     前后端通信相关的配置,注释只允许使用多行方式
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class UEditorConfig
    {
        public static UEditorConfig Default = new UEditorConfig();

        /// <summary>
        ///     执行抓取远程图片的action名称
        /// </summary>
        [JsonProperty("catcherActionName", NullValueHandling = NullValueHandling.Ignore)]
        public string CatcherActionName = "catchimage";

        /// <summary>
        ///     抓取图片格式显示
        /// </summary>
        [JsonProperty("catcherAllowFiles", NullValueHandling = NullValueHandling.Ignore)]
        public string[] CatcherAllowFiles = {".png", ".jpg", ".jpeg", ".gif", ".bmp"};

        /// <summary>
        ///     提交的图片列表表单名称
        /// </summary>
        [JsonProperty("catcherFieldName", NullValueHandling = NullValueHandling.Ignore)]
        public string CatcherFieldName = "source";

        /// <summary>
        /// 抓取远程图片配置
        /// </summary>
        [JsonProperty("catcherLocalDomain", NullValueHandling = NullValueHandling.Ignore)]
        public string[] CatcherLocalDomain = {"127.0.0.1", "localhost", "img.baidu.com"};

        /// <summary>
        ///     上传大小限制，单位B
        /// </summary>
        [JsonProperty("catcherMaxSize", NullValueHandling = NullValueHandling.Ignore)]
        public int CatcherMaxSize = 2048000;

        /// <summary>
        ///     上传保存路径,可以自定义保存路径和文件名格式
        /// </summary>
        [JsonProperty("catcherPathFormat", NullValueHandling = NullValueHandling.Ignore)]
        public string CatcherPathFormat = "upload/image/{yyyy}{mm}{dd}/{time}{rand:6}";

        /// <summary>
        ///     图片访问路径前缀
        /// </summary>
        [JsonProperty("catcherUrlPrefix", NullValueHandling = NullValueHandling.Ignore)]
        public string CatcherUrlPrefix = "";

        /* 上传文件配置 */
        /// <summary>
        ///     controller里,执行上传视频的action名称
        /// </summary>
        [JsonProperty("fileActionName", NullValueHandling = NullValueHandling.Ignore)]
        public string FileActionName = "uploadfile";

        /// <summary>
        ///     上传文件格式显示
        /// </summary>
        [JsonProperty("fileAllowFiles", NullValueHandling = NullValueHandling.Ignore)]
        public string[] FileAllowFiles =
        {
            ".png", ".jpg", ".jpeg", ".gif", ".bmp",
            ".flv", ".swf", ".mkv", ".avi", ".rm", ".rmvb", ".mpeg", ".mpg",
            ".ogg", ".ogv", ".mov", ".wmv", ".mp4", ".webm", ".mp3", ".wav", ".mid",
            ".rar", ".zip", ".tar", ".gz", ".7z", ".bz2", ".cab", ".iso",
            ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx", ".pdf", ".txt", ".md", ".xml"
        };

        /// <summary>
        ///     提交的文件表单名称
        /// </summary>
        [JsonProperty("fileFieldName", NullValueHandling = NullValueHandling.Ignore)]
        public string FileFieldName = "upfile";

        /* 列出指定目录下的文件 */
        /// <summary>
        ///     执行文件管理的action名称
        /// </summary>
        [JsonProperty("fileManagerActionName", NullValueHandling = NullValueHandling.Ignore)]
        public string FileManagerActionName = "listfile";

        /// <summary>
        ///     列出的文件类型
        /// </summary>
        [JsonProperty("fileManagerAllowFiles", NullValueHandling = NullValueHandling.Ignore)]
        public string[] FileManagerAllowFiles =
        {
            ".png", ".jpg", ".jpeg", ".gif", ".bmp",
            ".flv", ".swf", ".mkv", ".avi", ".rm", ".rmvb", ".mpeg", ".mpg",
            ".ogg", ".ogv", ".mov", ".wmv", ".mp4", ".webm", ".mp3", ".wav", ".mid",
            ".rar", ".zip", ".tar", ".gz", ".7z", ".bz2", ".cab", ".iso",
            ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx", ".pdf", ".txt", ".md", ".xml"
        };

        /// <summary>
        ///     指定要列出文件的目录
        /// </summary>
        public string FileManagerListPath = "upload/file";

        /// <summary>
        ///     每次列出文件数量
        /// </summary>
        [JsonProperty("fileManagerListSize", NullValueHandling = NullValueHandling.Ignore)]
        public int FileManagerListSize = 20;

        /// <summary>
        ///     文件访问路径前缀
        /// </summary>
        [JsonProperty("fileManagerUrlPrefix", NullValueHandling = NullValueHandling.Ignore)]
        public string FileManagerUrlPrefix = "";

        /// <summary>
        ///     上传大小限制，单位B，默认50MB
        /// </summary>
        [JsonProperty("fileMaxSize", NullValueHandling = NullValueHandling.Ignore)]
        public int FileMaxSize = 51200000;

        /// <summary>
        ///     上传保存路径,可以自定义保存路径和文件名格式
        /// </summary>
        public string FilePathFormat = "upload/file/{yyyy}{mm}{dd}/{time}{rand:6}";

        /// <summary>
        ///     文件访问路径前缀
        /// </summary>
        [JsonProperty("fileUrlPrefix", NullValueHandling = NullValueHandling.Ignore)]
        public string FileUrlPrefix = "";

        /* 上传图片配置项 */
        /// <summary>
        ///     执行上传图片的action名称
        /// </summary>
        [JsonProperty("imageActionName", NullValueHandling = NullValueHandling.Ignore)]
        public string ImageActionName = "uploadimage";

        /// <summary>
        ///     上传图片格式显示
        /// </summary>
        [JsonProperty("imageAllowFiles", NullValueHandling = NullValueHandling.Ignore)]
        public string[] ImageAllowFiles = {".png", ".jpg", ".jpeg", ".gif", ".bmp"};

        /// <summary>
        ///     图片压缩最长边限制
        /// </summary>
        [JsonProperty("imageCompressBorder", NullValueHandling = NullValueHandling.Ignore)]
        public int ImageCompressBorder = 1600;

        /// <summary>
        ///     是否压缩图片,默认是true
        /// </summary>
        [JsonProperty("imageCompressEnable", NullValueHandling = NullValueHandling.Ignore)]
        public bool ImageCompressEnable = true;

        /// <summary>
        ///     提交的图片表单名称
        /// </summary>
        [JsonProperty("imageFieldName", NullValueHandling = NullValueHandling.Ignore)]
        public string ImageFieldName = "upfile";

        /// <summary>
        ///     插入的图片浮动方式
        /// </summary>
        [JsonProperty("imageInsertAlign", NullValueHandling = NullValueHandling.Ignore)]
        public string ImageInsertAlign = "none";

        /* 列出指定目录下的图片 */
        /// <summary>
        ///     执行图片管理的action名称
        /// </summary>
        [JsonProperty("imageManagerActionName", NullValueHandling = NullValueHandling.Ignore)]
        public string ImageManagerActionName = "listimage";

        /// <summary>
        ///     列出的文件类型
        /// </summary>
        [JsonProperty("imageManagerAllowFiles", NullValueHandling = NullValueHandling.Ignore)]
        public string[] ImageManagerAllowFiles = {".png", ".jpg", ".jpeg", ".gif", ".bmp"};

        /// <summary>
        ///     插入的图片浮动方式
        /// </summary>
        [JsonProperty("imageManagerInsertAlign", NullValueHandling = NullValueHandling.Ignore)]
        public string ImageManagerInsertAlign = "none";

        /// <summary>
        ///     指定要列出图片的目录
        /// </summary>
        public string ImageManagerListPath = "";

        /// <summary>
        ///     每次列出文件数量
        /// </summary>
        [JsonProperty("imageManagerListSize", NullValueHandling = NullValueHandling.Ignore)]
        public int ImageManagerListSize = 20;

        /// <summary>
        ///     图片访问路径前缀
        /// </summary>
        [JsonProperty("imageManagerUrlPrefix", NullValueHandling = NullValueHandling.Ignore)]
        public string ImageManagerUrlPrefix = "";

        /// <summary>
        ///     上传大小限制，单位B
        /// </summary>
        [JsonProperty("imageMaxSize", NullValueHandling = NullValueHandling.Ignore)]
        public int ImageMaxSize = 2048000;

        /* {filename} 会替换成原文件名,配置这项需要注意中文乱码问题 */
        /* {rand:6} 会替换成随机数,后面的数字是随机数的位数 */
        /* {time} 会替换成时间戳 */
        /* {yyyy} 会替换成四位年份 */
        /* {yy} 会替换成两位年份 */
        /* {mm} 会替换成两位月份 */
        /* {dd} 会替换成两位日期 */
        /* {hh} 会替换成两位小时 */
        /* {ii} 会替换成两位分钟 */
        /* {ss} 会替换成两位秒 */
        /* 非法字符 \ : * ? " < > | */
        /* 具请体看线上文档: fex.baidu.com/ueditor/#use-format_upload_filename */
        /// <summary>
        ///     上传保存路径,可以自定义保存路径和文件名格式
        /// </summary>
        public string ImagePathFormat = "upload/image/{yyyy}{mm}{dd}/{time}{rand:6}";

        /// <summary>
        ///     图片访问路径前缀
        /// </summary>
        [JsonProperty("imageUrlPrefix", NullValueHandling = NullValueHandling.Ignore)]
        public string ImageUrlPrefix = "";

        /* 涂鸦图片上传配置项 */
        /// <summary>
        ///     执行上传涂鸦的action名称
        /// </summary>
        [JsonProperty("scrawlActionName", NullValueHandling = NullValueHandling.Ignore)]
        public string ScrawlActionName = "uploadscrawl";

        /// <summary>
        ///     提交的图片表单名称
        /// </summary>
        [JsonProperty("scrawlFieldName", NullValueHandling = NullValueHandling.Ignore)]
        public string ScrawlFieldName = "upfile";

        [JsonProperty("scrawlInsertAlign", NullValueHandling = NullValueHandling.Ignore)]
        public string ScrawlInsertAlign = "none";

        /// <summary>
        ///     上传大小限制，单位B
        /// </summary>
        [JsonProperty("scrawlMaxSize", NullValueHandling = NullValueHandling.Ignore)]
        public int ScrawlMaxSize = 2048000;

        /// <summary>
        ///     上传保存路径,可以自定义保存路径和文件名格式
        /// </summary>
        public string ScrawlPathFormat = "upload/image/{yyyy}{mm}{dd}/{time}{rand:6}";

        /// <summary>
        ///     图片访问路径前缀
        /// </summary>
        [JsonProperty("scrawlUrlPrefix", NullValueHandling = NullValueHandling.Ignore)]
        public string ScrawlUrlPrefix = "";

        /* 截图工具上传 */
        /// <summary>
        ///     执行上传截图的action名称
        /// </summary>
        [JsonProperty("snapscreenActionName", NullValueHandling = NullValueHandling.Ignore)]
        public string SnapscreenActionName = "uploadimage";

        /// <summary>
        ///     插入的图片浮动方式
        /// </summary>
        [JsonProperty("snapscreenInsertAlign", NullValueHandling = NullValueHandling.Ignore)]
        public string SnapscreenInsertAlign = "none";

        /// <summary>
        ///     上传保存路径,可以自定义保存路径和文件名格式
        /// </summary>
        public string SnapscreenPathFormat = "upload/image/{yyyy}{mm}{dd}/{time}{rand:6}";

        /// <summary>
        ///     图片访问路径前缀
        /// </summary>
        [JsonProperty("snapscreenUrlPrefix", NullValueHandling = NullValueHandling.Ignore)]
        public string SnapscreenUrlPrefix = "";

        /* 上传视频配置 */
        /// <summary>
        ///     执行上传视频的action名称
        /// </summary>
        [JsonProperty("videoActionName", NullValueHandling = NullValueHandling.Ignore)]
        public string VideoActionName = "uploadvideo";

        [JsonProperty("videoAllowFiles", NullValueHandling = NullValueHandling.Ignore)]
        public string[] VideoAllowFiles =
        {
            ".flv", ".swf", ".mkv", ".avi", ".rm", ".rmvb", ".mpeg", ".mpg",
            ".ogg", ".ogv", ".mov", ".wmv", ".mp4", ".webm", ".mp3", ".wav", ".mid"
        }; /* 上传视频格式显示 */

        /// <summary>
        ///     提交的视频表单名称
        /// </summary>
        [JsonProperty("videoFieldName", NullValueHandling = NullValueHandling.Ignore)]
        public string VideoFieldName = "upfile";

        /// <summary>
        ///     上传大小限制，单位B，默认100MB
        /// </summary>
        [JsonProperty("videoMaxSize", NullValueHandling = NullValueHandling.Ignore)]
        public int VideoMaxSize = 102400000;

        /// <summary>
        ///     上传保存路径,可以自定义保存路径和文件名格式
        /// </summary>
        public string VideoPathFormat = "upload/video/{yyyy}{mm}{dd}/{time}{rand:6}";

        /// <summary>
        ///     视频访问路径前缀
        /// </summary>
        [JsonProperty("videoUrlPrefix", NullValueHandling = NullValueHandling.Ignore)]
        public string VideoUrlPrefix = "";
        
    }
}