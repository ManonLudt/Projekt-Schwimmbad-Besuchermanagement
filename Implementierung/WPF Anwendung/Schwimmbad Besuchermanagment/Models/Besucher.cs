using System;
using System.Collections.Generic;

namespace Schwimmbad_Besuchermanagment.Models
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
