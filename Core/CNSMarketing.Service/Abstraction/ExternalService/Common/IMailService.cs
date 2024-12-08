using System;

namespace CNSMarketing.Application.Abstraction.ExternalService.Common;

public interface IMailService
{
    Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true); // Todo1 Model
    Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true); // Todo1 Model

    Task SendPasswordResetMailAsync(string to, string userId, string resetToken); // Todo1 Model
    Task SendCompletedOrderMailAsync(string to, string orderCode, DateTime orderDate, string userName); // Todo1 Model
}
