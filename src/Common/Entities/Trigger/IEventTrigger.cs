using System;

namespace Agebull.EntityModel
{
    /// <summary>
    /// 事件触发器
    /// </summary>
    public interface IEventTrigger
    {
        /// <summary>
        /// 目标类型
        /// </summary>
        Type TargetType { get; }

        /// <summary>
        /// 当前配置对象
        /// </summary>
        object Target { get; set; }

        /// <summary>
        ///     发出属性修改前事件
        /// </summary>
        /// <param name="config"></param>
        /// <param name="property">属性</param>
        /// <param name="oldValue">旧值</param>
        /// <param name="newValue">新值</param>
        void BeforePropertyChanged(object config, string property, object oldValue, object newValue)
        {
            Target = config;
            var scope = NameEventScope.CreateScope(config, this.GetTypeName(), property);
            if (scope == null)
                return;
            using (scope)
            {
                BeforePropertyChanged(property, oldValue, newValue);
            }
        }
        /// <summary>
        ///     发出属性修改前事件
        /// </summary>
        /// <param name="property">属性</param>
        /// <param name="oldValue">旧值</param>
        /// <param name="newValue">新值</param>
        void BeforePropertyChanged(string property, object oldValue, object newValue)
        {
        }
        /// <summary>
        /// 属性事件处理
        /// </summary>
        /// <param name="config"></param>
        /// <param name="property"></param>
        void OnPropertyChanged(object config, string property)
        {
            Target = config;
            var scope = NameEventScope.CreateScope(config, this.GetTypeName(), property);
            if (scope == null)
                return;
            using (scope)
            {
                OnPropertyChanged(property);
            }
        }
        /// <summary>
        /// 属性事件处理
        /// </summary>
        /// <param name="property"></param>
        void OnPropertyChanged(string property)
        {

        }

        /// <summary>
        /// 载入事件处理
        /// </summary>
        /// <param name="config"></param>
        void OnCreate(object config)
        {
            Target = config;
            var scope = NameEventScope.CreateScope(config, this.GetTypeName(), nameof(OnCreate));
            if (scope == null)
                return;
            using (scope)
            {
                OnCreate();
            }
        }

        /// <summary>
        /// 载入事件处理
        /// </summary>
        void OnCreate()
        {
        }

        /// <summary>
        /// 载入事件处理
        /// </summary>
        /// <param name="config"></param>
        void OnLoad(object config)
        {
            Target = config;
            var scope = NameEventScope.CreateScope(config, this.GetTypeName(), nameof(OnLoad));
            if (scope == null)
                return;
            using (scope)
            {
                using (WorkModelScope.CreateScope(WorkModel.Repair))
                    OnLoad();
            }
        }
        /// <summary>
        /// 载入事件处理
        /// </summary>
        void OnLoad()
        {
        }

        /// <summary>
        /// 加入事件处理
        /// </summary>
        /// <param name="config"></param>
        /// <param name="parent"></param>
        void OnAdded(object parent, object config)
        {
            Target = parent;
            var scope = NameEventScope.CreateScope(config, this.GetTypeName(), nameof(OnAdded));
            if (scope == null)
                return;
            using (scope)
            {
                OnAdded(config);
            }
        }

        /// <summary>
        /// 加入事件处理
        /// </summary>
        /// <param name="config"></param>
        void OnAdded(object config)
        {
        }

        /// <summary>
        /// 删除事件处理
        /// </summary>
        /// <param name="config"></param>
        /// <param name="parent"></param>
        void OnRemoved(object parent, object config)
        {
            Target = parent;
            var scope = NameEventScope.CreateScope(config, this.GetTypeName(), nameof(OnRemoved));
            if (scope == null)
                return;
            using (scope)
            {
                OnRemoved(config);
            }
        }
        /// <summary>
        /// 删除事件处理
        /// </summary>
        /// <param name="config"></param>
        void OnRemoved(object config)
        {
        }


        /// <summary>
        /// 规整对象
        /// </summary>
        void Regularize(object config)
        {
            Target = config;
            var scope = NameEventScope.CreateScope(config, this.GetTypeName(), nameof(Regularize));
            if (scope == null)
                return;
            using (scope)
            {
                Regularize();
            }
        }


        /// <summary>
        /// 规整对象
        /// </summary>
        void Regularize()
        {

        }

        /// <summary>
        /// 开始代码生成
        /// </summary>
        void OnCodeGeneratorBegin(object config)
        {
            Target = config;
            var scope = NameEventScope.CreateScope(config, this.GetTypeName(), nameof(OnCodeGeneratorBegin));
            if (scope == null)
                return;
            using (scope)
            {
                OnCodeGeneratorBegin();
            }
        }

        /// <summary>
        /// 开始代码生成
        /// </summary>
        void OnCodeGeneratorBegin()
        {

        }

        /// <summary>
        /// 完成代码生成
        /// </summary>
        void OnCodeGeneratorEnd(object config)
        {
            Target = config;
            var scope = NameEventScope.CreateScope(config, this.GetTypeName(), nameof(OnCodeGeneratorBegin));
            if (scope == null)
                return;
            using (scope)
            {
                OnCodeGeneratorEnd();
            }
        }

        /// <summary>
        /// 完成代码生成
        /// </summary>
        void OnCodeGeneratorEnd()
        {

        }
    }
}
