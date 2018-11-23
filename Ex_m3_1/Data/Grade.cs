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
        int StudentID;
        string AssessmentDate;
        string SubjectName;
        string Assessment;
        string Comments;
    }

    public struct Student
    {
        int StudentID;
        string UserName;
        string Password;
        int TeacherID;
        string FirstName;
        string LastName;
    }

    public struct Teacher
    {
        int TeacherID;
        string UserName;
        string Password;
        string FirstName;
        string LastName;
        string Class;
    }
}
