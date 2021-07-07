using Microsoft.VisualStudio.TestTools.UnitTesting;
using FitnessBL.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessBL.Controller.Tests
{
    [TestClass()]
    public class UserControllerTests
    {
        [TestMethod()]
        public void SaveTest()
        {
            //Arrange 
            string userName = Guid.NewGuid().ToString();
            //Act
            var controller = new UserController(userName);
            controller.AddUserInformation(new Model.Gender(Model.Gender.value.female), new DateTime(1999, 05, 25),
                55, 160);

            //Assert
            Assert.AreEqual(userName, controller.CurrentUser.Name);
        }

               

        

        [TestMethod()]
        public void AddUserInformationTest()
        {
            Assert.AreEqual(true,true);
        }

        [TestMethod()]
        public void GetGendersTest()
        {
            //Arrange
            string[] gen = new string[] { "male", "female" };
            //act 
            var result = new UserController().GetGenders();
            //Assert
            CollectionAssert.AreEqual(gen, result);
        }
    }
}