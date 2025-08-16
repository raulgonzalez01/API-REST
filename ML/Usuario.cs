using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set;}
    public string Email { get; set;}
        public string Telefono  { get; set;}
        public string Password { get; set;}
        public string TaxId { get; set;}

        public DateTimeOffset FechaCreacion { get; set;}

        //public List<object> Usuarios { get; set; }

        public List<ML.Direccion> Direcciones { get; set; }
    }
}
