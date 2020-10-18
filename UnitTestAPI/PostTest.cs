using System;
using System.Collections.Generic;
using System.Linq;
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
        ResponseStatus testResponse;
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
            testResponse = new ResponseStatus();

            //Act

            //Assert

        }
    }
}
