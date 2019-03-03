using Microsoft.AspNetCore.Http;

namespace Agebull.ZeroNet.ManageApplication
{

    /// <summary>
    /// Handler 的摘要说明
    /// </summary>
    internal abstract class Handler
    {
        public void SetContext(HttpContext context)
        {
            Request = context.Request;
            Response = context.Response;
            Context = context;
        }

        public abstract void Process();
        public object Result { get; protected set; }


        public HttpRequest Request { get; private set; }
        public HttpResponse Response { get; private set; }
        public HttpContext Context { get; private set; }
    }
}