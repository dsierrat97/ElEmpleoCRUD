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
    public class VendedorController : ApiController
    {
        Entities db = new Entities();

        [HttpGet]
        public IEnumerable<VENDEDOR> Get()
        {            

            using (Entities entities = new Entities())
            {
                entities.Configuration.ProxyCreationEnabled = false;
                return entities.VENDEDOR.ToList();
            }
        }

        [HttpGet]
        public VENDEDOR GetCiudades(int id)
        {

            using (Entities entities = new Entities())
            {
                entities.Configuration.ProxyCreationEnabled = false;
                return entities.VENDEDOR.FirstOrDefault(e => e.CODIGO == (decimal)id);
            }
        }

        [HttpPost]
        public IHttpActionResult AgregaCiudad([FromBody] VENDEDOR vendedor)
        {

            if (ModelState.IsValid)
            {
                db.VENDEDOR.Add(vendedor);
                db.SaveChanges();
                return Ok(vendedor);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public IHttpActionResult ActualizarCiudad(int id, [FromBody] VENDEDOR vendedor)
        {
            if (ModelState.IsValid)
            {
                var ciudadExiste = db.VENDEDOR.Count(c => c.CODIGO == vendedor.CODIGO) > 0;
                if (ciudadExiste)
                {
                    db.Entry(vendedor).State = EntityState.Modified;
                    db.SaveChanges();
                    return Ok(vendedor);
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
        public IHttpActionResult BorrarCiudad(int id)
        {
            var vendedor = db.VENDEDOR.Find(id);

            if (vendedor != null)
            {
                db.VENDEDOR.Remove(vendedor);
                db.SaveChanges();
                return Ok(vendedor);
            }
            else
            {
                return NotFound();
            }
        }
    }

}

