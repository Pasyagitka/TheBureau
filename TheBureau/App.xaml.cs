using System.Windows;
using TheBureau.Repositories;

namespace TheBureau
{
    public partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            Application.Current.Properties["User"] = null;
            RequestRepository requestRepository = new RequestRepository();
            ToolRepository toolRepository = new ToolRepository();
            RequestEquipmentRepository requestEquipmentRepository = new RequestEquipmentRepository();
            AccessoryRepository accessoryRepository = new AccessoryRepository();
            
            var request = requestRepository.Get(1014);
            var tools = toolRepository.GetByStage(request.stage);
            var accessories = requestEquipmentRepository.GetAccessories(request.RequestEquipments);
            //Notifications.SendRequestAccept(request, tools, accessories);
            //Notifications.SendRequestStatusChanged(request);
        }
    }
}
