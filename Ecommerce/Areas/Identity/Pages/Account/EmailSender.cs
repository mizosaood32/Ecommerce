// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using Microsoft.AspNetCore.Identity.UI.Services;

namespace Ecommerce.Areas.Identity.Pages.Account
{
   
        public class EmailSender : IEmailSender
        {
            public Task SendEmailAsync(string email, string subject, string htmlMessage)
            {
                // Log the email for debugging (remove in production)
                Console.WriteLine($"Sending email to: {email} | Subject: {subject}");
                return Task.CompletedTask;
            }
        }


    }

