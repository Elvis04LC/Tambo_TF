using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class NInventario
    {
        DInventario dInventario = new DInventario();
        public int Registrar(Inventario inventario)
        {
            return dInventario.Registrar(inventario);
        }
        public String EliminarFisico(int inventarioId)
        {
            return dInventario.EliminarFisico(inventarioId);
        }
        public int Modificar(Inventario inventario)
        {
            return dInventario.Modificar(inventario);
        }
        public List<Inventario> ListarTodoFisico()
        {
            return dInventario.ListarTodoFisico();
        }
        public Inventario ObtenerInventario(int inventarioId)
        {
            return dInventario.ObtenerInventario(inventarioId);
        }
        public int SucursalConInventario(int inventarioId)
        {
            return dInventario.SucursalConInventario(inventarioId);
        }
        public Inventario ObtenerSucursalInventario(int sucursalId)
        {
            return dInventario.ObtenerSucursalInventario(sucursalId);
        }
    }
}
