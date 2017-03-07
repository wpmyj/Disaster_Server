using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisasterReport
{
    public class SignalrMessageBody
    {
        public virtual string Type { get; set; }
        public virtual object Content { get; set; }
    }
}
