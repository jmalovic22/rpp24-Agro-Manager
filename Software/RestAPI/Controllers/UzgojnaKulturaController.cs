using EntitiesLayer.Entiteti;
using System;
using BusinessLogicLayer;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using RestAPI.Models;
using RestAPI.Utilities;
namespace RestAPI.Controllers
{
    public class UzgojnaKulturaController : ApiController
    {
        // GET: api/UzgojnaKultura
        [ResponseType(typeof(IEnumerable<Uzgojna_kultura>))]
        public IHttpActionResult Get()
        {
            try
            {
                var servis = new ServisUzgojnaKultura();
                IEnumerable<Uzgojna_kultura> uzgojnaKulturas = servis.DohvatiUzgojneKulture();
                return Ok(uzgojnaKulturas);
            }
            catch (ArgumentException ax)
            {
                var greska = new Greska() { Status = "Greška ", Poruka = ax.Message };
                return Content(HttpStatusCode.BadRequest, greska);
            }
            catch (Exception ex)
            {
                var greska = Zajednicko.DohvatiOpisGreske(ex);
                return Content(HttpStatusCode.BadRequest, greska);
            }
        }

        // GET: api/UzgojnaKultura/5
        [ResponseType(typeof(Tip_korisnika))]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var servis = new ServisUzgojnaKultura();
                Uzgojna_kultura uzgojnaKulturas = servis.DohvatiUzgonjuKulturuPremaId(id);
                return Ok(uzgojnaKulturas);
            }
            catch (ArgumentException ax)
            {
                var greska = new Greska() { Status = "Greška ", Poruka = ax.Message };
                return Content(HttpStatusCode.BadRequest, greska);
            }
            catch (Exception ex)
            {
                var greska = Zajednicko.DohvatiOpisGreske(ex);
                return Content(HttpStatusCode.BadRequest, greska);
            }
        }
    }
}
