namespace Agebull.OAuth.Contract
{
    /// <summary>
    /// 令牌信息节点
    /// </summary>
    public class AuthTokenItem
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// 设备标识
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