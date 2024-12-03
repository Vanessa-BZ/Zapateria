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
    public partial class Form1 : Form
    {
        //Fields
        private IconButton currentBtn; 
        private Panel leftBorderBtn;
        private Form FormularioHijoActual;
        public Form1()
        {
            InitializeComponent();
            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7,60);
            panelMenu.Controls.Add(leftBorderBtn);

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
                currentBtn =(IconButton)senderBtn;
                currentBtn.BackColor = Color.FromArgb(240, 128, 128);
                currentBtn.ForeColor = color;
                currentBtn.TextAlign=ContentAlignment.MiddleCenter;
                currentBtn.IconColor=color;
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentBtn.ImageAlign=ContentAlignment.MiddleRight;
                //borde de boton
                leftBorderBtn.BackColor=color;
                leftBorderBtn.Location=new Point(0,currentBtn.Location.Y);
                leftBorderBtn.Visible=true;
                leftBorderBtn.BringToFront();
                //icon current formulario hijo 
                iconFormularioHijoActual.IconChar = currentBtn.IconChar;
                iconFormularioHijoActual.IconColor= color;
            }
        }

        private void  DisableButtom()
        {
            if(currentBtn != null)
            {
                currentBtn.BackColor = Color.FromArgb(255, 128, 128);
                currentBtn.ForeColor = Color.White;
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.IconColor = Color.White;
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }
        private void AbrirFormularioHijo(Form FormularioHijo)
        {
            if(FormularioHijoActual != null)
            {
                FormularioHijoActual.Close();
            }
            FormularioHijoActual = FormularioHijo;
            FormularioHijo.TopLevel = false;
            FormularioHijo.FormBorderStyle = FormBorderStyle.None;
            FormularioHijo.Dock = DockStyle.Fill;
            panelEscritorio.Controls.Add(FormularioHijo);
            panelEscritorio.Tag = FormularioHijo;
            FormularioHijo.BringToFront();
            FormularioHijo.Show();
            labelFormularioHijo.Text = FormularioHijo.Text;

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            ConexionBD conexion=new ConexionBD();
            conexion.abrir();
        }

        private void btnVentas_Click(object sender, EventArgs e)
        {
            ActivateButtom(sender, Color.Purple);
            AbrirFormularioHijo(new Ventas());
        }

        private void btnProveedores_Click(object sender, EventArgs e)
        {
            ActivateButtom(sender, Color.MediumPurple);
            AbrirFormularioHijo(new Proveedores());
        }

        private void btnZapatos_Click(object sender, EventArgs e)
        {
            ActivateButtom(sender, Color.Purple);
            AbrirFormularioHijo(new Zapatos());
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            FormularioHijoActual.Close();
            Reset();
        }
        private void Reset()
        {
            DisableButtom();
            leftBorderBtn.Visible = false;
            iconFormularioHijoActual.IconChar = IconChar.Home;
            iconFormularioHijoActual.IconColor = Color.MediumPurple;
            labelFormularioHijo.Text = "Home";
        }

        private void labelFormularioHijo_MouseDown(object sender, MouseEventArgs e)
        {

        }
        //Drag Form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void panelBarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
