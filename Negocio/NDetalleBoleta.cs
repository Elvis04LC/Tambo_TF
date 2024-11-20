using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class NDetalleBoleta
    {
        DDetalleBoleta dDetalleBoleta = new DDetalleBoleta();
        public String Registrar(DetalleBoleta detalleBoleta)
        {
            return dDetalleBoleta.Asignar(detalleBoleta);
        }
        public String EliminarFisico(int idDetalleBoleta)
        {
            return dDetalleBoleta.Desasignar(idDetalleBoleta);
        }
        public String Modificar(DetalleBoleta detalleBoleta)
        {
            return dDetalleBoleta.Modificar(detalleBoleta);
        }
        public List<DetalleBoleta> ListarTodoFisico(int boletaId)
        {
            return dDetalleBoleta.ListarTodoFisico(boletaId);
        }
        public decimal CalcularSubTotal(int idProducto, int cantidad)
        {
            return dDetalleBoleta.CalcularSubTotal(idProducto, cantidad);
        }
        public decimal CalcularTotal(Boleta boleta, int boletaId)
        {
            return dDetalleBoleta.CalcularTotal(boleta, boletaId);
        }
        public DetalleBoleta ObtenerPorId(int detalleBoletaId)
        {
            return dDetalleBoleta.ObtenerPorId(detalleBoletaId);
        }
        public int BoletaConProducto(int productoId, int boletaId)
        {
            return dDetalleBoleta.BoletaConProducto(productoId, boletaId);
        }
    }
}
