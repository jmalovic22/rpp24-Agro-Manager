using BusinessLogicLayer;
using EntitiesLayer.Entiteti;
using RestAPI.Models;
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
    public class SilosController : ApiController
    {
        // GET: api/Silos/Farma/{idFarma}
        [Route("api/Silos/Farma/{idFarma}")]
        [ResponseType(typeof(IEnumerable<Silos>))]
        public async Task<IHttpActionResult> GetByFarmaId(int idFarma)
        {
            try
            {
                var servis = new ServisSilos();
                IEnumerable<Silos> silos = await servis.DohvatiSiloseNekeFarmeAsync(idFarma);
                return Ok(silos);
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

        // GET: api/Silos/Poduzeca/{idPoduzece}
        [Route("api/Silos/Poduzeca/{idPoduzece}")]
        [ResponseType(typeof(IEnumerable<Silos>))]
        public async Task<IHttpActionResult> GetByPoduzeceId(int idPoduzece)
        {
            try
            {
                var servis = new ServisSilos();
                IEnumerable<Silos> silos = await servis.DohvatiSiloseNekogPoduzecaAsync(idPoduzece);
                return Ok(silos);
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

        // GET: api/Silos/{id}
        [Route("api/Silos/{id}")]
        [ResponseType(typeof(Silos))]
        public async Task<IHttpActionResult> GetById(int id)
        {
            try
            {
                var servis = new ServisSilos();
                Silos silos = await servis.DohvatiSilosPremaIdAsync(id);
                return Ok(silos);
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

        // POST: api/Silos
        [ResponseType(typeof(bool))]
        public IHttpActionResult Post([FromBody] Silos silos)
        {
            try
            {
                var servis = new ServisSilos();
                var uspjeh = servis.DodajSilosFarmi(silos);
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

        // PUT: api/Silos
        [ResponseType(typeof(bool))]
        public IHttpActionResult Put([FromBody] Silos silos)
        {
            try
            {
                var servis = new ServisSilos();
                var uspjeh = servis.AzurirajPodatkeSilosa(silos);
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

        // DELETE: api/Silos/id
        [Route("api/Silos/{id}")]
        [ResponseType(typeof(bool))]
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                var servis = new ServisSilos();
                var uspjeh = await servis.IzbrisiSilosAsync(id);
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
