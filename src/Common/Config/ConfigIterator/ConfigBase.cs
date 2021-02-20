using System;

namespace Agebull.EntityModel.Config
{
    partial class ConfigBase : IConfigIterator
    {
        /// <summary>
        /// 遍历
        /// </summary>
        /// <param name="action"></param>
        public void Look(Action<ConfigBase> action)
        {
            if (IsDiscard || IsDelete)
                return;

            action(this);
            ForeachDown(action, true);
        }

        /// <summary>
        /// 遍历
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        public void Foreach<T>(Action<T> action)
        {
            if (IsDiscard || IsDelete)
                return;
            Foreach(action, false);
        }

        /// <summary>
        /// 遍历
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        public void Foreach<T>(Action<T> action, bool doAll)
        {
            if (IsDiscard || IsDelete)
                return;
            if (this is T t)
            {
                action(t);
                if (!doAll)
                    return;
            }
            if (doAll || !ForeachUp(action))
                ForeachDown(action, doAll);
        }

        /// <summary>
        /// 遍历
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        public void Look<T>(Action<T> action)
        {
            if (IsDiscard || IsDelete)
                return;
            Foreach(action, true);
        }

        /// <summary>
        /// 向上遍历
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected virtual bool ForeachUp<T>(Action<T> action)
        {
            return false;
        }

        /// <summary>
        /// 向下遍历
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected virtual void ForeachDown<T>(Action<T> action, bool doAction)
        {
        }

        /// <summary>
        /// 载入后的同步
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
        /// 向下遍历
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override void ForeachDown<T>(Action<T> action, bool doAction)
        {
            foreach (var project in Projects)
            {
                project.Foreach(action, doAction);
            }
        }
    }

    partial class ProjectConfig
    {
        /// <summary>
        /// 向下遍历
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
        /// 向上遍历
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
        /// 向下遍历
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
        /// 向上遍历
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
        /// 向下遍历
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
        public override void OnLoad()
        {
            DataTable?.OnLoad();
            //Page?.OnLoad();
            base.OnLoad();
        }

    }
    partial class EntityConfig
    {
        /// <summary>
        /// 向下遍历
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override void ForeachDown<T>(Action<T> action, bool doAction)
        {
            DataTable?.Foreach(action, doAction);
            //Page?.Foreach(action, doAction);

            if (WorkContext.InCoderGenerating)
            {
                LastProperties.DoForeach(action, doAction);
            }
            else
            {
                Properties.DoForeach(action, doAction);
            }
            Commands.DoForeach(action, doAction);
        }
        public override void OnLoad()
        {
            Commands.OnLoad(this);
            Properties.OnLoad(this);
            base.OnLoad();
        }
    }

    partial class ModelConfig
    {
        /// <summary>
        /// 向下遍历
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override void ForeachDown<T>(Action<T> action, bool doAction)
        {
            DataTable?.Foreach(action, doAction);
            //Page?.Foreach(action, doAction);

            if (WorkContext.InCoderGenerating)
            {
                LastProperties.DoForeach(action, doAction);
            }
            else
            {
                Properties.DoForeach(action, doAction);
            }
            Commands.DoForeach(action, doAction);
            Releations.DoForeach(action, doAction);
        }
        public override void OnLoad()
        {
            Commands.OnLoad(this);
            Releations.OnLoad(this);
            Properties.OnLoad(this);
            base.OnLoad();
        }
    }
    partial class FieldConfigBase
    {
        /// <summary>
        /// 向上遍历
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
        /// 向下遍历
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override void ForeachDown<T>(Action<T> action, bool doAction)
        {
            Me.EnumConfig?.Foreach(action, doAction);
            DataBaseField?.Foreach(action, doAction);
        }
    }
}
namespace Agebull.EntityModel.Config.V2021
{
    partial class EntityExtendConfig
    {
        /// <summary>
        /// 向上遍历
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

    partial class FieldExtendConfig<TParent>
    {
        /// <summary>
        /// 向上遍历
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
    partial class DataTableConfig
    {
        /// <summary>
        /// 向下遍历
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
    partial class DataBaseFieldConfig
    {
        public override void OnLoad()
        {
            Property.DataBaseField = this;
            base.OnLoad();
        }
    }
    /*
    partial class PageConfig
    {
        /// <summary>
        /// 向下遍历
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
        /// 向下遍历
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
        /// 向下遍历
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
    
    */
}