(function (root, factory) {
    if (typeof exports === "object") {
        module.exports = factory();
    } else {
        root.CONFIG = factory();
    }
}(this, function () {
    if (typeof CONFIG === 'undefined') {
        var CONFIG = {};
    } else if (typeof CONFIG === 'object') {
        CONFIG = CONFIG;
    }

    // 是否调试模式
    CONFIG.ISDEBUG = true;

    // 项目名称
    CONFIG.PROJNAME = 'disaster';

    // 用户登录后保存的信息
    CONFIG.LOGININFO = {};

    // 接口API地址配置
    CONFIG.API = {};
    //31 160
    CONFIG.API.AppApiPrefix = CONFIG.ISDEBUG ? ('http://192.168.31.160/' + CONFIG.PROJNAME) : ('http://118.178.19.15/' + CONFIG.PROJNAME);            //接口API前缀

    // 上传通用接口
    CONFIG.API.Upload = {};
    CONFIG.API.Upload.UploadPic = CONFIG.API.AppApiPrefix + '/api/FileUpload/UploadFile';

    // 账户管理接口
    CONFIG.API.AccountService = {};
    CONFIG.API.AccountService.Login = CONFIG.API.AppApiPrefix + '/api/services/app/userAccount/Login';
    CONFIG.API.AccountService.AdminLogin = CONFIG.API.AppApiPrefix + '/api/services/app/userAccount/AdminLogin';
    CONFIG.API.AccountService.AddUserAccount = CONFIG.API.AppApiPrefix + '/api/services/app/userAccount/AddUserAccount';
    CONFIG.API.AccountService.GetUserAccountById = CONFIG.API.AppApiPrefix + '/api/services/app/userAccount/GetUserAccountById';

    // 设备管理接口
    CONFIG.API.Device = {};
    CONFIG.API.Device.GetDeviceByReporterId = CONFIG.API.AppApiPrefix + '/api/services/app/device/GetDeviceByReporterId';
    CONFIG.API.Device.GetPageDevice = CONFIG.API.AppApiPrefix + '/api/services/app/device/GetPageDevice';
    CONFIG.API.Device.AddDevice = CONFIG.API.AppApiPrefix + '/api/services/app/device/AddDevice';

    // 上报人员接口
    CONFIG.API.Reporter = {};
    CONFIG.API.Reporter.GetReporterById = CONFIG.API.AppApiPrefix + '/api/services/app/reporter/GetReporterById';
    CONFIG.API.Reporter.GetPageReporter = CONFIG.API.AppApiPrefix + '/api/services/app/reporter/GetPageReporter';
    CONFIG.API.Reporter.GetReporterByNameOrPhone = CONFIG.API.AppApiPrefix + '/api/services/app/reporter/GetReporterByNameOrPhone';
    CONFIG.API.Reporter.UpdateReporter = CONFIG.API.AppApiPrefix + '/api/services/app/reporter/UpdateReporter';
    CONFIG.API.Reporter.AddReporter = CONFIG.API.AppApiPrefix + '/api/services/app/reporter/AddReporter';

    // 灾情接口
    CONFIG.API.Disaster = {};
    CONFIG.API.Disaster.GetPageDisaster = CONFIG.API.AppApiPrefix + '/api/services/app/disaster/GetPageDisaster';
    CONFIG.API.Disaster.GetDisasterById = CONFIG.API.AppApiPrefix + '/api/services/app/disaster/GetDisasterById';
    CONFIG.API.Disaster.ReportDisaster = CONFIG.API.AppApiPrefix + '/api/services/app/disaster/ReportDisaster';
    CONFIG.API.Disaster.GetDisasterFilePicById = CONFIG.API.AppApiPrefix + '/api/services/app/disaster/GetDisasterFilePicById';
    CONFIG.API.Disaster.GetDisasterKindCount = CONFIG.API.AppApiPrefix + '/api/services/app/disaster/GetDisasterKindCount';
    CONFIG.API.Disaster.GetDisasterNetworkCount = CONFIG.API.AppApiPrefix + '/api/services/app/disaster/GetDisasterNetworkCount';
    CONFIG.API.Disaster.GetDisasterTrend = CONFIG.API.AppApiPrefix + '/api/services/app/disaster/GetDisasterTrend';
    CONFIG.API.Disaster.GetDisasterResultSum = CONFIG.API.AppApiPrefix + '/api/services/app/disaster/GetDisasterResultSum';
    CONFIG.API.Disaster.SetDisasterStatus = CONFIG.API.AppApiPrefix + '/api/services/app/disaster/SetDisasterStatus';
    CONFIG.API.Disaster.GetPageDisasterByReporterId = CONFIG.API.AppApiPrefix + '/api/services/app/disaster/GetPageDisasterByReporterId';

    // 消息组接口
    CONFIG.API.MessageGroup = {};
    CONFIG.API.MessageGroup.AddMessageGroup = CONFIG.API.AppApiPrefix + '/api/services/app/messageGroup/AddMessageGroup';
    CONFIG.API.MessageGroup.AddGroupMember = CONFIG.API.AppApiPrefix + '/api/services/app/messageGroup/AddGroupMember';
    CONFIG.API.MessageGroup.GetPageMessageGroup = CONFIG.API.AppApiPrefix + '/api/services/app/messageGroup/GetPageMessageGroup';
    CONFIG.API.MessageGroup.GetMessageGroupById = CONFIG.API.AppApiPrefix + '/api/services/app/messageGroup/GetMessageGroupById';
    CONFIG.API.MessageGroup.GetPageGroupMember = CONFIG.API.AppApiPrefix + '/api/services/app/messageGroup/GetPageGroupMember';
    CONFIG.API.MessageGroup.GetOtherNoGroupMember = CONFIG.API.AppApiPrefix + '/api/services/app/messageGroup/GetOtherNoGroupMember';

    // 城市接口
    CONFIG.API.City = {};
    CONFIG.API.City.GetCommunityInfoByName = CONFIG.API.AppApiPrefix + '/api/services/app/city/GetCommunityInfoByName';
    CONFIG.API.City.GetCityInfoByLngLat = 'http://api.map.baidu.com/geocoder/v2/';
    // queryData = {
    //     ak: 'C93b5178d7a8ebdb830b9b557abce78b',
    //     location: '38.005,103.254',
    //     output: 'json'
    // }
    CONFIG.API.City.GetPageCity = CONFIG.API.AppApiPrefix + '/api/services/app/city/GetPageCity';
    CONFIG.API.City.GetCityByPid = CONFIG.API.AppApiPrefix + '/api/services/app/city/GetCityByPid';

    return CONFIG;
}));