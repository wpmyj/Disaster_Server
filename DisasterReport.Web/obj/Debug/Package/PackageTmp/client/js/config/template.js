(function (root, factory) {
    if (typeof exports === "object") {
        module.exports = factory();
    } else {
        root.Template = factory();
    }
}(this, function () {
    if (typeof Template === 'undefined') {
        var Template = {};
    } else if (typeof Template === 'object') {
        Template = Template;
    }

    /**
     * 灾情列表Item
     * @param {any} selector
     * @param {any} data
     * @param {any} callback
     */
    Template.DisasterListItem = function (selector, data, callback) {
        var html = '';
        for (var i = 0; i < data.length; i++) {
            var imgUrl = '';
            if (data[i].disasterKindCode === 'swzhfbc') {
                imgUrl = 'img/fengbaochao.png';
            } else if (data[i].disasterKindCode === 'swzhhx') {
                imgUrl = 'img/haixiao.png'
            }
            html += '<div data-index="'+ i +'" data-id="'+ data[i].id +'" class="info_item">'
                +  '<div> '
                +  '    <img src="' + imgUrl + '" alt=""> ' 
                +  '</div> '
                +  '<p class="text-ellipsis"> '
                +  '    <strong>上报时间：' + data[i].reportDate.replace('T', ' ') + '</strong> '
                +  '</p> '
                +  '<p class="text-ellipsis"><a href="javascript:;">详细地址：' + data[i].disasterAddress + '</a></p> '
                +'</div>';
        }
        document.querySelector(selector).innerHTML = html;
        callback && callback instanceof Function && callback(data, document.querySelector(selector).children);
    };

    /**
     * 灾情人员跟踪列表Item
     * @param {any} selector
     * @param {any} data
     * @param {any} callback
     */
    Template.ReporterListItem = function (selector, data, callback) {
        var html = '';
        for (var i = 0; i < data.length; i++) {
            var imgUrl = 'img/default-person.png';
            html += '<div data-index="'+ i +'" data-id="'+ data[i].id +'" class="info_item">'
                +  '<div> '
                +  '    <img src="' + imgUrl + '" alt=""> ' 
                +  '</div> '
                +  '<p class="text-ellipsis"> '
                +  '    <strong>姓名：' + data[i].name + '</strong> '
                +  '</p> '
                +  '<p class="text-ellipsis"><a href="javascript:;">最后定位地址：' + data[i].lastAddress + '</a></p> '
                +'</div>';
        }
        document.querySelector(selector).innerHTML = html;
        callback && callback instanceof Function && callback(data, document.querySelector(selector).children);
    };

    /**
     * @name 灾情详情模板
     * @param {any} data
     */
    Template.DisasterDetailInfo = function(data) {

        var html = '<form class="form-horizontal center-block" style="width: 94%; margin: 0 auto;"> <input id="disasterDetailId" type="text" readonly style="display:none;" value="'+ data.id +'">'
            +           '<div class="form-group">'
            +               '<div id="layer-photos-dangerInfo" class="col-sm-12" style="height: 260px;padding: 5px;text-align: center;border: 1px solid #3892D3;margin-top: 5px; overflow: hidden;">';

            for(var i = 0; i < data.file.length; i++) {
                        html += '<img style="height: 100%; width: 33%;" layer-src="'+ data.file[i].path +'" src="'+ data.file[i].path +'" alt="现场图片">'
            }
                html +=    '</div>'
                     +  '</div>'
                     +  '<div class="row-fluid show-grid">'
                     +  '   <div class="span2 _lable">上报人：</div>'
                     +  '   <div class="span4">'
                     +  '       <input type="text" readonly value="'+ data.reporter.name +'">'
                     +  '   </div>'
                     +  '   <div class="span2 _lable">上报时间：</div>'
                     +  '   <div class="span4">'
                     +  '       <input type="text" value="'+ data.reportDate.replace('T', ' ') +'" readonly>'
                     +  '   </div>'
                     +  '</div>'
                     +  '<div class="row-fluid show-grid">'
                     +  '   <div class="span2 _lable">灾情类型：</div>'
                     +  '   <div class="span4">'
                     +  '       <input type="text" readonly value="'+ data.disasterKind.name +'">'
                     +  '   </div>'
                     +  '   <div class="span2 _lable">灾情状态：</div>'
                     +  '   <div class="span4">'
                     +  '       <input type="text" readonly value="'+ (data.status === 0 ? '暂未处理' : data.status === 1 ? '正在处理' : '销结') +'">'
                     +  '   </div>'
                     +  '</div>'
                     +  '<div class="row-fluid show-grid">'
                     +  '   <div class="span2 _lable">灾情地址：</div>'
                     +  '   <div class="span8">'
                     +  '       <input type="text" readonly style="width: 100%" value="'+ data.disasterAddress +'">'
                     +  '   </div>'
                     +  '</div>'
                     +  '<div class="row-fluid show-grid">'
                     +  '    <div class="span2 _lable">灾情描述：</div>'
                     +  '    <div class="span8">'
                     +  '        <textarea style="resize:none;width: 100%" readonly rows="4">'+ data.remark +'</textarea>'
                     +  '    </div>'
                     +  '</div>'
                     +  '</form>';
        return html;
    };

    /**
     * @name 灾情详情的popup模板
     * @param {any} data
     */
    Template.DisasterDetailPopup = function(data) {
        var html = '<table class="table table-hover">'
                +  '    <thead>'
                +  '        <tr>'
                +  '            <th>灾情类型</th>'
                +  '            <th>'+ data.disasterKind.name +'</th>'
                +  '        </tr>'
                +  '    </head>' 
                +  '    <tbody>'
                +  '        <tr class="first">'
                +  '            <td>灾情上报时间</td>'
                +  '            <td>'+ data.reportDate.replace('T', ' ') +'</td>'
                +  '        </tr>'
                +  '        <tr>'
                +  '            <td>灾情发生地点</td>'
                +  '            <td>'+ data.disasterAddress +'</td>'
                +  '        </tr>'
                +  '        <tr>'
                +  '            <td>灾情描述</td>'
                +  '            <td>'+ data.remark +'</td>'
                +  '        </tr>'
                +  '    </tbody>'
                +  '</table>';
        return html;
    };

    /**
     * @name 人员详情的popup模板
     * @param {any} data
     */
    Template.ReporterDetailPopup = function(data) {
        var html = '<table class="table table-hover">'
                +  '    <thead>'
                +  '        <tr>'
                +  '            <th>人员姓名</th>'
                +  '            <th>'+ data.name +'</th>'
                +  '        </tr>'
                +  '    </head>' 
                +  '    <tbody>'
                +  '        <tr class="first">'
                +  '            <td>手机号码</td>'
                +  '            <td>'+ data.phone +'</td>'
                +  '        </tr>'
                +  '        <tr>'
                +  '            <td>住址</td>'
                +  '            <td>'+ data.address +'</td>'
                +  '        </tr>'
                +  '        <tr>'
                +  '            <td>备注</td>'
                +  '            <td>'+ data.remark +'</td>'
                +  '        </tr>'
                +  '    </tbody>'
                +  '</table>';
        return html;
    };

    /**
     * @name 统计各类灾情上报个数
     * 
     * @param {css selector} selector
     * @param { [{}] } data
     * @param {Function} callback
     */
    Template.DisasterKindCount = function(selector, data, callback) {
        var html = '<h4>灾情统计</h4>'
                +  '<div class="knob-wrapper">'
                +  '    <input type="text" value="'+ data[0].count +'" class="knob" data-readOnly="true" data-thickness=".3" data-inputcolor="#333" data-fgcolor="#30a1ec" data-bgcolor="#d4ecfd" data-width="150" />'
                +  '    <div class="info">'
                +  '        <div class="param">'
                +  '            <span class="line blue"></span>'
                +  '            '+ data[0].type + ''
                +  '        </div>'
                +  '    </div>'
                +  '</div>'
                +  '<div class="knob-wrapper">'
                +  '    <input type="text" value="'+ data[1].count +'" class="knob second" data-readOnly="true" data-thickness=".3" data-inputcolor="#333" data-fgcolor="#3d88ba" data-bgcolor="#d4ecfd" data-width="150" />'
                +  '    <div class="info">'
                +  '        <div class="param">'
                +  '            <span class="line blue"></span>'
                +  '            '+ data[1].type + ''
                +  '        </div>'
                +  '    </div>'
                +  '</div>';
        var selectorContainer = document.querySelector(selector);
        selectorContainer.innerHTML = html;

        callback && callback instanceof Function && callback(data);
        return true;
    };

    /**
     * @name 统计灾情总数、已销结数、未解决数、今日上报数
     * 
     * @param {css selector} selector
     * @param { [{}] } data
     * @param {Function} callback
     */
    Template.DisasterResultSum = function(selector, data, callback) {
        var html = '<div class="row-fluid stats-row">'
                +  '    <div class="span3 stat">'
                +  '        <div class="data">'
                +  '            <span class="number">'+ data.disasterCount +'</span>'
                +  '            灾情总数'
                +  '        </div>'
                +  '    </div>'
                +  '    <div class="span3 stat">'
                +  '        <div class="data">'
                +  '            <span class="number">'+ data.finishCount +'</span>'
                +  '            已解决数'
                +  '        </div>'
                +  '    </div>'
                +  '    <div class="span3 stat">'
                +  '        <div class="data">'
                +  '            <span class="number">'+ data.remainderCount +'</span>'
                +  '            未解决'
                +  '        </div>'
                +  '    </div>'
                +  '    <div class="span3 stat last">'
                +  '        <div class="data">'
                +  '            <span class="number">'+ data.todayCount +'</span>'
                +  '            今日上报'
                +  '        </div>'
                +  '    </div>'
                +  '</div>';
        var selectorContainer = document.querySelector(selector);
        selectorContainer.innerHTML = html;

        callback && callback instanceof Function && callback(data);
        return true;
    };

    /**
     * @name 人员列表表格模板
     * 
     * @param {any} selector
     * @param {any} data
     * @param {any} callback
     */
    Template.ReporterTableList = function(selector, data, callback) {
        var html = '<table class="table table-hover">'
                +  '    <thead>'
                +  '        <tr>'
                +  '            <th class="span4 sortable">'
                +  '                姓名'
                +  '            </th>'
                +  '            <th class="span3 sortable">'
                +  '                <span class="line"></span>手机'
                +  '            </th>'
                +  '            <th class="span2 sortable">'
                +  '                <span class="line"></span>地址'
                +  '            </th>'
                +  '            <th class="span3 sortable">'
                +  '                <span class="line"></span>备注'
                +  '            </th>'
                +  '            <th class="span3 sortable align-right">'
                +  '                <span class="line"></span>操作'
                +  '            </th>'
                +  '        </tr>'
                +  '    </thead>'
                +  '    <tbody>';
        for(var i = 0; i < data.length; i++) {
            html += '       <tr data-id="'+ data[i].id +'" '+ (i === 0 ? 'class="first"' : '')  +'>'
                +  '            <td>'
                +  '                <img src="'+ (data[i].photo ? data[i].photo : 'img/default-person.png') +'" class="img-circle avatar hidden-phone" />'
                +  '                <a data-id="'+ data[i].id +'" href="user-profile.html" class="name">'+ data[i].name +'</a>'
                +  '                <span class="subtext">'+ data[i].age +'岁</span>'
                +  '            </td>'
                +  '            <td>'
                +  '                '+ data[i].phone
                +  '            </td>'
                +  '            <td>'
                +  '                '+ data[i].address
                +  '            </td>'
                +  '            <td>'
                +  '                '+ data[i].remark
                +  '            </td>'
                +  '            <td class="align-right">'
                +  '                <a data-id="'+ data[i].id +'" href="user-profile.html">最近状态</a> | '
                +  '                <a data-id="'+ data[i].id +'" href="personal-info.html">修改</a> | '
                +  '                <a data-id="'+ data[i].id +'" href="javascript:;">删除</a>'
                +  '            </td>'
                +  '        </tr>';
        }
                            
        html +=    '    </tbody>'
                +  '</table>';
        var selectorContainer = document.querySelector(selector);
        selectorContainer.innerHTML = html;

        callback && callback instanceof Function && callback(data);
        return true;
    };

    /**
     * @name 团队列表表格模板
     * 
     * @param {any} selector
     * @param {any} data
     * @param {any} callback
     */
    Template.RescueTableList = function(selector, data, callback) {
        var html = '<table class="table table-hover">'
                +  '    <thead>'
                +  '        <tr>'
                +  '            <th class="span4 sortable">'
                +  '                救援团队名'
                +  '            </th>'
                +  '            <th class="span3 sortable">'
                +  '                <span class="line"></span>负责人'
                +  '            </th>'
                +  '            <th class="span2 sortable">'
                +  '                <span class="line"></span>备注'
                +  '            </th>'
                +  '            <th class="span3 sortable">'
                +  '                <span class="line"></span>团队人数'
                +  '            </th>'
                +  '            <th class="span3 sortable align-right">'
                +  '                <span class="line"></span>操作'
                +  '            </th>'
                +  '        </tr>'
                +  '    </thead>'
                +  '    <tbody>';
        for(var i = 0; i < data.length; i++) {
            html += '       <tr data-id="'+ data[i].id +'" '+ (i === 0 ? 'class="first"' : '')  +'>'
                +  '            <td>'
                +  '                <img src="'+ (data[i].photo ? data[i].photo : 'img/hanzai.png') +'" class="img-circle avatar hidden-phone" />'
                +  '                <a data-id="'+ data[i].id +'" href="rescue-profile.html" class="name">'+ data[i].groupName +'</a>'
                +  '                <span class="subtext">'+ data[i].createTime.slice(0, 18).replace('T', '') +'</span>'
                +  '            </td>'
                +  '            <td>'
                +  '                '+ (function(item){
                    for(var index = 0; index < item.member.length; index++) {
                        if(item.member[index].type === 1) {
                            return item.member[index].reporter.name;
                        }
                    }
                }(data[i])) 
                +  '            </td>'
                +  '            <td>'
                +  '                '+ data[i].remark
                +  '            </td>'
                +  '            <td>'
                +  '                '+ data[i].groupTotalNum
                +  '            </td>'
                +  '            <td class="align-right">'
                +  '                <a data-id="'+ data[i].id +'" href="rescue-profile.html">查看详情</a> | '
                +  '                <a data-id="'+ data[i].id +'" href="javascript:;">删除</a>'
                +  '            </td>'
                +  '        </tr>';
        }
                            
        html +=    '    </tbody>'
                +  '</table>';
        var selectorContainer = document.querySelector(selector);
        selectorContainer.innerHTML = html;

        callback && callback instanceof Function && callback(data);
        return true;
    };

    /**
     * @name 上报人员的资料
     * 
     * @param {any} selector
     * @param {any} data
     * @param {any} callback
     */
    Template.ReporterProfile = function(selector, data, callback) {
        var html = '<div class="row-fluid header">'
                +  '    <div class="span8">'
                +  '        <img src="'+ (data.reporter.photo ? data.reporter.photo : 'img/default-person.png') +'" style="width: 90px;" class="avatar img-circle" />'
                +  '        <h3 class="name">'+ data.reporter.name +'</h3>'
                +  '        <span class="area">'+ data.reporter.age +'岁</span>'
                +  '    </div>'
                +  '</div>'
                +  '<div class="row-fluid profile">'
                +  '    <div class="span9 bio">'
                +  '        <div class="profile-box">'
                +  '            <div class="span12 section">'
                +  '                <h6>个人信息</h6>'
                +  '                <p>'+ data.reporter.remark +'</p>'
                +  '            </div>'
                +  '            <h6>最近上报</h6>'
                +  '            <br />'
                +  '            <table id="disasterTable" class="table table-hover">'
                +  '                <thead>'
                +  '                    <tr>'
                +  '                        <th class="span2">'
                +  '                            灾情种类'
                +  '                        </th>'
                +  '                        <th class="span3">'
                +  '                            <span class="line"></span>'
                +  '                            时间'
                +  '                        </th>'
                +  '                        <th class="span3">'
                +  '                            <span class="line"></span>'
                +  '                            地点'
                +  '                        </th>'
                +  '                        <th class="span3">'
                +  '                            <span class="line"></span>'
                +  '                            状态'
                +  '                        </th>'
                +  '                    </tr>'
                +  '                </thead>'
                +  '                <tbody>';
        if(data.disaster.length > 0) {
            for(var i = 0; i < data.disaster.length; i++) {
                html += '               <tr '+ ( i===0 ? 'class="first"' : '') +'>'
                +  '                        <td>'
                +  '                            <a href="#">'+ data.disaster[i].disasterKind.name +'</a>'
                +  '                        </td>'
                +  '                        <td>'
                +  '                            '+ data.disaster[i].reportDate.replace('T', ' ')
                +  '                        </td>'
                +  '                        <td>'
                +  '                            '+ data.disaster[i].disasterAddress
                +  '                        </td>'
                +  '                        <td>'
                +  '                            '+ (data.disaster[i].status === 0 ? '没有处理' : (data.disaster[i].status === 1 ? '正在处理' : '已处理'))
                +  '                        </td>'
                +  '                    </tr>';
            }
        }
        
        html += '                 </tbody>'
                +  '            </table>'
                +  '            <div id="page-layer-profile" class="pagination pull-right">'
                +  '            </div>'
                +  '        </div>'
                +  '    </div>'
                +  '    <div class="span3 address pull-right">'
                +  '        <h6>联系方式</h6>'
                +  '        <div style="width: 300px; height: 130px; background: #eee;" id="xyacmap"></div>'
                +  '        <ul>'
                +  '            <li id="lastAddress"></li>'
                +  '            <li>最后定位地址：</li>'
                +  '            <li class="ico-li">'
                +  '                <i class="ico-phone"></i>'
                +  '                <span style="position: relative; top: 4px;">'+ data.reporter.phone +'</span>'
                +  '            </li>'
                +  '        </ul>'
                +  '    </div>'
                +  '</div>'
        var selectorContainer = document.querySelector(selector);
        selectorContainer.innerHTML = html;

        callback && callback instanceof Function && callback(data);
        return true;
    };

    /**
     * @name 团队详细资料模板
     * 
     * @param {any} selector
     * @param {any} data
     * @param {any} callback
     */
    Template.MessageGroupProfile = function(selector, data, callback) {
        var html = '<div class="row-fluid header">'
                +  '    <div class="span8">'
                +  '        <img src="'+ (data.photo ? data.photo : 'img/hanzai.png') +'" style="width: 90px;" class="avatar img-circle" />'
                +  '        <h3 class="name">'+ data.groupName +'</h3>'
                +  '        <span class="area">创建时间：'+ data.createTime.slice(0, 19).replace('T', ' ') +'</span>'
                +  '    </div>'
                +  '</div>'
                +  '<div class="row-fluid profile">'
                +  '    <div class="span9 bio">'
                +  '        <div class="profile-box">'
                +  '            <div class="span12 section">'
                +  '                <h6>团队介绍</h6>'
                +  '                <p>'+ data.remark +'</p>'
                +  '            </div>'
                +  '            <div class="span12 section">'
                +  '                <h6>抢救灾情</h6>';
                for(var i = 0; i < data.disaster.length; i++) {
                    html += '<p>'+ data.disaster.disasterCode + '——【'+ data.disaster.disasterKind.name +'】' +'</p>';
                }
                                
        html +=    '            </div>'
                +  '            <h6>团队成员</h6>'
                +  '            <br />'
                +  '            <table id="left-exist-reporter" class="table table-hover">'
                +  '                <thead>'
                +  '                    <tr>'
                +  '                        <th class="span2">'
                +  '                            姓名'
                +  '                        </th>'
                +  '                        <th class="span3">'
                +  '                            <span class="line"></span>'
                +  '                            类型'
                +  '                        </th>'
                +  '                        <th class="span3">'
                +  '                            <span class="line"></span>'
                +  '                            地址'
                +  '                        </th>'
                +  '                        <th class="span3">'
                +  '                            <span class="line"></span>'
                +  '                            操作'
                +  '                        </th>'
                +  '                    </tr>'
                +  '                </thead>'
                +  '                <tbody>'
                +  '                </tbody>'
                +  '            </table>'
                +  '            <div id="page-layer-profile1" class="pagination pull-right">'
                +  '            </div>'
                +  '        </div>'
                +  '    </div>'
                +  '    <div class="span3 address pull-right">'
                +  '        <h6>团队建设</h6>'
                +  '        <table id="right-nexist-reporter" class="table table-hover">'
                +  '            <thead>'
                +  '                <tr>'
                +  '                    <th class="span2">'
                +  '                        选择'
                +  '                    </th>'
                +  '                    <th class="span4">'
                +  '                        姓名'
                +  '                    </th>'
                +  '                    <th class="span5">'
                +  '                        地址'
                +  '                    </th>'
                +  '                </tr>'
                +  '            </thead>'
                +  '            <tbody>'
                +  '            </tbody>'
                +  '        </table>'
                +  '        <div id="page-layer-profile2" class="pagination pull-right"></div>'
                +  '        <div class="span11 field-box actions">'
                +  '            <input type="button" id="addMemberBnt" class="btn-glow primary" value="加入" />'
                +  '        </div>'
                +  '    </div>'
                +  '</div>';

        var selectorContainer = document.querySelector(selector);
        selectorContainer.innerHTML = html;

        callback && callback instanceof Function && callback(data);
        return true;
    };

    Template.DeviceTableList = function(selector, data, callback) {
        var html = '<table class="table table-hover">'
                +  '    <thead>'
                +  '        <tr>'
                +  '            <th class="span4 sortable">'
                +  '                编号'
                +  '            </th>'
                +  '            <th class="span3 sortable">'
                +  '                <span class="line"></span>持有者'
                +  '            </th>'
                +  '            <th class="span2 sortable">'
                +  '                <span class="line"></span>所在区'
                +  '            </th>'
                +  '            <th class="span3 sortable align-right">'
                +  '                <span class="line"></span>操作'
                +  '            </th>'
                +  '        </tr>'
                +  '    </thead>'
                +  '    <tbody>'
                +  '    </tbody>'
                +  '</table>';
                
        var selectorContainer = document.querySelector(selector);
        selectorContainer.innerHTML = html;

        callback && callback instanceof Function && callback(data);
        return true;
    }

    return Template;
}));