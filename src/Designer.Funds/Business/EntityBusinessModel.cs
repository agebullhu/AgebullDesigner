using System;
using System.Text;

namespace Agebull.EntityModel.Config
{
    internal class EntityBusinessModel : ConfigModelBase
    {
        /// <summary>
        /// 表结构对象
        /// </summary>
        public EntityConfig Entity { get; set; }


        #region CheckDouble
        /// <summary>
        /// 修复文本长度
        /// </summary>
        public void CheckDouble()
        {
            if (Entity.IsReference)
                return;
            //EntityConfig friend = GetEntity(p => p != Entity && p.Tag == Entity.Tag);
            //if (friend == null)
            //    return;
            foreach (var col in Entity.Properties)
            {
                PropertyBusinessModel model = new PropertyBusinessModel { Property = col };
                model.CheckDouble();
                col.IsModify = true;
            }
            Entity.IsModify = true;
        }
        #endregion

        #region RepairByArrayLen
        /// <summary>
        /// 修复文本长度
        /// </summary>
        public void RepairByArrayLen()
        {
            foreach (var col in Entity.Properties)
            {
                PropertyBusinessModel model = new PropertyBusinessModel { Property = col };
                model.RepairByArrayLen();
                col.IsModify = true;
            }
            Entity.IsModify = true;
        }
        #endregion


        #region RepairCpp

        /// <summary>
        ///     自动修复(从模型修复数据存储)
        /// </summary>
        public void RepairCpp(bool repair = false)
        {
            if (Entity.IsFreeze)
                return;
            if (Entity.IsReference)
            {
                if (repair)
                    foreach (var col in Entity.Properties)
                        col.CppLastType = CppTypeHelper.CppLastType(col.CppType);
                return;
            }
            RepairEsName(repair);

            EntityConfig friend = GetEntity(p => p != Entity && p.Tag == Entity.Tag);

            PropertyBusinessModel model = new PropertyBusinessModel();
            foreach (var col in Entity.Properties)
            {
                if (col.Discard || Entity.IsFreeze)
                {
                    continue;
                }
                col.Parent = Entity;
                model.Property = col;
                model.RepairCpp(repair, friend);
                col.IsModify = true;
            }
            Entity.IsModify = true;
        }


        private void RepairEsName(bool repair)
        {
            if (Entity.CppName != null && !repair)
                return;

            if (Entity.OldNames.Contains(Entity.CppName))
                Entity.OldNames.Add(Entity.CppName);
            //Entity.Tag = "ES3.0," + Entity.CppName;

            if (string.IsNullOrWhiteSpace(Entity.Name))
                return;
            var name = Entity.Name;
            if (name.IndexOf("TEs", StringComparison.Ordinal) == 0)
                name = name.Substring(3);
            if (name.IndexOf("TapAPI", StringComparison.Ordinal) == 0)
                name = name.Substring(6);
            if (name.Length > 5 && name.IndexOf("Field", StringComparison.Ordinal) == name.Length - 5)
                name = name.Substring(0, name.Length - 5);
            if (name.Length > 3 && name.IndexOf("Req", StringComparison.Ordinal) == name.Length - 3)
                name = name.Substring(0, name.Length - 3) + "Arg";
            if (name.Length > 3 && name.IndexOf("QryRsp", StringComparison.Ordinal) == name.Length - 3)
                name = name.Substring(0, name.Length - 3) + "Item";
            else
                name = name.Replace("Qry", "Query");

            Entity.Name = name.Trim(NoneNameChar).ToUWord();
        }

        #endregion
    }
}