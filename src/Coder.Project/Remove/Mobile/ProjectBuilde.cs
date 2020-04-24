using System.ComponentModel.Composition;
using Agebull.Common;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;

namespace Agebull.EntityModel.RobotCoder.Mobile
{
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal sealed class MobileProjectBuilde : ProjectBuilder, IAutoRegister
    {
        /// <summary>
        /// 名称
        /// </summary>
        protected override string Name => "Mobile";

        /// <summary>
        /// 标题
        /// </summary>
        public override string Caption => Name;
        /// <summary>
        /// 执行自动注册
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            RegistBuilder<MobileProjectBuilde>();
        }

        /// <summary>
        /// 生成项目代码
        /// </summary>
        /// <param name="project"></param>
        public override void CreateProjectCode(ProjectConfig project)
        {
            var code = new MobileGlobalCoder();
            code.CreateBaseCode(project.MobileCsPath);
        }

        /// <summary>
        /// 生成实体代码
        /// </summary>
        /// <param name="project"></param>
        /// <param name="schema"></param>
        public override void CreateEntityCode(ProjectConfig project, EntityConfig schema)
        {
            IOHelper.CheckPath(schema.Parent.MobileCsPath);
            var builder = new MobileEntityCoder
            {
                Project = schema.Parent,
                Entity = schema
            };
            builder.CreateBaseCode(schema.Parent.MobileCsPath);
            builder.CreateExtendCode(schema.Parent.MobileCsPath);
        }
    }
}
