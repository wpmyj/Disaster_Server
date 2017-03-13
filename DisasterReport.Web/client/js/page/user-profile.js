$(function () {

    // 权限验证？

    var reporterId = sessionStorage.getItem('reporterId');
    // 先判断此人是否有上报过灾情
    $.ajax({
        url: CONFIG.API.Disaster.GetPageDisasterByReporterId,
        type: 'get',
        dataType: 'json',
        data: {
            id: reporterId,
            pageSize: 4
        },
        success: function (data) {
            if (data.success === true) {
                if (data.result.items.length > 0) {
                    var _data = {};
                    _data.reporter = data.result.items[0].reporter;
                    _data.disaster = [];

                    for (var i = 0; i < data.result.items.length; i++) {
                        var temp = $.extend({}, data.result.items[i]);
                        delete temp.disaster;
                        _data.disaster.push(temp);
                    }

                    Template.ReporterProfile('#pad-wrapper', _data, function (__data) {

                        // 注册分页
                        layui.use(['laypage'], function () {
                            var laypage = layui.laypage;
                            laypage({
                                cont: 'page-layer-profile',
                                pages: data.result.totalPage,
                                jump: function (obj, first) {
                                    $.ajax({
                                        url: CONFIG.API.Disaster.GetPageDisasterByReporterId,
                                        type: 'get',
                                        dataType: 'json',
                                        data: {
                                            pageIndex: obj.curr,
                                            pageSize: 4,
                                            id: reporterId
                                        },
                                        success: function (d) {
                                            if (d.success === true) {
                                                var disasterTable = document.querySelector('#disasterTable');
                                                var html = '';
                                                for (var i = 0; i < d.result.items.length; i++) {
                                                    html += '   <tr ' + (i === 0 ? 'class="first"' : '') + '>'
                                                        + '          <td>'
                                                        + '              <a href="#">' + d.result.items[i].disasterKind.name + '</a>'
                                                        + '          </td>'
                                                        + '          <td>'
                                                        + '              ' + d.result.items[i].reportDate.replace('T', '')
                                                        + '          </td>'
                                                        + '          <td>'
                                                        + '              ' + d.result.items[i].disasterAddress
                                                        + '          </td>'
                                                        + '          <td>'
                                                        + '              ' + (d.result.items[i].status === 0 ? '没有处理' : (d.result.items[i].status === 1 ? '正在处理' : '已处理'))
                                                        + '          </td>'
                                                        + '      </tr>';
                                                }
                                                disasterTable.querySelector('tbody').innerHTML = html;
                                            }
                                        }
                                    });
                                }
                            });
                        });

                        // 获取地址定位
                        getAddressInfo(__data.reporter.lastLng, __data.reporter.lastLat);
                        renderMap(__data.reporter.lastLng, __data.reporter.lastLat);
                    });
                } else {
                    getReporterProfile(reporterId);
                }
            }
        },
        error: function (e) {
            console.log(e);
        }
    });

});

function getReporterProfile(id) {
    // 获得此上报人信息
    $.ajax({
        url: CONFIG.API.Reporter.GetReporterById,
        dataType: 'json',
        type: 'get',
        data: {
            id: id
        },
        success: function (data) {
            if (data.success === true) {
                var _data = {};
                _data.reporter = data.result;
                _data.disaster = [];

                Template.ReporterProfile('#pad-wrapper', _data, function (__data) {
                    getAddressInfo(__data.reporter.lastLng, __data.reporter.lastLat);
                    renderMap(__data.reporter.lastLng, __data.reporter.lastLat);
                });
            }
        },
        error: function (e) {
            console.log(e);
        }
    });
}

function getAddressInfo(lng, lat) {
    $.ajax({
        url: CONFIG.API.City.GetCityInfoByLngLat,
        dataType: 'jsonp',
        type: 'get',
        data: {
            ak: 'C93b5178d7a8ebdb830b9b557abce78b',
            location: lat + ',' + lng,
            output: 'json'
        },
        success: function (data) {
            document.querySelector('#lastAddress').innerHTML = data.result.formatted_address
        },
        error: function (e) {
            console.log(e);
        }
    });
}

// 渲染地图
function renderMap(lng, lat) {
    // 创建地图
    XYACMAP = new XYACMapOl3.Map();

    // 构造灾情地理信息
    var _features = [];
    _features.push({
        type: 'point',
        attr: null,
        path: [lng, lat]
    });

    // 增加到地图信息
    XYACMAP.addLayer({
        id: 'reporter',
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

    // 定位到对应的位置
    XYACMAP.getView().setCenter({
        zoom: 14,
        center: [lng, lat]
    });
}