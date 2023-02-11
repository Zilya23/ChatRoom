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
    /// Логика взаимодействия для EmployeeFinderPage.xaml
    /// </summary>
    public partial class EmployeeFinderPage : Page
    {
        public List<Departament> Departaments { get; set; }
        public List<Employee> Employees = new List<Employee>();
        public Chatroom ChatroomNow = null;

        public EmployeeFinderPage(Chatroom chatroom)
        {
            InitializeComponent();

            Departaments = BDConnection.connection.Departament.ToList();
            (App.Current.MainWindow as MainWindow).Title = Title;
            ChatroomNow = chatroom; 

            DataContext= this;
        }

        private void tbSearch_SelectionChanged(object sender, RoutedEventArgs e)
        {
            Filter();
        }

        private void lvDepartament_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void Filter()
        {
            Employees = new List<Employee>();
            foreach (var item in Departaments)
            {
                if (item.IsChecked)
                    Employees.AddRange(item.Employee);
            }

            if (tbSearch.Text.Trim().Length != 0)
            {
                Employees = Employees.Where(x => x.Name.Contains(tbSearch.Text.Trim())).ToList();
            }

            lvEmployee.ItemsSource = Employees;
        }

        private void cbDepartament_Checked(object sender, RoutedEventArgs e)
        {
            Filter();
        }

        private void lvEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lvEmployee.SelectedItem != null)
            {
                var item = lvEmployee.SelectedItem as Employee;
                EmployeeChatroom chatroom = new EmployeeChatroom();
                chatroom.IDEmployee = item.ID;
                chatroom.IDChatroom = ChatroomNow.ID;

                var uniqUser = BDConnection.connection.EmployeeChatroom.Where(x => x.IDChatroom == chatroom.ID && x.IDEmployee == chatroom.IDEmployee).FirstOrDefault();
                if(uniqUser == null)
                {
                    BDConnection.connection.EmployeeChatroom.Add(chatroom);
                    BDConnection.connection.SaveChanges();

                    NavigationService.Navigate(new ReadingChatPage(ChatroomNow, null));
                }
                else
                {
                    MessageBox.Show("Этот пользователь уже в чате");
                }
            }
        }
    }
}
