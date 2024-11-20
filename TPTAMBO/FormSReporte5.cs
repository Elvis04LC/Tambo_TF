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
    public partial class FormSReporte5 : Form
    {
        private static FormSReporte5 instancia = null;
        private NSucursal nSucursal = new NSucursal();
        private NProducto nProducto = new NProducto();
        public static FormSReporte5 Windows_Unique()
        {
            if (instancia == null)
            {
                instancia = new FormSReporte5();
                return instancia;
            }
            else
            {
                instancia.BringToFront();
            }

            return instancia;
        }
        public FormSReporte5()
        {
            InitializeComponent();
            MostrarSucursales(nSucursal.ListarTodoLogico());

        }
      
        private void MostrarProductoStock(List<CProductoStock> cProductoStocks)
        {
            dgProductos.DataSource = null;
            if (cProductoStocks.Count == 0)
            {
                MessageBox.Show("No hay productos con stock bajo en la sucursal seleccionada.");
                lblResumen.Text = $"Total productos con stock bajo: 0";
                return;
            }
            else
            {
                dgProductos.DataSource = cProductoStocks;


                dgProductos.Columns["idProducto"].HeaderText = "ID Producto";
                dgProductos.Columns["Nombre"].HeaderText = "Nombre del Producto";
                dgProductos.Columns["Descripcion"].HeaderText = "Descripción";
                dgProductos.Columns["Stock"].HeaderText = "Stock Actual";
                dgProductos.Columns["PrecioProducto"].HeaderText = "Precio";
                lblResumen.Text = $"Total productos con stock bajo: {cProductoStocks.Count}";
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
        private void FormSReporte5_Load(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
            instancia = null;
        }

        private void btnListar_Click(object sender, EventArgs e)
        {

            if (cbSucursal.Text == "")
            {
                MessageBox.Show("Ingrese los campos requeridos");
                return;
            }
            int idSucursal = int.Parse(cbSucursal.SelectedValue.ToString());
            MostrarProductoStock(nProducto.ListarProductoMenosStock(idSucursal));
        }
    }
}
