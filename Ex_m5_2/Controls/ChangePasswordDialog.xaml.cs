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
    /// Interaction logic for ChangePasswordDialog.xaml
    /// </summary>
    public partial class ChangePasswordDialog : Window
    {
        public ChangePasswordDialog()
        {
            InitializeComponent();
        }

        // If the user clicks OK to change the password, validate the information that the user has provided
        private void ok_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Exercise 2: Task 4a: Get the details of the current user
            User currentUser;
            if (SessionContext.UserRole == Role.Teacher)
                currentUser = SessionContext.CurrentTeacher;
            else
                currentUser = SessionContext.CurrentStudent;

            // TODO: Exercise 2: Task 4b: Check that the old password is correct for the current user
            if (currentUser.VerifyPassword(this.oldPassword.Password) == false)
            {
                MessageBox.Show("Incorrect password.\nPassword not changed.");
                return;
            }

            // TODO: Exercise 2: Task 4c: Check that the new password and confirm password fields are the same
            if (this.newPassword.Password != this.confirm.Password)
            {
                MessageBox.Show("Confirm password not the same as new password.\nPassword not changed.");
                return;
            }

            // TODO: Exercise 2: Task 4d: Attempt to change the password
            // If the password is not sufficiently complex, display an error message
            if (currentUser.SetPassword(this.newPassword.Password) == false)
            {
                MessageBox.Show("Password insufficiently safe.\nPassword not changed.");
                return;
            }

            // Indicate that the data is valid
            this.DialogResult = true;
        }
    }
}
