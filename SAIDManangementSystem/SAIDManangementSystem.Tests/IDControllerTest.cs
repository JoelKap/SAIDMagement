using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SAIDManangementSystem.BusinessLayerLogic;
using SAIDManangementSystem.Models;

namespace SAIDManangementSystem.Tests
{
    [TestClass]
    public class IdControllerTest
    {
       
        [TestMethod]
        public void Get_UserWithValidIdNumber_ShouldValidateAndPass_AndReturn_UserIdModel()
        {
            const string idNumber = "8511136005186";

            var bll = new Idbll();
            var model = bll.GetIdUserDetailByIdNumber(idNumber);

            Assert.AreEqual(idNumber, model.SAId);
        }
         
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Get_UserWithInvalidIdNumber_ShouldValidateAndFail()
        {
            const string idNumber = "8511136005187";
            var bll = new Idbll();
           bll.GetIdUserDetailByIdNumber(idNumber);
        }
          

        [TestMethod]
        public void Generate_UseValidIdNumber_ShouldPass_AndReturn_UserIdModel()
        {
           var bll = new Idbll();
            var model = bll.GetValidGeneratedId();
            Assert.IsNotNull(model);
            
        }
    }
}
