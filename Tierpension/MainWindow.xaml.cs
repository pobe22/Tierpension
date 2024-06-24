using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Tierpension;

namespace Tierpension
{
    public partial class MainWindow : Window
    {
        public static string Name { get; private set; }
        public static string Standort { get; private set; }
        public MainWindow()
        {
            InitializeComponent();

            var inputDialog = new InputDialog("");
            if (inputDialog.ShowDialog() == true)
            {
                Name = inputDialog.EnteredText;
                Standort = inputDialog.SelectedStandort;
                this.Visibility = Visibility.Visible;

                this.Title = $"Willkommen, {Name} - Standort: {Standort}";
            }
            else
            {
                // Beenden Sie die Anwendung, wenn der Benutzer den Dialog schließt
                Application.Current.Shutdown();
            }
        }

        private void TiereAbgeben_Click(object sender, RoutedEventArgs e)
        {
            var tiereAbgebenPage = new TiereAbgeben(Name, Standort);
            NavigationWindow window = new NavigationWindow();
            window.Content = tiereAbgebenPage;
            window.Show();
            this.Visibility = Visibility.Hidden;
        }

        private void TiereAbholen_Click(object sender, RoutedEventArgs e)
        {
            var tiereAbholenPage = new TiereAbholen(Name, Standort);
            NavigationWindow window = new NavigationWindow();
            window.Content = tiereAbholenPage;
            window.Show();
            this.Visibility = Visibility.Hidden;
        }


    }
}
