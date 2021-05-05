using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TheBureau.Models;

namespace TheBureau.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // MainWindowViewModel mainWindowViewModel = new MainWindowViewModel();
            using (var c = new TheBureauModel()) //async?
            {
                //DG.ItemsSource = c.Accessories.ToList();
            }
        }
        // private void MainWindowClose_Click(object sender, RoutedEventArgs e)
        // {
        //     Application.Current.Shutdown();
        // }
        //
        // private void MainWindowMinimize_Click(object sender, RoutedEventArgs e)
        // {
        //     this.WindowState = WindowState.Minimized;
        // }
        //
        // private void MainWindowTop_MouseDown(object sender, MouseButtonEventArgs e)
        // {
        //     if (e.ChangedButton == MouseButton.Left)
        //         this.DragMove();
        // }
        //
        // private void MainWindowResize_Click(object sender, RoutedEventArgs e)
        // {
        //     if (this.WindowState != WindowState.Maximized)
        //     {
        //         this.WindowState = WindowState.Maximized;
        //         MainWindowResizeIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.WindowMaximize;
        //     }
        //     else
        //     {
        //         this.WindowState = WindowState.Normal;
        //         MainWindowResizeIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Resize;
        //     }
        // }
        private void menuList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "ItemRequestMain":
                    {
                        var usc = new RequestMainView();
                        GridMain.Children.Add(usc);
                        break;
                    }
                //case "ItemRequestMain":
                //    {
                //        var requestMain = new RequestMain();
                //        requestMain.ShowDialog();
                //        break;
                //    }
                //case "ItemBrigadesMain":
                //    {
                //        var brigadesMain = new BrigadesMain();
                //        brigadesMain.ShowDialog();
                //        break;
                //    }
                default:
                    break;
            }
        }
        // private void MoveCursorMenu(int index)
        // {
        //     TransitionSlider.OnApplyTemplate();
        //     GridCursor.Margin = new Thickness(0, 40 + (85 * index), 0, 0);
        // }
        //
        // private void outButton_Click(object sender, RoutedEventArgs e)
        // {
        //     throw new System.NotImplementedException();
        // }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            GridMain.Children.Clear();

            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "ItemAdminMain":
                    {
                        var usc = new AdminMainView();
                        GridMain.Children.Add(usc);
                        break;
                    }
                case "ItemRequestMain":
                    {
                        var usc = new RequestMainView();
                        GridMain.Children.Add(usc);
                        break;
                    }
                case "ItemBrigadesMain":
                    {
                        var usc = new BrigadeMainView();
                        GridMain.Children.Add(usc);
                        break;
                    }
                default:
                    break;
            }
        }
        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
        }
    }
}
