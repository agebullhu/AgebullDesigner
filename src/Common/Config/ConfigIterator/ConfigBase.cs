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
        protected virtual int CheckType(Type type)
        {
            if(type == GetType())
                return 0;
            return 1;
        }

        /// <summary>
        /// 遍历
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        public void Foreach<T>(Action<T> action)
            where T : class
        {
            Foreach(action, false);
        }

        /// <summary>
        /// 遍历
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        public void Foreach<T>(Action<T> action, bool doAll)
            where T : class
        {
            if (IsDiscard || IsDelete)
                return;

            if (typeof(T) == typeof(ConfigDesignOption))
            {
                action(Option as T);
                ForeachDown(action, doAll);
                return;
            }
            if (this is T t)
            {
                action(t);
                if (!doAll)
                    return;
            }
            switch (CheckType(typeof(T)))
            {
                case -1:
                    ForeachUp(action);
                    break;
                case 1:
                    ForeachDown(action, doAll);
                    break;
            }
        }

        /// <summary>
        /// 遍历
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        public void Look<T>(Action<T> action)
            where T : class
        {
            Foreach(action, true);
        }

        /// <summary>
        /// 向上遍历
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected virtual void ForeachUp<T>(Action<T> action)
            where T : class
        {
        }

        /// <summary>
        /// 向下遍历
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected virtual void ForeachDown<T>(Action<T> action, bool doAction)
            where T : class
        {
        }

        /// <summary>
        /// 载入后的同步
        /// </summary>
        /// <returns></returns>
        public virtual void OnLoad()
        {
            _option.Config = this;
            ValueRecords.Add(nameof(Option), _option);
            GlobalConfig.AddNormalConfig(this);

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
            where T : class
        {
            foreach (var project in Projects)
            {
                project.Foreach(action, doAction);
            }
        }
    }

    partial class ProjectConfig
    {
        protected override int CheckType(Type type)
        {
            if (type == GetType())
                return 0;
            if (type ==typeof(SolutionConfig))
                return -1;
            return 1;
        }

        /// <summary>
        /// 向下遍历
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override void ForeachUp<T>(Action<T> action)
            where T : class
        {
            Solution.Foreach(action);
        }

        /// <summary>
        /// 向下遍历
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override void ForeachDown<T>(Action<T> action, bool doAction)
            where T : class
        {
            Entities.DoForeach(action, doAction);
            Models.DoForeach(action, doAction);
            Enums.DoForeach(action, doAction);
            ApiItems.DoForeach(action, doAction);
        }
        public override void OnLoad()
        {
            Entities.OnLoad(nameof(Entities), this);
            Models.OnLoad(nameof(Models), this);
            Enums.OnLoad(nameof(Enums), this);
            ApiItems.OnLoad(nameof(ApiItems), this);
            base.OnLoad();
        }
    }


    partial class EntityClassify
    {
        protected override int CheckType(Type type)
        {
            if (type == GetType() || type == typeof(ApiItem) || type == typeof(EnumConfig))
                return 0;
            if (type == typeof(SolutionConfig) || type == typeof(ProjectConfig))
                return -1;
            return 1;
        }

        /// <summary>
        /// 向上遍历
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override void ForeachUp<T>(Action<T> action)
            where T : class
        {
            Project.Foreach(action);
        }

        /// <summary>
        /// 向下遍历
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override void ForeachDown<T>(Action<T> action, bool doAction)
            where T : class
        {
            Items.DoForeach(action, doAction);
        }
    }
    partial class ProjectChildConfigBase
    {
        protected override int CheckType(Type type)
        {
            if (type == GetType())
                return 0;
            if (type == typeof(SolutionConfig) || type == typeof(ProjectConfig))
                return -1;
            return 1;
        }

        /// <summary>
        /// 向上遍历
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override void ForeachUp<T>(Action<T> action)
            where T : class
        {
            Project.Foreach(action);
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
            where T : class
        {
            Items.DoForeach(action, doAction);
        }
        public override void OnLoad()
        {
            Items.OnLoad(nameof(Items), this);
            base.OnLoad();
        }
    }


    partial class EntityConfigBase
    {
        /// <summary>
        /// 向下遍历
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override void ForeachDown<T>(Action<T> action, bool doAction)
            where T : class
        {
            Commands.DoForeach(action, doAction);
            DataTable?.Foreach(action, doAction);
            //Page?.Foreach(action, doAction);
        }
        protected override int CheckType(Type type)
        {
            if (type.IsSubclassOf(typeof(EntityConfigBase)) || type == typeof(IEntityConfig))
                return 0;
            if (type == typeof(SolutionConfig) || type == typeof(ProjectConfig))
                return -1;
            return 1;
        }

        public override void OnLoad()
        {
            (DataTable as IChildrenConfig)?.OnLoad(nameof(DataTable), this);
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
            where T : class
        {
            if (WorkContext.InCoderGenerating)
            {
                LastProperties.DoForeach(action, doAction);
            }
            else
            {
                Properties.DoForeach(action, doAction);
            }
            base.ForeachDown<T>(action, doAction);
        }
        public override void OnLoad()
        {
            Commands.OnLoad(nameof(Commands), this);
            Properties.OnLoad(nameof(Properties), this);
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
            where T : class
        {
            if (WorkContext.InCoderGenerating)
            {
                LastProperties.DoForeach(action, doAction);
            }
            else
            {
                Properties.DoForeach(action, doAction);
            }
            Releations.DoForeach(action, doAction);
            base.ForeachDown<T>(action, doAction);

        }
        public override void OnLoad()
        {
            Commands.OnLoad(nameof(Commands), this);
            Releations.OnLoad(nameof(Releations), this);
            Properties.OnLoad(nameof(Properties), this);
            base.OnLoad();
        }
    }
    partial class FieldConfigBase
    {
        protected override int CheckType(Type type)
        {
            if (type == GetType() || type == typeof(IPropertyConfig))
                return 0;
            if (type == typeof(SolutionConfig) || type == typeof(ProjectConfig) || type == typeof(EntityConfig) || type == typeof(ModelConfig))
                return -1;
            return 1;
        }

        /// <summary>
        /// 向上遍历
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override void ForeachUp<T>(Action<T> action)
            where T : class
        {
            Me.Entity.Foreach(action);
        }

        /// <summary>
        /// 向下遍历
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override void ForeachDown<T>(Action<T> action, bool doAction)
            where T : class
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
        protected override int CheckType(Type type)
        {
            if (type == GetType() || type == typeof(IPropertyConfig))
                return 0;
            if (type == typeof(SolutionConfig) || type == typeof(ProjectConfig) || type == typeof(EntityConfig) || type == typeof(ModelConfig))
                return -1;
            return 1;
        }

        /// <summary>
        /// 向上遍历
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override void ForeachUp<T>(Action<T> action)
            where T : class
        {
            Entity.Foreach(action);
        }
    }

    partial class FieldExtendConfig<TParent>
    {
        protected override int CheckType(Type type)
        {
            if (type == typeof(SolutionConfig) || type == typeof(ProjectConfig) || type == typeof(EntityConfig) || type == typeof(ModelConfig) || type == typeof(EntityExtendConfig))
                return -1;
            return 1;
        }

        /// <summary>
        /// 向上遍历
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override void ForeachUp<T>(Action<T> action)
            where T : class
        {
            if (ConfigIteratorExtension.IsParentOrFriendType<T>(typeof(EntityExtendConfig)))
            {
                Parent.Foreach(action);
            }
            else
            {
                Property.Foreach(action);
            }
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
            where T : class
        {
            Fields.DoForeach(action, doAction);
        }
        public override void OnLoad()
        {
            Fields.OnLoad(nameof(Fields), this);
            base.OnLoad();
        }
    }
    partial class DataBaseFieldConfig
    {
        protected override int CheckType(Type type)
        {
            if (type == GetType() || type == typeof(IPropertyConfig))
                return 0;
            if (type == typeof(SolutionConfig) || type == typeof(ProjectConfig) || type == typeof(EntityConfig) || type == typeof(ModelConfig))
                return -1;
            return 1;
        }

        public override void OnLoad()
        {
            var pro = Property;
            if (pro != null)
                pro.DataBaseField = this;
            else 
                Parent.Fields.Remove(this);
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