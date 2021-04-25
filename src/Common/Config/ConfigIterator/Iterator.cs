using System;

namespace Agebull.EntityModel.Config
{
    partial class ConfigBase : IConfigIterator
    {
        /// <summary>
        /// �ȸ�����
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        public void Preorder<T>(Action<T> action)
            where T : class
        {
            DoForeach(action, true);
        }

        /// <summary>
        /// �������
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        public void Postorder<T>(Action<T> action)
            where T : class
        {
            DoForeach(action, false);
        }

        internal ConfigBase CheckParent(Type type)
        {
            if (this is IChildrenConfig children && children.Parent is ConfigBase parent)
            {
                if (parent.IsType(type))
                    return parent;
                return parent.CheckParent(type);
            }
            return null;
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <param name="preorder">true:�ȸ����� false:�������</param>
        void DoForeach<T>(Action<T> action, bool preorder)
            where T : class
        {
            if (IsDiscard || IsDelete)
                return;
            var root = CheckParent(typeof(T));
            if (root != null)
            {
                root.Foreach<T>(action, preorder);
                return;
            }
            Foreach<T>(action, preorder);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <param name="preorder">true:�ȸ����� false:�������</param>
        void IConfigIterator.Foreach<T>(Action<T> action, bool preorder)
            where T : class
        {
            Foreach(action, preorder);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <param name="preorder">true:�ȸ����� false:�������</param>
        internal void Foreach<T>(Action<T> action, bool preorder)
            where T : class
        {
            if (IsDiscard || IsDelete)
                return;
            T arg;
            if (typeof(T) == typeof(ConfigDesignOption))
                arg = Option as T;
            else
                arg = this as T;
            if (preorder && arg != null)
                action(arg);
            ForeachDown(action, true);
            if (!preorder && arg != null)
                action(arg);
        }
        /// <summary>
        /// ���±���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <param name="preorder">true:�ȸ����� false:�������</param>
        protected virtual void ForeachDown<T>(Action<T> action, bool preorder)
            where T : class
        {
        }

        /// <summary>
        /// ������ͬ��
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
        /// <summary>
        ///     ����״̬
        /// </summary>
        public void ResetStatus()
        {
            Postorder<IModifyObject>(p => p.ResetModify(true));
        }
    }

    partial class SolutionConfig
    {
        /// <summary>
        /// ���±���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <param name="preorder">true:�ȸ����� false:�������</param>
        protected override void ForeachDown<T>(Action<T> action, bool preorder)
            where T : class
        {
            foreach (var project in Projects)
            {
                project.Foreach(action, preorder);
            }
        }
    }

    partial class ProjectConfig
    {
        /// <summary>
        /// ���±���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <param name="preorder">true:�ȸ����� false:�������</param>
        protected override void ForeachDown<T>(Action<T> action, bool preorder)
            where T : class
        {
            Entities.DoForeachDown(action, preorder);
            Models.DoForeachDown(action, preorder);
            Enums.DoForeachDown(action, preorder);
            ApiItems.DoForeachDown(action, preorder);
        }

        public override void OnLoad()
        {
            Classifies.OnLoad(nameof(Entities), this);
            Entities.OnLoad(nameof(Entities), this);
            Models.OnLoad(nameof(Models), this);
            Enums.OnLoad(nameof(Enums), this);
            ApiItems.OnLoad(nameof(ApiItems), this);
            base.OnLoad();
        }
    }


    partial class EntityClassify
    {
        /// <summary>
        /// ���±���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <param name="preorder">true:�ȸ����� false:�������</param>
        protected override void ForeachDown<T>(Action<T> action, bool preorder)
            where T : class
        {
            Items.DoForeachDown(action, preorder);
        }


        public override void OnLoad()
        {
            Items.OnLoad(nameof(Items), this);
            base.OnLoad();
        }
    }

    partial class EnumConfig
    {
        /// <summary>
        /// ���±���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <param name="preorder">true:�ȸ����� false:�������</param>
        protected override void ForeachDown<T>(Action<T> action, bool preorder)
            where T : class
        {
            Items.DoForeachDown(action, preorder);
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
        /// ���±���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <param name="preorder">true:�ȸ����� false:�������</param>
        protected override void ForeachDown<T>(Action<T> action, bool preorder)
            where T : class
        {
            Commands.DoForeachDown(action, preorder);
            DataTable?.Foreach(action, preorder);
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
        /// ���±���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <param name="preorder">true:�ȸ����� false:�������</param>
        protected override void ForeachDown<T>(Action<T> action, bool preorder)
            where T : class
        {
            if (WorkContext.InCoderGenerating)
            {
                LastProperties.DoForeachDown(action, preorder);
            }
            else
            {
                Properties.DoForeachDown(action, preorder);
            }
            base.ForeachDown<T>(action, preorder);
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
        /// ���±���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <param name="preorder">true:�ȸ����� false:�������</param>
        protected override void ForeachDown<T>(Action<T> action, bool preorder)
            where T : class
        {
            if (WorkContext.InCoderGenerating)
            {
                LastProperties.DoForeachDown(action, preorder);
            }
            else
            {
                Properties.DoForeachDown(action, preorder);
            }
            base.ForeachDown<T>(action, preorder);
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
        /// <summary>
        /// ���±���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <param name="preorder">true:�ȸ����� false:�������</param>
        protected override void ForeachDown<T>(Action<T> action, bool preorder)
            where T : class
        {
            Me.EnumConfig?.Foreach(action, preorder);
            DataBaseField?.Foreach(action, preorder);
        }
    }
}
namespace Agebull.EntityModel.Config.V2021
{
    partial class DataTableConfig
    {
        /// <summary>
        /// ���±���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <param name="preorder">true:�ȸ����� false:�������</param>
        protected override void ForeachDown<T>(Action<T> action, bool preorder)
            where T : class
        {
            Fields.DoForeachDown(action, preorder);
        }
        public override void OnLoad()
        {
            Fields.OnLoad(nameof(Fields), this);
            base.OnLoad();
        }
    }
    partial class DataBaseFieldConfig
    {
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
    
    */
}