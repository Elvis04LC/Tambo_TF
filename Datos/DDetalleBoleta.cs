using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class DDetalleBoleta
    {
        DProducto dProducto = new DProducto();
        public String Asignar(DetalleBoleta detalleBoleta)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    context.DetalleBoleta.Add(detalleBoleta);
                    context.SaveChanges();
                }
                return "Asignado correctamente";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public String Modificar(DetalleBoleta detalleBoleta)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    DetalleBoleta detalleBoletaTemp = context.DetalleBoleta.Find(detalleBoleta.idDetalleBoleta);
                    detalleBoletaTemp.CantidadProducto = detalleBoleta.CantidadProducto;
                    detalleBoletaTemp.Subtotal = detalleBoleta.Subtotal;
                    detalleBoletaTemp.idBoleta = detalleBoleta.idBoleta;
                    detalleBoletaTemp.idProducto = detalleBoleta.idProducto;
                    detalleBoletaTemp.UsuarioModificadorId = detalleBoleta.UsuarioModificadorId;
                    detalleBoletaTemp.FechaModificacion = detalleBoleta.FechaModificacion;
                    context.SaveChanges();
                    return "Modificado correctamente";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        //Eliminar fisico
        public String Desasignar(int idDetalleBoleta)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    DetalleBoleta detalleBoletaTemp = context.DetalleBoleta.Find(idDetalleBoleta);
                    context.DetalleBoleta.Remove(detalleBoletaTemp);
                    context.SaveChanges();
                }
                return "Desasignado correctamente";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        //Listar fisico
        public List<DetalleBoleta> ListarTodoFisico(int boletaId)
        {
            List<DetalleBoleta> detalleBoletaes = new List<DetalleBoleta>();
            try
            {
                using (var context = new BDEFEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    detalleBoletaes = context.DetalleBoleta.Where(d => d.idBoleta.Equals(boletaId)).ToList();
                }
                return detalleBoletaes;
            }
            catch (Exception ex)
            {
                return detalleBoletaes;
            }
        }
        public decimal CalcularSubTotal(int idProducto, int cantidad)
        {
            decimal total = 0;
            decimal precio;
            try
            {
                using (var context = new BDEFEntities())
                {
                    Producto productoTemp = dProducto.ObtenerProductoPorId(idProducto);
                    precio = productoTemp.PrecioProducto;
                    total = precio * cantidad;
                    return total;
                }
            }
            catch (Exception ex)
            {
                return total;
            }
        }
        public decimal CalcularTotal(Boleta boleta, int boletaId)
        {
            List<DetalleBoleta> detalleBoletaes = new List<DetalleBoleta>();
            decimal total = 0;
            try
            {
                using (var context = new BDEFEntities())
                {
                    Boleta boletaTemp = context.Boleta.Find(boleta.idBoleta);
                    boletaTemp.Total = boleta.Total;

                    detalleBoletaes = context.DetalleBoleta.Where(d => d.idBoleta.Equals(boletaId)).ToList();
                    total = detalleBoletaes.Sum(d => d.Subtotal);
                    boletaTemp.Total = total;
                    context.SaveChanges();
                    return total;
                }
            }
            catch (Exception ex)
            {
                return total;
            }
        }
        public DetalleBoleta ObtenerPorId(int detalleBoletaId)
        {
            DetalleBoleta detalleBoleta = new DetalleBoleta();
            try
            {
                using (var context = new BDEFEntities())
                {
                    detalleBoleta = context.DetalleBoleta.Find(detalleBoletaId);
                }
                return detalleBoleta;
            }
            catch (Exception ex)
            {
                return detalleBoleta;
            }
        }
        public int BoletaConProducto(int productoId, int boletaId)
        {
            List<DetalleBoleta> detalleBoletas = new List<DetalleBoleta>();
            int val = -1;
            try
            {
                using (var context = new BDEFEntities())
                {
                    detalleBoletas = context.DetalleBoleta.Where(s => s.idProducto.Equals(productoId) && s.idBoleta.Equals(boletaId)).ToList();
                    if (detalleBoletas.Count > 0)
                    {
                        val = 0;
                    }
                    else
                    {
                        val = 1;
                    }
                }
                return val;
            }
            catch (Exception ex)
            {
                return val;
            }
        }
    }
}
