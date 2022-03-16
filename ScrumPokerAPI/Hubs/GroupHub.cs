using Microsoft.AspNetCore.SignalR;
using ScrumPokerAPI.Data;
using ScrumPokerAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScrumPokerAPI.Hubs
{
    public class GroupHub:Hub
    {
        private readonly IGroupService _groupService;

        public GroupHub(IGroupService groupService)
        {
            _groupService = groupService;
        }


        /// <summary>
        /// Bağlanan kullanıcıya clientId bilgisini döner.
        /// Log işlemi eklenebilir.
        /// </summary>
        public async override Task OnConnectedAsync()
        {
            Dummy.ConnectionIdList.Add(this.Context.ConnectionId);
            await this.Clients.Caller.SendAsync("connectionInfo",this.Context.ConnectionId);
        }

        /// <summary>
        /// Bağlantısı kapanan kullanıcının bağlı olduğu gruplardan çıkarma işlemini yapar.
        /// </summary>
        public async override Task OnDisconnectedAsync(Exception exception)
        {
            await _groupService.Disconnect(this.Context.ConnectionId);
            Dummy.ConnectionIdList.Remove(this.Context.ConnectionId);
        }

    }
}
