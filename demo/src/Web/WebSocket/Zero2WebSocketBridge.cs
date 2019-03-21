using System;
using Agebull.Common.Logging;
using Agebull.MicroZero.PubSub;

namespace Agebull.ZeroNet.WebSocket
{

    /// <summary>
    /// 内部ZeroNet的Notify广播到WebSocket的桥接
    /// </summary>
    public class Zero2WebSocketBridge : SubStation<PublishItem>
    {
        /// <summary>
        /// 构造
        /// </summary>
        public Zero2WebSocketBridge()
        {
            Name = "Summary";
            StationName = "Summary";
            IsRealModel = true;
            Subscribe = "";
        }

        /// <inheritdoc />
        /// <summary>执行命令</summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override void Handle(PublishItem item)
        {
            try
            {
                WebSocketNotify.Publish("mq", item.Title, item.SubTitle, item.Content);
            }
            catch (Exception ex)
            {
                LogRecorder.Exception(ex);
            }
        }
    }
}