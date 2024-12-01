namespace ConexionSQL
{
    partial class Ventas
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panelventas = new System.Windows.Forms.Panel();
            this.btnEliminar = new FontAwesome.Sharp.IconButton();
            this.btnVmodificar = new FontAwesome.Sharp.IconButton();
            this.btnVagregar = new FontAwesome.Sharp.IconButton();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.btnEliminar);
            this.panel1.Controls.Add(this.btnVmodificar);
            this.panel1.Controls.Add(this.btnVagregar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(621, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(179, 450);
            this.panel1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(179, 100);
            this.panel2.TabIndex = 8;
            // 
            // panelventas
            // 
            this.panelventas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelventas.Location = new System.Drawing.Point(0, 0);
            this.panelventas.Name = "panelventas";
            this.panelventas.Size = new System.Drawing.Size(621, 450);
            this.panelventas.TabIndex = 2;
            // 
            // btnEliminar
            // 
            this.btnEliminar.FlatAppearance.BorderSize = 0;
            this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminar.Font = new System.Drawing.Font("Morning Rainbow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminar.ForeColor = System.Drawing.Color.White;
            this.btnEliminar.IconChar = FontAwesome.Sharp.IconChar.Trash;
            this.btnEliminar.IconColor = System.Drawing.Color.White;
            this.btnEliminar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnEliminar.IconSize = 32;
            this.btnEliminar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEliminar.Location = new System.Drawing.Point(9, 314);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btnEliminar.Size = new System.Drawing.Size(163, 96);
            this.btnEliminar.TabIndex = 7;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEliminar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnVmodificar
            // 
            this.btnVmodificar.FlatAppearance.BorderSize = 0;
            this.btnVmodificar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVmodificar.Font = new System.Drawing.Font("Morning Rainbow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVmodificar.ForeColor = System.Drawing.Color.White;
            this.btnVmodificar.IconChar = FontAwesome.Sharp.IconChar.Pen;
            this.btnVmodificar.IconColor = System.Drawing.Color.White;
            this.btnVmodificar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnVmodificar.IconSize = 32;
            this.btnVmodificar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVmodificar.Location = new System.Drawing.Point(6, 212);
            this.btnVmodificar.Name = "btnVmodificar";
            this.btnVmodificar.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btnVmodificar.Size = new System.Drawing.Size(166, 96);
            this.btnVmodificar.TabIndex = 6;
            this.btnVmodificar.Text = "Modificar";
            this.btnVmodificar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVmodificar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnVmodificar.UseVisualStyleBackColor = true;
            this.btnVmodificar.Click += new System.EventHandler(this.btnVmodificar_Click);
            // 
            // btnVagregar
            // 
            this.btnVagregar.FlatAppearance.BorderSize = 0;
            this.btnVagregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVagregar.Font = new System.Drawing.Font("Morning Rainbow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVagregar.ForeColor = System.Drawing.Color.White;
            this.btnVagregar.IconChar = FontAwesome.Sharp.IconChar.Plus;
            this.btnVagregar.IconColor = System.Drawing.Color.White;
            this.btnVagregar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnVagregar.IconSize = 32;
            this.btnVagregar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVagregar.Location = new System.Drawing.Point(6, 110);
            this.btnVagregar.Name = "btnVagregar";
            this.btnVagregar.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btnVagregar.Size = new System.Drawing.Size(166, 96);
            this.btnVagregar.TabIndex = 5;
            this.btnVagregar.Text = "Agregar";
            this.btnVagregar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVagregar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnVagregar.UseVisualStyleBackColor = true;
            this.btnVagregar.Click += new System.EventHandler(this.btnVagregar_Click);
            // 
            // Ventas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MediumOrchid;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelventas);
            this.Controls.Add(this.panel1);
            this.Name = "Ventas";
            this.Text = "Agregar";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private FontAwesome.Sharp.IconButton btnEliminar;
        private FontAwesome.Sharp.IconButton btnVmodificar;
        private FontAwesome.Sharp.IconButton btnVagregar;
        private System.Windows.Forms.Panel panelventas;
    }
}