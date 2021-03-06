﻿using Abp.Domain.Repositories;
using DisasterReport.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DisasterReport.MessageNoteService.Dto;
using Abp.UI;
using DisasterReport.DtoTemplate;
using Abp.AutoMapper;

namespace DisasterReport.MessageNoteService
{
    public class MessageNoteAppService: DisasterReportAppServiceBase, IMessageNoteAppService
    {
        private readonly IRepository<MessageNoteTb, Guid> _messageNoteRepo;
        private readonly IRepository<ReporterInfoTb, Guid> _reporterInfoRepo;

        public MessageNoteAppService(
                IRepository<MessageNoteTb, Guid> messageNoteRepo,
                IRepository<ReporterInfoTb, Guid> reporterInfoRepo
            )
        {
            _messageNoteRepo = messageNoteRepo;
            _reporterInfoRepo = reporterInfoRepo;
        }

        public void DeleteReporterMessageNote(DeleteReporterMessageNoteInput input)
        {
            var existReporter = _reporterInfoRepo.FirstOrDefault(r => r.Id == input.ReporterId);
            if(existReporter == null)
            {
                throw new UserFriendlyException("没有此上报人员");
            }
            for(var i = 0; i < input.MessageNoteId.Count; i++)
            {
                var id = input.MessageNoteId[i];
                var existMessageNote = _messageNoteRepo.FirstOrDefault(m => m.Id == id);
                // 判断人员是否在此消息里

                var existIn = existMessageNote.ToReporter.Any(r => r.Id == input.ReporterId);
                if(existIn == true)
                {
                    existMessageNote.ToReporter.Remove(existReporter);
                    _messageNoteRepo.InsertOrUpdate(existMessageNote);
                }
            }
        }

        public MessageNoteOutput GetMessageNoteById(Guid id)
        {
            var existMessageNote = _messageNoteRepo.FirstOrDefault(m => m.Id == id);
            if(existMessageNote == null)
            {
                throw new UserFriendlyException("没有此消息");
            }
            return existMessageNote.MapTo<MessageNoteOutput>();
        }

        public RuimapPageResultDto<MessageNoteOutput> GetPageMessageNote(string title = "", int flag = 9, string reporterId = "", int pageIndex = 1, int pageSize = 9999)
        {
            int count = 0 ;
            List<MessageNoteTb> result = new List<MessageNoteTb>();
            if (flag == 9)
            {
                count = _messageNoteRepo.Count(m => m.Title.Contains(title) && m.ToReporter.Any(r => r.Id.ToString().Contains(reporterId)));
                result = _messageNoteRepo.GetAll().Where(m => m.Title.Contains(title) && m.ToReporter.Any(r => r.Id.ToString().Contains(reporterId))).OrderByDescending(m => m.Date).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }
            else
            {
                count = _messageNoteRepo.Count(m => m.Title.Contains(title) && m.Flag == flag && m.ToReporter.Any(r => r.Id.ToString().Contains(reporterId)));
                result = _messageNoteRepo.GetAll().Where(m => m.Title.Contains(title) && m.Flag == flag && m.ToReporter.Any(r => r.Id.ToString().Contains(reporterId))).OrderByDescending(m => m.Date).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }

            int currPage = pageIndex;
            int totalPage = (int)Math.Ceiling(count / (pageSize * 1.0));

            return new RuimapPageResultDto<MessageNoteOutput>(count, currPage, totalPage, result.MapTo<List<MessageNoteOutput>>());
        }

        public MessageNoteOutput PostMessage(AddMessageInput input)
        {
            // 先判断此人发送者是否是管理员
            var existFromReporter = _reporterInfoRepo.FirstOrDefault(r => r.Id == input.FromReporterId && r.Type == 2);
            if(existFromReporter == null)
            {
                throw new UserFriendlyException("不是管理员权限");
            }

            var newMessageNote = new MessageNoteTb();
            // 如果是群发
            if(input.Type == 1)
            {
                var allRePorterType1 = _reporterInfoRepo.GetAll().Where(r => r.Type == 1).ToList();
                newMessageNote.ToReporter = allRePorterType1;
            }

            newMessageNote.FromReporter = existFromReporter;
            newMessageNote.Date = DateTime.Now;
            newMessageNote.Flag = 1;    // 最新
            newMessageNote.Summary = input.Summary;
            newMessageNote.Text = input.Text;
            newMessageNote.Topic = input.Topic;
            newMessageNote.Type = input.Type;
            newMessageNote.Title = input.Title;

            var id = _messageNoteRepo.InsertAndGetId(newMessageNote);
            newMessageNote.Id = id;

            return newMessageNote.MapTo<MessageNoteOutput>();
        }
    }
}
