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
        private List<DataRow> searchResults = new List<DataRow>();  // Lista para almacenar los resultados de la búsqueda
        private int currentIndex = -1;  // Índice del registro actual en los resultados

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

        private void Busqueda()
        {
            string searchText = txtBuscarZ.Text.Trim();
            string query = "SELECT * FROM Zapatos WHERE Categoria LIKE @SearchText OR Marca LIKE @SearchText OR ID_Proveedor LIKE @SearchText OR Medida LIKE @SearchText OR Color LIKE @SearchText OR Material LIKE @SearchText";

            SqlCommand cmd = new SqlCommand(query, Conexion);
            cmd.Parameters.AddWithValue("@SearchText", "%" + searchText + "%");

            Conexion.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            searchResults.Clear(); // Limpiar resultados anteriores
            while (reader.Read())
            {
                // Agregar cada fila encontrada a la lista de resultados
                DataRow row = ((DataTable)dtw_Zapatos.DataSource).NewRow();
                row["ID_Zapato"] = reader["ID_Zapato"];
                row["Categoria"] = reader["Categoria"];
                row["Medida"] = reader["Medida"];
                row["Color"] = reader["Color"];
                row["Marca"] = reader["Marca"];
                row["Material"] = reader["Material"];
                row["ID_Proveedor"] = reader["ID_Proveedor"];
                searchResults.Add(row);
            }

            if (searchResults.Count > 0)
            {
                currentIndex = 0; // Iniciar en el primer registro
                MostrarRegistro();
                btnModificarZ.Enabled = true;
                btnEliminarZ.Enabled = true;

                // Desactivar el botón de agregar
                btnAgregarZ.Enabled = false;

                // Actualizar el DataGridView con los resultados de la búsqueda
                DataTable searchResultsTable = new DataTable();
                searchResultsTable.Columns.Add("ID_Zapato");
                searchResultsTable.Columns.Add("Categoria");
                searchResultsTable.Columns.Add("Medida");
                searchResultsTable.Columns.Add("Color");
                searchResultsTable.Columns.Add("Marca");
                searchResultsTable.Columns.Add("Material");
                searchResultsTable.Columns.Add("ID_Proveedor");

                foreach (var row in searchResults)
                {
                    searchResultsTable.Rows.Add(row.ItemArray);
                }

                dtw_Zapatos.DataSource = searchResultsTable;  // Actualizar el DataGridView con los resultados de búsqueda
            }
            else
            {
                MessageBox.Show("Registro no encontrado.");
                Limpiar();  // Limpiar los campos de texto si no se encuentra el registro
                btnModificarZ.Enabled = false;
                btnEliminarZ.Enabled = false;
            }
            Conexion.Close();
        }

        private void MostrarRegistro()
        {
            if (currentIndex >= 0 && currentIndex < searchResults.Count)
            {
                var row = searchResults[currentIndex];
                lbl_ID.Text = row["ID_Zapato"].ToString();
                txtCategoria.Text = row["Categoria"].ToString();
                txtMedida.Text = row["Medida"].ToString();
                txtColor.Text = row["Color"].ToString();
                txtMarca.Text = row["Marca"].ToString();
                txtMaterial.Text = row["Material"].ToString();
                txtProveedor.Text = row["ID_Proveedor"].ToString();
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
                Busqueda(); // Realiza la búsqueda cuando el campo no esté vacío
            }
            else
            {
                MessageBox.Show("Error. Intentelo de nuevo :)");
            }
            txtBuscarZ.Clear();  // Limpiar el cuadro de búsqueda después de buscar
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
    }
}






