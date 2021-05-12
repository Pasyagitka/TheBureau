using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TheBureau.Models;
using TheBureau.ViewModels;

namespace TheBureau.Views
{
    public partial class MainWindowView : Window
    {
        public MainWindowView()
        {
            InitializeComponent();
            
        }

        private void MainWindowMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        //
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

        private void SideMenuListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void MainWindowClose_OnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void TopGrid_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
