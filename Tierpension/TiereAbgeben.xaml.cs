﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;

namespace Tierpension
{
    public partial class TiereAbgeben : Page
    {
        private Dictionary<string, Tier> _tiere;
        private Pension _tierpension;
        private Buchung _aktuelleBuchung;

        public string BenutzerName { get; private set; }
        public string Standort { get; private set; }

        public TiereAbgeben(string benutzerName, string standort)
        {
            InitializeComponent();
            _tierpension = new Pension("Meine Tierpension", "Musterstraße 1");
            BenutzerName = benutzerName;
            Standort = standort;
            DataContext = this;
            InitialisiereTiere("Unbekannt");
            InitialisiereComboBox();
        }

        private void InitialisiereTiere(string tiername)
        {
            _tiere = new Dictionary<string, Tier>
            {
                { "Hund", new Hund("Hund", tiername, 20.0m, 10.0m, new List<string> { "Fleisch", "Knochen" }) },
                { "Katze", new Katze("Katze", tiername, 15.0m, 8.0m, new List<string> { "Fisch", "Milch" }) },
                { "Wellensittich", new Wellensittich("Wellensittich", tiername, 10.0m, 5.0m, new List<string> { "Samen", "Obst" }) },
                { "Kaninchen", new Kaninchen("Kaninchen", tiername, 12.0m, 6.0m, new List<string> { "Heu", "Gemüse" }) },
                { "Meerschweinchen", new Meerschweinchen("Meerschweinchen", tiername, 14.0m, 7.0m, new List<string> { "Gemüse", "Früchte" }) }
            };
        }

        private void InitialisiereComboBox()
        {
            if (_tiere != null)
            {
                TierComboBox.ItemsSource = _tiere.Keys;
            }
            else
            {
                MessageBox.Show("Die Tiere wurden noch nicht initialisiert.");
            }
        }

        private void TierComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TierComboBox.SelectedItem is string selectedTier && _tiere.ContainsKey(selectedTier))
            {
                Tier tier = _tiere[selectedTier];
                EssenComboBox.ItemsSource = tier.Essen;
            }
        }

        private void BerechnePreis_Click(object sender, RoutedEventArgs e)
        {
            if (TierComboBox.SelectedItem is string selectedTier &&
                TageSlider.Value >= 0 &&
                !string.IsNullOrEmpty(AdresseTextBox.Text) &&
                !string.IsNullOrEmpty(TelefonnummerTextBox.Text))
            {
                if (_tiere.ContainsKey(selectedTier))
                {
                    Tier tier = _tiere[selectedTier];
                    Kunde kunde = new Kunde(BenutzerName, AdresseTextBox.Text, Standort,  TelefonnummerTextBox.Text);
                    int tage = (int)TageSlider.Value;

                    decimal preis = Math.Round(tier.BerechnePreis(tage), 2);

                    int neueBuchungsnummer = FindeNeueBuchungsnummer();

                    _aktuelleBuchung = new Buchung(neueBuchungsnummer, DateTime.Now, DateTime.Now.AddDays(tage), kunde, tier, preis);

                    ErgebnisTextBlock.Text = $"Der Preis für {tage} Tage {selectedTier} beträgt {preis:C}.";
                    BuchungAbschliessenButton.Visibility = Visibility.Visible;
                }
                else
                {
                    ErgebnisTextBlock.Text = "Tier nicht gefunden.";
                }
            }
            else
            {
                ErgebnisTextBlock.Text = "Bitte wählen Sie ein Tier und geben Sie alle erforderlichen Daten ein.";
            }
        }

        private int FindeNeueBuchungsnummer()
        {
            int maxBuchungsnummer = 0;
            DirectoryInfo directoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory());
            FileInfo[] jsonFiles = directoryInfo.GetFiles("Buchung_*.json");
            foreach (FileInfo file in jsonFiles)
            {
                string fileName = Path.GetFileNameWithoutExtension(file.Name);
                if (int.TryParse(fileName.Substring(8), out int num))
                {
                    maxBuchungsnummer = Math.Max(maxBuchungsnummer, num);
                }
            }
            return maxBuchungsnummer + 1;
        }

        private void BuchungAbschliessen_Click(object sender, RoutedEventArgs e)
        {
            if (_aktuelleBuchung != null)
            {
                string tiername = TiernameTextBox.Text;
                InitialisiereTiere(tiername);

                // Update the selected animal with the new name
                if (TierComboBox.SelectedItem is string selectedTier && _tiere.ContainsKey(selectedTier))
                {
                    Tier selectedAnimal = _tiere[selectedTier];
                    selectedAnimal.Tiername = tiername;
                    _aktuelleBuchung.Tier = selectedAnimal;
                }

                string json = JsonConvert.SerializeObject(_aktuelleBuchung, Newtonsoft.Json.Formatting.Indented);
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Buchung_{_aktuelleBuchung.Buchungsnummer}.json");
                File.WriteAllText(filePath, json);
                MessageBox.Show($"Buchung gespeichert unter {filePath}");
                BuchungAbschliessenButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show("Es wurde keine Buchung erstellt.");
            }
        }


        private void TageSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (TageAnzeige != null)
            {
                TageAnzeige.Text = $"Anzahl der Tage: {TageSlider.Value}";
            }
        }

        private void ZurueckZumHome_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.Visibility = Visibility.Visible;
            Window win = (Window)this.Parent;
            win.Close();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox && textBox.Text == textBox.Name.Replace("TextBox", ""))
            {
                textBox.Text = "";
                textBox.Foreground = System.Windows.Media.Brushes.Black;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox && string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = textBox.Name.Replace("TextBox", "");
                textBox.Foreground = System.Windows.Media.Brushes.Gray;
            }
        }
    }
}
