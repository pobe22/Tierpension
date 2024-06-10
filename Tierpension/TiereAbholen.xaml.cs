using Newtonsoft.Json;
using PdfSharp.Drawing.Layout;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
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
        private string _Name;

        public TiereAbholen(string Name)
        {
            InitializeComponent();
            _Name = Name;
            LadeBuchungen();
            DataContext = this;
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

                    var settings = new JsonSerializerSettings
                    {
                        Converters = { new TierConverter() }
                    };

                    Buchung buchung = JsonConvert.DeserializeObject<Buchung>(json, settings);

                    // Füge nur Buchungen des aktuellen Benutzers hinzu
                    if (buchung.Kunde.Name.Equals(_Name, StringComparison.OrdinalIgnoreCase))
                    {
                        _buchungen.Add(buchung);
                    }
                }
            }

            BuchungenListBox.ItemsSource = _buchungen;
        }

        private string ErstellePDF(Buchung buchung)
        {
            if (buchung != null)
            {
                try
                {
                    string pdfPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Buchung_{buchung.Buchungsnummer}.pdf");
                    PdfDocument document = new PdfDocument();
                    PdfPage page = document.AddPage();
                    XGraphics gfx = XGraphics.FromPdfPage(page);

                    XFont font = new XFont("Verdana", 12);
                    gfx.DrawString("Ihr fett-kursiver Text", font, XBrushes.Black, new XPoint(10, 10), XStringFormats.TopLeft);

                    XTextFormatter tf = new XTextFormatter(gfx);
                    XRect rect = new XRect(40, 40, page.Width - 80, page.Height - 80);
                    tf.DrawString($"Buchungsnummer: {buchung.Buchungsnummer}\nKunde: {buchung.Kunde.Name}\nAdresse: {buchung.Kunde.Adresse}\nTelefonnummer: {buchung.Kunde.Telefonnummer}\nTiername: {buchung.Tier.Name}\nFixpreis: {buchung.Tier.Fixpreis}\nTagespreis: {buchung.Tier.Tagespreis}\nEssen: {string.Join(", ", buchung.Tier.Essen)}", font, XBrushes.Black, rect, XStringFormats.TopLeft);

                    document.Save(pdfPath);
                    document.Close();

                    return pdfPath;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Fehler beim Erstellen des PDFs: {ex.Message}");
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Ungültige Buchung.");
                return null;
            }
        }

        private void JetztAbholen_Click(object sender, RoutedEventArgs e)
        {
            if (BuchungenListBox.SelectedItem is Buchung selectedBuchung)
            {
                Buchung gefundenBuchung = _buchungen.FirstOrDefault(b => b.Buchungsnummer == selectedBuchung.Buchungsnummer && b.Kunde.Name == selectedBuchung.Kunde.Name);
                if (gefundenBuchung != null)
                {
                    string filePath = $"Buchung_{gefundenBuchung.Buchungsnummer}.json";
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                        MessageBox.Show($"Buchung {gefundenBuchung.Buchungsnummer} von {gefundenBuchung.Kunde.Name} erfolgreich abgeholt und gelöscht.");
                        _buchungen.Remove(gefundenBuchung);
                        BuchungenListBox.ItemsSource = null;
                        BuchungenListBox.ItemsSource = _buchungen;
                    }
                    else
                    {
                        MessageBox.Show($"Die Datei für die Buchung {gefundenBuchung.Buchungsnummer} wurde nicht gefunden.");
                    }
                }
                else
                {
                    MessageBox.Show($"Die Buchung von {selectedBuchung.Kunde.Name} mit der Buchungsnummer {selectedBuchung.Buchungsnummer} wurde nicht gefunden.");
                }
            }
            else
            {
                MessageBox.Show("Bitte wählen Sie eine Buchung aus, um sie abzuholen.");
            }
        }



        private void PdfIcon_Click(object sender, MouseButtonEventArgs e)
        {
            var selectedBuchung = (sender as FrameworkElement)?.DataContext as Buchung;

            if (selectedBuchung != null)
            {
                string pdfPath = ErstellePDF(selectedBuchung);

                if (!string.IsNullOrEmpty(pdfPath))
                {
                    try
                    {
                        var processStartInfo = new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = pdfPath,
                            UseShellExecute = true
                        };
                        System.Diagnostics.Process.Start(processStartInfo);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Fehler beim Öffnen der PDF-Datei: {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show("PDF-Datei nicht gefunden.");
                }
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
