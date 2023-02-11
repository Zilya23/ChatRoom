using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatRoom.DataBase
{
    public static class BDConnection
    {
        public static ChatRoomEntities connection = new ChatRoomEntities(); 
    }
}
