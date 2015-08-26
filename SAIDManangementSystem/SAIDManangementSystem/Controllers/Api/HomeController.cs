using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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
                return Request.CreateResponse(HttpStatusCode.OK);
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
                throw new Exception("NOT LOADING");
                //MOCKED DATA
                //todo will be fixed when am done with client side
                var userDumiData = new IdModel()
                {
                    Birthdate = new DateTime(1999,01,25).Date,
                    Gender = "Male",
                    IdStatus = "Single",
                    IsCitizen = true,
                    SAId = 12345678965412 
                };
                 var dt = userDumiData.Birthdate.ToShortDateString();
                userDumiData.DateConverter = dt;
               
               return Request.CreateResponse(HttpStatusCode.OK,userDumiData);
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
