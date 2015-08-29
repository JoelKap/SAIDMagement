using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

            var randomeDate = GetRandomDate(DateTime.Now.AddDays(-1), new DateTime(1900, 12, 31)).ToString("yyy-MM-dd");

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
        
        public IdModel GetIdUserDetailByIdNumber(string idNumber)
        {
            if (!idNumber.All(c=> Char.IsNumber(c)))
                throw new Exception("Only numeric values are allowed");

            var iDModel = new IdModel();
            var regex = "(?<Year>[0-9][0-9])(?<Month>([0][1-9])|([1][0-2]))(?<Day>([0-2][0-9])|([3][0-1]))(?<Gender>[0-9])(?<Series>[0-9]{3})(?<Citizenship>[0-9])(?<Uniform>[0-9])(?<Control>[0-9])";
            
            var date = ValidateAndReturnDateOfBirth(idNumber);
            var age = ReturnAgeIfValid(idNumber);
            var citizen = CheckIfCitizen(idNumber);
            var gender = ReturnCorrectGender(idNumber);

            bool isValidLength = false;
            bool isValidPattern = false;
            bool isValidControlDigit = false;

            const int validLength = 13;
            const int controlDigitCheckValue = 10;
            const int controlDigitLocation = 12;
            const int controlDigitCheckExceptionValue = 9;

            if (idNumber.Length == 13)
            {
                isValidLength = true;
            }

            if (isValidLength)
            {
                Regex idPattern = new Regex(regex);

                if (idPattern.IsMatch(idNumber))
                {
                    if (idNumber.Substring(2, 2) != "00" && idNumber.Substring(4, 2) != "00")
                    {
                        isValidPattern = true;
                    }
                }
            }

            // check control digit, only if previous validations passed
            if (isValidLength && isValidPattern)
            {
                int a = 0;
                int b = 0;
                int c = 0;
                int cDigit = -1;
                int tmp = 0;
                StringBuilder even = new StringBuilder();
                string evenResult = null;

                // sum odd digits
                for (int i = 0; i < validLength - 1; i = i + 2)
                {
                    a = a + int.Parse(idNumber[i].ToString());
                }

                // build a string containing even digits
                for (int i = 1; i < validLength - 1; i = i + 2)
                {
                    even.Append(idNumber[i]);
                }

                // multipy by 2
                tmp = int.Parse(even.ToString()) * 2;

                // convert to string again
                evenResult = tmp.ToString();

                // sum the digits in evenResult
                for (int i = 0; i < evenResult.Length; i++)
                {
                    b = b + int.Parse(evenResult[i].ToString());
                }

                c = a + b;

                cDigit = controlDigitCheckValue - int.Parse(c.ToString()[1].ToString());
                if (cDigit == int.Parse(idNumber[controlDigitLocation].ToString()))
                {
                    isValidControlDigit = true;
                }
                else
                {
                    if (cDigit > controlDigitCheckExceptionValue)
                    {
                        if (0 == int.Parse(idNumber[controlDigitLocation].ToString()))
                        {
                            isValidControlDigit = true;
                        }
                    }
                }

            }

            // final check
            if (isValidLength && isValidPattern && isValidControlDigit)
            {
                iDModel.SAId = idNumber;
                iDModel.DateConverter = date;
                iDModel.Gender = gender;
                iDModel.IsCitizen = citizen;
                iDModel.Age = age;
            }
            else
            {
                throw new Exception("Invalid ID Number, Please double check your Id Number!");
            }

            return iDModel;
        }
         
        private string ValidateAndReturnDateOfBirth(string idNumber)
        {

            try
            {
                if (idNumber.Length == 13)
                {
                    DateTime date = DateTime.ParseExact(idNumber.Substring(0, 2) + "/" + idNumber.Substring(2, 2) + "/" + idNumber.Substring(4, 2), "yy/MM/dd", System.Globalization.CultureInfo.InvariantCulture);

                    int years = DateTime.Now.Year - date.Year;

                    if (years < 0)
                    {
                        date = DateTime.ParseExact("19" + idNumber.Substring(0, 2) + "/" + idNumber.Substring(2, 2) + "/" + idNumber.Substring(4, 2), "yyyy/MM/dd", System.Globalization.CultureInfo.InvariantCulture);
                    }

                    return date.ToShortDateString();
                }
                else
                {
                    throw new Exception("Invalid ID Number");
                }
            }
            catch (Exception exception)
            {
                
                throw new Exception("The date of birth was incorrect");
            }

        }

        private string ReturnCorrectGender(string idNumber)
        {

            if (idNumber.Length == 13)
            {
                if (int.Parse(idNumber.Substring(6, 1)) < 5)
                {
                    return "Female";
                }
                else
                {
                    return "Male";
                }
            }
            else
            {
                throw new Exception("Invalid ID");
            }

        }

        private bool CheckIfCitizen(string idNumber)
        {

            if (idNumber.Length == 13)
            {
                if (int.Parse(idNumber.Substring(10, 1)) == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                throw new Exception("Invalid ID");
            }

        }

        private int ReturnAgeIfValid(string idNumber)
        {

            if (idNumber.Length == 13)
            {
                DateTime birthDate = DateTime.ParseExact(idNumber.Substring(0, 2) + "/" + idNumber.Substring(2, 2) + "/" + idNumber.Substring(4, 2), "yy/MM/dd", System.Globalization.CultureInfo.InvariantCulture);
                int years = DateTime.Now.Year - birthDate.Year;

                if (years < 0)
                {
                    birthDate = DateTime.ParseExact("19" + idNumber.Substring(0, 2) + "/" + idNumber.Substring(2, 2) + "/" + idNumber.Substring(4, 2), "yyyy/MM/dd", System.Globalization.CultureInfo.InvariantCulture);
                    years = DateTime.Now.Year - birthDate.Year;
                }

                if (DateTime.Now.Month < birthDate.Month ||
                    (DateTime.Now.Month == birthDate.Month &&
                    DateTime.Now.Day < birthDate.Day))
                    years--;
                return years;
            }
            else
            {
                throw new Exception("Invalid ID");
            }

        }
    }
}