using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            using (var client = new HttpClient())
            {
                using (var content = new MultipartFormDataContent())
                {
                    MemoryStream target = new MemoryStream();
                    file.InputStream.CopyTo(target);
                    byte[] Bytes = target.ToArray();


                    file.InputStream.Read(Bytes, 0, Bytes.Length);
                    var fileContent = new ByteArrayContent(Bytes);
                    fileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment") { FileName = file.FileName };


                    content.Add(fileContent);

                    content.Add(new StringContent("123"), "FileId");
                    //content.Headers.Add("Key", "abc23sdflsdf");
                    var requestUri = "http://localhost:65440/api/Values/";
                    var result = client.PostAsync(requestUri, content).Result;
                    if (result.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        ViewBag.Success = result.ReasonPhrase;

                    }
                    else
                    {
                        ViewBag.Failed = "Failed !" + result.Content.ToString();
                    }
                }
            }


            return View();
        }
    }
}