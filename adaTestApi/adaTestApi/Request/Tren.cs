using adaTestApi.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace adaTestApi.Request
{
    public class Tren
    {
        public string Ad { get; set; }
        public List<Vagonlar> vagonlar { get; set; }
    }
}
