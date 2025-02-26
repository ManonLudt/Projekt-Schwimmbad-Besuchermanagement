using System;
using System.Collections.Generic;

namespace Besuchermanagement_Benutzerverwaltung.Models
{
    public partial class Ticket
    {
        public int IdTicket { get; set; }
        public string? Bezeichnung { get; set; }
        public decimal? Preis { get; set; }
        public int? Anzahl { get; set; }
    }
}
