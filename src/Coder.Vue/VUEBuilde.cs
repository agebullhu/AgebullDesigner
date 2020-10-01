using System.ComponentModel.Composition;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;

namespace Agebull.EntityModel.RobotCoder.VUE
{
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]

    internal class VUEBuilderRegister : IAutoRegister
    {
        /// <summary>
        /// ִ���Զ�ע��
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            NormalCodeModel.RegistBuilder<VUEBuilder<EntityConfig>, EntityConfig>();
            NormalCodeModel.RegistBuilder<VUEBuilder<ModelConfig>, ModelConfig>();
        }
    }

    public class VUEBuilder<TModelConfig> : ProjectBuilder<TModelConfig>
        where TModelConfig : ProjectChildConfigBase, IEntityConfig
    {
        /// <summary>
        /// ����
        /// </summary>
        public override string Name => "VUE";

        /// <summary>
        /// ����
        /// </summary>
        public override string Caption => Name;

        /// <summary>
        /// ������Ŀ����
        /// </summary>
        /// <param name="project"></param>
        public override void CreateProjectCode(ProjectConfig project)
        {

        }

        /// <summary>
        /// ����ʵ�����
        /// </summary>
        /// <param name="project"></param>
        /// <param name="schema"></param>
        public override void CreateModelCode(ProjectConfig project, TModelConfig schema)
        {
            var pg = new PorjectVUEGenerator<TModelConfig>
            {
                Model = schema,
                Project = schema.Parent,
            };
            pg.WriteDesignerCode(project.PagePath);
            pg.WriteCustomCode(project.PagePath);
        }
    }
}