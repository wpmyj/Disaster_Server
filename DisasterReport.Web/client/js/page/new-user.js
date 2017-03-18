$(function () {
    layui.use(['layer'], function () {
        var layer = layui.layer;


        getProvinceTxt();
        bindFormEvent();
    });
});

// 绑定表单注册事件
function bindFormEvent() {

    var postData = {};

    var userAddress = document.querySelector('#userAddress');
    var provinceTxt = document.querySelector('#provinceTxt');
    var cityTxt = document.querySelector('#cityTxt');
    var areaTxt = document.querySelector('#areaTxt');
    var streetTxt = document.querySelector('#streetTxt');

    provinceTxt.onchange = function(e) {
        e = e || window.event;
        e.stopepropagation ? e.stopepropagation() : e.cancelBubble = true;
        var _this = this;
        $.ajax({
            url: CONFIG.API.City.GetCityByPid,
            type: 'get',
            dataType: 'json',
            data: {
                pId: _this.value,
                pageSize: 40
            },
            success: function(data) {
                var cityTxt = document.querySelector('#cityTxt');
                var html = '';
                for(var i = 0; i < data.result.items.length; i++) {
                    html += '<option value="'+ data.result.items[i].id +'">'+ data.result.items[i].name +'</option>'
                }
                cityTxt.innerHTML = html;
                cityTxt.value = '';
                areaTxt.innerHTML = '';
                areaTxt.value = '';
                streetTxt.innerHTML = '';
                streetTxt.value = '';
                postData.areaCode = '';
            },
            error: function() {

            }
        });
    };
    cityTxt.onchange = function(e) {
        e = e || window.event;
        e.stopepropagation ? e.stopepropagation() : e.cancelBubble = true;
        var _this = this;
        $.ajax({
            url: CONFIG.API.City.GetCityByPid,
            type: 'get',
            dataType: 'json',
            data: {
                pId: _this.value,
                pageSize: 40
            },
            success: function(data) {
                var areaTxt = document.querySelector('#areaTxt');
                var html = '';
                for(var i = 0; i < data.result.items.length; i++) {
                    html += '<option value="'+ data.result.items[i].id +'">'+ data.result.items[i].name +'</option>'
                }
                areaTxt.innerHTML = html;
                areaTxt.value = '';
                streetTxt.innerHTML = '';
                streetTxt.value = '';
            },
            error: function() {

            }
        });
        postData.areaCode = _this.value;
    };
    areaTxt.onchange = function(e) {
        e = e || window.event;
        e.stopepropagation ? e.stopepropagation() : e.cancelBubble = true;
        var _this = this;
        $.ajax({
            url: CONFIG.API.City.GetCityByPid,
            type: 'get',
            dataType: 'json',
            data: {
                pId: _this.value,
                pageSize: 40
            },
            success: function(data) {
                var streetTxt = document.querySelector('#streetTxt');
                var html = '';
                for(var i = 0; i < data.result.items.length; i++) {
                    html += '<option value="'+ data.result.items[i].id +'">'+ data.result.items[i].name +'</option>'
                }
                streetTxt.innerHTML = html;
                streetTxt.value = '';
            },
            error: function() {

            }
        });
    };

    var userCode = document.querySelector('#userCode');
    var userName = document.querySelector('#userName');
    var passWord = document.querySelector('#passWord');
    var userPhone = document.querySelector('#userPhone');
    var userAge = document.querySelector('#userAge');
    var userType = document.querySelector('#userType');
    var userRemark = document.querySelector('#userRemark');

    // 街道地址
    // userAddress.onchange = function (e) {
    //     e = e || window.event;
    //     e.stopepropagation ? e.stopepropagation() : e.cancelBubble = true;
    //     var _this = this;
    //     $.ajax({
    //         url: CONFIG.API.City.GetCommunityInfoByName,
    //         type: 'get',
    //         dataType: 'json',
    //         data: {
    //             name: this.value.replace(/\s/g, "")
    //         },
    //         beforeSend: function () {
    //             postData.areaCode = "";
    //         },
    //         success: function (data) {
    //             if (data.success === true) {
    //                 _this.value = data.result.parent[0].name + data.result.name;
    //                 areaTxt.value = data.result.parent[1].name;
    //                 cityTxt.value = data.result.parent[2].name;
    //                 provinceTxt.value = data.result.parent[3].name;
    //                 postData.areaCode = data.result.id;
    //             } else {
    //                 layer.msg(data.error.message, {
    //                     icon: 2,
    //                     shift: 6
    //                 });
    //                 return false;
    //             }
    //         },
    //         error: function (e) {
    //             var responseText = JSON.parse(e.responseText);
    //             layer.msg(responseText.error.message, {
    //                 icon: 2,
    //                 shift: 6
    //             });
    //             return false;
    //         }
    //     })
    // };

    var userAddSubmit = document.querySelector('#userAddSubmit');
    // 添加按钮
    userAddSubmit.onclick = function (e) {
        e = e || window.event;
        e.stopepropagation ? e.stopepropagation() : e.cancelBubble = true;

        if (userCode.value.replace(/\s/g, '') === '') {
            layer.msg("账号不能为空", {
                icon: 2,
                shift: 6
            });
            return false;
        }

        if (passWord.value.replace(/\s/g, '') === '') {
            layer.msg("密码不能为空", {
                icon: 2,
                shift: 6
            });
            return false;
        }

        if (userName.value.replace(/\s/g, '') === '') {
            layer.msg("姓名不能为空", {
                icon: 2,
                shift: 6
            });
            return false;
        }

        if (userPhone.value.replace(/\s/g, '') === '') {
            layer.msg("手机不能为空", {
                icon: 2,
                shift: 6
            });
            return false;
        }

        if (userAge.value.replace(/\s/g, '') === '') {
            layer.msg("年龄不能为空", {
                icon: 2,
                shift: 6
            });
            return false;
        }

        if (!postData.areaCode || postData.areaCode === '' || userAddress.value === '') {
            layer.msg("请填写街道地址及选择城市", {
                icon: 2,
                shift: 6
            });
            return false;
        }

        if (userRemark.value.replace(/\s/g, '') === '') {
            layer.msg("备注不能为空", {
                icon: 2,
                shift: 6
            });
            return false;
        }

        // 查询账号有没有被注册
        $.ajax({
            url: CONFIG.API.AccountService.AddUserAccount,
            type: 'post',
            dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify({
                userCode: userCode.value.replace(/\s/g, ''),
                password: passWord.value.replace(/\s/g, '')
            }),
            success: function (data) {
                if (data.success === true) {
                    postData.userId = data.result.id;
                    postData.name = userName.value.replace(/\s/g, '');
                    postData.phone = userPhone.value.replace(/\s/g, '');
                    postData.address = userAddress.value;
                    postData.type = parseInt(userType.value);
                    postData.age = parseInt(userAge.value);
                    postData.remark = userRemark.value;

                    $.ajax({
                        url: CONFIG.API.Reporter.AddReporter,
                        type: 'post',
                        dataType: 'json',
                        contentType: 'application/json',
                        data: JSON.stringify(postData),
                        success: function (d) {
                            if(d.success === true) {
                                layer.msg('添加成功');
                                setTimeout(function() {
                                    location.href = 'user-list.html';
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
// 得到一组所有的省份
function getProvinceTxt() {
    $.ajax({
        url: CONFIG.API.City.GetPageCity,
        type: 'get',
        dataType: 'json',
        data: {
            type: 1,
            pageSize: 40
        },
        success: function(data) {
            if(data.success) {
                var provinceTxt = document.querySelector('#provinceTxt');
                var html = '';
                for(var i = 0; i < data.result.items.length; i++) {
                    html += '<option value="'+ data.result.items[i].id +'">'+ data.result.items[i].name +'</option>'
                }
                provinceTxt.innerHTML = html;
                provinceTxt.dataset.id = provinceTxt.value;
                provinceTxt.value = '';
            }
        },
        error: function(e) {

        }
    });
}