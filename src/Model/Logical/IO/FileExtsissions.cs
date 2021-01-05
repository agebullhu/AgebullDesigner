using System.IO;

namespace Agebull.Common
{
    public static class FileExtsissions
    {
        /// <summary>
        /// 不覆盖地复制文件
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        public static void FileCopyTo(this string source, string dest)
        {
            if (File.Exists(dest))
                return;
            File.Copy(source, dest);
        }
    }
}