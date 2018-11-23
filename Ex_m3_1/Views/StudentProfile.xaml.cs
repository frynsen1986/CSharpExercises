using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for StudentProfile.xaml
    /// </summary>
    public partial class StudentProfile : UserControl
    {
        public StudentProfile()
        {
            InitializeComponent();
        }

        #region Event Members
        public event EventHandler Back;
        #endregion

        #region Events
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            // If the user is not a teacher, do nothing
            if (SessionContext.UserRole != Role.Teacher)
            {
                return;
            }

            // If the user is a teacher, raise the Back event
            // The MainWindow page has a handler that catches this event and returns to the Students page
            if (Back != null)
            {
                Back(sender, e);
            }
        }
        #endregion

        // TODO: Exercise 1: Task 4d: Display the details for the current student including the grades for the student
        // The name of the student is available in the CurrentStudent property of the global context
        // Grades data is hardcoded in the XAML code for the StudentProfile view in this version of the prototype
        //public void Refresh()
        //{
        //    Match matchNames = Regex.Match(SessionContext.CurrentStudent
        //        , @"([^]+) ([^]+)");

        //    if (matchNames.Success)
        //    {
        //        firstName.Text = matchNames.Value;
        //        lastName.Text = matchNames.NextMatch().Value;
        //    }

        //    switch (SessionContext.UserRole)
        //    {
        //        case Role.Student:
        //            btnBack.Visibility = Visibility.Hidden;
        //            break;
        //        case Role.Teacher:
        //            btnBack.Visibility = Visibility.Visible;
        //            break;
        //    }

        //}

        public void Refresh()
        {
            // Bind the studentName StackPanel to display the details of the student in the TextBlocks in this panel
            studentName.DataContext = SessionContext.CurrentStudent;

            // If the current user is a student, hide the Back button 
            // (only applicable to teachers who can use the Back button to return to the list of students)
            if (SessionContext.UserRole == Role.Student)
            {
                btnBack.Visibility = Visibility.Hidden;
            }
            else
            {
                btnBack.Visibility = Visibility.Visible;
            }

            // Find all the grades for the student
            ArrayList grades = new ArrayList();
            foreach (Grade grade in DataSource.Grades)
            {
                if (grade.StudentID == SessionContext.CurrentStudent.StudentID)
                {
                    grades.Add(grade);
                }
            }

            // Display the grades in the studentGrades ItemsControl by using databinding
            studentGrades.ItemsSource = grades;
        }
    }
}
