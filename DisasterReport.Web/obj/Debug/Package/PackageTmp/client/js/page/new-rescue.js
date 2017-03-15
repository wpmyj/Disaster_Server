$(function () {
    layui.use(['layer'], function () {
        var layer = layui.layer;

        // getDisasterByStatus();

        getReporterByType();

        bindFormEvent();
    });
});

// 获取对应的上报人员 type值为1的
function getReporterByType() {
    $.ajax({
        url: CONFIG.API.Reporter.GetPageReporter,
        type: 'get',
        dataType: 'json',
        data: {
            type: 1
        },
        success: function (data) {
            if (data.success === true) {
                var html = '';
                for (var i = 0; i < data.result.items.length; i++) {
                    html += '<option value="' + data.result.items[i].id + '">' + data.result.items[i].name + '</option>';
                }
                document.querySelector('#reporterId').innerHTML = html;
            }
        },
        error: function (e) {
            var responseText = JSON.parse(e.responseText);
            layer.msg(responseText.error.message, {
                icon: 2,
                shift: 6
            });
        }
    });
}

// 绑定表单注册事件
function bindFormEvent() {

    var postData = {};

    var groupSubmitBtn = document.querySelector('#groupSubmitBtn');
    var groupMark = document.querySelector('#groupMark');
    var groupType = document.querySelector('#groupType');
    var reporterId = document.querySelector('#reporterId');
    var groupName = document.querySelector('#groupName');
    // var disasterId = document.querySelector('#disasterId');

    // 添加按钮
    groupSubmitBtn.onclick = function (e) {
        e = e || window.event;
        e.stopepropagation ? e.stopepropagation() : e.cancelBubble = true;

        if (groupName.value.replace(/\s/g, '') === '') {
            layer.msg("名称不能为空", {
                icon: 2,
                shift: 6
            });
            return false;
        }

        if (reporterId.value === '') {
            layer.msg("负责人不能为空", {
                icon: 2,
                shift: 6
            });
            return false;
        }

        if (groupType.value === '') {
            layer.msg("类型不能为空", {
                icon: 2,
                shift: 6
            });
            return false;
        }

        // if (disasterId.value === '') {
        //     layer.msg("灾情不能为空", {
        //         icon: 2,
        //         shift: 6
        //     });
        //     return false;
        // }

        if (groupMark.value === '') {
            layer.msg("备注不能为空", {
                icon: 2,
                shift: 6
            });
            return false;
        }

        postData.groupName = groupName.value.replace(/\s/g, '');
        postData.remark = groupMark.value;
        postData.type = parseInt(groupType.value);
        postData.reporterId = reporterId.value;
        // postData.disasterId = disasterId.value;

        // 注册团队
        $.ajax({
            url: CONFIG.API.MessageGroup.AddMessageGroup,
            type: 'post',
            dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify(postData),
            success: function (d) {
                if (d.success === true) {
                    layer.msg('添加成功');
                    setTimeout(function () {
                        location.href = 'rescue-list.html';
                    }, 400);
                }
            },
            error: function (e) {
                var responseText = JSON.parse(e.responseText);
                layer.msg(responseText.error.message, {
                    icon: 2,
                    shift: 6
                });
            }
        });
    };
};

// 获取没有进行处理的灾情 status 0
// function getDisasterByStatus() {
//     $.ajax({
//         url: CONFIG.API.Disaster.GetPageDisaster,
//         type: 'get',
//         dataType: 'json',
//         data: {
//             status: 0
//         },
//         success: function (data) {
//             if (data.success === true) {
//                 var html = '';
//                 for (var i = 0; i < data.result.items.length; i++) {
//                     html += '<option value="' + data.result.items[i].id + '">' + data.result.items[i].disasterCode + '</option>';
//                 }
//                 document.querySelector('#disasterId').innerHTML = html;
//             }
//         },
//         error: function (e) {
//             var responseText = JSON.parse(e.responseText);
//             layer.msg(responseText.error.message, {
//                 icon: 2,
//                 shift: 6
//             });
//         }
//     });
// }