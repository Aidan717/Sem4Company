using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Web_API_Service.Controllers;
using Web_API_Service.Models;

namespace UnitTestAPI {
    [TestClass]
    public class PostTest {
        Schools._Source testSchool;
        TestApiController testApiCon;

        [TestInitialize]
        public void setUpPostTest() {
            testApiCon = new TestApiController();
            testSchool = new Schools._Source();
            testSchool.name = "Post est city";
            testSchool.description = "Post testing Place";
            testSchool.street = "Test street";
            testSchool.city = "New York";
            testSchool.state = "UP";
            testSchool.zip = "6000";
            testSchool.location = null;
            testSchool.fees = 1000;
            testSchool.tags = new string[1] { "Test testing" };
            testSchool.rating = "Very good";
        }

        [TestMethod]
        public void TestPost() {

            //Arrange
            ResponseStatus testResponse = new ResponseStatus();

            //Act
            var APIPostResult = testApiCon.Post("school", testSchool).Result.Value;
            testResponse = APIPostResult;

            //Assert
            Assert.IsTrue(testResponse.result.Equals("created"));
        }

        [TestMethod]
        public void TestPostParametersNotMet() {

            //Arrange
            ResponseStatus testResponse = new ResponseStatus();

            //Act
            var APIPostResult = testApiCon.PostParametersNotMet("school", testSchool).Result.Value;
            testResponse = APIPostResult;

            //Assert
            Assert.IsFalse(testResponse.result.Equals("created"));
        }
    }
}
