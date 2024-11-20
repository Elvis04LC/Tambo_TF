using Datos;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class NTrabajador
    {
        DTrabajador dTrabajador = new DTrabajador();
        public static Trabajador trabajadorLogueado { get; set; }
        public int Registrar(Trabajador trabajador)
        {
            if (dTrabajador.NombreUsuarioRepetido(trabajador.Nombre))
            {
                return -2;
            }
            return dTrabajador.Registrar(trabajador);
        }

        public Trabajador IniciarSesion(String user, String password)
        {
            trabajadorLogueado = dTrabajador.IniciarSesion(user, password);
            return trabajadorLogueado;
        }
        public String EliminarFisico(int vendedorId)
        {
            return dTrabajador.EliminarFisico(vendedorId);
        }
        public String EliminarLogico(int vendedorId)
        {
            return dTrabajador.EliminarLogico(vendedorId);
        }
        public int Modificar(Trabajador vendedor)
        {
            if (dTrabajador.NombreUsuarioRepetido(vendedor.Nombre))
            {
                return -2;
            }
            return dTrabajador.Modificar(vendedor);

        }
        public List<Trabajador> ListarTodoFisico()
        {
            return dTrabajador.ListarTodoFisico();
        }
        public List<Trabajador> ListarTodoLogico()
        {
            return dTrabajador.ListarTodoLogico();

        }
        public Trabajador ObtenerTrabajadorPorNombreUsuario(String nombreUsuario)
        {
            return dTrabajador.ObtenerTrabajadorPorNombreUsuario(nombreUsuario);
        }
    }
 
}
