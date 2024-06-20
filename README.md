# Tierpension Management System

Dieses Projekt ist ein Tierpensions-Management-System, das es Benutzern ermöglicht, Buchungen für die Pflege von Haustieren zu verwalten.

## Inhaltsverzeichnis

- [Projektübersicht](#projektübersicht)
- [Features](#features)
- [Klassenstruktur](#klassenstruktur)
- [Verwendung](#verwendung)
- [Anforderungen](#anforderungen)
- [Installation](#installation)



## Projektübersicht

Das System ermöglicht es Benutzern, sich anzumelden, Tiere abzugeben und abzuholen. Es nutzt eine grafische Benutzeroberfläche (GUI) basierend auf WPF (Windows Presentation Foundation) und unterstützt das Speichern und Laden von Buchungen in JSON-Dateien. Zudem können PDFs mit Buchungsdetails erstellt werden.

## Features

- **Benutzeranmeldung:** Ein Dialog zur Eingabe des Benutzernamens bei Programmstart.
- **Buchungsverwaltung:** Anzeigen und Verwalten von Buchungen basierend auf dem Benutzernamen.
- **PDF-Export:** Erstellen von PDFs mit Buchungsdetails.
- **Polymorphismus:** Jede Tierart hat spezifische Methoden zur Pflege (`care()`) und zum Rufen (`call()`), die durch Polymorphismus implementiert sind.
- **Datenbindung:** Nutzung von Datenbindung in WPF zur Anzeige von Benutzer- und Buchungsinformationen.

## Klassenstruktur

### Basisklasse `Tier`

```csharp
public abstract class Tier
{
    public string Name { get; set; }

    public Tier(string name)
    {
        Name = name;
    }

    public abstract void Care();
    public abstract string Call();

    public override string ToString()
    {
        return Call();
    }
}
```

### Abgeleitete Klassen

#### Hund-Klasse

```csharp
public class Hund : Tier
{
    public Hund(string name) : base(name) { }

    public override void Care()
    {
        Console.WriteLine($"{Name} wird spazieren geführt und gebürstet.");
    }

    public override string Call()
    {
        return $"{Name}, der Hund, kommt gelaufen, wenn man 'Hier' ruft.";
    }
}
```

#### Katze-Klasse

```csharp
public class Katze : Tier
{
    public Katze(string name) : base(name) { }

    public override void Care()
    {
        Console.WriteLine($"{Name} wird gestreichelt und bekommt ihr Katzenklo gereinigt.");
    }

    public override string Call()
    {
        return $"{Name}, die Katze, kommt schnurrend, wenn man mit einer Dose raschelt.";
    }
}
```

## Verwendung

### Anmeldung

Beim Start des Programms wird ein Dialog zur Eingabe des Benutzernamens angezeigt. Der eingegebene Name wird für die Verwaltung der Buchungen verwendet.

### Tiere abgeben

Klicken Sie auf die Schaltfläche "Tiere Abgeben", um zur Seite zum Abgeben von Tieren zu gelangen. Hier können Sie Kundendaten eingeben und eine Buchung für ein Tier erstellen.

### Tiere abholen

Klicken Sie auf die Schaltfläche "Tiere Abholen", um zur Seite zum Abholen von Tieren zu gelangen. Hier werden alle Buchungen des aktuellen Benutzers angezeigt und können verwaltet werden.

### PDF-Export

Klicken Sie auf das PDF-Symbol neben einer Buchung, um ein PDF mit den Buchungsdetails zu erstellen und anzuzeigen.

## Anforderungen

- .NET Framework
- Newtonsoft.Json
- PdfSharp

## Installation

1. Klonen Sie das Repository: `git clone https://github.com/pobe22/tierpension.git`
2. Öffnen Sie das Projekt in Visual Studio.
3. Stellen Sie sicher, dass alle Abhängigkeiten installiert sind.
4. Starten Sie die Anwendung.

