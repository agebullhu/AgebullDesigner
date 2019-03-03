namespace Agebull.OAuth.Contract
{
    /// <summary>
    /// ������Ϣ�ڵ�
    /// </summary>
    public class AuthTokenItem
    {
        /// <summary>
        /// �û�ID
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// �豸��ʶ
        /// </summary>
        public string DeviceId { get; set; }
        /// <summary>
        /// RT
        /// </summary>
        public string RefreshToken { get; set; }
        /// <summary>
        /// AT
        /// </summary>
        public string AccessToken { get; set; }
    }
}