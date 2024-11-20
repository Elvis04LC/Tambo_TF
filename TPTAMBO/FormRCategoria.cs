using Datos;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPTAMBO
{
    public partial class FormRCategoria : Form
    {

        private NCategoria nCategoria = new NCategoria();
        private static FormRCategoria instancia = null;
        public static FormRCategoria Windows_Unique()
        {
            if (instancia == null)
            {
                instancia = new FormRCategoria();
                return instancia;
            }
            return instancia;
        }
        public FormRCategoria()
        {
            InitializeComponent();
            MostrarCategorias(nCategoria.ListarTodoFisico());
        }
        private void LimpiarCampos()
        {
            tbNombre.Clear();
        }
        private void MostrarCategorias(List<Categoria> categorias)
        {
            dgCategorias.DataSource = null;
            if (categorias.Count == 0)
            {
                return;
            }
            else
            {
                dgCategorias.DataSource = categorias;
                dgCategorias.Columns["Producto"].Visible = false;
            }
        }
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (tbNombre.Text.Trim() == "")
            {
                MessageBox.Show("Ingrese los campos requeridos");
                return;
            }

            Categoria categoria = new Categoria();
            categoria.Nombre = tbNombre.Text.Trim();
            categoria.Eliminado = false;
            categoria.UsuarioCreadorId = NTrabajador.trabajadorLogueado.idTrabajador;
            categoria.FechaCreacion = DateTime.Now;
            categoria.UsuarioModificadorId = NTrabajador.trabajadorLogueado.idTrabajador;
            categoria.FechaModificacion = DateTime.Now;

            try
            {               
                int registrado = nCategoria.Registrar(categoria);

                if (registrado == -2)
                {
                    MessageBox.Show("El nombre de la categoría ya existe.");
                    return;
                }

                if (registrado > 0)
                {
                    MessageBox.Show("Categoría registrada correctamente.");
                    LimpiarCampos();
                    MostrarCategorias(nCategoria.ListarTodoFisico());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar categoría: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void tbNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && e.KeyChar != ' ')
            {
                MessageBox.Show("Solo se permiten letras", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
            }
        }
        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgCategorias.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una categoria");
                return;
            }
            int categoriaId = int.Parse(dgCategorias.SelectedRows[0].Cells[0].Value.ToString());

            if (tbNombre.Text.Trim() == "")
            {
                MessageBox.Show("Ingrese los campos requeridos");
                return;
            }

            Categoria categoria = new Categoria();
            categoria.idCategoria = categoriaId;
            categoria.Nombre = tbNombre.Text.Trim();
            categoria.Eliminado = false;
            categoria.UsuarioModificadorId = NTrabajador.trabajadorLogueado.idTrabajador;
            categoria.FechaModificacion = DateTime.Now;

            try
            {
                int registrado = nCategoria.Registrar(categoria);
                if (registrado > 0)
                {
                    MessageBox.Show("Registrado correctamente");
                    LimpiarCampos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar categoría: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            MostrarCategorias(nCategoria.ListarTodoFisico());
        }
        private void FormRCategoria_Load(object sender, EventArgs e)
        {

        }
        private void btnEliminarFisico_Click(object sender, EventArgs e)
        {
            if (dgCategorias.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una categoria");
                return;
            }
            var confirmResult = MessageBox.Show("¿Está seguro de que desea eliminar esta categoría de forma permanente?",
                                        "Confirmar Eliminación Física",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Warning);

            if (confirmResult == DialogResult.No)
                return;

            int categoriaId = int.Parse(dgCategorias.SelectedRows[0].Cells[0].Value.ToString());
            string mensaje = nCategoria.EliminarFisico(categoriaId);
            MessageBox.Show(mensaje);

            MostrarCategorias(nCategoria.ListarTodoFisico());
        }
        private void btnEliminarLogicos_Click(object sender, EventArgs e)
        {
            if (dgCategorias.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una categoria");
                return;
            }
            var confirmResult = MessageBox.Show("¿Está seguro de que desea eliminar esta categoría de forma lógica?",
                                         "Confirmar Eliminación Lógica",
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);

            if (confirmResult == DialogResult.No)
                return;

            int categoriaId = int.Parse(dgCategorias.SelectedRows[0].Cells[0].Value.ToString());
            string mensaje = nCategoria.EliminarLogico(categoriaId);
            MessageBox.Show(mensaje);

            MostrarCategorias(nCategoria.ListarTodoFisico());
        }
        private void dgCategorias_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tbNombre.Text = dgCategorias.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();
        }



        private void btnSalir_Click_1(object sender, EventArgs e)
        {
            this.Hide();
        }
        private void FiltrarCategorias(string filtro)
        {
            var categorias = nCategoria.ListarTodoFisico()
                .Where(c => c.Nombre.IndexOf(filtro, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();

            MostrarCategorias(categorias);
        }
        private void tbBuscar_TextChanged(object sender, EventArgs e)
        {
            FiltrarCategorias(tbBuscar.Text);
        }
    }
}
