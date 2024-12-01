using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;
using System.Runtime.InteropServices;

namespace ConexionSQL
{
    public partial class Proveedores : Form
    {
        private Form FormularioProveedoresActual;
        private Panel leftBorderBtn;
        private IconButton currentBtn;

        public Proveedores()
        {
            InitializeComponent();
            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, 60);

            //form
            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;

        }
        private void ActivateButtom(object senderBtn, Color color)
        {
            if (senderBtn != null)
            {
                DisableButtom();
                currentBtn = (IconButton)senderBtn;
                currentBtn.BackColor = Color.FromArgb(240, 128, 128);
                currentBtn.ForeColor = color;
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.IconColor = color;
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentBtn.ImageAlign = ContentAlignment.MiddleRight;
                //borde de boton
                leftBorderBtn.BackColor = color;
                leftBorderBtn.Location = new Point(0, currentBtn.Location.Y);
                leftBorderBtn.Visible = true;
                leftBorderBtn.BringToFront();
                //icon current formulario hijo 
                // iconFormularioHijoActual.IconChar = currentBtn.IconChar;
                //iconFormularioHijoActual.IconColor = color;
            }
        }

        private void DisableButtom()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = Color.FromArgb(255, 128, 128);
                currentBtn.ForeColor = Color.White;
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.IconColor = Color.White;
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }
        private void AbrirFormularioProveedores(Form FormularioProveedores)
        {
            if (FormularioProveedoresActual != null)
            {
                FormularioProveedoresActual.Close();
            }
            FormularioProveedoresActual = FormularioProveedores;
            FormularioProveedores.TopLevel = false;
            FormularioProveedores.FormBorderStyle = FormBorderStyle.None;
            FormularioProveedores.Dock = DockStyle.Fill;
            panelProveedores.Controls.Add(FormularioProveedores);
            panelProveedores.Tag = FormularioProveedores;
            FormularioProveedores.BringToFront();
            FormularioProveedores.Show();

        }
    }
}
