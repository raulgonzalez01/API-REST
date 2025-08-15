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

                    var query = await Task.Run(() => context.GetAll().ToList());
                    switch (sortedBy.ToLower())
                    {
                        case "email":
                            query = query.OrderBy(u => u.Email).ToList();
                            break;
                        case "id":
                            query = query.OrderBy(u => u.IdUsuario).ToList();
                            break;
                        case "name":
                            query = query.OrderBy(u => u.Nombre).ToList();
                            break;
                        case "phone":
                            query = query.OrderBy(u => u.TelefonO).ToList();
                            break;
                        case "tax_id":
                            query = query.OrderBy(u => u.TaxId).ToList();
                            break;
                        case "created_at":
                            query = query.OrderBy(u => u.FechaCreacion).ToList();
                            break;
                        default:
                          
                            break;
                    }

                    if (query != null)
                    {
                        result.Objects = new List<object>();

                        foreach (var item in query)
                        {
                            ML.Usuario usuario = new ML.Usuario();

                            usuario.IdUsuario = item.IdUsuario;
                            usuario.Nombre = item.Nombre;
                            usuario.Email = item.Email;
                            usuario.Telefono = item.TelefonO;
                            usuario.Password = item.Password;
                            usuario.TaxId = item.TaxId;
                            usuario.FechaCreacion = item.FechaCreacion.ToString("dd-mm-yyyy HH:mm");

                            usuario.Direcciones = new List<ML.Direccion>
                                {
                                    new ML.Direccion
                                    {
                                        IdDireccion = (int)item.IdDireccion,
                                        Nombre = item.EntornoDireccion,
                                        Calle = item.Calle,
                                        CodigoPais = item.CodigoPais,
                                        UsuarioId = item.IdUsuario
                                    }
                                };


                            result.Objects.Add(usuario);
                           


                        }
                        result.Correct = true;
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









    }
}
