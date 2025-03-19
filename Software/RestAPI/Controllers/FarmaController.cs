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
    public class FarmaController : ApiController
    {
        // GET: /api/Farma/Poduzece/{id}
        [Route("api/Farma/Poduzece/{id}")]
        [ResponseType(typeof(IEnumerable<Farma>))]
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                //if (id <= 0)
                //{
                //    var greska = new Greska() { Status = "Greška ", Poruka = "Neispravna vrijednost za id poduzeća" };
                //    return Content(HttpStatusCode.BadRequest, greska);
                //}
                var servis = new ServisFarma();
                IEnumerable<Farma> farma = await servis.DohvatiFarmeTrazenogPoduzecaAsync(id);
                return Ok(farma);
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

        // POST: api/Farma
        [ResponseType(typeof(bool))]
        public async Task<IHttpActionResult> Post([FromBody] Farma farma)
        {
            try
            {
                var servis = new ServisFarma();
                var uspjeh = await servis.DodajFarmuAsync(farma);
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

        // PUT: api/Farma/5
        [ResponseType(typeof(bool))]
        public async Task<IHttpActionResult> Put([FromBody] Farma farma)
        {
            try
            {
                var servis = new ServisFarma();
                var uspjeh = await servis.AzurirajFarmuAsync(farma);
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

        // DELETE: api/Farma/5
        [Route("api/Farma/{id}")]
        [ResponseType(typeof(bool))]
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                var servis = new ServisFarma();
                var uspjeh = await servis.IzbrisiFarmuAsync(id);
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
