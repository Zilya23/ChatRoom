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
            (App.Current.MainWindow as MainWindow).Title = Title;
            DataContext = this;
        }

        private void btnEmpSearch_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EmployeeFinderPage(null));
        }

        private void btnCloseApp_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void lvChat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lvChat.SelectedItem != null)
            {
                var item = lvChat.SelectedItem as Chatroom;

                EmployeeChatroom userInChat = BDConnection.connection.EmployeeChatroom.Where(x => x.IDChatroom == item.ID
                && x.IDEmployee == AuthorizPage.userExsist.ID).FirstOrDefault();
                if(userInChat == null)
                {
                    var messUser = MessageBox.Show("You dont be in chat. You won`t enter in chat?", "Attection", MessageBoxButton.YesNo);
                    if (messUser == MessageBoxResult.Yes)
                    {
                        EmployeeChatroom employeeChatroom = new EmployeeChatroom();
                        employeeChatroom.IDChatroom = item.ID;
                        employeeChatroom.IDEmployee = AuthorizPage.userExsist.ID;
                        BDConnection.connection.EmployeeChatroom.Add(employeeChatroom);
                        BDConnection.connection.SaveChanges();
                        NavigationService.Navigate(new ReadingChatPage(item));
                    }
                }
                else
                {
                    NavigationService.Navigate(new ReadingChatPage(item));
                }
            }
        }
    }
}
