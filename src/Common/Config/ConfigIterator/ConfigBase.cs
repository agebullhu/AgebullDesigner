using System;
using System.Linq;

namespace Agebull.EntityModel.Config
{
    partial class ConfigBase : IConfigIterator
    {
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="action"></param>
        public void Foreach(Action<ConfigBase> action)
        {
            if (IsDiscard || IsDelete)
                return;

            action(this);
            ForeachDown(action, false);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        public void Foreach<T>(Action<T> action)
        {
            if (IsDiscard || IsDelete)
                return;
            Look(action, false);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        public void Look<T>(Action<T> action, bool doAll)
        {
            if (IsDiscard || IsDelete)
                return;

            if (this is T t)
            {
                action(t);
                if (!doAll)
                    return;
            }
            if (!doAll && ForeachUp(action))
                return;
            ForeachDown(action, doAll);
        }

        /// <summary>
        /// ���ϱ���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected virtual bool ForeachUp<T>(Action<T> action)
        {
            return false;
        }

        /// <summary>
        /// ���±���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected virtual void ForeachDown<T>(Action<T> action, bool doAction)
        {
        }

        /// <summary>
        /// ������ͬ��
        /// </summary>
        /// <returns></returns>
        public virtual void OnLoad()
        {
            GlobalTrigger.OnLoad(this);
            GlobalTrigger.OnLoad(Option);
        }
    }

    partial class SolutionConfig
    {
        /// <summary>
        /// ���±���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override void ForeachDown<T>(Action<T> action, bool doAction)
        {
            foreach (var project in Projects)
            {
                project.Look(action, doAction);
            }
        }
    }

    partial class ProjectConfig
    {
        /// <summary>
        /// ���±���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override void ForeachDown<T>(Action<T> action, bool doAction)
        {
            Entities.DoForeach(action, doAction);
            Models.DoForeach(action, doAction);
            Enums.DoForeach(action, doAction);
            ApiItems.DoForeach(action, doAction);
        }
        public override void OnLoad()
        {
            Entities.OnLoad(this);
            Models.OnLoad(this);
            Enums.OnLoad(this);
            ApiItems.OnLoad(this);
            base.OnLoad();
        }
    }


    partial class EntityClassify
    {
        /// <summary>
        /// ���ϱ���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override bool ForeachUp<T>(Action<T> action)
        {
            if (ConfigIteratorExtension.IsParentOrFriendType<T>(typeof(ProjectConfig), typeof(SolutionConfig)))
            {
                Project.Foreach(action);
                return true;
            }
            return false;
        }

        /// <summary>
        /// ���±���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override void ForeachDown<T>(Action<T> action, bool doAction)
        {
            Items.DoForeach(action, doAction);
        }

    }
    partial class ProjectChildConfigBase
    {
        /// <summary>
        /// ���ϱ���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override bool ForeachUp<T>(Action<T> action)
        {
            if (ConfigIteratorExtension.IsParentOrFriendType<T>(typeof(ProjectConfig), typeof(SolutionConfig)))
            {
                Project.Foreach(action);
                return true;
            }
            return false;
        }
    }
    partial class EnumConfig
    {
        /// <summary>
        /// ���±���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override void ForeachDown<T>(Action<T> action, bool doAction)
        {
            Items.DoForeach(action, doAction);
        }
        public override void OnLoad()
        {
            Items.OnLoad(this);
            base.OnLoad();
        }
    }


    partial class EntityConfigBase
    {
        /// <summary>
        /// ���±���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override void ForeachDown<T>(Action<T> action, bool doAction)
        {
            DataTable?.Look(action, doAction);
            Page?.Look(action, doAction);

            if (WorkContext.InCoderGenerating)
            {
                Me.LastProperties.DoForeach(action, doAction);
            }
            else
            {
                Me.Properties.DoForeach(action, doAction);
            }
            Me.Commands.DoForeach(action, doAction);
        }
        public override void OnLoad()
        {
            DataTable?.OnLoad();
            Page?.OnLoad();
            base.OnLoad();
        }

    }
    partial class EntityConfig
    {
        public override void OnLoad()
        {
            Properties.OnLoad(this);
            base.OnLoad();
        }
    }

    partial class ModelConfig
    {
        public override void OnLoad()
        {
            Properties.OnLoad(this);
            base.OnLoad();
        }
    }
    partial class FieldConfigBase
    {
        /// <summary>
        /// ���ϱ���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override bool ForeachUp<T>(Action<T> action)
        {
            if (ConfigIteratorExtension.IsParentOrFriendType<T>(typeof(IEntityConfig), typeof(ProjectConfig), typeof(SolutionConfig)))
            {
                Me.Entity.Foreach(action);
                return true;
            }
            return false;
        }

        /// <summary>
        /// ���±���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override void ForeachDown<T>(Action<T> action, bool doAction)
        {
            Me.EnumConfig?.Look(action, doAction);
        }
    }
}
namespace Agebull.EntityModel.Config.V2021
{
    partial class EntityExtendConfig
    {
        /// <summary>
        /// ���ϱ���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override bool ForeachUp<T>(Action<T> action)
        {
            if (ConfigIteratorExtension.IsParentOrFriendType<T>(typeof(IEntityConfig), typeof(ProjectConfig), typeof(SolutionConfig)))
            {
                Entity.Foreach(action);
                return true;
            }
            return false;
        }
    }

    partial class DataTableConfig
    {
        /// <summary>
        /// ���±���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override void ForeachDown<T>(Action<T> action, bool doAction)
        {
            Fields.DoForeach(action, doAction);
        }
        public override void OnLoad()
        {
            Fields.OnLoad(this);
            base.OnLoad();
        }
    }

    partial class PageConfig
    {
        /// <summary>
        /// ���±���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override void ForeachDown<T>(Action<T> action, bool doAction)
        {
            Properties.DoForeach(action, doAction);
        }
        public override void OnLoad()
        {
            Properties.OnLoad(this);
            Main ??= new PageContentConfig();
            Main.Page = this;
            Main.OnLoad();
            Details ??= new PageContentConfig();
            Details.Page = this;
            Details.OnLoad();
            base.OnLoad();
        }
    }

    partial class PageContentConfig
    {
        /// <summary>
        /// ���±���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override void ForeachDown<T>(Action<T> action, bool doAction)
        {
            base.ForeachDown(action, doAction);
            Regions.DoForeach(action, doAction);
        }
        public override void OnLoad()
        {
            Regions ??= new NotificationList<PageRegionConfig>();
            Regions.OnLoad(this);
            base.OnLoad();
        }
    }
    partial class PageRegionConfig
    {
        /// <summary>
        /// ���±���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override void ForeachDown<T>(Action<T> action, bool doAction)
        {
            base.ForeachDown(action, doAction);
            Items.DoForeach(action, doAction);
        }
        public override void OnLoad()
        {
            Items ??= new NotificationList<PageItemConfig>();
            Items.OnLoad(this);
            base.OnLoad();
        }
    }

    partial class FieldExtendConfig
    {
        /// <summary>
        /// ���ϱ���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override bool ForeachUp<T>(Action<T> action)
        {
            if (ConfigIteratorExtension.IsParentOrFriendType<T>(typeof(IEntityConfig), typeof(ProjectConfig), typeof(SolutionConfig)))
            {
                Property.Foreach(action);
                return true;
            }
            if (ConfigIteratorExtension.IsParentOrFriendType<T>(typeof(EntityExtendConfig)))
            {
                Parent.Foreach(action);
                return true;
            }
            return false;
        }
    }
}