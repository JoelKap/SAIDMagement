using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SAIDManangementSystem.BusinessLayerLogic;
using SAIDManangementSystem.Models;

namespace SAIDManangementSystem.Controllers.Api
{
    public class HomeController : ApiController
    {
        // GET: api/Home
        public HttpResponseMessage Get()
        {
            try
            {
                var bll = new Idbll();
                var id = bll.GetValidGeneratedId();
                return Request.CreateResponse(HttpStatusCode.OK, id);
            }
            catch (Exception exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, exception.Message);
            }
        }

        // GET: api/Home/5 
        public HttpResponseMessage GetUserDetails(int id)
        {
            try
            {
                var bll = new Idbll();
                var userDetail = bll.GetIdUserDetailByIdNumber(id);
                return Request.CreateResponse(HttpStatusCode.OK, userDetail);
            }
            catch (Exception exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, exception.Message);
            }
        }

        // POST: api/Home
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Home/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Home/5
        public void Delete(int id)
        {
        }
    }
}
