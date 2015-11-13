using BussinesLogic;
using MutantsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MutantsAPI.Controllers
{
    [RoutePrefix("Mutants")]
    public class MutantsController : ApiController
    {
        [HttpPost]
        [Route("isMutant")]
        public bool SetPermisosInstitucion([FromBody]MutantModel dna)
        {
            string[] secuenciaEntrada = dna.cadena.Replace(@"""", "").Split(',');

            BL negocio = new BL();
            if (negocio.IsMutant(secuenciaEntrada))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
