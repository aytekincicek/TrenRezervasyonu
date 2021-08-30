using adaTestApi.Response;
using System;
using System.Collections.Generic;

namespace adaTestApi
{
    public class Rezervasyon
    {
        public Boolean RezervasyonYapilabilir { get; set; }
        public List<RezervasyonDetay> YerlesimAyrinti { get; set; }
    }
}
