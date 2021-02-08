using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Agebull.EntityModel.Config.V2021
{
    /// <summary>
    /// 基于实体的扩展配置
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public partial class PageConfig : EntityExtendConfig
    {
        #region 设计

        /// <summary>
        /// 取文件名
        /// </summary>
        /// <returns></returns>
        public override string GetFileName() => GetFileName(Entity);

        /// <summary>
        /// 取文件名
        /// </summary>
        /// <returns></returns>
        public static string GetFileName(IEntityConfig entity) => entity?.Name.Trim().Replace(' ', '_').Replace('>', '_').Replace('<', '_') + ".page.json";

        #endregion

        #region 内容
        /// <summary>
        /// 主页面
        /// </summary>
        public PageContentConfig Main { get; set; }

        /// <summary>
        /// 详细页面
        /// </summary>
        public PageContentConfig Details { get; set; }

        #endregion

        #region 配置

        /// <summary>
        /// 界面只读
        /// </summary>
        [DataMember, JsonProperty("isUiReadOnly", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isUiReadOnly;

        /// <summary>
        /// 界面只读
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"界面只读"), Description("界面只读")]
        public bool IsUiReadOnly
        {
            get => _isUiReadOnly;
            set
            {
                if (_isUiReadOnly == value)
                    return;
                BeforePropertyChanged(nameof(IsUiReadOnly), _isUiReadOnly, value);
                _isUiReadOnly = value;
                OnPropertyChanged(nameof(IsUiReadOnly));
            }
        }

        /// <summary>
        /// 页面文件夹名称
        /// </summary>
        [DataMember, JsonProperty("PageFolder", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _pageFolder;

        /// <summary>
        /// 页面文件夹名称
        /// </summary>
        /// <remark>
        /// 页面文件夹名称
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"页面文件夹名称"), Description("页面文件夹名称")]
        public string PageFolder
        {
            get => WorkContext.InCoderGenerating ? _pageFolder ?? Name : _pageFolder;
            set
            {
                if (_pageFolder == value)
                    return;
                BeforePropertyChanged(nameof(PageFolder), _pageFolder, value);
                _pageFolder = string.IsNullOrWhiteSpace(value) || value == Name ? null : value.Trim();
                OnPropertyChanged(nameof(PageFolder));
            }
        }

        #endregion

        #region 子级

        /// <summary>
        /// 字段列表
        /// </summary>
        [DataMember, JsonProperty("properties", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal ConfigCollection<UserInterfaceField> _properties;

        /// <summary>
        /// 字段列表
        /// </summary>
        /// <remark>
        /// 字段列表
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据模型"), DisplayName(@"字段列表"), Description("字段列表")]
        public ConfigCollection<UserInterfaceField> Properties
        {
            get
            {
                if (_properties != null)
                    return _properties;
                _properties = new ConfigCollection<UserInterfaceField>();
                RaisePropertyChanged(nameof(Properties));
                return _properties;
            }
            set
            {
                if (_properties == value)
                    return;
                BeforePropertyChanged(nameof(Properties), _properties, value);
                _properties = value;
                OnPropertyChanged(nameof(Properties));
            }
        }

        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public UserInterfaceField Find(string name)
        {
            return Properties.FirstOrDefault(p => name.IsMe(p.Property.Name) );
        }

        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public UserInterfaceField Find(params string[] names)
        {
            return Properties.FirstOrDefault(p => names.Exist(p.Property.Name));
        }

        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool TryGet(out UserInterfaceField field, params string[] names)
        {
            field = Properties.FirstOrDefault(p => names.Exist(p.Property.Name));
            return field != null;
        }

        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool Exist(params string[] names)
        {
            return Properties.Any(p => names.Exist(p.Property.Name));
        }

        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public void Add(UserInterfaceField field)
        {
            if (!Properties.Any(p => p.Property == field.Property))
            {
                field.Parent = this;
                Properties.Add(field);
            }
        }

        #endregion

        #region 兼容性升级

        /// <summary>
        /// 兼容性升级
        /// </summary>
        public void Upgrade()
        {
            IsUiReadOnly = Entity.IsUiReadOnly;
            PageFolder = Entity.PageFolder;
            _properties = new ConfigCollection<UserInterfaceField>();
            foreach (var field in Entity.Properties)
            {
                var uiField = new UserInterfaceField
                {
                    Property = field,
                    Parent = this,
                };
                uiField.Copy(field);
                _properties.Add(uiField);
            }
            Main = new PageContentConfig
            {
                Regions = new NotificationList<PageRegionConfig>()
            };
            Details = new PageContentConfig
            {
                Regions = new NotificationList<PageRegionConfig>()
            };
            Main.Regions.Add(new PageRegionConfig
            {
                RegionType = PageRegionType.Center,
                Items = new NotificationList<PageItemConfig>
                {
                    new GridItemConfig
                    {
                        OrderField = Entity.OrderField,
                        OrderDesc = Entity.OrderDesc,
                    }
                }
            });
            if (Entity.TreeUi)
            {
                Main.Regions.Add(new PageRegionConfig
                {
                    RegionType = PageRegionType.Left,
                    Items = new NotificationList<PageItemConfig>
                    {
                        new TreeItemConfig()
                    }
                });
            }
            if (Entity.DetailsPage)
            {
                Details.Regions.Add(new PageRegionConfig
                {
                    RegionType = PageRegionType.Center,
                    Items = new NotificationList<PageItemConfig>
                    {
                        new FormItemConfig
                        {
                            FormCloumn = Entity.FormCloumn
                        }
                    }
                });
            }
            else
            {
                Main.Regions.Add(new PageRegionConfig
                {
                    RegionType = PageRegionType.Dialog,
                    Items = new NotificationList<PageItemConfig>
                    {
                        new FormItemConfig
                        {
                            FormCloumn = Entity.FormCloumn
                        }
                    }
                });
            }
        }

        #endregion

        #region 自定义载入

        /// <summary>
        /// 自定义载入
        /// </summary>
        /// <param name="jObject"></param>
        public void Load(JObject jObject)
        {
            IsUiReadOnly = TryGetValue<bool>(jObject, nameof(IsUiReadOnly));
            PageFolder = TryGetValue<string>(jObject, nameof(PageFolder));
            _properties = new ConfigCollection<UserInterfaceField>();
            if (jObject.TryGetValue(nameof(Properties), StringComparison.OrdinalIgnoreCase, out var token) && token is JObject obj)
            {
                foreach (var field in obj.Values<UserInterfaceField>())
                {
                    field.Parent = this;
                    _properties.Add(field);
                }
            }

            Main = new PageContentConfig
            {
                Regions = new NotificationList<PageRegionConfig>()
            };
            ReadPageCotent(jObject, nameof(Main), Main);

            Details = new PageContentConfig
            {
                Regions = new NotificationList<PageRegionConfig>()
            };
            ReadPageCotent(jObject, nameof(Details), Details);

        }

        T TryGetValue<T>(JObject jObject, string name, T def = default)
        {
            if (jObject.TryGetValue(name, StringComparison.OrdinalIgnoreCase, out var token))
            {
                return token.Value<T>();
            }
            return def;
        }

        static void ReadPageCotent(JObject jObject, string name, PageContentConfig contentConfig)
        {
            if (!jObject.TryGetValue(name, StringComparison.OrdinalIgnoreCase, out var token) || !(token is JObject content))
            {
                return;
            }
            if (!content.TryGetValue(nameof(PageContentConfig.Regions), StringComparison.OrdinalIgnoreCase, out token) || !(token is JObject items))
            {
                return;
            }
            foreach (var item in items.Values().OfType<JObject>())
            {
                var region = new PageRegionConfig()
                {
                    Content = contentConfig
                };
                ReadPageRegion(item, region);
                contentConfig.Regions.Add(region);
            }
        }

        static void ReadPageRegion(JObject jObject, PageRegionConfig regionConfig)
        {
            if (!jObject.TryGetValue(nameof(PageRegionConfig.Items), StringComparison.OrdinalIgnoreCase, out var token) || !(token is JObject items))
            {
                return;
            }
            foreach (var item in items.Values().OfType<JObject>())
            {
                if (!item.TryGetValue(nameof(PageItemConfig.ItemType), out var itemType))
                    continue;

                switch (itemType.Value<string>())
                {
                    case nameof(FormItemConfig):
                        regionConfig.Items.Add(item.ToObject<FormItemConfig>());
                        break;
                    case nameof(GridItemConfig):
                        regionConfig.Items.Add(item.ToObject<GridItemConfig>());
                        break;
                    case nameof(TreeItemConfig):
                        regionConfig.Items.Add(item.ToObject<TreeItemConfig>());
                        break;
                }
            }
        }
        #endregion
    }
}