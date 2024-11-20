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
    public partial class FormLogin : Form
    {

        private NTrabajador nTrabajador = new NTrabajador();
        public FormLogin()
        {
            InitializeComponent();
        }
        private void btnAcceder_Click(object sender, EventArgs e)
        {

            string nombreUsuario = tbUser.Text.Trim();
            string contrasenia = tbPassword.Text.Trim();

            if (tbUser.Text.Length == 0) //si ingrese un texto al usuario
            {
                MessageBox.Show("Falta Ingresar Nombre del Usuario", "Error");
                tbUser.Focus();//envia el puntero al control seleccionado
                return;
            }
            else if (tbPassword.Text.Length == 0)
            {
                MessageBox.Show("Falta Ingresar la Clave del Usuario", "Error");
                tbPassword.Focus();
                return;
            }
            Trabajador vendedorTemp = nTrabajador.IniciarSesion(nombreUsuario, contrasenia);

            if (vendedorTemp==null)
            {
                //si la clave o usuario es incorrecta
                
                MessageBox.Show("Error en Usuario o Clave de Acceso", "Error");
                tbUser.Clear(); tbPassword.Clear(); tbUser.Focus();
                return;

            }         
            else
            {
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
           
        }

        private void tbPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            string nombreUsuario = tbUser.Text.Trim();
            string contrasenia = tbPassword.Text.Trim();
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (tbUser.Text.Length == 0) //si ingrese un texto al usuario
                {
                    MessageBox.Show("Falta Ingresar Nombre del Usuario", "Error");
                    tbUser.Focus();//envia el puntero al control seleccionado
                }
                else if (tbPassword.Text.Length == 0)//si ingrese un texto a la clave
                {
                    MessageBox.Show("Falta Ingresar la Clave del Usuario", "Error");
                    tbPassword.Focus(); //envia el puntero al control seleccionado
                }
                Trabajador vendedorTemp = nTrabajador.IniciarSesion(nombreUsuario, contrasenia);

                if (vendedorTemp == null)
                {
                    //si la clave o usuario es incorrecta

                    MessageBox.Show("Error en Usuario o Clave de Acceso", "Error");
                    tbUser.Clear(); tbPassword.Clear(); tbUser.Focus();
                    return;

                }
                else
                {
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                }
            }
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
