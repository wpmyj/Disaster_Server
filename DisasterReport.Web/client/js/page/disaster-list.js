var XYACMAP = null;
$(function () {
    layui.use(['layer'], function () {
        var layer = layui.layer;

        // 创建地图
        XYACMAP = new XYACMapOl3.Map();

        // 分页获取所有隐患item
        getPageDisaster();

        // 增加一个popup
        XYACMAP.addPopupLayer({
            container: '#tpl_popupcontainer',
            content: '#tpl_popupcontainer_content',
            closer: '#tpl_popupcontainer_closer',
            id: 'disasterPopup'
        });

        bindSelectDisasterReporter();

        bindMapTopEvent();
    });
});

// 分页获取所有隐患item
function getPageDisaster() {
    $.ajax({
        url: CONFIG.API.Disaster.GetPageDisaster,
        type: 'get',
        dataType: 'json',
        data: {
            pageSize: 6
        },
        success: function (data) {
            // page-layer-disaster
            if (data.success === true) {
                // 加载右侧灾情item模板
                Template.DisasterListItem('div.disaster-list', data.result.items, function (data_, items) {
                    for (var i = 0; i < items.length; i++) {
                        // 单击事件
                        items[i].onclick = function (e) {
                            e = e || window.event;
                            e.stopepropagation ? e.stopepropagation() : e.cancelBubble = true;


                            XYACMAP.setPopupContent({
                                id: 'disasterPopup',
                                content: Template.DisasterDetailPopup(data_[parseInt(this.dataset.index)])
                            }).showPopup({
                                id: 'disasterPopup',
                                center: [data_[parseInt(this.dataset.index)].lng, data_[parseInt(this.dataset.index)].lat]
                            });


                            // 定位到对应的位置
                            XYACMAP.getView().setCenter({
                                zoom: 14,
                                center: [data_[parseInt(this.dataset.index)].lng, data_[parseInt(this.dataset.index)].lat]
                            });

                            e.preventDefault();
                            return false;
                        };

                        // 双击事件
                        items[i].ondblclick = function (e) {
                            e = e || window.event;
                            e.stopepropagation ? e.stopepropagation() : e.cancelBubble = true;
                            var _this = this;
                            // 先找到对应的图片信息
                            $.ajax({
                                url: CONFIG.API.Disaster.GetDisasterFilePicById,
                                type: 'get',
                                dataType: 'json',
                                data: {
                                    id: this.dataset.id
                                },
                                success: function (data) {
                                    if (data.success === true) {
                                        var detailInfo = $.extend(data_[parseInt(_this.dataset.index)], { file: data.result });
                                        var htmlContent = Template.DisasterDetailInfo(detailInfo);

                                        layer.open({
                                            type: 1,
                                            title: ['隐患详情', 'background: #3892D3; border-color: #3892D3; color: #fff;'],
                                            area: ['750px', '600px'],
                                            skin: 'zxyui-zxyer-rim', //加上边框
                                            content: htmlContent,
                                            btn: ['关闭', '销结', '号召抢救'],
                                            btn3: function(index) {
                                                var disasterDetailId = document.querySelector('#disasterDetailId');

                                                // 发送给APP终端抢救消息
                                                SignalRHub.hub.invoke('CallReporter', disasterDetailId.value);
                                            }
                                        });
                                    }
                                },
                                error: function (e) {

                                }
                            });

                            e.preventDefault();
                            return false;
                        };
                    }
                });

                // 构造灾情地理信息
                var _features = [];
                for (var i = 0; i < data.result.items.length; i++) {
                    _features.push({
                        type: 'point',
                        attr: data.result.items[i],
                        path: [data.result.items[i].lng, data.result.items[i].lat]
                    });
                }

                // 增加到地图信息
                XYACMAP.addLayer({
                    id: 'disaster',
                    isReturnBefore: true
                }).clearAll().addFeatures(_features).setStyle({
                    image: new ol.style.Icon({
                        anchor: [0.5, 20],
                        anchorOrigin: 'top-right',
                        anchorXUnits: 'fraction',
                        anchorYUnits: 'pixels',
                        offsetOrigin: 'top-right',
                        opacity: 1,
                        src: './img/flag.png'
                    })
                });

                // 构造分页
                layui.use(['laypage'], function () {
                    var laypage = layui.laypage;
                    laypage({
                        cont: 'page-layer-disaster',
                        pages: data.result.totalPage,
                        jump: function (obj, first) {
                            if (!first) {
                                $.ajax({
                                    url: CONFIG.API.Disaster.GetPageDisaster,
                                    type: 'get',
                                    dataType: 'json',
                                    data: {
                                        pageIndex: obj.curr,
                                        pageSize: 6
                                    },
                                    success: function (data) {
                                        // 加载右侧灾情item模板
                                        Template.DisasterListItem('div.disaster-list', data.result.items, function (data_, items) {
                                            for (var i = 0; i < items.length; i++) {
                                                // 单击事件
                                                items[i].onclick = function (e) {
                                                    e = e || window.event;
                                                    e.stopepropagation ? e.stopepropagation() : e.cancelBubble = true;

                                                    XYACMAP.setPopupContent({
                                                        id: 'disasterPopup',
                                                        content: Template.DisasterDetailPopup(data_[parseInt(this.dataset.index)])
                                                    }).showPopup({
                                                        id: 'disasterPopup',
                                                        center: [data_[parseInt(this.dataset.index)].lng, data_[parseInt(this.dataset.index)].lat]
                                                    });

                                                    // 定位到对应的位置
                                                    XYACMAP.getView().setCenter({
                                                        zoom: 14,
                                                        center: [data_[parseInt(this.dataset.index)].lng, data_[parseInt(this.dataset.index)].lat]
                                                    });

                                                    e.preventDefault();
                                                    return false;
                                                };

                                                // 双击事件
                                                items[i].ondblclick = function (e) {
                                                    e = e || window.event;
                                                    e.stopepropagation ? e.stopepropagation() : e.cancelBubble = true;

                                                    var _this = this;
                                                    // 先找到对应的图片信息
                                                    $.ajax({
                                                        url: CONFIG.API.Disaster.GetDisasterFilePicById,
                                                        type: 'get',
                                                        dataType: 'json',
                                                        data: {
                                                            id: this.dataset.id
                                                        },
                                                        success: function (data) {
                                                            if (data.success === true) {
                                                                var detailInfo = $.extend(data_[parseInt(_this.dataset.index)], { file: data.result });
                                                                var htmlContent = Template.DisasterDetailInfo(detailInfo);

                                                                layer.open({
                                                                    type: 1,
                                                                    title: ['隐患详情', 'background: #3892D3; border-color: #3892D3; color: #fff;'],
                                                                    area: ['750px', '600px'],
                                                                    skin: 'zxyui-zxyer-rim', //加上边框
                                                                    content: htmlContent,
                                                                    btn: ['关闭']
                                                                });
                                                            }
                                                        },
                                                        error: function (e) {

                                                        }
                                                    });

                                                };
                                            }
                                        });

                                        // 构造灾情地理信息
                                        var _features = [];
                                        for (var i = 0; i < data.result.items.length; i++) {
                                            _features.push({
                                                type: 'point',
                                                attr: data.result.items[i],
                                                path: [data.result.items[i].lng, data.result.items[i].lat]
                                            });
                                        }

                                        // 增加到地图信息
                                        XYACMAP.addLayer({
                                            id: 'disaster',
                                            isReturnBefore: true
                                        }).clearAll().addFeatures(_features).setStyle({
                                            image: new ol.style.Icon({
                                                anchor: [0.5, 20],
                                                anchorOrigin: 'top-right',
                                                anchorXUnits: 'fraction',
                                                anchorYUnits: 'pixels',
                                                offsetOrigin: 'top-right',
                                                opacity: 1,
                                                src: './img/flag.png'
                                            })
                                        });
                                    }
                                });
                            }
                        }
                    });
                });
            }
        },
        error: function (e) {
            console.log(e);
        }
    });
}

