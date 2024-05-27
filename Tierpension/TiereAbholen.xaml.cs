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

                    // Konfiguriere den JsonConverter
                    var settings = new JsonSerializerSettings
                    {
                        Converters = { new TierConverter() }
                    };

                    Buchung buchung = JsonConvert.DeserializeObject<Buchung>(json, settings);

                    // Füge die deserialisierte Buchung zur Liste hinzu
                    _buchungen.Add(buchung);

                    // Zeige die Buchung in der ListBox an
                    string kundenName = buchung.Kunde.Name;
                    BuchungenListBox.Items.Add($"Buchungsnummer {buchung.Buchungsnummer} - Kunde: {kundenName}");
                }
            }
        }

        private void JetztAbholen_Click(object sender, RoutedEventArgs e)
        {
            if (BuchungenListBox.SelectedItem is string selectedBuchung)
            {
                if (!string.IsNullOrEmpty(selectedBuchung))
                {
                    // Extrahiere die Buchungsnummer aus dem ausgewählten Eintrag
                    string[] parts = selectedBuchung.Split(' ');
                    if (parts.Length >= 3 && int.TryParse(parts[1], out int buchungsnummer))
                    {
                        // Extrahiere den Kundennamen aus dem ausgewählten Eintrag
                        string kundenName = string.Join(" ", parts[2..]);

                        // Extrahiere den tatsächlichen Kundenname
                        string[] nameParts = kundenName.Split(':');
                        if (nameParts.Length >= 2)
                        {
                            kundenName = nameParts[1].Trim();
                        }

                        // Suche die ausgewählte Buchung anhand der Buchungsnummer und des Kundennamens
                        Buchung gefundenBuchung = _buchungen.FirstOrDefault(b => b.Buchungsnummer == buchungsnummer && b.Kunde.Name == kundenName);
                        if (gefundenBuchung != null)
                        {
                            string filePath = $"Buchung_{gefundenBuchung.Buchungsnummer}.json";
                            File.Delete(filePath);
                            MessageBox.Show($"Buchung {gefundenBuchung.Buchungsnummer} von {gefundenBuchung.Kunde.Name} erfolgreich abgeholt und gelöscht.");
                            BuchungenListBox.Items.Remove(selectedBuchung);
                        }
                        else
                        {
                            MessageBox.Show($"Die Buchung von {kundenName} mit der Buchungsnummer {buchungsnummer} wurde nicht gefunden.");
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
