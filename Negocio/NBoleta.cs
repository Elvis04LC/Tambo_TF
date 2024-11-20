using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class NBoleta
    {
        DBoleta dBoleta = new DBoleta();
        public int Registrar(Boleta boleta)
        {
            return dBoleta.Registrar(boleta);
        }
        public String EliminarFisico(int boletaId)
        {
            return dBoleta.EliminarFisico(boletaId);
        }
        public String EliminarLogico(int boletaId)
        {
            return dBoleta.EliminarLogico(boletaId);
        }
        public int Modificar(Boleta boleta)
        {
            return dBoleta.Modificar(boleta);
        }
        public List<Boleta> ListarTodoFisico()
        {
            return dBoleta.ListarTodoFisico();
        }
        public List<Boleta> ListarTodoLogico()
        {
            return dBoleta.ListarTodoLogico();
        }
        public Boleta ObtenerBoleta(int boletaId)
        {
            return dBoleta.ObtenerBoleta(boletaId);
        }
        public List<Boleta> ListarPorVendedor(Trabajador trabajador)
        {
            return dBoleta.ListarPorVendedor(trabajador);
        }
    }
}
