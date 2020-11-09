namespace Agebull.EntityModel.Config
{
    public interface IApiFieldConfig : IConfig
    {
        /// <summary>
        /// 字段名称(ApiArgument)
        /// </summary>
        string ApiArgumentName
        {
            get;
            set;
        }
        /// <summary>
        /// 不参与Json序列化
        /// </summary>
        bool NoneJson
        {
            get;
            set;
        }
        /// <summary>
        /// 字段名称(json)
        /// </summary>
        string JsonName
        {
            get;
            set;
        }
        /// <summary>
        /// 示例内容
        /// </summary>
        string HelloCode
        {
            get;
            set;
        }
    }
}