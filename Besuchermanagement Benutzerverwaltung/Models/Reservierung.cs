using System;
using System.Collections.Generic;

namespace Besuchermanagement_Benutzerverwaltung.Models
{
    public partial class Reservierung
    {
        public int IdReservierung { get; set; }
        public string? Vorname { get; set; }
        public string? Nachname { get; set; }
        public string? Status { get; set; }
        public string? Ticket { get; set; }
        public int? Rabatt { get; set; }
        public int? Anwesend { get; set; }
    }
}
