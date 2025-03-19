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
    public class StavkaRadnogNalogaController : ApiController
    {
        // GET: /api/StavkaRadnogNaloga/RadniNalog/{id}
        [Route("api/StavkaRadnogNaloga/RadniNalog/{id}")]
        [ResponseType(typeof(IEnumerable<Stavka_radnog_naloga>))]
        public IHttpActionResult GetByRadniNalog(int id)
        {
            try
            {
                var servis = new ServisStavkaRadnogNaloga();
                IEnumerable<Stavka_radnog_naloga> stavke = servis.DohvatiStavkeRadnogNaloga(id);
                return Ok(stavke);
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

        // GET: /api/StavkaRadnogNaloga/5
        [Route("api/StavkaRadnogNaloga/{id}")]
        [ResponseType(typeof(Stavka_radnog_naloga))]
        public IHttpActionResult GetById(int id)
        {
            try
            {
                var servis = new ServisStavkaRadnogNaloga();
                Stavka_radnog_naloga stavka = servis.DohvatiStavkuRadnogNalogaPoId(id);
                return Ok(stavka);
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

        // POST: api/StavkaRadnogNaloga
        [ResponseType(typeof(bool))]
        public IHttpActionResult Post([FromBody] Stavka_radnog_naloga stavkaRadnogNaloga)
        {
            try
            {
                var servis = new ServisStavkaRadnogNaloga();
                var uspjeh = servis.DodajStavkuRadnogNaloga(stavkaRadnogNaloga);
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

        // PUT: api/StavkaRadnogNaloga
        [ResponseType(typeof(bool))]
        public IHttpActionResult Put([FromBody] Stavka_radnog_naloga stavkaRadnogNaloga)
        {
            try
            {
                var servis = new ServisStavkaRadnogNaloga();
                var uspjeh = servis.AzurirajStavkuRadnogNaloga(stavkaRadnogNaloga);
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

        // DELETE: api/StavkaRadnogNaloga/5
        [Route("api/StavkaRadnogNaloga/{id}")]
        [ResponseType(typeof(bool))]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var servis = new ServisStavkaRadnogNaloga();
                var uspjeh = servis.ObrisiStavkuRadnogNaloga(id);
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
