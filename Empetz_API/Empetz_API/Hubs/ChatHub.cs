using AutoMapper;
using Domain.Models;
using Domain.Service.Chat.DTOs;
using Domain.Service.Chat.Interfaces;
using Domain.Service.User;
using Domain.Service.User.DTO;
using Domain.Service.User.Interfaces;
using Empetz_API.API.Chat.RequestObject;
using Microsoft.AspNetCore.SignalR;
using System.Numerics;

namespace Empetz_API.Hubs
{
    public class ChatHub: Hub
    {
        IUserService _userService;
        IMessageGroupRepository _messageGroupRepository;
        IChatRepository _chatRepository;
        IMapper _mapper;
        public ChatHub(IChatRepository chatRepository, IMessageGroupRepository messageGroupRepository, IUserService userService, IMapper mapper)
        {
            _chatRepository = chatRepository;
            _messageGroupRepository=messageGroupRepository;
            _userService=userService;
            _mapper=mapper;
        }
       

        public override async Task OnConnectedAsync()
        {
            string phone = Context.GetHttpContext().Request.Query["phone"];
           await _userService.UpdateUserConnectionId(phone, Context.ConnectionId,true);
             await base.OnConnectedAsync();
            await DisplayOnlineUsers();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            string phone =  Context.GetHttpContext().Request.Query["phone"];
            await _userService.UpdateUserConnectionId(phone, null, false);
            //await OnlineUsersListAltered();
            await DisplayOnlineUsers();
        }

        public async Task CreatePrivateChat(Message message)
        {
            try
            {
                UserDto fromUser = await _userService.GetByPhone(message.From);
                message.FromUserId = fromUser.Id;

                UserDto toUser = await _userService.GetByPhone(message.To);
                message.ToUserId = toUser.Id;

                string privateGroupName = GetPrivateGroupName(message.From, message.To);
                await _messageGroupRepository.CreateChatGroupAsync(message);
                message.GroupName=privateGroupName;
                MessageDto messagedto = _mapper.Map<MessageDto>(message);
                messagedto.FromName=fromUser.FirstName;
                messagedto.ToName=toUser.FirstName;
                if (toUser.ConnectionId != "")
                    await Clients.Client(toUser.ConnectionId).SendAsync("MessageReceived", messagedto);
     //           else
					//await _userService.updateNotification(toUser.Phone, true);

				//await Groups.AddToGroupAsync(Context.ConnectionId, privateGroupName);
				//var authUser = await _authUserRepository.GetAuthUserByUserEmail(message.To);
				//if (authUser!=null&&authUser.ConnectionId !=null)
				//{
				//    await Groups.AddToGroupAsync(authUser.ConnectionId, privateGroupName);
				//    await Clients.Client(authUser.ConnectionId).SendAsync("OpenPrivateChat", message);
				//}

			}
            catch(Exception ex) {
            
            
            }
        }

        private async Task DisplayOnlineUsers()
        {
            try
            {
                List<UserDto> onlineUserslist = await _userService.GetOnlineUsers();
                await Clients.All.SendAsync("OnlineUsers", onlineUserslist);
            }
            catch (Exception ex)
            {
               
            }

        }

        private string GetPrivateGroupName(string from, string to)
        {
            var stringToCompare = string.CompareOrdinal(from, to)<0;
            return stringToCompare ? $"{from}-{to}" : $"{to}-{from}";

        }

        public async Task AddMessageToChat(string user, string message)
        {
            await Clients.All.SendAsync("GetThatMessageDude", user, message);
        }

        public async Task OnlineUsersListAltered()
        {
            await Clients.All.SendAsync("OnlineUsersListAltered");
        }
    }
}
