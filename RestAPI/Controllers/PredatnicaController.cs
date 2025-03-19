using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using BusinessLogicLayer;
using EntitiesLayer.Entiteti;
using RestAPI.Models;
using RestAPI.Utilities;
using System.Web.Http.Description;

namespace RestAPI.Controllers
{
    public class PredatnicaController : ApiController
    {
        // GET: /api/Predatnica/Silos/{id}
        [Route("api/Predatnica/Silos/{id}")]
        [ResponseType(typeof(IEnumerable<Predatnica>))]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var servis = new ServisPredatnica();
                IEnumerable<Predatnica> vozila = servis.DohvatiSvePredatniceSilosa(id);
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

        // GET: /api/Predatnica/Korisnik/{OIB}
        [Route("api/Predatnica/Korisnik/{OIB}")]
        [ResponseType(typeof(IEnumerable<Predatnica>))]
        public IHttpActionResult Get(string OIB)
        {
            try
            {
                var servis = new ServisPredatnica();
                IEnumerable<Predatnica> vozila = servis.DohvatiSvePredatniceKorisnika(OIB);
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

        // POST: /api/Predatnica
        [ResponseType(typeof(bool))]
        public async Task<IHttpActionResult> Post([FromBody] Predatnica predatnica)
        {
            try
            {
                var servis = new ServisPredatnica();
                var uspjeh = await servis.DodajPredatnicu(predatnica);
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
