using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisasterReport.MessageNoteService.Dto
{
    public class DeleteReporterMessageNoteInput
    {
        public virtual Guid ReporterId { get; set; }
        public virtual List<Guid> MessageNoteId { get; set; }
    }
}
