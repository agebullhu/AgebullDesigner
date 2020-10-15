using Newtonsoft.Json;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 数据字典
    /// </summary>
    [JsonObject]
    public class WordItem : NotificationObject
    {
        private string english;
        private string chiness;
        private string description;

        /// <summary>
        /// 英文
        /// </summary>
        [JsonProperty("en")]
        public string English
        {
            get => english;
            set
            {
                if (english == value)
                    return;
                english = value;
                RaisePropertyChanged(nameof(English));
            }
        }

        /// <summary>
        /// 中文
        /// </summary>
        [JsonProperty("cn")]
        public string Chiness
        {
            get => chiness;
            set
            {
                if (chiness == value)
                    return;
                chiness = value;
                RaisePropertyChanged(nameof(Chiness));
            }
        }

        /// <summary>
        /// 说明
        /// </summary>
        [JsonProperty("des")]
        public string Description
        {
            get => description;
            set
            {
                if (description == value)
                    return;
                description = value;
                RaisePropertyChanged(nameof(Description));
            }
        }
    }
}
