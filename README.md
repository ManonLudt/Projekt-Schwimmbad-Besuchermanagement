Projekt-Schwimmbad Besuchermanagement
=====

## 1. Ziel des Projekts
Mein Name ist Manon Ludt und das Hauptziel meines Projekts ist die Entwicklung und Implementierung eines umfassenden Besuchermanagementsystems, das den Verkauf von Eintrittskarten, die Erfassung von Besucherzahlen und die Überwachung der Auslastung des Schwimmbads ermöglicht. Dies soll die Effizienz und den Komfort für Besucher und Betreiber gleichermaßen verbessern.


## 2. Funktionen des Systems
#### Eintrittskartenverkauf:
* Online-Verkauf: Besucher können Eintrittskarten über eine benutzerfreundliche Website oder mobile App kaufen. Dies reduziert Warteschlangen und ermöglicht eine bessere Planung.
* Vor-Ort-Verkauf: An Kassenautomaten oder Schaltern im Schwimmbad können Besucher ebenfalls Eintrittskarten erwerben. Diese Automaten sind mit dem zentralen System verbunden, um Echtzeit-Daten zu gewährleisten.
* Reservierungen: Über eine benutzerfreundliche Website oder mobile App wird es auch ermöglicht Reservierungen vorzunehmen.
* Rabatte: Zudem erkennt das System durch den Status des Kunden möglich Rabatte, wie Kinder, Schüler oder Studenten.
#### Erfassung der Besucherzahlen:
* Eingangskontrolle: Drehkreuze oder Scanner erfassen die Eintrittskarten der Besucher beim Betreten des Schwimmbads. Diese Daten werden in Echtzeit an das zentrale System übermittelt.
Zählung der Besucher: Sensoren und Kameras können die Anzahl der Personen im Schwimmbadbereich überwachen und so eine genaue Erfassung der aktuellen Besucherzahl ermöglichen.
* Überwachung der Auslastung:
Echtzeit-Daten: Das System zeigt in Echtzeit die aktuelle Auslastung des Schwimmbads an. Dies hilft dem Management, Überfüllungen zu vermeiden und die Besucherströme besser zu steuern.
Berichte und Analysen: Das System generiert regelmäßige Berichte über Besucherzahlen und Auslastung, die für die Planung und Optimierung des Betriebs genutzt werden können.

   
## 3. Technische Umsetzung
* Software-Architektur:
Backend: Ein robustes Backend-System, das die Datenbankverwaltung, Benutzerverwaltung und die Integration mit verschiedenen Hardware-Komponenten (z.B. Drehkreuze, Scanner) übernimmt.
Frontend: Eine intuitive Benutzeroberfläche für die Website und mobile App, die den Kauf von Eintrittskarten und die Anzeige von Echtzeit-Informationen ermöglicht.
* Hardware-Komponenten:
Drehkreuze und Scanner: Diese Geräte sind mit dem zentralen System verbunden und ermöglichen die Erfassung der Eintrittskarten und Besucherzahlen.
Sensoren und Kameras: Diese Geräte überwachen die Besucherzahlen und übermitteln die Daten an das zentrale System.

  
## 4. Vorteile des Systems
* Effizienzsteigerung: Reduzierung von Warteschlangen und Verbesserung des Besuchererlebnisses durch schnellen und einfachen Zugang.
* Bessere Planung: Echtzeit-Daten und Berichte ermöglichen eine bessere Planung und Optimierung des Schwimmbadbetriebs.
* Sicherheit: Überwachung der Besucherzahlen hilft, Überfüllungen zu vermeiden und die Sicherheit der Besucher zu gewährleisten.

# Anwendung des Programms

## Login-Vorgang
Der Benutzer kann sich durch Eingabe seines Benutzernamens und Passworts in das System einloggen.
Sollte der Benutzer noch kein Konto besitzen, kann er über das Menüfeld _Registrieren_ ein neues Benutzerprofil anlegen.
Unter dem Menüfeld _Passwort vergessen?_ kann er ein neues Passwort vergesben.

## Ticketverwaltung
Befindet der Benutzer sich im Dashboard, so kann er über den Menüpunkt _Ticket_ Tickets verwalten.

**Anlegen:** Hier kann der Benutzer ein neues Ticket anlegen, indem er eine neue TicketID, eine Bezeichnung, einen Preis und die verfügbare Anzahl angibt.
             Jedes Feld muss dabei ausgefüllt sein, um das Erstellen zu gewährleisten.
             Zudem müssen in den Feldern TicketID, Preis und Anzahl ein INT-Wert eingegeben werden.
             Nach dem anlegen, wird das Ticket im Dashboard ausgegeben.

**Ändern:** Hier kann er Benutzer ein bereits existierendes Ticket anpassen, indem er die entsprechende TicketID angibt.
            Er kann eine neue Bezeichnung, Preis oder Anzahl vergeben.
            Abgesehen von der TicketID, müssen nur die Felder ausgefüll werden, die angepasst werden sollen.
            Zudem müssen in den Feldern TicketID, Preis und Anzahl ein INT-Wert weiterhin eingegeben werden, um eine Änderung durchzuführen.
            Nach dem ändern, werden die Ticketinformation im Dashboard ausgegeben.

**Löschen:** Bei diesem Punkt kann der Benutzer ein bereits existierendes Ticket löschen, indem er die entsprechende TicketID angibt.
             Es muss nur beachtet werden, das kein String-Wert in der TicketID eingegeben wird.
             Nach der Löschung wird das Ticket nicht mehr im Dashboard ausgegeben.

