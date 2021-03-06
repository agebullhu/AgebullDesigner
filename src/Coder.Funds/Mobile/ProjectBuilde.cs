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
            GlobalConfig.CheckPath(schema.Parent.MobileCsPath);

            Message = "正在生成" + schema.Caption + "...";
            if (!schema.IsReference && !string.IsNullOrWhiteSpace(project.MobileCsPath))
            {
                var entityPath = GlobalConfig.CheckPath(project.MobileCsPath);
                var builder = new MobileEntityCoder
                {
                    Entity = schema,
                    Project = project
                };
                builder.CreateBaseCode(entityPath);
                builder.CreateExtendCode(entityPath);
            }
            if (!string.IsNullOrWhiteSpace(project.CppCodePath))
            {
                var cppPath = GlobalConfig.CheckPath(project.CppCodePath);
                var structCoder = new CppStructCoder
                {
                    Entity = schema,
                    Project = project
                };
                structCoder.CreateBaseCode(cppPath);
                structCoder.CreateExtendCode(cppPath);
                if (!schema.NoDataBase)
                {
                    var modelCoder = new CppModelCoder
                    {
                        Entity = schema,
                        Project = project
                    };
                    modelCoder.CreateBaseCode(cppPath);
                    modelCoder.CreateExtendCode(cppPath);
                    if (!schema.IsReference)
                    {
                        var accessCoder = new CppAccessCoder
                        {
                            Entity = schema,
                            Project = project
                        };
                        accessCoder.CreateBaseCode(cppPath);
                        accessCoder.CreateExtendCode(cppPath);
                    }
                }
            }

            Message = schema.Caption + "已完成";
        }
    }
}
