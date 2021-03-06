﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GradesPrototype.Data;
using GradesPrototype.Services;

namespace GradesPrototype.Views
{
    /// <summary>
    /// Interaction logic for LogonPage.xaml
    /// </summary>
    public partial class LogonPage : UserControl
    {
        public LogonPage()
        {
            InitializeComponent();
        }

        #region Event Members
        public event EventHandler LogonSuccess;
        public event EventHandler LogonFailed;

        // TODO: Exercise 3: Task 1a: Define LogonFailed event

        #endregion

        #region Logon Validation

        // TODO: Exercise 3: Task 1b: Validate the username and password against the Users collection in the MainWindow window
        private void Logon_Click(object sender, RoutedEventArgs e)
        {
            var teacherQuery =
                from Teacher teacher in DataSource.Teachers
                where teacher.UserName == username.Text &&    // User exists
                        teacher.Password == password.Password   // Password is correct
                select teacher;

            var studentQuery =
                from Student student in DataSource.Students
                where student.UserName == username.Text &&
                      student.Password == password.Password
                select student;

            if (teacherQuery.Count() == 1)  // Only if there's exactly one match!
            {
                Teacher signedInTeacher = teacherQuery.First();

                // Save current teacher in SessionContext
                SessionContext.UserName = signedInTeacher.UserName;
                SessionContext.UserID = signedInTeacher.TeacherID;
                SessionContext.UserRole = Role.Teacher;
                SessionContext.CurrentTeacher = signedInTeacher;

                // Raise succes event
                LogonSuccess?.Invoke(sender, e); // ? checker om LogonSucces er null (ingen lyttere) før den kalder invoke på alle.
                return;
            }
            else if (studentQuery.Count() == 1)
            {
                Student signedInStudent = studentQuery.First();

                // Save current teacher in SessionContext
                SessionContext.UserName = signedInStudent.UserName;
                SessionContext.UserID = signedInStudent.StudentID;
                SessionContext.UserRole = Role.Student;
                SessionContext.CurrentStudent = signedInStudent;

                LogonSuccess?.Invoke(sender, e); // ? checker om LogonSucces er null (ingen lyttere) før den kalder invoke på alle.
                return;
            }

            LogonFailed?.Invoke(sender, e); // Raise failed event
        }
        #endregion
    }
}
