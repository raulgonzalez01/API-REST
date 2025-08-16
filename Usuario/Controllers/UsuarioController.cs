using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;



namespace Usuario.Controllers
{
    public class UsuarioController : ApiController
    {

        [HttpGet]
        [Route("api/Usuario/GetAll")]
        public async Task<IHttpActionResult> GetAll(string sortedBy)
        {
            ML.Result result = await BL.Usuarios.GetAll(sortedBy);


            if (result.Correct)
            {
                return Ok(result.Objects);

            }

            else
            {
                return BadRequest(result.ErrorMessage);

            }

        }


        [HttpPost]
        [Route("api/Usuario/Agregar")]
        public async Task<IHttpActionResult> Agregar([FromBody] ML.Usuario usuario)
        {
            ML.Result result = await Task.Run(() => BL.Usuarios.Agregar(usuario));


            if (result.Correct)
            {
                return Ok(result);
            }
            else { return BadRequest(result.ErrorMessage); }

        }

        [HttpPost]
        [Route("api/Usuario/AgregarDireccion")]
        public async Task<IHttpActionResult> AgregarDireccion([FromBody] ML.Direccion direccion)
        {
            ML.Result result = await Task.Run(() => BL.Usuarios.AgregarDireccion(direccion));


            if (result.Correct)
            {
                return Ok(result);
            }
            else { return BadRequest(result.ErrorMessage); }

        }

        [HttpPut]
        [Route("api/Usuario/Actualizar")]
        public async Task<IHttpActionResult> Actualizar([FromBody] ML.Usuario usuario)
        {
            ML.Result result = await Task.Run(() => BL.Usuarios.Actualizar(usuario));


            if (result.Correct)
            {
                return Ok(result);
            }
            else { return BadRequest(result.ErrorMessage); }


        }



        [HttpPut]
        [Route("api/Usuario/ActualizarDireccion")]
        public async Task<IHttpActionResult> ActualizarDireccion([FromBody] ML.Direccion direccion)
        {
            ML.Result result = await Task.Run(() => BL.Usuarios.ActualizarDireccion(direccion));


            if (result.Correct)
            {
                return Ok(result);
            }
            else { return BadRequest(result.ErrorMessage); }


        }

        [HttpDelete]
        [Route("api/Usuario/Eliminar/{idUsuario}")]
        public async Task<IHttpActionResult> Eliminar(int idUsuario)
        {
            ML.Result result = await Task.Run(() => BL.Usuarios.Eliminar(idUsuario));


            if (result.Correct)
            {
                return Ok(result);
            }
            else { return BadRequest(result.ErrorMessage); }


        }


        [HttpGet]
        [Route("api/Usuario/filtro")]
        public async Task<IHttpActionResult> Filtro(string campo, string operacion, string valor)
        {
            ML.Result result = await Task.Run(() => BL.Usuarios.Filtro(campo, operacion, valor));


            if (result.Correct)
            {
                return Ok(result);
            }
            else { return BadRequest(result.ErrorMessage); }


        }



        [HttpPost]
        [Route("api/Usuario/Login")]
        public async Task<IHttpActionResult> Login(string taxId, string password)
        {
            ML.Result result = await Task.Run(() => BL.Usuarios.Login(taxId, password));


            if (result.Correct)
            {
                return Ok(result.Correct);
              
            }
            else { return BadRequest(result.ErrorMessage); }

        }


    }
}