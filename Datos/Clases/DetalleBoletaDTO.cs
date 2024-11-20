using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Clases
{
    public class DetalleBoletaDTO
    {
        public int idDetalleBol { get; set; }
        public int idBoleta { get; set; }
        public int idProducto { get; set; }
        public string NombreProducto { get; set; } // NOMBREPRODUCTO
        public int CantidadProducto { get; set; }
        public decimal Subtotal { get; set; }
        public int UsuarioCrea { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
