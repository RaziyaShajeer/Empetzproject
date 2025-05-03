using DAL.Models;
using Domain.Models;
using Domain.Service.Chat.Interfaces;
using Domain.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.Chat
{
    public class MessageGroupRepository: IMessageGroupRepository
    {
        private EmpetzContext _context;
        public MessageGroupRepository(EmpetzContext context)
        {
            this._context = context;
        }
        public async Task<MessageGroup> AddAsync(MessageGroup messageGroup)
        {
            _context.MessageGroups.Add(messageGroup);
             await _context.SaveChangesAsync();
            return messageGroup;
        }

        public async Task CreateChatGroupAsync(Message message)
        {
            try
            {
                MessageGroup group = new MessageGroup();
                string privateGroupName = StringUtils.GetPrivateGroupName(message.From, message.To);
                var grp = _context.MessageGroups.Where(e => e.Name==privateGroupName).Count();
                if (grp > 0)
                {
                    group = await _context.MessageGroups.Where(e => e.Name==privateGroupName).FirstOrDefaultAsync();
                }
                    
                group.Name = privateGroupName;
                group.Messages.Add(message);
                group.newCount++;

               

                group.Messages.Add(message);

                if (grp == 0)
                {
                    GroupMember groupMember = new GroupMember();
                    groupMember.Name=message.From;
                    groupMember.Phone=message.From;
                    groupMember.MemberId=message.FromUserId;

                    GroupMember groupMember2 = new GroupMember();
                    groupMember2.Name=message.To;
                    groupMember2.Phone=message.To;
                    groupMember2.MemberId=message.ToUserId;

                    group.GroupMembers.Add(groupMember);
                    group.GroupMembers.Add(groupMember2);

                    await _context.MessageGroups.AddAsync(group);

                }
                    await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public async Task<MessageGroup> GetMessageGroupByName(string grpName)
        {
           return await _context.MessageGroups.Where(e=>e.Name==grpName).FirstOrDefaultAsync();
        }

        //public async Task<IList<AuthUser>> GetAllUsers()
        //{
        //    return await DbContext.AuthUsers.ToListAsync();
        //}

        public async Task<IList<MessageGroup>> GetMessageGroupByUser(Guid userId)
        {
            var res = await _context.MessageGroups.Include(e => e.GroupMembers).Where(e => e.GroupMembers.Any(g => g.MemberId==userId)).ToListAsync();
            return res;
        }

       

    }
}
