using System;
using System.Collections.Generic;

namespace Tierpension
{
    public abstract class Tier
    {
        public string Tierart { get; protected set; }
        public string Tiername { get; set; }
        public decimal Fixpreis { get; protected set; }
        public decimal Tagespreis { get; protected set; }
        public List<string> Essen { get; set; }

        public Tier(string tierart, string tiername, decimal fixpreis, decimal tagespreis, List<string> essen)
        {
            Tierart = tierart;
            Tiername = tiername;
            Fixpreis = fixpreis;
            Tagespreis = tagespreis;
            Essen = essen;

        }
        public abstract void Care();
        public abstract string Call();

        public string GetCareDescription()
        {
            Care(); 
            return GenerateCareDescription(); 
        }
        protected abstract string GenerateCareDescription();
        public abstract decimal BerechnePreis(int tage);
    }

    public class Hund : Tier
    {
        public Hund(string tierart, string tiername, decimal fixpreis, decimal tagespreis, List<string> essen) : base(tierart, tiername, fixpreis, tagespreis, essen)
        {
        }
        public override void Care()
        {
            Console.WriteLine($"{Tiername} wird spazieren geführt und gebürstet.");
        }

        public override string Call()
        {
            return $"{Tiername}, der Hund, kommt gelaufen, wenn man 'Hier' ruft.";
        }

        protected override string GenerateCareDescription()
        {
            return $"{Tiername} wird spazieren geführt und gebürstet.";
        }

        public override decimal BerechnePreis(int tage)
        {
            return Fixpreis + (Tagespreis * tage);
        }
    }

    public class Katze : Tier
    {
        public Katze(string tierart, string tiername, decimal fixpreis, decimal tagespreis, List<string> essen) : base(tierart, tiername, fixpreis, tagespreis, essen)
        {
        }
        public override void Care()
        {
            Console.WriteLine($"{Tiername} wird gestreichelt und bekommt ihr Katzenklo gereinigt.");
        }

        public override string Call()
        {
            return $"{Tiername}, die Katze, kommt schnurrend, wenn man mit einer Dose raschelt.";
        }
        protected override string GenerateCareDescription()
        {
            return $"{Tiername} wird gestreichelt und bekommt ihr Katzenklo gereinigt.";
        }

        public override decimal BerechnePreis(int tage)
        {
            return Fixpreis + (Tagespreis * tage);
        }
    }

    public class Wellensittich : Tier
    {
        public Wellensittich(string tierart, string tiername, decimal fixpreis, decimal tagespreis, List<string> essen) : base(tierart, tiername, fixpreis, tagespreis, essen)
        {
        }
        public override void Care()
        {
            Console.WriteLine($"{Tiername} wird gepflegt und gefüttert.");
        }
        public override string Call()
        {
            return $"{Tiername}, der Wellensittich, zwitschert fröhlich, wenn man pfeift.";
        }
        protected override string GenerateCareDescription()
        {
            return $"{Tiername} wird gepflegt und gefüttert.";
        }
        public override decimal BerechnePreis(int tage)
        {
            return Fixpreis + (Tagespreis * tage);
        }
    }

    public class Kaninchen : Tier
    {
        public Kaninchen(string tierart, string tiername, decimal fixpreis, decimal tagespreis, List<string> essen) : base(tierart, tiername, fixpreis, tagespreis, essen)
        {
        }
        public override void Care()
        {
            Console.WriteLine($"{Tiername} bekommt frisches Heu und das Gehege wird gereinigt.");
        }

        public override string Call()
        {
            return $"{Tiername}, das Kaninchen, hoppelt herbei, wenn man es ruft.";
        }
        protected override string GenerateCareDescription()
        {
            return $"{Tiername} bekommt frisches Heu und das Gehege wird gereinigt.";
        }
        public override decimal BerechnePreis(int tage)
        {
            return Fixpreis + (Tagespreis * tage);
        }
    }

    public class Meerschweinchen : Tier
    {
        public Meerschweinchen(string tierart, string tiername, decimal fixpreis, decimal tagespreis, List<string> essen) : base(tierart, tiername, fixpreis, tagespreis, essen)
        {
        }
        public override void Care()
        {
            Console.WriteLine($"{Tiername} bekommt frisches Gemüse und das Gehege wird gereinigt.");
        }

        public override string Call()
        {
            return $"{Tiername}, das Meerschweinchen, pfeift vor Freude, wenn es frisches Futter bekommt.";
        }
        protected override string GenerateCareDescription()
        {
            return $"{Tiername} bekommt frisches Gemüse und das Gehege wird gereinigt.";
        }
        public override decimal BerechnePreis(int tage)
        {
            return Fixpreis + (Tagespreis * tage);
        }
    }
}
