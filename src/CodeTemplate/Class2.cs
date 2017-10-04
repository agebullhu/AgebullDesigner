using Agebull.Common.SimpleDesign;
using Gboxt.Common.DataAccess.Schemas;

namespace CodeTemplate
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
