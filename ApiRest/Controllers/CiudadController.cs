using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DatosModel;

namespace ApiRest.Controllers
{
    public class CiudadController : ApiController
    {
        Entities db = new Entities();
        [HttpGet]
        public IEnumerable<CIUDAD> GetCiudades()
        {

            using (Entities entities = new Entities())
            {
                entities.Configuration.ProxyCreationEnabled = false;
                return entities.CIUDAD.ToList();
            }
        }

        [HttpGet]
        public CIUDAD GetCiudades(int id)
        {

            using (Entities entities = new Entities())
            {
                entities.Configuration.ProxyCreationEnabled = false;
                return entities.CIUDAD.FirstOrDefault(e => e.CODIGO == (decimal) id);
            }
        }

        [HttpPost]
        public IHttpActionResult AgregaCiudad([FromBody] CIUDAD ciudad)
        {

            if (ModelState.IsValid)
            {
                db.CIUDAD.Add(ciudad);
                db.SaveChanges();
                return Ok(ciudad);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public IHttpActionResult ActualizarCiudad(int id, [FromBody] CIUDAD ciudad)
        {
            if (ModelState.IsValid)
            {
                var ciudadExiste = db.CIUDAD.Count(c => c.CODIGO == ciudad.CODIGO) > 0;
                if (ciudadExiste)
                {
                    db.Entry(ciudad).State = EntityState.Modified;
                    db.SaveChanges();
                    return Ok(ciudad);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        public IHttpActionResult BorrarCiudad(int id )
        {
            var ciudad = db.CIUDAD.Find(id);

            if (ciudad != null)
            {
                db.CIUDAD.Remove(ciudad);
                db.SaveChanges();
                return Ok(ciudad);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
