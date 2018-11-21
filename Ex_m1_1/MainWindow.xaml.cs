using System;
using System.Collections;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using School.Data;


namespace School
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Connection to the School database
        private SchoolDBEntities schoolContext = null;

        // Field for tracking the currently selected teacher
        private Teacher teacher = null;

        // List for tracking the students assigned to the teacher's class
        private IList studentsInfo = null;

        #region Predefined code

        public MainWindow()
        {
            InitializeComponent();
            // Playing around with lambda expression :).
            saveChanges.Click += (s, e) => { MessageBox.Show("Nothing has been implemented yet" + ((RoutedEventArgs)e).GetType().ToString()); };
        }

        // Connect to the database and display the list of teachers when the window appears
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.schoolContext = new SchoolDBEntities();
            teachersList.DataContext = this.schoolContext.Teachers;
        }

        // When the user selects a different teacher, fetch and display the students for that teacher
        private void teachersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Find the teacher that has been selected
            this.teacher = teachersList.SelectedItem as Teacher;
            this.schoolContext.LoadProperty<Teacher>(this.teacher, s => s.Students);

            // Find the students for this teacher
            this.studentsInfo = ((IListSource)teacher.Students).GetList();

            // Use databinding to display these students
            studentsList.DataContext = this.studentsInfo;
            studentsList.Focus();
        }

        #endregion

        // When the user presses a key, determine whether to add a new student to a class, remove a student from a class, or modify the details of a student
        private void studentsList_KeyDown(object sender, KeyEventArgs e)
        {
            // TODO: Exercise 1: Task 1a: If the user pressed Enter, edit the details for the currently selected student
            // TODO: Exercise 1: Task 2a: Use the StudentsForm to display and edit the details of the student
            // TODO: Exercise 1: Task 2b: Set the title of the form and populate the fields on the form with the details of the student
            // TODO: Exercise 1: Task 3a: Display the form
            // TODO: Exercise 1: Task 3b: When the user closes the form, copy the details back to the student
            // TODO: Exercise 1: Task 3c: Enable saving (changes are not made permanent until they are written back to the database)

            switch (e.Key)
            {
                case Key.Enter:
                    {    //Student st = studentsList.SelectedItem as Student;  // Retrieving the selected item from the studentsList object in the UI, regarless of the sender.
                        Student st = ((ListView)sender).SelectedItem as Student;   // Retrieving the selected item from the listView sender of the event. Require double cast, which introduce a new possible error.
                        editStudent(st);
                        break;
                    }
                case Key.Insert:
                    {   // Create new clean StudentForm.
                        addNewStudent();
                        break;
                    }
                case Key.Delete:
                    {
                        // Select current student, prompt user using MessageBox and delete if accept.
                        Student st = ((ListView)sender).SelectedItem as Student;
                        removeStudent(st);
                        break;
                    }
            }
        }

        private void addNewStudent()
        {
            StudentForm sf = new StudentForm();
            sf.Title = "New Student for Class " + teacher.Class;

            if (sf.ShowDialog().Value)  // If pressing OK, add new student to current teacher.
            {
                Student st = new Student();
                st.FirstName = sf.firstName.Text;
                st.LastName = sf.lastName.Text;
                st.DateOfBirth = DateTime.Parse(sf.dateOfBirth.Text);

                this.teacher.Students.Add(st);

                saveChanges.IsEnabled = true;   // Enable save changes.
            }
        }

        private void removeStudent(Student st)
        {
            MessageBoxResult deletePrompt = MessageBox.Show(
                                        String.Format("Remove {0} {1} from class {2}?",
                                            st.FirstName, st.LastName, this.teacher.Class),
                                        "Delete student",
                                        MessageBoxButton.YesNo);

            if (deletePrompt == MessageBoxResult.Yes)
            {
                schoolContext.Students.DeleteObject(st);
                saveChanges.IsEnabled = true;
            }
        }

        #region Predefined code

        // Save changes back to the database and make them permanent
        private void saveChanges_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        private void studentsList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //Student st = studentsList.SelectedItem as Student;  // Retrieving the selected item from the studentsList object in the UI, regarless of the sender.
            Student st = ((ListView)sender).SelectedItem as Student;   // Retrieving the selected item from the listView sender of the event. Require double cast, which introduce a new possible error.

            editStudent(st);

        }

        private void editStudent(Student st)
        {
            // Create and populate a new instance of the StudentForm
            if (st != null)
            {
                StudentForm sf = new StudentForm();

                sf.Title = "Edit Student Details";
                sf.firstName.Text = st.FirstName;
                sf.lastName.Text = st.LastName;
                sf.dateOfBirth.Text = st.DateOfBirth.ToString("d");
                if (sf.ShowDialog().Value)  // Detecting if user pressed "OK" on the form.
                {
                    st.FirstName = sf.firstName.Text;
                    st.LastName = sf.lastName.Text;
                    st.DateOfBirth = DateTime.Parse(sf.dateOfBirth.Text);

                    // Enable the "Save changes"-button on the form.
                    saveChanges.IsEnabled = true;
                }
            }
        }
    }


    [ValueConversion(typeof(string), typeof(Decimal))]
    class AgeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
                              System.Globalization.CultureInfo culture)
        {
            String strAge = "";

            if (value != null)
            {
                DateTime dtDob = (DateTime)value;
//                DateTime temp = new DateTime(DateTime.Now.Subtract(dtDob).Ticks);
//                int intAge = temp.Year - 1;
                int intAge = new DateTime(DateTime.Now.Subtract(dtDob).Ticks).Year - 1;
                strAge = intAge.ToString();
            }

            return strAge;
        }

        #region Predefined code

        public object ConvertBack(object value, Type targetType, object parameter,
                                  System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
