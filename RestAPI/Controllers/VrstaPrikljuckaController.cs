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
    public class VrstaPrikljuckaController : ApiController
    {
        // GET: api/VrstaPrikljucka
        [ResponseType(typeof(IEnumerable<Vrsta_prikljucka>))]
        public IHttpActionResult Get()
        {
            try
            {
                var servis = new ServisVrstaPrikljucka();
                IEnumerable<Vrsta_prikljucka> vrstePrikljucka = servis.DohvatiVrstePrikljucka();
                return Ok(vrstePrikljucka);
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

        // GET: api/VrstaPrikljucka/{id}
        [ResponseType(typeof(Vrsta_prikljucka))]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var servis = new ServisVrstaPrikljucka();
                var uspjeh = servis.DohvatiVrstuPrikljuckaPoId(id);
                return Ok(uspjeh);
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
