using System;
using System.Windows;
using System.Windows.Controls;

namespace Tierpension
{
    public partial class StartScreen : Page
    {
        // Ereignis, das ausgelöst wird, wenn der Benutzer auf die Schaltfläche "OK" klickt
        public event EventHandler<string> OKButtonClicked;

        public StartScreen()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            // Überprüfen Sie, ob der Benutzer einen Benutzernamen eingegeben hat
            if (!string.IsNullOrEmpty(UsernameTextBox.Text))
            {
                // Senden Sie den Benutzernamen über das Ereignis
                OKButtonClicked?.Invoke(this, UsernameTextBox.Text);

            }
            else
            {
                MessageBox.Show("Bitte geben Sie Ihren Namen ein.");
            }
        }
    }
}
