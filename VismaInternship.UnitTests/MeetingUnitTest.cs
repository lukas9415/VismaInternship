using Microsoft.VisualStudio.TestTools.UnitTesting;
using VismaInternship.Models;

namespace VismaInternship.UnitTests
{
    [TestClass]
    public class MeetingUnitTest
    {
        [TestMethod]
        public void Meeting_Name_UnitTest()
        {
            Meeting meeting = new Meeting();
            meeting.Name = "Test TEST";
            Assert.IsTrue(meeting.Name == "Test TEST");
        }
        [TestMethod]
        public void Meeting_ResponsiblePerson_UnitTest()
        {
            Meeting meeting = new Meeting();
            Person person = new Person();
            person.Name = "Test";
            person.Surname = "Testovic";
            meeting.ResponsiblePerson = person;
            
            Assert.IsTrue(meeting.ResponsiblePerson.Name == "Test" && meeting.ResponsiblePerson.Surname == "Testovic");
        }
        [TestMethod]
        public void Meeting_Description_UnitTest()
        {
            Meeting meeting = new Meeting();
            meeting.Description = "Testing";

            Assert.IsTrue(meeting.Description == "Testing");
        }
        [TestMethod]
        public void Meeting_Category_UnitTest()
        {
            Meeting meeting = new Meeting();
            meeting.Category = "Testing";

            Assert.IsTrue(meeting.Category == "Testing");
        }
        [TestMethod]
        public void Meeting_Type_UnitTest()
        {
            Meeting meeting = new Meeting();
            meeting.Type = "Testing";

            Assert.IsTrue(meeting.Type == "Testing");
        }
        [TestMethod]
        public void Meeting_StartDate_UnitTest()
        {
            Meeting meeting = new Meeting();
            meeting.StartDate = System.DateTime.Today;

            Assert.IsTrue(meeting.StartDate == System.DateTime.Today);
        }
        [TestMethod]
        public void Meeting_EndDate_UnitTest()
        {
            Meeting meeting = new Meeting();
            meeting.EndDate = System.DateTime.Today;

            Assert.IsTrue(meeting.EndDate == System.DateTime.Today);
        }
        [TestMethod]
        public void Meeting_People_UnitTest()
        {
            Meeting meeting = new Meeting();
            Person person = new Person();
            person.Name = "TestName";
            person.Surname = "TestSurname";
            meeting.People.Add(person);

            Assert.IsTrue(meeting.People.Count == 1 && meeting.People[0].Name == "TestName" && meeting.People[0].Surname == "TestSurname");
        }
        [TestMethod]
        public void Meeting_Load_UnitTest()
        {
            Meeting meeting = new Meeting();
            Person person = new Person();
            person.Name = "TestName";
            person.Surname = "TestSurname";
            meeting.Load(person);

            Assert.IsTrue(meeting.People.Count == 1 && meeting.People[0].Name == "TestName" && meeting.People[0].Surname == "TestSurname");
        }
    }
}