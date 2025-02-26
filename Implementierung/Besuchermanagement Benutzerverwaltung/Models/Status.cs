using System;
using System.Collections.Generic;

namespace Besuchermanagement_Benutzerverwaltung.Models
{
    public partial class Status
    {
        public int IdStatus { get; set; }
        public string? Bezeichnung { get; set; }
        public int? Rabatt { get; set; }
    }
}
