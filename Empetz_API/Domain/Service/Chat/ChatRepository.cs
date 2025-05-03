using DAL.Models;
using Domain.Models;
using Domain.Service.Chat.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.Service.Chat
{
    public class ChatRepository : IChatRepository
    {
        private EmpetzContext _context;
        IMessageGroupRepository _messageGroupRepository;

        public ChatRepository(EmpetzContext context, IMessageGroupRepository messageGroupRepository)
        {
            _context = context;
            _messageGroupRepository = messageGroupRepository;
        }

        public async Task<Message> AddMessageAsync(Message message)
        {
            if (message.MessageGroupId!=null)
            {
                _context.Messages.Add(message);
                _context.SaveChanges();

            }
            else
            {
                IList<Guid> ids = new List<Guid>();
                ids.Add(message.FromUserId.Value);
                ids.Add(message.ToUserId.Value);

                var groupslist = GetMessageGroupContainsMembers(ids);
                if (groupslist!=null && groupslist.Count>0)
                {
                    message.MessageGroupId = groupslist[0].Id;
                    message.MessageGroup = groupslist[0];
                    _context.Messages.Add(message);
                    _context.SaveChanges();
                }
                else
                {
                    await _messageGroupRepository.CreateChatGroupAsync(message);
                }
            }
            return message;

        }
        public IList<MessageGroup> GetMessageGroupContainsMembers(IList<Guid> memberIds)
        {

            var allMessageGroups = _context.MessageGroups.Include(mg => mg.GroupMembers) // Ensure GroupMembers are included in the query
         .ToList();

            var messageGroupsWithAllMembers = allMessageGroups
                .Where(messageGroup =>
                    memberIds.All(memberId =>
                        messageGroup.GroupMembers.Any(gm => gm.MemberId == memberId)
                    )
                )
                .ToList();

            return messageGroupsWithAllMembers;

        }


        public async Task<IList<Message>> GetMessagesByGroup(Guid groupId)
        {
            var res = await _context.Messages.Where(e => e.MessageGroupId==groupId).ToListAsync();
            var msgGrp=await _context.MessageGroups.Where(e=>e.Id==groupId).FirstOrDefaultAsync();
            msgGrp.IsNewMessages = false;
            msgGrp.newCount= 0;
            await _context.SaveChangesAsync();
            return res;

        }

        public async Task<IList<Message>> GetMessagesByGroupName(string grpName)
        {
            var grp =await  _context.MessageGroups.Where(e => e.Name==grpName).FirstOrDefaultAsync();
            return await GetMessagesByGroup(grp.Id);
        }
    }
}
