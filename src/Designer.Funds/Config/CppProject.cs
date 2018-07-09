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
    /// ��ʾһ��������֪ͨ
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public class CppProject : ProjectConfig
    {
        /// <summary>
        /// ����(δǨ����ɷ���)
        /// </summary>
        public static CppProject Instance = new CppProject();

        /// <summary>
        /// ����ʵ��
        /// </summary>
        /// <param name="api"></param>
        public void Add(NotifyItem api)
        {
            api.Parent = this;
            if (!NotifyItems.Contains(api))
                NotifyItems.Add(api);
        }
        /// <summary>
        /// ����ʵ��
        /// </summary>
        /// <param name="api"></param>
        public void Add(TypedefItem api)
        {
            api.Parent = this;
            if (!TypedefItems.Contains(api))
                TypedefItems.Add(api);
        }
        /// <summary>
        /// ����ʵ��
        /// </summary>
        /// <param name="api"></param>
        public void Remove(NotifyItem api)
        {
            api.Parent = this;
            NotifyItems.Remove(api);
        }
        /// <summary>
        /// ����ʵ��
        /// </summary>
        /// <param name="api"></param>
        public void Remove(TypedefItem api)
        {
            api.Parent = this;
            TypedefItems.Remove(api);
        }
        /// <summary>
        /// ����(C++)����
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        internal ObservableCollection<TypedefItem> _typedefItems;

        /// <summary>
        /// ����(C++)����
        /// </summary>
        /// <remark>
        /// ����C++���Ͷ���
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"���󼯺�"), DisplayName(@"����(C++)����"), Description("����C++���Ͷ���")]
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
        /// ֪ͨ����
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        internal ObservableCollection<NotifyItem> _notifyItems;

        /// <summary>
        /// ֪ͨ����
        /// </summary>
        /// <remark>
        /// ��Ӧ��֪ͨ����
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"���󼯺�"), DisplayName(@"֪ͨ����"), Description("��Ӧ��֪ͨ����")]
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
        ///     ����֪ͨ����
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public  NotifyItem GetNotify(Func<NotifyItem, bool> func)
        {
            return NotifyItems.FirstOrDefault(func);
        }

        /// <summary>
        ///     �������Ͷ������
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public  TypedefItem GetTypedef(Func<TypedefItem, bool> func)
        {
            return TypedefItems.FirstOrDefault(func);
        }

        /// <summary>
        ///     ͨ����ǩ�������Ͷ������
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
        ///     ����֪ͨ����
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public  NotifyItem Find(Func<NotifyItem, bool> func)
        {
            return NotifyItems.FirstOrDefault(func);
        }
        /// <summary>
        ///     �������Ͷ������
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public  TypedefItem Find(Func<TypedefItem, bool> func)
        {
            return TypedefItems.FirstOrDefault(func);
        }

        /// <summary>
        /// ����
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
        /// ����֪ͨ����
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
        ///     ȡ�����Ͷ������
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