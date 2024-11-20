using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class DInventario
    {
        public int Registrar(Inventario inventario)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    context.Inventario.Add(inventario);
                    context.SaveChanges();
                    return inventario.idInventario;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        public int Modificar(Inventario inventario)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    Inventario inventarioTemp = context.Inventario.Find(inventario.idInventario);
                    inventarioTemp.StockTotal = inventario.StockTotal;
                    inventarioTemp.UsuarioModificadorId = inventario.UsuarioModificadorId;
                    inventarioTemp.FechaModificacion = inventario.FechaModificacion;
                    context.SaveChanges();
                    return inventarioTemp.idInventario;
                }
            }
            catch (Exception ex)
            {
                return -3;
            }
        }

        //Eliminar fisico
        public String EliminarFisico(int inventarioId)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    Inventario inventarioTemp = context.Inventario.Find(inventarioId);
                    context.Inventario.Remove(inventarioTemp);
                    context.SaveChanges();
                }
                return "Eliminado fisico correctamente";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        //Listar fisico
        public List<Inventario> ListarTodoFisico()
        {
            List<Inventario> inventarios = new List<Inventario>();
            try
            {
                using (var context = new BDEFEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    inventarios = context.Inventario.ToList();
                }
                return inventarios;
            }
            catch (Exception ex)
            {
                return inventarios;
            }
        }
        public Inventario ObtenerInventario(int inventarioId)
        {
            Inventario inventario = new Inventario();
            try
            {
                using (var context = new BDEFEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    inventario = context.Inventario.Find(inventarioId);
                }
                return inventario;
            }
            catch (Exception ex)
            {
                return inventario;
            }
        }
        public int SucursalConInventario(int sucursalId)
        {
            List<Inventario> inventarios = new List<Inventario>();
            int val = -1;
            try
            {
                using (var context = new BDEFEntities())
                {
                    inventarios = context.Inventario.Where(s => s.idSucursal.Equals(sucursalId)).ToList();
                    if (inventarios.Count > 0)
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
        public Inventario ObtenerSucursalInventario(int sucursalId)
        {
            Inventario inventario = new Inventario();
            try
            {
                using (var context = new BDEFEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    inventario = context.Inventario.FirstOrDefault(i => i.idSucursal == sucursalId);
                }
                return inventario;
            }
            catch (Exception ex)
            {
                return inventario;
            }
        }
    }
}
