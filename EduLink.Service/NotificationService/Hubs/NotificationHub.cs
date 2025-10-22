using Microsoft.AspNetCore.SignalR;

namespace EduLink.Service.NotificationService.Hubs
{
    public class NotificationHub:Hub
    {
        public async Task JoinUserGroup(string userID)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"user_{userID}");
        }
        public async Task LeaveUserGroup(string userID)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"user_{userID}");
        }
        public override async Task OnConnectedAsync()
        {
            var userId = Context.User?.FindFirst("sub")?.Value ?? Context.User?.FindFirst("id")?.Value;
            if(!string.IsNullOrEmpty(userId))
                await Groups.AddToGroupAsync(Context.ConnectionId, $"user_{userId}");
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = Context.User?.FindFirst("sub")?.Value
                     ?? Context.User?.FindFirst("id")?.Value;
            if (!string.IsNullOrEmpty(userId))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"user_{userId}");
            }
            await base.OnDisconnectedAsync(exception);
        }
    }
}
