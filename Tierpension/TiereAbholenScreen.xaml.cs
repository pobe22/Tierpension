using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using Newtonsoft.Json;

namespace Tierpension
{
    public partial class TiereAbholenScreen : Window
    {
        private List<Buchung> _buchungen = new List<Buchung>();

        public TiereAbholenScreen()
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
                    // Extrahiere die Buchungsnummer aus dem ausgewählten Text
                    string[] parts = selectedBuchungsnummer.Split(' ');
                    if (parts.Length == 2 && int.TryParse(parts[1], out int buchungsnummer))
                    {
                        // Finde die Buchung mit der entsprechenden Buchungsnummer und entferne sie
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

    }
}
