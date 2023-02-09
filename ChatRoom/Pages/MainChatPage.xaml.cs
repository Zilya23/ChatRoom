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
    /// Логика взаимодействия для MainChatPage.xaml
    /// </summary>
    public partial class MainChatPage : Page
    {
        public List<Chatroom> chatrooms { get; set; }
        public MainChatPage(Employee employee)
        {
            InitializeComponent();
            tbHello.Text = tbHello.Text + employee.Name + "!";
            chatrooms = BDConnection.connection.Chatroom.ToList();
            DataContext = this;
        }

        private void btnEmpSearch_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EmployeeFinderPage());
        }

        private void btnCloseApp_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
