/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/4/22 11:57:16*/
var defRow = {
    selected : false,
    NickName : null,
    IdCard : null,
    certType : 0,
    RealName : null,
    AvatarUrl : null,
    phoneNumber : null,
    birthday : null,
    nation : null,
    tel : null,
    email : null,
    homeAddress : null
};
var rules = {
            'phoneNumber':[
                { max: 20, message: '长度不大于 20 个字符', trigger: 'blur' }
            ]
};
extend_filter({
    //性别枚举转文本
    sexTypeFormater(val) {
        switch (val) {
            case 0: return '未知';
            case 1: return '女';
            case 2: return '男';
            default:return '错误';
        }
    }
});

function doReady() {
    try {
        vue_option.data.apiPrefix = 'user/person/v1';
        vue_option.data.form.def = defRow;
        vue_option.data.form.data = defRow;
        vue_option.data.form.rules = rules; 
        globalOptions.api.customHost = globalOptions.api.userApiHost;
        vueObject = new Vue(vue_option);
        vue_option.methods.loadList();
        
    } catch (e) {
        console.error(e);
    }
}
doReady();