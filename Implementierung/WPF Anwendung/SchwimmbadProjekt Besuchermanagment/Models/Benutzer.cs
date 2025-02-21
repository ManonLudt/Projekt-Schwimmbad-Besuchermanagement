using System;
using System.Collections.Generic;

namespace Schwimmbad_Besuchermanagment.Models
{
    public partial class Benutzer
    {
        public int IdBenutzer { get; set; }
        public string? Benutzername { get; set; }
        public int? Passwort { get; set; }
    }
}
