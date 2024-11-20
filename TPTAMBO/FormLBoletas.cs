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
    public partial class FormLBoletas : Form
    {
        private static FormLBoletas instancia = null;
        private NBoleta nBoleta = new NBoleta();
        private NDetalleBoleta nDetalleBoleta = new NDetalleBoleta();
        private NSucursal nSucursal = new NSucursal();
        public static FormLBoletas Windows_Unique()
        {
            if (instancia == null || instancia.IsDisposed) // Verifica si está cerrado o eliminado
            {
                instancia = new FormLBoletas();
            }
            return instancia;
        }
        
        public FormLBoletas()
        {
            InitializeComponent();
            ConfigurarEstiloDataGridView(dgBoleta);
            ConfigurarEstiloDataGridView(dgDetalleBoleta);
            MostrarBoletas(nBoleta.ListarTodoFisico());
            MostrarSucursales(nSucursal.ListarTodoLogico());
            this.FormClosed += FormLBoletas_FormClosed;

        }
        private void ConfigurarEstiloDataGridView(DataGridView dgv)
        {

            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 9, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.NotSet;


            dgv.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Regular);
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.NotSet;


            dgv.ColumnHeadersHeight = 30; // Altura de los encabezados
            dgv.RowTemplate.Height = 25; // Altura de las filas
        }
        private void LimpiarCampos()
        {
            dpFechaEmision.Value = DateTime.Now;
            cbSucursal.SelectedIndex = -1;
            lblTotal.Text = "0";
        }
        private void FormLBoletas_FormClosed(object sender, FormClosedEventArgs e)
        {
            instancia = null; // Libera la instancia al cerrar el formulario
        }
        private void MostrarSucursales(List<Sucursal> sucursales)
        {
            cbSucursal.DataSource = null;
            if (sucursales.Count == 0)
            {
                return;
            }
            else
            {
                cbSucursal.DataSource = sucursales;
                cbSucursal.ValueMember = "idSucursal";
                cbSucursal.DisplayMember = "Nombre";
            }
        }
        private void MostrarBoletas(List<Boleta> boletas)
        {
            dgBoleta.DataSource = null;
            if (boletas.Count == 0)
            {
                return;
            }
            else
            {
                dgBoleta.DataSource = boletas;
                dgBoleta.Columns["Trabajador"].Visible = false;
                dgBoleta.Columns["Sucursal"].Visible = false;
                dgBoleta.Columns["DetalleBoleta"].Visible = false;
                dgBoleta.Columns["Eliminado"].HeaderText="Estado de Pago";
            }
        }
        private void MostrarDetalleBoleta(List<DetalleBoleta> detalleBoletas)
        {
            dgDetalleBoleta.DataSource = null;
            if (detalleBoletas.Count == 0)
            {
                return;
            }
            else
            {
                dgDetalleBoleta.DataSource = detalleBoletas;
                dgDetalleBoleta.Columns["Boleta"].Visible = false;
                dgDetalleBoleta.Columns["Producto"].Visible = false;
            }
        }
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbSucursal.SelectedIndex == -1)
                {
                    MessageBox.Show("Seleccione una sucursal.");
                    return;
                }

                int idSucursal = int.Parse(cbSucursal.SelectedValue.ToString());
                Decimal total = 0;

                Boleta boleta = new Boleta
                {
                    FechaEmision = dpFechaEmision.Value.Date,
                    idSucursal = idSucursal,
                    Eliminado = false,
                    Total = total,
                    UsuarioCreadorId = NTrabajador.trabajadorLogueado.idTrabajador,
                    FechaCreacion = DateTime.Now,
                    idTrabajador = NTrabajador.trabajadorLogueado.idTrabajador,
                    UsuarioModificadorId = NTrabajador.trabajadorLogueado.idTrabajador,
                    FechaModificacion = DateTime.Now
                };

                int registrado = nBoleta.Registrar(boleta);

                if (registrado > 0)
                {
                    MessageBox.Show("Boleta registrada correctamente.");
                    LimpiarCampos();
                    MostrarBoletas(nBoleta.ListarTodoFisico());
                }
                else
                {
                    MessageBox.Show("Se produjo un error al registrar la boleta.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgBoleta.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una boleta para marcarla como pagada.");
                return;
            }
            int boletaId = int.Parse(dgBoleta.SelectedRows[0].Cells[0].Value.ToString());
            DialogResult confirmacion = MessageBox.Show(
                 $"¿Está seguro de marcar como pagada la boleta con ID: {boletaId}?",
                "Confirmación de Pago",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirmacion == DialogResult.Yes)
            {
                String mensaje = nBoleta.EliminarLogico(boletaId);
                MessageBox.Show(mensaje);
                MostrarBoletas(nBoleta.ListarTodoFisico());
                LimpiarCampos();
            }
        }

        

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            if (dgBoleta.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una boleta");
                return;
            }
            int boletaId = int.Parse(dgBoleta.SelectedRows[0].Cells[0].Value.ToString());
            int idSucursal = nBoleta.ObtenerBoleta(boletaId).idSucursal;
            bool esEliminado = (bool)dgBoleta.SelectedRows[0].Cells["Eliminado"].Value;

            // Validar si la boleta está eliminada
            if (esEliminado)
            {
                MessageBox.Show("No se pueden agregar productos a una boleta eliminada.");
                return;
            }
            FormDetalleBoleta form = new FormDetalleBoleta(boletaId, idSucursal);
            form.MdiParent = this.MdiParent;
            form.FormClosed += (s, args) =>
            {
                // Actualiza las boletas después de cerrar el formulario hijo
                MostrarBoletas(nBoleta.ListarTodoFisico());
            };
            form.Show();
        }


        private void FormLBoletas_Load(object sender, EventArgs e)
        {
            btnAgregarProducto.BackColor = Color.LightGray;
            btnAgregarProducto.Enabled = false;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void dgBoleta_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (dgBoleta.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Seleccione una boleta");
                    return;
                }
                int boletaId = int.Parse(dgBoleta.SelectedRows[0].Cells[0].Value.ToString());
                MostrarDetalleBoleta(nDetalleBoleta.ListarTodoFisico(boletaId));
                Boleta boleta = nBoleta.ObtenerBoleta(boletaId);
                boleta.Total = nDetalleBoleta.CalcularTotal(boleta, boletaId);
                lblTotal.Text = boleta.Total.ToString("C2");
            }
        }

        private void dgBoleta_SelectionChanged(object sender, EventArgs e)
        {
            if (dgBoleta.SelectedRows.Count > 0)
            {
                bool esEliminado = (bool)dgBoleta.SelectedRows[0].Cells["Eliminado"].Value;

                if (esEliminado)
                {
                    btnAgregarProducto.BackColor = Color.Gray;
                    btnAgregarProducto.Enabled = false;
                }
                else
                {
                    btnAgregarProducto.BackColor = Color.LightGreen;
                    btnAgregarProducto.Enabled = true;
                }

                // Actualizar el total de la boleta seleccionada
                int boletaId = int.Parse(dgBoleta.SelectedRows[0].Cells[0].Value.ToString());
                Boleta boleta = nBoleta.ObtenerBoleta(boletaId);
                boleta.Total = nDetalleBoleta.CalcularTotal(boleta, boletaId);
                lblTotal.Text = boleta.Total.ToString("C2"); // Formato de moneda con dos decimales
            }
            else
            {
                btnAgregarProducto.BackColor = Color.LightGray;
                btnAgregarProducto.Enabled = false;
                lblTotal.Text = "0.00"; 
            }
        }
    }
}
