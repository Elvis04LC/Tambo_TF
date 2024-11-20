using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class DBoleta
    {
        public int Registrar(Boleta boleta)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    context.Boleta.Add(boleta);
                    context.SaveChanges();
                    return boleta.idBoleta;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        public int Modificar(Boleta boleta)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    Boleta boletaTemp = context.Boleta.Find(boleta.idBoleta);
                    boletaTemp.FechaEmision = boleta.FechaEmision;
                    boletaTemp.UsuarioModificadorId = boleta.UsuarioModificadorId;
                    boletaTemp.FechaModificacion = boleta.FechaModificacion;
                    context.SaveChanges();
                    return boletaTemp.idBoleta;
                }
            }
            catch (Exception ex)
            {
                return -3;
            }
        }

        //Eliminar fisico
        public String EliminarFisico(int boletaId)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    Boleta boletaTemp = context.Boleta.Find(boletaId);
                    context.Boleta.Remove(boletaTemp);
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
        public String EliminarLogico(int boletaId)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    Boleta boletaTemp = context.Boleta.Find(boletaId);
                    boletaTemp.Eliminado = true;
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
        public List<Boleta> ListarTodoFisico()
        {
            List<Boleta> boletas = new List<Boleta>();
            try
            {
                using (var context = new BDEFEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    boletas = context.Boleta.ToList();
                }
                return boletas;
            }
            catch (Exception ex)
            {
                return boletas;
            }
        }
        //Listar logico
        public List<Boleta> ListarTodoLogico()
        {
            List<Boleta> boletaes = new List<Boleta>();
            try
            {
                using (var context = new BDEFEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    boletaes = context.Boleta.Where(s => s.Eliminado == false).ToList();
                }
                return boletaes;
            }
            catch (Exception ex)
            {
                return boletaes;
            }
        }
        public Boleta ObtenerBoleta(int boletaId)
        {
            Boleta boleta = new Boleta();
            try
            {
                using (var context = new BDEFEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    boleta = context.Boleta.Find(boletaId);
                }
                return boleta;
            }
            catch (Exception ex)
            {
                return boleta;
            }
        }
        public List<Boleta> ListarPorVendedor(Trabajador trabajador)
        {
            List<Boleta> boletas = new List<Boleta>();
            try
            {
                using (var context = new BDEFEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    boletas = context.Boleta.Where(b => b.UsuarioCreadorId == trabajador.idTrabajador).ToList();
                }
                return boletas;
            }
            catch (Exception ex)
            {
                return boletas;
            }
        }
    }
}
