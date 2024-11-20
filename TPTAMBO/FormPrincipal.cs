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
    public partial class FormPrincipal : Form
    {
        private FormLogin form;
        public FormPrincipal()
        {
            InitializeComponent();

            if (NTrabajador.trabajadorLogueado != null)
            {
                lblNombreTrabajador.Text = $"{NTrabajador.trabajadorLogueado.Nombre} {NTrabajador.trabajadorLogueado.Apellido}";
                lblNombreTrabajador.TextAlign = ContentAlignment.MiddleLeft;
            }
            else
            {
                lblNombreTrabajador.Text = "Usuario no identificado";
            }

        }
       
        private void trabajadorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormRTrabajador formRTrabajador = FormRTrabajador.Windows_Unique();
            if (formRTrabajador == null)
            {
                return;
            }

            formRTrabajador.MdiParent = this;
            formRTrabajador.Show();
        }

        private void sucursalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormRSucursal formRSucursal = FormRSucursal.Windows_Unique();
            if (formRSucursal == null)
            {
                return;
            }
            formRSucursal.MdiParent = this;
            formRSucursal.Show();
        }

        private void categoríaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormRCategoria formRCategoria = FormRCategoria.Windows_Unique();
            formRCategoria.MdiParent = this;
            formRCategoria.Show();
        }

        private void productoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormRProducto formRProducto = FormRProducto.Windows_Unique();
            formRProducto.MdiParent = this;
            formRProducto.Show();
        }

        private void boletasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormLBoletas formLBoletas = FormLBoletas.Windows_Unique();
            formLBoletas.MdiParent = this;
            formLBoletas.Show();
        }

        private void inventarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormLInventario formLInventario = FormLInventario.Windows_Unique();
            formLInventario.MdiParent = this;
            formLInventario.Show();
        }

        private void productosMásVendidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSReporte1 Fr1 = FormSReporte1.Windows_Unique();
            Fr1.MdiParent = this;
            Fr1.Show();
        }

        private void sucursalesConMásVentasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSReporte2 Fr2 = FormSReporte2.Windows_Unique();
            Fr2.MdiParent = this;
            Fr2.Show();
        }

        private void verVentasPorTrabajadorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSReporte3 Fr3 = FormSReporte3.Windows_Unique();
            Fr3.MdiParent = this;
            Fr3.Show();
        }

        private void categoríasMásVendidasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSReporte4 Fr4 = FormSReporte4.Windows_Unique();
            Fr4.MdiParent = this;
            Fr4.Show();
        }

        private void productosConStockBajoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSReporte5 Fr5 = FormSReporte5.Windows_Unique();
            Fr5.MdiParent = this;
            Fr5.Show();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            var confirmar = MessageBox.Show("¿Estás seguro de que deseas cerrar tu sesión?",
                                         "Confirmar Salida",
                                         MessageBoxButtons.YesNo);
            if (confirmar == DialogResult.Yes)
            {
                this.Close();
            }
        }
      
        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            
        }

        private void timerReloj_Tick(object sender, EventArgs e)
        {
            lblHora.Text = "Hora: " + DateTime.Now.ToString("HH:mm:ss"); 
            lblFecha.Text = "Fecha: " + DateTime.Now.ToString("dddd, dd MMMM yyyy");
        }

        private void FormPrincipal_Load_1(object sender, EventArgs e)
        {
            timerReloj.Tick += timerReloj_Tick; 
            timerReloj.Start();        
            lblFecha.Text = "Fecha: " + DateTime.Now.ToString("dddd, dd MMMM yyyy");
            lblHora.Text = "Hora: " + DateTime.Now.ToString("HH:mm:ss");
        }
    }
}
