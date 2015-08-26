using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAIDManangementSystem.Models;

namespace SAIDManangementSystem.BusinessLayerLogic
{
    public class Idbll
    {
        public long GetValidGeneratedId()
        {
            //MOCKED DATA
            //todo will be fixed when am done with client side
            var rnd = new Random();
            var id = rnd.Next(1, 15);
            return id;
        }
          
        public IdModel GetIdUserDetailByIdNumber(long idNumber)
        {
           
            var userDumiData = new IdModel()
            {
                Birthdate = new DateTime(1999, 01, 25).Date,
                Gender = "Male",
                IdStatus = "Single",
                IsCitizen = true,
                SAId = 12345678965412
            };
            var dt = userDumiData.Birthdate.ToShortDateString();
            userDumiData.DateConverter = dt;
            return userDumiData;
        }
    }
}