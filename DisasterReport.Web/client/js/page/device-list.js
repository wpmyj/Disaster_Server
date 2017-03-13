$(function () {
    getPageDeviceList();
});

// 分页得到团队list
function getPageDeviceList() {
    $.ajax({
        //Template.RescueTableList
        url: CONFIG.API.Device.GetPageDevice,
        type: 'get',
        dataType: 'json',
        data: {
            pageSize: 6
        },
        success: function (data) {
            Template.DeviceTableList('div.table', data.result.items, function (_data) {
                layui.use(['laypage'], function () {
                    var laypage = layui.laypage;
                    laypage({
                        cont: 'page-layer-device',
                        pages: data.result.totalPage,
                        jump: function (obj, first) {
                            $.ajax({
                                url: CONFIG.API.Device.GetPageDevice,
                                type: 'get',
                                dataType: 'json',
                                data: {
                                    pageIndex: obj.curr,
                                    pageSize: 6
                                },
                                success: function (d) {
                                    if (d.success === true) {
                                        var tableBy = document.querySelector('table.table tbody');
                                        var html = '';
                                        for(var i = 0; i < d.result.items.length; i++) {
                                            html += '<tr class="first">'
                                                 +'     <td>'
                                                 +'         <img src="img/default-device.png" class="avatar hidden-phone" />'
                                                 +'         <a href="#" class="name">'+ d.result.items[i].deviceCode +'</a>'
                                                 +'         <span class="subtext">'+ d.result.items[i].type +'</span>'
                                                 +'     </td>'
                                                 +'     <td>'
                                                 +'         '+ d.result.items[i].reporter.name
                                                 +'     </td>'
                                                 +'     <td>'
                                                 +'         '+ d.result.items[i].areaAddress
                                                 +'     </td>'
                                                 +'     <td class="align-right">'
                                                 +'         <a data-id="'+ d.result.items[i].id +'" href="javascript:;">解除绑定</a>'
                                                 +'     </td>'
                                                 +' </tr>'
                                        }
                                        tableBy.innerHTML = html;
                                    }
                                }
                            });
                        }
                    });
                });
            });
        }
    });
}

function bindTableClickEvent() {

}