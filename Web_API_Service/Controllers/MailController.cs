﻿using MailKit;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_API_Service.Models;
using System.Text.Json;

namespace Web_API_Service.Controllers {

    //[Route("api/[controller]")]
    //[ApiController]
    public class MailController : Controller {
        private readonly Service.IMailService mailService;
        public MailController(Service.IMailService mailService) {
            this.mailService = mailService;
        }

        //[HttpPost("send")]
        //public async Task<IActionResult> SendMail([FromForm] MailRequest request) {
        //    try {
        //        await mailService.SendEmailAsync(request);
        //        return Ok();
        //    } catch (Exception ex) {
        //        throw;
        //    }
        //}

        //[HttpPost("welcome")]
        //public async Task<IActionResult> SendWelcomeMail([FromForm] WelcomeRequest request) {
        //    try {
        //        await mailService.SendWelcomeEmailAsync(request);
        //        return Ok();
        //    } catch (Exception ex) {
        //        throw;
        //    }
        //}

        //[HttpPost("warning")]
        public async Task<IActionResult> SendWarningMail([FromForm] MailRequest warningRequest) {
            string Query = "Query ting og sager";
            string Destination = "Destinations ting og sager";
            string Error = "Error ting og sager";

            try {
                await mailService.SendWarningEmailAsync(Query, Destination, Error);
                return Ok();
            } catch (Exception ex) {
                throw;
            }
        }
    }
}