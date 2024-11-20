using Datos;
using Datos.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class NProducto
    {
        DProducto dProducto = new DProducto();
        public int Registrar(Producto producto)
        {
            if (dProducto.NombreRepetido(producto.Nombre))
            {
                return -2;
            }
            return dProducto.Registrar(producto);
        }
        public String EliminarFisico(int productoId)
        {
            return dProducto.EliminarFisico(productoId);
        }
        public String EliminarLogico(int productoId)
        {
            return dProducto.EliminarLogico(productoId);
        }
        public int Modificar(Producto producto)
        {
            return dProducto.Modificar(producto);
        }
        public List<Producto> ListarTodoFisico()
        {
            return dProducto.ListarTodoFisico();
        }
        public List<Producto> ListarTodoLogico()
        {
            return dProducto.ListarTodoLogico();
        }
        public Producto ObtenerProductoPorId(int idProducto)
        {
            return dProducto.ObtenerProductoPorId(idProducto);
        }
        public int ActualizarCantidadProductoRegistrar(Producto producto, int cantidadNueva)
        {
            return dProducto.ActualizarCantidadProductoRegistrar(producto, cantidadNueva);
        }
        public int ActualizarCantidadProductoElmininar(Producto producto, int cantidadNueva)
        {
            return dProducto.ActualizarCantidadProductoElmininar(producto, cantidadNueva);
        }
        public List<CProductoStock> ListarProductoMenosStock(int sucursalId)
        {
            return dProducto.ListarProductoMenosStock(sucursalId);
        }
        public List<CProductoVendido> ListarProductoMasVendidos(int sucursalId)
        {
            return dProducto.ListarProductoMasVendidos(sucursalId);
        }
        public List<CCategoriaVendida> ListarCategoriasMasVendidas(int sucursalId)
        {
            return dProducto.ListarCategoriasMasVendidas(sucursalId);
        }
        public List<Producto> ListarPorCategoria(int categoriaId)
        {
            return dProducto.ListarPorCategoria(categoriaId);
        }
    }
}
