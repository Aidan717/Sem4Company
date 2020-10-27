using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Web_API_Service.Controllers;
using Web_API_Service.Models;

namespace UnitTestAPI {
    [TestClass]
    class DeleteTest {
        TestApiController testApiCon;
        SchoolsFake._Source schools;

        [TestInitialize]

        public void setUpDeleteTest() {
            testApiCon = new TestApiController();



        }



        [TestMethod]

        public void TestDelete() {



            //Arrange

            //Act

            //Assert

        }
    }
}

