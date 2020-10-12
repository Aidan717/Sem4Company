﻿using MailKit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_API_Service.Models;

namespace Web_API_Service.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase {
        private readonly IMailService mailService;
        public MailController(IMailService mailService) {
            this.mailService = mailService;
        }
        [HttpPost("send")]
        public async Task<IActionResult> SendMail([FromForm] MailRequest request) {
            try {
                await mailService.SendEmailAsync(request);
                return Ok();
            } catch (Exception ex) {
                throw;
            }

        }
    }
}
