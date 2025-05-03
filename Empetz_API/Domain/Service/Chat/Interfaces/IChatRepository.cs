using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.Chat.Interfaces
{
    public interface IChatRepository
    {
        Task<Message> AddMessageAsync(Message message);
        Task<IList<Message>> GetMessagesByGroup(Guid groupId);
        Task<IList<Message>> GetMessagesByGroupName(string grpName);
    }
}
