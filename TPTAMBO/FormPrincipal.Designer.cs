namespace TPTAMBO
{
    partial class FormPrincipal
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPrincipal));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.registroToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trabajadorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sucursalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.categoríaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.productoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gestionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.boletasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inventarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.productosMásVendidosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sucursalesConMásVentasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verVentasPorTrabajadorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.categoríasMásVendidasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.productosConStockBajoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSalir = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblHora = new System.Windows.Forms.Label();
            this.lblFecha = new System.Windows.Forms.Label();
            this.lblNombreTrabajador = new System.Windows.Forms.Label();
            this.timerReloj = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Left;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(18, 18);
            this.menuStrip1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.registroToolStripMenuItem,
            this.gestionToolStripMenuItem,
            this.reportesToolStripMenuItem});
            this.menuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(6, 2, 2, 2);
            this.menuStrip1.Size = new System.Drawing.Size(150, 1007);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // registroToolStripMenuItem
            // 
            this.registroToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.trabajadorToolStripMenuItem,
            this.sucursalToolStripMenuItem,
            this.categoríaToolStripMenuItem,
            this.productoToolStripMenuItem});
            this.registroToolStripMenuItem.Font = new System.Drawing.Font("Dubai Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.registroToolStripMenuItem.Image = global::TPTAMBO.Properties.Resources.registro;
            this.registroToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.registroToolStripMenuItem.Name = "registroToolStripMenuItem";
            this.registroToolStripMenuItem.Padding = new System.Windows.Forms.Padding(4, 0, 4, 10);
            this.registroToolStripMenuItem.Size = new System.Drawing.Size(133, 64);
            this.registroToolStripMenuItem.Text = "&Registro";
            // 
            // trabajadorToolStripMenuItem
            // 
            this.trabajadorToolStripMenuItem.Name = "trabajadorToolStripMenuItem";
            this.trabajadorToolStripMenuItem.Size = new System.Drawing.Size(167, 32);
            this.trabajadorToolStripMenuItem.Text = "&Trabajador";
            this.trabajadorToolStripMenuItem.Click += new System.EventHandler(this.trabajadorToolStripMenuItem_Click);
            // 
            // sucursalToolStripMenuItem
            // 
            this.sucursalToolStripMenuItem.Name = "sucursalToolStripMenuItem";
            this.sucursalToolStripMenuItem.Size = new System.Drawing.Size(167, 32);
            this.sucursalToolStripMenuItem.Text = "&Sucursal";
            this.sucursalToolStripMenuItem.Click += new System.EventHandler(this.sucursalToolStripMenuItem_Click);
            // 
            // categoríaToolStripMenuItem
            // 
            this.categoríaToolStripMenuItem.Name = "categoríaToolStripMenuItem";
            this.categoríaToolStripMenuItem.Size = new System.Drawing.Size(167, 32);
            this.categoríaToolStripMenuItem.Text = "&Categoría";
            this.categoríaToolStripMenuItem.Click += new System.EventHandler(this.categoríaToolStripMenuItem_Click);
            // 
            // productoToolStripMenuItem
            // 
            this.productoToolStripMenuItem.Name = "productoToolStripMenuItem";
            this.productoToolStripMenuItem.Size = new System.Drawing.Size(167, 32);
            this.productoToolStripMenuItem.Text = "&Producto";
            this.productoToolStripMenuItem.Click += new System.EventHandler(this.productoToolStripMenuItem_Click);
            // 
            // gestionToolStripMenuItem
            // 
            this.gestionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.boletasToolStripMenuItem,
            this.inventarioToolStripMenuItem});
            this.gestionToolStripMenuItem.Font = new System.Drawing.Font("Dubai Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gestionToolStripMenuItem.Image = global::TPTAMBO.Properties.Resources.listas;
            this.gestionToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.gestionToolStripMenuItem.Name = "gestionToolStripMenuItem";
            this.gestionToolStripMenuItem.Padding = new System.Windows.Forms.Padding(4, 0, 4, 10);
            this.gestionToolStripMenuItem.Size = new System.Drawing.Size(133, 64);
            this.gestionToolStripMenuItem.Text = "&Gestión";
            this.gestionToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // boletasToolStripMenuItem
            // 
            this.boletasToolStripMenuItem.Name = "boletasToolStripMenuItem";
            this.boletasToolStripMenuItem.Size = new System.Drawing.Size(161, 32);
            this.boletasToolStripMenuItem.Text = "&Boletas";
            this.boletasToolStripMenuItem.Click += new System.EventHandler(this.boletasToolStripMenuItem_Click);
            // 
            // inventarioToolStripMenuItem
            // 
            this.inventarioToolStripMenuItem.Name = "inventarioToolStripMenuItem";
            this.inventarioToolStripMenuItem.Size = new System.Drawing.Size(161, 32);
            this.inventarioToolStripMenuItem.Text = "&Inventario";
            this.inventarioToolStripMenuItem.Click += new System.EventHandler(this.inventarioToolStripMenuItem_Click);
            // 
            // reportesToolStripMenuItem
            // 
            this.reportesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.productosMásVendidosToolStripMenuItem,
            this.sucursalesConMásVentasToolStripMenuItem,
            this.verVentasPorTrabajadorToolStripMenuItem,
            this.categoríasMásVendidasToolStripMenuItem,
            this.productosConStockBajoToolStripMenuItem});
            this.reportesToolStripMenuItem.Font = new System.Drawing.Font("Dubai Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reportesToolStripMenuItem.Image = global::TPTAMBO.Properties.Resources.reportes;
            this.reportesToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.reportesToolStripMenuItem.Name = "reportesToolStripMenuItem";
            this.reportesToolStripMenuItem.Padding = new System.Windows.Forms.Padding(4, 0, 4, 10);
            this.reportesToolStripMenuItem.Size = new System.Drawing.Size(133, 64);
            this.reportesToolStripMenuItem.Text = "Reporte&s";
            // 
            // productosMásVendidosToolStripMenuItem
            // 
            this.productosMásVendidosToolStripMenuItem.Name = "productosMásVendidosToolStripMenuItem";
            this.productosMásVendidosToolStripMenuItem.Size = new System.Drawing.Size(307, 32);
            this.productosMásVendidosToolStripMenuItem.Text = "&1. Productos más vendidos";
            this.productosMásVendidosToolStripMenuItem.Click += new System.EventHandler(this.productosMásVendidosToolStripMenuItem_Click);
            // 
            // sucursalesConMásVentasToolStripMenuItem
            // 
            this.sucursalesConMásVentasToolStripMenuItem.Name = "sucursalesConMásVentasToolStripMenuItem";
            this.sucursalesConMásVentasToolStripMenuItem.Size = new System.Drawing.Size(307, 32);
            this.sucursalesConMásVentasToolStripMenuItem.Text = "&2. Sucursales con más ventas";
            this.sucursalesConMásVentasToolStripMenuItem.Click += new System.EventHandler(this.sucursalesConMásVentasToolStripMenuItem_Click);
            // 
            // verVentasPorTrabajadorToolStripMenuItem
            // 
            this.verVentasPorTrabajadorToolStripMenuItem.Name = "verVentasPorTrabajadorToolStripMenuItem";
            this.verVentasPorTrabajadorToolStripMenuItem.Size = new System.Drawing.Size(307, 32);
            this.verVentasPorTrabajadorToolStripMenuItem.Text = "&3. Ver ventas por Trabajador";
            this.verVentasPorTrabajadorToolStripMenuItem.Click += new System.EventHandler(this.verVentasPorTrabajadorToolStripMenuItem_Click);
            // 
            // categoríasMásVendidasToolStripMenuItem
            // 
            this.categoríasMásVendidasToolStripMenuItem.Name = "categoríasMásVendidasToolStripMenuItem";
            this.categoríasMásVendidasToolStripMenuItem.Size = new System.Drawing.Size(307, 32);
            this.categoríasMásVendidasToolStripMenuItem.Text = "&4. Categorías más vendidas";
            this.categoríasMásVendidasToolStripMenuItem.Click += new System.EventHandler(this.categoríasMásVendidasToolStripMenuItem_Click);
            // 
            // productosConStockBajoToolStripMenuItem
            // 
            this.productosConStockBajoToolStripMenuItem.Name = "productosConStockBajoToolStripMenuItem";
            this.productosConStockBajoToolStripMenuItem.Size = new System.Drawing.Size(307, 32);
            this.productosConStockBajoToolStripMenuItem.Text = "&5. Productos con stock bajo";
            this.productosConStockBajoToolStripMenuItem.Click += new System.EventHandler(this.productosConStockBajoToolStripMenuItem_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Font = new System.Drawing.Font("Dubai Medium", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnSalir.Image = global::TPTAMBO.Properties.Resources.salir;
            this.btnSalir.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSalir.Location = new System.Drawing.Point(12, 918);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(123, 82);
            this.btnSalir.TabIndex = 3;
            this.btnSalir.Text = "Cerrar Sesión";
            this.btnSalir.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::TPTAMBO.Properties.Resources.Sin_título__1315_x_150_px___1_;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.lblHora);
            this.panel1.Controls.Add(this.lblFecha);
            this.panel1.Controls.Add(this.lblNombreTrabajador);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(150, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1315, 150);
            this.panel1.TabIndex = 5;
            // 
            // lblHora
            // 
            this.lblHora.AutoSize = true;
            this.lblHora.BackColor = System.Drawing.Color.Transparent;
            this.lblHora.Font = new System.Drawing.Font("Dubai Medium", 10F, System.Drawing.FontStyle.Bold);
            this.lblHora.Location = new System.Drawing.Point(395, 7);
            this.lblHora.Name = "lblHora";
            this.lblHora.Size = new System.Drawing.Size(16, 24);
            this.lblHora.TabIndex = 9;
            this.lblHora.Text = "-";
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.BackColor = System.Drawing.Color.Transparent;
            this.lblFecha.Font = new System.Drawing.Font("Dubai Medium", 10F, System.Drawing.FontStyle.Bold);
            this.lblFecha.Location = new System.Drawing.Point(29, 7);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(16, 24);
            this.lblFecha.TabIndex = 8;
            this.lblFecha.Text = "-";
            // 
            // lblNombreTrabajador
            // 
            this.lblNombreTrabajador.BackColor = System.Drawing.Color.Transparent;
            this.lblNombreTrabajador.Font = new System.Drawing.Font("Lucida Calligraphy", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombreTrabajador.ForeColor = System.Drawing.Color.Khaki;
            this.lblNombreTrabajador.Location = new System.Drawing.Point(27, 116);
            this.lblNombreTrabajador.Name = "lblNombreTrabajador";
            this.lblNombreTrabajador.Size = new System.Drawing.Size(363, 34);
            this.lblNombreTrabajador.TabIndex = 7;
            this.lblNombreTrabajador.Text = "username";
            this.lblNombreTrabajador.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // timerReloj
            // 
            this.timerReloj.Interval = 1000;
            this.timerReloj.Tick += new System.EventHandler(this.timerReloj_Tick);
            // 
            // FormPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1465, 1007);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormPrincipal";
            this.Text = "Tambo Home";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormPrincipal_Load_1);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem registroToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem trabajadorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sucursalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem categoríaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem productoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gestionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem boletasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inventarioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem productosMásVendidosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sucursalesConMásVentasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem verVentasPorTrabajadorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem categoríasMásVendidasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem productosConStockBajoToolStripMenuItem;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblNombreTrabajador;
        private System.Windows.Forms.Label lblHora;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.Timer timerReloj;
    }
}

