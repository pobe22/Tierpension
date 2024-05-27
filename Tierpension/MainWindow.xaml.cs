using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Tierpension;

namespace Tierpension
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Application.Current.MainWindow = this;
        }

        private void TiereAbgeben_Click(object sender, RoutedEventArgs e)
        {
            NavigationWindow window = new NavigationWindow();
            window.Source = new Uri("TiereAbgeben.xaml", UriKind.Relative);
            window.Show();
            this.Visibility = Visibility.Hidden;
        }

        private void TiereAbholen_Click(object sender, RoutedEventArgs e)
        {
            NavigationWindow window = new NavigationWindow();
            window.Source = new Uri("TiereAbholen.xaml", UriKind.Relative);
            window.Show();
            this.Visibility = Visibility.Hidden;
        }


    }
}
