namespace Agebull.EntityModel.Config
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
            else
            {
                Entity.Name = Entity.Name.Trim();
            }
            if (!Entity.IsClass && !Entity.IsReference)
            {
                var pc = Entity.PrimaryColumn;
                if (pc == null)
                {
                    //result = false;
                    Message.Track = "***没有主键字段";
                }
                else if (pc.Discard)
                {
                    //result = false;
                    Message.Track = "***主键字段被设置为过时";
                }
                if (!string.IsNullOrWhiteSpace(Entity.SaveTableName))
                {
                    Entity.SaveTableName = Entity.SaveTableName.Trim();
                }
                if (string.IsNullOrWhiteSpace(Entity.ReadTableName))
                {
                    result = false;
                    Message.Track = "***实体存储名称不能为空";
                }
                else
                {
                    Entity.ReadTableName = Entity.ReadTableName.Trim();
                }
            }
            foreach (var col in Entity.Properties)
            {
                col.Parent = Entity;
                if (col.Discard)
                {
                    continue;
                }
                Message.Message3 = $"=>{col.Caption}:{col.Name}";
                PropertyValidater model = new PropertyValidater {Property = col};
                if (!model.Validate(Message))
                {
                    result = false;
                }
            }
            return result;
        }
    }
}


