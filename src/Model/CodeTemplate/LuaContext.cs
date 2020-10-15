using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder.CodeTemplate.LuaTemplate
{
    public class LuaContext : CoderBase
    {
        public EntityConfig Entity { get; set; }
        public EntityConfig Project { get; set; }
        public EntityConfig Soluction { get; set; }

        public EntityConfig GetEntity()
        {
            return Entity;
        }
    }
}
