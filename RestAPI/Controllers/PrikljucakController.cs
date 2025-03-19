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
    public class PrikljucakController : ApiController
    {
        // GET: api/Prikljucak/Poduzece/{id}
        [Route("api/Prikljucak/Poduzece/{id}")]
        [ResponseType(typeof(IEnumerable<Prikljucak>))]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var servis = new ServisPrikljucak();
                IEnumerable<Prikljucak> prikljuci = servis.DohvatiPrikljuckePoduzeca(id);
                return Ok(prikljuci);
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

        // POST: api/Prikljucak
        [ResponseType(typeof(bool))]
        public IHttpActionResult Post([FromBody] Prikljucak prikljucak)
        {
            try
            {
                var servis = new ServisPrikljucak();
                var uspjeh = servis.DodajPrikljucak(prikljucak);
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

        // PUT: api/Prikljucak/5
        [ResponseType(typeof(bool))]
        public IHttpActionResult Put([FromBody] Prikljucak prikljucak)
        {
            try
            {
                var servis = new ServisPrikljucak();
                var uspjeh = servis.AzurirajPrikljucak(prikljucak);
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

        // DELETE: api/Prikljucak/5
        [Route("api/Prikljucak/{id}")]
        [ResponseType(typeof(bool))]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var servis = new ServisPrikljucak();
                var uspjeh = servis.IzbrisiPrikljucak(id);
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
