using Deliver.Application.Contracts.Service;
using Deliver.Application.Models.Notification;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;

namespace Persistence.Services;

public class FcmServices : INotificationServices
{
    private readonly FirebaseApp _firebaseApp = FirebaseApp.DefaultInstance;

    public async Task<string> SendNotificationAsync(SendNotificationRequest request)
    {
        var message = new Message
        {
            Data = request.Data,
            Token = request.Token,
            Notification = new Notification
            {
                Title = request.Title,
                Body = request.Body
            }
        };

        var messaging = FirebaseMessaging.GetMessaging(_firebaseApp);
        var response = await messaging.SendAsync(message);

        return response;
    }
}