namespace ConexionSQL
{
    partial class Proveedores
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.panelProveedor = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnPeliminar = new FontAwesome.Sharp.IconButton();
            this.btnPmodificar = new FontAwesome.Sharp.IconButton();
            this.btnPagregar = new FontAwesome.Sharp.IconButton();
            this.panelProveedores = new System.Windows.Forms.Panel();
            this.panelProveedor.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 16);
            this.label1.TabIndex = 0;
            // 
            // panelProveedor
            // 
            this.panelProveedor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.panelProveedor.Controls.Add(this.panel2);
            this.panelProveedor.Controls.Add(this.btnPeliminar);
            this.panelProveedor.Controls.Add(this.btnPmodificar);
            this.panelProveedor.Controls.Add(this.btnPagregar);
            this.panelProveedor.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelProveedor.Location = new System.Drawing.Point(621, 0);
            this.panelProveedor.Name = "panelProveedor";
            this.panelProveedor.Size = new System.Drawing.Size(179, 450);
            this.panelProveedor.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(179, 100);
            this.panel2.TabIndex = 8;
            // 
            // btnPeliminar
            // 
            this.btnPeliminar.FlatAppearance.BorderSize = 0;
            this.btnPeliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPeliminar.Font = new System.Drawing.Font("Morning Rainbow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPeliminar.ForeColor = System.Drawing.Color.White;
            this.btnPeliminar.IconChar = FontAwesome.Sharp.IconChar.Trash;
            this.btnPeliminar.IconColor = System.Drawing.Color.White;
            this.btnPeliminar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnPeliminar.IconSize = 32;
            this.btnPeliminar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPeliminar.Location = new System.Drawing.Point(9, 314);
            this.btnPeliminar.Name = "btnPeliminar";
            this.btnPeliminar.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btnPeliminar.Size = new System.Drawing.Size(163, 96);
            this.btnPeliminar.TabIndex = 7;
            this.btnPeliminar.Text = "Eliminar";
            this.btnPeliminar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPeliminar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPeliminar.UseVisualStyleBackColor = true;
            this.btnPeliminar.Click += new System.EventHandler(this.btnPeliminar_Click);
            // 
            // btnPmodificar
            // 
            this.btnPmodificar.FlatAppearance.BorderSize = 0;
            this.btnPmodificar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPmodificar.Font = new System.Drawing.Font("Morning Rainbow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPmodificar.ForeColor = System.Drawing.Color.White;
            this.btnPmodificar.IconChar = FontAwesome.Sharp.IconChar.Pen;
            this.btnPmodificar.IconColor = System.Drawing.Color.White;
            this.btnPmodificar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnPmodificar.IconSize = 32;
            this.btnPmodificar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPmodificar.Location = new System.Drawing.Point(6, 212);
            this.btnPmodificar.Name = "btnPmodificar";
            this.btnPmodificar.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btnPmodificar.Size = new System.Drawing.Size(166, 96);
            this.btnPmodificar.TabIndex = 6;
            this.btnPmodificar.Text = "Modificar";
            this.btnPmodificar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPmodificar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPmodificar.UseVisualStyleBackColor = true;
            this.btnPmodificar.Click += new System.EventHandler(this.btnPmodificar_Click);
            // 
            // btnPagregar
            // 
            this.btnPagregar.FlatAppearance.BorderSize = 0;
            this.btnPagregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPagregar.Font = new System.Drawing.Font("Morning Rainbow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPagregar.ForeColor = System.Drawing.Color.White;
            this.btnPagregar.IconChar = FontAwesome.Sharp.IconChar.Plus;
            this.btnPagregar.IconColor = System.Drawing.Color.White;
            this.btnPagregar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnPagregar.IconSize = 32;
            this.btnPagregar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPagregar.Location = new System.Drawing.Point(6, 110);
            this.btnPagregar.Name = "btnPagregar";
            this.btnPagregar.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btnPagregar.Size = new System.Drawing.Size(166, 96);
            this.btnPagregar.TabIndex = 5;
            this.btnPagregar.Text = "Agregar";
            this.btnPagregar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPagregar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPagregar.UseVisualStyleBackColor = true;
            this.btnPagregar.Click += new System.EventHandler(this.btnPagregar_Click);
            // 
            // panelProveedores
            // 
            this.panelProveedores.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelProveedores.Location = new System.Drawing.Point(0, 0);
            this.panelProveedores.Name = "panelProveedores";
            this.panelProveedores.Size = new System.Drawing.Size(621, 450);
            this.panelProveedores.TabIndex = 4;
            // 
            // Proveedores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Thistle;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelProveedores);
            this.Controls.Add(this.panelProveedor);
            this.Name = "Proveedores";
            this.Text = "Modificar";
            this.panelProveedor.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelProveedor;
        private System.Windows.Forms.Panel panel2;
        private FontAwesome.Sharp.IconButton btnPeliminar;
        private FontAwesome.Sharp.IconButton btnPmodificar;
        private FontAwesome.Sharp.IconButton btnPagregar;
        private System.Windows.Forms.Panel panelProveedores;
    }
}