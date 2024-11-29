using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konsolen_Prototype
{
    internal class Tickets
    {
        public Dictionary<string, string> TicketListe { get; set; }

        public Tickets()
        {
            TicketListe = new Dictionary<string, string>();
            TicketListe.Add("Bezeichnung", "Preis\tAnzahl");
            TicketListe.Add("Standartticket", "4 €\t22");
            TicketListe.Add("Tagesticket", "5 €\t45");
            TicketListe.Add("Kinderticket", "3 €\t67");
            TicketListe.Add("Familienticket", "7,5 €\t34");
            TicketListe.Add("Gruppenticket", "10 €\t53");
        }

        public void Ausgabe()
        {
            Console.OutputEncoding = Encoding.UTF8;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Ticket");
                Console.WriteLine();

                foreach (KeyValuePair<string, string> x in TicketListe)
                {
                    Console.WriteLine($"{x.Key}\t\t{x.Value}");
                }

                Console.WriteLine();
                Console.WriteLine("a: Neu anlegen");
                Console.WriteLine("c: Ändern");
                Console.WriteLine("d: Löschen");
                Console.WriteLine("r: Zurück zum Menü");
                Console.Write("> ");
                string eingabe = Console.ReadLine();

                Console.WriteLine();

                if (eingabe == "a")
                {
                    AddTickets();
                }

                if (eingabe == "c")
                {
                    ChangeTicket();
                }

                if (eingabe == "d")
                {
                    DeleteTicket();
                }

                if (eingabe == "r")
                {
                    break;
                }
            }
        }

        public void AddTickets()
        {
            while (true)
            {
                Console.Write("Ticketname: ");
                string name = Console.ReadLine();

                if (name == "")
                {
                    Console.WriteLine("Name eingeben!");
                }

                else
                {
                    Console.Write("Preis: ");
                    string preis = Console.ReadLine();

                    if (preis == "")
                    {
                        Console.WriteLine("Preis eingeben!");
                    }

                    if (preis == "0")
                    {
                        Console.WriteLine("Preis  zu niedrieg!");
                    }

                    else
                    {
                        Console.Write("Anzahl: ");
                        string anzahl = Console.ReadLine();

                        if (anzahl == "")
                        {
                            Console.WriteLine("Anzahl eingeben!");
                        }

                        else
                        {
                            TicketListe.Add(name, "\t" + preis + " €" + "\t" + anzahl);
                        }

                        Console.WriteLine("Ticket wurde hinzugefügt!");
                        Console.ReadKey();
                        break;
                    }
                }
            }
        }

        public void ChangeTicket()
        {
            while (true)
            {
                Console.Write("Ticketbezeichnung: ");
                string name = Console.ReadLine();

                if (TicketListe.ContainsKey(name))
                {
                    Console.Write("Neuer Preis: ");
                    string preis = Console.ReadLine();

                    if (preis == "")
                    {
                        Console.WriteLine("Preis eingeben!");
                    }

                    if (preis == "0")
                    {
                        Console.WriteLine("Preis zu niedrig!");
                    }

                    else
                    {
                        Console.Write("Neue Anzahl: ");
                        string anzahl  = Console.ReadLine();

                        preis = "\t" + preis + " €" + "\t" + anzahl;
                        TicketListe[name] = preis;
                        Console.WriteLine("Preis wurde geändert!");
                        Console.ReadKey();
                        break;
                    }
                }

                else
                {
                    Console.WriteLine("Ticketbezeichnung falsch oder existiert nicht!");
                }
            }
        }

        public void DeleteTicket()
        {
            while (true)
            {
                Console.Write("Ticketbezeichnung: ");
                string name = Console.ReadLine();

                if (TicketListe.ContainsKey(name))
                {
                    TicketListe.Remove(name);
                    Console.WriteLine("Ticket wurde gelöscht!");
                    Console.ReadKey();
                    break;
                }

                else
                {
                    Console.WriteLine("Ticketbezeichnung falsch oder existiert nicht!");
                }
            }
        }
    }
}
