/*design by:agebull designer date:2017/5/31 16:12:03*/

using System;
using System.IO;
using System.Threading;
using Agebull.Common;
using Agebull.EntityModel.MySql.BusinessLogic;
using Agebull.EntityModel.Common;
using Agebull.ZeroNet.ManageApplication.DataAccess;

namespace Agebull.ZeroNet.ManageApplication
{
    /// <summary>
    /// 附件
    /// </summary>
    public sealed partial class AnnexBusinessLogic : BusinessLogicByStateData<AnnexData, AnnexDataAccess, AnnexDb>
    {

        #region 保存文件
        /// <summary>
        ///     被用户编辑的数据的保存前操作
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="isAdd">是否为新增</param>
        /// <returns>如果为否将阻止后续操作</returns>
        protected override bool PrepareSaveByUser(AnnexData data, bool isAdd)
        {
            if (string.IsNullOrWhiteSpace(data.Title))
                data.Title = data.FileName;
            SaveFile(data);
            return base.PrepareSaveByUser(data, isAdd);
        }

        /// <summary>
        /// 锁定对象
        /// </summary>
        private static readonly object LockKey = new object();

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="data"></param>
        private static void SaveFile(AnnexData data)
        {
            if (data.Buffer == null)
                return;
            var ext = Path.GetExtension(data.FileName);
            if (string.IsNullOrWhiteSpace(ext))
            {
                data.AnnexType = AnnexType.None;
            }
            else
            {
                switch (ext.ToLower())
                {
                    case ".doc":
                    case ".docx":
                        data.AnnexType = AnnexType.Wrod;
                        break;
                    case ".xls":
                    case ".xlsx":
                        data.AnnexType = AnnexType.Excel;
                        break;
                    case ".ppt":
                    case ".pptx":
                        data.AnnexType = AnnexType.Ppt;
                        break;
                    case ".pdf":
                        data.AnnexType = AnnexType.Pdf;
                        break;
                    case ".wps":
                        data.AnnexType = AnnexType.WPS;
                        break;
                    case ".txt":
                        data.AnnexType = AnnexType.Text;
                        break;
                    case ".jpg":
                    case ".jpeg":
                    case ".png":
                    case ".gif":
                        data.AnnexType = AnnexType.Image;
                        break;
                    case ".wav":
                    case ".wma":
                    case ".mp3":
                    case ".ra":
                    case ".midi":
                    case ".ogg":
                    case ".ape":
                    case ".flac":
                    case ".aac":
                        data.AnnexType = AnnexType.Audio;
                        break;
                    case ".mp4":
                    case ".rm":
                    case ".rmvb":
                    case ".3gp":
                    case ".mpeg":
                    case ".mpg":
                    case ".wmv":
                    case ".avi":
                    case ".mov":
                    case ".mtv":
                        data.AnnexType = AnnexType.Video;
                        break;
                    default:
                        ext = ".bin";
                        data.AnnexType = AnnexType.None;
                        break;
                }
            }
            SaveFile(data, ext);


        }


        /// <summary>
        /// 保存文件
        /// </summary>
        private static void SaveFile(AnnexData data, string ext)
        {
            var folder = IOHelper.CheckPath(Environment.CurrentDirectory, "wwwroot", "Anuexs", data.AnnexType.ToString());
            string file;
            if (data.Storage != null)
            {
                file = Path.Combine(folder, data.Storage);
                if (File.Exists(file))
                    File.Delete(file);
            }
            //生成不重复的随机名
            string name;
            try
            {
                Monitor.Enter(LockKey);
                do
                {
                    name = RandomOperate.Generate(10);
                    file = Path.Combine(folder, name + ext);
                }
                while (File.Exists(file));
                File.WriteAllBytes(file, data.Buffer);
            }
            finally
            {
                Monitor.Exit(LockKey);
            }
            data.Url = $"/Anuexs/{data.AnnexType}/{name}{ext}";
            data.Storage = $"Anuexs\\{data.AnnexType}\\{name}{ext}";
        }

        #endregion

        #region 同步删除
        /// <summary>
        ///     删除对象后置处理
        /// </summary>
        protected override bool DoDelete(long id)
        {
            var storage = Access.LoadValue(p => p.Storage, id);
            string file = IOHelper.CheckPath(Environment.CurrentDirectory, "wwwroot", storage);
            if (File.Exists(file))
            {
                File.Delete(file);
            }
            return base.DoDelete(id);
        }

        #endregion

    }
}