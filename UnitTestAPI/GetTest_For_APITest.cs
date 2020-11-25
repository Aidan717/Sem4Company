using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Web_API_Service.Controllers;
using Web_API_Service.Models;


namespace GetTest_For_APITest { 
    [TestClass]
    public class GetTest_For_APITest {
        TestApiController testApiCon;
        Schools schools;

        [TestInitialize]
        public void setUpGetTest() {
            testApiCon = new TestApiController();
            schools = new Schools();

        }


        [TestMethod]
        public void TestGet(){

            //Arrange
            string chosenDB = "schools";
            string query = "zip=9000";
            int moreThen = 0;

            //Act
            schools = testApiCon.GetSchool(chosenDB, query).Result.Value;

            //Assert
            Assert.IsTrue(schools.hits.total.value > moreThen, "The Get didnt return anything that was Greater than (" + moreThen + ") in DB (" + chosenDB + ") when looking for (" + query + ")");

        }
    }
}
