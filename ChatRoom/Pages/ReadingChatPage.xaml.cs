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
    /// Логика взаимодействия для ReadingChatPage.xaml
    /// </summary>
    public partial class ReadingChatPage : Page
    {
        public List<ChatMessage> ChatMessages { get; set; }
        public List<EmployeeChatroom> EmployeeChatrooms { get; set; }
        public Chatroom Chatroom { get; set; }
        public ReadingChatPage(Chatroom chatRoom)
        {
            InitializeComponent();
            (App.Current.MainWindow as MainWindow).Title = chatRoom.Topic;
            Chatroom = chatRoom;
            ChatMessages = BDConnection.connection.ChatMessage.Where(x => x.ChatroomID== chatRoom.ID).ToList();
            EmployeeChatrooms = BDConnection.connection.EmployeeChatroom.Where(x => x.IDChatroom == chatRoom.ID).ToList();

            DataContext = this;
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            if(tbChat.Text.Trim().Length != 0)
            {
                ChatMessage message = new ChatMessage();
                message.SenderID = AuthorizPage.userExsist.ID;
                message.ChatroomID = Chatroom.ID;
                message.Date= DateTime.Now;
                message.Message = tbChat.Text.Trim();
                BDConnection.connection.ChatMessage.Add(message);
                BDConnection.connection.SaveChanges();

                ChatMessages = BDConnection.connection.ChatMessage.Where(x => x.ChatroomID == Chatroom.ID).ToList();
                EmployeeChatrooms = BDConnection.connection.EmployeeChatroom.Where(x => x.IDChatroom == Chatroom.ID).ToList();

                lvChat.ItemsSource= ChatMessages;
                lvEmployee.ItemsSource= EmployeeChatrooms;

                tbChat.Text = "";
            }
        }
    }
}
