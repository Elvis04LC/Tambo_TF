using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Clases
{
    public class CProductoVendido
    {
        public CProductoVendido() { }
        public int idProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int NroVentas { get; set; }
        public decimal PrecioProducto { get; set; }
    }
}
