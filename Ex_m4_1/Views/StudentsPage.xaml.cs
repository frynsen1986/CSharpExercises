using System;
using System.Collections;
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
    /// Interaction logic for StudentsPage.xaml
    /// </summary>
    public partial class StudentsPage : UserControl
    {
        public StudentsPage()
        {
            InitializeComponent();
        }

        #region Display Logic
        public void Refresh()
        {
            // Find students for the current teacher
            // This approach require the DataSource.Students to be sorted. Don't know in regards of performance which approach is faster.
            ArrayList students = new ArrayList();
            foreach (Student student in DataSource.Students)
            {
                if (student.TeacherID == SessionContext.CurrentTeacher.TeacherID)
                {
                    students.Add(student);
                }
            }

            // Alternative way of getting the Students, using LINQ and the buildin "orderby" using the implemented ICompare<Student> in the Student class
            //var query =
            //    from Student student in DataSource.Students
            //    where student.TeacherID == SessionContext.CurrentTeacher.TeacherID
            //    orderby student
            //    select student;

            //ArrayList students = new ArrayList(query.ToArray());

            // Bind the collection to the list item template
            list.ItemsSource = students;

            // Display the class name
            txtClass.Text = String.Format("Class {0}", SessionContext.CurrentTeacher.Class);
        }
        #endregion

        #region Event Members
        public delegate void StudentSelectionHandler(object sender, StudentEventArgs e);
        public event StudentSelectionHandler StudentSelected;
        #endregion

        #region Event Handlers
        private void Student_Click(object sender, RoutedEventArgs e)
        {
            Button itemClicked = sender as Button;
            if (itemClicked != null)
            {
                // Find out which student was clicked
                int studentID = (int)itemClicked.Tag;
                if (StudentSelected != null)
                {
                    // Find the details of the student by examining the DataContext of the Button
                    Student student = (Student)itemClicked.DataContext;

                    // Raise the StudentSelected event (handled by MainWindow) to display the details for this student
                    StudentSelected(sender, new StudentEventArgs(student));
                }
            }
        }
        #endregion
    }

    // EventArgs class for passing Student information to an event
    public class StudentEventArgs : EventArgs
    {
        public Student Child { get; set; }

        public StudentEventArgs(Student s)
        {
            Child = s;
        }
    }
}
