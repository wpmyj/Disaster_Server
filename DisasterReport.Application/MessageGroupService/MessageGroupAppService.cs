using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DisasterReport.MessageGroupService.Dto;
using Abp.Domain.Repositories;
using DisasterReport.DomainEntities;
using Abp.UI;
using Abp.AutoMapper;
using DisasterReport.DtoTemplate;

namespace DisasterReport.MessageGroupService
{
    public class MessageGroupAppService : DisasterReportAppServiceBase, IMessageGroupAppService
    {
        private readonly IRepository<MessageGroupTb, Guid> _messageGroupRepo;
        private readonly IRepository<ReporterInfoTb, Guid> _reporterInfoRepo;
        private readonly IRepository<GroupMemberTb, Guid> _groupMemberRepo;
        private readonly IRepository<DisasterInfoTb, Guid> _disasterInfoRepo;

        public MessageGroupAppService(
                IRepository<MessageGroupTb, Guid> messageGroupRepo,
                IRepository<ReporterInfoTb, Guid> reporterInfoRepo,
                IRepository<GroupMemberTb, Guid> groupMemberRepo,
                IRepository<DisasterInfoTb, Guid> disasterInfoRepo
            )
        {
            _messageGroupRepo = messageGroupRepo;
            _reporterInfoRepo = reporterInfoRepo;
            _groupMemberRepo = groupMemberRepo;
            _disasterInfoRepo = disasterInfoRepo;
        }

        public MessageGroupOutput AddGroupMember(GroupMemberAddInput input)
        {
            // 先得到消息组
            var existMessageGroup = _messageGroupRepo.FirstOrDefault(m => m.Id == input.MessageGroupId);
            if(existMessageGroup == null)
            {
                throw new UserFriendlyException("没有对应的消息组");
            }
            // 之前的总人数
            var beforeCount = existMessageGroup.Reporter.Count;

            List<ReporterInfoTb> reporterList = new List<ReporterInfoTb>();

            // 得到对应的上报人员
            for(var i = 0; i < input.ReporterId.Count; i++)
            {
                // 判断此人是否在群组里
                var flag = false;

                foreach(var r in existMessageGroup.Reporter)
                {
                    if(r.Id == input.ReporterId[i])
                    {
                        flag = true;
                        break;
                    }
                }

                if(flag == true)
                {
                    continue;
                }

                try {
                    var tempId = input.ReporterId[i];
                    var tempReporter = _reporterInfoRepo.FirstOrDefault(r => r.Id == tempId);
                    reporterList.Add(tempReporter);

                    existMessageGroup.Reporter.Add(tempReporter);
                } catch (Exception e)
                {
                    throw new UserFriendlyException(e.ToString());
                }
            }

            // 如果没有增加任何人
            if(beforeCount == existMessageGroup.Reporter.Count)
            {
                throw new UserFriendlyException("没有增加任何队员");
            }

            // 得到组员总数
            existMessageGroup.GroupTotalNum = existMessageGroup.Reporter.Count;

            // 更新组
            _messageGroupRepo.InsertOrUpdate(existMessageGroup);

            var newMembers = new List<GroupMemberTb>();
            foreach(var r in reporterList)
            {
                var groupMember = new GroupMemberTb()
                {
                    MessageGroup = existMessageGroup,
                    Reporter = r,
                    Type = 3
                };

                var id = _groupMemberRepo.InsertAndGetId(groupMember);
                groupMember.Id = id;

                newMembers.Add(groupMember);
            }

            var beforeMembers = _groupMemberRepo.GetAll().Where(g => g.MessageGroup.Id == existMessageGroup.Id).ToList();
            beforeMembers.AddRange(newMembers);

            var outResult = new MessageGroupOutput()
            {
                CreateTime = existMessageGroup.CreateTime,
                GroupName = existMessageGroup.GroupName,
                GroupTotalNum = existMessageGroup.GroupTotalNum,
                Id = existMessageGroup.Id,
                Member = beforeMembers.MapTo<List<ReporterMemberOutput>>(),
                Photo = existMessageGroup.Photo,
                Remark = existMessageGroup.Remark,
                Type = existMessageGroup.type,
                Disaster = existMessageGroup.Disaster.MapTo<List<MessageDisasterOutput>>()
            };

            return outResult;
        }

        public MessageGroupOutput AddMessageGroup(MessageGroupAddInput input)
        {
            // 需要指定一个队长
            var existReporter = _reporterInfoRepo.FirstOrDefault(r => r.Id == input.ReporterId);

            if(existReporter == null)
            {
                throw new UserFriendlyException("没有对应的上报人员");
            }
            
            var newMessageGroup = new MessageGroupTb()
            {
                CreateTime = DateTime.Now,
                GroupName = input.GroupName,
                GroupTotalNum = 1,
                Remark = input.Remark,
                type = input.Type
            };
            
            try
            {
                List<ReporterInfoTb> _reporter = new List<ReporterInfoTb>();
                _reporter.Add(existReporter);

                newMessageGroup.Reporter = _reporter;
                
                var id = _messageGroupRepo.InsertOrUpdateAndGetId(newMessageGroup);
                newMessageGroup.Id = id;
                
                var newGroupMember = new GroupMemberTb()
                {
                    MessageGroup = newMessageGroup,
                    Reporter = existReporter,
                    Type = 1
                };
                
                id = _groupMemberRepo.InsertAndGetId(newGroupMember);
                newGroupMember.Id = id;

                var Members = new List<GroupMemberTb>();

                // 把刚刚增加的成员添加进去
                Members.Add(newGroupMember);

                var outResult = new MessageGroupOutput()
                {
                    CreateTime = newMessageGroup.CreateTime,
                    GroupName = newMessageGroup.GroupName,
                    GroupTotalNum = newMessageGroup.GroupTotalNum,
                    Id = newMessageGroup.Id,
                    Member = Members.MapTo<List<ReporterMemberOutput>>(),
                    Photo = newMessageGroup.Photo,
                    Remark = newMessageGroup.Remark,
                    Type = newMessageGroup.type,
                    Disaster = newMessageGroup.Disaster.MapTo<List<MessageDisasterOutput>>()
                };

                return outResult;
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.ToString());
            }
        }

