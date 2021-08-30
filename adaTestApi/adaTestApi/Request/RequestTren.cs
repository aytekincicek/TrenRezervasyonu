using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace adaTestApi.Request
{
    public class RequestTren
    {
        public Tren tren { get; set; }
        public int RezervasyonYapilacakKisiSayisi { get; set; }
        public Boolean KisilerFarkliVagonlaraYerlestirilebilir { get; set; }
    }
}
