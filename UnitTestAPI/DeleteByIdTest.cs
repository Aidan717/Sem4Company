using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Web_API_Service.Controllers;
using Web_API_Service.Models;

namespace UnitTestAPI {
    [TestClass]
    public class DeleteByIdTest {
        TestApiController testApiCon;
        ResponseStatus status;

        [TestInitialize]
        public void setUpGetTest() {
            testApiCon = new TestApiController();
            status = new ResponseStatus();

        }


        [TestMethod]
        public void TestGet() {

            //Arrange
            string Id = "";
            string isDeleted = "deleted";

            //Act
            status = testApiCon.DelSchool(Id).Result.Value;

            //Assert
            Assert.IsTrue(status.result.Equals(isDeleted), "The status of the request: (" + isDeleted + ") in DB (Schools) when looking for Id: (" + Id + ")");

        }
    }
}
