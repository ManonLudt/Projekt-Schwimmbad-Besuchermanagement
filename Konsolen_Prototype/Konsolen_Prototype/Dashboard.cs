using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konsolen_Prototype
{
    public class Dashboard
    {
        public int BesucherAnwesend { get; set; } = 39;
        public int MaxAuslasung { get; set; } = 120;
        public int BesucherGesamtTag { get; set; } = 56;

        Kunde kunde = new Kunde();


        public void Ausgabe()
        {
            Console.OutputEncoding = Encoding.UTF8;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Dashboard");
                Console.WriteLine();
                Console.WriteLine($"Anwesend: {BesucherAnwesend}");
                Console.WriteLine($"Gesamt: {BesucherGesamtTag}");
                Console.WriteLine($"Max. Auslastung: {MaxAuslasung}");
                Console.WriteLine("r: Zurück zum Menü");
                Console.WriteLine();
                Console.WriteLine("Reservierung");
                Console.WriteLine("-----------------------------------------------------");
                foreach (KeyValuePair<string, string> x in kunde.Reservierungen)
                {
                    Console.WriteLine($"{x.Key}\t{x.Value}");
                }

                Console.WriteLine();
                Console.Write("> ");
                string eingabe = Console.ReadLine();

                if (eingabe == "r")
                {
                    break;
                }
            }           
        }
    }
}
