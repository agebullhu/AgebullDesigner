globalOptions.appId = "10V6WMADM";
//ajax_ex.baseURL = 'http://localhost';
var vue_option = {
    el: '#body',
    data: {
        user: '',
        password: '',
        message: ''
    },
    filters: {
    },
    methods: {
        login: function () {
            var that = this;
            if (!that.user || that.user.trim() === "" || !that.password || that.password.trim() === "") {
                that.message = "用户名密码不能为空";
                return false;
            }
            ajax_post("登录", "/Authority/v1/login/back", {
                AppId: globalOptions.appId,
                MobilePhone: that.user,
                Password: that.password
            }, function (res) {
                if (!res.success)
                    that.message = res.message;
                else {
                    globalOptions.user.setUserInfo(res.data);
                    window.location.href = "/index.html";
                }
            }, function (res) {
                if (res && !res.message)
                    that.message = res.message;
                else
                    that.message = "登录失败请重试";
            });
            return false;
        }
    }
};
vueObject = new Vue(vue_option);
globalOptions.user.logout(true);
globalOptions.user.refreshDid();