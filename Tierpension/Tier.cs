using System;

namespace Tierpension
{
    public abstract class Tier
    {
        public string Name { get; protected set; }
        public decimal Fixpreis { get; protected set; }
        public decimal Tagespreis { get; protected set; }

        public Tier(string name, decimal fixpreis, decimal tagespreis)
        {
            Name = name;
            Fixpreis = fixpreis;
            Tagespreis = tagespreis;
        }

        public abstract decimal BerechnePreis(int tage);
    }

    public class Hund : Tier
    {
        public Hund(string name, decimal fixpreis, decimal tagespreis) : base(name, fixpreis, tagespreis)
        {
        }

        public override decimal BerechnePreis(int tage)
        {
            return Fixpreis + (Tagespreis * tage);
        }
    }

    public class Katze : Tier
    {
        public Katze(string name, decimal fixpreis, decimal tagespreis) : base(name, fixpreis, tagespreis)
        {
        }

        public override decimal BerechnePreis(int tage)
        {
            return Fixpreis + (Tagespreis * tage);
        }
    }

    public class Wellensittich : Tier
    {
        public Wellensittich(string name, decimal fixpreis, decimal tagespreis) : base(name, fixpreis, tagespreis)
        {
        }

        public override decimal BerechnePreis(int tage)
        {
            return Fixpreis + (Tagespreis * tage);
        }
    }
}
