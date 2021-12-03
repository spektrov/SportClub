using SportClub.SportClubDbContext;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using SportClub.Model;
using System.Threading.Tasks;

namespace SportClub.Miscellaneous
{
    class SubscriptionNotificationEmail
    {
        public SportClubContext Context { get; }

        public SubscriptionNotificationEmail(SportClubContext context)
        {
            Context = context;
            Context.Clients.Load();
            Context.Subscriptions.Load();
        }

        public async Task SendAllNotifications(int dayBefore)
        {

            var clientsList = GetClientsToSendEmail(dayBefore);
            foreach (Client client in clientsList)
            {
                var expiredDate = client.Subscriptions.FirstOrDefault(s => s.ClientId == client.ClientId).ValidityDate;
                var gender = client.Gender == Genders.Мужской ? "Уважаемый" : "Уважаемая";

                var emailSender = new EmailSender(client.Email, client.FirstName + " " + client.LastName, "Продлите сотрудничество с Харьков-Спорт",
                    $"<div><p style=\"font-size: 14pt;\">" +
                    $"{gender} <b>{client.FirstName + " " + client.LastName}!</b> </p>" +
                    $"<p style=\"font-size: 14pt;\">Срок действия Вашего абонемента истекает <b>{expiredDate.Day}.{expiredDate.Month}.{expiredDate.Year}</b></p>" +
                    $"<p style=\"font-size: 14pt;\">Продлите абонемент, чтобы продолжить расти вместе с нами.</p>" +
                    $"<p>С уважением, администрация <i>Харьков-Спорт</i>.</p></div>");

                await emailSender.SendEmail();

                Context.Subscriptions.FirstOrDefault(cl => cl.Client.ClientId == client.ClientId).IsNotified = true;
                Context.SaveChanges();
            }

        }

        private List<Client> GetClientsToSendEmail(int daysBefore)
        {
            var query1 = $"SELECT * FROM Clients " +
                $"WHERE ClientId IN " +
                $"(SELECT ClientId FROM Subscriptions " +
                $"WHERE ValidityDate < DATEADD(DAY, {daysBefore}, GETDATE()) " +
                $"AND IsNotified = 0) " +
                $"AND Email IS NOT NULL; ";

            var subscriptions = Context.Clients.SqlQuery(query1).ToListAsync();

           return subscriptions.Result;
        }
    }
}
