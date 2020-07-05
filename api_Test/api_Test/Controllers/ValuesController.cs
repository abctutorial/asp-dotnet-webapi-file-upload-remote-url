using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace api_Test.Controllers
{
    public class ValuesController : ApiController
    {
        public HttpResponseMessage Post()
        {
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count < 1)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            var name = httpRequest.Params["FileId"];
            foreach (string file in httpRequest.Files)
            {
                var postedFile = httpRequest.Files[file];
                var filePath = HttpContext.Current.Server.MapPath("~/" + postedFile.FileName);
                postedFile.SaveAs(filePath);
            }

            return Request.CreateResponse(HttpStatusCode.Created);
        }
    }
}
