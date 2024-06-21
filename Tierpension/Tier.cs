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
        public abstract void Care();
        public abstract string Call();

        public override string ToString()
        {
            return Call();
        }

        public abstract decimal BerechnePreis(int tage);
    }

    public class Hund : Tier
    {
        public Hund(string name, decimal fixpreis, decimal tagespreis, List<string> essen) : base(name, fixpreis, tagespreis, essen)
        {
        }

        public override void Care()
        {
            Console.WriteLine($"{Name} wird spazieren geführt und gebürstet.");
        }

        public override string Call()
        {
            return $"{Name}, der Hund, kommt gelaufen, wenn man 'Hier' ruft.";
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
        public override void Care()
        {
            Console.WriteLine($"{Name} wird gestreichelt und bekommt ihr Katzenklo gereinigt.");
        }

        public override string Call()
        {
            return $"{Name}, die Katze, kommt schnurrend, wenn man mit einer Dose raschelt.";
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
        public override void Care()
        {
            Console.WriteLine($"{Name} wird gepflegt und geb?rstet.");
        }
        public override string Call()
        {
            return $"{Name}, der Wellensittich, kommt gelaufen, wenn man 'Hier' ruft.";
        }

        public override decimal BerechnePreis(int tage)
        {
            return Fixpreis + (Tagespreis * tage);
        }
    }
}
