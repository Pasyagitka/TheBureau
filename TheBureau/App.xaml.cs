using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Windows;
using TheBureau.ViewModels;
using TheBureau.Views;

namespace TheBureau
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            Application.Current.Properties["User"] = null;
            //todo email
            // using (MailMessage mm = new MailMessage("БЮРО МОНТАЖНИКА<thebureaunotificationcenter@gmail.com>", "lizavetazinovich@gmail.com"))
            // {
            //     mm.Subject = "Ваша заявка на монтаж принята!";
            //     mm.Body = "<h2>Текущий статус заявки: В обработке</h2><p>Мы уведомим вас о смене статуса.</p>";
            //     mm.IsBodyHtml = true;
            //     using (SmtpClient sc = new SmtpClient("smtp.gmail.com", 587))
            //     {
            //         sc.EnableSsl = true;
            //         sc.DeliveryMethod = SmtpDeliveryMethod.Network;
            //         sc.UseDefaultCredentials = false;
            //         sc.Credentials = new NetworkCredential("thebureaunotificationcenter@gmail.com", "thebureau");
            //         sc.Send(mm);
            //     }
            // }
        }
    }
}
