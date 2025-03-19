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
using System.Threading.Tasks;

namespace RestAPI.Controllers
{
    public class EvidencijaStokeFarmaController : ApiController
    {
        // GET: /api/EvidencijaStokeFarma/Poduzece/{id}
        [Route("api/EvidencijaStokeFarma/Poduzece/{id}")]
        [ResponseType(typeof(IEnumerable<Evidencija_stoke_farma>))]
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                var servis = new ServisEvidencijaStokeFarma();
                IEnumerable<Evidencija_stoke_farma> evidencije = await servis.DohvatiSveEvidencijeStokeNaFarmiPoduzeca(id);
                return Ok(evidencije);
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

        // GET: /api/EvidencijaStokeFarma/Farma/{id}
        [Route("api/EvidencijaStokeFarma/Farma/{id}")]
        [ResponseType(typeof(IEnumerable<Evidencija_stoke_farma>))]
        public async Task<IHttpActionResult> GetZaFarmu(int id)
        {
            try
            {
                var servis = new ServisEvidencijaStokeFarma();
                IEnumerable<Evidencija_stoke_farma> evidencije = await servis.DohvatiSveEvidencijeStokeNaFarmi(id);
                return Ok(evidencije);
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

        // POST: api/EvidencijaStokeFarma
        [Route("api/EvidencijaStokeFarma")]
        [ResponseType(typeof(bool))]
        public async Task<IHttpActionResult> Post([FromBody] Evidencija_stoke_farma evidencijaStokeFarma)
        {
            try
            {
                var servis = new ServisEvidencijaStokeFarma();
                var uspjeh = await servis.DodajEvidencijuStokeNaFarmi(evidencijaStokeFarma);
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