// 分页获取所有人员item
function getPageReporter() {
    $.ajax({
        url: CONFIG.API.Reporter.GetPageReporter,
        type: 'get',
        dataType: 'json',
        data: {
            pageSize: 6,
            type: 1
        },
        success: function (data) {
            // page-layer-disaster
            if (data.success === true) {
                // 加载右侧灾情item模板
                Template.ReporterListItem('div.disaster-list', data.result.items, function (data_, items) {
                    for (var i = 0; i < items.length; i++) {
                        // 单击事件
                        items[i].onclick = function (e) {
                            e = e || window.event;
                            e.stopepropagation ? e.stopepropagation() : e.cancelBubble = true;


                            XYACMAP.setPopupContent({
                                id: 'disasterPopup',
                                content: Template.ReporterDetailPopup(data_[parseInt(this.dataset.index)])
                            }).showPopup({
                                id: 'disasterPopup',
                                center: [data_[parseInt(this.dataset.index)].lastLng, data_[parseInt(this.dataset.index)].lastLat]
                            });


                            // 定位到对应的位置
                            XYACMAP.getView().setCenter({
                                zoom: 14,
                                center: [data_[parseInt(this.dataset.index)].lastLng, data_[parseInt(this.dataset.index)].lastLat]
                            });

                            e.preventDefault();
                            return false;
                        };
                    }
                });

                // 构造灾情地理信息
                var _features = [];
                for (var i = 0; i < data.result.items.length; i++) {
                    _features.push({
                        type: 'point',
                        attr: data.result.items[i],
                        path: [data.result.items[i].lastLng, data.result.items[i].lastLat]
                    });
                }

                // 增加到地图信息
                XYACMAP.addLayer({
                    id: 'disaster',
                    isReturnBefore: true
                }).clearAll().addFeatures(_features).setStyle({
                    image: new ol.style.Icon({
                        anchor: [0.5, 20],
                        anchorOrigin: 'top-right',
                        anchorXUnits: 'fraction',
                        anchorYUnits: 'pixels',
                        offsetOrigin: 'top-right',
                        opacity: 1,
                        src: './img/inspector.png'
                    })
                });

                // 构造分页
                layui.use(['laypage'], function () {
                    var laypage = layui.laypage;
                    laypage({
                        cont: 'page-layer-disaster',
                        pages: data.result.totalPage,
                        jump: function (obj, first) {
                            if (!first) {
                                $.ajax({
                                    url: CONFIG.API.Reporter.GetPageReporter,
                                    type: 'get',
                                    dataType: 'json',
                                    data: {
                                        pageIndex: obj.curr,
                                        pageSize: 6,
                                        type: 1
                                    },
                                    success: function (data) {
                                        // 加载右侧灾情item模板
                                        Template.ReporterListItem('div.disaster-list', data.result.items, function (data_, items) {
                                            for (var i = 0; i < items.length; i++) {
                                                // 单击事件
                                                items[i].onclick = function (e) {
                                                    e = e || window.event;
                                                    e.stopepropagation ? e.stopepropagation() : e.cancelBubble = true;

                                                    XYACMAP.setPopupContent({
                                                        id: 'disasterPopup',
                                                        content: Template.ReporterDetailPopup(data_[parseInt(this.dataset.index)])
                                                    }).showPopup({
                                                        id: 'disasterPopup',
                                                        center: [data_[parseInt(this.dataset.index)].lastLng, data_[parseInt(this.dataset.index)].lastLat]
                                                    });

                                                    // 定位到对应的位置
                                                    XYACMAP.getView().setCenter({
                                                        zoom: 14,
                                                        center: [data_[parseInt(this.dataset.index)].lastLng, data_[parseInt(this.dataset.index)].lastLat]
                                                    });

                                                    e.preventDefault();
                                                    return false;
                                                };
                                            }
                                        });

                                        // 构造灾情地理信息
                                        var _features = [];
                                        for (var i = 0; i < data.result.items.length; i++) {
                                            _features.push({
                                                type: 'point',
                                                attr: data.result.items[i],
                                                path: [data.result.items[i].lastLng, data.result.items[i].lastLat]
                                            });
                                        }

                                        // 增加到地图信息
                                        XYACMAP.addLayer({
                                            id: 'disaster',
                                            isReturnBefore: true
                                        }).clearAll().addFeatures(_features).setStyle({
                                            image: new ol.style.Icon({
                                                anchor: [0.5, 20],
                                                anchorOrigin: 'top-right',
                                                anchorXUnits: 'fraction',
                                                anchorYUnits: 'pixels',
                                                offsetOrigin: 'top-right',
                                                opacity: 1,
                                                src: './img/inspector.png'
                                            })
                                        });
                                    }
                                });
                            }
                        }
                    });
                });
            }
        },
        error: function (e) {
            console.log(e);
        }
    });
}

