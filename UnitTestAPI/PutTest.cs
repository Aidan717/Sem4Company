using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Web_API_Service.Controllers;
using Web_API_Service.Models;


namespace UnitTestAPI {
   [TestClass]
    class PutTest {
        TestApiController testApiCon;
        DBscheme._Source schools;

        [TestInitialize]

        public void setUpGetTest() {
            testApiCon = new TestApiController();
            schools = new DBscheme._Source();
            schools.name = "putSchool";
            schools.description = "put test";
            schools.street = "put street";
            schools.city = "put city";
            schools.state = "UP";
            schools.location = null;
            schools.fees = 1000;
            schools.tags = new string[1] { "Test testing" };
            schools.rating = "nice";

        }
        
        [TestMethod] //troer ikke det her er helt rigtigt.Skal jeg bruge get først for at hente en school? 

        public void TestPut() {

            //Arrange
            ResponseStatus responsestatus = new ResponseStatus();
            string id = "10";

            //Act
            var putResult = testApiCon.Put("school", id, schools).Result.Value;
            responsestatus = putResult;

            //Assert
            Assert.IsTrue(putResult.result.Equals("updated"));
        }

    }
}
