using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tierpension
{
    public class Kunde
    {
        public string Name { get; set; }
        public string Adresse { get; set; }
        public string Telefonnummer { get; set; }
        public List<Tier> Tiere { get; set; }

        public Kunde(string name, string adresse, string telefonnummer)
        {
            Name = name;
            Adresse = adresse;
            Telefonnummer = telefonnummer;
            Tiere = new List<Tier>();
        }

        public void AddTier(Tier tier)
        {
            Tiere.Add(tier);
        }
    }

}
