using Microsoft.VisualStudio.TestTools.UnitTesting;
using VismaInternship.Models;

namespace VismaInternship.UnitTests
{
    [TestClass]
    public class PersonUnitTest
    {
        [TestMethod]
        public void Person_Name_UnitTest()
        {
            Person person = new Person();
            person.Name = "Testas";
            Assert.IsTrue(person.Name == "Testas");
        }
        [TestMethod]
        public void Person_Surname_UnitTest()
        {
            Person person = new Person();
            person.Surname = "Testas";
            Assert.IsTrue(person.Surname == "Testas");
        }

        [TestMethod]
        public void Person_isResponsible_Responsible_UnitTest()
        {
            Person person = new Person();
            person.isResponsible = true;
            Assert.IsTrue(person.isResponsible);
        }

        [TestMethod]
        public void Person_isResponsible_NotResponsible_UnitTest()
        {
            Person person = new Person();
            person.isResponsible = false;
            Assert.IsFalse(person.isResponsible);
        }

    }
}