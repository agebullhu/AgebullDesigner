﻿function UrlHelper() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0].toLowerCase()] = hash[1];
    }
    this.queryString = vars;
    this.tag = null;
    this.ssl = window.location.protocol == "https:"
    var url = document.location.pathname;
    var tagIndex = document.location.href.indexOf('#');
    if (tagIndex > 0) {
        this.tag = document.location.href.substring(tagIndex + 1);
    }
    this.folder = url.substring(url.indexOf('/'), url.lastIndexOf('/'));
    url = url.substring(0, (url.indexOf("#") == -1) ? url.length : url.indexOf("#"));
    url = url.substring(0, (url.indexOf("?") == -1) ? url.length : url.indexOf("?"));
    url = url.substring(url.lastIndexOf("/") + 1, url.length);
    if (url) {
        this.fileName = decodeURIComponent(url);
        this.ext = this.fileName.substring(this.fileName.lastIndexOf(".") + 1, this.fileName.length)
        this.fileNameWithOutExt = this.fileName.substring(0, this.fileName.lastIndexOf(".") == -1 ? this.fileName.length : this.fileName.lastIndexOf("."));
    }

}

var _url = new UrlHelper();

function beetlexWebSocket() {
    this.wsUri = null;
    if (window.location.protocol == "https:") {
        this.wsUri = "wss://" + window.location.host;
    }
    else {
        this.wsUri = "ws://" + window.location.host;
    }
    this.websocket;
    this.status = false;
    this.messagHandlers = new Object();
    this.timeout = 2000;
    this.receive = null;
    this.disconnect = null;
    this.connected = null;
}

beetlexWebSocket.prototype.send = function (url, params, callback) {
    if (this.status == false) {
        if (callback != null) {
            callback({ url: url, code: 505, message: 'disconnect' })
        }
    }
    this.messagHandlers[params._requestid] = callback;
    var data = { url: url, params: params };
    this.websocket.send(JSON.stringify(data));
}

beetlexWebSocket.prototype.onOpen = function (evt) {
    this.status = true;
    if (this.connected)
        this.connected();
}

beetlexWebSocket.prototype.onClose = function (evt) {
    this.status = false;
    var _this = this;
    if (this.disconnect)
        this.disconnect();
    if (evt.code == 1006) {
        setTimeout(function () {
            _this.connect();
        }, _this.timeout);
        if (_this.timeout < 10000)
            _this.timeout += 1000;
    }

}

beetlexWebSocket.prototype.onMessage = function (evt) {
    var msg = JSON.parse(evt.data);
    var callback = this.messagHandlers[msg.id];
    if (callback)
        callback(msg);
    else
        if (this.receive) {
            if (msg.data != null && msg.data != undefined)
                this.receive(msg.data);
            else
                this.receive(msg);
        }
}

beetlexWebSocket.prototype.onReceiveMessage = function (callback) {
    this.callback = callback;
};

beetlexWebSocket.prototype.onError = function (evt) {

}

beetlexWebSocket.prototype.connect = function () {
    this.websocket = new WebSocket(this.wsUri);
    _this = this;
    this.websocket.onopen = function (evt) { _this.onOpen(evt) };
    this.websocket.onclose = function (evt) { _this.onClose(evt) };
    this.websocket.onmessage = function (evt) { _this.onMessage(evt) };
    this.websocket.onerror = function (evt) { _this.onError(evt) };
}

function beetlex4axios() {
    this._requestid = 1;
    this.errorHandlers = new Object();
    this.websocket = new beetlexWebSocket();
}

beetlex4axios.prototype.useWebsocket = function (host) {
    if (host)
        this.websocket.wsUri = host;
    var _this = this;
    this.websocket.connect();
}

beetlex4axios.prototype.getErrorHandler = function (code) {
    return this.errorHandlers[code];
}

beetlex4axios.prototype.SetErrorHandler = function (code, callback) {
    this.errorHandlers[code] = callback;
}

beetlex4axios.prototype.getRequestID = function () {
    if (++this._requestid > 65536) {
        this._requestid = 1;
    }

    return this._requestid;
}

