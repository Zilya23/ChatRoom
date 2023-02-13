using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatRoom.DataBase
{

    public partial class Chatroom
    {
        public string LastChatMessage
        {
            get
            {
                try
                {
                    return this.ChatMessage.Max(x => (DateTime)x.Date).ToString();
                }
                catch (Exception)
                {
                    return "Пусто";
                }
            }
        }

    }

    public partial class Departament
    {
        public bool IsChecked { get; set; } = false;
    }
}
