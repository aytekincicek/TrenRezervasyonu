using adaTestApi.Request;
using adaTestApi.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace adaTestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RezervasyonController : ControllerBase
    {
        private readonly ILogger<RezervasyonController> _logger;

        public RezervasyonController(ILogger<RezervasyonController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("Rezervasyon")]
        public Rezervasyon findSeat([FromBody] RequestTren trenList)
        {
            Rezervasyon rezervasyon = new Rezervasyon();
            rezervasyon.RezervasyonYapilabilir = true;
            RezervasyonDetay YerlesimAyrinti = new RezervasyonDetay();
            List<RezervasyonDetay> rezervasyonDetayList = new List<RezervasyonDetay>();
            if (trenList.tren != null)
            {
                if (trenList.KisilerFarkliVagonlaraYerlestirilebilir == false)
                {               
                        foreach (Vagonlar vagonlar in trenList.tren.vagonlar)
                        {
                            if (vagonlar.DoluKoltukAdet + trenList.RezervasyonYapilacakKisiSayisi > (vagonlar.Kapasite) * 0.7)
                            {
                                rezervasyon.YerlesimAyrinti = rezervasyonDetayList;
                            }
                            else
                            {
                                YerlesimAyrinti.VagonAdi = vagonlar.Ad;
                                YerlesimAyrinti.RezervasyonYapilacakKisiSayisi = trenList.RezervasyonYapilacakKisiSayisi;
                                rezervasyonDetayList.Add(YerlesimAyrinti);
                                rezervasyon.YerlesimAyrinti = rezervasyonDetayList;
                                return rezervasyon;
                            }
                        }                  
                }
                else {
                    int sayac = 0;
                    int control = trenList.RezervasyonYapilacakKisiSayisi;
                    for (int i = 0; i < trenList.RezervasyonYapilacakKisiSayisi; i++)
                    {
                        foreach (var vagonlar in trenList.tren.vagonlar)
                            {
                            int sayac1 = 0;
                                while(vagonlar.DoluKoltukAdet < (vagonlar.Kapasite) * 0.7 && control > 0){
                                    vagonlar.DoluKoltukAdet++;
                                    control--;
                                    rezervasyon.YerlesimAyrinti = rezervasyonDetayList;
                                    sayac++;
                                    sayac1++;
                                }
                            if (sayac1 != 0) 
                            {
                                rezervasyonDetayList.Add(YerlestirilenKisi(vagonlar, sayac1));
                            }
                            if (sayac == trenList.RezervasyonYapilacakKisiSayisi)
                            {
                                return rezervasyon;
                            }
                        }
                     }
                    rezervasyon.YerlesimAyrinti = rezervasyonDetayList;
                }
            }
            
            return rezervasyon;
        }
        public RezervasyonDetay YerlestirilenKisi(Vagonlar vagonlar, int sayac1)
        {
            RezervasyonDetay YerlesimAyrinti = new RezervasyonDetay();
            YerlesimAyrinti.VagonAdi = vagonlar.Ad;
            YerlesimAyrinti.RezervasyonYapilacakKisiSayisi = sayac1;
            return YerlesimAyrinti;
        }
    }
}