![image alt](https://github.com/ManonLudt/Projekt-Schwimmbad-Besuchermanagement/blob/9aa46e36012018e8302d4c2f3b5ba2e9331b39da/Screenshots/Screenshot%202025-02-25%20151924.png)
![image alt](https://github.com/ManonLudt/Projekt-Schwimmbad-Besuchermanagement/blob/363038ec3d191d84a536501d8a9603e7a739a06e/Screenshots/Screenshot%202025-02-25%20151938.png)

## Kundenverwaltung
Befindet der Benutzer sich im Dashboard, so kann er über den Menüpunkt _Kunden_ Kunden verwalten.

**Anlegen:** Hier kann der Benutzer einen neuen Kunden anlegen, indem er eine neue KundenID, den Vor- und Nachnamen, das Alter und den Status über eine Checkbox
             auswählt.
             Dabei ist der Status _keinen_ als Standart ausgewählt, welcher nicht im Dashboard angezeigt wird.
             Jedes Feld muss dabei ausgefüllt sein, um das Erstellen zu gewährleisten.
             Zudem müssen in den Feldern KundenID und Alter INT-Wert eingegeben werden.
             Nach dem anlegen, wird der Kunde im Dashboard ausgegeben.

**Ändern:** Hier kann er Benutzer einen bereits existierenden Kunden anpassen, indem er die entsprechende KundenID angibt.
            Er kann einen Vor und Nachnamen, das Alter oder den Status vergeben.
            Abgesehen von der KundenID, müssen nur die Felder ausgefüllt werden, die angepasst werden sollen.
            Wird kein Status ausgewählt, wird die Standartauswahlr _keiner_ ausgewählt.
            Zudem müssen in den Feldern KundenID und Alter ein INT-Wert weiterhin eingegeben werden, um eine Änderung durchzuführen.
            Nach dem ändern, werden die Kundeninformation im Dashboard ausgegeben.

**Löschen:** Bei diesem Punkt kann der Benutzer einen bereits existierenden Kunden löschen, indem er die entsprechende KundenID angibt.
             Es muss nur beachtet werden, das kein String-Wert in der KundenID eingegeben wird.
             Nach der Löschung wird der Kunde nicht mehr im Dashboard ausgegeben.
             
![image alt](https://github.com/ManonLudt/Projekt-Schwimmbad-Besuchermanagement/blob/f561df57d50e625b78889702bf5a4b4e04a1cb8c/Screenshots/Screenshot%202025-02-25%20152401.png)
![image alt](https://github.com/ManonLudt/Projekt-Schwimmbad-Besuchermanagement/blob/f72fa504ff58aa7c9bd2e109f913e44f50f555a0/Screenshots/Screenshot%202025-02-25%20152411.png)

## Reservierungverwaltung
Befindet der Benutzer sich im Dashboard, so kann er über den Menüpunkt _Reservierung_ Reservierungen verwalten.
Zudem sieht er alle anwesenden und abwesenden Kunden, sowie sie Gesamtanzahl.

**Anlegen:** Hier kann der Benutzer eine neue Reservierung anlegen, indem er eine ReservierungID vergibt und die ID eines existierenden 
             Kunden und Tickets angibt.
             Dabei wird die Anwesenheit des Kunden standartmäßig auf Abwesend gesetzt und uss im nachinein angepasst werden.
             Jedes Feld muss dabei ausgefüllt sein, um das Erstellen zu gewährleisten.
             Zudem müssen in allen Feldern ein INT-Wert eingegeben werden.
             Nach dem anlegen, wird die Reservierung im Dashboard ausgegeben.

**Ändern:** Hier kann er Benutzer eine bereits existierende Reservierung anpassen, indem er die entsprechende ReservierungID angibt.
            Er kann den Kunden oder das Tiket mit der entsprechenden  ID anpassen.
            Auch hier müssen alle Felder ausgefüllt werden um eine Änderung durchzuführen.
            Zudem müssen weiterhin in alle Feldern INT-Werte eingegeben werden.
            Nach dem ändern, werden die Reservierungsinformation im Dashboard ausgegeben.

**Löschen:** Bei diesem Punkt kann der Benutzer ein bereits existierende Reservierung löschen, indem er die entsprechende ReservierungID angibt.
             Es muss nur beachtet werden, das kein String-Wert in der ReservierungID eingegeben wird.
             Nach der Löschung wird die Reservierung nicht mehr im Dashboard ausgegeben.

![image alt](https://github.com/ManonLudt/Projekt-Schwimmbad-Besuchermanagement/blob/9f218cb20c850d10ff40d651ba9c835358de95ce/Screenshots/Screenshot%202025-02-25%20152426.png)
![image alt](https://github.com/ManonLudt/Projekt-Schwimmbad-Besuchermanagement/blob/2e696fcedcfdb961e4df19191e806cd5d57ad421/Screenshots/Screenshot%202025-02-25%20152437.png)

Schnittstelle | Code
------------- | -------------
Microsoft SQL Server Managment Studio | SQL-Code
Visual Studio  | C#
Visual Studio  | WPF

Ordner
--------
```
Dokumentation:
---

Planungsdokumente:
Hier ist der SQL-Code der Datenbank vorhanden.

Implementierung:
Hier werden die Anforderungen, der Papierprototyp, sowie ein ER-Diagramm der Datenbank gespeichert.

Konsolen-Prototyp:
Hier ist der C# Code für den Konsolen-Prototyp für das Besuchermanagment enthalten.
```


