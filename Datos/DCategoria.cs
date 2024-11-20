using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class DCategoria
    {
        public int Registrar(Categoria categoria)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    context.Categoria.Add(categoria);
                    context.SaveChanges();
                    return categoria.idCategoria;
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
                    List<Categoria> categoriaTemp = context.Categoria.Where(a => a.Nombre == nombre).ToList();
                    return !(categoriaTemp.Count == 0);
                }
            }
            catch (Exception ex)
            {
                return true;
            }
        }
        public int Modificar(Categoria categoria)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    Categoria categoriaTemp = context.Categoria.Find(categoria.idCategoria);
                    categoriaTemp.Nombre = categoria.Nombre;
                    categoriaTemp.UsuarioModificadorId = categoria.UsuarioModificadorId;
                    categoriaTemp.FechaModificacion = categoria.FechaModificacion;
                    context.SaveChanges();
                    return categoriaTemp.idCategoria;
                }
            }
            catch (Exception ex)
            {
                return -3;
            }
        }

        //Eliminar fisico
        public String EliminarFisico(int categoriaId)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    Categoria categoriaTemp = context.Categoria.Find(categoriaId);
                    context.Categoria.Remove(categoriaTemp);
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
        public String EliminarLogico(int categoriaId)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    Categoria categoriaTemp = context.Categoria.Find(categoriaId);
                    categoriaTemp.Eliminado = true;
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
        public List<Categoria> ListarTodoFisico()
        {
            List<Categoria> categoriaes = new List<Categoria>();
            try
            {
                using (var context = new BDEFEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    categoriaes = context.Categoria.ToList();
                }
                return categoriaes;
            }
            catch (Exception ex)
            {
                return categoriaes;
            }
        }
        //Listar logico
        public List<Categoria> ListarTodoLogico()
        {
            List<Categoria> categoriaes = new List<Categoria>();
            try
            {
                using (var context = new BDEFEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    categoriaes = context.Categoria.Where(s => s.Eliminado == false).ToList();
                }
                return categoriaes;
            }
            catch (Exception ex)
            {
                return categoriaes;
            }
        }
    }
}
