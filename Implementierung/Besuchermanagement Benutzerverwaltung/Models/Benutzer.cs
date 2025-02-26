using System;
using System.Collections.Generic;

namespace Besuchermanagement_Benutzerverwaltung.Models
{
    public partial class Benutzer
    {
        public int IdBenutzer { get; set; }
        public string? Benutzername { get; set; }
        public int? Passwort { get; set; }
        public string? Email { get; set; }
        public string? Telefon { get; set; }
    }
}
