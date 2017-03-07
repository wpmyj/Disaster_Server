using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisasterReport.DtoTemplate
{
    public class RuimapPageResultDto<T> : PagedResultDto<T>
    {
        public int TotalCount { get; set; }
        public int CurrPage { get; set; }
        public int TotalPage { get; set; }
        public RuimapPageResultDto(int totalCount, int currPage, int totalPage, IReadOnlyList<T> items) : base(totalCount, items)
        {
            TotalCount = totalCount;
            CurrPage = currPage;
            TotalPage = totalPage;
        }
    }
}
