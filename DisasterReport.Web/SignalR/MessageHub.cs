using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Castle.Core.Logging;
using DisasterReport.DisasterService.Dto;
using DisasterReport.DomainEntities;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DisasterReport.Web.SignalR
{
    [HubName("messageHub")]
    public class MessageHub : Hub, ITransientDependency
    {
        /// <summary>
        /// 上报用户的connectionID与用户名对照表
        /// </summary>
        private readonly static Dictionary<Guid, ReporterLoginHub> _ReporterConnections = new Dictionary<Guid, ReporterLoginHub>();

        private readonly IRepository<DisasterInfoTb, Guid> _disasterInfoRepo;
        private readonly IRepository<UploadsFileTb, Guid> _uploadsFileRepo;
        private readonly IRepository<ReporterInfoTb, Guid> _reporterRepo;
        private readonly IRepository<DisasterKindTb, Guid> _disasterKindRepo;
        private readonly IRepository<MessageGroupTb, Guid> _messageGroupRepo;

        /// <summary>
        /// 后端用户的connectionID与用户名对照表
        /// </summary>
        private readonly static Dictionary<Guid, ReporterLoginHub> _WebConnections = new Dictionary<Guid, ReporterLoginHub>();

        public ILogger Logger { get; set; }

        public MessageHub(
                IRepository<DisasterInfoTb, Guid> disasterInfoRepo,
                IRepository<UploadsFileTb, Guid> uploadsFileRepo,
                IRepository<ReporterInfoTb, Guid> reporterRepo,
                IRepository<DisasterKindTb, Guid> disasterKindRspo,
                IRepository<MessageGroupTb, Guid> messageGroupRepo
            )
        {
            _disasterInfoRepo = disasterInfoRepo;
            _uploadsFileRepo = uploadsFileRepo;
            _reporterRepo = reporterRepo;
            _disasterKindRepo = disasterKindRspo;
            _messageGroupRepo = messageGroupRepo;

            Logger = NullLogger.Instance;
        }

        public override async Task OnConnected()
        {
            await base.OnConnected();
        }

        // APP用户上线
        public void AppLogin(ReporterLoginInputHub input)
        {
            // 每次登陆id会发生变化
            // 保存该用户信息
            Logger.Debug("Server: " + Context.ConnectionId);
            Logger.Debug("Client: " + Context.ConnectionId);
            _ReporterConnections[input.ReporterId] = new ReporterLoginHub()
            {
               
                HubId = input.HubId,
                Name = input.Name,
                ReporterId = input.ReporterId
            };
            this.SendToWebMessage(input.Name + "上线了");
        }

        // Web用户上线
        public void WebLogin (ReporterLoginInputHub input)
        {
            
            _WebConnections[input.ReporterId] = new ReporterLoginHub()
            {
                HubId = input.HubId,
                Name = input.Name,
                ReporterId = input.ReporterId
            };
        }

        // 号召上报人员处理此灾情
        public void CallReporter(Guid disasterId)
        {
            var existDisaster = _disasterInfoRepo.FirstOrDefault(d => d.Id == disasterId);
            try
            {
                this.SendToAppMessage(new CallReporterDealDisasterHub() {
                    Address = existDisaster.DisasterAddress,
                    Id = existDisaster.Id,
                    Remark = existDisaster.Remark
                });
            } catch (Exception e)
            {

            }
        }

        public override async Task OnDisconnected(bool stopCalled)
        {
            await base.OnDisconnected(stopCalled);
        }

        /// <summary>
        /// 发送消息给Web端
        /// </summary>
        private void SendToWebMessage(string msg)
        {
            List<string> connectIds = new List<string>();
            foreach(var con in _WebConnections)
            {
                connectIds.Add(con.Value.HubId);
            }

            Clients.Clients(connectIds).sendToWebMessage(msg);
        }

        private void SendToAppMessage(CallReporterDealDisasterHub msg)
        {
            List<string> connectIds = new List<string>();
            foreach (var con in _ReporterConnections)
            {
                connectIds.Add(con.Value.HubId);
            }

            Logger.Debug("最终: " + connectIds[0]);
            Clients.Clients(connectIds).sendToAppMessage(msg);
        }
    }
}