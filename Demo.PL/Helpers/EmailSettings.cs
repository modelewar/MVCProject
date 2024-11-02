using Demo.DAL.Models;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Demo.PL.Helpers
{
    public static class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            var Client = new SmtpClient("smtp.gmail.com", 587);
            Client.EnableSsl = true;
            Client.Credentials = new NetworkCredential("delewarmohamed@gmail.com", "elgkwqbiqyqtxqrf");
            Client.Send("delewarmohamed@gmail.com",email.To,email.Subject,email.Body);
        }
    }
}
