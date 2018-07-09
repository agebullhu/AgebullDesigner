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
        internal ObservableCollection<TypedefItem> _typedefItems;

        /// <summary>
        /// 类型(C++)集合
        /// </summary>
        /// <remark>
        /// 所有C++类型定义
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"对象集合"), DisplayName(@"类型(C++)集合"), Description("所有C++类型定义")]
        public ObservableCollection<TypedefItem> TypedefItems
        {
            get
            {
                if (_typedefItems != null)
                    return _typedefItems;
                _typedefItems = new ObservableCollection<TypedefItem>();
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
        internal ObservableCollection<NotifyItem> _notifyItems;

        /// <summary>
        /// 通知集合
        /// </summary>
        /// <remark>
        /// 对应的通知集合
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"对象集合"), DisplayName(@"通知集合"), Description("对应的通知集合")]
        public ObservableCollection<NotifyItem> NotifyItems
        {
            get
            {
                if (_notifyItems != null)
                    return _notifyItems;
                _notifyItems = new ObservableCollection<NotifyItem>();
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
        public  NotifyItem GetNotify(Func<NotifyItem, bool> func)
        {
            return NotifyItems.FirstOrDefault(func);
        }

        /// <summary>
        ///     查找类型定义对象
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public  TypedefItem GetTypedef(Func<TypedefItem, bool> func)
        {
            return TypedefItems.FirstOrDefault(func);
        }

        /// <summary>
        ///     通过标签查找类型定义对象
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public  TypedefItem GetTypedefByTag(string tag)
        {
            if (string.IsNullOrWhiteSpace(tag))
                return null;
            var array = tag.Split(',');
            if (array.Length != 2)
                return null;
            return TypedefItems.FirstOrDefault(p => p.Tag == array[0] && p.Name == array[1]);
        }
        /// <summary>
        ///     查找通知对象
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public  NotifyItem Find(Func<NotifyItem, bool> func)
        {
            return NotifyItems.FirstOrDefault(func);
        }
        /// <summary>
        ///     查找类型定义对象
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public  TypedefItem Find(Func<TypedefItem, bool> func)
        {
            return TypedefItems.FirstOrDefault(func);
        }

        /// <summary>
        /// 保存
        /// </summary>
        public void SaveProject(ConfigWriter write)
        {
            using (WorkModelScope.CreateScope(WorkModel.Saving))
            {
                SaveTypedefs(write);
                SaveNotifies(write);
            }
        }
        public void SaveTypedefs(ConfigWriter write)
        {
            var path = IOHelper.CheckPath(write.Directory, "Typedefs");
            foreach (var type in TypedefItems.ToArray())
            {
                SaveTypedef(write,type, path);
            }
        }
        /// <summary>
        /// 保存通知对象
        /// </summary>
        public void SaveNotifies(ConfigWriter write)
        {
            string path = IOHelper.CheckPath(write.Directory, "notifies");
            foreach (var notify in NotifyItems.ToArray())
            {
                write.Save(notify, path, ".ent");
            }
        }
        /// <summary>
        ///     取得类型定义对象
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public  TypedefItem GetTypedef(string name)
        {
            return TypedefItems.FirstOrDefault(p => p.Name == name);
        }


        public void SaveTypedef(ConfigWriter write,TypedefItem type, string path, bool checkState = true)
        {
            var filename = Path.Combine(path, type.GetFileName(".typ"));
            if (checkState && !write.CheckCanSave(type, filename))
                return;
            ConfigWriter.DeleteOldFile(type, filename, false);
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
            if (type.IsDelete)
                Remove(type);
            ConfigWriter.Serializer(filename, type);
        }


        private void LoadNotify(string directory)
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            string path = Path.Combine(directory, "notifies");
            var notifies = IOHelper.GetAllFiles(path, "ent");
            foreach (var file in notifies)
            {
                var notify = JsonConvert.DeserializeObject<NotifyItem>(file);
                notify.SaveFileName = file;
                notify.IsFreeze = false;
                if (!notify.IsDelete)
                    Add(notify);
            }
        }

        private void LoadTypedefs(string directory)
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            string path = Path.Combine(directory, "Typedefs");
            var typedefs = IOHelper.GetAllFiles(path, "typ");
            foreach (var file in typedefs)
            {
                TypedefItem type = JsonConvert.DeserializeObject< TypedefItem>(file);
                if (!type.IsDelete)
                {
                    Add(type);
                }
            }
        }
    }
}