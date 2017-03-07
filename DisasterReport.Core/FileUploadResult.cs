using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisasterReport
{
    /// <summary>
    /// 文件上传结果
    /// </summary>
    public class FileUploadResult
    {
        public virtual string FileName { get; set; }
        public virtual string FilePath { get; set; }
    }
}