        public MessageGroupOutput GetMessageGroupById(Guid id, int pageIndex = 1, int pageSize = 9999)
        {
            var existMessageGroup = _messageGroupRepo.FirstOrDefault(m => m.Id == id);
            if(existMessageGroup == null)
            {
                throw new UserFriendlyException("没有此消息团队组");
            }

            var Members = _groupMemberRepo.GetAll().Where(g => g.MessageGroup.Id == existMessageGroup.Id).OrderBy(g => g.Type).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            var outResult = new MessageGroupOutput()
            {
                CreateTime = existMessageGroup.CreateTime,
                GroupName = existMessageGroup.GroupName,
                GroupTotalNum = existMessageGroup.GroupTotalNum,
                Id = existMessageGroup.Id,
                Member = Members.MapTo<List<ReporterMemberOutput>>(),
                Photo = existMessageGroup.Photo,
                Remark = existMessageGroup.Remark,
                Type = existMessageGroup.type,
                Disaster = existMessageGroup.Disaster.MapTo<List<MessageDisasterOutput>>()
            };

            return outResult;
        }

        public RuimapPageResultDto<ReporterGroupOutput> GetOtherNoGroupMember(Guid messageGroupId, int pageIndex = 1, int pageSize = 9999)
        {
            var existMessageGroup = _messageGroupRepo.FirstOrDefault(m => m.Id == messageGroupId);
            if (existMessageGroup == null)
            {
                throw new UserFriendlyException("没有对应的消息组团队");
            }

            //// 获取所有上报人员 type 为1
            //var reporterNotInThisGroup = _reporterInfoRepo.GetAll().Where(r => !r.MessageGroup.Any(m => m.Id == messageGroupId)).OrderBy(r => r.Name).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            //var count = _reporterInfoRepo.Count(r => r.MessageGroup.All(m => m.Id != messageGroupId));
            //int currPage = pageIndex;
            //int totalPage = (int)Math.Ceiling(count / (pageSize * 1.0));

            var count = _reporterInfoRepo.Count(r => r.MessageGroup == null && r.Type == 1);
            var reporterNotHasGroup = _reporterInfoRepo.GetAll().Where(r => r.MessageGroup == null && r.Type == 1).OrderBy(r => r.Name).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            int currPage = pageIndex;
            int totalPage = (int)Math.Ceiling(count / (pageSize * 1.0));

            return new RuimapPageResultDto<ReporterGroupOutput>(count, currPage, totalPage, reporterNotHasGroup.MapTo<List<ReporterGroupOutput>>());
        }

        public RuimapPageResultDto<ReporterMemberOutput> GetPageGroupMember(Guid messageGroupId, int pageIndex = 1, int pageSize = 9999)
        {
            var existMessageGroup = _messageGroupRepo.FirstOrDefault(m => m.Id == messageGroupId);

            if(existMessageGroup == null)
            {
                throw new UserFriendlyException("没有此消息组团队");
            }

            var count = existMessageGroup.GroupTotalNum;

            var Members = _groupMemberRepo.GetAll().Where(g => g.MessageGroup.Id == messageGroupId).OrderBy(g => g.Type).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            List<ReporterMemberOutput> OutList = new List<ReporterMemberOutput>();

            foreach(var memeber in Members)
            {
                OutList.Add(new ReporterMemberOutput()
                {
                    Type = memeber.Type,
                    Reporter = memeber.Reporter.MapTo<ReporterGroupOutput>()
                });
            }

            int currPage = pageIndex;
            int totalPage = (int)Math.Ceiling(count / (pageSize * 1.0));

            return new RuimapPageResultDto<ReporterMemberOutput>(count, currPage, totalPage, OutList);
        }

        public RuimapPageResultDto<MessageGroupOutput> GetPageMessageGroup(int pageIndex = 1, int pageSize = 9999)
        {
            var count = _messageGroupRepo.Count();
            try
            {
                var messageGroupResult = _messageGroupRepo.GetAll().OrderByDescending(m => m.CreateTime).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                List<MessageGroupOutput> outResult = new List<MessageGroupOutput>(); 
                foreach (var messageGroup in messageGroupResult)
                {
                    var Members = _groupMemberRepo.GetAll().Where(g => g.MessageGroup.Id == messageGroup.Id).ToList();

                    outResult.Add(new MessageGroupOutput()
                    {
                        CreateTime = messageGroup.CreateTime,
                        GroupName = messageGroup.GroupName,
                        GroupTotalNum = messageGroup.GroupTotalNum,
                        Id = messageGroup.Id,
                        Member = Members.MapTo<List<ReporterMemberOutput>>(),
                        Photo = messageGroup.Photo,
                        Remark = messageGroup.Remark,
                        Type = messageGroup.type,
                        Disaster = messageGroup.Disaster.MapTo<List<MessageDisasterOutput>>()
                    });
                }

                int currPage = pageIndex;
                int totalPage = (int)Math.Ceiling(count / (pageSize * 1.0));

                return new RuimapPageResultDto<MessageGroupOutput>(count, currPage, totalPage, outResult);
            }
            catch (Exception e)
            {
                throw new Exception("错误:", e);
            }
        }
    }
}
