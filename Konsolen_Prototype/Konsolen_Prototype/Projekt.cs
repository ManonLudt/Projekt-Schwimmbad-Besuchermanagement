using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Konsolen_Prototype
{
    internal class Projekt
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Passwort Passwort = new Passwort();
            Dashboard Dashboard = new Dashboard();
            Kunde Kunde = new Kunde();
            Tickets Tickets = new Tickets();

            while(true)
            {
                Passwort.Ausgabe();

                while(true)
                {
                    Console.Clear();
                    Console.WriteLine("Menü");
                    Console.WriteLine("-----------------------------------------------------");
                    Console.WriteLine("1. Dashboard");
                    Console.WriteLine("2. Kunden");
                    Console.WriteLine("3. Tickets");
                    Console.WriteLine("i + Zahl: Informationen");
                    Console.WriteLine("q: Beenden des Programms");
                    Console.Write("> ");
                    string eingabe = Console.ReadLine();
                    Console.WriteLine();

                    if (eingabe == "1")
                    {
                        Dashboard.Ausgabe();
                    }

                    if (eingabe == "i1")
                    {
                        Console.WriteLine("Bei dem Menüpunkt 'Dashboard' werden die aktuelle Besucherzahl, sowie eine Liste aller Reservierungen von Tickets.");
                        Console.WriteLine("Die aktuelle Besucherzahl wird in 'anwesend' und 'abwesend' seperiert, sowie die maximale Auslastungsmöglichkeit eines Schwimmbads.");
                        Console.ReadKey();
                    }

                    if (eingabe == "2")
                    {
                        Kunde.Ausgabe();
                    }

                    if (eingabe == "i2")
                    {
                        Console.WriteLine("Bei dem Menüpunkt 'Kunde' wird eine Liste aller Reservierungen von Tickets zurückgegeben, sowie wer diese reserviert hat.");
                        Console.WriteLine("Zu dieser Liste können hier neue Reservierungen angelegt, gelöscht oder geändert werden.");
                        Console.ReadKey();
                    }

                    if (eingabe == "3")
                    {
                        Tickets.Ausgabe();
                    }

                    if (eingabe == "i3")
                    {
                        Console.WriteLine("Bei dem Menüpunkt 'Tickets' wird eine Liste aller Tickets angezeigt, sowie die verfügbare Anzahl und mögliche Rabatte.");
                        Console.WriteLine("Zu dieser Liste können hier neue Tickets angelegt, gelöscht oder geändert werden.");
                        Console.ReadKey();
                    }

                    if (eingabe == "q")
                    {
                        return;
                    }
                }
                
            }
            
            
            



            Console.ReadLine();
        }

    }
}