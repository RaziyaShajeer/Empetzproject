using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.Chat.DTOs
{
    public class ChatUser
    {
        public int id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string connectionId { get; set; }
        public string status { get; set; }
        public string lastseen { get; set; }
        public ChatUser()
        {

        }
    }
}
