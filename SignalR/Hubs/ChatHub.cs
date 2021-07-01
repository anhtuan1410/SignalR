using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }

    public class ChatHubGroup : Hub
    {
        public Task JoinGroup(string group)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, group);
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task SendMessageGroup(MyMesseage msg)
        {
            await Clients.All.SendAsync("ReceiveMessage", msg.msg);
            await Clients.Groups(msg.groupname).SendAsync("ReceiveMessageGroup", msg.msg);
        }

        //public Task SendMessageToCaller(string user, string message)
        //{
        //    return Clients.Caller.SendAsync("ReceiveMessage", user, message);
        //}

        //public Task SendMessageToGroup(string user, string message)
        //{
        //    return Clients.Group("SignalR Users").SendAsync("ReceiveMessage", user, message);
        //}
    }

    public class MyMesseage
    {
        public string msg { get; set; }
        public string groupname { get; set; }
    }


}
