using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Citizens.Core
{
    public static class SendGridMessageExtension
    {
        
        const string SENDGRID_APIKEY = "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";
        public static void Send(this SendGridMessage message)
        {
           
            var messages = new List<SendGridMessage>() { message };
            messages.Send();
        }
        public static void Send(this IEnumerable<SendGridMessage> messages)
        {
            var client = new SendGridClient(SENDGRID_APIKEY);
            var responses = messages.SendAsync().GetAwaiter().GetResult();
        }
        private static async Task<List<Response>> SendAsync(this IEnumerable<SendGridMessage> messages)
        {
            var client = new SendGridClient(SENDGRID_APIKEY);
            var list = new List<Response>();
            foreach (var message in messages)
            {
                list.Add(await client.SendEmailAsync(message));
            }
            return list;
        }
    }
}
