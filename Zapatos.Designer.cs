namespace ConexionSQL
{
    partial class Zapatos
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
            this.label2 = new System.Windows.Forms.Label();
            this.btnZeliminar = new FontAwesome.Sharp.IconButton();
            this.btnZmodificar = new FontAwesome.Sharp.IconButton();
            this.btnZagregar = new FontAwesome.Sharp.IconButton();
            this.panelZapatos = new System.Windows.Forms.Panel();
            this.panelProveedor.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(362, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Zapatos";
            // 
            // panelProveedor
            // 
            this.panelProveedor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.panelProveedor.Controls.Add(this.panel2);
            this.panelProveedor.Controls.Add(this.btnZeliminar);
            this.panelProveedor.Controls.Add(this.btnZmodificar);
            this.panelProveedor.Controls.Add(this.btnZagregar);
            this.panelProveedor.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelProveedor.Location = new System.Drawing.Point(674, 0);
            this.panelProveedor.Name = "panelProveedor";
            this.panelProveedor.Size = new System.Drawing.Size(126, 450);
            this.panelProveedor.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(126, 100);
            this.panel2.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 16);
            this.label2.TabIndex = 0;
            // 
            // btnZeliminar
            // 
            this.btnZeliminar.FlatAppearance.BorderSize = 0;
            this.btnZeliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnZeliminar.Font = new System.Drawing.Font("Morning Rainbow", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnZeliminar.ForeColor = System.Drawing.Color.White;
            this.btnZeliminar.IconChar = FontAwesome.Sharp.IconChar.Trash;
            this.btnZeliminar.IconColor = System.Drawing.Color.White;
            this.btnZeliminar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnZeliminar.IconSize = 20;
            this.btnZeliminar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnZeliminar.Location = new System.Drawing.Point(9, 314);
            this.btnZeliminar.Name = "btnZeliminar";
            this.btnZeliminar.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btnZeliminar.Size = new System.Drawing.Size(163, 96);
            this.btnZeliminar.TabIndex = 7;
            this.btnZeliminar.Text = "Eliminar";
            this.btnZeliminar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnZeliminar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnZeliminar.UseVisualStyleBackColor = true;
            this.btnZeliminar.Click += new System.EventHandler(this.btnZeliminar_Click);
            // 
            // btnZmodificar
            // 
            this.btnZmodificar.FlatAppearance.BorderSize = 0;
            this.btnZmodificar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnZmodificar.Font = new System.Drawing.Font("Morning Rainbow", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnZmodificar.ForeColor = System.Drawing.Color.White;
            this.btnZmodificar.IconChar = FontAwesome.Sharp.IconChar.Pen;
            this.btnZmodificar.IconColor = System.Drawing.Color.White;
            this.btnZmodificar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnZmodificar.IconSize = 20;
            this.btnZmodificar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnZmodificar.Location = new System.Drawing.Point(6, 212);
            this.btnZmodificar.Name = "btnZmodificar";
            this.btnZmodificar.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btnZmodificar.Size = new System.Drawing.Size(166, 96);
            this.btnZmodificar.TabIndex = 6;
            this.btnZmodificar.Text = "Modificar";
            this.btnZmodificar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnZmodificar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnZmodificar.UseVisualStyleBackColor = true;
            this.btnZmodificar.Click += new System.EventHandler(this.btnZmodificar_Click);
            // 
            // btnZagregar
            // 
            this.btnZagregar.FlatAppearance.BorderSize = 0;
            this.btnZagregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnZagregar.Font = new System.Drawing.Font("Morning Rainbow", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnZagregar.ForeColor = System.Drawing.Color.White;
            this.btnZagregar.IconChar = FontAwesome.Sharp.IconChar.Plus;
            this.btnZagregar.IconColor = System.Drawing.Color.White;
            this.btnZagregar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnZagregar.IconSize = 20;
            this.btnZagregar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnZagregar.Location = new System.Drawing.Point(6, 110);
            this.btnZagregar.Name = "btnZagregar";
            this.btnZagregar.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btnZagregar.Size = new System.Drawing.Size(166, 96);
            this.btnZagregar.TabIndex = 5;
            this.btnZagregar.Text = "Agregar";
            this.btnZagregar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnZagregar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnZagregar.UseVisualStyleBackColor = true;
            this.btnZagregar.Click += new System.EventHandler(this.btnZagregar_Click);
            // 
            // panelZapatos
            // 
            this.panelZapatos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelZapatos.Location = new System.Drawing.Point(0, 0);
            this.panelZapatos.Name = "panelZapatos";
            this.panelZapatos.Size = new System.Drawing.Size(674, 450);
            this.panelZapatos.TabIndex = 5;
            // 
            // Zapatos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Plum;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelZapatos);
            this.Controls.Add(this.panelProveedor);
            this.Controls.Add(this.label1);
            this.Name = "Zapatos";
            this.Text = "Eliminar";
            this.panelProveedor.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelProveedor;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private FontAwesome.Sharp.IconButton btnZeliminar;
        private FontAwesome.Sharp.IconButton btnZmodificar;
        private FontAwesome.Sharp.IconButton btnZagregar;
        private System.Windows.Forms.Panel panelZapatos;
    }
}