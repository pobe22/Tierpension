using System;
using System.Collections.Generic;

namespace Tierpension
{
    public abstract class Tier
    {
        public string Name { get; protected set; }
        public decimal Fixpreis { get; protected set; }
        public decimal Tagespreis { get; protected set; }
        public List<string> Essen { get; set; }

        public Tier(string name, decimal fixpreis, decimal tagespreis, List<string> essen)
        {
            Name = name;
            Fixpreis = fixpreis;
            Tagespreis = tagespreis;
            Essen = essen;
        }

        public abstract decimal BerechnePreis(int tage);
    }

    public class Hund : Tier
    {
        public Hund(string name, decimal fixpreis, decimal tagespreis, List<string> essen) : base(name, fixpreis, tagespreis, essen)
        {
        }

        public override decimal BerechnePreis(int tage)
        {
            return Fixpreis + (Tagespreis * tage);
        }
    }

    public class Katze : Tier
    {
        public Katze(string name, decimal fixpreis, decimal tagespreis, List<string> essen) : base(name, fixpreis, tagespreis, essen)
        {
        }

        public override decimal BerechnePreis(int tage)
        {
            return Fixpreis + (Tagespreis * tage);
        }
    }

    public class Wellensittich : Tier
    {
        public Wellensittich(string name, decimal fixpreis, decimal tagespreis, List<string> essen) : base(name, fixpreis, tagespreis, essen)
        {
        }

        public override decimal BerechnePreis(int tage)
        {
            return Fixpreis + (Tagespreis * tage);
        }
    }
}
