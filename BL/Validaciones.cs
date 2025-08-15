using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BL
{
    public class Validaciones
    {
        public static bool ValidarRFC(string rfc)
        {
            string rfcPattern = @"^[A-ZÑ&]{3,4}\d{6}[A-Z0-9]{3}$";
            return Regex.IsMatch(rfc, rfcPattern, RegexOptions.IgnoreCase);
        }

        public static bool ValidarTelefono(string telefono)
        {
            if (string.IsNullOrEmpty(telefono))
                return false;

            string numeros = new string(telefono.Where(char.IsDigit).ToArray());

           
            return (numeros.Length == 10 || (numeros.Length == 11 && numeros.StartsWith("1")));
        }

    }
}
