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

                        break;
                    }
                case Key.Insert:
                    {   // Create new clean StudentForm.
                        StudentForm sf = new StudentForm();
                        sf.Title = "New Student for Class " + teacher.Class;

                        if (sf.ShowDialog().Value)  // If pressing OK, add new student to current teacher.
                        {
                            Student st = new Student();
                            st.FirstName = sf.firstName.Text;
                            st.LastName = sf.lastName.Text;
                            st.DateOfBirth = DateTime.Parse(sf.dateOfBirth.Text);

                            teacher.Students.Add(st);

                            saveChanges.IsEnabled = true;   // Enable save changes.
                        }

                        break;
                    }
            }
        }

        #region Predefined code

        // Save changes back to the database and make them permanent
        private void saveChanges_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion
    }

    [ValueConversion(typeof(string), typeof(Decimal))]
    class AgeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
                              System.Globalization.CultureInfo culture)
        {
            return "";
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