beetlex4axios.prototype.get = function (url, params, callback) {
    var httpurl = url;
    if (!params)
        params = new Object();
    var _this = this;
    params['_requestid'] = id;
    //params['_token'] = globalOptions.getAuthorization();
    if (this.websocket.status == true) {
        var wscallback = function (r) {
            var data = r.data;
            if (!data.success) {
                _this.onError(data.code, data.message);
            }
            else {
                if (callback) {
                    if (data.data != null && data.data != undefined)
                        callback(data.data);
                    else
                        callback(data);
                }
            }
        };
        this.websocket.send(url, params, wscallback);
    }
    else {
        axios.get(httpurl, { params: params, headers: { 'Content-Type': 'application/json;charset=UTF-8' } })
            .then(function (response) {
                var data = response.data;
                if (!data.success) {
                    _this.onError(data.code, data.message);
                }
                else {
                    if (callback) {
                        if (data.data != null && data.data != undefined)
                            callback(data.data);
                        else
                            callback(data);
                    }
                }
            })
            .catch(function (error) {
                var code = error.response ? error.response.status : 500;
                var message = error.message;
                if (error.response)
                    message += "\r\n" + error.response.data;
                _this.onError(code, message);
            });
    }
};

beetlex4axios.prototype.onError = function (code, message) {
    var handler = this.getErrorHandler(code);
    if (handler)
        handler(message);
    else
        alert(message);
}

beetlex4axios.prototype.post = function (url, params, callback) {
    var httpurl = url;
    if (!params)
        params = new Object();
    var id = this.getRequestID();
    var _this = this;
    params['_requestid'] = id;
    //params['_token'] = globalOptions.getAuthorization();

    if (this.websocket.status == true) {
        var wscallback = function (r) {
            var data = r;
            if (!data.success) {
                _this.onError(data.code, data.message);
            }
            else {
                if (callback) {
                    if (data.data != null && data.data != undefined)
                        callback(data.data);
                    else
                        callback(data);
                }
            }
        };
        this.websocket.send(url, params, wscallback);
    }
    else {
        axios.post(httpurl, JSON.stringify(params), {
            headers: {
                'Content-Type': 'application/json;charset=UTF-8',
                "x-zmvc-app": globalOptions.appId,
                "x-zmvc-page-title": page,
                "x-zmvc-action-code": action,
                "x-zmvc-action-title": title,
                "Authorization": globalOptions.getAuthorization()
            }
        }).then(function (response) {
            var data = response.data;
            if (!data.success) {
                _this.onError(data.code, data.message);
            }
            else {
                if (callback) {
                    if (data.data != null && data.data != undefined)
                        callback(data.data);
                    else
                        callback(data);
                }
            }
        }).catch(function (error) {
            var code = error.response ? error.response.status : 500;
            var message = error.message;
            if (error.response)
                message += "\r\n" + error.response.data;
            _this.onError(code, message);
        });
    }
};

var beetlex = new beetlex4axios();

function beetlexAction(actionUrl, actionData, defaultResult) {
    this.url = actionUrl;
    this.data = actionData;
    this.result = defaultResult;
    this.requesting = null;
    this.requested = null;

}

beetlexAction.prototype.onCallback = function (data) {
    if (this.requested)
        this.requested(data);
}

beetlexAction.prototype.onValidate = function (data) {
    if (this.requesting)
        return this.requesting(data);
    return true;
}

beetlexAction.prototype.get = function (data) {
    var _this = this;
    var _postData = this.data;
    if (data)
        _postData = data;
    if (!this.onValidate(_postData))
        return;
    beetlex.get(this.url, _postData, function (r) {

        _this.result = r;
        _this.onCallback(r);
    });
};

beetlexAction.prototype.post = function (data) {
    var _this = this;
    var _postData = this.data;
    if (data)
        _postData = data;
    if (!this.onValidate(_postData))
        return;
    beetlex.post(this.url, _postData, function (r) {
        _this.result = r;
        _this.onCallback(r);
    });

};


