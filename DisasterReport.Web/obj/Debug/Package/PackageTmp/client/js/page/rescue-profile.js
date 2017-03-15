$(function () {
    // 权限验证？
    layui.use(['layer'], function () {
        var layer = layui.layer;
        var messageGroupId = sessionStorage.getItem('messageGroupId');
        $.ajax({
            url: CONFIG.API.MessageGroup.GetMessageGroupById,
            type: 'get',
            dataType: 'json',
            data: {
                id: messageGroupId,
                pageSize: 4
            },
            success: function (data) {
                if (data.success === true) {
                    Template.MessageGroupProfile('#pad-wrapper', data.result, function (_data) {
                        getOtherNoGroupMember(messageGroupId);
                        bindAddBtnEvent();
                        // 注册左侧分页
                        layui.use(['laypage'], function () {
                            var laypage = layui.laypage;
                            laypage({
                                cont: 'page-layer-profile1',
                                pages: Math.ceil(_data.groupTotalNum / 4),
                                jump: function (obj, first) {
                                    $.ajax({
                                        url: CONFIG.API.MessageGroup.GetPageGroupMember,
                                        type: 'get',
                                        dataType: 'json',
                                        data: {
                                            pageIndex: obj.curr,
                                            pageSize: 4,
                                            messageGroupId: messageGroupId
                                        },
                                        success: function (d) {
                                            if (d.success === true) {
                                                var leftTb = document.querySelector('#left-exist-reporter');
                                                var html = '';
                                                for (var i = 0; i < d.result.items.length; i++) {
                                                    html += ' <tr ' + (i === 0 ? 'class="first"' : '') + '>'
                                                        + '      <td>'
                                                        + '          ' + d.result.items[i].reporter.name
                                                        + '      </td>'
                                                        + '      <td>'
                                                        + '          ' + (d.result.items[i].type === 1 ? '负责人' : (d.result.items[i].type === 2 ? '临时队长' : '队员'))
                                                        + '      </td>'
                                                        + '      <td>'
                                                        + '          ' + d.result.items[i].reporter.address
                                                        + '      </td>'
                                                        + '      <td>'
                                                        + '          <a data-id="' + d.result.items[i].reporter.id + '" href="#">详细资料</a> |'
                                                        + '          <a data-id="' + d.result.items[i].reporter.id + '" href="#">移除团队</a>'
                                                        + '      </td>'
                                                        + '  </tr>';
                                                }
                                                leftTb.querySelector('tbody').innerHTML = html;
                                            }
                                        }
                                    });
                                }
                            });
                        });

                    });
                }
            },
            error: function (e) {
                console.log(e);
            }
        });
    });
});

function getOtherNoGroupMember(id) {
    $.ajax({
        url: CONFIG.API.MessageGroup.GetOtherNoGroupMember,
        dataType: 'json',
        type: 'get',
        data: {
            messageGroupId: id,
            pageSize: 5
        },
        success: function (data) {
            if (data.success === true) {
                var rightTb = document.querySelector('#right-nexist-reporter');
                // 注册分页
                layui.use(['laypage'], function () {
                    var laypage = layui.laypage;
                    laypage({
                        cont: 'page-layer-profile2',
                        pages: data.result.totalPage,
                        jump: function (obj, first) {
                            $.ajax({
                                url: CONFIG.API.MessageGroup.GetOtherNoGroupMember,
                                type: 'get',
                                dataType: 'json',
                                data: {
                                    pageIndex: obj.curr,
                                    messageGroupId: id,
                                    pageSize: 5
                                },
                                success: function (d) {
                                    if (d.success === true) {
                                        var html = '';
                                        for (var i = 0; i < d.result.items.length; i++) {
                                            html += '  <tr ' + (i === 0 ? 'class="first"' : '') + '>'
                                                + '       <td class="span2">'
                                                + '           <input data-id="' + d.result.items[i].id + '" type="checkbox">'
                                                + '       </td>'
                                                + '       <td class="span4">'
                                                + '           ' + d.result.items[i].name
                                                + '       </td>'
                                                + '       <td class="5">'
                                                + '           ' + d.result.items[i].address
                                                + '       </td>'
                                                + '   </tr>';
                                        }
                                        rightTb.querySelector('tbody').innerHTML = html;
                                    }
                                }
                            });
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

// 绑定加入按钮事件
function bindAddBtnEvent() {
    var addMemberBnt = document.querySelector('#addMemberBnt');

    addMemberBnt.onclick = function (e) {
        e = e || window.event;
        e.stopepropagation ? e.stopepropagation() : e.cancelBubble = true;

        // 获取所有选中的input
        var inputChk = document.querySelectorAll('#right-nexist-reporter tbody tr input[type=checkbox]:checked');
        if (!inputChk || inputChk.length <= 0) {
            return false;
        }

        var postData = {};
        postData.reporterId = [];
        postData.messageGroupId = sessionStorage.getItem('messageGroupId');
        for (var i = 0; i < inputChk.length; i++) {
            postData.reporterId.push(inputChk[i].dataset.id);
        }

        $.ajax({
            url: CONFIG.API.MessageGroup.AddGroupMember,
            type: 'post',
            dataType: 'json',
            data: JSON.stringify(postData),
            contentType: 'application/json',
            success: function (data) {
                if (data.success === true) {
                    layer.msg("操作成功");
                    setTimeout(function() {
                        location.reload();
                    }, 300);
                }
            },
            error: function (e) {
                console.log(e);
            }
        });
    };
}
