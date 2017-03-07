using Abp.Application.Services.Dto;
using Abp.Web.Models;
using Abp.WebApi.Controllers;
using Nito.AsyncEx.Synchronous;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace DisasterReport.Api.Controllers
{
    /// <summary>
    /// 文件上传通用接口
    /// </summary>
    public class FileUploadController: AbpApiController
    {
        /// <summary>
        /// 文件上传通用接口
        /// </summary>
        [HttpPost]
        public async Task<AjaxResponse<IListResult<FileUploadResult>>> UploadFile()
        {
            var output = new List<FileUploadResult>();

            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var streamContent = GetStreamContent();
            var streamProvider = await streamContent.ReadAsMultipartAsync();

            string root = System.Web.HttpContext.Current.Server.MapPath("~/Uploads");

            foreach (var item in streamProvider.Contents)
            {
                var fileName = item.Headers.ContentDisposition.FileName.Replace("\"", string.Empty);
                var bytes = item.ReadAsByteArrayAsync().WaitAndUnwrapException();
                var localFileName = root + "/" + Guid.NewGuid() + Path.GetExtension(fileName);


                using (FileStream fs = new FileStream(localFileName, FileMode.Create))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs))
                    {
                        bw.Write(bytes);
                    }
                }


                // With IE, filename is a fullpath, so we take just the filename
                fileName = Path.GetFileName(fileName);

                var fileUrl = "http://" + HttpContext.Current.Request.Url.Authority +
                              HttpContext.Current.Request.ApplicationPath + "/Uploads/" +
                              Path.GetFileName(localFileName);
                output.Add(new FileUploadResult() { FileName = fileName, FilePath = fileUrl });
            }

            return new AjaxResponse<IListResult<FileUploadResult>>() { Result = new ListResultDto<FileUploadResult>(output) }; //new ListResultDto<FileUploadResult>(output);
        }

        private StreamContent GetStreamContent()
        {
            Stream reqStream = Request.Content.ReadAsStreamAsync().WaitAndUnwrapException();
            MemoryStream tempStream = new MemoryStream();
            reqStream.CopyTo(tempStream);

            tempStream.Seek(0, SeekOrigin.End);
            StreamWriter writer = new StreamWriter(tempStream);
            writer.WriteLine();
            writer.Flush();
            tempStream.Position = 0;


            StreamContent streamContent = new StreamContent(tempStream);
            foreach (var header in Request.Content.Headers)
            {
                streamContent.Headers.Add(header.Key, header.Value);
            }
            return streamContent;
        }
    }
}
