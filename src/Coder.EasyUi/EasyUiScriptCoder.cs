using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Designer;

namespace Agebull.EntityModel.RobotCoder.EasyUi
{
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    public class EasyUiScriptCoder : EasyUiPageScriptCoderBase
    {
        /// <summary>
        /// 名称
        /// </summary>
        protected override string FileName => "script.js";

        /// <summary>
        /// 名称
        /// </summary>
        protected override string ExFileName => null;

        protected override string BaseCode()
        {
            return $@"

/*
*   {Entity.Caption}的前端操作类对象,实现基本的增删改查的界面操作
*/
var page = {{
    /**
    表格对象
     */
    grid: null,
    /**
     * 标题
     */
    title:'{Entity.Caption}',
    /**
     * 名称
     */
    name: '{Entity.EntityName}',
    /**
     * API前缀
     */
    apiPrefix: '{Project.Abbreviation ?? Project.Name}/{Entity.Abbreviation ?? Entity.Name}/v1/',
    /**
     * 列表的URL
     */
    listUrl: 'edit/list',
    /**
     * 列表的URL的扩展参数
     */
    urlArg: '',
    /**
     * 表单地址
     */
    formUrl: 'Form.htm',
    /**
     * 表单参数
     */
    formArg: '_-_=1',
    /**
     * 是否自动载入数据
     */
    autoLoad: true,
    /**
     * 是否最小系统
     */
    isSmall: false,
    /**
     * 默认支持命令按钮的名称后缀
     */
    cmdElementEx: '',
    /**
     * 表节点名称
     */
    gridId: '#grid',
    /**
     * 工具栏节点名称
     */
    toolbarId: '#pageToolbarEx',
    /**
    * 当前录入是否校验正确
    */
    inputSucceed: true,
    /**
    * {Entity.Caption}的页面初始化
    */
    initialize: function() {{
        var me = this;
        if(!me.isSmall)
            me.initHistoryQuery();
        me.initGrid();
        me.initToolBar();
    }},
    /**
    * 初始化工具栏
    */
    initToolBar: function() {{
        var me = this;{CommandJsCode()}
    }},
    /**
    * 初始化列表表格
    */
    initGrid: function() {{
        var me = this;
        var grid = new GridPanel();
        me.grid=grid;
        grid.tag = me;
        grid.idField = '{Entity.PrimaryColumn.JsonName}';
        grid.cmdPath = page.apiPrefix;{Gridjs()}
        grid.pageSize = 20;
        grid.elementId = this.gridId;
        grid.toolbar = this.toolbarId;
        grid.elementEx = me.cmdElementEx;
        grid.columns = me.columns;
        grid.edit = me.edit;
        grid.addNew = me.addNew;
        if(!me.isSmall){{
            grid.getQueryParams = me.getQueryParams;{GridDetailsScript()}
        }}
        if(me.autoLoad){{
            me.setUrlArgs(me.urlArg);
        }}
        grid.initialize();
    }},
    /**
    * 改变列表参数
     * @param {{string}} args 参数
    */
    setUrlArgs: function (args) {{
        var me = this;
        if (args && args != '') {{
            me.urlArg = args;
            me.formArg = args;
            me.grid.changedUrl(me.apiPrefix + me.listUrl + '?' + args);
        }} else {{
            me.formArg = me.urlArg = '_-_=1';
            me.grid.changedUrl(me.apiPrefix + me.listUrl);
        }}
    }},
    /**
    * 历史查询条件还原
    */
    initHistoryQuery: function() {{
        $('#qKeyWord').textbox('setValue', preQueryArgs.keyWord);
        {InitQueryParams()}
    }},
    /**
    * 读取查询条件
    * @returns {{object}} 查询条件
    */
    getQueryParams: function () {{
        return {{
            keyWord:$('#qKeyWord').textbox('getValue')
            {QueryParams()}
        }};
    }},
    /**
    * 重新载入{Entity.Caption}的列表数据
    */
    reload:function() {{
        $(this.gridId).datagrid('reload');
    }},
    /**
    * 录入界面载入时执行控件初始化
    * @param {{object}} editor 编辑器
    * @param {{Function}} callback 回调
    */
    onFormUiLoaded: function (editor,callback) {{
        var me = editor.ex;
        me.setFormValidate();{InputInitCode()}
        if (callback)
            callback();
    }},
    /**
    * 录入界面数据载入后给Form赋值前,对数据进行预处理
    * @param {{object}} data 数据
    * @param {{object}} editor 编辑器
    */
    onFormDataLoaded: function (data, editor) {{
    }},
    /**
    * 录入界面数据载入后且已给Form赋值,对进行界面逻辑处理
    * @param {{object}} data 数据
    * @param {{object}} editor 编辑器
    */
    afterFormDataLoaded: function (data, editor) {{
        var me = editor.ex;
        me.inputSucceed = true;{DataLoadScript()}
    }},
    /**
    * 设置校验规则
    */
    setFormValidate: function() {{
{ValidateConfig()}
    }},

    /**
     * 生成{Entity.Caption}的编辑器
     * @param {{{Entity.Name}Page}} me 当前页面对象
     * @returns {{EditorDialog}} 编辑器
     */
    createEditor: function (me) {{
        var editor = me.grid.createEditor();
        editor.ex = me;
        editor.title = me.title;
        if(me.grid.auditData) {{
            editor.showValidate = true;
            editor.validatePath = me.apiPrefix + 'audit/validate';
            editor.setFormValidate= me.setFormValidate;
        }}
        editor.onUiLoaded = function (ed, callback) {{
            me.onFormUiLoaded(ed, callback);
        }};
        editor.onDataLoaded = function (data, ed) {{
            me.onFormDataLoaded(data, ed);
        }};
        editor.afterDataLoaded = function (data, ed) {{
            me.afterFormDataLoaded(data, ed);
        }};
        editor.afterSave = function(succeed, data) {{
            me.reload();
        }};
        return editor;
    }},
    /**
    * 新增一条{Entity.Caption}的界面操作
    */
    addNew: function () {{
        var me = this.tag;
        var editor = me.createEditor(me);
        editor.uiUrl = me.formUrl + '?' + me.formArg + '&id=0';
        if(me.urlArg)
            editor.dataUrl = me.apiPrefix + 'edit/details?' + me.urlArg + '&id=';
        else
            editor.dataUrl = me.apiPrefix + 'edit/details?id=';
        editor.saveUrl = me.apiPrefix + 'edit/addnew?id=';
        editor.dataId = 0;
        editor.show();
    }},
    /**
    * 修改或查看{Entity.Caption}的界面操作
    */
    edit: function (id) {{
        var me = this.tag;
        var editor = me.createEditor(me);
        {(Entity.IsUiReadOnly ? "editor.readOnly=true;" : "")}
        editor.uiUrl = me.formUrl + '?' + me.formArg + '&id=' + id;
        editor.dataUrl = me.apiPrefix + 'edit/details?id=';
        editor.saveUrl = me.apiPrefix + 'edit/update?id=';
        editor.dataId = id;
        editor.show();
    }},{CommandJsCode2()}
    /**
    * 列表表格的列信息
    */
    columns:{GridFields()}
}};

/**
 * 依赖功能扩展
 */
mainPageOptions.extend({{
    doPageInitialize: function (callback) {{
        globalOptions.api.customHost = globalOptions.api.{(Project.Abbreviation ?? Project.Name).ToLWord()}Host;
        mainPageOptions.loadPageInfo(page.name, function () {{
            page.initialize();
            callback();
        }});
    }},
    onCheckSize: function (wid, hei) {{
{(Entity.TreeUi ? treeCheckSize : listCheckSize)}
    }}
}});

";
        }

        private string DataLoadScript()
        {
            var code = new StringBuilder();
            foreach (var field in Entity.UserProperty.Where(p => !p.NoneDetails && p.IsImage))
            {
                code.Append($@"
        if (data.{field.JsonName}) {{
            if (data.{field.JsonName}[0] != '/' || data.{field.JsonName}.length > 100){{
                $('#img{field.Name}').attr('src', 'data:image/png;base64,' + data.{field.JsonName});
                data.{field.JsonName} = '*';//防止重复传输
            }}else
                $('#img{field.Name}').attr('src', data.{field.JsonName});
        }}");
            }

            return code.ToString();
        }
    }
}