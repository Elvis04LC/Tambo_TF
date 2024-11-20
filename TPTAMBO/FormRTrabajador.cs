
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
    public partial class FormRTrabajador : Form
    {
        private NTrabajador nTrabajador = new NTrabajador();
        private static FormRTrabajador instancia = null;
        public static FormRTrabajador Windows_Unique()
        {
            if (instancia == null)
            {
                instancia = new FormRTrabajador();
                return instancia;
            }
            return instancia;
        }
        public FormRTrabajador()
        {
            InitializeComponent();
            MostrarTrabajadores(nTrabajador.ListarTodoFisico());
            ConfigurarAcceso();
        }
        private void ConfigurarAcceso()
        {
            if (NTrabajador.trabajadorLogueado.NombreUsuario != "admin")
            {
                btnRegistrar.Enabled = false;
                btnEliminarLogico.Enabled = false;
                btnEliminarFisico.Enabled = false;
                btnModificar.Enabled = false;

                btnRegistrar.BackColor = Color.Gray;
                btnEliminarLogico.BackColor = Color.Gray;
                btnEliminarFisico.BackColor = Color.Gray;
                btnModificar.BackColor = Color.Gray;

                btnRegistrar.ForeColor = Color.White;
                btnEliminarLogico.ForeColor = Color.White;
                btnEliminarFisico.ForeColor = Color.White;
                btnModificar.ForeColor = Color.White;

                MessageBox.Show("Solo tiene permiso para visualizar la información.", "Acceso Limitado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void MostrarTrabajadores(List<Trabajador> trabajadores)
        {
            dgTrabajadores.DataSource = null;
            if (trabajadores.Count == 0)
            {
                return;
            }
            else
            {
                dgTrabajadores.DataSource = trabajadores;
            }
        }
        private bool EsCorreoValido(string correo)
        {
            string patron = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return System.Text.RegularExpressions.Regex.IsMatch(correo, patron);
        }
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (tbNombre.Text == "" || tbApellido.Text == "" || tbCorreo.Text == "" || tbNombreUsuario.Text == "" || tbPassword.Text == "")
            {
                MessageBox.Show("Ingrese los campos requeridos");
                return;
            }
            if (!EsCorreoValido(tbCorreo.Text))
            {
                MessageBox.Show("El correo electrónico no tiene un formato válido");
                return;
            }

            Trabajador trabajador = new Trabajador();
            trabajador.Nombre = tbNombre.Text;
            trabajador.Apellido = tbApellido.Text;
            trabajador.CorreoElectronico = tbCorreo.Text;
            trabajador.NombreUsuario = tbNombreUsuario.Text;
            trabajador.Contrasenia = tbPassword.Text;
            trabajador.UsuarioCreadorId = NTrabajador.trabajadorLogueado.idTrabajador;
            trabajador.FechaCreacion = DateTime.Now;
            trabajador.UsuarioModificacdorId = NTrabajador.trabajadorLogueado.idTrabajador;
            trabajador.FechaModificacion = DateTime.Now;

            try
            {
                int resultado = nTrabajador.Registrar(trabajador);

                if (resultado == -2)
                {
                    MessageBox.Show("El nombre de usuario ya existe.");
                    return;
                }

                if (resultado > 0)
                {
                    MessageBox.Show("Trabajador registrado correctamente.");
                    MostrarTrabajadores(nTrabajador.ListarTodoFisico());
                }
                else
                {
                    MessageBox.Show("No se pudo registrar el trabajador.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inesperado: {ex.Message}");
            }
        }
        private void btnEliminarLogico_Click(object sender, EventArgs e)
        {
            if (dgTrabajadores.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un trabajador");
                return;
            }
            var confirmResult = MessageBox.Show("¿Está seguro de que desea eliminar este trabajador de forma lógica?",
                                        "Confirmar Eliminación Lógica",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question);

            if (confirmResult == DialogResult.No)
            {
                return;
            }

            int idTrabajador = int.Parse(dgTrabajadores.SelectedRows[0].Cells[0].Value.ToString());
            String mensaje = nTrabajador.EliminarLogico(idTrabajador);
            MessageBox.Show(mensaje);
            MostrarTrabajadores(nTrabajador.ListarTodoFisico());
        }
        private void btnEliminarFisico_Click(object sender, EventArgs e)
        {
            if (dgTrabajadores.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un Trabajador");
                return;
            }
            var confirmResult = MessageBox.Show("¿Está seguro de que desea eliminar este trabajador de forma permanente?",
                                        "Confirmar Eliminación Física",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Warning);

            if (confirmResult == DialogResult.No)
            {
                return;
            }

            int idTrabajador = int.Parse(dgTrabajadores.SelectedRows[0].Cells[0].Value.ToString());
            String mensaje = nTrabajador.EliminarFisico(idTrabajador);
            MessageBox.Show(mensaje);
            MostrarTrabajadores(nTrabajador.ListarTodoFisico());
        }
        private void FormRTrabajador_Load(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
            instancia = null;
        }


        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgTrabajadores.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un Trabajador");
                return;
            }
            int idtrabajador = int.Parse(dgTrabajadores.SelectedRows[0].Cells[0].Value.ToString());

            if (tbNombre.Text == "" || tbApellido.Text == "" || tbCorreo.Text == "" || tbNombreUsuario.Text == "" || tbPassword.Text == "")
            {
                MessageBox.Show("Ingrese los campos requeridos");
                return;
            }
            Trabajador trabajador = new Trabajador();

            trabajador.idTrabajador = idtrabajador;
            trabajador.Nombre = tbNombre.Text;
            trabajador.Apellido = tbApellido.Text;
            trabajador.CorreoElectronico = tbCorreo.Text;
            trabajador.NombreUsuario = tbNombreUsuario.Text;
            trabajador.Contrasenia = tbPassword.Text;
            trabajador.UsuarioModificacdorId = NTrabajador.trabajadorLogueado.idTrabajador;
            trabajador.FechaModificacion = DateTime.Now;

            try
            {
                int resultado = nTrabajador.Modificar(trabajador);

                if (resultado == -2)
                {
                    MessageBox.Show("El nombre de usuario ya existe.");
                    return;
                }

                if (resultado > 0)
                {
                    MessageBox.Show("Trabajador Modificado correctamente.");
                    MostrarTrabajadores(nTrabajador.ListarTodoFisico());
                }
                else
                {
                    MessageBox.Show("No se pudo modificar el trabajador.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inesperado: {ex.Message}");
            }
        }
        private void dgTrabajadores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tbNombre.Text = dgTrabajadores.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();
            tbApellido.Text = dgTrabajadores.Rows[e.RowIndex].Cells["Apellido"].Value.ToString();
            tbCorreo.Text = dgTrabajadores.Rows[e.RowIndex].Cells["CorreoElectronico"].Value.ToString();
            tbNombreUsuario.Text = dgTrabajadores.Rows[e.RowIndex].Cells["NombreUsuario"].Value.ToString();
            tbPassword.Text = dgTrabajadores.Rows[e.RowIndex].Cells["Contraseña"].Value.ToString();
        }
        private void dgTrabajadores_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
