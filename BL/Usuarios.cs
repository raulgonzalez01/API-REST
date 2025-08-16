using ML;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuarios
    {
        public static async Task<ML.Result> GetAll(string sortedBy)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.UsuarioEntities context = new DL.UsuarioEntities())
                {
                    var query = await Task.Run(() => context.GetAll(sortedBy).ToList());



                    var usuariosDict = new Dictionary<int, ML.Usuario>();

                    foreach (var item in query)
                    {
                        if (usuariosDict.ContainsKey(item.IdUsuario))
                        {
                            ML.Usuario usuario = new ML.Usuario
                            {
                                IdUsuario = item.IdUsuario,
                                Nombre = item.Nombre,
                                Email = item.Email,
                                Telefono = item.TelefonO,
                                Password = item.Password,
                                TaxId = item.TaxId,
                                FechaCreacion = item.FechaCreacion,
                                Direcciones = new List<ML.Direccion>()
                            };

                            usuariosDict.Add(item.IdUsuario, usuario);
                        }

                        if (item.IdDireccion != null)
                        {
                            ML.Direccion direccion = new ML.Direccion
                            {
                                IdDireccion = item.IdDireccion ?? 0,
                                Nombre = item.EntornoDireccion ?? "",
                                Calle = item.Calle ?? "",
                                CodigoPais = item.CodigoPais ?? "",
                                UsuarioId = item.IdUsuario
                            };

                            usuariosDict[item.IdUsuario].Direcciones.Add(direccion);
                        }
                    }

                    var usuarios = usuariosDict.Values.ToList();

 
                    result.Objects = usuarios.Cast<object>().ToList();
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }


        public static async Task<ML.Result> Agregar(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
              
                if (BL.Validaciones.ValidarRFC(usuario.TaxId))
                {
                    result.Correct = false;
                    result.ErrorMessage = "El TaxId no cumple con el formato RFC.";
                    return result;
                }

         
                if (BL.Validaciones.ValidarTelefono(usuario.Telefono))
                {
                    result.Correct = false;
                    result.ErrorMessage = "El número de teléfono no es válido.";
                    return result;
                }

                using (DL.UsuarioEntities context = new DL.UsuarioEntities())
                {
                    TimeZoneInfo madagascarTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. Africa Standard Time");
                    DateTimeOffset fechaCreacion = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, madagascarTimeZone);

                    var item = await Task.Run(() => context.AgregarUsuario(
                        usuario.Nombre,
                        usuario.Email,
                        usuario.Telefono,
                        usuario.Password,
                        usuario.TaxId, // se puso UNIQUE en la base de datos
                        fechaCreacion));

                    if (item > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se agregó ningún usuario.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }


        public static async Task<ML.Result> AgregarDireccion(ML.Direccion direccion)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.UsuarioEntities context = new DL.UsuarioEntities())
                {

                    var item = await Task.Run(() => context.AgregarDireccion(
                     direccion.Nombre,
                     direccion.Calle,
                     direccion.CodigoPais,
                     direccion.UsuarioId));
                
                   

                    if (item > 0)
                    {
                        result.Correct = true;

                    }
                    else
                    {
                        result.ErrorMessage = "No se agrego ningun usuario";
                    }

                }
            }
            catch (Exception ex)
            {

                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;


            }

            return result;

        }


        public static async Task<ML.Result> Actualizar(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {




                using (DL.UsuarioEntities context = new DL.UsuarioEntities())
                {
                    var query = await Task.Run(() => context.ActualizarUsuario(usuario.IdUsuario,
                     usuario.Nombre,
                     usuario.Email,
                     usuario.Telefono,
                     usuario.Password,
                     usuario.TaxId
                     ));


                    if (query > 0)
                    {

                        result.Correct = true;

                    }

                    else
                    {
                        result.ErrorMessage = "Error al actualizar el usuario";
                    }

                }


            }

            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;

        }


        public static async Task<ML.Result> ActualizarDireccion(ML.Direccion direccion)
        {
            ML.Result result = new ML.Result();

            try
            {




                using (DL.UsuarioEntities context = new DL.UsuarioEntities())
                {
                    var query = await Task.Run(() => context.ActualizarDireccion(
                        direccion.IdDireccion,
                     direccion.Nombre,
                     direccion.Calle,
                     direccion.CodigoPais
                  
                   
                     ));


                    if (query > 0)
                    {

                        result.Correct = true;

                    }

                    else
                    {
                        result.ErrorMessage = "Error al actualizar el usuario";
                    }

                }


            }

            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;

        }







        public static async Task<ML.Result> Eliminar(int idUsuario)
        {
            ML.Result result = new ML.Result();

            try
            {


                using (DL.UsuarioEntities context = new DL.UsuarioEntities())
                {
                    var query = await Task.Run(() => context.Eliminar(idUsuario));


                    if (query > 0)
                    {

                        result.Correct = true;

                    }

                    else
                    {
                        result.ErrorMessage = "Error al Eliminar el usuario";
                    }

                }


            }

            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;

        }


        public static ML.Result Filtro(string campo, string operacion, string valor)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.UsuarioEntities context = new DL.UsuarioEntities())
                {
                    
                    string query = $"EXEC FiltroDinamico @Campo = '{campo}', @Operacion = '{operacion}', @Valor = '{valor}'";
                    var filtro = context.Database.SqlQuery<ML.Usuario>(query).ToList();

                   

                    result.Objects = new List<object>();
                    foreach (var usuario in filtro)
                    {
                        

                        ML.Usuario user = new ML.Usuario();
                        user.IdUsuario = usuario.IdUsuario;
                        user.Nombre = usuario.Nombre;
                        user.Email = usuario.Email;
                        user.Telefono = usuario.Telefono;
                        user.Password = usuario.Password;
                        user.TaxId = usuario.TaxId;
                        user.FechaCreacion = usuario.FechaCreacion;


                        result.Objects.Add(usuario);
                    }

                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }



        public static async Task<ML.Result> Login(string taxId, string password)
        {
            ML.Result result = new ML.Result();

            try
            {

      

                using (DL.UsuarioEntities context = new DL.UsuarioEntities())
                {
                    

                    var item = await Task.Run(() => context.SpLogin(
                       taxId,  
                       password).ToList());

                    if (item != null && item.Any())
                    {
                        result.Correct = true;
                        result.Object = item;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Contraseña o RFC Incorrecto";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }





    }
}
