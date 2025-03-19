using BusinessLogicLayer;
using EntitiesLayer.Entiteti;
using RestAPI.Models;
using RestAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace RestAPI.Controllers
{
    public class PosaoController : ApiController
    {
        // GET: api/Posao
        [ResponseType(typeof(IEnumerable<Posao>))]
        public IHttpActionResult Get()
        {
            try
            {
                var servis = new ServisPosao();
                IEnumerable<Posao> posao = servis.DohvatiPoslove();
                return Ok(posao);
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

        // GET: api/Posao
        [Route("api/Posao/{id}")]
        [ResponseType(typeof(Posao))]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var servis = new ServisPosao();
                Posao posao = servis.DohvatiPosao(id);
                return Ok(posao);
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

        // Post: api/Posao
        [ResponseType(typeof(bool))]
        public IHttpActionResult Post([FromBody] Posao posao)
        {
            try
            {
                var servis = new ServisPosao();
                var uspjeh = servis.DodajPosao(posao);
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

        // Put: api/Posao
        [ResponseType(typeof(bool))]
        public IHttpActionResult Put([FromBody] Posao posao)
        {
            try
            {
                var servis = new ServisPosao();
                var uspjeh = servis.AzurirajPosao(posao);
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

        // Delete: api/Posao/{id}
        [Route("api/Posao/{id}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var servis = new ServisPosao();
                var uspjeh = servis.IzbrisiPosao(id);
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
