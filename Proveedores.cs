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
using System.IO;
using System.Data.SqlClient;

namespace ConexionSQL
{
    public partial class Proveedores : Form
    {
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
        SqlConnection Conexion = new SqlConnection("Data Source=; Initial Catalog= Inventario_Zapateria; Integrated Security = True");
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
                currentBtn.ImageAlign = ContentAlignment.MiddleCenter;
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
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.IconColor = Color.White;
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleCenter;
            }
        }
        Random id = new Random();
      
        private void btnAgregarP_Click(object sender, EventArgs e)
        {
            int valor = 0;
            valor = Convert.ToInt32(id.Next(300, 1000));
            lbl_id.Text = "P" + valor.ToString();
            if (txtNombreProv.Text!="" && txtPrecio.Text!="" && txtUnidades.Text!="")
            {                
                dtw_Proovedores.Rows.Add(lbl_id.Text, txtNombreProv.Text, txtPrecio.Text, txtUnidades.Text, CBXcomercio.Text);
            }

            txtNombreProv.Text = "";
            txtPrecio.Text = "";
            txtUnidades.Text = "";
        }

        private void btnModificarP_Click(object sender, EventArgs e)
        {

        }

        private void btnEliminarP_Click(object sender, EventArgs e)
        {

        }

        private void btnBuscarP_Click(object sender, EventArgs e)
        {

        }

        private void btnGuardarP_Click(object sender, EventArgs e)
        {
             SqlCommand Agregar = new SqlCommand("insert into Proovedores values @ID_Proovedor, @Nombre, @Precio, @unidades, @Comercializacion", Conexion);
             Conexion.Open();
        }
    }
}
