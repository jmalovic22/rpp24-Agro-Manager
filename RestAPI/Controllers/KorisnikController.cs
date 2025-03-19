using BusinessLogicLayer;
using EntitiesLayer.Entiteti;
using EntitiesLayer.Models;
using RestAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace RestAPI.Controllers
{
    public class KorisnikController : ApiController
    {
        // GET: /api/Korisnik/Poduzece/{id}
        [Route("api/Korisnik/Poduzece/{id}")]
        [ResponseType(typeof(IEnumerable<Korisnik>))]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var servis = new ServisKorisnik();
                IEnumerable<Korisnik> korisnici = servis.DohvatiKorisnikePoduzeca(id);
                return Ok(korisnici);
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

        // POST: /api/Korisnik/prijava
        [Route("api/Korisnik/prijava")]
        [HttpPost]
        [ResponseType(typeof(Korisnik))]
        public IHttpActionResult Prijava([FromBody] KorisnikPrijava prijava)
        {
            try
            {
                var servis = new ServisKorisnik();
                Korisnik k = servis.PrijavaKorisnika(prijava.Korisnicko_ime, prijava.Lozinka);
                return Ok(k);
            }
            catch (ArgumentException ax)
            {
                var greska = new Greska() { Status = "Greška ", Poruka = ax.Message };
                return Content(HttpStatusCode.BadRequest, greska);
            }
            catch (Exception ex)
            {
                var greska = Zajednicko.DohvatiOpisGreske(ex);
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.BadRequest, greska));
            }
        }

        // POST: api/Korisnik
        [ResponseType(typeof(bool))]
        public IHttpActionResult Post([FromBody] Korisnik korisnik)
        {
            try
            {
                var servis = new ServisKorisnik();
                var uspjeh = servis.DodajKorisnika(korisnik);
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

        // PUT: api/Korisnik/5
        [ResponseType(typeof(bool))]
        public IHttpActionResult Put([FromBody] Korisnik korisnik)
        {
            try
            {
                var servis = new ServisKorisnik();
                var uspjeh = servis.AzurirajKorisnika(korisnik);
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

        // DELETE: api/Korisnik/{oib}
        [Route("api/Korisnik/{oib}")]
        [ResponseType(typeof(bool))]
        public IHttpActionResult Delete(string oib)
        {
            try
            {
                var servis = new ServisKorisnik();
                var uspjeh = servis.IzbrisiKorisnika(oib);
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

        // GET: /api/Resursi/Poduzece/{id}
        [Route("api/Resursi/Poduzece/{id}")]
        [ResponseType(typeof(ResursiPoduzeca))]
        public IHttpActionResult GetResursi(int id)
        {
            try
            {
                var servis = new ServisKorisnik();
                ResursiPoduzeca korisnici = servis.DohvatiResursePoduzeca(id);
                return Ok(korisnici);
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
