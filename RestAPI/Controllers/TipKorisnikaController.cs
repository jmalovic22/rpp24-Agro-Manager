using System;
using BusinessLogicLayer;
using EntitiesLayer.Entiteti;
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
    public class TipKorisnikaController : ApiController
    {
        // GET: api/TipKorisnika
        [ResponseType(typeof(IEnumerable<Tip_korisnika>))]
        public IHttpActionResult Get()
        {
            try
            {
                var servis = new ServisTipKorisnika();
                IEnumerable<Tip_korisnika> tipoviKorisnika = servis.DohvatiTipKorisnika();
                return Ok(tipoviKorisnika);
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

        // GET: api/TipKorisnika/5
        [ResponseType(typeof(Tip_korisnika))]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var servis = new ServisTipKorisnika();
                Tip_korisnika tipKorisnika = servis.DohvatiTipKorisnikaPoId(id);
                return Ok(tipKorisnika);
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
