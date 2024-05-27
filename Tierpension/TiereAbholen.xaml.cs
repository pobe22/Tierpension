using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Tierpension
{
    /// <summary>
    /// Interaction logic for TiereAbholen.xaml
    /// </summary>
    public partial class TiereAbholen : Page
    {
        private List<Buchung> _buchungen = new List<Buchung>();

        public TiereAbholen()
        {
            InitializeComponent();
            LadeBuchungen();
        }

        private void LadeBuchungen()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory());
            FileInfo[] jsonFiles = directoryInfo.GetFiles("Buchung_*.json");
            foreach (FileInfo file in jsonFiles)
            {
                using (StreamReader reader = new StreamReader(file.FullName))
                {
                    string json = reader.ReadToEnd();
                    Buchung buchung = JsonConvert.DeserializeObject<Buchung>(json);
                    _buchungen.Add(buchung);
                    BuchungenListBox.Items.Add($"Buchungsnummer {buchung.Buchungsnummer}");
                }
            }
        }


        private void JetztAbholen_Click(object sender, RoutedEventArgs e)
        {
            if (BuchungenListBox.SelectedItem is string selectedBuchungsnummer)
            {
                if (!string.IsNullOrEmpty(selectedBuchungsnummer))
                {
                    string[] parts = selectedBuchungsnummer.Split(' ');
                    if (parts.Length == 2 && int.TryParse(parts[1], out int buchungsnummer))
                    {
                        Buchung selectedBuchung = _buchungen.FirstOrDefault(b => b.Buchungsnummer == buchungsnummer);
                        if (selectedBuchung != null)
                        {
                            string filePath = $"Buchung_{selectedBuchung.Buchungsnummer}.json";
                            File.Delete(filePath);
                            MessageBox.Show($"Buchung {selectedBuchung.Buchungsnummer} erfolgreich abgeholt und gelöscht.");
                            BuchungenListBox.Items.Remove(selectedBuchungsnummer);
                        }
                        else
                        {
                            MessageBox.Show($"Die Buchung mit der Buchungsnummer {buchungsnummer} wurde nicht gefunden.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ungültiges Format für die ausgewählte Buchungsnummer.");
                    }
                }
                else
                {
                    MessageBox.Show("Die ausgewählte Buchungsnummer ist leer.");
                }
            }
            else
            {
                MessageBox.Show("Bitte wählen Sie eine Buchung aus, um sie abzuholen.");
            }
        }
        private void ZurueckZumHome_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.Visibility = Visibility.Visible;
            Window win = (Window)this.Parent;
            win.Close();
        }

    }
}
