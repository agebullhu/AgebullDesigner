using System.ComponentModel.Composition;
using Agebull.Common;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;

namespace Agebull.EntityModel.RobotCoder.Cpp
{
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal sealed class CppProjectBuilde : ProjectBuilder, IAutoRegister
    {
        /// <summary>
        /// 名称
        /// </summary>
        protected override string Name => "Cpp";

        /// <summary>
        /// 标题
        /// </summary>
        public override string Caption => Name;
        /// <summary>
        /// 执行自动注册
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            RegistBuilder<CppProjectBuilde>();
        }

        /// <summary>
        /// 生成项目代码
        /// </summary>
        /// <param name="project"></param>
        public override void CreateProjectCode(ProjectConfig project)
        {

        }

        /// <summary>
        /// 生成实体代码
        /// </summary>
        /// <param name="project"></param>
        /// <param name="schema"></param>
        public override void CreateEntityCode(ProjectConfig project, EntityConfig schema)
        {
            Message = "正在生成" + schema.Caption + "...";
            if (!schema.IsReference)
            {
                var entityPath = IOHelper.CheckPath(project.ClientCsPath);
                var builder = new ClientEntityCoder
                {
                    Entity = schema,
                    Project = project
                };
                builder.CreateBaseCode(entityPath);
                builder.CreateExtendCode(entityPath);
            }
            if (!string.IsNullOrEmpty(project.CppCodePath))
            {
                var cppPath = IOHelper.CheckPath(project.CppCodePath);
                var builder = new CppStructCoder
                {
                    Entity = schema,
                    Project = project
                };
                builder.CreateBaseCode(cppPath);
                builder.CreateExtendCode(cppPath);
            }
            if (!schema.IsClass && !string.IsNullOrEmpty(project.CppCodePath))
            {
                var cppPath = IOHelper.CheckPath(project.CppCodePath);
                {
                    var builder = new CppModelCoder
                    {
                        Entity = schema,
                        Project = project
                    };
                    builder.CreateBaseCode(cppPath);
                    builder.CreateExtendCode(cppPath);
                }
                if (!schema.IsReference)
                {
                    var builder = new CppAccessCoder
                    {
                        Entity = schema,
                        Project = project
                    };
                    builder.CreateBaseCode(cppPath);
                    builder.CreateExtendCode(cppPath);
                }
            }

            Message = schema.Caption + "已完成";
        }

    }


    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal sealed class CppDataFactoryProjectBuilde : ProjectBuilder, IAutoRegister
    {
        /// <summary>
        /// 名称
        /// </summary>
        protected override string Name => "CppDataFactory";

        /// <summary>
        /// 标题
        /// </summary>
        public override string Caption => Name;
        /// <summary>
        /// 执行自动注册
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            RegistBuilder<CppDataFactoryProjectBuilde>();
        }

        /// <summary>
        /// 生成项目代码
        /// </summary>
        /// <param name="project"></param>
        public override void CreateProjectCode(ProjectConfig project)
        {
            var builder = new CppDataFactoryCode();
            builder.CreateBaseCode(null);
            builder.CreateExtendCode(null);
        }

        /// <summary>
        /// 生成实体代码
        /// </summary>
        /// <param name="project"></param>
        /// <param name="schema"></param>
        public override void CreateEntityCode(ProjectConfig project, EntityConfig schema)
        {
        }
    }

    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal sealed class EsFundsProjectBuilde : ProjectBuilder, IAutoRegister
    {
        /// <summary>
        /// 名称
        /// </summary>
        protected override string Name => "EsFunds";

        /// <summary>
        /// 标题
        /// </summary>
        public override string Caption => "易盛期货接口代码";
        /// <summary>
        /// 执行自动注册
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            RegistBuilder<EsFundsProjectBuilde>();
        }
        /// <summary>
        /// 生成项目代码
        /// </summary>
        /// <param name="project"></param>
        public override void CreateProjectCode(ProjectConfig project)
        {
            var coder = new EsPublishCoder
            {
                Project = project
            };
            coder.CreateBaseCode(null);
        }

        /// <summary>
        /// 生成实体代码
        /// </summary>
        /// <param name="project"></param>
        /// <param name="schema"></param>
        public override void CreateEntityCode(ProjectConfig project, EntityConfig schema)
        {
        }
    }
}
