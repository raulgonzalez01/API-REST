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

            ML.Usuario usuario = new ML.Usuario();

            ML.Direccion direccion = new ML.Direccion();

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
            ML.Result result = await Task.Run(() => BL.Usuarios.AgregarDireccion(direccion));


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


        //[HttpPost]
        //[Route("api/Usuario/Login")]
        //public async Task<IHttpActionResult> Login([FromBody] ML.Usuario usuario)
        //{
        //    ML.Result result = await Task.Run(() => BL.Usuarios.login(usuario));


        //    if (result.Correct)
        //    {
        //        return Ok(result);
        //    }
        //    else { return BadRequest(result.ErrorMessage); }

        //}


    }
}