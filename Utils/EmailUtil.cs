using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;

namespace Inventory_Management_System.Utils
{
    public class EmailUtil
    {
        public EmailUtil() { }

        
        public async void SendEmail(MimeMessage message)
        {
            ////instantiate mime message
            //var message = new MimeMessage();
            ////From Address
            //message.From.Add(new MailboxAddress("Silent", "silentxtwilight2@gmail.com"));
            ////To Address
            //message.To.Add(new MailboxAddress("Darell", "darellchua2@gmail.com"));
            ////Subject
            //message.Subject = "I learn to sent an email";
            ////Body
            //message.Body = new TextPart("plain")
            //{
            //    Text = "I am using nuget mailkit pakager to send email easily"
            //};

            //Configure and send message
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, false);
                client.Authenticate("silentxtwilight2@gmail.com", "Chelsea1@");
                await client.SendAsync(message);
                client.Disconnect(true);
            }
        }

        //public void SendEmail()
        //{
        //    //instantiate mime message
        //    var message = new MimeMessage();
        //    //From Address
        //    message.From.Add(new MailboxAddress("Silent", "silentxtwilight2@gmail.com"));
        //    //To Address
        //    message.To.Add(new MailboxAddress("Darell", "darellchua2@gmail.com"));
        //    //Subject
        //    message.Subject = "I learn to sent an email";
        //    //Body
        //    message.Body = new TextPart("plain")
        //    {
        //        Text = "I am using nuget mailkit pakager to send email easily"
        //    };

        //    //Configure and send message
        //    using (var client = new SmtpClient())
        //    {
        //        client.Connect("smtp.gmail.com", 587, false);
        //        client.Authenticate("silentxtwilight2@gmail.com", "Chelsea1@");
        //        client.Send(message);
        //        client.Disconnect(true);
        //    }
        //}

        //public void SendEmail(
        //    string emailNameFrom,
        //    string emailFrom,
        //    string emailNameTo,
        //    string emailTo, string )
        //{
        //    var message = new MimeMessage();
        //    message.From.Add(new MailboxAddress("Silent", "silentxtwilight2@gmail.com"));
        //    message.To.Add(new MailboxAddress("Darell", "darellchua2@gmail.com"));
        //    message.Subject = "I learn to sent an email";
        //    message.Body = new TextPart("plain")
        //    {
        //        Text = "I am using nuget mailkit pakager to send email easily"
        //    };

        //    //Configure and send message
        //    using (var client = new SmtpClient())
        //    {
        //        client.Connect("smtp.gmail.com", 587, false);
        //        client.Authenticate("silentxtwilight2@gmail.com", "Chelsea1@");
        //        client.Send(message);
        //        client.Disconnect(true);
        //    }
        //}


    }
}