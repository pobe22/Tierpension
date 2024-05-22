using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tierpension
{
    public class Tier
    {
        public string Name { get; set; }
        public decimal Fixpreis { get; set; }
        public decimal Tagespreis { get; set; }

        public Tier(string name, decimal fixpreis, decimal tagespreis)
        {
            Name = name;
            Fixpreis = fixpreis;
            Tagespreis = tagespreis;
        }

        public decimal BerechnePreis(int tage)
        {
            return Fixpreis + (Tagespreis * tage);
        }
    }

}
