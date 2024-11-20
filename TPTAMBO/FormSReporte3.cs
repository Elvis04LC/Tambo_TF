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
    public partial class FormSReporte3 : Form
    {
        private NBoleta nBoleta = new NBoleta();
        private NTrabajador nTrabajador = new NTrabajador();
        private static FormSReporte3 instancia = null;
        public static FormSReporte3 Windows_Unique()
        {
            if (instancia == null)
            {
                instancia = new FormSReporte3();
                return instancia;
            }
            return instancia;
        }
        public FormSReporte3()
        {
            InitializeComponent();
        }
        private void MostrarBoletas(List<Boleta> boletas)
        {
            dgVentas.DataSource = null;
            if (boletas.Count == 0)
            {
                lblTotalVentas.Text = "Total Ventas: S/. 0.00";
                return;
            }
            else
            {
                dgVentas.DataSource = boletas;
                FormatearDataGridView();
                MostrarResumen(boletas);
            }
        }
        private void MostrarResumen(List<Boleta> boletas)
        {
            decimal totalVentas = boletas.Sum(b => b.Total);
            lblTotalVentas.Text = $"Total Ventas: {totalVentas:C2}";
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
            instancia = null;
        }
        private void FormatearDataGridView()
        {
            dgVentas.Columns["idBoleta"].HeaderText = "ID Boleta";
            dgVentas.Columns["FechaEmision"].HeaderText = "Fecha de Emisión";
            dgVentas.Columns["Total"].HeaderText = "Total de Venta";

            dgVentas.Columns["Total"].DefaultCellStyle.Format = "C2"; // Formato moneda
            dgVentas.DefaultCellStyle.Font = new Font("Arial", 9);
            dgVentas.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            dgVentas.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        }
        private void btnListar_Click(object sender, EventArgs e)
        {
            if (tbNombreUsuario.Text == "")
            {
                MessageBox.Show("Ingrese los campos requeridos");
                return;
            }
            String nombreUsuario = tbNombreUsuario.Text.Trim();
            Trabajador trabajador = nTrabajador.ObtenerTrabajadorPorNombreUsuario(nombreUsuario);
            if (trabajador == null)
            {
                MessageBox.Show("No se encontro al Trabajador");
                return;
            }
            MostrarBoletas(nBoleta.ListarPorVendedor(trabajador));
        }
    }
}
