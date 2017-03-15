$(function () {
    getPageReporter();
});

// 分页得到上报人员
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
            Template.ReporterTableList('div.table', data.result.items, function (_data) {

                bindTableClickEvent();

                // 构造分页
                layui.use(['laypage'], function () {
                    var laypage = layui.laypage;
                    laypage({
                        cont: 'page-layer-reporter',
                        pages: data.result.totalPage,
                        jump: function (obj, first) {
                            $.ajax({
                                url: CONFIG.API.Reporter.GetPageReporter,
                                type: 'get',
                                dataType: 'json',
                                data: {
                                    pageIndex: obj.curr,
                                    pageSize: 6,
                                    type: 1
                                },
                                success: function (d) {
                                    Template.ReporterTableList('div.table', d.result.items, function (data) {
                                        bindTableClickEvent();
                                    });
                                }
                            });
                        }
                    });
                });
            });
        }
    });
};

// 注册表格点击事件
function bindTableClickEvent() {
    var aBtn = document.querySelectorAll('table tbody tr a');
    for (var i = 0; i < aBtn.length; i++) {
        aBtn[i].onclick = function (e) {
            e = e || window.event;
            e.stopepropagation ? e.stopepropagation() : e.cancelBubble = true;

            sessionStorage.setItem('reporterId', this.dataset.id);
        };
    }
}