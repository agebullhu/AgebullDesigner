using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using Agebull.Common;
using Agebull.EntityModel.Designer;
using Newtonsoft.Json;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// 表示一个第三方通知
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public class CppProject : ProjectConfig
    {
        /// <summary>
        /// 单例(未迁移完成方案)
        /// </summary>
        public static CppProject Instance = new CppProject();

        /// <summary>
        /// 加入实体
        /// </summary>
        /// <param name="api"></param>
        public void Add(NotifyItem api)
        {
            api.Parent = this;
            if (!NotifyItems.Contains(api))
                NotifyItems.Add(api);
        }
        /// <summary>
        /// 加入实体
        /// </summary>
        /// <param name="api"></param>
        public void Add(TypedefItem api)
        {
            api.Parent = this;
            if (!TypedefItems.Contains(api))
                TypedefItems.Add(api);
        }
        /// <summary>
        /// 加入实体
        /// </summary>
        /// <param name="api"></param>
        public void Remove(NotifyItem api)
        {
            api.Parent = this;
            NotifyItems.Remove(api);
        }
        /// <summary>
        /// 加入实体
        /// </summary>
        /// <param name="api"></param>
        public void Remove(TypedefItem api)
        {
            api.Parent = this;
            TypedefItems.Remove(api);
        }
        /// <summary>
        /// 类型(C++)集合
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        internal NotificationList<TypedefItem> _typedefItems;

        /// <summary>
        /// 类型(C++)集合
        /// </summary>
        /// <remark>
        /// 所有C++类型定义
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"对象集合"), DisplayName(@"类型(C++)集合"), Description("所有C++类型定义")]
        public NotificationList<TypedefItem> TypedefItems
        {
            get
            {
                if (_typedefItems != null)
                    return _typedefItems;
                _typedefItems = new NotificationList<TypedefItem>();
                OnPropertyChanged(nameof(TypedefItems));
                return _typedefItems;
            }
            set
            {
                if (_typedefItems == value)
                    return;
                BeforePropertyChanged(nameof(TypedefItems), _typedefItems, value);
                _typedefItems = value;
                OnPropertyChanged(nameof(TypedefItems));
            }
        }

        /// <summary>
        /// 通知集合
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        internal NotificationList<NotifyItem> _notifyItems;

        /// <summary>
        /// 通知集合
        /// </summary>
        /// <remark>
        /// 对应的通知集合
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"对象集合"), DisplayName(@"通知集合"), Description("对应的通知集合")]
        public NotificationList<NotifyItem> NotifyItems
        {
            get
            {
                if (_notifyItems != null)
                    return _notifyItems;
                _notifyItems = new NotificationList<NotifyItem>();
                OnPropertyChanged(nameof(NotifyItems));
                return _notifyItems;
            }
            set
            {
                if (_notifyItems == value)
                    return;
                BeforePropertyChanged(nameof(NotifyItems), _notifyItems, value);
                _notifyItems = value;
                OnPropertyChanged(nameof(NotifyItems));
            }
        }


        /// <summary>
        ///     查找通知对象
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public NotifyItem GetNotify(Func<NotifyItem, bool> func)
        {
            return NotifyItems.FirstOrDefault(func);
        }

        /// <summary>
        ///     查找类型定义对象
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public TypedefItem GetTypedef(Func<TypedefItem, bool> func)
        {
            return TypedefItems.FirstOrDefault(func);
        }

        /// <summary>
        ///     通过标签查找类型定义对象
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public TypedefItem GetTypedefByTag(string tag)
        {
            if (string.IsNullOrWhiteSpace(tag))
                return null;
            var array = tag.Split(',');
            if (array.Length != 2)
                return null;
            return TypedefItems.FirstOrDefault(p => p.Option.ReferenceTag == array[0] && p.Name == array[1]);
        }
        /// <summary>
        ///     查找通知对象
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public NotifyItem Find(Func<NotifyItem, bool> func)
        {
            return NotifyItems.FirstOrDefault(func);
        }
        /// <summary>
        ///     查找类型定义对象
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public TypedefItem Find(Func<TypedefItem, bool> func)
        {
            return TypedefItems.FirstOrDefault(func);
        }

        public override void ForeachChild(Action<ConfigBase> action)
        {
            base.ForeachChild(action);

            if (_notifyItems != null)
                foreach (var item in _notifyItems)
                    action(item);

            if (_typedefItems != null)
                foreach (var item in _typedefItems)
                    action(item);
        }

        /// <summary>
        /// 保存
        /// </summary>
        public void SaveProject(string dir, ConfigWriter write)
        {
            using (WorkModelScope.CreateScope(WorkModel.Saving))
            {
                dir = write.SaveProject(this, dir);
                SaveTypedefs(dir, write);
                SaveNotifies(dir, write);
            }
        }
        public void SaveTypedefs(string dir, ConfigWriter write)
        {
            var path = GlobalConfig.CheckPath(dir, "Typedef");
            foreach (var type in TypedefItems.ToArray())
            {
                SaveTypedef(write, type, path);
            }
        }
        /// <summary>
        /// 保存通知对象
        /// </summary>
        public void SaveNotifies(string dir, ConfigWriter write)
        {
            string path = GlobalConfig.CheckPath(dir, "Notify");
            foreach (var notify in NotifyItems.ToArray())
            {
                write.SaveConfig(notify, path, true);
            }
        }
        /// <summary>
        ///     取得类型定义对象
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public TypedefItem GetTypedef(string name)
        {
            return TypedefItems.FirstOrDefault(p => p.Name == name);
        }

        /// <summary>
        ///     取得类型定义对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public TypedefItem GetTypedef(Guid key)
        {
            return TypedefItems.FirstOrDefault(p => p.Key == key);
        }

        public void SaveTypedef(ConfigWriter write, TypedefItem type, string path, bool checkState = true)
        {
            if (type.IsDelete)
            {
                Remove(type);
            }
            else
            {
                foreach (var field in type.Items.Where(p => p.Value.IsDelete).ToArray())
                {
                    type.Items.Remove(field.Key);
                }
            }
            write.SaveConfig(type, path, true);
        }


        private void LoadNotify(string directory)
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            string path = Path.Combine(directory, "Notify");
            var notifies = IOHelper.GetAllFiles(path, "json");
            foreach (var file in notifies)
            {
                var notify = JsonConvert.DeserializeObject<NotifyItem>(file);
                notify.SaveFileName = file;
                if (!notify.IsDelete)
                    Add(notify);
            }
        }

        private void LoadTypedefs(string directory)
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            string path = Path.Combine(directory, "Typedef");
            var typedefs = IOHelper.GetAllFiles(path, "json");
            foreach (var file in typedefs)
            {
                TypedefItem type = JsonConvert.DeserializeObject<TypedefItem>(file);
                if (!type.IsDelete)
                {
                    Add(type);
                }
            }
        }
    }
}