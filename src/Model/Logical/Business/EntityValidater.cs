﻿namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// 实体配置校验器
    /// </summary>
    public class EntityValidater : ConfigValidaterBase
    {
        /// <summary>
        /// 表结构对象
        /// </summary>
        public EntityConfig Entity { get; set; }


        /// <summary>
        ///     数据校验
        /// </summary>
        protected override bool Validate()
        {
            var result = true;
            Message.Message2 = Entity.Name;
            if (string.IsNullOrWhiteSpace(Entity.Name))
            {
                result = false;
                Message.Track = "***实体名称不能为空";
            }
            if (!Entity.EnableDataBase && !Entity.IsReference)
            {
                var pc = Entity.PrimaryColumn;
                if (pc == null)
                {
                    //result = false;
                    Message.Track = "***没有主键字段";
                }
                else if (pc.IsDiscard)
                {
                    //result = false;
                    Message.Track = "***主键字段被设置为过时";
                }
                if (!string.IsNullOrWhiteSpace(Entity.DataTable.SaveTableName))
                {
                    Entity.DataTable.SaveTableName = Entity.DataTable.SaveTableName.Trim();
                }
                if (string.IsNullOrWhiteSpace(Entity.DataTable.ReadTableName))
                {
                    result = false;
                    Message.Track = "***实体存储名称不能为空";
                }
                else
                {
                    Entity.DataTable.ReadTableName = Entity.DataTable.ReadTableName.Trim();
                }
            }
            foreach (var col in Entity.Properties)
            {
                col.Entity = Entity;
                if (col.IsDiscard)
                {
                    continue;
                }
                Message.Message3 = $"=>{col.Caption}:{col.Name}";
                var model = new PropertyValidater
                {
                    Property = col,
                    DataBaseType = Entity.Project.DbType
                };
                if (!model.Validate(Message))
                {
                    result = false;
                }
            }
            return result;
        }
    }
}


