using System;
using System.Windows;

namespace School
{
    /// <summary>
    /// Interaction logic for StudentForm.xaml
    /// </summary>
    public partial class StudentForm : Window
    {
        #region Predefined code

        public StudentForm()
        {
            InitializeComponent();
        }

        private void ok_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(this.firstName.Text))
            {
                MessageBox.Show("The student must have a first name", "Error");
                return;
            }

            if (string.IsNullOrEmpty(this.lastName.Text))
            {
                MessageBox.Show("The student must have a last name", "Error");
                return;
            }

            DateTime dtDOB;
            bool isValidDate = DateTime.TryParse(this.dateOfBirth.Text, out dtDOB);
            if (!isValidDate)
            {
                MessageBox.Show("The date of birth must be a valid date", "Error");
                return;
            }

            int age = AgeCalculator.CalcAge(dtDOB);
            if (age < 5)
            {
                MessageBox.Show("The student must at least be 5 years old", "Error");
                return;
            }

            this.DialogResult = true;


        }

        #endregion
    }
}
