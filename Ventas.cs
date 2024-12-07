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
        private void Reset()
        {
            DisableButtom();
            leftBorderBtn.Visible = false;

        }
        Random id = new Random();
        private void btnAgregarV_Click(object sender, EventArgs e)
        {
            int valor = 0;
            valor = Convert.ToInt32(id.Next(300, 1000));
            lbl_ID.Text = "V" + valor.ToString();
            if (txtZapatos.Text != "" && txtFecha.Text != "" && txtTotal.Text != "")
            {
                dtw_Ventas.Rows.Add(lbl_ID.Text, txtZapatos.Text, txtFecha.Text, txtTotal.Text);
            }
            txtZapatos.Text = "";
            txtFecha.Text = "";
            txtTotal.Text = "";
            MessageBox.Show($"Datos guardados");
        }

        private void btnModificarV_Click(object sender, EventArgs e)
        {

        }

        private void btnEliminarV_Click(object sender, EventArgs e)
        {

        }

        private void btnBuscarV_Click(object sender, EventArgs e)
        {

        }

        private void btnGuardarV_Click(object sender, EventArgs e)
        {

        }
    }
}
