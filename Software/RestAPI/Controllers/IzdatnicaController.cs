using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLogicLayer;
using System.Web.Http.Description;
using EntitiesLayer.Entiteti;
using RestAPI.Models;
using RestAPI.Utilities;
using System.Threading.Tasks;

namespace RestAPI.Controllers
{
    public class IzdatnicaController : ApiController
    {
        // GET: /api/Izdatnica/Silos/{id}
        [Route("api/Izdatnica/Silos/{id}")]
        [ResponseType(typeof(IEnumerable<Izdatnica>))]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var servis = new ServisIzdatnica();
                IEnumerable<Izdatnica> vozila = servis.DohvatiSveIzdatniceSilosa(id);
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

        // GET: /api/Izdatnica/Korisnik/{OIB}
        [Route("api/Izdatnica/Korisnik/{OIB}")]
        [ResponseType(typeof(IEnumerable<Izdatnica>))]
        public IHttpActionResult Get(string OIB)
        {
            try
            {
                var servis = new ServisIzdatnica();
                IEnumerable<Izdatnica> vozila = servis.DohvatiSveIzdatniceKorisnika(OIB);
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

        // POST: /api/Izdatnica
        [ResponseType(typeof(bool))]
        public async Task<IHttpActionResult> Post([FromBody] Izdatnica izdatnica)
        {
            try
            {
                var servis = new ServisIzdatnica();
                var uspjeh = await servis.DodajIzdatnicu(izdatnica);
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
