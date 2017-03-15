$(function () {
    getPageMessageGroup();
});

// 分页得到团队list
function getPageMessageGroup() {
    $.ajax({
        //Template.RescueTableList
        url: CONFIG.API.MessageGroup.GetPageMessageGroup,
        type: 'get',
        dataType: 'json',
        data: {
            pageSize: 6
        },
        success: function(data) {
            Template.RescueTableList('div.table', data.result.items, function (_data) {
                // 构造分页
                bindTableClickEvent();
                
                layui.use(['laypage'], function () {
                    var laypage = layui.laypage;
                    laypage({
                        cont: 'page-layer-rescue',
                        pages: data.result.totalPage,
                        jump: function (obj, first) {
                            $.ajax({
                                url: CONFIG.API.Reporter.GetPageMessageGroup,
                                type: 'get',
                                dataType: 'json',
                                data: {
                                    pageIndex: obj.curr,
                                    pageSize: 6
                                },
                                success: function (d) {
                                    Template.RescueTableList('div.table', d.result.items, function(){
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
}

// 注册表格点击事件
function bindTableClickEvent() {
    var aBtn = document.querySelectorAll('table tbody tr a');
    for (var i = 0; i < aBtn.length; i++) {
        aBtn[i].onclick = function (e) {
            e = e || window.event;
            e.stopepropagation ? e.stopepropagation() : e.cancelBubble = true;

            sessionStorage.setItem('messageGroupId', this.dataset.id);
        };
    }
}