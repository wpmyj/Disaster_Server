$(function() {
    $('a.login').on('click', function() {
        var usercode = document.querySelector('#usercode');
        var userpswd = document.querySelector('#userpswd');
        if(usercode.value === '' || userpswd.value === '') {
            return;
        }
        $.ajax({
            url: CONFIG.API.AccountService.AdminLogin,
            type: 'post',
            data: JSON.stringify({
                userCode: usercode.value,
                password: userpswd.value
            }),
            dataType: 'json',
            contentType: 'application/json',
            success: function(data) {
                if(data.success === true) {
                    sessionStorage.setItem('reporterId', data.result.id);
                    sessionStorage.setItem('reporterName', data.result.name);
                    sessionStorage.setItem('isLogin', 'true');
                    location.href = './index.html';
                }
            },
            error: function(error) {
                console.log(error);
            }
        });
    });
});