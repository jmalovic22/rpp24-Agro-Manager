using BusinessLogicLayer;
using EntitiesLayer.Entiteti;
using EntitiesLayer.Models;
using EntitiesLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace RestAPI.Controllers
{
    public class VrstaStokeController : ApiController
    {
        // GET: api/VrstaStoke
        [ResponseType(typeof(IEnumerable<Vrsta_stoke>))]
        public IHttpActionResult Get()
        {
            try
            {
                var servis = new ServisVrstaStoke();
                IEnumerable<Vrsta_stoke> vrstaStoke = servis.DohvatiVrsteStoke();
                return Ok(vrstaStoke);
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

        // GET: api/VrstaStoke/{id}
        //[Route("api/VrstaStoke/{id}")]
        [ResponseType(typeof(Vrsta_stoke))]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var servis = new ServisVrstaStoke();
                Vrsta_stoke vrstaStoke = servis.DohvatiVrsteStokePoId(id);
                return Ok(vrstaStoke);
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
