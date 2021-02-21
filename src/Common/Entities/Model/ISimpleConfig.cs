namespace Agebull.EntityModel.Config
{
    /// <summary>
    ///     配置基础
    /// </summary>
    public interface ISimpleConfig : IKey, IModifyObject
    {
        /// <summary>
        ///     名称
        /// </summary>
        string Name
        {
            get;
            set;
        }

        /// <summary>
        ///     标题
        /// </summary>
        string Caption
        {
            get;
            set;
        }

        /// <summary>
        ///     说明
        /// </summary>
        string Description
        {
            get;
            set;
        }

        /// <summary>
        /// 参见
        /// </summary>
        string Remark
        {
            get;
            set;
        }

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest"></param>
        /// <returns></returns>
        void CopyConfig(ISimpleConfig dest)
        {
            using (WorkModelScope.CreateScope(WorkModel.Loding))
            {
                CopyFrom(dest);
            }
        }

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest"></param>
        /// <returns></returns>
        void Copy(ISimpleConfig dest)
        {
            using (WorkModelScope.CreateScope(WorkModel.Loding))
            {
                Name = dest.Name;
                Caption = dest.Caption;
                Description = dest.Description;
                Remark = dest.Remark;
                CopyFrom(dest);
            }
        }

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest"></param>
        /// <returns></returns>
        void CopyFrom(ISimpleConfig dest)
        {
        }
    }
}