using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Konsolen_Prototype
{
    public class Kunde
    {
        public Dictionary<string, string> Reservierungen { get; set; }
        Tickets tickets = new Tickets();

        public Kunde()
        {
            Reservierungen = new Dictionary<string, string>();
            Reservierungen.Add("Vorname\tNachname\tStatus\t", "Ticket\tRabatt");
            Reservierungen.Add("Albert\tEinstein\t\t", "Tagesticket");
            Reservierungen.Add("Viktor\tRoth\t\t", "\tFamilienticket");
            Reservierungen.Add("Monikat\tBauer\t\tStuden", "\tSchülerticket\t5%");
            Reservierungen.Add("Angela\tMerkel\t\t", "\tFamilienticket");
            Reservierungen.Add("Peter\tHarrer\t\t", "\tTagesticket");
            Reservierungen.Add("Antonia\tSchmidt\t\tSchüler", "Tagesticket\t5%");
        }  

        public void Ausgabe()
        {
            Console.OutputEncoding = Encoding.UTF8;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Kunden");
                Console.WriteLine();

                foreach (KeyValuePair<string, string> x in Reservierungen)
                {
                    Console.WriteLine($"{x.Key}\t{x.Value}");
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
                    ReservierungAdd();
                }

                if (eingabe == "c")
                {
                    ReservierungChange();
                }

                if (eingabe == "d")
                {
                    ReservierungDelete();
                }

                if (eingabe == "r")
                {
                    break;
                }
            }            
        }
        
        public void ReservierungAdd()
        {
            while(true)
            {
                Console.Write("Kundenvorname: ");
                string vorname = Console.ReadLine();

                if (vorname == "")
                {
                    Console.WriteLine("Vorname eingeben!");
                }

                else
                {
                    Console.Write("Kundennachname: ");
                    string nachname = Console.ReadLine();

                    if (nachname == "")
                    {
                        Console.WriteLine("Nachname eingeben!");
                    }

                    else
                    {
                        Console.Write("Status (Schüler/ Student): ");
                        string status = Console.ReadLine();

                        if (status == "")
                        {
                            Console.WriteLine("Status eingeben!");
                        }

                        string name = vorname + "\t" + nachname + "\t" + status + "\t";

                        Console.Clear();
                        foreach (KeyValuePair<string, string> x in tickets.TicketListe)
                        {
                            Console.WriteLine($"{x.Key}\t\t{x.Value}");
                        }
                        Console.Write("Ticket: ");
                        string bezeichnung = Console.ReadLine();
                        string ticket;
  

                        if (tickets.TicketListe.ContainsKey(bezeichnung))
                        {

                            if (status != "")
                            {
                                Reservierungen.Add(name, "\t" + bezeichnung + "\t5%");
                                Console.WriteLine("Reservierung wurde hinzugefügt!");
                                Console.ReadKey();                               
                            }

                            else
                            {
                                Reservierungen.Add(name, "\t" + bezeichnung);
                                Console.WriteLine("Reservierung wurde hinzugefügt!");
                                Console.ReadKey();
                            }
                            break;
                        }

                        else
                        {
                            Console.WriteLine("Ticket existiert nicht!");
                        }
                    }                    
                }
            }           
        }

        public void ReservierungChange()
        {
            while (true)
            {
                Console.Write("Kundenvorname: ");
                string vorname = Console.ReadLine();

                if (vorname == "")
                {
                    Console.WriteLine("Vorname eingeben!");
                }

                else
                {
                    Console.Write("Kundennachname: ");
                    string nachname = Console.ReadLine();

                    if (nachname == "")
                    {
                        Console.WriteLine("Nachname eingeben!");
                    }

                    else
                    {
                        Console.Write("Status (Schüler/ Student): ");
                        string status = Console.ReadLine();

                        string name = vorname + "\t" + nachname + "\t" + status + "\t";

                        if (Reservierungen.ContainsKey(name))
                        {
                            Console.Clear();
                            foreach (KeyValuePair<string, string> x in tickets.TicketListe)
                            {
                                Console.WriteLine($"{x.Key}\t\t{x.Value}");
                            }
                            Console.Write("Neues Ticket:  ");
                            string bezeichnung = Console.ReadLine();
                            string ticket;


                            if (tickets.TicketListe.ContainsKey(bezeichnung))
                            {
                                ticket = bezeichnung + "\t5%";

                                if (status != "")
                                {
                                    ticket = bezeichnung + "\t5%";
                                    Reservierungen[name] = ticket;
                                    Console.WriteLine("Reservierung wurde geändert!");
                                    Console.ReadKey();
                                }

                                else
                                {
                                    ticket = bezeichnung;
                                    Reservierungen[name] = ticket;
                                    Console.WriteLine("Reservierung wurde geändert!");
                                    Console.ReadKey();
                                }
                                break;
                            }

                            else
                            {
                                Console.WriteLine("Kunde existiert nicht!");
                            }
                        }
                    }
                }           
            }          
        }

        public void ReservierungDelete()
        {
            while (true)
            {
                Console.Write("Kundenvorname: ");
                string vorname = Console.ReadLine();

                if (vorname == "")
                {
                    Console.WriteLine("Vorname eingeben!");
                }

                else
                {
                    Console.Write("Kundennachname: ");
                    string nachname = Console.ReadLine();

                    if (nachname == "")
                    {
                        Console.WriteLine("Nachname eingeben!");
                    }

                    else
                    {
                        Console.Write("Status (Schüler/ Student): ");
                        string status = Console.ReadLine();

                        string name = vorname + "\t" + nachname + "\t" + status + "\t";

                        if (Reservierungen.ContainsKey(name))
                        {
                            Reservierungen.Remove(name);
                            Console.WriteLine("Reservierung wurde gelöscht!");
                            Console.ReadKey();
                            break;
                        }

                        else
                        {
                            Console.WriteLine("Kundenname falsch oder existiert nicht!");
                        }
                    }
                }
            }
        }

        public void Test()
        {
            foreach (KeyValuePair<string, string> x in Reservierungen)
            {
                Console.WriteLine(x.Key);
                Console.WriteLine(x.Value);
                Console.WriteLine();
            }
        }
    }
}
