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
    public class VrstaStokeFarmaController : ApiController
    {
        // GET: api/VrstaStokeFarma
        [Route("api/VrstaStokeFarma")]
        [ResponseType(typeof(IEnumerable<Vrsta_stoke_farma>))]
        public async Task<IHttpActionResult> GetOnForma()
        {
            try
            {
                var servis = new ServisVrstaStokeFarma();
                IEnumerable<Vrsta_stoke_farma> vrsteStoke = await servis.DohvatiVrsteStoke();
                return Ok(vrsteStoke);
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

        // GET: api/VrstaStokeFarma/Farma/{id}
        [Route("api/VrstaStokeFarma/Farma/{id}")]
        [ResponseType(typeof(IEnumerable<Vrsta_stoke_farma>))]
        public async Task<IHttpActionResult> GetOnForma(int id)
        {
            try
            {
                var servis = new ServisVrstaStokeFarma();
                IEnumerable<Vrsta_stoke_farma> vrsteStoke = await servis.DohvatiSveVrsteStokePremaIdFarmeAsync(id);
                return Ok(vrsteStoke);
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

        // GET: /api/VrstaStokeFarma/Farma/{id}/Nema
        // client.GetAsync("api/VrstaStokeFarma/Farma/" + farma.farmaid.toString() + "/Nema"
        [Route("api/VrstaStokeFarma/Farma/{id}/Nema")]
        [ResponseType(typeof(IEnumerable<Vrsta_stoke>))]
        public async Task<IHttpActionResult> GetNotForma(int id)
        {
            try
            {
                var servis = new ServisVrstaStokeFarma();
                IEnumerable<Vrsta_stoke> vrsteStoke = await servis.DohvatiSveVrsteStokeKojeNisuNaFarmi(id);
                return Ok(vrsteStoke);
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

        // POST: api/VrstaStoke
        [ResponseType(typeof(bool))]
        public IHttpActionResult Post([FromBody] Vrsta_stoke_farma vrstaStokeFarma)
        {
            try
            {
                var servis = new ServisVrstaStokeFarma();
                var uspjeh = servis.DodajVrstuStokeFarmi(vrstaStokeFarma);
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

        // DELETE: api/VrstaStoke/5
        [Route("api/VrstaStokeFarma/{id}")]
        [ResponseType(typeof(bool))]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var servis = new ServisVrstaStokeFarma();
                var uspjeh = servis.IzbrisiVrstuStokeNaFarmi(id);
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
