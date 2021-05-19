using System.Windows;

namespace TheBureau
{
    public partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {//todo попробовать запоминать юзера
            Application.Current.Properties["User"] = null;
        }
    }
}
