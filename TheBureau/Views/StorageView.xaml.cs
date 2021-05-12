using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TheBureau.Views
{
    /// <summary>
    /// Логика взаимодействия для StorageMainView.xaml
    /// </summary>
    public partial class StorageView : UserControl
    {
        public StorageView()
        {
            InitializeComponent();
            // EquipmentDataGrid.ItemsSource = windowView.TheBureauModel.Equipments.ToList();
            // ToolDataGrid.ItemsSource = windowView.TheBureauModel.Tools.ToList();
            //AccessoryDataGrid.ItemsSource = windowView.TheBureauModel.Accessories.ToList();
        }
    }
}
