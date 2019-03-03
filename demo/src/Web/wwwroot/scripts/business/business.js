var ProjectTreeExtend = {
    //当前的招商项目ID
    curMid: 0,
    //当前的在建项目ID
    curCid: 0,
    //当前的投产项目ID
    curOid: 0,
    //当前的项目ID
    curId: 0,
    //当前的项目类型
    curType: "root",
    //当前的机构ID
    curOrgId: 0,
    //选择变化的回调,参数为此对象
    onProjectSelected: null,
    /*
        重新载入树
    */
    reload: function () {
        $("#tree").tree("reload");
    },
    /**
     * 初始化树
     * @param {string} url url
     */
    initialize: function (url) {
        var me = this;
        if (!url)
            url = "Action.aspx?action=tree";
        $("#tree").tree({
            url: url,
            iconCls: "icon-details",
            lines: true,
            onSelect: function (node) {
                me.onSelect(node);
            }
        });
    },
    onSelect: function (node) {
        var me = this;
        me.curId = parseInt(node.tag);
        me.curType = node.attributes;
        if (node.attributes === window.PlanLevel) {
            me.curMid = id;
            me.curOrgId = parseInt(node.extend);
        }
        else if (node.attributes === window.ConstructionLevel) {
            me.curCid = id;
            me.curOrgId = parseInt(node.extend);
        }
        if (node.attributes === window.OperationLevel) {
            me.curOid = id;
            me.curOrgId = parseInt(node.extend);
        }
        else if (node.attributes === window.OrganizationAbbr) {
            me.curOrgId = me.curId;
        }
        else {
            return;
        }
        if (me.onProjectSelected)
            me.onProjectSelected(me);
    }
};


var TreeExtend = {
    //当前的ID
    curId: 0,
    //当前的类型
    curType: "root",
    //选择变化的回调,参数为此curId,curType,node
    onTreeSelected: null,
    /*
        重新载入树
    */
    reload: function () {
        $("#tree").tree("reload");
    },
    /**
     * 初始化树
     * @param {string} url url
     */
    initialize: function (url) {
        var me = this;
        if (!url)
            url = "Action.aspx?action=tree";
        $("#tree").tree({
            url: url,
            iconCls: "icon-details",
            lines: true,
            onSelect: function (node) {
                me.onSelect(node);
            }
        });
    },
    onSelect: function (node) {
        var me = this;
        console.log("tree=>id:%d,type:%s,tag:%s,children:%d", node.id, node.attributes, node.tag, !node.children ? 0 : node.children.length);
        me.curId = parseInt(node.id);
        me.curType = node.attributes;
        me.linkages.forEach(function (val, idx, arr) {
            if (!val.condition(node)) {
                disableButton(val.button);
            } else {
                enableButton(val.button);
            }
        });
        if (me.onTreeSelected) {
            me.onTreeSelected(me.curId, me.curType, node);
        }
    },
    /**
     * 联动的按钮
     */
    linkages: [],
    /**
     * 注册联运按钮
     * @param {string} btn a
     * @param {string} condition a
     */
    regLinkage: function (btn, condition) {
        disableButton(btn);
        this.linkages.push({ button: btn, condition: condition });
    }
};





