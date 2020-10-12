using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_API_Service.Models;

namespace Web_API_Service.Service {
    public interface IMailService {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
