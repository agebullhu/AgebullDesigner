/**
 * EasyUI for jQuery 1.6.4
 * 
 * Copyright (c) 2009-2018 www.jeasyui.com. All rights reserved.
 *
 * Licensed under the freeware license: http://www.jeasyui.com/license_freeware.php
 * To use it on other terms please contact us: info@jeasyui.com
 *
 */
(function($){
var _1=1;
function _2(_3){
$(_3).addClass("sidemenu");
};
function _4(_5,_6){
var _7=$(_5).sidemenu("options");
if(_6){
$.extend(_7,{width:_6.width,height:_6.height});
}
$(_5)._size(_7);
$(_5).find(".accordion").accordion("resize");
};
function _8(_9,_a,_b){
var _c=$(_9).sidemenu("options");
var tt=$("<ul class=\"sidemenu-tree\"></ul>").appendTo(_a);
tt.tree({data:_b,animate:_c.animate,onBeforeSelect:function(_d){
if(_d.children){
return false;
}
},onSelect:function(_e){
_12(_9,_e.id);
},onExpand:function(_f){
_23(_9,_f);
},onCollapse:function(_10){
_23(_9,_10);
},onClick:function(_11){
if(_11.children){
if(_11.state=="open"){
$(_11.target).addClass("tree-node-nonleaf-collapsed");
}else{
$(_11.target).removeClass("tree-node-nonleaf-collapsed");
}
$(this).tree("toggle",_11.target);
}
}});
tt.unbind(".sidemenu").bind("mouseleave.sidemenu",function(){
$(_a).trigger("mouseleave");
});
_12(_9,_c.selectedItemId);
};
function _13(_14,_15,_16){
var _17=$(_14).sidemenu("options");
$(_15).tooltip({content:$("<div></div>"),position:_17.floatMenuPosition,valign:"top",data:_16,onUpdate:function(_18){
var _19=$(this).tooltip("options");
var _1a=_19.data;
_18.accordion({width:_17.floatMenuWidth,multiple:false}).accordion("add",{title:_1a.text,collapsed:false,collapsible:false});
_8(_14,_18.accordion("panels")[0],_1a.children);
},onShow:function(){
var t=$(this);
var tip=t.tooltip("tip").addClass("sidemenu-tooltip");
tip.children(".tooltip-content").addClass("sidemenu");
tip.find(".accordion").accordion("resize");
tip.add(tip.find("ul.tree")).unbind(".sidemenu").bind("mouseover.sidemenu",function(){
t.tooltip("show");
}).bind("mouseleave.sidemenu",function(){
t.tooltip("hide");
});
t.tooltip("reposition");
},onPosition:function(_1b,top){
var tip=$(this).tooltip("tip");
if(!_17.collapsed){
tip.css({left:-999999});
}else{
if(top+tip.outerHeight()>$(window)._outerHeight()+$(document).scrollTop()){
top=$(window)._outerHeight()+$(document).scrollTop()-tip.outerHeight();
tip.css("top",top);
}
}
}});
};
function _1c(_1d,_1e){
$(_1d).find(".sidemenu-tree").each(function(){
_1e($(this));
});
$(_1d).find(".tooltip-f").each(function(){
var tip=$(this).tooltip("tip");
if(tip){
tip.find(".sidemenu-tree").each(function(){
_1e($(this));
});
$(this).tooltip("reposition");
}
});
};
function _12(_1f,_20){
var _21=$(_1f).sidemenu("options");
_1c(_1f,function(t){
t.find("div.tree-node-selected").removeClass("tree-node-selected");
var _22=t.tree("find",_20);
if(_22){
$(_22.target).addClass("tree-node-selected");
_21.selectedItemId=_22.id;
t.trigger("mouseleave.sidemenu");
_21.onSelect.call(_1f,_22);
}
});
};
function _23(_24,_25){
_1c(_24,function(t){
var _26=t.tree("find",_25.id);
if(_26){
var _27=t.tree("options");
var _28=_27.animate;
_27.animate=false;
t.tree(_25.state=="open"?"expand":"collapse",_26.target);
_27.animate=_28;
}
});
};
function _29(_2a){
var _2b=$(_2a).sidemenu("options");
$(_2a).empty();
if(_2b.data){
$.easyui.forEach(_2b.data,true,function(_2c){
if(!_2c.id){
_2c.id="_easyui_sidemenu_"+(_1++);
}
if(!_2c.iconCls){
_2c.iconCls="sidemenu-default-icon";
}
if(_2c.children){
_2c.nodeCls="tree-node-nonleaf";
if(!_2c.state){
_2c.state="closed";
}
if(_2c.state=="open"){
_2c.nodeCls="tree-node-nonleaf";
}else{
_2c.nodeCls="tree-node-nonleaf tree-node-nonleaf-collapsed";
}
}
});
var acc=$("<div></div>").appendTo(_2a);
acc.accordion({fit:_2b.height=="auto"?false:true,border:_2b.border,multiple:_2b.multiple});
var _2d=_2b.data;
for(var i=0;i<_2d.length;i++){
acc.accordion("add",{title:_2d[i].text,selected:_2d[i].state=="open",iconCls:_2d[i].iconCls,onBeforeExpand:function(){
return !_2b.collapsed;
}});
var ap=acc.accordion("panels")[i];
_8(_2a,ap,_2d[i].children);
_13(_2a,ap.panel("header"),_2d[i]);
}
}
};
function _2e(_2f,_30){
var _31=$(_2f).sidemenu("options");
_31.collapsed=_30;
var acc=$(_2f).find(".accordion");
var _32=acc.accordion("panels");
acc.accordion("options").animate=false;
if(_31.collapsed){
$(_2f).addClass("sidemenu-collapsed");
for(var i=0;i<_32.length;i++){
var _33=_32[i];
if(_33.panel("options").collapsed){
_31.data[i].state="closed";
}else{
_31.data[i].state="open";
acc.accordion("unselect",i);
}
var _34=_33.panel("header");
_34.find(".panel-title").html("");
_34.find(".panel-tool").hide();
}
}else{
$(_2f).removeClass("sidemenu-collapsed");
for(var i=0;i<_32.length;i++){
var _33=_32[i];
if(_31.data[i].state=="open"){
acc.accordion("select",i);
}
var _34=_33.panel("header");
_34.find(".panel-title").html(_33.panel("options").title);
_34.find(".panel-tool").show();
}
}
acc.accordion("options").animate=_31.animate;
};
function _35(_36){
$(_36).find(".tooltip-f").each(function(){
$(this).tooltip("destroy");
});
$(_36).remove();
};
$.fn.sidemenu=function(_37,_38){
if(typeof _37=="string"){
var _39=$.fn.sidemenu.methods[_37];
return _39(this,_38);
}
_37=_37||{};
return this.each(function(){
var _3a=$.data(this,"sidemenu");
if(_3a){
$.extend(_3a.options,_37);
}else{
_3a=$.data(this,"sidemenu",{options:$.extend({},$.fn.sidemenu.defaults,$.fn.sidemenu.parseOptions(this),_37)});
_2(this);
}
_4(this);
_29(this);
_2e(this,_3a.options.collapsed);
});
};
$.fn.sidemenu.methods={options:function(jq){
return jq.data("sidemenu").options;
},resize:function(jq,_3b){
return jq.each(function(){
_4(this,_3b);
});
},collapse:function(jq){
return jq.each(function(){
_2e(this,true);
});
},expand:function(jq){
return jq.each(function(){
_2e(this,false);
});
},destroy:function(jq){
return jq.each(function(){
_35(this);
});
}};
$.fn.sidemenu.parseOptions=function(_3c){
var t=$(_3c);
return $.extend({},$.parser.parseOptions(_3c,["width","height"]));
};
$.fn.sidemenu.defaults={width:200,height:"auto",border:true,animate:true,multiple:true,collapsed:false,data:null,floatMenuWidth:200,floatMenuPosition:"right",onSelect:function(_3d){
}};
})(jQuery);

