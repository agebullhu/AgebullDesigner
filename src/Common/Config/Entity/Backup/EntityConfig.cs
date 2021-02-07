/*design by:agebull designer date:2017/7/12 23:16:38*/
/*****************************************************
©2008-2017 Copy right by agebull.hu(胡天水)
作者:agebull.hu(胡天水)
工程:Agebull.Common.Config
建立:2014-12-03
修改:2017-07-12
*****************************************************/

using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// 实体配置
    /// </summary>
    partial class EntityConfig
    {
        #region 用户界面

        /// <summary>
        /// 接口名称
        /// </summary>
        [DataMember, JsonProperty("_apiName", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _apiName;

        /// <summary>
        /// 接口名称
        /// </summary>
        /// <remark>
        /// 接口名称
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"解决方案"), DisplayName(@"接口名称"), Description("接口名称")]
        public string ApiName
        {
            get => WorkContext.InCoderGenerating ? _apiName ?? Abbreviation : _apiName;
            set
            {
                if (_apiName == value)
                    return;
                if (value == Name)
                    value = null;
                BeforePropertyChanged(nameof(ApiName), _apiName, value);
                _apiName = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(ApiName));
            }
        }


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

        /// <summary>
        /// 树形界面
        /// </summary>
        [DataMember, JsonProperty("TreeUi", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _treeUi;

        /// <summary>
        /// 树形界面
        /// </summary>
        /// <remark>
        /// 树形界面
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"树形界面"), Description("树形界面")]
        public bool TreeUi
        {
            get => _treeUi;
            set
            {
                if (_treeUi == value)
                    return;
                BeforePropertyChanged(nameof(TreeUi), _treeUi, value);
                _treeUi = value;
                OnPropertyChanged(nameof(TreeUi));
            }
        }

        /// <summary>
        /// 详细编辑页面
        /// </summary>
        [DataMember, JsonProperty("detailsPage", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _detailsPage;

        /// <summary>
        /// 详细编辑页面
        /// </summary>
        [IgnoreDataMember, JsonIgnore, Category(@"用户界面")]
        public bool DetailsPage
        {
            get => _detailsPage;
            set
            {
                if (_detailsPage == value)
                    return;
                BeforePropertyChanged(nameof(DetailsPage), _detailsPage, value);
                _detailsPage = value;
                OnPropertyChanged(nameof(DetailsPage));
            }
        }

        /// <summary>
        /// 编辑页面分几列
        /// </summary>
        [DataMember, JsonProperty("FormCloumn", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal int _formCloumn;

        /// <summary>
        /// 编辑页面分几列
        /// </summary>
        /// <remark>
        /// 编辑页面分几列
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"编辑页面分几列"), Description("编辑页面分几列")]
        public int FormCloumn
        {
            get => _formCloumn;
            set
            {
                if (_formCloumn == value)
                    return;
                BeforePropertyChanged(nameof(FormCloumn), _formCloumn, value);
                _formCloumn = value;
                OnPropertyChanged(nameof(FormCloumn));
            }
        }

        [DataMember, JsonProperty("OrderField", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _orderField;

        [DataMember, JsonProperty("OrderDesc", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _orderDesc;

        /// <summary>
        /// 默认排序字段
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"默认排序字段"), Description("默认排序字段")]
        public string OrderField
        {
            get => _orderField ?? PrimaryField;
            set
            {
                if (_orderField == value)
                    return;
                BeforePropertyChanged(nameof(OrderField), _orderField, value);
                _orderField = value;
                OnPropertyChanged(nameof(OrderField));
            }
        }

        /// <summary>
        /// 默认反序
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"默认反序"), Description("默认反序")]
        public bool OrderDesc
        {
            get => _orderDesc;
            set
            {
                if (_orderDesc == value)
                    return;
                BeforePropertyChanged(nameof(OrderDesc), _orderDesc, value);
                _orderDesc = value;
                OnPropertyChanged(nameof(OrderDesc));
            }
        }


        #endregion

        #region C++

        /// <summary>
        /// C++名称
        /// </summary>
        [DataMember, JsonProperty("CppName", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _cppName;

        /// <summary>
        /// C++名称
        /// </summary>
        /// <remark>
        /// C++字段名称
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"C++"), DisplayName(@"C++名称"), Description("C++字段名称")]
        public string CppName
        {
            get => WorkContext.InCoderGenerating ? _cppName ?? Name : _cppName;
            set
            {
                if (Name == value)
                    value = null;
                if (_cppName == value)
                    return;
                BeforePropertyChanged(nameof(CppName), _cppName, value);
                _cppName = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(CppName));
            }
        }
        #endregion
    }
}