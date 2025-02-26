using System;
using System.Collections.Generic;

namespace Besuchermanagement_Benutzerverwaltung.Models
{
    public partial class Besucher
    {
        public int IdBesucher { get; set; }
        public string? Vorname { get; set; }
        public string? Nachname { get; set; }
        public int? AlterKunde { get; set; }
        public string? Status { get; set; }
    }
}
