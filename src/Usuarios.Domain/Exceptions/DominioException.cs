using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usuarios.Domain.Exceptions
{
    public class DominioException : Exception
    {
        public DominioException(string mensaje) : base(mensaje)
        {
        }
    }
}
