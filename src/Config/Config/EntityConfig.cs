namespace Agebull.EntityModel.Config
{
    public sealed partial class EntityConfig
    {
        #region 运行时

        /// <summary>
        ///     对象文本
        /// </summary>
        /// <returns>
        ///     对象文本
        /// </returns>
        public override string ToString()
        {
            return $"{Name}({Caption})";
        }
        
        #region 逻辑相关
        
        /// <summary>
        ///     复制值
        /// </summary>
        /// <param name="source">复制的源字段</param>
        /// <param name="noChilds">是否复制子级(默认为是)</param>
        public void CopyValue(EntityConfig source, bool noChilds = false)
        {
            Caption = source.Caption + "(复制)";
            Description = source.Description;
            Name = source.Name + "_copy";
            DataVersion = source.DataVersion;
            DbIndex = source.DbIndex;
            IsInternal = source.IsInternal;
            IsClass = source.IsClass;
            ReadTableName = source.ReadTableName;
            SaveTableName = source.SaveTableName;
            Classify = source.Classify;
            CppName = source.CppName;
            TreeUi = source.TreeUi;
            Tag = source.Tag + "," + source.Name;
            if (noChilds)
                return;
            foreach (var field in source.Properties)
            {
                var nf = new PropertyConfig();
                nf.CopyFrom(field);
                if (field.IsPrimaryKey)
                    nf.IsPrimaryKey = true;
                nf.Tag = $"{source.Tag},{source.Name},{field.CppType},{field.Name}";
                Properties.Add(nf);
            }
        }

        #endregion

        #endregion
    }
}