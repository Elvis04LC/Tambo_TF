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
    public partial class FormProductoInventario : Form
    {
        private static FormProductoInventario instancia = null;
        private NInventario nInventario = new NInventario();
        private NProductoInventario nProductoInventario = new NProductoInventario();
        private NProducto nProducto = new NProducto();
        private int inventarioId;
        public static FormProductoInventario Windows_Unique(int inventarioId)
        {
            if (instancia == null || instancia.IsDisposed)
            {
                instancia = new FormProductoInventario(inventarioId);
            }
            return instancia;
        }
        private void ConfigurarEstiloDataGridView(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 9, FontStyle.Bold); 
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; 
            dgv.DefaultCellStyle.Font = new Font("Arial", 8);
            dgv.ColumnHeadersHeight = 30;
        }
        public FormProductoInventario(int inventarioId)
        {
            this.inventarioId = inventarioId;
            InitializeComponent();
            ConfigurarEstiloDataGridView(dgProductoInventario);
            MostrarProductos(nProducto.ListarTodoLogico());
            MostrarProductoInventario(nProductoInventario.ListarTodoFisico(this.inventarioId));
        }

        private void LimpiarCampos()
        {
            tbStock.Clear();
            cbProducto.SelectedIndex = -1;
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
                lblStock.Text = nProductoInventario.CalcularStockTotal(nInventario.ObtenerInventario(inventarioId), this.inventarioId).ToString();
                dgProductoInventario.Columns["Inventario"].Visible = false;
                dgProductoInventario.Columns["Producto"].Visible = false;
            }
        }
        private void MostrarProductos(List<Producto> productos)
        {
            cbProducto.DataSource = null;
            if (productos.Count == 0)
            {
                return;
            }
            else
            {
                cbProducto.DataSource = productos;
                cbProducto.ValueMember = "idProducto";
                cbProducto.DisplayMember = "Nombre";
            }
        }
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbStock.Text == "" || cbProducto.Text == "")
                {
                    MessageBox.Show("Ingrese los campos requeridos");
                    return;
                }

                int idProducto = int.Parse(cbProducto.SelectedValue.ToString());
                int stock = int.Parse(tbStock.Text.Trim());

                if (stock < 1)
                {
                    MessageBox.Show("El stock debe ser positivo");
                    return;
                }

                ProductoInventario productoInventario = new ProductoInventario
                {
                    idProducto = idProducto,
                    idInventario = this.inventarioId,
                    Stock = stock,
                    UsuarioCreadorId = NTrabajador.trabajadorLogueado.idTrabajador,
                    FechaCreacion = DateTime.Now,
                    UsuarioModificadorId = NTrabajador.trabajadorLogueado.idTrabajador,
                    FechaModificacion = DateTime.Now
                };

                int existe = nProductoInventario.ProductoConInventario(idProducto, this.inventarioId);
                if (existe == 0)
                {
                    MessageBox.Show("Este producto ya se encuentra registrado en el inventario, intenta modificarlo");
                    return;
                }

                Producto producto = nProducto.ObtenerProductoPorId(idProducto);
                int actualizar = nProducto.ActualizarCantidadProductoRegistrar(producto, stock);

                if (actualizar == 0)
                {
                    MessageBox.Show("El producto no cuenta con stock suficiente");
                }
                else if (actualizar > 0)
                {
                    string mensaje = nProductoInventario.Registrar(productoInventario);
                    MessageBox.Show(mensaje);
                    LimpiarCampos();
                    MostrarProductoInventario(nProductoInventario.ListarTodoFisico(this.inventarioId));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgProductoInventario.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un productoInventario");
                return;
            }
            int productoInventarioId = int.Parse(dgProductoInventario.SelectedRows[0].Cells[0].Value.ToString());

            ProductoInventario productoInventarioTemp = nProductoInventario.ObtenerPorId(productoInventarioId);
            Producto productoTemp = nProducto.ObtenerProductoPorId(productoInventarioTemp.idProducto);

            int eliminar = nProducto.ActualizarCantidadProductoElmininar(productoTemp, productoInventarioTemp.Stock);

            if (tbStock.Text == "" || cbProducto.Text == "")
            {
                MessageBox.Show("Ingrese los campos requeridos");
                return;
            }
            int idProducto = int.Parse(cbProducto.SelectedValue.ToString());

            int stock = 0;
            try
            {
                stock = int.Parse(tbStock.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ingrese los campos numericos correctamente");
                return;
            }

            if (stock < 1)
            {
                MessageBox.Show("El stock debe ser positivo");
                return;
            }

            ProductoInventario productoInventario = new ProductoInventario();
            productoInventario.idProductoInventario = productoInventarioId;
            productoInventario.idInventario = this.inventarioId;
            productoInventario.idProducto = idProducto;
            productoInventario.Stock = stock;
            productoInventario.UsuarioModificadorId = NTrabajador.trabajadorLogueado.idTrabajador;
            productoInventario.FechaModificacion = DateTime.Now;

            Inventario inventario = nInventario.ObtenerInventario(inventarioId);
            int totalStock = nProductoInventario.CalcularStockTotal(nInventario.ObtenerInventario(inventarioId), inventarioId);

            Producto producto = nProducto.ObtenerProductoPorId(idProducto);

            int existe = nProductoInventario.ProductoConInventario(idProducto, this.inventarioId);
            int actualizar = nProducto.ActualizarCantidadProductoRegistrar(producto, stock);

            if (actualizar == 0)
            {
                MessageBox.Show("El producto no cuenta con stock suficiente");
                return;
            }
            else if (actualizar == -1)
            {
                MessageBox.Show("Se produjo un error");
                return;
            }
            else if (actualizar > 0)
            {
                String mensaje = nProductoInventario.Modificar(productoInventario);
                Inventario inventarioTemp = nInventario.ObtenerInventario(this.inventarioId);
                int totalStockTemp = nProductoInventario.CalcularStockTotal(inventarioTemp, this.inventarioId);
                MessageBox.Show(mensaje);
                MostrarProductoInventario(nProductoInventario.ListarTodoFisico(this.inventarioId));
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgProductoInventario.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un productoInventario");
                return;
            }

            DialogResult confirmacion = MessageBox.Show(
                "¿Está seguro de eliminar este producto del inventario?",
                "Confirmar Eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirmacion == DialogResult.Yes)
            {
                int productoInventarioId = int.Parse(dgProductoInventario.SelectedRows[0].Cells[0].Value.ToString());
                ProductoInventario productoInventarioTemp = nProductoInventario.ObtenerPorId(productoInventarioId);
                Producto producto = nProducto.ObtenerProductoPorId(productoInventarioTemp.idProducto);

                int actualizar = nProducto.ActualizarCantidadProductoElmininar(producto, productoInventarioTemp.Stock);
                if (actualizar == -1)
                {
                    MessageBox.Show("Se produjo un error");
                    return;
                }
                else if (actualizar > 0)
                {
                    String mensaje = nProductoInventario.EliminarFisico(productoInventarioId);
                    MessageBox.Show(mensaje);
                    MostrarProductoInventario(nProductoInventario.ListarTodoFisico(this.inventarioId));
                }
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
