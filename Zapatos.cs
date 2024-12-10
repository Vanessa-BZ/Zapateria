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
        private SqlConnection Conexion = new SqlConnection("Data Source=LATPTOP\\SQLSERVEREXPRESS; Initial Catalog=Inventario_Zapateria; Integrated Security=True");  //Salma
        //private SqlConnection Conexion = new SqlConnection("Data Source=DESKTOP-L2KNQNU\\SQLEXPRESS; Initial Catalog=Inventario_Zapateria; Integrated Security=True");  //Vanessita
        private Random id = new Random();
        private int currentIndex = 0;  // Índice del registro actual en los resultados

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

            CargarDatos();  // Cargar los datos en el DataGridView

            // Deshabilitar botones al inicio
            btnModificarZ.Enabled = false;
            btnEliminarZ.Enabled = false;
        }

        private void CargarDatos()
        {
            string query = "SELECT * FROM Zapatos";
            SqlDataAdapter da = new SqlDataAdapter(query, Conexion);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dtw_Zapatos.DataSource = dt;  // Cargar los datos en el DataGridView
        }

        private void Busqueda(DataGridView d, int col)
        {
            string query = "SELECT * FROM Zapatos WHERE ID_Zapato LIKE @ID_Zapato OR Categoria LIKE @Categoria OR Medida LIKE @Medida OR Color LIKE @Color OR Marca LIKE @Marca OR Material LIKE @Material OR ID_Proveedor LIKE @ID_Proveedor";
            SqlCommand cmd = new SqlCommand(query, Conexion);

            cmd.Parameters.AddWithValue("@ID_Zapato", "%" + lbl_ID.Text.Trim() + "%");
            cmd.Parameters.AddWithValue("@Categoria", "%" + txtCategoria.Text.Trim() + "%");
            cmd.Parameters.AddWithValue("@Medida", "%" + txtMedida.Text.Trim() + "%");
            cmd.Parameters.AddWithValue("@Color", "%" + txtColor.Text.Trim() + "%");
            cmd.Parameters.AddWithValue("@Marca", "%" + txtMarca.Text.Trim() + "%");
            cmd.Parameters.AddWithValue("@Material", "%" + txtMaterial.Text.Trim() + "%");
            cmd.Parameters.AddWithValue("@ID_Proveedor", "%" + txtProveedor.Text.Trim() + "%");

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dtw_Zapatos.DataSource = dt;
        }

        private void MostrarRegistro(int index)
        {
            if (dtw_Zapatos.Rows.Count > 0)
            {
                // Obtiene la primera fila del DataGridView
                DataGridViewRow row = dtw_Zapatos.Rows[index];

                lbl_ID.Text = row.Cells["ID_Zapato"].ToString();
                txtCategoria.Text = row.Cells["Categoria"].Value.ToString();
                txtMedida.Text = row.Cells["Medida"].Value.ToString();
                txtColor.Text = row.Cells["Color"].Value.ToString();
                txtMarca.Text = row.Cells["Marca"].Value.ToString();
                txtMaterial.Text = row.Cells["Material"].Value.ToString();
                txtProveedor.Text = row.Cells["ID_Proveedor"].Value.ToString();
            }
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

            // Generar un ID único para el nuevo zapato
            int valor = id.Next(100, 999);
            lbl_ID.Text = "Z" + valor.ToString();

            // Preparar la consulta para insertar el nuevo zapato en la base de datos
            SqlCommand cmd = new SqlCommand(
                "INSERT INTO Zapatos (ID_Zapato, Categoria, Medida, Color, Marca, Material, ID_Proveedor) VALUES (@ID_Zapato, @Categoria, @Medida, @Color, @Marca, @Material, @ID_Proveedor)",
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
                MessageBox.Show("Zapato agregado correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar el zapato: {ex.Message}");
            }
            finally
            {
                Conexion.Close();
            }

            // Recargar los datos en el DataGridView para mostrar el nuevo zapato
            CargarDatos();
            Limpiar();
        }

        private void btnModificarZ_Click(object sender, EventArgs e)
        {
            string query = "UPDATE Zapatos SET Categoria = @Categoria, Medida = @Medida, Color = @Color, Marca = @Marca, Material = @Material, ID_Proveedor = @ID_Proveedor WHERE ID_Zapato = @ID_Zapato";
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

            CargarDatos(); // Recargar los datos después de modificar
            Limpiar();
        }

        private void btnEliminarZ_Click(object sender, EventArgs e)
        {
            string query = "DELETE FROM Zapatos WHERE ID_Zapato = @ID_Zapato";
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

            CargarDatos(); // Recargar los datos después de eliminar
            Limpiar();
        }

        private void btnBuscarZ_Click(object sender, EventArgs e)
        {
            if (txtBuscarZ.Text != "")
            {
                // Realiza la búsqueda
                Busqueda(dtw_Zapatos, 0);

                // Desactiva el botón de agregar para evitar duplicados
                btnAgregarZ.Enabled = false;

                // Activa los botones de modificar y eliminar
                btnModificarZ.Enabled = true;
                btnEliminarZ.Enabled = true;

                // Asegura que el primer registro se cargue en los TextBox
                MostrarRegistro(currentIndex);
            }
            else
            {
                MessageBox.Show("Error. Intentelo de nuevo :)");
            }

            // Limpiar el campo de búsqueda
            txtBuscarZ.Clear();
            btnLimpiarZ.Visible = true;
        }

        private void Limpiar()
        {
            txtCategoria.Clear();
            txtMedida.Clear();
            txtColor.Clear();
            txtMarca.Clear();
            txtMaterial.Clear();
            txtProveedor.Clear();
            lbl_ID.Text = "";
        }

        private void btnLimpiarZ_Click(object sender, EventArgs e)
        {
            Limpiar();
            btnAgregarZ.Enabled = true;
        }

        private void btnAtrasZ_Click(object sender, EventArgs e)
        {
            if (currentIndex > 0)
            {
                currentIndex--;
                MostrarRegistro(currentIndex);

                // Enable/Disable navigation buttons
                btnAtrasZ.Enabled = true;
            }
            else
            {
                btnAtrasZ.Enabled = false;
            }
        }

        private void btnSiguienteZ_Click(object sender, EventArgs e)
        {
            if (currentIndex < dtw_Zapatos.Rows.Count - 1)
            {
                currentIndex++;
                MostrarRegistro(currentIndex);

                btnAtrasZ.Enabled = true;
            }
            else
            {
                btnSiguienteZ.Enabled = false;
            }
        }

        private void btnUltimoZ_Click(object sender, EventArgs e)
        {
            currentIndex = dtw_Zapatos.Rows.Count - 1;
            MostrarRegistro(currentIndex);

            btnSiguienteZ.Enabled = false;
            btnAtrasZ.Enabled = true;
        }

        private void btnPrimerZ_Click(object sender, EventArgs e)
        {
            currentIndex = 0;
            MostrarRegistro(currentIndex);

            btnAtrasZ.Enabled = false;
            btnSiguienteZ.Enabled = true;
        }
    }
}






