using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using TheBureau.Enums;
using TheBureau.Models;
using TheBureau.Repositories;
using TheBureau.Views.Controls;

namespace TheBureau.Services
{
    public static class Notifications
    {
        private static readonly string CompanyName = "БЮРО МОНТАЖНИКА";
        private static readonly int PORT = 587;
        private static readonly string HOST = "smtp.gmail.com";

        private static readonly string RequestAcceptSubject = "Ваша заявка на монтаж принята!";
        private static readonly string RequestStatusChangedSubject = "Изменен статус вашей заявки!";

        #region Body
        private static readonly string AcceptBodyHeader = "<h2>Текущий статус заявки: В обработке</h2><p>Мы уведомим вас о смене статуса заявки.</p>";
        private static string Client = "<p style=\"text-align: left;\"><strong>Заказчик</strong>: {0} {1} {2}, {3}, +{4}</p>";
        private static string Address = "<p style=\"text-align: left;\"><strong>Адрес</strong>: г.{0}, ул. {1}, д. {2}, к. {3}, кв. {4}.</p>";
        private static string MountingDate = "<p style=\"text-align: left;\"><span><strong>Дата выполнения работ</strong>: {0}.</span></p>";
        private static string Stages = "<p style=\"text-align: left;\"><span><strong>Стадия отделки</strong>: {0}.</span></p>";
        private static string Equipment = "<p style=\"text-align: left;\"><span></span><span><strong>Оборудование: (наименование, количество)</strong>:<br/></span></p>";
        private static string Tools = "<p style=\"text-align: left;\"><span></span><span><strong>Инструменты</strong>:<br/></span></p>";
        private static string Accessory = "<p style=\"text-align: left;\"><span></span><span><strong>Комплектующие (артикул, наименование, цена за комплект)</strong>:<br/></span></p>";
        private static string StatusChangedMessage = "Текущий статус вашей заявки: {0}";
        private static string TotalAccessoriesPrice = "<p style=\"text-align: left;\"><span></span><span><strong>Итого за комплектующие</strong>: {0}<br/></span></p>";
        #endregion
        
        public static async void SendRequestAccept(Request request, IEnumerable<Tool> tools, IEnumerable<Accessory> accessories)
        {
            AccessoryRepository accessoryRepository = new AccessoryRepository();
            var equipment = request.RequestEquipments;

            string clientString = String.Format(Client, request.Client.surname, request.Client.firstname, request.Client.patronymic, request.Client.email, request.Client.contactNumber.ToString());
            string addressString = String.Format(Address, request.Address.city, request.Address.street,request.Address.house.ToString(), request.Address.corpus, request.Address.flat.ToString());
            string mountingDateString = String.Format(MountingDate, request.mountingDate.ToString("MM/dd/yyyy"));
            
            string stageToString ="";
            if (request.stage == (int) Enums.Stages.rough) stageToString = "Черновая";
                else if (request.stage == (int)Enums.Stages.clean) stageToString = "Чистовая";
                else stageToString = "Черновая и чистовая";
            string stagesString = String.Format(Stages, stageToString);
            
            string equipmentTable = GetTable(equipment, x=>x.Equipment.type + " (" + x.Equipment.mounting + ") ", x=>x.quantity); 
            string toolTable = GetTable(tools, x => x.name);
            string accessoryTable = GetTable(accessories, x => x.art, x=>x.name, x=>x.price.ToString());
            string accessoryPrice = String.Format(TotalAccessoriesPrice, accessoryRepository.TotalPrice(accessories));
            
            string body = String.Format(AcceptBodyHeader + clientString + addressString + mountingDateString +
                                        stagesString
                                        + Equipment + equipmentTable + Tools + toolTable
                                        + Accessory + accessoryTable + accessoryPrice);
            await SendEmail(request.Client.email, RequestAcceptSubject, body);
        }
        
        public static async void SendRequestStatusChanged(Request request)
        {
            string requestStatus = "";
            if (request.status == (int) Statuses.InProcessing) requestStatus = "В обработке";  
            else if (request.status == (int)Statuses.InProgress) requestStatus = "В процессе";
            else requestStatus = "Готово";
            
            string statusString = String.Format(StatusChangedMessage, requestStatus);
            string clientString = String.Format(Client, request.Client.surname, request.Client.firstname, request.Client.patronymic, request.Client.email, request.Client.contactNumber.ToString());

            await SendEmail(request.Client.email, RequestStatusChangedSubject, statusString + clientString);
        }
        
        public static async Task SendEmail(string clientEmail, string subject, string body)
        {
            String result = "";
            try
            {
                CompanyRepository companyRepository = new();
                var credentials = companyRepository.Get();
                using (MailMessage letter = new MailMessage(CompanyName + "<" + credentials.email + ">", clientEmail))
                {
                    letter.Subject = subject;
                    letter.Body = body;
                    letter.IsBodyHtml = true;
                    using (SmtpClient sc = new SmtpClient(HOST, PORT))
                    {
                        sc.EnableSsl = true;
                        sc.DeliveryMethod = SmtpDeliveryMethod.Network;
                        sc.UseDefaultCredentials = false;
                        sc.Credentials = new NetworkCredential(credentials.email, credentials.password);
                        await sc.SendMailAsync(letter);
                        result = "Письмо успешно отправлено клиенту.";
                        InfoWindow infoWindow = new InfoWindow("Успех!", result);
                        infoWindow.ShowDialog();
                    }
                }
            }
            catch (Exception e)
            {
                result = String.Format("Ошибка отправки письма: {0}", e.Message);
                InfoWindow infoWindow = new InfoWindow("Ошибка!", result);
                infoWindow.ShowDialog();
            }
        }

        private static string GetTable<T>(IEnumerable<T> list, params Func<T, object>[] columns)
        {
            var sb = new StringBuilder();
            sb.Append("<table>");
            foreach (var item in list)
            {
                sb.Append("<tr>");
                foreach (var column in columns)
                    sb.Append("<td>" + column(item) + "<td>");
                sb.Append("</tr>");
            }
            sb.Append("</table>");
            return sb.ToString();
        }
    }
}