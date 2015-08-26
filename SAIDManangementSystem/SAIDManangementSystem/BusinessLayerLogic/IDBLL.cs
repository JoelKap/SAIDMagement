using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using SAIDManangementSystem.Models;

namespace SAIDManangementSystem.BusinessLayerLogic
{
    public class Idbll
    {
        public string GetValidGeneratedId()
        {

            var random = new Random();
           
            var randomeDate = GetRandomDate(DateTime.Now.AddDays(-1), new DateTime(1900,12,31)).ToString("yyy-MM-dd");

            //Less than 5 equals female, more than 5 equals male
            int gender = random.Next(0, 10);


            DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
            dtfi.ShortDatePattern = "yyy-MM-dd";
            DateTime dtObjc = Convert.ToDateTime(randomeDate, dtfi);

            
           //Get Year, Month and Day and concatinate them
            var year = dtObjc.Year.ToString().Substring(2);
            var month = dtObjc.Month < 10 ? "0" + dtObjc.Month.ToString() : dtObjc.Month.ToString();
            var day = dtObjc.Day < 10 ? "0" + dtObjc.Day.ToString() : dtObjc.Day.ToString();
            var dob = year + month + day;

            //Putting birthdate, Gender, citizenship and random number that use to mean "Race" and now it means nothing
            var nin = "" + dob + gender + random.Next(10) + random.Next(10) + random.Next(10) + random.Next(2) + random.Next(10);

            //Getting the last number
            var sumEven = 0;
            var sumOdd = 0;
            var even = false;
            for (var i = 0; i < 12; i++)
            {
                if (!even)
                    sumOdd += int.Parse(nin[i].ToString());
                else
                {
                    var doubleEven = 2 * int.Parse(nin[i].ToString());
                    sumEven += (doubleEven / 10) + (doubleEven % 10);
                }
                even = !even;
            }
            var check = (10 - ((sumOdd + sumEven) % 10)) % 10; 
            var rowId = nin + check;

            //Formating the ID
            string id = rowId.Substring(0, 6) + " " + rowId.Substring(6, 4) + " " + rowId.Substring(4, 3);

            return id;


        }
          
        private DateTime GetRandomDate(DateTime from, DateTime to)
        {
            Random rnd = new Random();
            var range = to - from;
            var randTimeSpan = new TimeSpan((long)(rnd.NextDouble() * range.Ticks));
            return from + randTimeSpan;
        }


        public IdModel GetIdUserDetailByIdNumber(long idNumber)
        {

            //Fake data
            //todo : will fix as i go on
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