// 绑定灾情与上报人员的切换按钮
function bindSelectDisasterReporter() {
    var select_disaster_btn = document.querySelector('#select-disaster-btn');
    var select_reporter_btn = document.querySelector('#select-reporter-btn');

    // 灾情选择按钮
    select_disaster_btn.onclick = function (e) {
        e = e || window.event;
        e.stopepropagation ? e.stopepropagation() : e.cancelBubble = true;

        if (this.classList.contains('active')) {
            return false;
        } else {
            select_reporter_btn.classList.remove('active');
            this.classList.add('active');
        }

        XYACMAP.hiddenPopup();

        getPageDisaster();
    };

    // 灾情选择按钮
    select_reporter_btn.onclick = function (e) {
        e = e || window.event;
        e.stopepropagation ? e.stopepropagation() : e.cancelBubble = true;

        if (this.classList.contains('active')) {
            return false;
        } else {
            select_disaster_btn.classList.remove('active');
            this.classList.add('active');
        }

        XYACMAP.hiddenPopup();

        getPageReporter();
    };
}

// 绑定地图上的topbar工具栏事件
function bindMapTopEvent() {
    // 定位按钮
    var setCenterBtn = document.querySelector('#setCenterBtn');
    var searchLng = document.querySelector('#searchLng');
    var searchLat = document.querySelector('#searchLat');
    setCenterBtn.onclick = function(e) {
        e = e || window.event;
        e.stopepropagation ? e.stopepropagation() : e.cancelBubble = true;

        // 定位到对应的位置
        XYACMAP.getView().setCenter({
            zoom: 8,
            center: [parseFloat(searchLng.value), parseFloat(searchLat.value)]
        });
    };
}