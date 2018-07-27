using Newtonsoft.Json;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// �����û��Ĳ����ֳ�
    /// </summary>
    [JsonObject]
    public class UserScreen
    {
        /// <summary>
        /// ��ǰ���ҳ��
        /// </summary>
        [JsonProperty]
        public string NowEditor { get; set; }
        /// <summary>
        /// ������ͼ
        /// </summary>
        [JsonProperty]
        public string WorkView { get; set; }

        /// <summary>
        /// �߼��ӽ�
        /// </summary>
        public bool AdvancedView { get; set; }

        /// <summary>
        ///���һ�δ򿪵��ļ�
        /// </summary>
        public string LastFile { get; set; }
    }
}