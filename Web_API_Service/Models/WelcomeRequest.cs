using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_API_Service.Models {
    public class WelcomeRequest {
        public string toEmail { get; set; }
        public string userName { get; set; }
    }
}
