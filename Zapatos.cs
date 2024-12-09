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
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Media.Media3D;

namespace ConexionSQL
{
    public partial class Zapatos : Form
    {
        
        private Panel leftBorderBtn;
        private IconButton currentBtn;
        private SqlConnection Conexion = new SqlConnection("Data Source=DESKTOP-L2KNQNU\\SQLEXPRESS; Initial Catalog=Inventario_Zapateria; Integrated Security=True");
        private Random id = new Random();

        public Zapatos()
        {
            InitializeComponent();
            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, 60);
            

            //form
            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;

            CargarDatos();

        }
        private void CargarDatos()
        {
            string query = "SELECT * FROM Zapatos";
            SqlDataAdapter da = new SqlDataAdapter(query, Conexion);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dtw_Zapatos.DataSource = dt;
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


        void Limpiar()
        {
            txtCategoria.Clear();
            txtMedida.Clear();
            txtColor.Clear();
            txtMarca.Clear();
            txtMaterial.Clear();
            txtProveedor.Clear();

            btnEliminarZ.Enabled = false;
            btnModificarZ.Enabled = false;
        }
        private void Busqueda(DataGridView d, int col)
        {
            string query = "SELECT * FROM Zapatos WHERE ID_Zapato = @ID_Zapato";
            SqlCommand cmd = new SqlCommand(query, Conexion);
            cmd.Parameters.AddWithValue("@ID_Proveedor", txtBuscarZ.Text.Trim());

            Conexion.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                lbl_ID.Text = reader["ID_Zapato"].ToString();
                txtCategoria.Text = reader["Categoria"].ToString();
            }
            else
            {
                MessageBox.Show("Registro no encontrado.");
            }
            Conexion.Close();
        }
        private void btnAgregarZ_Click(object sender, EventArgs e)
        {
            string Categoria = txtCategoria.Text;
            string Medida = txtMedida.Text;
            string Color = txtColor.Text;
            string Marca = txtMarca.Text;
            string Material = txtMaterial.Text;
            string Proveedor = txtProveedor.Text;

            if (string.IsNullOrEmpty(Categoria) || string.IsNullOrEmpty(Medida) || string.IsNullOrEmpty(Color) || string.IsNullOrEmpty(Marca) || string.IsNullOrEmpty(Material) || string.IsNullOrEmpty(Proveedor))
            {
                MessageBox.Show("Campos incompletos");
                return;
            }

            // Generar un ID único para el nuevo proveedor
            int valor = id.Next(100, 999);
            lbl_ID.Text = "Z" + valor.ToString();

            // Preparar la consulta para insertar el nuevo proveedor en la base de datos
            SqlCommand cmd = new SqlCommand(
                "INSERT INTO Zpatos (No.Zapato, Categoria, Medida, Color, Marca, Material, Proveedor) VALUES (@ID_Zapatos, @Categoria, @Precio, @Medida, @Color, @Marca, @Material, @ID_Proveedor)",
                Conexion);

            // Asignar parámetros a la consulta
            cmd.Parameters.AddWithValue("@ID_Zapato", lbl_ID.Text);
            cmd.Parameters.AddWithValue("@Categoria", Categoria);
            cmd.Parameters.AddWithValue("@Medida", Medida);
            cmd.Parameters.AddWithValue("@Color", Color);
            cmd.Parameters.AddWithValue("@Marca", Marca);
            cmd.Parameters.AddWithValue("@Material", Material);
            cmd.Parameters.AddWithValue("@ID_Proveedor", Proveedor);

            // Ejecutar la consulta para insertar los datos
            Conexion.Open();
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Zapatos agregados correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar los Zapatos: {ex.Message}");
            }
            finally
            {
                Conexion.Close();
            }

            // Recargar los datos en el DataGridView para mostrar el nuevo proveedor
            CargarDatos(); // Este método recarga todos los proveedores
            Limpiar(); // Limpiar los campos de texto
        }

        private void btnModificarZ_Click(object sender, EventArgs e)
        {
            string query = "UPDATE Zapatos SET Categoria = @Categoria, Medida = @Medida, Color = @Color, Marca = @Marca, Material = @Material, Proovedor = @ID_Proveedor WHERE ID_Zapato = @ID_Zapato";
            SqlCommand cmd = new SqlCommand(query, Conexion);

            cmd.Parameters.AddWithValue("@ID_Zapato", lbl_ID.Text);
            cmd.Parameters.AddWithValue("@Categoria", txtCategoria.Text);
            cmd.Parameters.AddWithValue("@Medida", txtMedida.Text);
            cmd.Parameters.AddWithValue("@Color", txtColor.Text);
            cmd.Parameters.AddWithValue("@Marca", txtMarca.Text);
            cmd.Parameters.AddWithValue("@Material", txtMaterial.Text);
            cmd.Parameters.AddWithValue("@ID_Proveedor", txtProveedor.Text);

            Conexion.Open();
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Registro modificado en la base de datos");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al modificar: {ex.Message}");
            }
            finally
            {
                Conexion.Close();
            }

            CargarDatos(); // Recarga los datos después de modificar
            Limpiar();
        }

        private void btnEliminarZ_Click(object sender, EventArgs e)
        {
            string query = "DELETE FROM Zapatos WHERE ID_Zapatos = @ID_Zapatos";
            SqlCommand cmd = new SqlCommand(query, Conexion);

            cmd.Parameters.AddWithValue("@ID_Zapato", lbl_ID.Text);

            Conexion.Open();
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Registro eliminado de la base de datos");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar: {ex.Message}");
            }
            finally
            {
                Conexion.Close();
            }

            CargarDatos(); // Recarga los datos después de eliminar
            Limpiar();
        }

        private void btnBuscarZ_Click(object sender, EventArgs e)
        {
            if (txtBuscarZ.Text != "")
            {
                Busqueda(dtw_Zapatos, 0);
                Busqueda(dtw_Zapatos, 1);
            }
            else
            {
                MessageBox.Show("Error. Intentelo de nuevo :)");
            }
            txtBuscarZ.Clear();

            btnModificarZ.Enabled = true;
            btnEliminarZ.Enabled = true;
        }
    }
}
