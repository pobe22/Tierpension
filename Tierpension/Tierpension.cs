using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tierpension;

namespace Tierpension
{
    public class Pension
    {
        public string Name { get; set; }
        public string Adresse { get; set; }
        public List<Buchung> Buchungen { get; set; }

        public Pension(string name, string adresse)
        {
            Name = name;
            Adresse = adresse;
            Buchungen = new List<Buchung>();
        }

        public void AddBuchung(Buchung buchung)
        {
            Buchungen.Add(buchung);
        }
    }
}


