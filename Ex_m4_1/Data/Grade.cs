using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GradesPrototype.Data
{
    // Types of user
    public enum Role { Teacher, Student };

    // WPF Databinding requires properties

    // TODO: Exercise 1: Task 1a: Convert Grade into a class and define constructors
    public class Grade
    {
        public int StudentID { get; set; }
        public string AssessmentDate { get; set; }
        public string SubjectName { get; set; }
        public string Assessment { get; set; }
        public string Comments { get; set; }

        #region Constructors
        public Grade() : this(0, DateTime.Now.ToString("d"), "A", "Math", "") { }

        public Grade(int studentID, string assessmentDate, string subjectName, string assessment
            , string comments)
        {
            this.StudentID = studentID;
            this.AssessmentDate = assessmentDate;
            this.SubjectName = subjectName;
            this.Assessment = assessment;
            this.Comments = comments;
        }

        #endregion Constructors
    }

    // TODO: Exercise 1: Task 2a: Convert Student into a class, make the password property write-only, add the VerifyPassword method, and define constructors
    public class Student
    {
        #region Properties
        public int StudentID { get; set; }
        public string UserName { get; set; }
        public string Password { private get; set; }
        public int TeacherID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        #endregion Properties

        #region Constructors
        public Student() : this(0, "", "", "", "", 0) { }

        public Student(int studentID, string userName, string password, string firstName
            , string lastName, int teacherID)
        {
            this.StudentID = studentID;
            this.UserName = userName;
            this.Password = password;
            this.FirstName = firstName;
            this.LastName = LastName;
            this.TeacherID = teacherID;
        }
        #endregion Constructors

        #region Methods
        public bool VerifyPassword(string pw)
        {
            int pwIsCorrect = Password.CompareTo(pw);
            return (pwIsCorrect == 0 ? true : false);

        }
        #endregion Methods

    }

    // TODO: Exercise 1: Task 2b: Convert Teacher into a class, make the password property write-only, add the VerifyPassword method, and define constructors
    public class Teacher
    {
        public int TeacherID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Class { get; set; }

        #region Constructors

        public Teacher() : this(0, "", "", "", "", "") { }

        public Teacher(int teacherID, string userName, string password, string firstName
            , string lastName, string className)
        {
            this.TeacherID = teacherID;
            this.UserName = userName;
            this.Password = password;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Class = className;
        }

        #endregion Constructors

        #region Methods
        public bool VerifyPassword(string pw)
        {
            int pwIsCorrect = Password.CompareTo(pw);
            return (pwIsCorrect == 0 ? true : false);

        }
        #endregion Methods
    }
}
