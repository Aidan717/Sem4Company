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
        public async Task TestGet(){

            //Arrange
            string schoolsDB = "schools";
            string pams = "_index=schools";

            schools = testApiCon.GetSchool(schoolsDB, pams).Result.Value;
            
            //Act

            //Assert
            //Assert.IsTrue(schools.hits.total < 0);

        }
    }
}
