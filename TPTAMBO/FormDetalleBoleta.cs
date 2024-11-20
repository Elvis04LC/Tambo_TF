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
    public partial class FormDetalleBoleta : Form
    {
        private static FormDetalleBoleta instancia = null;

        private NDetalleBoleta nDetalleBoleta = new NDetalleBoleta();
        private NProducto nProducto = new NProducto();
        private NBoleta nBoleta = new NBoleta();
        private NSucursal nSucursal = new NSucursal();
        private NProductoInventario nProductoInventario = new NProductoInventario();
        private NInventario nInventario = new NInventario();
        public int inventarioId;
        public int boletaId;
        public int sucursalId;
        public static FormDetalleBoleta Windows_Unique(int boletaID, int sucursalid)
        {
            if (instancia == null)
            {
                instancia = new FormDetalleBoleta(boletaID,sucursalid);
                return instancia;
            }
            return instancia;
        }
        private void LimpiarCampos()
        {
            cbProducto.SelectedIndex = -1; 
            tbCantidad.Clear();           
        }
        public FormDetalleBoleta(int boletaId, int sucursalId)
        {
            InitializeComponent();
            this.boletaId = boletaId;
            MostrarDetalleBoleta(nDetalleBoleta.ListarTodoFisico(this.boletaId));
            MostrarProductos(nProducto.ListarTodoLogico());
            this.sucursalId = sucursalId;
            this.inventarioId = nInventario.ObtenerSucursalInventario(this.sucursalId).idInventario;
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
                lblTotal.Text = nDetalleBoleta.CalcularTotal(nBoleta.ObtenerBoleta(this.boletaId), this.boletaId).ToString("C2"); // Formato de moneda
                dgDetalleBoleta.Columns["Boleta"].Visible = false;
                dgDetalleBoleta.Columns["Producto"].Visible = false;
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
            if (tbCantidad.Text == "" || cbProducto.Text == "")
            {
                MessageBox.Show("Ingrese los campos requeridos");
                return;
            }
            int idProducto = int.Parse(cbProducto.SelectedValue.ToString());

            int cantidad = 0;
            try
            {
                cantidad = int.Parse(tbCantidad.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ingrese los campos numericos correctamente");
                return;
            }

            if (cantidad < 1)
            {
                MessageBox.Show("La cantidad debe ser positiva");
                return;
            }

            DetalleBoleta detalleBoleta = new DetalleBoleta();
            detalleBoleta.idBoleta = this.boletaId;
            detalleBoleta.idProducto = idProducto;
            detalleBoleta.CantidadProducto = cantidad;
            detalleBoleta.Subtotal = nDetalleBoleta.CalcularSubTotal(idProducto, cantidad);
            detalleBoleta.UsuarioCreadorId = NTrabajador.trabajadorLogueado.idTrabajador;
            detalleBoleta.FechaCreacion = DateTime.Now;
            detalleBoleta.UsuarioModificadorId = NTrabajador.trabajadorLogueado.idTrabajador;
            detalleBoleta.FechaModificacion = DateTime.Now;

            // Verificar existencia en ProductoInventario
            Producto producto = nProducto.ObtenerProductoPorId(idProducto);
            Sucursal sucursal = nSucursal.ObtenerSucursalPorId(this.sucursalId);
            ProductoInventario productoInventarioTemp = nProductoInventario.VerificarExistenciaProductoInventario(producto, sucursal);
            if (productoInventarioTemp == null)
            {
                MessageBox.Show("La sucursal no cuenta con este producto");
                return;
            }
            else
            {
                int existe = nDetalleBoleta.BoletaConProducto(idProducto, this.boletaId);
                if (existe == 0)
                {
                    MessageBox.Show("Este producto ya se encuentra registrado en la boleta, intenta modificarlo");
                    return;
                }
                else
                {
                    int actualizar = nProductoInventario.ActualizarStockRegistrar(productoInventarioTemp, cantidad);
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
                        String mensaje = nDetalleBoleta.Registrar(detalleBoleta);
                        MessageBox.Show(mensaje);
                        Boleta boleta = nBoleta.ObtenerBoleta(boletaId);
                        boleta.Total = nDetalleBoleta.CalcularTotal(boleta, boletaId);
                        lblTotal.Text = boleta.Total.ToString("C2"); // Formato de moneda
                        MostrarDetalleBoleta(nDetalleBoleta.ListarTodoFisico(this.boletaId));
                        LimpiarCampos();
                    }
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgDetalleBoleta.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un detalle de boleta");
                return;
            }

            int detalleBoletaId = int.Parse(dgDetalleBoleta.SelectedRows[0].Cells[0].Value.ToString());

            DialogResult confirmacion = MessageBox.Show(
                "¿Está seguro de eliminar este detalle de boleta?",
                "Confirmar Eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirmacion == DialogResult.Yes)
            {
                DetalleBoleta detalleBoletaTemp = nDetalleBoleta.ObtenerPorId(detalleBoletaId);
                int idProducto = detalleBoletaTemp.idProducto;
                int cantidad = detalleBoletaTemp.CantidadProducto;

                // Verificar Existencia en ProductoInventario
                Producto producto = nProducto.ObtenerProductoPorId(idProducto);
                Sucursal sucursal = nSucursal.ObtenerSucursalPorId(this.sucursalId);
                ProductoInventario productoInventarioTemp = nProductoInventario.VerificarExistenciaProductoInventario(producto, sucursal);
                int actualizar = nProductoInventario.ActualizarStockEliminar(productoInventarioTemp, cantidad);

                if (actualizar == -1)
                {
                    MessageBox.Show("Se produjo un error");
                    return;
                }
                else if (actualizar > 0)
                {
                    String mensaje = nDetalleBoleta.EliminarFisico(detalleBoletaId);
                    Boleta boleta = nBoleta.ObtenerBoleta(boletaId);
                    boleta.Total = nDetalleBoleta.CalcularTotal(boleta, boletaId);
                    MessageBox.Show(mensaje);
                    MostrarDetalleBoleta(nDetalleBoleta.ListarTodoFisico(this.boletaId));
                }
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgDetalleBoleta.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un detalle de boleta");
                return;
            }

            int dBoletaID = int.Parse(dgDetalleBoleta.SelectedRows[0].Cells[0].Value.ToString());

            DetalleBoleta dBoletaTemp = nDetalleBoleta.ObtenerPorId(dBoletaID);
            int idProductoTemp = dBoletaTemp.idProducto;
            int cantidadTemp = dBoletaTemp.CantidadProducto;

            // Verificar existencia en ProductoInventario
            Producto productoTemp = nProducto.ObtenerProductoPorId(idProductoTemp);
            Sucursal sucursalTemp = nSucursal.ObtenerSucursalPorId(this.sucursalId);
            ProductoInventario productoInventarioTemp = nProductoInventario.VerificarExistenciaProductoInventario(productoTemp, sucursalTemp);
            nProductoInventario.ActualizarStockEliminar(productoInventarioTemp, cantidadTemp);

            if (tbCantidad.Text == "" || cbProducto.Text == "")
            {
                MessageBox.Show("Ingrese los campos requeridos");
                return;
            }

            int idProducto = int.Parse(cbProducto.SelectedValue.ToString());
            int cantidad = 0;
            try
            {
                cantidad = int.Parse(tbCantidad.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ingrese los campos numéricos correctamente");
                return;
            }

            if (cantidad < 1)
            {
                MessageBox.Show("La cantidad debe ser positiva");
                return;
            }

            // Si el producto seleccionado es el mismo que el que se desea modificar, permitir la actualización
            if (idProducto == idProductoTemp)
            {
                // Actualizar solo la cantidad y el subtotal
                DetalleBoleta detalleBoleta = new DetalleBoleta
                {
                    idDetalleBoleta = dBoletaID,
                    idBoleta = this.boletaId,
                    idProducto = idProducto,
                    CantidadProducto = cantidad,
                    Subtotal = nDetalleBoleta.CalcularSubTotal(idProducto, cantidad),
                    UsuarioModificadorId = NTrabajador.trabajadorLogueado.idTrabajador,
                    FechaModificacion = DateTime.Now
                };

                int actualizar = nProductoInventario.ActualizarStockRegistrar(productoInventarioTemp, cantidad - cantidadTemp);
                if (actualizar == 0)
                {
                    MessageBox.Show("El producto no cuenta con stock suficiente");
                    return;
                }

                if (actualizar > 0)
                {
                    String mensaje = nDetalleBoleta.Modificar(detalleBoleta);
                    Boleta boleta = nBoleta.ObtenerBoleta(boletaId);
                    boleta.Total = nDetalleBoleta.CalcularTotal(boleta, boletaId);
                    MessageBox.Show(mensaje);
                    MostrarDetalleBoleta(nDetalleBoleta.ListarTodoFisico(this.boletaId));
                    LimpiarCampos();
                }
            }
            else
            {
                MessageBox.Show("Solo puedes modificar la cantidad del producto seleccionado en la boleta");
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
           // Inventario inventario = nInventario.ObtenerInventario(this.inventarioId);
           // int totalStock = nProductoInventario.CalcularStockTotal(nInventario.ObtenerInventario(this.inventarioId), this.inventarioId);
           // FormLBoletas form = new FormLBoletas();
           // form.Show();
            this.Close();
        }
    }
}
