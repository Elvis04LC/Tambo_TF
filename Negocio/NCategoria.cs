using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class NCategoria
    {
        DCategoria dCategoria = new DCategoria();
        public int Registrar(Categoria categoria)
        {
            if (dCategoria.NombreRepetido(categoria.Nombre))
            {
                return -2;
            }
            return dCategoria.Registrar(categoria);
        }
        public String EliminarFisico(int categoriaId)
        {
            return dCategoria.EliminarFisico(categoriaId);
        }
        public String EliminarLogico(int categoriaId)
        {
            return dCategoria.EliminarLogico(categoriaId);
        }
        public int Modificar(Categoria categoria)
        {
            if (dCategoria.NombreRepetido(categoria.Nombre))
            {
                return -2;
            }
            return dCategoria.Modificar(categoria);
        }
        public List<Categoria> ListarTodoFisico()
        {
            return dCategoria.ListarTodoFisico();
        }
        public List<Categoria> ListarTodoLogico()
        {
            return dCategoria.ListarTodoLogico();
        }
    }
}
