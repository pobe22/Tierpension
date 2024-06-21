using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tierpension
{
    public class Buchung
    {
        public int Buchungsnummer { get; set; }
        public DateTime Startdatum { get; set; }
        public DateTime Enddatum { get; set; }
        public Kunde Kunde { get; set; }
        public Tier Tier { get; set; }
        public decimal Preis { get; set; }

        public Buchung(int buchungsnummer, DateTime startdatum, DateTime enddatum, Kunde kunde, Tier tier, decimal preis)
        {
            Buchungsnummer = buchungsnummer;
            Startdatum = startdatum;
            Enddatum = enddatum;
            Kunde = kunde;
            Tier = tier;
            Preis = preis;
        }

        public int BerechneTage()
        {
            return (Enddatum - Startdatum).Days;
        }

        public decimal BerechnePreis()
        {
            return Tier.BerechnePreis(BerechneTage());

        }
    }

}
