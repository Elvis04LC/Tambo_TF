using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class DProductoInventario
    {
        DProducto dProducto = new DProducto();
        public String Asignar(ProductoInventario productoInventario)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    context.ProductoInventario.Add(productoInventario);
                    context.SaveChanges();
                }
                return "Asignado correctamente";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public String Modificar(ProductoInventario productoInventario)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    ProductoInventario productoInventarioTemp = context.ProductoInventario.Find(productoInventario.idProductoInventario);
                    productoInventarioTemp.Stock = productoInventario.Stock;
                    productoInventarioTemp.idInventario = productoInventario.idInventario;
                    productoInventarioTemp.idProducto = productoInventario.idProducto;
                    productoInventarioTemp.UsuarioModificadorId = productoInventario.UsuarioModificadorId;
                    productoInventarioTemp.FechaModificacion = productoInventario.FechaModificacion;
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
        public String Desasignar(int idProductoInventario)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    ProductoInventario productoInventarioTemp = context.ProductoInventario.Find(idProductoInventario);
                    context.ProductoInventario.Remove(productoInventarioTemp);
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
        public List<ProductoInventario> ListarTodoFisico(int inventarioId)
        {
            List<ProductoInventario> productoInventarioes = new List<ProductoInventario>();
            try
            {
                using (var context = new BDEFEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    productoInventarioes = context.ProductoInventario.Where(d => d.idInventario.Equals(inventarioId)).ToList();
                }
                return productoInventarioes;
            }
            catch (Exception ex)
            {
                return productoInventarioes;
            }
        }
        public int CalcularStockTotal(Inventario inventario, int inventarioId)
        {
            List<ProductoInventario> productoInventarioes = new List<ProductoInventario>();
            int total = 0;
            try
            {
                using (var context = new BDEFEntities())
                {
                    Inventario inventarioTemp = context.Inventario.Find(inventario.idInventario);
                    inventarioTemp.StockTotal = inventario.StockTotal;

                    productoInventarioes = context.ProductoInventario.Where(d => d.idInventario.Equals(inventarioId)).ToList();
                    total = productoInventarioes.Sum(s => s.Stock);
                    inventarioTemp.StockTotal = total;
                    context.SaveChanges();
                    return total;
                }
            }
            catch (Exception ex)
            {
                return total;
            }
        }
        public ProductoInventario ObtenerPorId(int productoInventarioID)
        {
            ProductoInventario productoInventarioes = new ProductoInventario();
            try
            {
                using (var context = new BDEFEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    productoInventarioes = context.ProductoInventario.Find(productoInventarioID);
                }
                return productoInventarioes;
            }
            catch (Exception ex)
            {
                return productoInventarioes;
            }
        }
        public ProductoInventario VerificarExistenciaProductoInventario(Producto producto, Sucursal sucursal)
        {
            ProductoInventario productoInventario = new ProductoInventario();
            int val = -1;
            try
            {
                using (var context = new BDEFEntities())
                {
                    Sucursal sucursalTemp = context.Sucursal.Find(sucursal.idSucursal);
                    Producto productoTemp = context.Producto.Find(producto.idProducto);

                    int idSucursal = sucursalTemp.idSucursal;
                    int idProducto = productoTemp.idProducto;

                    Inventario inventario = context.Inventario.FirstOrDefault(s => s.idSucursal == idSucursal);
                    productoInventario = context.ProductoInventario.FirstOrDefault(p => p.idInventario == inventario.idInventario && p.idProducto == idProducto);
                }
                return productoInventario;
            }
            catch (Exception ex)
            {
                return productoInventario;
            }
        }
        public int ActualizarStockRegistrar(ProductoInventario productoInventario, int cantidadNueva)
        {
            int val = -1;
            try
            {
                using (var context = new BDEFEntities())
                {
                    ProductoInventario productoInventarioTemp = context.ProductoInventario.Find(productoInventario.idProductoInventario);
                    productoInventarioTemp.Stock = productoInventario.Stock;
                    if (productoInventario.Stock < cantidadNueva)
                    {
                        val = 0;
                    }
                    else
                    {
                        productoInventarioTemp.Stock = productoInventarioTemp.Stock - cantidadNueva;
                        context.SaveChanges();
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
        public int ActualizarStockEliminar(ProductoInventario productoInventario, int cantidadNueva)
        {
            int val = -1;
            try
            {
                using (var context = new BDEFEntities())
                {
                    ProductoInventario productoInventarioTemp = context.ProductoInventario.Find(productoInventario.idProductoInventario);
                    productoInventarioTemp.Stock = productoInventario.Stock;
                    productoInventarioTemp.Stock = productoInventarioTemp.Stock + cantidadNueva;
                    context.SaveChanges();
                    val = 1;
                }
                return val;
            }
            catch (Exception ex)
            {
                return val;
            }
        }
        public int ProductoConInventario(int productoId, int inventarioId)
        {
            List<ProductoInventario> productoInventarios = new List<ProductoInventario>();
            int val = -1;
            try
            {
                using (var context = new BDEFEntities())
                {
                    productoInventarios = context.ProductoInventario.Where(s => s.idProducto.Equals(productoId) && s.idInventario.Equals(inventarioId)).ToList();
                    if (productoInventarios.Count > 0)
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
        public List<ProductoInventario> ListarProductosBajoStock(int inventarioId)
        {
            List<ProductoInventario> productoInventarioes = new List<ProductoInventario>();
            try
            {
                using (var context = new BDEFEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    productoInventarioes = context.ProductoInventario.Where(d => d.idInventario.Equals(inventarioId) && d.Stock < 10).ToList();
                }
                return productoInventarioes;
            }
            catch (Exception ex)
            {
                return productoInventarioes;
            }
        }
        public int CalcularCantidadTotalProducto(int idProducto)
        {
            int cantidadTotal = 0;

            try
            {
                using (var context = new BDEFEntities())
                {
                    cantidadTotal = context.ProductoInventario
                                           .Where(pi => pi.idProducto == idProducto)
                                           .Sum(pi => pi.Stock);
                }
            }
            catch (Exception ex)
            {
         
                cantidadTotal = 0;
            }

            return cantidadTotal;
        }
    }
}
