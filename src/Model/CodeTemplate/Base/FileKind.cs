namespace Agebull.CodeRefactor
{
    /// <summary>
    /// 文件种类
    /// </summary>

    public enum FileKind
    {
        /// <summary>
        /// 文本文件
        /// </summary>
        TextFile,
        /// <summary>
        /// 工程定义文件
        /// </summary>
        SolutionFile,
        /// <summary>
        /// 项目定义文件
        /// </summary>
        ProjectFile,
        /// <summary>
        /// 代码文件
        /// </summary>
        CodeFile,
        /// <summary>
        ///  XML文件
        /// </summary>
        XmlFile,
        /// <summary>
        /// 二进制文件
        /// </summary>
        BinaryFile,
        /// <summary>
        /// 程序集文件
        /// </summary>
        AssemblyFile,
        /// <summary>
        /// 图片文件
        /// </summary>
        ImageFile
    }
}