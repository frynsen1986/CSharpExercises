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
        #region Properties and fields

        public int StudentID { get; set; }

        private string _assessmentDate;
        public string AssessmentDate
        {
            get => _assessmentDate;
            set
            {
                DateTime input;
                try
                {
                    input = DateTime.Parse(value);
                }
                catch (Exception ex)
                {
                    throw new ArgumentException("Invalid Date", ex);
                }

                if (input.CompareTo(DateTime.Now) > 0)
                    throw new ArgumentOutOfRangeException(
                        string.Format("Date can not be later than today ({0})", DateTime.Now.ToString()));

                _assessmentDate = input.ToString();
            }
        }

        private string _subjectName;
        public string SubjectName
        {
            get => _subjectName;
            set
            {
                if (!DataSource.ValidSubjects.Contains(value))
                    throw new ArgumentException("Not a valid Subject");

                _subjectName = value;
            }
        }

        private string _assessment;
        public string Assessment
        {
            get => _assessment;
            set
            {
                bool isValidGrade = Regex.IsMatch(value, @"^[A-E]{1}[+-]?$");
                if (!isValidGrade)
                    throw new ArgumentOutOfRangeException("Invalid Grade");

                _assessment = value;
            }
        }

        public string Comments { get; set; }

        #endregion Properties and fields

        #region Constructors
        public Grade() : this(0, DateTime.Now.ToString("d"), "Math", "A", "") { }

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
