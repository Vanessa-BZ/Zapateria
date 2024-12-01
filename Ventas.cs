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
    public partial class Ventas : Form
    {
        private Form FormularioVentasActual;
        private Panel leftBorderBtn;
        private IconButton currentBtn;

        public Ventas()
        {
            InitializeComponent();
            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, 60);
            panel1.Controls.Add(leftBorderBtn);

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
        private void AbrirFormularioVentas(Form FormularioVentas)
        {
            if (FormularioVentasActual != null)
            {
                FormularioVentasActual.Close();
            }
            FormularioVentasActual = FormularioVentas;
            FormularioVentas.TopLevel = false;
            FormularioVentas.FormBorderStyle = FormBorderStyle.None;
            FormularioVentas.Dock = DockStyle.Fill;
            panelventas.Controls.Add(FormularioVentas);
            panelventas.Tag = FormularioVentas;
            FormularioVentas.BringToFront();
            FormularioVentas.Show();

        }
        private void btnVagregar_Click(object sender, EventArgs e)
        {
            ActivateButtom(sender, Color.Purple);
            AbrirFormularioVentas(new VentasAgregar());
        }

        private void btnVmodificar_Click(object sender, EventArgs e)
        {
            ActivateButtom(sender, Color.Purple);
            AbrirFormularioVentas(new VentasModificar());
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            ActivateButtom(sender, Color.Purple);
            AbrirFormularioVentas(new VentasEliminar());
        }
        private void Reset()
        {
            DisableButtom();
            leftBorderBtn.Visible = false;
            //iconFormularioHijoActual.IconChar = IconChar.Home;
            //iconFormularioHijoActual.IconColor = Color.MediumPurple;
           // labelFormularioHijo.Text = "Home";
        }
    }
}
