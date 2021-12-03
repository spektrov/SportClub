using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SportClub.Miscellaneous
{
    class EmailSender
    {
        private readonly string _addressReciever;
        private readonly string _nameReciever;
        private readonly string _subject;
        private readonly string _body;
        private readonly bool _valid;

        public EmailSender(string addressReciever, string nameReciever, string subject, string body)
        {

            if (string.IsNullOrEmpty(addressReciever)
                || string.IsNullOrEmpty(nameReciever)
                || string.IsNullOrEmpty(subject)
                || string.IsNullOrEmpty(body))
            {
                _valid = false;
            }
            else
            {
                _valid = true;
            }

            _addressReciever = addressReciever;
            _nameReciever = nameReciever;
            _subject = subject;
            _body = body;
        }


        public async Task SendEmail()
        {
            if (!_valid) return;

            MailAddress fromMailAddress = new MailAddress("kharkov.sport.club@gmail.com", "Харьков-Спорт");
            MailAddress toMailAddress = new MailAddress(_addressReciever, _nameReciever);

            using (MailMessage mailMessage = new MailMessage(fromMailAddress, toMailAddress))
            using (SmtpClient smtpClient = new SmtpClient())
            {
                mailMessage.Subject = _subject;
                mailMessage.Body = _body;
                mailMessage.IsBodyHtml = true;

                smtpClient.Host = "smtp.gmail.com";
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(fromMailAddress.Address, "sportclub1=");

                await smtpClient.SendMailAsync(mailMessage);
            }
        }
    }
}
