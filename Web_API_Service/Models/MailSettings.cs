using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_API_Service.Models {
    public class MailSettings {
        public string mailReciever { get; set; }
        public string mailSender { get; set; }
        public string displayName { get; set; }
        public string password { get; set; }
        public string host { get; set; }
        public int port { get; set; }
    }
}
