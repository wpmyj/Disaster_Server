using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Castle.Core.Logging;
using DisasterReport.DisasterService.Dto;
using DisasterReport.DomainEntities;
using DisasterReport.Web.SignalR.HubData;
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
        private readonly IRepository<MessageNoteTb, Guid> _messageNoteRepo;

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
                IRepository<MessageGroupTb, Guid> messageGroupRepo,
                IRepository<MessageNoteTb, Guid> messageNoteRepo
            )
        {
            _disasterInfoRepo = disasterInfoRepo;
            _uploadsFileRepo = uploadsFileRepo;
            _reporterRepo = reporterRepo;
            _disasterKindRepo = disasterKindRspo;
            _messageGroupRepo = messageGroupRepo;
            _messageNoteRepo = messageNoteRepo;

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
                ReporterId = input.ReporterId,
                Type = input.Type
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
                ReporterId = input.ReporterId,
                Type = input.Type
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
                    Remark = existDisaster.Remark,
                    Type = "号召响应"
                });
            } catch (Exception e)
            {

            }
        }

        /// <summary>
        /// 消息通知给APP端
        /// </summary>
        /// <param name="messageNoteId"></param>
        public void postReporter(Guid messageNoteId)
        {
            var existMessageNote = _messageNoteRepo.FirstOrDefault(m => m.Id == messageNoteId);
            try
            {
                this.SendToAppMessage(new PostReporterMessageHub()
                {
                    Summary = existMessageNote.Summary,
                    Date = existMessageNote.Date,
                    Id = existMessageNote.Id,
                    Flag = existMessageNote.Flag,
                    Title = existMessageNote.Title,
                    Topic = existMessageNote.Topic,
                    Type = "消息通知"
                });
            }
            catch (Exception e)
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
        /// <param name="msg"></param>
        private void SendToWebMessage(Object msg)
        {
            List<string> connectIds = new List<string>();
            foreach(var con in _WebConnections)
            {
                connectIds.Add(con.Value.HubId);
            }

            Clients.Clients(connectIds).sendToWebMessage(msg);
        }

        /// <summary>
        /// 发送消息给App端用户
        /// </summary>
        /// <param name="msg"></param>
        private void SendToAppMessage(Object msg)
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