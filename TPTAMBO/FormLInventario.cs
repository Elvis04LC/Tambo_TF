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
    public partial class FormLInventario : Form
    {
        private static FormLInventario instancia = null;
        private NInventario nInventario = new NInventario();
        private NProductoInventario nProductoInventario = new NProductoInventario();
        private NSucursal nSucursal = new NSucursal();
        public static FormLInventario Windows_Unique()
        {
            if (instancia == null || instancia.IsDisposed) // Verifica si está cerrado o eliminado
            {
                instancia = new FormLInventario();
            }
            return instancia;

        }
        public FormLInventario()
        {
            InitializeComponent();
            ConfigurarEstiloDataGridView(dgInventario); 
            ConfigurarEstiloDataGridView(dgProductoInventario);
            MostrarInventarios(nInventario.ListarTodoFisico());
            MostrarSucursales(nSucursal.ListarTodoLogico());
            this.FormClosed += FormLInventario_FormClosed;
        }
        private void ConfigurarEstiloDataGridView(DataGridView dgv)
        {
         
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 9, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;


            dgv.DefaultCellStyle.Font = new Font("Arial",8, FontStyle.Regular);
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; 

            
            dgv.ColumnHeadersHeight = 30;
            dgv.RowTemplate.Height = 25;
        }
        private void LimpiarCampos()
        {
            lblStockTotal.Text = "0";
            dgProductoInventario.DataSource = null;
        }
        private void FormLInventario_FormClosed(object sender, FormClosedEventArgs e)
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
        private void MostrarInventarios(List<Inventario> inventarios)
        {
            dgInventario.DataSource = null;
            if (inventarios.Count == 0)
            {
                return;
            }
            else
            {
                foreach (var inventario in inventarios)
                {                 
                    inventario.StockTotal = nProductoInventario.CalcularStockTotal(inventario, inventario.idInventario);
                }

                dgInventario.DataSource = inventarios;
                dgInventario.Columns["Sucursal"].Visible = false;
                dgInventario.Columns["ProductoInventario"].Visible = false;
                dgInventario.Columns["idInventario"].HeaderText = "ID Inventario";
                dgInventario.Columns["StockTotal"].HeaderText = "Stock Total Disponible";
            }
        }
        private void MostrarProductoInventario(List<ProductoInventario> productoInventarios)
        {
            dgProductoInventario.DataSource = null;
            if (productoInventarios.Count == 0)
            {
                return;
            }
            else
            {
                dgProductoInventario.DataSource = productoInventarios;
                dgProductoInventario.Columns["Inventario"].Visible = false;
                dgProductoInventario.Columns["Producto"].Visible = false;
            }
        }
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbSucursal.Text == "")
                {
                    MessageBox.Show("Ingrese los campos requeridos");
                    return;
                }

                int idSucursal = int.Parse(cbSucursal.SelectedValue.ToString());

                Inventario inventario = new Inventario
                {
                    StockTotal = 0,
                    idSucursal = idSucursal,
                    UsuarioCreadorId = NTrabajador.trabajadorLogueado.idTrabajador,
                    FechaCreacion = DateTime.Now,
                    UsuarioModificadorId = NTrabajador.trabajadorLogueado.idTrabajador,
                    FechaModificacion = DateTime.Now
                };

                int existe = nInventario.SucursalConInventario(idSucursal);
                if (existe == 0)
                {
                    MessageBox.Show("La sucursal ya posee un inventario");
                    return;
                }

                int registrado = nInventario.Registrar(inventario);
                if (registrado > 0)
                {
                    MessageBox.Show("Registrado correctamente");
                    LimpiarCampos();
                    MostrarInventarios(nInventario.ListarTodoFisico());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgInventario.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un inventario");
                return;
            }
            try
            {
                int inventarioId = int.Parse(dgInventario.SelectedRows[0].Cells[0].Value.ToString());
                DialogResult dialog = MessageBox.Show(
                    $"¿Está seguro de eliminar el inventario con ID: {inventarioId}?",
                    "Confirmación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (dialog == DialogResult.Yes)
                {
                    string mensaje = nInventario.EliminarFisico(inventarioId);
                    MessageBox.Show(mensaje);
                    LimpiarCampos();
                    MostrarInventarios(nInventario.ListarTodoFisico());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar inventario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgInventario_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) 
            {
                int inventarioId;

                try
                {
                   
                    inventarioId = int.Parse(dgInventario.Rows[e.RowIndex].Cells[0].Value.ToString());
                    MostrarProductoInventario(nProductoInventario.ListarTodoFisico(inventarioId));                  
                    Inventario inventario = nInventario.ObtenerInventario(inventarioId);
                    int totalStock = nProductoInventario.CalcularStockTotal(inventario, inventarioId);
                    lblStockTotal.Text = totalStock.ToString();                   
                    btnAgregarProducto.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al seleccionar el inventario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);                 
                    lblStockTotal.Text = "0";
                    btnAgregarProducto.Enabled = false;
                }
            }
            else
            {               
                btnAgregarProducto.Enabled = false;
            }
        }
        private void dgProductoInventario_CellClick(object sender, DataGridViewCellEventArgs e)
        {
         
        }
        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            if (dgInventario.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un inventario");
                return;
            }

           
            int inventarioId = int.Parse(dgInventario.SelectedRows[0].Cells[0].Value.ToString());
            FormProductoInventario form = new FormProductoInventario(inventarioId);
            form.MdiParent = this.MdiParent;
            form.FormClosed += (s, args) =>
            {              
                MostrarInventarios(nInventario.ListarTodoFisico());
            };
            form.Show();
           
        }
        private void FormLInventario_Load(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();

        }

     
    }
}
