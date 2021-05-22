using System.Threading;
using System.Windows;

namespace TheBureau
{
    // public partial class App : Application
    // {
    //     private void App_OnStartup(object sender, StartupEventArgs e)
    //     {//todo попробовать запоминать юзера
    //         
    //         Application.Current.Properties["User"] = null;
    //     }
    // }
    
    public partial class App : Application
    {
        const string AppId = "MY APP ID FOR THE MUTEX"; //todo настроить*????????
        static Mutex mutex = new Mutex(false, AppId);
        static bool mutexAccessed = false;

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            Application.Current.Properties["User"] = null;
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                if (mutex.WaitOne(0))
                    mutexAccessed = true;
            }
            catch (AbandonedMutexException)
            {
                //handle the rare case of an abandoned mutex
                //in the case of my app this isn't a problem, and I can just continue
                mutexAccessed = true;
            }

            if (mutexAccessed)
                base.OnStartup(e);
            else
            {
                MessageBox.Show("Невозможно открыть более 1 экземпляра приложения");
                Shutdown();
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            if (mutexAccessed) mutex?.ReleaseMutex();
            mutex?.Dispose();
            mutex = null;
            base.OnExit(e);
        }
    }
}
