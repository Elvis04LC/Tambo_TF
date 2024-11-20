using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Datos
{
    public class DTrabajador
    {
        public int Registrar(Trabajador trabajador)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    context.Trabajador.Add(trabajador);
                    context.SaveChanges();
                    return trabajador.idTrabajador;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        public Trabajador IniciarSesion(string nombreUsuario, string contrasenia)
        {
            Trabajador trabajadorTemp = null;
            using (var context = new BDEFEntities())
            {
                //var trabajadorTemp = context.Trabajador.FirstOrDefault(a=>a.NombreUsuario.Equals(nombreUsuario)&&a.Contrasenia.Equals(contrasenia)&&a.Eliminado==false);
              var query = from b in context.Trabajador
                          where b.NombreUsuario == nombreUsuario
                                && b.Contrasenia == contrasenia
                                && b.Eliminado == false
                          select b;
                trabajadorTemp = query.FirstOrDefault();
            }
            return trabajadorTemp;
        }
        public bool NombreUsuarioRepetido(String nombreUsuario)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    List<Trabajador> trabajadorTemp = context.Trabajador.Where(s => s.NombreUsuario == nombreUsuario).ToList();
                    return !(trabajadorTemp.Count == 0);
                }
            }
            catch (Exception ex)
            {
                return true;
            }
        }
    
        public int Modificar(Trabajador trabajador)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    // Buscar el trabajador en la base de datos
                    Trabajador trabajadorTemp = context.Trabajador.Find(trabajador.idTrabajador);

                    // Verificar si el trabajador existe
                    if (trabajadorTemp == null)
                    {
                        return -1; // Registro no encontrado
                    }

                   
                    trabajadorTemp.Nombre = trabajador.Nombre;
                    trabajadorTemp.Apellido = trabajador.Apellido;
                    trabajadorTemp.NombreUsuario = trabajador.NombreUsuario;
                    trabajadorTemp.CorreoElectronico = trabajador.CorreoElectronico;
                    trabajadorTemp.Contrasenia = trabajador.Contrasenia;
                    trabajadorTemp.UsuarioModificacdorId = trabajador.UsuarioModificacdorId;
                    trabajadorTemp.FechaModificacion = trabajador.FechaModificacion;

                    
                    context.SaveChanges();
                    return trabajadorTemp.idTrabajador; 
                }
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error: {ex.Message}");
                return -3; // Error genérico
            }
        }
        public String EliminarFisico(int trabajdorID)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    Trabajador trabajadorTemp = context.Trabajador.Find(trabajdorID);
                    context.Trabajador.Remove(trabajadorTemp);
                    context.SaveChanges();
                }
                return "Eliminado fisico correctamente";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public String EliminarLogico(int trabajdorID)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    Trabajador trabajadorTemp = context.Trabajador.Find(trabajdorID);
                    trabajadorTemp.Eliminado = true;
                    context.SaveChanges();
                }
                return "Eliminado logico correctamente";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public List<Trabajador> ListarTodoFisico()
        {
            List<Trabajador> trabajadores = new List<Trabajador>();
            try
            {
                using (var context = new BDEFEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    trabajadores = context.Trabajador.ToList();
                }
                return trabajadores;
            }
            catch (Exception ex)
            {
                return trabajadores;
            }
        }
        public List<Trabajador> ListarTodoLogico()
        {
            List<Trabajador> trabajadores = new List<Trabajador>();
            try
            {
                using (var context = new BDEFEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    trabajadores = context.Trabajador.Where(s => s.Eliminado == false).ToList();
                }
                return trabajadores;
            }
            catch (Exception ex)
            {
                return trabajadores;
            }
        }
        public Trabajador ObtenerTrabajadorPorNombreUsuario(String nombreUsuario)
        {
            Trabajador trabajador = new Trabajador();
            try
            {
                using (var context = new BDEFEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    trabajador = context.Trabajador.FirstOrDefault(s => s.NombreUsuario.Contains(nombreUsuario));
                }
                return trabajador;
            }
            catch (Exception ex)
            {
                return trabajador;
            }
        }
    }
}
