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
    public partial class FormRProducto : Form
    {
        private static FormRProducto instancia = null;
        private NProducto nProducto = new NProducto();
        private NCategoria nCategoria = new NCategoria();

        public static FormRProducto Windows_Unique()
        {
            if (instancia == null)
            {
                instancia = new FormRProducto();
                return instancia;
            }
            return instancia;
        }
        public FormRProducto()
        {
            InitializeComponent();
            MostrarProductos(nProducto.ListarTodoFisico());
            MostrarCategorias(nCategoria.ListarTodoLogico());   
        }
        private void LimpiarCampos()
        {
            tbNombre.Clear();
            tbDescripcion.Clear();
            tbCantidadTotal.Clear();
            tbPrecio.Clear();
            cbCategoria.SelectedIndex = -1;
        }
        
        private void MostrarProductos(List<Producto> productos)
        {
            dgProductos.DataSource = null;
            if (productos.Count == 0)
            {
                return;
            }
            else
            {
                tbNombre.Enabled = true;
                dgProductos.DataSource = productos;
                dgProductos.Columns["Categoria"].Visible = false;
                dgProductos.Columns["DetalleBoleta"].Visible = false;
                dgProductos.Columns["ProductoInventario"].Visible = false;
            }
        }
        private void MostrarCategorias(List<Categoria> categorias)
        {
            cbCategoria.DataSource = null;
            if (categorias.Count == 0)
            {
                return;
            }
            else
            {
                cbCategoria.DataSource = categorias;
                cbCategoria.ValueMember = "idCategoria";
                cbCategoria.DisplayMember = "Nombre";
            }
        }
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (tbNombre.Text == "" || tbDescripcion.Text == "" || tbCantidadTotal.Text == "" || tbPrecio.Text == "" || cbCategoria.Text == "")
            {
                MessageBox.Show("Ingrese los campos requeridos");
                return;
            }

            int idCategoria = int.Parse(cbCategoria.SelectedValue.ToString());

            // Validacion de campos numericos
            Decimal precio = 0;
            int cantidad = 0;
            try
            {
                precio = Decimal.Parse(tbPrecio.Text);
                cantidad = int.Parse(tbCantidadTotal.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ingrese los campos numericos correctamente");
                return;
            }

            if (precio < 1 || cantidad < 1)
            {
                MessageBox.Show("El precio y la cantidad deben ser positivos");
                return;
            }
            Producto producto = new Producto();
            producto.Nombre = tbNombre.Text;
            producto.Descripcion = tbDescripcion.Text;
            producto.PrecioProducto = precio;
            producto.CantidadTotal = cantidad;
            producto.idCategoria = idCategoria;
            producto.UsuarioCreadorId = NTrabajador.trabajadorLogueado.idTrabajador;
            producto.FechaCreacion = DateTime.Now;
            producto.UsuarioModificadorId = NTrabajador.trabajadorLogueado.idTrabajador;
            producto.FechaModificacion = DateTime.Now;

            try
            {
                int registrado = nProducto.Registrar(producto);

                if (registrado == -2)
                {
                    MessageBox.Show("Nombre de producto repetido.");
                    return;
                }

                if (registrado > 0)
                {
                    MessageBox.Show("Producto registrado correctamente.");
                    LimpiarCampos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar producto: {ex.Message}");
            }

            MostrarProductos(nProducto.ListarTodoFisico());
        }
     
        private void FormRProducto_Load(object sender, EventArgs e)
        {

        }
        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgProductos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un producto");
                return;
            }
            int productoId = int.Parse(dgProductos.SelectedRows[0].Cells[0].Value.ToString());

            if (tbNombre.Text == "" || tbDescripcion.Text == "" || tbCantidadTotal.Text == "" || tbPrecio.Text == "" || cbCategoria.Text == "")
            {
                MessageBox.Show("Ingrese los campos requeridos");
                return;
            }

            int idCategoria = int.Parse(cbCategoria.SelectedValue.ToString());

            // Validacion de campos numericos
            Decimal precio = 0;
            int cantidad = 0;
            try
            {
                precio = Decimal.Parse(tbPrecio.Text);
                cantidad = int.Parse(tbCantidadTotal.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ingrese los campos numericos correctamente");
                return;
            }

            if (precio < 1 || cantidad < 1)
            {
                MessageBox.Show("El precio y la cantidad deben ser positivos");
                return;
            }
            Producto producto = new Producto();
            producto.idProducto = productoId;
            producto.Nombre = tbNombre.Text;
            producto.Descripcion = tbDescripcion.Text;
            producto.PrecioProducto = precio;
            producto.CantidadTotal = cantidad;
            producto.idCategoria = idCategoria;
            producto.UsuarioModificadorId = NTrabajador.trabajadorLogueado.idTrabajador;
            producto.FechaModificacion = DateTime.Now;

            try
            {
                int registrado = nProducto.Modificar(producto);

                if (registrado == -1)
                {
                    MessageBox.Show("Se produjo un error");
                    return;
                }

                if (registrado > 0)
                {
                    MessageBox.Show("Modificado correctamente");
                    LimpiarCampos();
                }

                MostrarProductos(nProducto.ListarTodoFisico());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al modificar producto: {ex.Message}");
            }
        }

        private void btnEliminarLogico_Click(object sender, EventArgs e)
        {
            if (dgProductos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un producto");
                return;
            }
            var confirmResult = MessageBox.Show("¿Está seguro de que desea eliminar este producto de forma lógica?",
                                        "Confirmar Eliminación Lógica",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question);

            if (confirmResult == DialogResult.No)
                return;

            int productoId = int.Parse(dgProductos.SelectedRows[0].Cells[0].Value.ToString());
            string mensaje = nProducto.EliminarLogico(productoId);
            MessageBox.Show(mensaje);

            MostrarProductos(nProducto.ListarTodoFisico());
        }

        private void btnEliminarFisico_Click(object sender, EventArgs e)
        {
            if (dgProductos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un producto");
                return;
            }
            var confirmResult = MessageBox.Show("¿Está seguro de que desea eliminar este producto de forma permanente?",
                                         "Confirmar Eliminación Física",
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Warning);

            if (confirmResult == DialogResult.No)
                return;

            int productoId = int.Parse(dgProductos.SelectedRows[0].Cells[0].Value.ToString());
            string mensaje = nProducto.EliminarFisico(productoId);
            MessageBox.Show(mensaje);

            MostrarProductos(nProducto.ListarTodoFisico());
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
            instancia = null;
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            if (cbCategoria.SelectedValue == null)
            {
                MessageBox.Show("Seleccione una categoría válida.");
                return;
            }

            int categoriaId = int.Parse(cbCategoria.SelectedValue.ToString());

            List<Producto> productosPorCategoria = nProducto.ListarPorCategoria(categoriaId);

            if (productosPorCategoria.Count == 0)
            {
                MessageBox.Show("No hay productos en la categoría seleccionada.");
            }
            MostrarProductos(productosPorCategoria);
        }
    }
}
