using System;
using System.Diagnostics;
using Agebull.Common.Ioc;
using Agebull.Common.Logging;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Agebull.ZeroNet.ManageApplication.Controllers
{
    [ApiController]
    public class UEditorController : PageBase
    {
        [Route("api/v1/ueditor/action")]
        public void OnAction()
        {
            try
            {
                _action = GetArg("action");
                using (IocScope.CreateScope())
                {
                    OnPageLoaded();
                }
            }
            catch (Exception exception)
            {
                LogRecorder.Exception(exception);
                _result = new
                {
#if DEBUG
                    state = exception.Message,
#else
                    state = "系统错误",
#endif
                    url = "/Images/Error.png",
                    title = "错误",
                    original = "error.png",
                    error = "0x3"
                };
            }
            OnResult();
        }

        private string _action;
        private object _result;

        protected void OnResult()
        {
            string json = _result == null ? "{}" : JsonConvert.SerializeObject(_result);
            //if (action == "config")
            //{
            //    var path = Path.Combine(Request.MapPath("~"), "ueditor", "config.json");
            //    if (File.Exists(path))
            //    {
            //        json = File.ReadAllText(path);
            //        Response.AddHeader("Content-Type", "text/json");
            //        Response.Write(json);
            //        return;
            //    }
            //}
            Debug.WriteLine(json);
            string jsonpCallback = GetArg("callback");
            if (string.IsNullOrWhiteSpace(jsonpCallback))
            {
                Response.Headers["Content-Type"] = "text/plain";
                var bytes = json.ToUtf8Bytes();
                Response.Body.Write(bytes, 0, bytes.Length);
            }
            else
            {
                Response.Headers["Content-Type"] = "application/javascript";
                var bytes = $"{jsonpCallback}({json});".ToUtf8Bytes();
                Response.Body.Write(bytes, 0, bytes.Length);
            }
        }

        private Handler handler;
        protected void OnPageLoaded()
        {
            switch (_action)
            {
                case "config":
                    _result = UEditorConfig.Default;
                    OnResult();
                    return;
                default:
                    _result = new
                    {
                        state = "action 参数为空或者 action 不被支持。"
                    };
                    OnResult();
                    return;
                case "uploadimage":
                    handler = new UploadHandler(new UploadConfig()
                    {
                        AllowExtensions = UEditorConfig.Default.ImageAllowFiles,
                        PathFormat = UEditorConfig.Default.ImagePathFormat,
                        SizeLimit = UEditorConfig.Default.ImageMaxSize,
                        UploadFieldName = UEditorConfig.Default.ImageFieldName
                    });
                    break;
                case "uploadscrawl":
                    handler = new UploadHandler(new UploadConfig()
                    {
                        AllowExtensions = new string[] { ".png" },
                        PathFormat = UEditorConfig.Default.ScrawlPathFormat,
                        SizeLimit = UEditorConfig.Default.ScrawlMaxSize,
                        UploadFieldName = UEditorConfig.Default.ScrawlFieldName,
                        Base64 = true,
                        Base64Filename = "scrawl.png"
                    });
                    break;
                case "uploadvideo":
                    handler = new UploadHandler(new UploadConfig()
                    {
                        AllowExtensions = UEditorConfig.Default.VideoAllowFiles,
                        PathFormat = UEditorConfig.Default.VideoPathFormat,
                        SizeLimit = UEditorConfig.Default.VideoMaxSize,
                        UploadFieldName = UEditorConfig.Default.VideoFieldName
                    });
                    break;
                case "uploadfile":
                    handler = new UploadHandler(new UploadConfig()
                    {
                        AllowExtensions = UEditorConfig.Default.FileAllowFiles,
                        PathFormat = UEditorConfig.Default.FilePathFormat,
                        SizeLimit = UEditorConfig.Default.FileMaxSize,
                        UploadFieldName = UEditorConfig.Default.FileFieldName
                    });
                    break;
                case "listimage":
                    handler = new ListFileManager(AnnexType.Image, GetIntArg("start", 0),
                        GetIntArg("size", UEditorConfig.Default.FileManagerListSize));
                    break;
                case "listfile":
                    handler = new ListFileManager(AnnexType.None, GetIntArg("start", 0),
                        GetIntArg("size", UEditorConfig.Default.FileManagerListSize));
                    break;
                case "catchimage":
                    _result = new
                    {
                        state = "系统错误",
                        url = "/Images/Error.png",
                        title = "错误",
                        original = "error.png",
                        error = "0x3"
                    };
                    return;
                //handler = new CrawlerHandler();
                //break;
                case "base64":
                    handler = new Base64File(GetArg("url"));
                    break;
            }
            handler.SetContext(HttpContext);
            handler.Process();
            _result = handler.Result;
        }
    }
}