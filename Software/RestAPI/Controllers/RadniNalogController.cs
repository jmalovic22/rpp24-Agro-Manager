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
using Newtonsoft.Json;
using System.IO;
using System.Web;
using System.Threading.Tasks;

namespace RestAPI.Controllers
{
    public class RadniNalogController : ApiController
    {
        // GET: /api/RadniNalog/Polje/{id}
        [Route("api/RadniNalog/Polje/{id}")]
        [ResponseType(typeof(IEnumerable<Radni_nalog>))]
        public IHttpActionResult GetByPolje(int id)
        {
            try
            {
                var servis = new ServisRadniNalog();
                IEnumerable<Radni_nalog> radniNalozi = servis.DohvatiRadneNalogePolja(id);
                return Ok(radniNalozi);
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

        // GET: /api/RadniNalog/Poduzece/{id}
        [Route("api/RadniNalog/Poduzece/{id}")]
        [ResponseType(typeof(IEnumerable<Radni_nalog>))]
        public IHttpActionResult GetByPoduzece(int id)
        {
            try
            {
                var servis = new ServisRadniNalog();
                IEnumerable<Radni_nalog> radniNalozi = servis.DohvatiRadneNalogePoduzeca(id);
                return Ok(radniNalozi);
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

        // GET: /api/RadniNalog/5
        [Route("api/RadniNalog/{id}")]
        [ResponseType(typeof(Radni_nalog))]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var servis = new ServisRadniNalog();
                Radni_nalog radniNalozi = servis.DohvatiRadniNalogPremaId(id);
                return Ok(radniNalozi);
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

        // POST: /api/RadniNalog

        [ResponseType(typeof(bool))]
        public IHttpActionResult Post([FromBody] Radni_nalog radniNalog)
        {
            try
            {
                // var servis = new ServisRadniNalog();
                // Read the post data from the request body
                // Stream radniNalog3 = Request.Content.ReadAsStreamAsync().Result;
                // radniNalog3.Position = 0;
                // StreamReader reader = new StreamReader(radniNalog3);
                //string radniNalog2 = reader.ReadToEnd();
                //Radni_nalog radniNalog = JsonConvert.DeserializeObject<Radni_nalog>(radniNalog2);

                var servis = new ServisRadniNalog();
                var uspjeh = servis.DodajRadniNalog(radniNalog);
                return Ok(uspjeh);
                //return Ok(false);
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

        // PUT: /api/RadniNalog
        [ResponseType(typeof(bool))]
        public IHttpActionResult Put([FromBody] Radni_nalog radniNalog)
        {
            try
            {
                var servis = new ServisRadniNalog();
                var uspjeh = servis.AzurirajRadniNalog(radniNalog);
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

        // DELETE: /api/RadniNalog/5
        [Route("api/RadniNalog/{id}")]
        [ResponseType(typeof(bool))]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var servis = new ServisRadniNalog();
                var uspjeh = servis.ObrisiRadniNalog(id);
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
