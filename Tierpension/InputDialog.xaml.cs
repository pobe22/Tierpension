using System.Windows;

namespace Tierpension
{
    public partial class InputDialog : Window
    {
        public string EnteredText { get; private set; }

        public InputDialog(string prompt)
        {
            InitializeComponent();
            PromptTextBlock.Text = prompt;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            // Speichern Sie den eingegebenen Benutzernamen
            EnteredText = UsernameTextBox.Text;

            // Schließen Sie das Dialogfeld
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Schließen des Dialogs ohne eine Eingabe zu speichern
            DialogResult = false;
        }
    }
}
