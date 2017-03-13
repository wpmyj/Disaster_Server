(function (root, factory) {
    if (typeof exports === "object") {
        module.exports = factory();
    } else {
        root.SignalRHub = factory();
    }
}(this, function () {

    if (typeof SignalRHub === 'undefined') {
        var SignalRHub = {};
    } else if (typeof SignalRHub === 'object') {
        SignalRHub = SignalRHub;
    }

    // SignalRHub.con = $.hubConnection('http://192.168.31.160/disaster/signalr', { useDefaultPath: false });
    SignalRHub.con = $.hubConnection('http://localhost:61759/signalr', { useDefaultPath: false });
    SignalRHub.hub = SignalRHub.con.createHubProxy('messageHub');

    SignalRHub.con.start().done(function () {
        SignalRHub.hub.invoke('WebLogin', {
            reporterId: sessionStorage.getItem('reporterId'),
            name: sessionStorage.getItem('reporterName')
        });
    }).fail(function () { console.log('Could not connect'); });;


    SignalRHub.hub.on('sendToWebMessage', function (user) {
        alert(user);
    });

    SignalRHub.hub.on('postMessage', function (msg) {
        console.log(msg);
        // 如果是移动端上报灾情通知
        if (msg.type === 'disasterReport') {
            // 找到对应的灾情显示通知在前端
            $.ajax({
                url: CONFIG.API.DisasterService.GetDisasterById,
                type: 'get',
                data: {
                    id: msg.content.disasterInfoId
                },
                dataType: 'json',
                success: function (data) {
                    if (data.success === true) {
                        var disasterTrigger = document.querySelectorAll('a.trigger')[0];
                        var disasterNotifications = document.querySelector('div.notifications');
                        var newItem = document.createElement('a');
                        newItem.href = '#';
                        newItem.className = 'item';
                        newItem.dataset.id = msg.content.disasterInfoId;
                        newItem.innerHTML = '<i class="icon-envelope-alt"></i> 来自' + data.result.reporter.name + '的灾情上报' +
                            '<span class="time"><i class="icon-time"></i> ' + data.result.reportDate.replace('T', ' ') + '</span>';

                        if (disasterNotifications.children.length >= 2) {
                            disasterNotifications.insertBefore(newItem, disasterNotifications.children[1])
                        } else {
                            disasterNotifications.appendChild(newItem);
                        }

                        newItem.onclick = function (e) {
                            e = e || window.event;
                            e.stopPropagation ? e.stopPropagation() : e.cancelBubble = true;
                            var _this = this;
                            // 先找到对应的图片信息
                            $.ajax({
                                url: CONFIG.API.Disaster.GetDisasterFilePicById,
                                type: 'get',
                                dataType: 'json',
                                data: {
                                    id: this.dataset.id
                                },
                                success: function (dd) {
                                    if (dd.success === true) {
                                        var detailInfo = $.extend(data.result, { file: dd.result });
                                        var htmlContent = Template.DisasterDetailInfo(detailInfo);

                                        layer.open({
                                            type: 1,
                                            title: ['隐患详情', 'background: #3892D3; border-color: #3892D3; color: #fff;'],
                                            area: ['750px', '600px'],
                                            skin: 'zxyui-zxyer-rim', //加上边框
                                            content: htmlContent,
                                            btn: ['关闭']
                                        });

                                        // 然后移除此条记录
                                        disasterNotifications.removeChild(_this);
                                        disasterNotifications.children[0].innerHTML = '<h3>你有' + (disasterNotifications.children.length - 1) + '条新的消息通知</h3>';
                                        var count = disasterTrigger.querySelector('span.count');
                                        count.innerHTML = (disasterNotifications.children.length - 1);
                                    }
                                },
                                error: function (e) {

                                }
                            });

                        };

                        disasterNotifications.children[0].innerHTML = '<h3>你有' + (disasterNotifications.children.length - 1) + '条新的消息通知</h3>';

                        var count = disasterTrigger.querySelector('span.count');
                        count.innerHTML = (disasterNotifications.children.length - 1);

                        // 触发显示
                        disasterTrigger.classList.add('active');
                        disasterTrigger.nextElementSibling.classList.add('is-visible');
                    }
                },
                error: function (e) {
                    console.log(e);
                }
            });

        }

    });

    return SignalRHub;
}));