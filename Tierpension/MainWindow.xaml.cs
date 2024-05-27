using System;
using System.Windows;
using System.Windows.Navigation;
using Tierpension;

namespace Tierpension
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TiereAbgeben_Click(object sender, RoutedEventArgs e)
        {
            TiereAbgeben tiereAbgeben = new TiereAbgeben(this);
            MainFrame.Navigate(tiereAbgeben);
        }


        private void TiereAbholen_Click(object sender, RoutedEventArgs e)
        {
            TiereAbholen tiereAbholen = new TiereAbholen();
            MainFrame.Navigate(tiereAbholen);
        }


    }
}
