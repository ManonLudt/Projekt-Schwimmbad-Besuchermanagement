using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Konsolen_Prototype
{
    public class Passwort
    {
        Dictionary<string, string> passwortListe = new Dictionary<string, string>()
        {
            {"Test" , "1234"},
            {"Manon", "4321"}
        };

        public void Ausgabe()
        {
            Console.OutputEncoding = Encoding.UTF8;
            while (true)
            {
                Begin:
                Console.Clear();
                Console.WriteLine("Schwimmbad Besuchermanagemente");
                Console.WriteLine();
                Console.WriteLine("Herzlich Willkommen");
                Console.WriteLine();
                Console.WriteLine("a: Neu anlegen");
                Console.WriteLine("c: Ändern");
                Console.WriteLine("d: Löschen");
                Console.WriteLine("q: Programm beenden");
                Console.WriteLine();
                Console.Write("Benutzername: ");
                string name = Console.ReadLine();

                if (name == "a")
                {
                    AddPasswort();
                    goto Begin;
                }

                if (name == "c")
                {
                    ChangePasswort();
                    goto Begin;
                }

                if (name == "d")
                {
                    DeletePasswort();
                    goto Begin;
                }

                if (name == "q")
                {
                    return;
                }

                else
                {
                    Console.Write("Passwort: ");
                    string passwort = Console.ReadLine();

                    if (passwort == "a")
                    {
                        AddPasswort();
                        goto Begin;
                    }

                    if (passwort == "c")
                    {
                        ChangePasswort();
                        goto Begin;
                    }

                    if (passwort == "d")
                    {
                        DeletePasswort();
                        goto Begin;
                    }

                    if (passwort == "q")
                    {
                        return;
                    }

                    if (passwortListe.ContainsKey(name) && passwortListe[name] == passwort)
                    {
                        break;
                    }

                    else
                    {
                        Console.WriteLine("Anmeldedatein falsch!");
                        Console.ReadLine();
                    }
                }             
            }
        }



        public void AddPasswort()
        {
            while(true)
            {
                Console.Write("Benutzername: ");
                string name = Console.ReadLine();
                
                if (name == "")
                {
                    Console.WriteLine("Name eingeben!");
                }
                    
                else
                {                    
                    Console.Write("Passwort: ");
                    string passwort = Console.ReadLine();

                    if (passwort == "")
                    {
                        Console.WriteLine("Passwort eingeben!");
                    }

                    if (passwort.Length <= 2)
                    {
                        Console.WriteLine("Passwort zu kurz!");
                    }

                    if (passwort.Length >= 10)
                    {
                        Console.WriteLine("Passwort zu lang!");
                    }

                    else
                    {
                        passwortListe.Add(name, passwort);
                        Console.WriteLine("Passwort wurde hinzugefügt!");
                        Console.ReadKey();
                        break;
                    }
                }              
            }           
        }

        public void ChangePasswort()
        {
            while(true)
            {
                Console.Write("Benutzename: ");
                string name = Console.ReadLine();

                if (passwortListe.ContainsKey(name))
                {
                    Console.Write("Neues Passwort: ");
                    string NeuesPasswort = Console.ReadLine();

                    if (NeuesPasswort == "")
                    {
                        Console.WriteLine("Passwort eingeben!");
                    }

                    if (NeuesPasswort.Length <= 2)
                    {
                        Console.WriteLine("Passwort zu kurz!");
                    }

                    if (NeuesPasswort.Length >= 10)
                    {
                        Console.WriteLine("Passwort zu lang!");
                    }

                    else
                    {
                        passwortListe[name] = NeuesPasswort;
                        Console.WriteLine("Passwort wurde geändert!");
                        Console.ReadKey();
                        break;
                    }                  
                }

                else
                {
                    Console.WriteLine("Benutzername falsch oder existiert nicht!");
                }
            }
        }

        public void DeletePasswort()
        {
            while (true)
            {
                Console.Write("Benutzename: ");
                string name = Console.ReadLine();

                if (passwortListe.ContainsKey(name))
                {
                    passwortListe.Remove(name);
                    Console.WriteLine("Anmeldedaten wurde gelöscht!");
                    Console.ReadKey();
                    break;
                }

                else
                {
                    Console.WriteLine("Benutzername falsch oder existiert nicht!");
                }
            }
        }

        public void Test()
        {
            foreach (KeyValuePair<string, string> x in passwortListe)
            {
                Console.WriteLine(x.Key);
                Console.WriteLine(x.Value);
                Console.WriteLine();
            }
        }
    }
}

