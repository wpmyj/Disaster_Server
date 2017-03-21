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
using DisasterReport.DisasterService.Dto;

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
            // 要减去有messagegroupId 但是 hasgroup为false的总数
            beforeCount = beforeCount - _reporterInfoRepo.Count(r => r.MessageGroup.Id == input.MessageGroupId && r.HasGroup == false);

            // 新进的组员
            List<ReporterInfoTb> reporterList = new List<ReporterInfoTb>();

            // 新的成员
            var newMembers = new List<GroupMemberTb>();

            var beforeMemberCount = 0;

            // 得到对应的上报人员
            for (var i = 0; i < input.ReporterId.Count; i++)
            {
                // 判断此人是否在群组里
                var flag = false;
                // 是否是之前组里的人员
                var isBefore = false;

                foreach(var r in existMessageGroup.Reporter)
                {
                    if(r.Id == input.ReporterId[i])
                    {
                        // 如果在组里 并且有组hasGroup不为false
                        if(r.HasGroup == true)
                        {
                            flag = true;
                            break;
                        }
                        else
                        {
                            // 需要重新添加进组
                            flag = false;
                            isBefore = true;
                            break;
                        }
                    }
                }

                if(flag == true)
                {
                    continue;
                }

                try {

                    // 如果是之前组里有过的人员
                    if(isBefore == true)
                    {
                        var tempId = input.ReporterId[i];
                        var beforeReporter = _reporterInfoRepo.FirstOrDefault(r => r.Id == tempId);
                        beforeReporter.HasGroup = true;
                        _reporterInfoRepo.InsertOrUpdate(beforeReporter);

                        // 添加到组员表中
                        var beforeMember = new GroupMemberTb()
                        {
                            MessageGroup = existMessageGroup,
                            Reporter = beforeReporter,
                            Type = 3
                        };

                        var id = _groupMemberRepo.InsertAndGetId(beforeMember);
                        beforeMember.Id = id;
                        newMembers.Add(beforeMember);
                        beforeMemberCount++;
                    }
                    else // 如果在上一次的变更队伍后没加入过此队伍
                    {
                        var tempId = input.ReporterId[i];
                        var tempReporter = _reporterInfoRepo.FirstOrDefault(r => r.Id == tempId);

                        // 添加进组
                        reporterList.Add(tempReporter);
                        tempReporter.HasGroup = true;
                        _reporterInfoRepo.InsertOrUpdate(tempReporter);

                        existMessageGroup.Reporter.Add(tempReporter);
                    }
                } catch (Exception e)
                {
                    throw new UserFriendlyException(e.ToString());
                }
            }

            // 如果没有增加任何人
            if(0 == (reporterList.Count + beforeMemberCount))
            {
                throw new UserFriendlyException("没有增加任何队员");
            }

            // 得到组员总数
            existMessageGroup.GroupTotalNum = beforeCount + (reporterList.Count + beforeMemberCount);

            // 更新组
            _messageGroupRepo.InsertOrUpdate(existMessageGroup);

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
                Type = existMessageGroup.Type,
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
                Type = input.Type
            };
            
            try
            {
                List<ReporterInfoTb> _reporter = new List<ReporterInfoTb>();
                _reporter.Add(existReporter);

                // 设置为有组了
                existReporter.HasGroup = true;
                _reporterInfoRepo.InsertOrUpdate(existReporter);

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
                    Type = newMessageGroup.Type,
                    Disaster = newMessageGroup.Disaster.MapTo<List<MessageDisasterOutput>>()
                };

                return outResult;
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.ToString());
            }
        }

        public MessageGroupOutput DeleteGroupMember(GroupMemeberDeleteInput input)
        {
            // 先得到消息组
            var existMessageGroup = _messageGroupRepo.FirstOrDefault(m => m.Id == input.MessageGroupId);
            if (existMessageGroup == null)
            {
                throw new UserFriendlyException("没有对应的消息组");
            }
            // 之前的总人数
            var beforeCount = existMessageGroup.Reporter.Count;
            // 要减去有messagegroupId 但是 hasgroup为false的总数
            beforeCount = beforeCount - _reporterInfoRepo.Count(r => r.MessageGroup.Id == input.MessageGroupId && r.HasGroup == false);

            // 用来保存删除队员后 剩下的队员
            List<ReporterInfoTb> reporterList = new List<ReporterInfoTb>();

            // 得到删除后的对应的上报人员
            foreach (var r in existMessageGroup.Reporter)
            {
                // 判断此队员是否没有组
                if(r.HasGroup == false)
                {
                    continue;
                }
                // 判断此人是否在删除队列里
                var flag = false;

                for(var i = 0; i < input.ReporterId.Count; i++)
                {
                    if(input.ReporterId[i] == r.Id)
                    {
                        // 判断是否是负责人
                        var tempId = input.ReporterId[i];
                        var member_ = _groupMemberRepo.FirstOrDefault(g => g.Reporter.Id == tempId);
                        if(member_.Type == 1)   // 负责人
                        {
                            throw new UserFriendlyException("负责人不能被移除团队");
                        }
                        flag = true;
                        break;
                    }
                }
                // 如果此人是要被删除的
                if (flag == true)
                {
                    // 移除此人关联队伍关系
                    r.HasGroup = false;
                    _reporterInfoRepo.Update(r);
                    continue;
                }
                else
                {
                    try
                    {
                        reporterList.Add(r);
                    }
                    catch (Exception e)
                    {
                        throw new UserFriendlyException(e.ToString());
                    }
                }
            }

            // 如果没有删除任何人
            if (beforeCount == reporterList.Count)
            {
                throw new UserFriendlyException("没有删除任何队员");
            }

            // 得到组员总数
            existMessageGroup.GroupTotalNum = reporterList.Count;

            // 更新组
            _messageGroupRepo.InsertOrUpdate(existMessageGroup);

            // 再删除对应表上的人员
            var afterMembers = _groupMemberRepo.GetAll().Where(g => g.MessageGroup.Id == input.MessageGroupId).ToList();
            for (var i = 0; i < input.ReporterId.Count; i++)
            {
                var tempId = input.ReporterId[i];
                var deleteEntity = _groupMemberRepo.FirstOrDefault(g => g.Reporter.Id == tempId);
                afterMembers.Remove(deleteEntity);
                _groupMemberRepo.Delete(deleteEntity);
            }
            
            var outResult = new MessageGroupOutput()
            {
                CreateTime = existMessageGroup.CreateTime,
                GroupName = existMessageGroup.GroupName,
                GroupTotalNum = existMessageGroup.GroupTotalNum,
                Id = existMessageGroup.Id,
                Member = afterMembers.MapTo<List<ReporterMemberOutput>>(),
                Photo = existMessageGroup.Photo,
                Remark = existMessageGroup.Remark,
                Type = existMessageGroup.Type,
                Disaster = existMessageGroup.Disaster.MapTo<List<MessageDisasterOutput>>()
            };

            return outResult;
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
                Type = existMessageGroup.Type,
                Disaster = existMessageGroup.Disaster.MapTo<List<MessageDisasterOutput>>()
            };

            return outResult;
        }

        public MessageGroupOutput GetMessageGroupByReporterId(Guid id)
        {
            var existReporter = _reporterInfoRepo.FirstOrDefault(r => r.Id == id);
            if(existReporter == null)
            {
                throw new UserFriendlyException("没有此人员");
            }
            var messageGroupId = existReporter.MessageGroup.Id;
            var members = _groupMemberRepo.GetAll().Where(g => g.MessageGroup.Id == messageGroupId).ToList();
            if (members == null)
            {
                throw new UserFriendlyException("此人员没有队伍");
            }
            var existMessageGroup = _messageGroupRepo.FirstOrDefault(m => m.Id == messageGroupId);

            var outResult = new MessageGroupOutput()
            {
                CreateTime = existMessageGroup.CreateTime,
                Disaster = existMessageGroup.Disaster.MapTo<List<MessageDisasterOutput>>(),
                GroupName = existMessageGroup.GroupName,
                GroupTotalNum = existMessageGroup.GroupTotalNum,
                Id = existMessageGroup.Id,
                Member = members.MapTo<List<ReporterMemberOutput>>(),
                Photo = existMessageGroup.Photo,
                Remark = existMessageGroup.Remark,
                Type = existMessageGroup.Type,
            };

            return outResult;
        }

        public List<MessageDisasterOutput> GetMessageGroupDisaster(Guid id)
        {
            var existMessage = _messageGroupRepo.FirstOrDefault(m => m.Id == id);
            List<MessageDisasterOutput> outResult = new List<MessageDisasterOutput>();
            if (existMessage == null)
            {
                throw new UserFriendlyException("没有此消息组");
            }
            var disaster = existMessage.Disaster.ToList();
            for (var i = 0; i < disaster.Count; i++)
            {
                outResult.Add(new MessageDisasterOutput()
                {
                    DisasterAddress = disaster[i].DisasterAddress,
                    Status = disaster[i].Status,
                    DisasterCode = disaster[i].DisasterCode,
                    DisasterKind = disaster[i].DisasterKind.MapTo<DisasterKindOutput>(),
                    Id = disaster[i].Id,
                    Lat = disaster[i].Lat,
                    Lng = disaster[i].Lng,
                    Remark = disaster[i].Remark,
                    ReportDate = disaster[i].ReportDate
                });
            }
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

            var count = _reporterInfoRepo.Count(r => (r.MessageGroup == null || r.HasGroup != true) && r.Type == 1);
            var reporterNotHasGroup = _reporterInfoRepo.GetAll().Where(r => (r.MessageGroup == null || r.HasGroup != true) && r.Type == 1).OrderBy(r => r.Name).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
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
                        Type = messageGroup.Type,
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
