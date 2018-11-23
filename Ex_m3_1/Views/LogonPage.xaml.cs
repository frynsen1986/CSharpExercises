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
        // TODO: Exercise 1: Task 2a: Define the LogonSuccess event handler
        public delegate void MyOwnEventHandler(object sender, EventArgs e);
        public event MyOwnEventHandler LogonSucces;

        #endregion

        #region Logon Validation
        // TODO: Exercise 1: Task 2b: Implement the Logon_Click event handler for the Logon button
        // Simulate logging on (no validation or authentication performed yet)
        public void Logon_Click(object sender, RoutedEventArgs e)
        {
            SessionContext.UserName = username.Text;
            SessionContext.UserRole = userrole.IsChecked.Value ? Role.Teacher : Role.Student;
            if (userrole.IsChecked.Value == false)
                SessionContext.CurrentStudent = "Eric Gruber";

            // Raise 
            //if (LogonSucces != null)    // Check om der er nogen lyttere.
            //{
            //    LogonSucces(sender, e);
            //}
            // Alternativ invocation:
            LogonSucces?.Invoke(sender, e); // ? checker om LogonSucces er null (ingen lyttere) før den kalder invoke på alle.
        }


        #endregion
    }
}
