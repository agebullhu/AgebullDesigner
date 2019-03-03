using System;
using System.IO;
using Agebull.Common.Configuration;
using Agebull.Common.Logging;
using Gboxt.Common.DataModel;
using Agebull.ZeroNet.ManageApplication.DataAccess;

namespace Agebull.ZeroNet.ManageApplication
{
    /// <summary>
    /// FileManager 的摘要说明
    /// </summary>
    internal class Base64File : Handler
    {
        private readonly string _url;

        public Base64File(string url)
        {
            _url = url;
        }

        public override void Process()
        {
            if (string.IsNullOrEmpty(_url))
            {
                Result = ApiResult.ArgumentError;
                return;
            }
            var access = new AnnexDataAccess();
            AnnexData annex;
            try
            {
                //var convert = access.Compile(lambda);
                annex = access.First(p => p.Url == _url);
            }
            catch (Exception ex)
            {
                LogRecorder.Exception(ex);
                Result = ApiResult.ArgumentError;
                return;
            }
            if (annex == null)
            {
                Result = ApiResult.ArgumentError;
                return;
            }
            try
            {

                var file = Path.Combine(ConfigurationManager.BasePath, "wwwroot", annex.Storage.Replace('\\','/'));
                if (!File.Exists(file))
                {
                    access.DeletePrimaryKey(annex.ID);
                    Result = ApiResult.ArgumentError;
                    return;
                }

                var binary = File.ReadAllBytes(file);
                if (binary == null || binary.Length == 0)
                {
                    access.DeletePrimaryKey(annex.ID);
                    Result = ApiResult.ArgumentError;
                    return;
                }
                Result = ApiResult.Succees(Convert.ToBase64String(binary));
            }
            catch (Exception ex)
            {
                LogRecorder.Exception(ex);
                Result = ApiResult.ArgumentError;
            }
        }
    }
}