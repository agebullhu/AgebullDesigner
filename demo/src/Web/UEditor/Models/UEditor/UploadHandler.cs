using System;
using System.IO;
using System.Linq;
using Agebull.Common.Logging;
namespace Agebull.ZeroNet.ManageApplication
{

    public class UploadResult
    {
        public UploadState State { get; set; }
        public string Url { get; set; }
        public string OriginFileName { get; set; }

        public string ErrorMessage { get; set; }
    }

    public enum UploadState
    {
        Success = 0,
        SizeLimitExceed = -1,
        TypeNotAllow = -2,
        FileAccessError = -3,
        NetworkError = -4,
        Unknown = 1,
    }
    /// <summary>
    /// UploadHandler 的摘要说明
    /// </summary>
    internal class UploadHandler : Handler
    {
        private readonly UploadConfig _uploadConfig;

        public UploadHandler(UploadConfig config)
        {
            _uploadConfig = config;
        }

        public override void Process()
        {
            //if (UploadConfig.Base64)
            //{
            //    uploadFileName = UploadConfig.Base64Filename;
            //    uploadFileBytes = Convert.FromBase64String(Request[UploadConfig.UploadFieldName]);
            //}

            try
            {
                var task = Request.ReadFormAsync();
                task.Wait();
                
                var file = task.Result.Files[_uploadConfig.UploadFieldName];

                if (file == null || !CheckFileType(file.FileName))
                {
                    Result = new
                    {
                        state = "不允许的文件格式",
                        url = "/Images/Error.png",
                        title = "错误",
                        original = "error.png",
                        error = "0x1"
                    };
                    return;
                }
                if (!CheckFileSize(file.Length))
                {
                    Result = new
                    {
                        state = "文件大小超出服务器限制",
                        url = "/Images/Error.png",
                        title = "错误",
                        original = "error.png",
                        error = "0x2"
                    };
                    return;
                }
                AnnexData data = new AnnexData
                {
                    FileName = file.FileName,
                    Title = file.FileName,
                    __IsFromUser =true,
                    Buffer = new byte[file.Length]
                };
                using (var stream = file.OpenReadStream())
                {
                    stream.Read(data.Buffer, 0, data.Buffer.Length);
                }
                var bl = new AnnexBusinessLogic();
                bl.AddNew(data);

                Result = new
                {
                    state = "SUCCESS",
                    url = data.Url,
                    title = data.Title,
                    original = data.Url,
                    error = "0x0"
                };
            }
            catch(Exception exception)
            {
                LogRecorder.Exception(exception);
                Result = new
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
        }

        private bool CheckFileType(string filename)
        {
            var fileExtension = Path.GetExtension(filename)?.ToLower();
            return _uploadConfig.AllowExtensions.Select(x => x.ToLower()).Contains(fileExtension);
        }

        private bool CheckFileSize(long size)
        {
            return size < _uploadConfig.SizeLimit;
        }
    }

}

