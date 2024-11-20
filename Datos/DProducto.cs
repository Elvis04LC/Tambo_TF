using Datos.Clases;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class DProducto
    {
        public int Registrar(Producto producto)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    context.Producto.Add(producto);
                    context.SaveChanges();
                    return producto.idProducto;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        public bool NombreRepetido(String nombre)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    List<Producto> productosTemp = context.Producto.Where(s => s.Nombre == nombre).ToList();
                    return !(productosTemp.Count == 0);
                }
            }
            catch (Exception ex)
            {
                return true;
            }
        }
        public int Modificar(Producto producto)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    Producto productoTemp = context.Producto.Find(producto.idProducto);
                    productoTemp.Nombre = producto.Nombre;
                    productoTemp.Descripcion = producto.Descripcion;
                    productoTemp.CantidadTotal = producto.CantidadTotal;
                    productoTemp.PrecioProducto = producto.PrecioProducto;
                    productoTemp.idCategoria = producto.idCategoria;
                    productoTemp.UsuarioModificadorId = producto.UsuarioModificadorId;
                    productoTemp.FechaModificacion = producto.FechaModificacion;
                    context.SaveChanges();
                    return productoTemp.idProducto;
                }
            }
            catch (Exception ex)
            {
                return -3;
            }
        }

        //Eliminar fisico
        public String EliminarFisico(int productoId)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    Producto productoTemp = context.Producto.Find(productoId);
                    context.Producto.Remove(productoTemp);
                    context.SaveChanges();
                }
                return "Eliminado fisico correctamente";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        //Eliminar logico
        public String EliminarLogico(int productoId)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    Producto productoTemp = context.Producto.Find(productoId);
                    productoTemp.Eliminado = true;
                    context.SaveChanges();
                }
                return "Eliminado logico correctamente";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        //Listar fisico
        public List<Producto> ListarTodoFisico()
        {
            List<Producto> productoes = new List<Producto>();
            try
            {
                using (var context = new BDEFEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    productoes = context.Producto.ToList();
                }
                return productoes;
            }
            catch (Exception ex)
            {
                return productoes;
            }
        }
        //Listar logico
        public List<Producto> ListarTodoLogico()
        {
            List<Producto> productoes = new List<Producto>();
            try
            {
                using (var context = new BDEFEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    productoes = context.Producto.Where(s => s.Eliminado == false).ToList();
                }
                return productoes;
            }
            catch (Exception ex)
            {
                return productoes;
            }
        }
        public Producto ObtenerProductoPorId(int idProducto)
        {
            Producto producto = new Producto();
            try
            {
                using (var context = new BDEFEntities())
                {
                    producto = context.Producto.FirstOrDefault(s => s.idProducto == idProducto);
                }
                return producto;
            }
            catch (Exception ex)
            {
                return producto;
            }
        }
        public int ActualizarCantidadProductoRegistrar(Producto producto, int cantidadNueva)
        {
            int val = -1;
            try
            {
                using (var context = new BDEFEntities())
                {
                    Producto productoTemp = context.Producto.Find(producto.idProducto);
                    productoTemp.CantidadTotal = producto.CantidadTotal;
                    if (productoTemp == null)
                    {
                        val = -1;
                    }
                    else if (producto.CantidadTotal < cantidadNueva)
                    {
                        val = 0;
                    }
                    else
                    {
                        productoTemp.CantidadTotal = productoTemp.CantidadTotal - cantidadNueva;
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
        public int ActualizarCantidadProductoElmininar(Producto producto, int cantidadNueva)
        {
            int val = -1;
            try
            {
                using (var context = new BDEFEntities())
                {
                    Producto productoTemp = context.Producto.Find(producto.idProducto);
                    productoTemp.CantidadTotal = producto.CantidadTotal;
                    productoTemp.CantidadTotal = productoTemp.CantidadTotal + cantidadNueva;
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
        public List<CProductoStock> ListarProductoMenosStock(int sucursalId)
        {
            List<CProductoStock> cProductoStocks = new List<CProductoStock>();
            try
            {
                using (var context = new BDEFEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    cProductoStocks = context.Database.SqlQuery<CProductoStock>("ObtenerProductosConMenosStock @idSucursal", new SqlParameter("@idSucursal", sucursalId)
                    ).ToList();
                }
                return cProductoStocks;
            }
            catch (Exception ex)
            {
                return cProductoStocks;
            }
        }
        public List<CProductoVendido> ListarProductoMasVendidos(int sucursalId)
        {
            List<CProductoVendido> cProductoVendidos = new List<CProductoVendido>();
            try
            {
                using (var context = new BDEFEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    cProductoVendidos = context.Database.SqlQuery<CProductoVendido>("ObtenerProductosMasVendidos @idSucursal", new SqlParameter("@idSucursal", sucursalId)
                    ).ToList();
                }
                return cProductoVendidos;
            }
            catch (Exception ex)
            {
                return cProductoVendidos;
            }
        }
        public List<CCategoriaVendida> ListarCategoriasMasVendidas(int sucursalId)
        {
            List<CCategoriaVendida> cCategoriaVendidas = new List<CCategoriaVendida>();
            try
            {
                using (var context = new BDEFEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    cCategoriaVendidas = context.Database.SqlQuery<CCategoriaVendida>("ObtenerCategoriasMasVendidos @idSucursal", new SqlParameter("@idSucursal", sucursalId)
                    ).ToList();
                }
                return cCategoriaVendidas;
            }
            catch (Exception ex)
            {
                return cCategoriaVendidas;
            }
        }
        public List<Producto> ListarPorCategoria(int categoriaId)
        {
            List<Producto> productos = new List<Producto>();
            try
            {
                using (var context = new BDEFEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;

                    // Filtrar productos por idCategoria
                    productos = context.Producto
                        .Where(p => p.idCategoria == categoriaId && p.Eliminado == false)
                        .ToList();
                }
                return productos;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return productos; // Devuelve una lista vacía en caso de error
            }
        }
    }
}
