using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradesPrototype.Data
{
    // Types of user
    public enum Role { Teacher, Student };

    public struct Grade
    {
        public int StudentID;
        public string AssessmentDate;
        public string SubjectName;
        public string Assessment;
        public string Comments;
    }

    public struct Student
    {
        public int StudentID;
        public string UserName;
        public string Password;
        public int TeacherID;
        public string FirstName;
        public string LastName;
    }

    public struct Teacher
    {
        public int TeacherID;
        public string UserName;
        public string Password;
        public string FirstName;
        public string LastName;
        public string Class;
    }
}
