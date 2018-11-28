using System;
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
using System.Windows.Shapes;
using GradesPrototype.Data;
using GradesPrototype.Services;

namespace GradesPrototype.Controls
{
    /// <summary>
    /// Interaction logic for AssignStudentDialog.xaml
    /// </summary>
    public partial class AssignStudentDialog : Window
    {
        public AssignStudentDialog()
        {
            InitializeComponent();
        }

        // TODO: Exercise 4: Task 3b: Refresh the display of unassigned students
        private void Refresh()
        {
            var unassignedStudentsQuery =
                from Student stud in DataSource.Students
                where stud.TeacherID == 0
                orderby stud
                select stud;

            if (unassignedStudentsQuery.Count() > 0)
            {
                var studentList = new System.Collections.ArrayList(unassignedStudentsQuery.ToArray());
                txtMessage.Visibility = Visibility.Collapsed;
                list.ItemsSource = studentList;
                list.Visibility = Visibility.Visible;
            }
            else // no students are unassigned
            {
                txtMessage.Visibility = Visibility.Visible;
                list.Visibility = Visibility.Collapsed;
            }
        }

        private void AssignStudentDialog_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        // TODO: Exercise 4: Task 3a: Enroll a student in the teacher's class
        private void Student_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                Student selectedStudent = (from Student stud in DataSource.Students
                                           where stud.StudentID == (int)btn.Tag
                                           select stud).FirstOrDefault();

                MessageBoxResult shouldEnroll = MessageBox.Show(
                        $"Enroll {selectedStudent.FirstName} {selectedStudent.LastName} in class?"
                        , "Accept enrollment in class."
                        , MessageBoxButton.YesNo);

                if (shouldEnroll == MessageBoxResult.Yes)
                {
                    SessionContext.CurrentTeacher.EnrollInClass(selectedStudent);
                }

                Refresh();
            }
            catch (Exception)
            {
                MessageBox.Show("Something happened", "Exception caught!");
                throw;
            }
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            // Close the dialog box
            this.Close();
        }
    }
}
