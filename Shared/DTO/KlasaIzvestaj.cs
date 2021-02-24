using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERacuniNovi.Shared.DTO
{
    public class KlasaIzvestaj
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public string NacinSlanja { get; set; }

        public string TipDatuma { get; set; }

        public KlasaIzvestaj() { }

        public KlasaIzvestaj(DateTime s, DateTime e, string n, string t)
        {
            Start = s;
            End = e;
            NacinSlanja = n;
            TipDatuma = t;
        }

    }
}
