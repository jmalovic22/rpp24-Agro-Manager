using BusinessLogicLayer;
using EntitiesLayer.Entiteti;
using RestAPI.Models;
using RestAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace RestAPI.Controllers
{
    public class VoziloController : ApiController
    {
        // GET: api/Vozilo/Poduzece/{id}
        [Route("api/Vozilo/Poduzece/{id}")]
        [ResponseType(typeof(IEnumerable<Vozilo>))]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var servis = new ServisVozilo();
                IEnumerable<Vozilo> vozila = servis.DohvatiVozilaTrazenogPoduzeca(id);
                return Ok(vozila);
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

        // POST: api/Vozilo
        [ResponseType(typeof(bool))]
        public IHttpActionResult Post([FromBody] Vozilo vozilo)
        {
            try
            {
                var servis = new ServisVozilo();
                var uspjeh = servis.DodajVozilo(vozilo);
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

        // PUT: api/Vozilo/5
        [ResponseType(typeof(bool))]
        public IHttpActionResult Put([FromBody] Vozilo vozilo)
        {
            try
            {
                var servis = new ServisVozilo();
                var uspjeh = servis.AzurirajVozilo(vozilo);
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

        // DELETE: api/Vozilo/5
        [Route("api/Vozilo/{registracija}")]
        [ResponseType(typeof(bool))]
        public IHttpActionResult Delete(string registracija)
        {
            try
            {
                var servis = new ServisVozilo();
                var uspjeh = servis.IzbrisiVozilo(registracija);
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
