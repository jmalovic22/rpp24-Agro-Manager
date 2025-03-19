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
    public class PoljeController : ApiController
    {
        // GET: api/Polje/Poduzece/{id}
        [Route("api/Polje/Poduzece/{id}")]
        [ResponseType(typeof(IEnumerable<Polje>))]
        public IHttpActionResult GetPoljePoduzeca(int id)
        {
            try
            {
                var servis = new ServisPolje();
                IEnumerable<Polje> polja = servis.DohvatiPoljaPoduzeca(id);
                return Ok(polja);
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

        // GET: api/Polje/5
        [Route("api/Polje/{id}")]
        [ResponseType(typeof(Polje))]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var servis = new ServisPolje();
                Polje polje = servis.DohvatiPolje(id);
                return Ok(polje);
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

        // Post: api/Polje
        [ResponseType(typeof(bool))]
        public IHttpActionResult Post([FromBody] Polje polje)
        {
            try
            {
                var servis = new ServisPolje();
                var uspjeh = servis.DodajPolje(polje);
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

        // Put: api/Polje
        [ResponseType(typeof(bool))]
        public IHttpActionResult Put([FromBody] Polje polje)
        {
            try
            {
                var servis = new ServisPolje();
                var uspjeh = servis.AzurirajPolje(polje);
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

        // Delete: api/Polje/{id}
        [Route("api/Polje/{id}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var servis = new ServisPolje();
                var uspjeh = servis.IzbrisiPolje(id);
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
