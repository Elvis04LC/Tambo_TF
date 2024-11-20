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
    public partial class FormRSucursal : Form
    {
        private static FormRSucursal instancia = null;
        NSucursal nSucursal = new NSucursal();
        public static FormRSucursal Windows_Unique()
        {
            if (instancia == null)
            {
                instancia = new FormRSucursal();
                return instancia;
            }
            return instancia;
        }
        public FormRSucursal()
        {
            InitializeComponent();
            MostrarSucursales(nSucursal.ListarTodoFisico());
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
        private void LimpiarCampos()
        {
            tbNombre.Clear();
            tbDistrito.Clear();
        }
        private void MostrarSucursales(List<Sucursal> sucursales)
        {
            dgSucursal.DataSource = null;
            if (sucursales.Count == 0)
            {
                return;
            }
            else
            {
                dgSucursal.DataSource = sucursales;
                dgSucursal.Columns["Boleta"].Visible = false;
                dgSucursal.Columns["Inventario"].Visible = false;

                dgSucursal.Columns["idSucursal"].HeaderText = "ID Sucursal";
                dgSucursal.Columns["Nombre"].HeaderText = "Nombre de la Sucursal";
                dgSucursal.Columns["Distrito"].HeaderText = "Distrito";
                dgSucursal.Columns["Eliminado"].HeaderText = "Eliminado";
                dgSucursal.Columns["UsuarioCreadorId"].HeaderText = "Usuario Creador";
            }
        }
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (tbNombre.Text.Trim() == "" || tbDistrito.Text.Trim() == "")
            {
                MessageBox.Show("Ingrese los campos requeridos");
                return;
            }

            try
            {
                Sucursal sucursal = new Sucursal
                {
                    Nombre = tbNombre.Text.Trim(),
                    Distrito = tbDistrito.Text.Trim(),
                    Eliminado = false,
                    UsuarioCreadorId = NTrabajador.trabajadorLogueado.idTrabajador,
                    FechaCreacion = DateTime.Now,
                    UsuarioModificacdorId = NTrabajador.trabajadorLogueado.idTrabajador,
                    FechaModificacion = DateTime.Now
                };

                int registrado = nSucursal.Registrar(sucursal);

                if (registrado == -2)
                {
                    MessageBox.Show("El nombre de la sucursal ya existe.");
                    return;
                }

                if (registrado > 0)
                {
                    MessageBox.Show("Sucursal registrada correctamente.");
                    LimpiarCampos();
                    MostrarSucursales(nSucursal.ListarTodoFisico());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar sucursal: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void FormRSucursal_Load(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void dgSucursal_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnEliminarLogico_Click(object sender, EventArgs e)
        {
            if (dgSucursal.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un sucursal");
                return;
            }
            var confirmResult = MessageBox.Show("¿Está seguro de que desea eliminar esta sucursal de forma lógica?",
                                         "Confirmar Eliminación Lógica",
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);

            if (confirmResult == DialogResult.No)
                return;

            int sucursalId = int.Parse(dgSucursal.SelectedRows[0].Cells[0].Value.ToString());
            string mensaje = nSucursal.EliminarLogico(sucursalId);
            MessageBox.Show(mensaje);

            MostrarSucursales(nSucursal.ListarTodoFisico());
        }

        private void btnEliminarFisico_Click(object sender, EventArgs e)
        {
            if (dgSucursal.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un sucursal");
                return;
            }
            var confirmResult = MessageBox.Show("¿Está seguro de que desea eliminar esta sucursal permanentemente?",
                                         "Confirmar Eliminación Lógica",
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);

            if (confirmResult == DialogResult.No)
                return;

            int sucursalId = int.Parse(dgSucursal.SelectedRows[0].Cells[0].Value.ToString());
            string mensaje = nSucursal.EliminarFisico(sucursalId);
            MessageBox.Show(mensaje);

            MostrarSucursales(nSucursal.ListarTodoFisico());
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgSucursal.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una sucursal");
                return;
            }

            int sucursalId = int.Parse(dgSucursal.SelectedRows[0].Cells[0].Value.ToString());

            if (tbNombre.Text.Trim() == "" || tbDistrito.Text.Trim() == "")
            {
                MessageBox.Show("Ingrese los campos requeridos");
                return;
            }

            Sucursal sucursal = new Sucursal();
            sucursal.idSucursal = sucursalId;
            sucursal.Nombre = tbNombre.Text.Trim();
            sucursal.Distrito = tbDistrito.Text.Trim();
            sucursal.Eliminado = false;
            sucursal.UsuarioModificacdorId = NTrabajador.trabajadorLogueado.idTrabajador;
            sucursal.FechaModificacion = DateTime.Now;

            // Validación para permitir nombres repetidos en el mismo ID
            int registrado = nSucursal.Modificar(sucursal);
            if (registrado == -2 && nSucursal.ObtenerSucursalPorNombre(tbNombre.Text.Trim())?.idSucursal != sucursalId)
            {
                MessageBox.Show("Nombre de la sucursal repetido para otra sucursal!");
                return;
            }

            if (registrado == -1)
            {
                MessageBox.Show("Se produjo un error");
                return;
            }

            if (registrado > 0)
            {
                MessageBox.Show("Modificado correctamente");
            }

            MostrarSucursales(nSucursal.ListarTodoFisico());
        }

        private void dgSucursal_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0) 
                {
                    var fila = dgSucursal.Rows[e.RowIndex];

                    
                    if (fila.Cells["Nombre"].Value != null && fila.Cells["Distrito"].Value != null)
                    {
                        
                        tbNombre.Text = fila.Cells["Nombre"].Value.ToString();
                        tbDistrito.Text = fila.Cells["Distrito"].Value.ToString();
                    }
                    else
                    {
                       
                        tbNombre.Clear();
                        tbDistrito.Clear();
                        MessageBox.Show("La fila seleccionada no tiene datos válidos en 'Nombre' o 'Distrito'.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al seleccionar la fila: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
