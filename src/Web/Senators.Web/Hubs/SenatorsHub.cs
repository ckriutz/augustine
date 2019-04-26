using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Augustine.Senators.Web.Hubs
{
    public class SenatorsHub : Hub
    {
        public async Task NotifyOnSenatorPage()
        {
            await Clients.Others.SendAsync("DisplayViewNotification");

        }
    }
}
