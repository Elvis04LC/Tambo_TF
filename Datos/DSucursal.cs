using Datos.Clases;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class DSucursal
    {
       
        public int Registrar(Sucursal sucursal)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    context.Sucursal.Add(sucursal);
                    context.SaveChanges();
                    return sucursal.idSucursal;
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
                    List<Sucursal> sucursalTemp = context.Sucursal.Where(a => a.Nombre == nombre).ToList();
                    return !(sucursalTemp.Count == 0);
                }
            }
            catch (Exception ex)
            {
                return true;
            }
        }
        public int Modificar(Sucursal sucursal)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    Sucursal sucursalTemp = context.Sucursal.Find(sucursal.idSucursal);
                    if (sucursalTemp == null)
                    {
                        return -1; // Registro no encontrado
                    }

                    sucursalTemp.Nombre = sucursal.Nombre;
                    sucursalTemp.Distrito = sucursal.Distrito;
                    sucursalTemp.UsuarioModificacdorId = sucursal.UsuarioModificacdorId;
                    sucursalTemp.FechaModificacion = sucursal.FechaModificacion;
                    context.SaveChanges();
                    return sucursalTemp.idSucursal;
                }
            }
            catch (Exception ex)
            {
                return -3;
            }
        }

        //Eliminar fisico
        public String EliminarFisico(int sucursalId)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    Sucursal sucursalTemp = context.Sucursal.Find(sucursalId);
                    context.Sucursal.Remove(sucursalTemp);
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
        public String EliminarLogico(int sucursalId)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    Sucursal sucursalTemp = context.Sucursal.Find(sucursalId);
                    sucursalTemp.Eliminado = true;
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
        public List<Sucursal> ListarTodoFisico()
        {
            List<Sucursal> sucursales = new List<Sucursal>();
            try
            {
                using (var context = new BDEFEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    sucursales = context.Sucursal.ToList();
                }
                return sucursales;
            }
            catch (Exception ex)
            {
                return sucursales;
            }
        }
        //Listar logico
        public List<Sucursal> ListarTodoLogico()
        {
            List<Sucursal> sucursales = new List<Sucursal>();
            try
            {
                using (var context = new BDEFEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    sucursales = context.Sucursal.Where(s => s.Eliminado == false).ToList();
                }
                return sucursales;
            }
            catch (Exception ex)
            {
                return sucursales;
            }
        }
        public Sucursal ObtenerSucursalPorId(int idSucursal)
        {
            Sucursal sucursal = new Sucursal();
            try
            {
                using (var context = new BDEFEntities())
                {
                    sucursal = context.Sucursal.FirstOrDefault(s => s.idSucursal == idSucursal);
                }
                return sucursal;
            }
            catch (Exception ex)
            {
                return sucursal;
            }
        }
        public List<CSucursalVenta> ListarSucursalesConMasVentas()
        {
            List<CSucursalVenta> sucursalVentas = new List<CSucursalVenta>();
            try
            {
                using (var context = new BDEFEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    sucursalVentas = context.Database.SqlQuery<CSucursalVenta>("ObtenerSucursalesConMasVentas").ToList();
                }
                return sucursalVentas;
            }
            catch (Exception ex)
            {
                return sucursalVentas;
            }
        }
        public List<CSucursalVenta> ListarSucursalesConMasVentasPorFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            List<CSucursalVenta> sucursalVentas = new List<CSucursalVenta>();
            try
            {
                using (var context = new BDEFEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;

                    // Llamar al procedimiento almacenado
                    sucursalVentas = context.Database.SqlQuery<CSucursalVenta>(
                        "EXEC ObtenerSucursalesConMasVentasPorFecha @FechaInicio, @FechaFin",
                        new SqlParameter("@FechaInicio", fechaInicio),
                        new SqlParameter("@FechaFin", fechaFin)
                    ).ToList();
                }

                return sucursalVentas;
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return sucursalVentas;
            }
        }
        public Sucursal ObtenerSucursalPorNombre(string nombre)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    return context.Sucursal.FirstOrDefault(s => s.Nombre == nombre);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
