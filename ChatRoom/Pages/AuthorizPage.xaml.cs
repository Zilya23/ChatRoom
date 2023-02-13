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
using ChatRoom.DataBase;

namespace ChatRoom.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthorizPage.xaml
    /// </summary>
    public partial class AuthorizPage : Page
    {
        public static Employee userExsist = new Employee();
        public AuthorizPage()
        {
            InitializeComponent();
            tbUsername.Text = Properties.Settings.Default.Login;
            tbPassword.Password = Properties.Settings.Default.Password;
            (App.Current.MainWindow as MainWindow).Title = Title;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            List<Employee> users = new List<Employee>(BDConnection.connection.Employee.ToList());
            string login = tbUsername.Text.Trim();
            string password = tbPassword.Password.Trim();
            userExsist = users.Where(user => user.Username == login && user.Password == password).FirstOrDefault();
            if (userExsist != null)
            {
                if(cbRemember.IsChecked == true)
                {
                    Properties.Settings.Default.Password = tbPassword.Password.Trim();
                    Properties.Settings.Default.Login = tbUsername.Text.Trim();
                    Properties.Settings.Default.Save();
                }
                else
                {
                    Properties.Settings.Default.Password = null;
                    Properties.Settings.Default.Login = null;
                    Properties.Settings.Default.Save();
                }
                NavigationService.Navigate(new MainChatPage(userExsist));
            }
            else
            {
                MessageBox.Show("Error, new login or new password!");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
