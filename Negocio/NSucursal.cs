using Datos;
using Datos.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class NSucursal
    {
        DSucursal dSucursal = new DSucursal();
        
        public int Registrar(Sucursal sucursal)
        {
            if (dSucursal.NombreRepetido(sucursal.Nombre))
            {
                return -2;
            }
            return dSucursal.Registrar(sucursal);
        }
        public String EliminarFisico(int sucursalId)
        {
            return dSucursal.EliminarFisico(sucursalId);
        }
        public String EliminarLogico(int sucursalId)
        {
            return dSucursal.EliminarLogico(sucursalId);
        }
        public int Modificar(Sucursal sucursal)
        {
            if (dSucursal.NombreRepetido(sucursal.Nombre))
            {
                return -2;
            }
            return dSucursal.Modificar(sucursal);
        }
        public List<Sucursal> ListarTodoFisico()
        {
            return dSucursal.ListarTodoFisico();
        }
        public List<Sucursal> ListarTodoLogico()
        {
            return dSucursal.ListarTodoLogico();
        }
        public Sucursal ObtenerSucursalPorId(int idSucursal)
        {
            return dSucursal.ObtenerSucursalPorId(idSucursal);
        }
        public List<CSucursalVenta> ListarSucursalesConMasVentas()
        {
            return dSucursal.ListarSucursalesConMasVentas();
        }
        public List<CSucursalVenta> ListarSucursalesConMasVentasPorFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            return dSucursal.ListarSucursalesConMasVentasPorFecha(fechaInicio, fechaFin);
        }
        public Sucursal ObtenerSucursalPorNombre(string nombre)
        {
            return dSucursal.ObtenerSucursalPorNombre(nombre);
        }
    }
}
