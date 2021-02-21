function MessageWebsocket(opt) {
    that = this;
    this.host = opt.host;
    var enterAction = new beetlexAction("/ws/v1/room/enter");
    var joinAction = new beetlexAction('/ws/v1/room/join');
    var leftAction = new beetlexAction('/ws/v1/room/left');
    this.isEnter = false;
    if (opt.rooms)
        this.rooms = opt.rooms;
    else
        this.rooms = [];

    beetlex.websocket.connected = function () {
        enterAction.post({ token: localStorage.getItem('user_access_token') });
    };

    enterAction.requested = function (r) {
        that.isEnter = r.success;
        if (!that.isEnter)
            return;
        for (var idx = 0; idx < that.rooms.length; idx++) {
            if (that.rooms[idx])
                joinAction.post({ room: that.rooms[idx] });
        }
    };

    this.join = function (room) {
        if (that.isEnter) {
            joinAction.post({ room: room });
        } else {
            that.rooms.push(room);
        }
    };

    this.left = function (room) {
        for (var idx = that.rooms.length - 1; idx >= 0; idx--) {
            if (that.rooms[idx] != room)
                nRoom.push(rooms[idx])
        }
        that.rooms = nRoom;
        if (that.isEnter) {
            leftAction.post({ room: room });
        }
    };
    if (opt.onSocketMessage)
        this.onSocketMessage = opt.onSocketMessage;

    beetlex.websocket.receive = function (r) {
        if (!r.Room)
            return;
        if (that.onSocketMessage && r.Message) {
            try {
                var obj = JSON.parse(r.Message);
                //console.log(obj);
                that.onSocketMessage(r.Room, r.Type, obj);
            } catch (e) {
                console.log(e);
            }
        }
    };
    this.open = function () {
        beetlex.useWebsocket(that.host);
    }
    this.close = function () {
        if (beetlex.websocket.websocket)
            beetlex.websocket.websocket.close();
        beetlex.websocket.websocket = null;
    }
}
