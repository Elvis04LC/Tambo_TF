using Datos.Clases;
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
    public partial class FormSReporte2 : Form
    {
        private static FormSReporte2 instancia = null;
        private NSucursal nSucursal = new NSucursal();
        public static FormSReporte2 Windows_Unique()
        {
            if (instancia == null)
            {
                instancia = new FormSReporte2();
                return instancia;
            }
            return instancia;
        }
        public FormSReporte2()
        {
            InitializeComponent();
            MostrarSucursalesConMasVentas(nSucursal.ListarSucursalesConMasVentas());
        }
        private void MostrarSucursalesConMasVentas(List<CSucursalVenta> sucursales)
        {
            dgSucursales.DataSource = null;

            if (sucursales.Count == 0)
            {
                MessageBox.Show("No se encontraron sucursales con ventas.");
                return;
            }

            dgSucursales.DataSource = sucursales;


            dgSucursales.Columns["idSucursal"].HeaderText = "ID Sucursal";
            dgSucursales.Columns["Nombre"].HeaderText = "Nombre de Sucursal";
            dgSucursales.Columns["Distrito"].HeaderText = "Distrito";
            dgSucursales.Columns["CantidadVentas"].HeaderText = "Cantidad de Ventas";

            dgSucursales.Columns["CantidadVentas"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgSucursales.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            dgSucursales.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
            instancia = null;
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            DateTime fechaInicio = dpFechaInicio.Value.Date;
            DateTime fechaFin = dpFechaFin.Value.Date;

            if (fechaInicio > fechaFin)
            {
                MessageBox.Show("La fecha de inicio no puede ser mayor que la fecha final.");
                return;
            }

            List<CSucursalVenta> sucursales = nSucursal.ListarSucursalesConMasVentasPorFecha(fechaInicio, fechaFin);

            if (sucursales.Count == 0)
            {
                MessageBox.Show("No se encontraron resultados para el rango de fechas seleccionado.");
            }
            else
            {
                dgSucursales.DataSource = null;
                dgSucursales.DataSource = sucursales;
            }
        }
    }
}
