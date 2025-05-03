using AutoMapper;
using Domain.Models;
using Domain.Service.Chat.Interfaces;
using Domain.Service.User.Interfaces;
using Domain.Utils;
using Empetz_API.API.Chat.RequestObject;
using Empetz_API.Hubs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Empetz_API.API.Chat
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {

        IChatRepository chatRepository;
        IMessageGroupRepository messageGroupRepository;
        IUserService _userService;
        IMapper mapper;
        private readonly IHubContext<ChatHub> _chatHubContext;
        public ChatController(IChatRepository _chatRepository, IMessageGroupRepository _messageGroupRepository,IMapper _mapper, IUserService userService,  IHubContext<ChatHub> chatHubContext)
        {
            chatRepository= _chatRepository;
            messageGroupRepository= _messageGroupRepository;
            mapper= _mapper;
            _chatHubContext = chatHubContext;
            _userService= userService;
        }

        //[HttpPost]
        //[Route("group/{groupId}/message")]
        //public IActionResult AddMessage(Message message, Guid groupId)
        //{
        //    chatRepository.AddMessageAsync(message);
        //    return Ok();
        //}

        [HttpPost]
        [Route("group")]
        public async Task<IActionResult> CreateNewChatGroupAsync(MessageGroupCreateRequestObject messageGroupCreateObj)
        {
            IList<GroupMember> groupMembers = new List<GroupMember>() {
                     new GroupMember() { Phone=messageGroupCreateObj.Phone1 },
                     new GroupMember() { Phone=messageGroupCreateObj.Phone2 } };
            MessageGroup messageGroup = new();
            messageGroup.GroupMembers=groupMembers;
            var res = await messageGroupRepository.AddAsync(messageGroup);
            return Ok(res);
        }



        [HttpGet]
        [Route("group/{groupId}/messages")]
        public async Task<IActionResult> GetChatByGroupAsync(Guid groupId)
        {
            IList<Message> res = await chatRepository.GetMessagesByGroup(groupId);
            var messages= mapper.Map<List<MessageDto>>(res);
            return Ok(messages);
        }
        [HttpGet]
        [Route("group/messages")]
        public async Task<IActionResult> GetChatByNumbers(string phone1,string phone2)
        {
            var grpName=StringUtils.GetPrivateGroupName(phone1,phone2);
            MessageGroup grp = await messageGroupRepository.GetMessageGroupByName(grpName);
            if (grp!=null)
            {
                IList<Message> res = await chatRepository.GetMessagesByGroup(grp.Id);
                return Ok(res);
            }
            else return BadRequest("Group is not Created");
        }

        [HttpGet]
        [Route("user/{userId}/chatgroup")]
        public async Task<IActionResult> GetGroupsByUserAsync(Guid userId)
        {
            IList<MessageGroup> res = await messageGroupRepository.GetMessageGroupByUser(userId);
            return Ok(res);
        }

        [HttpGet]
        [Route("user/chatgroup")]
        public async Task<IActionResult> GetGroupsByUserAsync(String phone)
        {
            var user= await _userService.GetByPhone(phone);
            if (user!=null)
            {
                IList<MessageGroup> res = await messageGroupRepository.GetMessageGroupByUser(user.Id);
                return Ok(res);
            }
            return BadRequest("No User found on this number");
        }

        //[HttpGet]
        //[Route("all-users")]
        //public async Task<IActionResult> GetAllUsers()
        //{
        //    IList<AuthUser> res = await messageGroupRepository.GetAllUsers();
        //    var returnUsers = mapper.Map<IList<ChatUserDto>>(res);
        //    return Ok(returnUsers);
        //}


        [HttpPost]
        [Route("message")]
        public async Task<IActionResult> CreateMessages(Message message)
        {
            try
            {
                message=await chatRepository.AddMessageAsync(message);
                //AuthUser touser = await _authUserRepository.GetAuthUserByUserId(message.ToUserId.Value);
                //await _chatHubContext.Clients.Client(touser.ConnectionId).InvokeAsync<string>("notifyNewMessageAsync", message, default); // Assuming string return type
            }
            catch (Exception ex) { }
            return Ok();
        }

        //FOR NOTIFICATION

    }
}
