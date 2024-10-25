using System;

namespace CNSMarketing.Service.Abstraction.ExternalService;

public interface IMailService
{
    Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true); // Todo Model
    Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true); // Todo Model

    Task SendPasswordResetMailAsync(string to, string userId, string resetToken); // Todo Model
    Task SendCompletedOrderMailAsync(string to, string orderCode, DateTime orderDate, string userName); // Todo Model
}
