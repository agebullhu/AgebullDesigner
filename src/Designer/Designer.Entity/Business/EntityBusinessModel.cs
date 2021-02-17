using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// 实体配置业务模型
    /// </summary>
    internal class EntityBusinessModel : ConfigModelBase
    {
        /// <summary>
        /// 表结构对象
        /// </summary>
        public IEntityConfig Entity { get; set; }

        #region 修复
        /// <summary>
        ///     自动修复(从模型修复数据存储)
        /// </summary>
        public void RepairRegular(bool repair)
        {
            if (Entity.IsFreeze)
                return;
            foreach (var perperty in Entity.Properties)
            {
                if (!perperty.IsActive)
                {
                    continue;
                }
                if (perperty.IsPrimaryKey)
                {
                    perperty.Nullable = false;
                    perperty.DataBaseField.DbNullable = false;
                    perperty.DataBaseField.KeepStorageScreen = StorageScreenType.Update;
                }
                if (perperty.IsIdentity || perperty.IsSystemField)
                    perperty.IsUserReadOnly = true;
                if (perperty.Nullable)
                    perperty.DataBaseField.DbNullable = true;
                if (perperty.IsCaption)
                    perperty.CanEmpty = false;
                if (perperty.DataBaseField.LinkTable != null)
                {
                    perperty.IsUserReadOnly = perperty.DataBaseField.IsLinkKey;
                    perperty.CanEmpty = true;
                }
                if (perperty.DataBaseField.IsText || perperty.DataBaseField.IsBlob)
                    perperty.CanEmpty = true;
                if (!repair)
                    continue;
                switch (perperty.Name)
                {
                    case "ParentId":
                    case "AuthorID":
                    case "AddDate":
                        perperty.Nullable = false;
                        //col.DbNullable = false;
                        perperty.IsUserReadOnly = true;
                        perperty.DataBaseField.KeepStorageScreen = StorageScreenType.Update;
                        break;
                }
            }
        }
        #endregion

    }
}


