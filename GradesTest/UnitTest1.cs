using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GradesPrototype.Data;

namespace GradesTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestInitialize]
        public void init()
        {
            DataSource.CreateData();
        }

        [TestMethod]
        public void TestValidGrade()
        {
            Grade grade = new Grade();
            grade.AssessmentDate = DateTime.Today.ToString();

            grade.Assessment = "A+";
            grade.Assessment = "E-";
            grade.Assessment = "C";

            grade.SubjectName = "Math";
            grade.SubjectName = "Science";
        }

        [TestMethod]
        public void TestBadDate()
        {
            Grade grade = new Grade();

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => grade.AssessmentDate = "3018-01-01");
        }

        [TestMethod]
        public void TestDateNotRecognized()
        {
            Grade grade = new Grade();
            Assert.ThrowsException<ArgumentException>(() => grade.AssessmentDate = "Hello World");
        }

        [TestMethod]
        public void TestBadAssessment()
        {
            Grade grade = new Grade();
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => grade.Assessment = "F");
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => grade.Assessment = "1");
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => grade.Assessment = "3");
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => grade.Assessment = "AA+");
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => grade.Assessment = "a");
        }

        [TestMethod]
        public void TestBadSubject()
        {
            var grade = new Grade();

            foreach (string subject in DataSource.ValidSubjects)
                grade.SubjectName = subject;

            Assert.ThrowsException<ArgumentException>(() =>
            {
                grade.SubjectName = "Geology";
            });
        }
    }
}
