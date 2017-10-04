namespace Agebull.EntityModel.Config
{
    public sealed partial class EntityConfig
    {
        #region ����ʱ

        /// <summary>
        ///     �����ı�
        /// </summary>
        /// <returns>
        ///     �����ı�
        /// </returns>
        public override string ToString()
        {
            return $"{Name}({Caption})";
        }
        
        #region �߼����
        
        /// <summary>
        ///     ����ֵ
        /// </summary>
        /// <param name="source">���Ƶ�Դ�ֶ�</param>
        /// <param name="noChilds">�Ƿ����Ӽ�(Ĭ��Ϊ��)</param>
        public void CopyValue(EntityConfig source, bool noChilds = false)
        {
            Caption = source.Caption + "(����)";
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