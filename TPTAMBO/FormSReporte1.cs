using Datos.Clases;
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
    public partial class FormSReporte1 : Form
    {
        private static FormSReporte1 instancia = null;
        private NProducto nProducto = new NProducto();
        private NSucursal nSucursal = new NSucursal();
        public static FormSReporte1 Windows_Unique()
        {
            if (instancia == null)
            {
                instancia = new FormSReporte1();
                return instancia;
            }
            return instancia;
        }
        public FormSReporte1()
        {
            InitializeComponent();
            MostrarSucursales(nSucursal.ListarTodoLogico());
        }
        private void MostrarProductoVendidos(List<CProductoVendido> cProductoVendidos)
        {
            dgProductos.DataSource = null;
            if (cProductoVendidos.Count == 0)
            {
                return;
            }
            else
            {
                dgProductos.DataSource = cProductoVendidos;

            }
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

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
            instancia = null;
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            if (cbSucursal.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione una sucursal.");
                return;
            }

            int idSucursal = int.Parse(cbSucursal.SelectedValue.ToString());
            var cProductoVendidos = nProducto.ListarProductoMasVendidos(idSucursal);

            if (cProductoVendidos == null || cProductoVendidos.Count == 0)
            {
                MessageBox.Show("No se encontraron productos vendidos para esta sucursal.");
                return;
            }

            MostrarProductoVendidos(cProductoVendidos);
            FormatearDataGridView();
        }
        private void FormatearDataGridView()
        {
            dgProductos.Columns["Nombre"].HeaderText = "Nombre del Producto";
            dgProductos.Columns["NroVentas"].HeaderText = "Cantidad Vendida";
            dgProductos.Columns["PrecioProducto"].HeaderText = "Precio Total";
            dgProductos.Columns["PrecioProducto"].DefaultCellStyle.Format = "C2"; // Formato moneda
            dgProductos.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 9, FontStyle.Bold);
            dgProductos.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        }
    }
}
