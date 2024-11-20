using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class NProductoInventario
    {
        DProductoInventario dProductoInventario = new DProductoInventario();
        public String Registrar(ProductoInventario productoInventario)
        {
            return dProductoInventario.Asignar(productoInventario);
        }
        public String EliminarFisico(int idProductoInventario)
        {
            return dProductoInventario.Desasignar(idProductoInventario);
        }
        public String Modificar(ProductoInventario productoInventario)
        {
            return dProductoInventario.Modificar(productoInventario);
        }
        public List<ProductoInventario> ListarTodoFisico(int boletaId)
        {
            return dProductoInventario.ListarTodoFisico(boletaId);
        }
        public int CalcularStockTotal(Inventario inventario, int inventarioId)
        {
            return dProductoInventario.CalcularStockTotal(inventario, inventarioId);
        }
        public ProductoInventario ObtenerPorId(int productoInventarioID)
        {
            return dProductoInventario.ObtenerPorId(productoInventarioID);
        }
        public ProductoInventario VerificarExistenciaProductoInventario(Producto producto, Sucursal sucursal)
        {
            return dProductoInventario.VerificarExistenciaProductoInventario(producto, sucursal);
        }
        public int ActualizarStockRegistrar(ProductoInventario productoInventario, int cantidadNueva)
        {
            return dProductoInventario.ActualizarStockRegistrar(productoInventario, cantidadNueva);
        }
        public int ActualizarStockEliminar(ProductoInventario productoInventario, int cantidadNueva)
        {
            return dProductoInventario.ActualizarStockEliminar(productoInventario, cantidadNueva);
        }
        public int ProductoConInventario(int productoId, int inventarioId)
        {
            return dProductoInventario.ProductoConInventario(productoId, inventarioId);
        }
        public List<ProductoInventario> ListarProductosBajoStock(int inventarioId)
        {
            return dProductoInventario.ListarProductosBajoStock(inventarioId);
        }
        public int CalcularCantidadTotalProducto(int productoId)
        {
            return dProductoInventario.CalcularCantidadTotalProducto(productoId);
        }
    }
}
