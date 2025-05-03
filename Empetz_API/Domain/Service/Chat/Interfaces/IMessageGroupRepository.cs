using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.Chat.Interfaces
{
    public interface IMessageGroupRepository
    {
        Task<MessageGroup> AddAsync(MessageGroup messageGroup);
        Task CreateChatGroupAsync(Message message);
        Task<MessageGroup> GetMessageGroupByName(string grpName);

        //Task<IList<AuthUser>> GetAllUsers();
        Task<IList<MessageGroup>> GetMessageGroupByUser(Guid userId);
    }
}
